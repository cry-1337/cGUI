using cGUI.Event.Abstraction;
using cGUI.Event.Extensions;
using cGUI.Events.Models;
using cGUI.Visual;
using System;

namespace cGUI.Core;

public struct MouseClickEvent : IEvent
{

}

public class ButtonElement(string id) : VisualElement(id), IEventMicroController<MouseClickEvent>, IEventHandler<MouseClickEvent>
{
    public override void OnLayout(LayoutEvent reason)
    {
    }

    public override void OnRender(RenderEvent reason)
    {
        Console.WriteLine("render");
    }

    bool IEventMicroController<MouseClickEvent>.GetEvent(MouseClickEvent reason)
    {
        return true;
    }

    bool IEventHandler<MouseClickEvent>.Handle(MouseClickEvent reason)
    {
        Console.WriteLine("mouse");
        return true;
    }
}