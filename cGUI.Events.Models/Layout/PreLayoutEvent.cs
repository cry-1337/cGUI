using cGUI.Event.Abstraction;

namespace cGUI.Events.Models.Layout;

public readonly struct PreLayoutEvent(float deltaTime) : IEvent
{
    public readonly float DeltaTime = deltaTime;
}