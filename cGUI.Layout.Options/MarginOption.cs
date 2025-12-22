using cGUI.Abstraction.Structs;
using cGUI.Layout.Abstraction;

namespace cGUI.Layout.Options;

public struct MarginOption(float left, float top, float right, float bottom) : ILayoutOption
{
    public GUIRectangle ProcessLayout(GUIRectangle desiredRect, ref LayoutContext context)
        => desiredRect with { X = desiredRect.X + left, Y = desiredRect.Y + bottom, Width = desiredRect.Width - (left + right), Height = desiredRect.Height - (top + bottom) };
}