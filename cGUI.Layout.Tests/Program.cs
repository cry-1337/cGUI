using cGUI.Abstraction.Structs;
using cGUI.Layout;
using cGUI.Layout.Abstraction;
using cGUI.Layout.Strategies;
using System;

public static class Program
{
    private static void Test(ILayout layout, ILayoutStrategy strategy, int num, GUIRectangle defaultRect, GUIRectangle parentRect, params ILayoutStrategy[] strategies)
    {
        layout.Reset();
        layout.PushStrategy(strategy);
        foreach (var item in strategies) layout.PushStrategy(item);

        for (int i = 0; i < num; i++)
            Console.WriteLine(layout.PerformLayout(defaultRect, parentRect));
    }

    public static void Main(string[] args)
    {
        var layout = new Layout();

        Console.WriteLine("Padding Test");
        var paddingStrategy = new PaddingStrategy(new(5f));
        Test(layout, paddingStrategy, 1, new(0, 0, 100, 100), new(0, 0, 600, 600));
        paddingStrategy.Padding = new(10f);
        Test(layout, paddingStrategy, 2, new(0, 0, 100, 100), new(0, 0, 600, 600));

        Console.WriteLine("Horizontal Layout Test");
        Test(layout, new HorizontalLayoutStrategy(10f), 3, new(0, 0, 100, 100), new(0, 0, 600, 600));

        Console.WriteLine("Vertical Layout Test");
        Test(layout, new VerticalLayoutStrategy(10f), 3, new(0, 0, 100, 100), new(0, 0, 600, 600));

        Console.WriteLine("Alignment Layout Test");
        Test(layout, new AlignmentStrategy(EAlignment.Bottom), 1, new(0, 0, 100, 100), new(0, 0, 600, 600));

        Console.WriteLine("Alignment-Padding (10, 5, 10, 5) Layout Test");
        Test(layout, new AlignmentStrategy(EAlignment.Bottom), 1, new(0, 0, 100, 100), new(0, 0, 600, 600), new PaddingStrategy(new(10, 5, 10, 5)));

        Console.WriteLine("Vertical-Horizontal Layout Test");
        Test(layout, new VerticalLayoutStrategy(5f), 2, new(0, 0, 100, 100), new(0, 0, 600, 600), new HorizontalLayoutStrategy(5f));

        Console.ReadLine();
    }
}