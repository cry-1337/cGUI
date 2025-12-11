using cGUI.Abstraction.Structs;
using cGUI.Layout.Abstraction;

namespace cGUI.Layout.Strategies;

public struct PaddingStrategy(float padding) : ILayoutStrategy
{
    public float Padding = padding;

    public readonly GUIRectangle ProcessLayout(GUIRectangle rect, in GUIRectangle parent)
        => new(parent.X + Padding, parent.Y + Padding, parent.Width - (2 * Padding), parent.Height - (2 * Padding));
}
