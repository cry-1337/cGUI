using cGUI.Abstraction.Interfaces;
using cGUI.Abstraction.Structs;
using System.Collections.Generic;

namespace cGUI.Layout.Abstraction;

public struct LayoutNode(IElement element, GUIRectangle desiredRect, List<ILayoutStrategy> strategies)
{
    public IElement Element = element;
    public GUIRectangle DesiredRect = desiredRect;
    public List<ILayoutStrategy> Strategies = strategies;
}