using cGUI.Abstraction.Interfaces;
using cGUI.Event.Abstraction;
using cGUI.Events.Models;

namespace cGUI.Visual.Abstraction;

public interface IVisualElement : IElement, IEventHandler<LayoutEvent>, IEventHandler<RenderEvent>
{
    bool IsMaskable { get; }

    void OnRender(in RenderEvent reason);
    void OnLayout(in LayoutEvent reason);
}