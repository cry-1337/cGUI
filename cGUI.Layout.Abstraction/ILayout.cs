using cGUI.Abstraction.Structs;
using System.Collections.Generic;

namespace cGUI.Layout.Abstraction;

public interface ILayout
{
    GUIRectangle PerformLayout(GUIRectangle rect, in GUIRectangle parent, IEnumerable<ILayoutStrategy> strategies);
}