using cGUI.Abstraction.Structs;
using cGUI.Event.Abstraction;

namespace cGUI.Events.Models.Input;

public struct MouseMoveEvent(GUIVector2 globalMousePosition, GUIVector2 mouseDelta) : IEvent
{
    public GUIVector2 GlobalMousePosition = globalMousePosition;
    public GUIVector2 MouseDelta = mouseDelta;
}