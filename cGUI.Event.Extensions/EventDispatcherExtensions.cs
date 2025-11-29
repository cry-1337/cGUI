using cGUI.Abstraction.Interfaces;
using cGUI.Event.Abstraction;
using cGUI.Events.Models;
using cGUI.Visual.Abstraction;
using System;

namespace cGUI.Event.Extensions;

public static class EventDispatcherExtensions
{
    public static void Register<TEvent>(this IEventDispatcher dispatcher, IElement element) where TEvent : IEvent
    {
        if (element is IEventHandler<TEvent> handler)
            dispatcher.Register(element, handler);
        else
            throw new InvalidOperationException($"Element does not implement IEventHandler<{typeof(TEvent).Name}>");
    }

    public static void Dispatch<TEvent>(this IEventDispatcher dispatcher, IVisualElement element, TEvent reason, bool useMicroController) where TEvent : IEvent
    {
        if (reason is RenderEvent rEvent) element.OnRender(rEvent);
        else if (reason is LayoutEvent lEvent) element.OnLayout(lEvent);
        else if (useMicroController && element is IEventMicroController<TEvent> microController) { if (microController.GetEvent(reason)) dispatcher.Dispatch(element, reason); }
        else dispatcher.Dispatch(element, reason);
    }
}
