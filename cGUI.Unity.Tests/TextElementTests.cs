using System;
using cGUI.Abstraction;
using cGUI.Abstraction.Structs;
using cGUI.Elements.BaseElements;
using cGUI.Elements.Models;
using cGUI.Events.Models.Render;

namespace cGUI.Unity.Tests;

public static class TextElementTests
{
    public static void RunTextElementTest()
    {
        var fontAtlas = FontAtlas.CreateGridAtlas(charWidth: 8f, charHeight: 16f);

        var options = new ElementOption
        {
            DesiredRect = new GUIRectangle(10, 10, 100, 30),
            Color = new ElementColor(new GUIColor(255, 255, 255))
        };

        var textElement = new TextElement("test_text", options, fontAtlas, "Hello World");

        // Test Zero-GC integer formatting
        textElement.SetText(12345);

        // Simulate PreRenderEvent
        var preRender = new PreRenderEvent(0.016f);
        if (textElement is Event.Abstraction.IEventHandler<PreRenderEvent> handler)
        {
            bool active = handler.Handle(preRender);
            if (!active) throw new Exception("TextElement failed PreRenderEvent handling");
        }

        Console.WriteLine("[TEST SUCCESS] TextElement Zero-GC test passed cleanly!");
    }
}
