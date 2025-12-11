using cGUI.Abstraction.Interfaces;

namespace cGUI.Abstraction.Structs;

public readonly partial struct HitTestResult(IElement element, GUIPoint point)
{
    public readonly IElement m_Element = element;
    public readonly GUIPoint m_Point = point;

    public readonly override string ToString() => $"(Element:{m_Element.Id}, Point:{m_Point})";
}
