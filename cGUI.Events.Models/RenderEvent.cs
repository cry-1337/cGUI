using cGUI.Event.Abstraction;
using cGUI.Render.Abstraction;

namespace cGUI.Events.Models;

public class RenderEvent(IRender render) : IEvent
{
    public IRender Render { get; set; } = render;
}