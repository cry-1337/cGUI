// EventDispatcher.cs
using cGUI.Abstraction.Interfaces;
using cGUI.Event.Abstraction;
using System.Collections.Generic;

namespace cGUI.Core;

public class EventDispatcher : IEventDispatcher
{
    private readonly List<IEventHandler> m_Handlers = [];

    public void Dispatch<TEvent>(IElement root, TEvent e) where TEvent : IEvent
    {
        m_Handlers.ForEach(x => x.Handle(e));
    }

    public void Register(IEventHandler handler) => m_Handlers.Add(handler);
    public void Unregister(IEventHandler handler) => m_Handlers.Remove(handler);
}