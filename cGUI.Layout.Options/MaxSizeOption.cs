using cGUI.Abstraction.Structs;
using cGUI.Layout.Abstraction;
using cGUI.Math;

namespace cGUI.Layout.Options;

/// <summary>
/// Maximizes size of Element
/// </summary>
/// <param name="maxWidth">Maximum Width. Pass 0 to keep current value</param>
/// <param name="maxHeight">Maximum Height. Pass 0 to keep current value</param>
/// <param name="resizeRemainingRect">Will rect that remains will be resized. Pass true if MaxSize is the last option</param>
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