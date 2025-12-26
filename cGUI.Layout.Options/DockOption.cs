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

public readonly struct DockOption(EDockType dockType) : ILayoutOption
{
    public readonly GUIRectangle ProcessLayout(GUIRectangle desiredRect, ref LayoutContext context)
    {
        var rem = context.RemainingRect;
        var size = desiredRect;

        switch (dockType)
        {
            case EDockType.Left:
                desiredRect = new GUIRectangle(rem.X, rem.Y, size.Width, rem.Height);
                context.RemainingRect = rem with { X = rem.X + size.Width, Width = rem.Width - size.Width };
                return desiredRect;

            case EDockType.Right:
                desiredRect = new GUIRectangle(rem.X + rem.Width - size.Width, rem.Y, size.Width, rem.Height);
                context.RemainingRect = rem with { Width = rem.Width - size.Width };
                return desiredRect;

            case EDockType.Top:
                desiredRect = rem with { Y = rem.Y + rem.Height - size.Height, Height = size.Height };
                context.RemainingRect = rem with { Height = rem.Height - size.Height };
                return desiredRect;

            case EDockType.Bottom:
                desiredRect = rem with { Height = size.Height };
                context.RemainingRect = rem with { Y = rem.Y + size.Height, Height = rem.Height - size.Height };
                return desiredRect;

            case EDockType.Fill:
            default:
                return rem;
        }
    }
}