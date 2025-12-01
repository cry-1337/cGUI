using cGUI.Event.Abstraction;

namespace cGUI.Events.Models;

public struct PreRenderEvent(float deltaTime) : IEvent
{
    public float DeltaTime = deltaTime;
}