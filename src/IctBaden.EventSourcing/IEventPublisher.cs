namespace IctBaden.EventSourcing
{
    public interface IEventPublisher
    {
        void Publish<T>(EventContext context, T eventDto) where T : Event;
    }
}