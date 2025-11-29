using cGUI.Abstraction.Interfaces;
using cGUI.Abstraction.Structs;
using cGUI.Event.Abstraction;
using cGUI.Visual.Abstraction;

namespace cGUI.Core;

public abstract class VisualElement(string id) : IVisualElement
{
    public string Id => id;

    public bool IsActive { get; set; }

    public bool IsHittable { get; set; }

    public bool IsMaskable { get; set; }

    public Rectangle Bounds { get; set; }

    public IContainer Parent { get; private set; }

    public virtual bool HitTest(GUIPoint point, out HitTestResult result)
    {
        if (IsActive && IsHittable)
        {
            Rectangle bounds = Bounds;

            if (bounds.Contains(point))
            {
                result = new HitTestResult(this, point.ConvertToLocalPoint(bounds));
                return true;
            }
        }

        result = default;
        return false;
    }

    internal protected virtual void OnParentChanged(IContainer container) => Parent = container;
}
