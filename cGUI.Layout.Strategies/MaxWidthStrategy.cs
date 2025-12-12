using cGUI.Abstraction.Structs;
using cGUI.Layout.Abstraction;
using cGUI.Math;

namespace cGUI.Layout.Strategies;

public readonly struct MaxWidthStrategy(float max) : ILayoutStrategy
{
    private readonly float m_Max = max;

    public readonly GUIRectangle ProcessLayout(GUIRectangle rect, in GUIRectangle parent)
        => rect with { Width = GUIMath.Max(rect.Width, m_Max) };
}