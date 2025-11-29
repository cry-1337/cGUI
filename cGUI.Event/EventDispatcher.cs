using cGUI.Abstraction.Interfaces;
using cGUI.Event.Abstraction;

namespace cGUI.Event;

public class EventDispatcher : IEventDispatcher
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
}