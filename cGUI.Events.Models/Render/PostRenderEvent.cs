using cGUI.Event.Abstraction;
using cGUI.Render.Abstraction;

namespace cGUI.Events.Models.Render;

public readonly struct PostRenderEvent(IRender render, float deltaTime) : IEvent
{
    public readonly IRender Render = render;
    public readonly float DeltaTime = deltaTime;
}