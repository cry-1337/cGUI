using cGUI.Layout;
using cGUI.Layout.Strategies;
using System;

public static class Program
{
    public static void Main(string[] args)
    {
        var layout = new Layout();

        var paddingStrategy = new PaddingStrategy(5f);
        layout.PushStrategy(paddingStrategy);

        var rect1 = layout.PerformLayout(new(0, 0, 100, 100), new(0, 0, 600, 600));
        Console.WriteLine(rect1);

        paddingStrategy.Padding = 10f;
        layout.PushStrategy(paddingStrategy);
        var rect2 = layout.PerformLayout(new(0, 0, 100, 100), new(0, 0, 600, 600));
        Console.WriteLine(rect2);

        var rect3 = layout.PerformLayout(new(0, 0, 100, 100), new(0, 0, 600, 600));
        Console.WriteLine(rect3);

        Console.ReadLine();
    }
}