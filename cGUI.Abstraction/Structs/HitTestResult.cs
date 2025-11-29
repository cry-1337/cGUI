using cGUI.Abstraction.Interfaces;

namespace cGUI.Abstraction.Structs;

public readonly struct HitTestResult(IElement element, Point point)
{
    public readonly IElement m_Element = element;
    public readonly Point m_Point = point;
}
