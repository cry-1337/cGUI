using cGUI.Abstraction.Structs;

namespace cGUI.Layout.Abstraction;

public class LayoutState(GUIRectangle remainingBounds)
{
    public GUIRectangle RemainingBounds = remainingBounds;
}