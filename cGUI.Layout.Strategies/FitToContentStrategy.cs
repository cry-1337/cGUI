using cGUI.Abstraction.Structs;
using cGUI.Layout.Abstraction;

namespace cGUI.Layout.Strategies;

public struct FitToContentStrategy : ILayoutStrategy
{
    public GUIRectangle ProcessLayout(GUIRectangle rect, in GUIRectangle parent)
    {
        return GUIRectangle.Zero;
    }
}