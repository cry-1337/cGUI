using cGUI.Abstraction.Structs;
using cGUI.Layout;
using cGUI.Layout.Abstraction;
using cGUI.Layout.Strategies;
using System;

public static class Program
{
    private static void Test(ILayout layout, ILayoutStrategy strategy, int num, GUIRectangle defaultRect, GUIRectangle parentRect, ILayoutStrategy strategy1 = null)
    {
        layout.Reset();
        layout.PushStrategy(strategy);
        if (strategy1 != null) layout.PushStrategy(strategy1);

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
        layout.Reset();
        layout.PushStrategy(new AlignmentStrategy(EAlignment.Bottom));
        Console.WriteLine(layout.PerformLayout(new(0, 0, 100, 100), new(0, 0, 600, 600)));

        Console.WriteLine("Alignment-Padding (10, 5, 10, 5) Layout Test");
        layout.Reset();
        layout.PushStrategy(new AlignmentStrategy(EAlignment.Bottom));
        layout.PushStrategy(new PaddingStrategy(new(10, 5, 10, 5)));
        Console.WriteLine(layout.PerformLayout(new(0, 0, 100, 100), new(0, 0, 600, 600)));

        Console.WriteLine("Alignment-Horizontal Layout Test");
        layout.Reset();
        layout.PushStrategy(new VerticalLayoutStrategy(5f));
        layout.PushStrategy(new HorizontalLayoutStrategy(5f));
        //layout.PushStrategy(new HorizontalLayoutStrategy(5f));
        //layout.PushStrategy(new AlignmentStrategy(EAlignment.Bottom));
        Console.WriteLine(layout.PerformLayout(new(0, 0, 100, 100), new(0, 0, 600, 600)));
        Console.WriteLine(layout.PerformLayout(new(0, 0, 100, 100), new(0, 0, 600, 600)));

        Console.ReadLine();
    }
}