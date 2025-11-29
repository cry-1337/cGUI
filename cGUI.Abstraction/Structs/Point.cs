namespace cGUI.Abstraction.Structs;

public readonly struct Point(float x, float y)
{
    public readonly float m_X = x, m_Y = y;

    public Point ConvertToLocalPoint(Rectangle bounds) => new(m_X - bounds.m_X, m_Y - bounds.m_Y);
}
