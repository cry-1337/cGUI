namespace cGUI.Abstraction.Structs;

public readonly partial struct GUIPoint(float x, float y)
{
    public readonly float m_X = x, m_Y = y;

    public GUIPoint ConvertToLocalPoint(GUIRectangle bounds) => new(m_X - bounds.X, m_Y - bounds.Y);
}
