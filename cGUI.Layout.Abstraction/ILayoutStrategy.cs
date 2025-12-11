using cGUI.Abstraction.Structs;

namespace cGUI.Layout.Abstraction;

public interface ILayoutStrategy
{
    GUIRectangle ProcessLayout(GUIRectangle rect, in GUIRectangle parent);
}