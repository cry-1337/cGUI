using cGUI.Event.Extensions;
using cGUI.Events.Models;
using cGUI.Visual;
using System;

namespace cGUI.Event.Tests;

public class Program
{
    private class Container(string id) : VisualContainer<VisualElement>(id)
    {
    }

    private class TestElement(string id) : VisualElement(id)
    {
    }

    private static void Main(string[] args)
    {
        var disp = new EventDispatcher();

        var container = new Container("321");
        container.Add(new TestElement("123"));
        container.Add(new TestElement("53"));

        //disp.Spread<RenderEvent>(container, new());
        //disp.Spread<LayoutEvent>(container, new());

        Console.ReadLine();
    }
}
