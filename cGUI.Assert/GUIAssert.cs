using System;

namespace cGUI.Assert;

public static class GUIAssert
{
    public static void IsNull<TValue>(TValue value, string message) { if (IsNull(value)) throw new NullReferenceException($"{value} is null. {message}"); }
    public static bool IsNull<TValue>(TValue value) => value is null;
}
