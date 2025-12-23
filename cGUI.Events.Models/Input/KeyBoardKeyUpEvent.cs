using cGUI.Event.Abstraction;

namespace cGUI.Events.Models.Input;

public readonly struct KeyBoardKeyUpEvent(int key) : IEvent
{
    public readonly int Key = key;
}