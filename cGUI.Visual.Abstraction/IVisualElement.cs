using cGUI.Abstraction.Interfaces;
using cGUI.Event.Abstraction;

namespace cGUI.Visual.Abstraction;

public interface IVisualElement : IElement, IEventHandler<RenderEvent>
{
    bool IsMaskable { get; }
}