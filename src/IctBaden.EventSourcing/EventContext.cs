using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace IctBaden.EventSourcing
{
    public class EventContext
    {
        private readonly IEventStore _store;
        private readonly string _contextId;
        private readonly Dictionary<Type, object> _contexts = new Dictionary<Type, object>();

        public EventContext(string contextId, IEventStore store)
        {
            _contextId = contextId;
            _store = store;
        }

        public void Notify(Event requestEventDto, CancellationToken cancellationToken = default)
        {
            _store.Save(_contextId, requestEventDto);
        }

        private void ReplayEvents(object context)
        {
            var contextType = context.GetType();
            var eventTypes = contextType.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IHandler<>))
                .Select(i => i.GenericTypeArguments.First())
                .ToArray();

            var events =_store.Replay(_contextId, eventTypes);

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
            return (T)GetContext(contextType);
        }

        public object GetContext(Type contextType)
        {
            if (_contexts.ContainsKey(contextType))
            {
                return _contexts[contextType];
            }

            object context;

            var ctor0 = contextType.GetConstructors()
                .FirstOrDefault(c => c.GetParameters().Length == 0);
            if (ctor0 != null)
            {
                context = Activator.CreateInstance(contextType);
            }
            else
            {
                var ctor1 = contextType.GetConstructors()
                    .FirstOrDefault(c => c.GetParameters().Length == 1 && c.GetParameters()[0].ParameterType == typeof(EventContext));
                if (ctor1 != null)
                {
                    context = Activator.CreateInstance(contextType, this);
                }
                else
                {
                    throw new NotSupportedException($"Missing ctor fot handler type {contextType.Name}.");
                }
            }

            ReplayEvents(context);
            _contexts[contextType] = context;
            return context;
        }
    }
}