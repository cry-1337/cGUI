using cGUI.Event.Abstraction;
using cGUI.Layout.Abstraction;

namespace cGUI.Events.Models.Layout;

public readonly struct LayoutEvent(IElementLayout layout, bool force = false) : IEvent
{
    public readonly IElementLayout Layout = layout;
    public readonly bool Force = force;
}