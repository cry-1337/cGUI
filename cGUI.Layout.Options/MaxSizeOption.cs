using cGUI.Abstraction.Structs;
using cGUI.Layout.Abstraction;
using cGUI.Math;

namespace cGUI.Layout.Options;

public struct MaxSizeOption(float maxWidth, float maxHeight, bool resizeRemainingRect = false) : ILayoutOption
{
    public readonly GUIRectangle ProcessLayout(GUIRectangle desiredRect, ref LayoutContext state)
    {
        if (maxWidth > 0) desiredRect.Width = GUIMath.Max(desiredRect.Width, maxWidth);
        if (maxHeight > 0) desiredRect.Height = GUIMath.Max(desiredRect.Height, maxHeight);
        if (resizeRemainingRect)
        {
            state.RemainingRect.Width -= desiredRect.Width;
            state.RemainingRect.Height -= desiredRect.Height;
        }
        return desiredRect;
    }
}