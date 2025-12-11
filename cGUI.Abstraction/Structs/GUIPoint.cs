namespace cGUI.Abstraction.Structs;

public partial struct GUIPoint(float x, float y)
{
    public float X = x, Y = y;

    public GUIPoint ConvertToLocalPoint(GUIRectangle bounds) => new(X - bounds.X, Y - bounds.Y);

    public readonly override string ToString() => $"(X:{X}, Y:{Y})";
}
