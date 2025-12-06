namespace cGUI.Abstraction.Structs;

public partial struct GUIPoint(float x, float y)
{
    public float x = x, y = y;

    public GUIPoint ConvertToLocalPoint(GUIRectangle bounds) => new(x - bounds.X, y - bounds.Y);
}
