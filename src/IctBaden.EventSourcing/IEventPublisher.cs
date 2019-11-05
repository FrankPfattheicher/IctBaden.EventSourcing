namespace IctBaden.EventSourcing
{
    public interface IEventPublisher
    {
        void Publish<T>(EventContext context, T eventDto) where T : Event;
        void Handle<T>(EventContext context, T commandDto) where T : Command;
    }
}