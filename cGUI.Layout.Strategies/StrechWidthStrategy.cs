using cGUI.Abstraction.Structs;
using cGUI.Layout.Abstraction;

namespace cGUI.Layout.Strategies;

public readonly struct StrechWidthStrategy : ILayoutStrategy
{
    public GUIRectangle ProcessLayout(GUIRectangle rect, in GUIRectangle parent)
        => rect with { X = parent.X, Width = parent.Width };
}