using System.Threading;
using System.Threading.Tasks;

namespace IctBaden.EventSourcing
{
    /// <summary>
    /// Marker interface for all handlers
    /// </summary>
    public interface IHandler { }

    /// <summary>
    /// Defines a handler for a message.
    /// </summary>
    /// <typeparam name="T">Event type being handled</typeparam>
    public interface IHandler<in T> : IHandler
    {
        /// <summary>
        ///  Handles a message
        /// </summary>
        /// <param name="eventDto">Event data being handled</param>
        /// <returns>
        /// </returns>
        void Handle(T eventDto);
    }

}
