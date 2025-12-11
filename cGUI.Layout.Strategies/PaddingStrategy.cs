using cGUI.Abstraction.Structs;
using cGUI.Layout.Abstraction;

namespace cGUI.Layout.Strategies;

public readonly struct PaddingStrategy(float padding) : ILayoutStrategy
{
    private readonly float m_Padding = padding;

    public readonly GUIRectangle ProcessLayout(GUIRectangle rect, in GUIRectangle parent)
        => new(parent.X + m_Padding, parent.Y + m_Padding, parent.Width - (2 * m_Padding), parent.Height - (2 * m_Padding));
}
