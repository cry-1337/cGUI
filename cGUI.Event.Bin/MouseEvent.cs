using cGUI.Abstraction.Structs;
using cGUI.Event.Abstraction;

namespace cGUI.Event.Bin;

public enum EMouseClickType
{
    Left,
    Middle,
    Right,
    XButton1,
    XButton2
}

public struct MouseEvent(GUIPoint point, EMouseClickType clickType) : IEvent
{
    public readonly GUIPoint Point => point;
    public readonly EMouseClickType ClickType => clickType;
}
