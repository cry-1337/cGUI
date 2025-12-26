using cGUI.Abstraction.Interfaces;
using cGUI.Abstraction.Structs;

namespace cGUI.Layout.Abstraction;

public struct LayoutNode(IElement element, GUIRectangle desiredRect, ILayoutOption[] strategies)
{
    public IElement Element = element;
    public GUIRectangle DesiredRect = desiredRect;
    public ILayoutOption[] Strategies = strategies;
}