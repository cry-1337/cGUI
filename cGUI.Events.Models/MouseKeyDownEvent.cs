using cGUI.Abstraction.Structs;

namespace cGUI.Events.Models;

public class MouseKeyDownEvent(GUIVector2 globalMousePosition, int key) : MouseKeyEvent(globalMousePosition, key);