using cGUI.Abstraction.Structs;
using UnityEngine;

namespace cGUI.Convert.Extensions;

public static class GUIRectangleEx
{
    extension(GUIRectangle rect)
    {
        public Rect ToRectangle() => new()
        {
            x = rect.X,
            y = rect.Y,
            width = rect.Width,
            height = rect.Height
        };

        public Vector4 ToVector4() => new()
        {
            x = rect.X,
            y = rect.Y,
            w = rect.Width,
            z = rect.Height
        };
    }

    extension(Rect rect)
    {
        public GUIRectangle ToRectangle() => new()
        {
            X = rect.x,
            Y = rect.y,
            Width = rect.width,
            Height = rect.height
        };
    }
}