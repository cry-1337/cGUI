using cGUI.Abstraction.Structs;
using cGUI.Layout.Abstraction;

namespace cGUI.Layout;

public class Layout : ILayout
{
    public GUIRectangle PerformLayout(GUIRectangle rect, in GUIRectangle parent, IEnumerable<ILayoutStrategy> strategies)
    {
        foreach (var strategy in strategies)
            rect = strategy.ProcessLayout(rect, parent);

        return rect;
    }
}