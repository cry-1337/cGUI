using cGUI.Abstraction.Structs;
using cGUI.Layout.Abstraction;
using System.Collections.Generic;

namespace cGUI.Layout;

public class Layout : ILayout
{
    private List<LayoutNode> m_Nodes = new(32);

    public void PushNode(LayoutNode node) => m_Nodes.Add(node);

    public void PerformLayout(in GUIRectangle screenBounds)
    {
        var layoutState = new LayoutState(screenBounds);

        foreach (var node in m_Nodes)
            foreach (var strategy in node.Strategies)
                node.Element.Bounds = strategy.ProcessLayout(node.DesiredRect, ref layoutState);
    }

    public void Reset() => m_Nodes.Clear();
}