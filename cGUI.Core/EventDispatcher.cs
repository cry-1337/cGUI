using cGUI.Abstraction.Interfaces;
using cGUI.Event.Abstraction;

namespace cGUI.Core;

public class EventDispatcher : IEventDispatcher
{
    public void Dispatch<TEvent>(IElement root, TEvent e)
    {

    }
}
