using cGUI.Event.Abstraction;
using cGUI.Layout.Abstraction;

namespace cGUI.Events.Models;

public readonly struct LayoutEvent(ILayout layout) : IEvent
{
    public readonly ILayout Layout = layout;
}