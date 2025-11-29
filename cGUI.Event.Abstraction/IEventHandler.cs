namespace cGUI.Event.Abstraction;

public interface IEventHandler<in TEvent> where TEvent : IEvent
{
    void Handle(TEvent e);
}