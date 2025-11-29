using cGUI.Abstraction.Interfaces;

namespace cGUI.Event.Abstraction;

public interface IEventDispatcher
{
    public void Dispatch<TEvent>(IElement root, TEvent e) where TEvent : IEvent;
    public void Register<TEvent>(IElement root, IEventHandler<TEvent> handler) where TEvent : IEvent;
    public void Unregister<TEvent>(IElement root, IEventHandler<TEvent> handler) where TEvent : IEvent;
}