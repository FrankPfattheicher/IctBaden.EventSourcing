using System.Threading;
using System.Threading.Tasks;

namespace IctBaden.EventSourcing
{
    public interface IEventPublisher
    {
        EventSession Session { get; set; }

        Task Publish<T>(T eventDto, CancellationToken cancellationToken = default) where T : Event;
    }
}