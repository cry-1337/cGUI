using cGUI.Abstraction.Structs;
using cGUI.Layout.Abstraction;

namespace cGUI.Elements.Models;

public struct ElementOption()
{
    public GUIRectangle DesiredRect;
    public ElementColor Color;

    public ILayoutOption[] LayoutOptions = [];
}