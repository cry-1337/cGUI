namespace cGUI.Event.Abstraction;

public interface IEventHandler<TEvent> where TEvent : IEvent
{
    bool Handle(in TEvent reason);
}