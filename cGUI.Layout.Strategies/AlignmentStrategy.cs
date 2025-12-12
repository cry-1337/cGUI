using cGUI.Abstraction.Structs;
using cGUI.Layout.Abstraction;

namespace cGUI.Layout.Strategies;

public struct AlignmentStrategy(EAlignment position) : ILayoutStrategy
{
    public EAlignment AlignmentPosition = position;
    public readonly GUIRectangle ProcessLayout(GUIRectangle rect, in GUIRectangle parent)
    {
        switch (AlignmentPosition)
        {
            default:
            case EAlignment.Left:
                rect.X = parent.X;
                rect.Y = parent.Y;
                break;

            case EAlignment.Right:
                rect.X = (parent.X + parent.Width) - rect.Width;
                rect.Y = parent.Y;
                break;

            case EAlignment.Top:
                rect.X = parent.Center.X - rect.Width / 2;
                rect.Y = parent.Y;
                break;

            case EAlignment.Center:
                rect.X = parent.Center.X - rect.Width / 2;
                rect.Y = parent.Center.Y - rect.Height / 2;
                break;

            case EAlignment.Bottom:
                rect.X = parent.X;
                rect.Y = (parent.Y + parent.Height) - rect.Height;
                break;
        }

        return rect;
    }
}