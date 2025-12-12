using cGUI.Abstraction.Structs;
using cGUI.Layout.Abstraction;

namespace cGUI.Layout.Strategies;

public readonly struct StrechHeightStrategy : ILayoutStrategy
{
    public GUIRectangle ProcessLayout(GUIRectangle rect, in GUIRectangle parent)
        => rect with { Y = parent.Y, Height = parent.Height };
}