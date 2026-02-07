using cGUI.Abstraction.Structs;
using cGUI.Layout.Abstraction;

namespace cGUI.Layout.Options;

public struct PaddingOption(float left, float top, float right, float bottom) : ILayoutOption
{
    public PaddingOption(float all) : this(all, all, all, all) { }

    public PaddingOption(float horizontal, float vertical) : this(horizontal, vertical, horizontal, vertical) { }

    public GUIRectangle ProcessLayout(GUIRectangle desiredRect, ref LayoutContext context)
    {
        context.RemainingRect = new GUIRectangle(
            context.RemainingRect.X + left,
            context.RemainingRect.Y + bottom,
            context.RemainingRect.Width - left - right,
            context.RemainingRect.Height - top - bottom);

        return desiredRect;
    }
}
