using cGUI.Abstraction.Structs;
using cGUI.Event.Abstraction;

namespace cGUI.Events.Models.Input;

public readonly struct MouseKeyDownEvent(GUIVector2 globalMousePosition, int key) : IEvent
{
    public readonly GUIVector2 GlobalMousePosition = globalMousePosition;
    public readonly int Key = key;
}