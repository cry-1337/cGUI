using cGUI.Abstraction.Structs;

namespace cGUI.Events.Models;

public class MouseMoveEvent(GUIVector2 globalMousePosition) : MouseEvent(globalMousePosition);