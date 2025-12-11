using cGUI.Abstraction.Structs;
using cGUI.Layout.Abstraction;

namespace cGUI.Layout.Strategies;

public struct PaddingStrategy(float padding) : ILayoutStrategy
{
    public float Padding = padding;

    public readonly GUIRectangle ProcessLayout(GUIRectangle rect, in GUIRectangle parent)
        => rect with { X = parent.X + Padding, Y = parent.Y + Padding, Width = parent.Width - (2 * Padding), Height = parent.Height - (2 * Padding) };
}
