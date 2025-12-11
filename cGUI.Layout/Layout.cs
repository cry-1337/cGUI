using cGUI.Abstraction.Structs;
using cGUI.Layout.Abstraction;
using System.Collections.Generic;

namespace cGUI.Layout;

public class Layout : ILayout
{
    private readonly List<ILayoutStrategy> m_LayoutStrategies = new(16);
    public void PushStrategy(ILayoutStrategy strategy) => m_LayoutStrategies.Add(strategy);
    public GUIRectangle PerformLayout(GUIRectangle rect, in GUIRectangle parent)
    {
        foreach (var strategy in m_LayoutStrategies)
            rect = strategy.ProcessLayout(rect, parent);

        return rect;
    }
    public void Reset() => m_LayoutStrategies.Clear();
}