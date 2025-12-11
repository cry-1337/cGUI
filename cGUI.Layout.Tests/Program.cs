using cGUI.Abstraction.Structs;
using cGUI.Layout;
using cGUI.Layout.Abstraction;
using cGUI.Layout.Strategies;
using System;
using static System.Net.Mime.MediaTypeNames;

public static class Program
{
    private static void Test(ILayout layout, ILayoutStrategy strategy, int num, GUIRectangle defaultRect, GUIRectangle parentRect)
    {
        layout.Reset();
        layout.PushStrategy(strategy);

        for (int i = 0; i < num; i++)
            Console.WriteLine(layout.PerformLayout(defaultRect, parentRect));
    }

    public static void Main(string[] args)
    {
        var layout = new Layout();

        Console.WriteLine("Padding Test");
        var paddingStrategy = new PaddingStrategy(5f);
        Test(layout, paddingStrategy, 1, new(0, 0, 100, 100), new(0, 0, 600, 600));
        paddingStrategy.Padding = 10f;
        Test(layout, paddingStrategy, 2, new(0, 0, 100, 100), new(0, 0, 600, 600));

        Console.WriteLine("Horizontal Layout Test");
        Test(layout, new HorizontalLayoutStrategy(10f), 3, new(0, 0, 100, 100), new(0, 0, 600, 600));

        Console.WriteLine("Vertical Layout Test");
        Test(layout, new VerticalLayoutStrategy(10f), 3, new(0, 0, 100, 100), new(0, 0, 600, 600));

        Console.ReadLine();
    }
}