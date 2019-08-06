namespace IctBaden.EventSourcing
{
    public interface IEventPublisher
    {
        EventContext Context { get; set; }

        void Publish<T>(string eventStream, T eventDto) where T : Event;
    }
}