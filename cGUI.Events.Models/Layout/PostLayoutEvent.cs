using cGUI.Event.Abstraction;
using cGUI.Layout.Abstraction;

namespace cGUI.Events.Models.Layout;

public readonly struct PostLayoutEvent(IElementLayout layout, float deltaTime) : IEvent
{
    public readonly IElementLayout Layout = layout;
    public readonly float DeltaTime = deltaTime;
}