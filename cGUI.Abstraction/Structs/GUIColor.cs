using cGUI.Abstraction.Interfaces;
using cGUI.Math;

namespace cGUI.Abstraction.Structs;

public struct GUIColor : IInterpolatable<GUIColor>
{
    public byte R, G, B, A;

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
}