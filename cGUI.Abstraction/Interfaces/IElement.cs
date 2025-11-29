using cGUI.Abstraction.Structs;

namespace cGUI.Abstraction.Interfaces;

public interface IElement
{
    string Id { get; }

    bool IsActive { get; }

    bool IsHittable { get; }

    Rectangle Bounds { get; }

    IContainer Parent { get; }

    bool HitTest(GUIPoint point, out HitTestResult result);
}