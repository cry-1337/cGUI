using cGUI.Abstraction.Interfaces;
using cGUI.Math;

namespace cGUI.Abstraction.Structs;

public partial struct GUIVector2(float x, float y) : IInterpolatable<GUIVector2>
{
    public float X = x, Y = y;

    public GUIVector2 Lerp(GUIVector2 b, float t)
    {
        X = GUIMath.Lerp(X, b.X, t);
        Y = GUIMath.Lerp(Y, b.Y, t);

        return this;
    }
}
