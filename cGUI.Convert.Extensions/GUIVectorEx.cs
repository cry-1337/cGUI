using cGUI.Abstraction.Structs;
using UnityEngine;

namespace cGUI.Convert.Extensions;

public static class GUIVectorEx
{
    extension(GUIVector3 vector)
    {
        public Vector3 ToVector3() => new()
        {
            x = vector.X,
            y = vector.Y,
            z = vector.Z
        };
    }

    extension(Vector3 vector)
    {
        public GUIVector3 ToGUIVector3() => new()
        {
            X = vector.x,
            Y = vector.y,
            Z = vector.z
        };
    }
}