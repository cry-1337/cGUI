using cGUI.Abstraction.Structs;
using cGUI.Layout.Abstraction;

namespace cGUI.Layout.Options;

public enum EDockType
{
    Left,
    Top,
    Right,
    Bottom,
    Fill
}

public struct DockOption(EDockType dockType) : ILayoutOption
{
    public GUIRectangle ProcessLayout(GUIRectangle desiredRect, ref LayoutContext context)
    {
        var rem = context.RemainingRect;
        var size = desiredRect;

        switch (dockType)
        {
            default:
            case EDockType.Top:
                desiredRect = rem with { Y = rem.Y + rem.Height - size.Height, Height = size.Height };
                context.RemainingRect = rem with { Height = rem.Height - size.Height };
                return desiredRect;
            case EDockType.Bottom:
                desiredRect = rem with { Height = size.Height };
                context.RemainingRect = rem with { Y = rem.Y + size.Height, Height = rem.Height - size.Height };
                return desiredRect;
            case EDockType.Fill:
                desiredRect = rem;
                return desiredRect;
        }
    }
}