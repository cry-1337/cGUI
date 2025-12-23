using cGUI.Event.Abstraction;
using cGUI.Render.Abstraction;

namespace cGUI.Events.Models.Render;

public readonly struct RenderEvent(IRender render) : IEvent
{
    public readonly IRender Render = render;
}