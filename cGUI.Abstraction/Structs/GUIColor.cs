using cGUI.Abstraction.Interfaces;
using cGUI.Math;
using System;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace cGUI.Abstraction.Structs;

public struct GUIColor : IEquatable<GUIColor>, IInterpolatable<GUIColor>
{
    public byte R, G, B, A;

    public static GUIColor Red
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return new(255, 0, 0);
        }
    }

    public static GUIColor Green
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return new(0, 255, 0);
        }
    }

    public static GUIColor Blue
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return new(0, 0, 255);
        }
    }

    public static GUIColor White
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return new(255, 255, 255);
        }
    }

    public static GUIColor Black
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return new(0, 0, 0);
        }
    }

    public static GUIColor Clear
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return new(0, 0, 0, 0);
        }
    }

    public GUIColor(byte r, byte g, byte b)
    {
        R = r;
        G = g;
        B = b;
        A = 255;
    }

    public GUIColor(byte r, byte g, byte b, byte a)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }

    public GUIColor(GUIColor clr)
    {
        R = clr.R;
        G = clr.G;
        B = clr.B;
        A = clr.A;
    }

    public GUIColor Lerp(GUIColor target, float t)
    {
        R = (byte) GUIMath.Lerp(R, target.R, t);
        G = (byte) GUIMath.Lerp(G, target.G, t);
        B = (byte) GUIMath.Lerp(B, target.B, t);
        A = (byte) GUIMath.Lerp(A, target.A, t);
        return this;
    }

    public readonly override string ToString() => $"(R: {R}, G: {G}, B: {B}, A: {A})";

    public bool Equals(GUIColor other) => R.Equals(other.R) && G.Equals(other.G) && B.Equals(other.B) && A.Equals(other.A);
}