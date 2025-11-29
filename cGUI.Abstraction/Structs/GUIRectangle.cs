namespace cGUI.Abstraction.Structs;

public readonly struct Rectangle
{
    public readonly float m_X, m_Y, m_Width, m_Height;

    public GUIPoint Point => new();

    public readonly bool Contains(GUIPoint point) => point.m_X >= m_X && point.m_Y >= m_Y && point.m_X <= m_Width && point.m_Y <= m_Height;
}
