using cGUI.Abstraction.Structs;
using cGUI.Math;
using UnityEngine;

namespace cGUI.Convert.Extensions;

public static class GUIColorEx
{
    extension(GUIColor baseColor)
    {
        public Color32 ToColor32() => new()
        {
            r = (byte) GUIMath.Round(GUIMath.Clamp(baseColor.R, 0, 1) * 255),
            g = (byte) GUIMath.Round(GUIMath.Clamp(baseColor.G, 0, 1) * 255),
            b = (byte) GUIMath.Round(GUIMath.Clamp(baseColor.B, 0, 1) * 255),
            a = (byte) GUIMath.Round(GUIMath.Clamp(baseColor.A, 0, 1) * 255)
        };

        public Color ToColor() => new()
        {
            r = baseColor.R / 255f,
            g = baseColor.G / 255f,
            b = baseColor.B / 255f,
            a = baseColor.A / 255f
        };
    }

    extension(Color baseColor)
    {
        public GUIColor ToGUIColor() => new()
        {
            R = (byte) GUIMath.Round(baseColor.r * 255),
            G = (byte) GUIMath.Round(baseColor.g * 255),
            B = (byte) GUIMath.Round(baseColor.b * 255),
            A = (byte) GUIMath.Round(baseColor.a * 255)
        };
    }

    extension(Color32 baseColor)
    {
        public GUIColor ToGUIColor() => new()
        {
            R = baseColor.r,
            G = baseColor.g,
            B = baseColor.b,
            A = baseColor.a
        };
    }
}
