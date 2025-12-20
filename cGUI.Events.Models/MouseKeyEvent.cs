using cGUI.Abstraction.Structs;

namespace cGUI.Events.Models;

public abstract class MouseKeyEvent(GUIVector2 globalMousePosition, int key) : MouseEvent(globalMousePosition)
{
    public int Key { get; set; } = key;
}