using System.Threading;
using System.Threading.Tasks;

namespace IctBaden.EventSourcing
{
    /// <summary>
    /// Marker interface for all handlers
    /// </summary>
    public interface ICommandHandler { }

    /// <summary>
    /// Defines a handler for a command.
    /// </summary>
    /// <typeparam name="T">Event type being handled</typeparam>
    public interface ICommandHandler<in T> : ICommandHandler
    {
        /// <summary>
        ///  Handles a command
        /// </summary>
        /// <param name="commandDto">Command data being handled</param>
        /// <returns>
        /// </returns>
        void Handle(T commandDto);
    }

}
