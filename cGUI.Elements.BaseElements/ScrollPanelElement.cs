using System.Collections.Generic;
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
using cGUI.Visual;

namespace cGUI.Elements.BaseElements;

public class ScrollPanelElement : VisualContainer<VisualElement>, IEventHandler<RenderEvent>, IEventMicroController<RenderEvent>, IEventHandler<LayoutEvent>, IEventMicroController<LayoutEvent>, IEventHandler<PostLayoutEvent>, IEventHandler<PreRenderEvent>, IScrollable, IEventsHandler
{
    private readonly GUIColor[] m_Color;
    private readonly IMeshRenderContext<UnityMeshData> m_Context = new UnityMeshRenderContext();
    private LayoutNode m_Node;

    public readonly IElementLayout m_ChildLayout = new Layout.ElementLayout();

    public float PaddingLeft { get; set; }
    public float PaddingTop { get; set; }
    public float PaddingRight { get; set; }
    public float PaddingBottom { get; set; }

    public bool SupportsScroll { get; set; } = true;
    public float ScrollY { get; set; } = 0f;
    public float MaxScroll { get; set; } = 0f;

    public ScrollPanelElement(string id, ElementOption options, float padding = 0f) : base(id)
    {
        GUIAssert.IsNull(options.DesiredRect, $"DesiredRect is null in {id}");
        GUIAssert.IsNull(options.Color, $"Color is null in {id}");

        PaddingLeft = PaddingTop = PaddingRight = PaddingBottom = padding;
        IsActive = true;
        IsHittable = false;

        m_Color = options.Color.ToQuadColors();
        m_Node = new LayoutNode(this, options.DesiredRect, options.LayoutOptions);
    }

    public void SetColor(GUIColor[] colorQuad)
    {
        if (colorQuad != null && colorQuad.Length == 4)
        {
            m_Color[0] = colorQuad[0];
            m_Color[1] = colorQuad[1];
            m_Color[2] = colorQuad[2];
            m_Color[3] = colorQuad[3];
        }
    }

    public void ConstrainScroll()
    {
        ScrollY = GUIMath.Clamp(ScrollY, 0f, MaxScroll);
    }

    public bool IsElementClipped(VisualElement element)
    {
        if (element == null) return false;
        float viewTop = Bounds.Y + Bounds.Height - PaddingTop;
        float viewBottom = Bounds.Y + PaddingBottom;

        GUIRectangle elBounds = element.Bounds;
        float elTop = elBounds.Y + elBounds.Height;
        float elBottom = elBounds.Y;

        return elBottom >= viewTop || elTop <= viewBottom;
    }

    void IEventsHandler.HandleEvents<TEvent>(in TEvent reason)
    {
        if (this is IEventHandler<TEvent> containerHandler) containerHandler.Handle(reason);

        for (int i = 0; i < Count; i++)
        {
            var element = Find(i);
            if (element == null) continue;
            if (element is not IEventHandler<TEvent> elementHandler) continue;

            if (SupportsScroll && (reason is PreRenderEvent || reason is RenderEvent || reason is Events.Models.Input.MouseMoveEvent || reason is Events.Models.Input.MouseKeyDownEvent || reason is Events.Models.Input.MouseKeyUpEvent))
            {
                if (IsElementClipped(element)) continue;
            }

            if (element is IEventMicroController<TEvent> microController && microController.GetEvent(reason)) elementHandler.Handle(reason);
            else if (element is not IEventMicroController<TEvent>) elementHandler.Handle(reason);
        }
    }

    bool IEventHandler<RenderEvent>.Handle(RenderEvent reason)
    {
        reason.Render.PushMesh(m_Context);
        m_Context.Clear();
        return IsActive;
    }

    bool IEventHandler<LayoutEvent>.Handle(LayoutEvent reason)
    {
        reason.Layout.PushNode(m_Node);
        return IsActive;
    }

    bool IEventHandler<PostLayoutEvent>.Handle(PostLayoutEvent reason)
    {
        var localContext = new LayoutContext
        {
            ParentRect = Bounds,
            RemainingRect = new GUIRectangle(
                Bounds.X + PaddingLeft,
                Bounds.Y + PaddingBottom,
                Bounds.Width - PaddingLeft - PaddingRight,
                Bounds.Height - PaddingTop - PaddingBottom
            ),
            CurrentOffset = new GUIVector2(0, 0),
            ElementsLeft = 0
        };

        m_ChildLayout.PerformLayout(localContext, overrideElementsCount: true);

        if (SupportsScroll)
        {
            float minY = float.MaxValue;
            float maxY = float.MinValue;
            bool hasChildren = false;

            for (int i = 0; i < Count; i++)
            {
                var child = Find(i);
                if (child == null || !child.IsActive) continue;
                hasChildren = true;
                if (child.Bounds.Y < minY) minY = child.Bounds.Y;
                if (child.Bounds.Y + child.Bounds.Height > maxY) maxY = child.Bounds.Y + child.Bounds.Height;
            }

            float viewportHeight = Bounds.Height - PaddingTop - PaddingBottom;
            float viewTop = Bounds.Y + Bounds.Height - PaddingTop;
            float contentHeight = hasChildren ? (viewTop - minY + 40f) : 0f;
            MaxScroll = GUIMath.Max(0f, contentHeight - viewportHeight);
            ScrollY = GUIMath.Clamp(ScrollY, 0f, MaxScroll);

            if (ScrollY > 0f)
            {
                for (int i = 0; i < Count; i++)
                {
                    var child = Find(i);
                    if (child == null) continue;
                    child.Bounds = new GUIRectangle(
                        child.Bounds.X,
                        child.Bounds.Y + ScrollY,
                        child.Bounds.Width,
                        child.Bounds.Height
                    );
                }
            }
        }

        return IsActive;
    }

    bool IEventHandler<PreRenderEvent>.Handle(PreRenderEvent reason)
    {
        m_Context.Clear();
        var meshData = new UnityMeshData(GUIGlobals.GlobalMaterial!);
        GUIRectangle drawBounds = Bounds;
        
        var parent = Parent;
        while (parent != null)
        {
            if (parent is IScrollable scrollable && scrollable.SupportsScroll)
            {
                var parentBounds = parent.Bounds;
                float x1 = GUIMath.Max(drawBounds.X, parentBounds.X);
                float y1 = GUIMath.Max(drawBounds.Y, parentBounds.Y);
                float x2 = GUIMath.Min(drawBounds.X + drawBounds.Width, parentBounds.X + parentBounds.Width);
                float y2 = GUIMath.Min(drawBounds.Y + drawBounds.Height, parentBounds.Y + parentBounds.Height);
                
                float w = GUIMath.Max(0f, x2 - x1);
                float h = GUIMath.Max(0f, y2 - y1);
                
                drawBounds = new GUIRectangle(x1, y1, w, h);
            }
            parent = parent.Parent;
        }

        if (drawBounds.Width > 0 && drawBounds.Height > 0)
        {
            m_Context.AddRect(drawBounds, m_Color[0], m_Color[1], m_Color[2], m_Color[3], ref meshData);
        }
        return IsActive;
    }

    bool IEventMicroController<RenderEvent>.GetEvent(RenderEvent reason) => IsActive;
    bool IEventMicroController<LayoutEvent>.GetEvent(LayoutEvent reason) => IsActive;
}
