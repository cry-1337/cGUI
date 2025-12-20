using cGUI.Abstraction.Structs;

namespace cGUI.Layout.Abstraction;

public struct LayoutContext()
{
    public GUIRectangle ParentRect;
    public GUIRectangle RemainingRect;
    public GUIVector2 CurrentOffset;
    public int ElementsLeft;
}