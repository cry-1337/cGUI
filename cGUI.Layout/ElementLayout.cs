using cGUI.Abstraction.Structs;
using cGUI.Layout.Abstraction;
using System.Collections.Generic;

namespace cGUI.Layout;

public class ElementLayout : IElementLayout
{
    private readonly List<LayoutNode> m_Nodes = new(32);

    public void PushNode(in LayoutNode node) => m_Nodes.Add(node);

    public void PerformLayout(LayoutContext layoutContext)
    {
        foreach (var node in m_Nodes)
        {
            GUIRectangle currentRect = node.DesiredRect;

            foreach (var strategy in node.Strategies)
                currentRect = strategy.ProcessLayout(currentRect, ref layoutContext);

            node.Element.Bounds = currentRect;
        }
    }

    public void Reset() => m_Nodes.Clear();
}