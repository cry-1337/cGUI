using cGUI.Abstraction.Structs;

namespace cGUI.Layout.Abstraction;

public interface ILayoutOption
{
    GUIRectangle ProcessLayout(GUIRectangle desiredRect, ref LayoutContext state);
}