namespace cGUI.Event.Abstraction;

public interface IEventsHandler
{
    void HandleEvents<TEvent>(in TEvent reason) where TEvent : IEvent;
}