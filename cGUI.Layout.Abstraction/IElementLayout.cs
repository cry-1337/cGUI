using cGUI.Abstraction.Interfaces;

namespace cGUI.Layout.Abstraction;

public interface IElementLayout : IResetable
{
    void PushNode(LayoutNode node);
    void PerformLayout(LayoutContext context);
}