namespace cGUI.Event.Abstraction;

public interface IEventMicroController<TEvent> where TEvent : IEvent
{
    bool GetEvent(TEvent reason);
}
