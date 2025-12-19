using cGUI.Abstraction.Structs;

namespace cGUI.Layout.Abstraction;

public interface ILayoutStrategy
{
    GUIRectangle ProcessLayout(GUIRectangle desiredRect, LayoutState state, out LayoutState newState);
}