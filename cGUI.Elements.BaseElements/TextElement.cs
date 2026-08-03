using System;
using cGUI.Abstraction;
using cGUI.Abstraction.Interfaces;
using cGUI.Abstraction.Structs;
using cGUI.Assert;
using cGUI.Elements.Globals;
using cGUI.Elements.Models;
using cGUI.Event.Abstraction;
using cGUI.Events.Models.Layout;
using cGUI.Events.Models.Render;
using cGUI.Layout.Abstraction;
using cGUI.Math;
using cGUI.Render.Abstraction;
using cGUI.Unity.Render.Abstraction;
using cGUI.Unity.Render.Contexts;
using cGUI.Unity.Render.Extensions;

namespace cGUI.Elements.BaseElements;

public class TextElement : BaseElement, IEventHandler<PreRenderEvent>
{
    private readonly IMeshRenderContext<UnityMeshData> m_Context = new UnityMeshRenderContext();
    private LayoutNode m_Node;
    private char[] m_TextBuffer = new char[256];
    private int m_TextLength = 0;

    public FontAtlas FontAtlas { get; set; }
    public GUIColor TextColor { get; set; }
    public float FontSize { get; set; } = 14f;

    public TextElement(string id, ElementOption options, FontAtlas fontAtlas, string initialText = "") : base(id)
    {
        GUIAssert.IsNull(options.DesiredRect, $"DesiredRect is null in {id}");
        GUIAssert.IsNull(options.Color, $"Color is null in {id}");

        IsActive = true;
        IsHittable = false;

        FontAtlas = fontAtlas;
        TextColor = options.Color.ToQuadColors()[0];
        m_Node = new LayoutNode(this, options.DesiredRect, options.LayoutOptions);

        SetText(initialText);
    }

    /// <summary>
    /// Sets text from string.
    /// </summary>
    public void SetText(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            m_TextLength = 0;
            return;
        }

        if (text.Length > m_TextBuffer.Length)
        {
            Array.Resize(ref m_TextBuffer, text.Length + 64);
        }

        text.CopyTo(0, m_TextBuffer, 0, text.Length);
        m_TextLength = text.Length;
    }

    /// <summary>
    /// Sets text from character array without string allocation.
    /// </summary>
    public void SetText(char[] buffer, int length)
    {
        SetText(buffer, 0, length);
    }

    /// <summary>
    /// Sets text from character array range without string allocation.
    /// </summary>
    public void SetText(char[] buffer, int startIndex, int length)
    {
        if (buffer == null || length <= 0)
        {
            m_TextLength = 0;
            return;
        }

        if (length > m_TextBuffer.Length)
        {
            Array.Resize(ref m_TextBuffer, length + 64);
        }

        Array.Copy(buffer, startIndex, m_TextBuffer, 0, length);
        m_TextLength = length;
    }

    /// <summary>
    /// Formats integer into text buffer without string allocation (Zero-GC).
    /// </summary>
    public void SetText(int value)
    {
        if (value == 0)
        {
            m_TextBuffer[0] = '0';
            m_TextLength = 1;
            return;
        }

        bool isNegative = value < 0;
        long val = isNegative ? -(long)value : value;

        int digits = 0;
        long temp = val;
        while (temp > 0)
        {
            digits++;
            temp /= 10;
        }

        if (isNegative) digits++;

        if (digits > m_TextBuffer.Length)
        {
            Array.Resize(ref m_TextBuffer, digits + 64);
        }

        int index = digits - 1;
        while (val > 0)
        {
            m_TextBuffer[index--] = (char)('0' + (val % 10));
            val /= 10;
        }

        if (isNegative)
        {
            m_TextBuffer[0] = '-';
        }

        m_TextLength = digits;
    }

    public override void OnRender(RenderEvent reason)
    {
        reason.Render.PushMesh(m_Context);
        m_Context.Clear();
    }

    public override void OnLayout(LayoutEvent reason)
    {
        reason.Layout.PushNode(m_Node);
    }

    bool IEventHandler<PreRenderEvent>.Handle(PreRenderEvent reason)
    {
        m_Context.Clear();
        if (m_TextLength == 0 || FontAtlas == null) return IsActive;

        var meshData = new UnityMeshData(GUIGlobals.GlobalMaterial!);
        GUIRectangle drawBounds = ClippingUtility.GetClippedBounds(this, Bounds);

        if (drawBounds.Width <= 0 || drawBounds.Height <= 0) return IsActive;

        float scale = FontSize > 0 ? FontSize / FontAtlas.FontSize : 1f;
        float currentX = Bounds.X;
        float currentY = Bounds.Y;

        for (int i = 0; i < m_TextLength; i++)
        {
            char ch = m_TextBuffer[i];
            if (ch == '\n')
            {
                currentX = Bounds.X;
                currentY -= FontAtlas.LineHeight * scale;
                continue;
            }

            if (!FontAtlas.TryGetCharacter(ch, out var fontChar)) continue;

            float charW = fontChar.Width * scale;
            float charH = fontChar.Height * scale;
            float charX = currentX + fontChar.OffsetX * scale;
            float charY = currentY + fontChar.OffsetY * scale;

            var charRect = new GUIRectangle(charX, charY, charW, charH);

            // Scissor / clipping check for character rect vs drawBounds
            float cx1 = GUIMath.Max(charRect.X, drawBounds.X);
            float cy1 = GUIMath.Max(charRect.Y, drawBounds.Y);
            float cx2 = GUIMath.Min(charRect.X + charRect.Width, drawBounds.X + drawBounds.Width);
            float cy2 = GUIMath.Min(charRect.Y + charRect.Height, drawBounds.Y + drawBounds.Height);

            float cw = GUIMath.Max(0f, cx2 - cx1);
            float chHeight = GUIMath.Max(0f, cy2 - cy1);

            if (cw > 0 && chHeight > 0)
            {
                var clippedCharRect = new GUIRectangle(cx1, cy1, cw, chHeight);
                
                // Proportional UV clipping so letters don't squish when scrolling out of bounds
                float u1 = fontChar.UVRect.X + (cx1 - charX) / charW * fontChar.UVRect.Width;
                float v1 = fontChar.UVRect.Y + (cy1 - charY) / charH * fontChar.UVRect.Height;
                float u2 = fontChar.UVRect.X + (cx2 - charX) / charW * fontChar.UVRect.Width;
                float v2 = fontChar.UVRect.Y + (cy2 - charY) / charH * fontChar.UVRect.Height;

                var clippedUVRect = new GUIRectangle(u1, v1, u2 - u1, v2 - v1);
                m_Context.AddRectWithUV(clippedCharRect, clippedUVRect, TextColor, ref meshData);
            }

            currentX += fontChar.AdvanceX * scale;
        }

        return IsActive;
    }
}
