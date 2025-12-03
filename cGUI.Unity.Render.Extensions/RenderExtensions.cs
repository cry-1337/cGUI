using cGUI.Abstraction.Structs;
using cGUI.Math;
using UnityEngine;

namespace cGUI.Unity.Render.Extensions;

public static class RenderExtensions
{
    extension (GUIColor baseColor)
    {
        public Color32 ToColor32() => new()
        {
            r = (byte) GUIMath.Round(GUIMath.Clamp(baseColor.r, 0, 1) * 255),
            g = (byte) GUIMath.Round(GUIMath.Clamp(baseColor.g, 0, 1) * 255),
            b = (byte) GUIMath.Round(GUIMath.Clamp(baseColor.b, 0, 1) * 255),
            a = (byte) GUIMath.Round(GUIMath.Clamp(baseColor.a, 0, 1) * 255)
        };

        public Color ToColor() => new()
        {
            r = baseColor.r / 255f,
            g = baseColor.r / 255f,
            b = baseColor.r / 255f,
            a = baseColor.r / 255f
        };
    }
}