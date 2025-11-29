using cGUI.Abstraction.Interfaces;

namespace cGUI.Event.Abstraction;

public interface IEventDispatcher
{
    void Register(IEventHandler handler);
    void Unregister(IEventHandler handler);
    void Dispatch<TEvent>(IElement root, TEvent e) where TEvent : IEvent;
}