using cGUI.Abstraction.Structs;

namespace cGUI.Events.Models;

public abstract class MouseEvent(GUIVector2 localMousePosition, GUIVector2 globalMousePosition) : InputEvent(localMousePosition, globalMousePosition);
