using cGUI.Event.Abstraction;
using cGUI.Events.Models;
using cGUI.Visual.Abstraction;
using System;
using System.Runtime.CompilerServices;

namespace cGUI.Event.Extensions;

public static class EventDispatcherExtensions
{
    public static void Register<TEventOwner, TEvent>(this IEventDispatcher dispatcher, TEventOwner owner) where TEvent : IEvent where TEventOwner : notnull
    {
        if (owner is IEventHandler<TEvent> handler)
            dispatcher.Register(owner, handler);
        else
            throw new InvalidOperationException($"Owner does not implement IEventHandler<{typeof(TEvent).Name}>");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Spread<TEvent>(this IEventDispatcher dispatcher, IVisualElement element, ref TEvent reason) where TEvent : IEvent
    {
        if (element is IEventMicroController<TEvent> microController && microController.GetEvent(reason)) dispatcher.Dispatch(element, in reason);
        else if (element is IEventsHandler eventsHandler) eventsHandler.HandleEvents(in reason);
        else dispatcher.Dispatch(element, reason);
    }
}
