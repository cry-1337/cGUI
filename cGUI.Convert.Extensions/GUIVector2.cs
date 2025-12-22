using cGUI.Abstraction.Structs;
using UnityEngine;

namespace cGUI.Convert.Extensions;

public static class GUIVector2Ex
{
    extension(GUIVector2 vector)
    {
        public Vector2 ToVector2() => new()
        {
            x = vector.X,
            y = vector.Y
        };
    }

    extension(Vector2 vector)
    {
        public GUIVector2 ToVector2() => new()
        {
            X = vector.x,
            Y = vector.y
        };
    }
}