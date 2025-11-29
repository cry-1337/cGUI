namespace cGUI.Event.Abstraction;

public interface IEventHandler<in TEvent> where TEvent : IEvent
{
    bool Handle(TEvent reason);
}