using cGUI.Abstraction.Interfaces;

namespace cGUI.Event.Abstraction;

public interface IEventDispatcher
{
    void Dispatch<TEvent>(IElement root, TEvent e);
}