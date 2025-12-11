using cGUI.Abstraction.Interfaces;
using cGUI.Abstraction.Structs;
using cGUI.Layout.Abstraction;

namespace cGUI.Layout.Strategies;

public struct HorizontalLayoutStrategy(float gap) : ILayoutStrategy, IResetable
{
    private float m_CurrentX = -gap;
    private readonly float m_Gap = gap;

    public GUIRectangle ProcessLayout(GUIRectangle rect, in GUIRectangle parent)
    {
        rect.X = parent.X + m_CurrentX + m_Gap;
        rect.Y = parent.Y;

        m_CurrentX += rect.Width + m_Gap;

        return rect;
    }

    public void Reset() => m_CurrentX = -m_Gap;
}
