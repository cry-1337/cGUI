using cGUI.Abstraction.Structs;
using cGUI.Layout.Abstraction;

namespace cGUI.Elements.Models;

public struct ElementOption
{
    public GUIRectangle DesiredRect;
    public ILayoutOption[] LayoutOptions;

    public GUIColor? Color;

    public GUIColor? ColorTopLeft;
    public GUIColor? ColorTopRight;
    public GUIColor? ColorBotLeft;
    public GUIColor? ColorBotRight;
}