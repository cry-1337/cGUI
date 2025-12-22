using cGUI.Abstraction.Structs;
using cGUI.Layout.Abstraction;

namespace cGUI.Layout.Options;

public struct ChangeRectOption(GUIRectangle rect) : ILayoutOption
{
    public readonly GUIRectangle ProcessLayout(GUIRectangle desiredRect, ref LayoutContext state)
        => rect;
}