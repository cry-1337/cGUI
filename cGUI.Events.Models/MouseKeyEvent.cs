using cGUI.Abstraction.Structs;

namespace cGUI.Events.Models;

public abstract class MouseKeyEvent(GUIVector2 localMousePosition, GUIVector2 globalMousePosition, int key) : MouseEvent(localMousePosition, globalMousePosition)
{
    public int Key { get; set; } = key;
}