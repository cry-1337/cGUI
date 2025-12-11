using cGUI.Abstraction.Structs;
using cGUI.Layout.Abstraction;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace cGUI.Layout;

public class Layout : ILayout
{
    private readonly List<ILayoutStrategy> m_LayoutStrategies = new(16);
    public void PushStrategy(in ILayoutStrategy strategy) => m_LayoutStrategies.Add(strategy);
    public GUIRectangle PerformLayout(GUIRectangle rect, in GUIRectangle parent)
    {
        foreach (var strategy in m_LayoutStrategies)
            rect = strategy.ProcessLayout(rect, parent);
        m_LayoutStrategies.Clear();

        return rect;
    }
}