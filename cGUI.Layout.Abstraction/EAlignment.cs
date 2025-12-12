using System;

namespace cGUI.Layout.Abstraction;

[Flags]
public enum EAlignment
{
    None = 0,

    Left = 1 << 0,
    Right = 1 << 1,

    Top = 1 << 4,
    Center = 1 << 5,
    Bottom = 1 << 6,

    TopLeft = Top | Left,
    BottomLeft = Bottom | Left,
    TopRight = Top | Right,
    BottomRight = Bottom | Right,
}