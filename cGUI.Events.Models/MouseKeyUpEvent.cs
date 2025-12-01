using cGUI.Abstraction.Structs;

namespace cGUI.Events.Models;

public class MouseKeyUpEvent(GUIVector2 localMousePosition, GUIVector2 globalMousePosition, int key) : MouseKeyEvent(localMousePosition, globalMousePosition, key);
