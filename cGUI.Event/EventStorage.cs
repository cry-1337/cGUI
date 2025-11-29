using cGUI.Abstraction.Interfaces;
using cGUI.Event.Abstraction;
using System.Collections.Generic;

namespace cGUI.Event;

public static class EventStorage<TEvent> where TEvent : IEvent
{
    public static readonly Dictionary<IElement, List<IEventHandler<TEvent>>> Handlers = [];
}