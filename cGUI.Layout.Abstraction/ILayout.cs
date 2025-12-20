using cGUI.Abstraction.Interfaces;
using cGUI.Abstraction.Structs;

namespace cGUI.Layout.Abstraction;

public interface ILayout : IResetable
{
    void PushNode(LayoutNode node);
    void PerformLayout(in GUIRectangle screenBounds);
}