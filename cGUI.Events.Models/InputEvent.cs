using cGUI.Abstraction.Structs;
using cGUI.Event.Abstraction;

namespace cGUI.Events.Models;

public abstract class InputEvent(GUIVector2 globalMousePosition) : IEvent
{
    public GUIVector2 GlobalMousePosition { get; } = globalMousePosition;
}