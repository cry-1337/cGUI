using cGUI.Event.Abstraction;

namespace cGUI.Events.Models;

public readonly struct PreLayoutEvent(float deltaTime) : IEvent
{
    public readonly float DeltaTime = deltaTime;
}