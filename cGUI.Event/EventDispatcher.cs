using cGUI.Abstraction.Interfaces;
using cGUI.Event.Abstraction;
using System.Collections.Generic;

namespace cGUI.Event;

public sealed class EventDispatcher : IEventDispatcher
{
    public void Dispatch<TEvent>(IElement root, TEvent e) where TEvent : IEvent 
    { 
        if (EventStorage<TEvent>.Handlers.TryGetValue(root, out var list))
           list.ForEach(x => x.Handle(e));
    }
    
    public void Register<TEvent>(IElement root, IEventHandler<TEvent> handler) where TEvent : IEvent
    {
        if (!EventStorage<TEvent>.Handlers.TryGetValue(root, out var list))
        {
            list = [];
            EventStorage<TEvent>.Handlers.Add(root, list);
        }
        list.Add(handler);
    }
    
    public void Unregister<TEvent>(IElement root, IEventHandler<TEvent> handler) where TEvent : IEvent
    {
        if (EventStorage<TEvent>.Handlers.TryGetValue(root, out var list))
        {
            list.Remove(handler);
    
            if (list.Count == 0)
                EventStorage<TEvent>.Handlers.Remove(root);
        }
    }

    private static class EventStorage<TEvent> where TEvent : IEvent
    {
        public static readonly Dictionary<IElement, List<IEventHandler<TEvent>>> Handlers = [];
    }
}