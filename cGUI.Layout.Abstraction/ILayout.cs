using cGUI.Abstraction.Interfaces;
using cGUI.Abstraction.Structs;

namespace cGUI.Layout.Abstraction;

public interface ILayout : IResetable
{
    void PushStrategy(ILayoutStrategy strategy);
    GUIRectangle PerformLayout(GUIRectangle rect, in GUIRectangle parent);
}