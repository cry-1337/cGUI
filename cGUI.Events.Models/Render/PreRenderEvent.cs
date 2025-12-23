using cGUI.Event.Abstraction;

namespace cGUI.Events.Models.Render;

public readonly struct PreRenderEvent(float deltaTime) : IEvent
{
    public readonly float DeltaTime = deltaTime;
}