using cGUI.Abstraction.Interfaces;
using cGUI.Math;
using System;

namespace cGUI.Abstraction.Structs;

public struct GUIVector3 : IEquatable<GUIVector3>, IInterpolatable<GUIVector3>
{
    public float X, Y, Z;

    public GUIVector3(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public GUIVector3(float x, float y)
    {
        X = x;
        Y = y;
        Z = 0f;
    }

    public GUIVector3(GUIVector3 vector)
    {
        X = vector.X;
        Y = vector.Y;
        Z = vector.Z;
    }

    public GUIVector3(GUIVector2 vector)
    {
        X = vector.X;
        Y = vector.Y;
        Z = 0f;
    }

    public readonly override string ToString() => $"(X:{X}, Y:{Y}, Z:{Z})";
    public bool Equals(GUIVector3 other) => other.X == X && other.Y == Y && other.Z == Z;

    public GUIVector3 Lerp(GUIVector3 b, float t)
    {
        X = GUIMath.Lerp(X, b.X, t);
        Y = GUIMath.Lerp(Y, b.Y, t);
        Z = GUIMath.Lerp(Z, b.Z, t);

        return this;
    }
}
