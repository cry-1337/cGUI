namespace cGUI.Event.Abstraction;

public interface IEventDispatcher
{
    void Dispatch<TEvent, TOwnerKey>(TOwnerKey ownerKey, in TEvent e)
        where TEvent : IEvent
        where TOwnerKey : notnull;
    void Register<TEvent, TOwnerKey>(TOwnerKey ownerKey, IEventHandler<TEvent> handler)
        where TEvent : IEvent
        where TOwnerKey : notnull;
    void Unregister<TEvent, TOwnerKey>(TOwnerKey ownerKey, IEventHandler<TEvent> handler)
        where TEvent : IEvent
        where TOwnerKey : notnull;
}