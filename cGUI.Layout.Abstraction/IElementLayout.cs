using cGUI.Abstraction.Interfaces;

namespace cGUI.Layout.Abstraction;

public interface IElementLayout : IResetable
{
    void PushNode(ref LayoutNode node);
    void PerformLayout(LayoutContext context);
}