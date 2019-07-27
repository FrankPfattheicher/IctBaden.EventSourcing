using System;
using System.Linq;
using System.Threading;

namespace IctBaden.EventSourcing
{
    public class EventContext
    {
        private readonly IEventStore _store;
        public EventContext(IEventStore store)
        {
            _store = store;
        }

        public void Notify(Event requestEventDto, CancellationToken cancellationToken = default)
        {
            _store.Save(requestEventDto);
        }

        private void ReplayEvents(object context)
        {
            var contextType = context.GetType();
            var eventTypes = contextType.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IHandler<>))
                .Select(i => i.GenericTypeArguments.First())
                .ToArray();

            var events =_store.Replay(eventTypes);

            foreach (var eventDto in events)
            {
                var method = contextType.GetMethods()
                    .FirstOrDefault(m => m.Name == "Handle" && m.GetParameters().First().GetType() == eventDto.GetType());

                method?.Invoke(context, new object[] {eventDto, CancellationToken.None});
            }
        }

        public T GetContext<T>()
        {
            var contextType = typeof(T);
            var context = Activator.CreateInstance(contextType);
            ReplayEvents(context);
            return (T)context;
        }
    }
}