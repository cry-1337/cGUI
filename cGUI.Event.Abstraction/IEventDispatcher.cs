using cGUI.Abstraction.Interfaces;

namespace cGUI.Event.Abstraction;

public interface IEventDispatcher
{
    void Dispatch<TEvent, TOwnerKey>(TOwnerKey ownerKey, TEvent e)
        where TEvent : IEvent
        where TOwnerKey : notnull;
    void Register<TEvent, TOwnerKey>(TOwnerKey ownerKey, IEventHandler<TEvent> handler)
        where TEvent : IEvent
        where TOwnerKey : notnull;
    void Unregister<TEvent, TOwnerKey>(TOwnerKey ownerKey, IEventHandler<TEvent> handler)
        where TEvent : IEvent
        where TOwnerKey : notnull;
}