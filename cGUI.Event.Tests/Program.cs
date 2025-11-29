using cGUI.Core;
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

    private static void Main(string[] args)
    {
        var disp = new EventDispatcher();

        var container = new Container("321");
        container.Add(new ButtonElement("123", disp));
        container.Add(new ButtonElement("53", disp));

        disp.Dispatch<RenderEvent>(container, new(), true);

        // dont work @todo: fix
        disp.Dispatch<MouseClickEvent>(container, new(), true);
        disp.Dispatch<MouseClickEvent>(container, new(), false);

        Console.ReadLine();
    }
}
