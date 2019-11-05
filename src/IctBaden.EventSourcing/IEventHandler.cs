using System.Threading;
using System.Threading.Tasks;

namespace IctBaden.EventSourcing
{
    /// <summary>
    /// Marker interface for all handlers
    /// </summary>
    public interface IEventHandler { }

    /// <summary>
    /// Defines a handler for a event.
    /// </summary>
    /// <typeparam name="T">Event type being handled</typeparam>
    public interface IEventHandler<in T> : IEventHandler
    {
        /// <summary>
        ///  Handles a event
        /// </summary>
        /// <param name="eventDto">Event data being handled</param>
        /// <returns>
        /// </returns>
        void Apply(T eventDto);
    }

}
