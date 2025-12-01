namespace cGUI.Abstraction.Interfaces;

public interface IInterpolatable<TValue> where TValue : notnull
{
    TValue Lerp(TValue b, float t);
}