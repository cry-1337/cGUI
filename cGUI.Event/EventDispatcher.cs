using cGUI.Event.Abstraction;
using System.Collections.Generic;

namespace cGUI.Event;

public sealed class EventDispatcher : IEventDispatcher
{
    public void Dispatch<TEvent, TOwnerKey>(TOwnerKey ownerKey, TEvent e)
        where TEvent : IEvent
        where TOwnerKey : notnull
    {
        if (!EventStorage<TEvent, TOwnerKey>.Handlers.TryGetValue(ownerKey, out var list))
            return;

        for (int i = 0; i < list.Count; i++)
            list[i].Handle(e);
    }

    public void Register<TEvent, TOwnerKey>(TOwnerKey ownerKey, IEventHandler<TEvent> handler)
        where TEvent : IEvent
        where TOwnerKey : notnull
    {
        if (!EventStorage<TEvent, TOwnerKey>.Handlers.TryGetValue(ownerKey, out var list))
        {
            list = [];
            EventStorage<TEvent, TOwnerKey>.Handlers.Add(ownerKey, list);
        }

        if (!list.Contains(handler))
            list.Add(handler);
    }

    public void Unregister<TEvent, TOwnerKey>(TOwnerKey ownerKey, IEventHandler<TEvent> handler)
        where TEvent : IEvent
        where TOwnerKey : notnull
    {
        if (!EventStorage<TEvent, TOwnerKey>.Handlers.TryGetValue(ownerKey, out var list))
            return;

        list.Remove(handler);

        if (list.Count == 0)
            EventStorage<TEvent, TOwnerKey>.Handlers.Remove(ownerKey);
    }

    private static class EventStorage<TEvent, TOwnerKey>
        where TEvent : IEvent
        where TOwnerKey : notnull
    {
        public static readonly Dictionary<TOwnerKey, List<IEventHandler<TEvent>>> Handlers = new();
    }
}
