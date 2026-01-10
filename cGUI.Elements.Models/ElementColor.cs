using cGUI.Abstraction.Structs;

namespace cGUI.Elements.Models;

/// <summary>
/// A structure for optimizing colors in ElementOption. 
/// A hexagonal element consumes 6 colors,
/// a square element typically consumes 4 colors,
/// and a triangular element consumes 3 colors.
/// </summary>
public struct ElementColor
{
    public GUIColor[] Value;

    public ElementColor(GUIColor color)
    {
        Value = [color];
    }

    public ElementColor(params GUIColor[] colors)
    {
        Value = colors;
    }
}