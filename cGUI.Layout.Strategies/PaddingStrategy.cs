using cGUI.Abstraction.Structs;
using cGUI.Layout.Abstraction;

namespace cGUI.Layout.Strategies;

public struct PaddingStrategy(GUIRectangle padding) : ILayoutStrategy
{
    public GUIRectangle Padding = padding;

    public readonly GUIRectangle ProcessLayout(GUIRectangle rect, in GUIRectangle parent)
        => new(rect.X + Padding.X, rect.Y + Padding.Y, rect.Width - (Padding.X + Padding.Width), rect.Height - (Padding.Y + Padding.Height));
}
