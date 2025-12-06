using cGUI.Abstraction.Structs;
using System.Runtime.InteropServices;

namespace cGUI.Render.Abstraction;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct Vertex(GUIVector2 position, GUIColor color, GUIVector2 uv)
{
    public GUIVector2 Position { get; set; } = position;
    public GUIColor Color { get; set; } = color;
    public GUIVector2 Uv { get; set; } = uv;
    public float Atlas { get; set; } = 0f;
}