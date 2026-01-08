using cGUI.Abstraction.Interfaces;

namespace cGUI.Layout.Abstraction;

public interface IElementLayout : IResetable
{
    void PushNode(in LayoutNode node);
    void PerformLayout(LayoutContext context, bool overrideElementsCount = false);
}