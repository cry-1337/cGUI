using cGUI.Abstraction.Interfaces;

namespace cGUI.Abstraction.Structs;

public readonly struct HitTestResult(IElement element, GUIPoint point)
{
    public readonly IElement m_Element = element;
    public readonly GUIPoint m_Point = point;
}
