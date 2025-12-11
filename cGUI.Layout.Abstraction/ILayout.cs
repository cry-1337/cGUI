using cGUI.Abstraction.Structs;

namespace cGUI.Layout.Abstraction;

public interface ILayout
{
    void PushStrategy(in ILayoutStrategy strategy);
    GUIRectangle PerformLayout(GUIRectangle rect, in GUIRectangle parent);
}