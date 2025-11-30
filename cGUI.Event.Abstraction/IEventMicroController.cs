namespace cGUI.Event.Abstraction;

public interface IEventMicroController<in TEvent> where TEvent : IEvent
{
    bool GetEvent(TEvent reason);
}
