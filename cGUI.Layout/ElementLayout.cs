using cGUI.Layout.Abstraction;
using System.Collections.Generic;

namespace cGUI.Layout;

public class ElementLayout : IElementLayout
{
    private List<LayoutNode> m_Nodes = new(32);

    public void PushNode(LayoutNode node) => m_Nodes.Add(node);

    public void PerformLayout(LayoutContext layoutContext)
    {
        foreach (var node in m_Nodes)
            foreach (var strategy in node.Strategies)
                node.Element.Bounds = strategy.ProcessLayout(node.DesiredRect, ref layoutContext);
    }

    public void Reset() => m_Nodes.Clear();
}