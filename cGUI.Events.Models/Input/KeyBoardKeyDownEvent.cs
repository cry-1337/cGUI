using cGUI.Event.Abstraction;

namespace cGUI.Events.Models.Input;

public readonly struct KeyBoardKeyDownEvent(int key) : IEvent
{
    public readonly int Key = key;
}