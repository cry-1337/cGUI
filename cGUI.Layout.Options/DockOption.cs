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

public class DockOption(EDockType dockType) : ILayoutOption
{
    public GUIRectangle ProcessLayout(GUIRectangle desiredRect, ref LayoutState state)
    {
        GUIRectangle remaining = state.RemainingBounds;
        GUIRectangle result = remaining;

        switch (dockType)
        {
            case EDockType.Left:
                // Устанавливаем ширину из желаемой, высоту на весь остаток
                result.Width = desiredRect.Width;

                // Сдвигаем оставшуюся область вправо
                state.RemainingBounds.X += result.Width;
                state.RemainingBounds.Width -= result.Width;
                break;

            case EDockType.Top:
                // Устанавливаем высоту из желаемой, ширина на весь остаток
                result.Height = desiredRect.Height;

                // Сдвигаем оставшуюся область вниз
                state.RemainingBounds.Y += result.Height;
                state.RemainingBounds.Height -= result.Height;
                break;

            case EDockType.Right:
                // Устанавливаем ширину, прижимаем к правому краю остатка
                result.Width = desiredRect.Width;
                result.X = remaining.X + remaining.Width - result.Width;

                // Уменьшаем ширину остатка справа
                state.RemainingBounds.Width -= result.Width;
                break;

            case EDockType.Bottom:
                // Устанавливаем высоту, прижимаем к нижнему краю остатка
                result.Height = desiredRect.Height;
                result.Y = remaining.Y + remaining.Height - result.Height;

                // Уменьшаем высоту остатка снизу
                state.RemainingBounds.Height -= result.Height;
                break;

            case EDockType.Fill:
                break;
        }

        return result;
    }
}