using cGUI.Abstraction.Interfaces;
using cGUI.Math;

namespace cGUI.Abstraction.Structs;

public struct GUIColor : IInterpolatable<GUIColor>
{
    public byte r;
    public byte g;
    public byte b;
    public byte a;

    public GUIColor(byte r, byte g, byte b, byte a)
    {
        this.r = r;
        this.g = g;
        this.b = b;
        this.a = a;
    }

    public GUIColor(GUIColor clr)
    {
        r = clr.r;
        g = clr.g;
        b = clr.b;
        a = clr.a;
    }

    public GUIColor Lerp(GUIColor target, float t)
    {
        r = (byte) GUIMath.Lerp(r, target.r, t);
        g = (byte) GUIMath.Lerp(g, target.g, t);
        b = (byte) GUIMath.Lerp(b, target.b, t);
        a = (byte) GUIMath.Lerp(a, target.a, t);
        return this;
    }
}