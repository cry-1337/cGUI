namespace cGUI.Event.Abstraction;

public interface IEventHandler<in TEvent> : IEventHandler where TEvent : IEvent
{
    bool Handle(TEvent reason);
}
public interface IEventHandler
{
    bool Handle(IEvent reason);
}