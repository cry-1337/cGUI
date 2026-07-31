using System;

namespace cGUI.Unity.Tests;

public static class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Running cGUI Unity & TextElement tests...");
        TextElementTests.RunTextElementTest();
        Console.WriteLine("All tests completed successfully.");
    }
}
