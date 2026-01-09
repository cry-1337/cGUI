using cGUI.Event.Abstraction;
using cGUI.Events.Models.Layout;
using cGUI.Events.Models.Render;
using cGUI.Visual;

namespace cGUI.Elements.BaseElements;

public abstract class BaseElement(string id) : VisualElement(id), IEventHandler<RenderEvent>, IEventMicroController<RenderEvent>, IEventHandler<LayoutEvent>, IEventMicroController<LayoutEvent>
{
    public abstract void OnRender(RenderEvent reason);
    public abstract void OnLayout(LayoutEvent reason);

    bool IEventHandler<RenderEvent>.Handle(RenderEvent reason)
    {
        OnRender(reason);
        return IsActive;
    }

    bool IEventHandler<LayoutEvent>.Handle(LayoutEvent reason)
    {
        OnLayout(reason);
        return IsActive;
    }

    bool IEventMicroController<RenderEvent>.GetEvent(RenderEvent reason) => IsActive;

    bool IEventMicroController<LayoutEvent>.GetEvent(LayoutEvent reason) => IsActive;
}