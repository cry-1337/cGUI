using cGUI.Abstraction.Interfaces;
using cGUI.Event.Abstraction;

namespace cGUI.Visual.Abstraction;

public interface IVisualElement<TEvent> : IElement, IEventHandler<TEvent> where TEvent : IEvent
{
    bool IsMaskable { get; }
}