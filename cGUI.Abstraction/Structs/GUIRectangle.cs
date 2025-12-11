using cGUI.Abstraction.Interfaces;
using cGUI.Math;

namespace cGUI.Abstraction.Structs;

public partial struct GUIRectangle : IInterpolatable<GUIRectangle>
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

    public readonly GUIPoint Position => new(m_X, m_Y);
    public readonly GUIPoint Size => new(m_Width, m_Height);

    public GUIRectangle(float x, float y, float width, float height)
    {
        m_X = x;
        m_Y = y;
        m_Width = width;
        m_Height = height;
    }

    public GUIRectangle(GUIPoint position, GUIPoint size)
    {
        m_X = position.x;
        m_Y = position.y;
        m_Width = size.x;
        m_Height = size.y;
    }

    public readonly bool Contains(GUIPoint point) => point.x >= m_X && point.y >= m_Y && point.x <= m_Width && point.y <= m_Height;

    public readonly override string ToString() => $"(X:{X}, Y:{Y}, W:{Width}, H:{Height})";

    public GUIRectangle Lerp(GUIRectangle b, float t)
    {
        m_X = GUIMath.Lerp(m_X, b.m_X, t);
        m_Y = GUIMath.Lerp(m_Y, b.m_Y, t);
        m_Width = GUIMath.Lerp(m_Width, b.m_Width, t);
        m_Height = GUIMath.Lerp(m_Height, b.m_Height, t);

        return this;
    }
}
