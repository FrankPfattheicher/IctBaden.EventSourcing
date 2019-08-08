using System;
using System.Collections.Generic;
using System.Linq;

namespace IctBaden.EventSourcing
{
    public class EventContext
    {
        public string StreamId { get; }
        private readonly IEventStore _store;
        private readonly IEventPublisher _publisher;

        private readonly Dictionary<Type, object> _contexts = new Dictionary<Type, object>();
        private bool _replay;

        public EventContext(string streamId, IEventStore store, IEventPublisher publisher)
        {
            StreamId = streamId;
            _store = store;
            _publisher = publisher;
        }

        public void Request(Request requestDto)
        {
            if (_replay) return;
            if (_store.Save(StreamId, requestDto))
            {
                _publisher.Publish(this, requestDto);
            }
        }
        public void Notify(Event eventDto)
        {
            if (_replay) return;
            if (eventDto.GetType().IsSubclassOf(typeof(Request)))
            {
                throw new InvalidOperationException("Use Request() to publish requests.");
            }

            if (_store.Save(StreamId, eventDto))
            {
                _publisher.Publish(this, eventDto);
            }
        }

        private void ReplayEvents(object context)
        {
            var contextType = context.GetType();
            var eventTypes = contextType.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IHandler<>))
                .Select(i => i.GenericTypeArguments.First())
                .Where(t => !t.IsSubclassOf(typeof(Request)))
                .ToArray();

            var events =_store.Replay(StreamId, eventTypes);

            _replay = true;
            foreach (var eventDto in events)
            {
                var method = contextType.GetMethods()
                    .FirstOrDefault(m => m.Name == "Handle" && m.GetParameters().First().ParameterType == eventDto.GetType());

                method?.Invoke(context, new object[] { eventDto });
            }
            _replay = false;
        }

        // ReSharper disable once UnusedMember.Global
        public void RegisterContextInstance(object context)
        {
            var contextType = context.GetType();
            _contexts[contextType] = context;
        }

        public T GetContextInstance<T>()
        {
            var contextType = typeof(T);
            return (T)GetContextInstance(contextType);
        }

        public object GetContextInstance(Type contextType)
        {
            if (_contexts.ContainsKey(contextType))
            {
                return _contexts[contextType];
            }

            var context = CreateContextInstance(contextType);
            ReplayEvents(context);
            return context;
        }

        private object CreateContextInstance(Type contextType)
        {
            var ctor0 = contextType.GetConstructors()
                .FirstOrDefault(c => c.GetParameters().Length == 0);
            if (ctor0 != null)
            {
                return Activator.CreateInstance(contextType);
            }
            else
            {
                var ctor1 = contextType.GetConstructors()
                    .FirstOrDefault(c => c.GetParameters().Length == 1 && c.GetParameters()[0].ParameterType == typeof(EventContext));
                if (ctor1 != null)
                {
                    return Activator.CreateInstance(contextType, this);
                }
                else
                {
                    throw new NotSupportedException($"Missing ctor fot handler type {contextType.Name}.");
                }
            }
        }

    }
}
