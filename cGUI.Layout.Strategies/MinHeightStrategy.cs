using cGUI.Abstraction.Structs;
using cGUI.Layout.Abstraction;
using cGUI.Math;

namespace cGUI.Layout.Strategies;

public readonly struct MinHeightStrategy(float min) : ILayoutStrategy
{
    private readonly float m_Min = min;

    public readonly GUIRectangle ProcessLayout(GUIRectangle rect, in GUIRectangle parent)
        => rect with { Height = GUIMath.Min(rect.Height, m_Min) };
}