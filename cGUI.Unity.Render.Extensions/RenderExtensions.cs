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
            r = (byte) GUIMath.Round(GUIMath.Clamp(baseColor.R, 0, 1) * 255),
            g = (byte) GUIMath.Round(GUIMath.Clamp(baseColor.G, 0, 1) * 255),
            b = (byte) GUIMath.Round(GUIMath.Clamp(baseColor.B, 0, 1) * 255),
            a = (byte) GUIMath.Round(GUIMath.Clamp(baseColor.A, 0, 1) * 255)
        };

        public Color ToColor() => new()
        {
            r = baseColor.R / 255f,
            g = baseColor.R / 255f,
            b = baseColor.R / 255f,
            a = baseColor.R / 255f
        };
    }

    extension(GUIVector3 vector)
    {
        public Vector3 ToVector3() => new()
        {
            x = vector.X,
            y = vector.Y,
            z = vector.Z
        };
    }

    extension(GUIVector2 vector)
    {
        public Vector2 ToVector2() => new()
        {
            x = vector.X,
            y = vector.Y
        };
    }
}