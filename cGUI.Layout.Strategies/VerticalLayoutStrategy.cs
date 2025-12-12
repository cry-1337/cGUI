using cGUI.Abstraction.Interfaces;
using cGUI.Abstraction.Structs;
using cGUI.Layout.Abstraction;

namespace cGUI.Layout.Strategies;

public struct VerticalLayoutStrategy(float gap) : ILayoutStrategy, IResetable
{
    private float m_CurrentY = 0f;
    private readonly float m_Gap = gap;

    public GUIRectangle ProcessLayout(GUIRectangle rect, in GUIRectangle parent)
    {
        rect.Y = parent.Y + m_CurrentY;
        m_CurrentY += rect.Height + m_Gap;

        return rect;
    }

    public void Reset() => m_CurrentY = -m_Gap;
}