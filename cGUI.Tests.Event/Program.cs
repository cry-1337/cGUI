using cGUI.Core;
using cGUI.Event;
using cGUI.Event.Abstraction;
using System;

public class Program
{
    private struct RenderEvent : IEvent
    {

    }

    private struct MouseEvent : IEvent
    {

    }

    private class TestElement : VisualElement, IEventHandler<RenderEvent>, IEventHandler<MouseEvent>
    {
        public TestElement(string id, IEventDispatcher dispatcher) : base(id)
        {
            dispatcher.Register<RenderEvent>(this, this);
            dispatcher.Register<MouseEvent>(this, this);
        }

        bool IEventHandler<RenderEvent>.Handle(in RenderEvent reason)
        {
            Console.WriteLine("render");
            return true;
        }

        bool IEventHandler<MouseEvent>.Handle(in MouseEvent reason)
        {
            Console.WriteLine("mouse");
            return true;
        }
    }

    public static void Main(string[] args)
    {
        var disp = new EventDispatcher();
        var element = new TestElement("123", disp);
        disp.Dispatch(element, new RenderEvent());
        disp.Dispatch(element, new MouseEvent());
        Console.ReadKey();
    }
}