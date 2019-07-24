using System.Threading;
using System.Threading.Tasks;

namespace IctBaden.EventSourcing
{
    /// <summary>
    /// Defines a handler for a message.
    /// </summary>
    /// <typeparam name="T">Event type being handled</typeparam>
    public interface IHandler<in T> where T : Event
    {
        /// <summary>
        ///  Handles a message
        /// </summary>
        /// <param name="eventDto">Event data being handled</param>
        /// <param name="token">Cancellation token from sender/publisher.</param>
        /// <returns>
        /// Task that represents handling of message.
        /// Task should return if event is handled.
        /// </returns>
        Task<bool> Handle(T eventDto, CancellationToken token = default);
    }

}
