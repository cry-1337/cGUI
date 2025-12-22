using cGUI.Abstraction.Interfaces;
using cGUI.Math;
using System;

namespace cGUI.Abstraction.Structs;

public partial struct GUIVector2(float x, float y) : IEquatable<GUIVector2>, IInterpolatable<GUIVector2>
{
    public float X = x, Y = y;

    public readonly override string ToString() => $"(X:{X}, Y:{Y})";
    public bool Equals(GUIVector2 other) => other.X == X && other.Y == Y;

    public GUIVector2 Lerp(GUIVector2 b, float t)
    {
        X = GUIMath.Lerp(X, b.X, t);
        Y = GUIMath.Lerp(Y, b.Y, t);

        return this;
    }
}
