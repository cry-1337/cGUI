using cGUI.Abstraction.Interfaces;
using cGUI.Abstraction.Structs;
using cGUI.Math;
using cGUI.Visual.Abstraction;

namespace cGUI.Elements.BaseElements;

public static class ClippingUtility
{
    public static GUIRectangle GetClippedBounds(IVisualElement? element, GUIRectangle drawBounds)
    {
        var parent = element?.Parent;
        while (parent != null)
        {
            if (parent is IScrollable scrollable && scrollable.SupportsScroll)
            {
                var parentBounds = parent.Bounds;
                float x1 = GUIMath.Max(drawBounds.X, parentBounds.X);
                float y1 = GUIMath.Max(drawBounds.Y, parentBounds.Y);
                float x2 = GUIMath.Min(drawBounds.X + drawBounds.Width, parentBounds.X + parentBounds.Width);
                float y2 = GUIMath.Min(drawBounds.Y + drawBounds.Height, parentBounds.Y + parentBounds.Height);

                float w = GUIMath.Max(0f, x2 - x1);
                float h = GUIMath.Max(0f, y2 - y1);

                drawBounds = new GUIRectangle(x1, y1, w, h);
            }
            parent = parent.Parent;
        }

        return drawBounds;
    }
}
