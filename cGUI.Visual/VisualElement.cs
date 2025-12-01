using cGUI.Abstraction.Interfaces;
using cGUI.Abstraction.Structs;
using cGUI.Event.Abstraction;
using cGUI.Events.Models;
using cGUI.Visual.Abstraction;

namespace cGUI.Visual;

public abstract class VisualElement(string id) : IVisualElement
{
    public string Id => id;

    public bool IsActive { get; set; }

    public bool IsHittable { get; set; }

    public bool IsMaskable { get; set; }

    public GUIRectangle Bounds { get; set; }

    public IContainer Parent { get; private set; }

    public virtual bool HitTest(GUIPoint point, out HitTestResult result)
    {
        if (IsActive && IsHittable)
        {
            GUIRectangle bounds = Bounds;

            if (bounds.Contains(point))
            {
                result = new HitTestResult(this, point.ConvertToLocalPoint(bounds));
                return true;
            }
        }

        result = default;
        return false;
    }

    public abstract void OnLayout(LayoutEvent reason);
    public abstract void OnRender(RenderEvent reason);

    bool IEventHandler<LayoutEvent>.Handle(LayoutEvent reason) { OnLayout(reason); return true; }

    bool IEventHandler<RenderEvent>.Handle(RenderEvent reason) { OnRender(reason); return true; }

    internal protected virtual void OnParentChanged(IContainer container) => Parent = container;
}
