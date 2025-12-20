using cGUI.Abstraction.Interfaces;
using cGUI.Math;
using System;

namespace cGUI.Abstraction.Structs;

public partial struct GUIRectangle : IEquatable<GUIRectangle>, IInterpolatable<GUIRectangle>
{
    private float m_X, m_Y, m_Width, m_Height;

    public float X
    {
        readonly get => m_X; set => m_X = value;
    }
    public float Y
    {
        readonly get => m_Y; set => m_Y = value;
    }
    public float Width
    {
        readonly get => m_Width; set => m_Width = value;
    }
    public float Height
    {
        readonly get => m_Height; set => m_Height = value;
    }

    public readonly GUIVector2 Center => new(m_X + m_Width / 2f, m_Y + m_Height / 2f);

    public readonly GUIVector2 Position => new(m_X, m_Y);
    public readonly GUIVector2 Size => new(m_Width, m_Height);

    public static GUIRectangle Zero => new();

    public GUIRectangle(float x, float y, float width, float height)
    {
        m_X = x;
        m_Y = y;
        m_Width = width;
        m_Height = height;
    }

    public GUIRectangle(GUIPoint position, GUIPoint size)
    {
        m_X = position.X;
        m_Y = position.Y;
        m_Width = size.X;
        m_Height = size.Y;
    }

    public GUIRectangle(GUIVector2 position, GUIVector2 size)
    {
        m_X = position.X;
        m_Y = position.Y;
        m_Width = size.X;
        m_Height = size.Y;
    }

    public GUIRectangle(float side)
    {
        m_X = side;
        m_Y = side;
        m_Width = side;
        m_Height = side;
    }

    public readonly bool Contains(GUIPoint point) => point.X >= m_X && point.Y >= m_Y && point.X <= m_Width && point.Y <= m_Height;
    public bool Equals(GUIRectangle other) => other.X == X && other.Y == Y && other.Width == Width && other.Height == Height;

    public readonly override string ToString() => $"(X:{X}, Y:{Y}, W:{Width}, H:{Height})";

    public GUIVector2 GetLocalPosition(float x, float y) => new(GUIMath.LerpUnclamped(X, X + Width, x), GUIMath.LerpUnclamped(Y, Y + Height, y));
    public GUIVector2 GetLocalPosition(GUIVector2 point) => new(GUIMath.LerpUnclamped(X, X + Width, point.X), GUIMath.LerpUnclamped(Y, Y + Height, point.Y));
    public GUIVector2 GetNormalPosition(float x, float y) => new(GUIMath.InverseLerp(X, X + Width, x), GUIMath.InverseLerp(Y, Y + Height, y));
    public GUIVector2 GetNormalPosition(GUIVector2 point) => new(GUIMath.InverseLerp(X, X + Width, point.X), GUIMath.InverseLerp(Y, Y + Height, point.Y));

    public GUIRectangle Lerp(GUIRectangle b, float t)
    {
        m_X = GUIMath.Lerp(m_X, b.m_X, t);
        m_Y = GUIMath.Lerp(m_Y, b.m_Y, t);
        m_Width = GUIMath.Lerp(m_Width, b.m_Width, t);
        m_Height = GUIMath.Lerp(m_Height, b.m_Height, t);

        return this;
    }
}
