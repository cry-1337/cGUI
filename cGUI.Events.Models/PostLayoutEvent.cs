using cGUI.Event.Abstraction;
using cGUI.Layout.Abstraction;

namespace cGUI.Events.Models;

public readonly struct PostLayoutEvent(IElementLayout layout) : IEvent
{
    public readonly IElementLayout Layout = layout;
}