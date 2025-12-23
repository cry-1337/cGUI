using cGUI.Event.Abstraction;

namespace cGUI.Events.Models.Input;

public readonly struct MouseWheelEvent(float delta) : IEvent
{
    public readonly float Delta = delta;
}