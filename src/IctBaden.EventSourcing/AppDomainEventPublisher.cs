using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace IctBaden.EventSourcing
{
    public class AppDomainEventPublisher : IEventPublisher
    {
        public EventContext Context { get; set; }

        private readonly Dictionary<Type, List<Type>> _handlers = new Dictionary<Type, List<Type>>();

        public AppDomainEventPublisher()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var types = assemblies
                .SelectMany(a => a.GetTypes());
            var handlers = types
                .Where(t => t.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IHandler<>)))
                .ToList();

            foreach (var handler in handlers)
            {
                var eventTypes = handler.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IHandler<>))
                    .Select(i => i.GenericTypeArguments.First())
                    .ToList();

                foreach (var eventType in eventTypes)
                {
                    if (_handlers.ContainsKey(eventType))
                    {
                        _handlers[eventType].Add(handler);
                    }
                    else
                    {
                        _handlers.Add(eventType, new List<Type>{ handler });
                    }
                }
            }
        }

        public void Publish<T>(string eventStream, T eventDto) where T : Event
        {
            var eventType = eventDto.GetType();
            if (!_handlers.ContainsKey(eventType))
            {
                Debug.WriteLine($"Handler for event type {eventType.Name} not defined.");
                return;
            }

            var handlers = _handlers[eventType];
            if (!handlers.Any())
            {
                Debug.WriteLine($"Handler for event type {eventType.Name} not defined.");
                return;
            }

            foreach (var handler in handlers)
            {
                var instance = Context.GetContext(handler);
                var method = handler.GetMethods()
                    .FirstOrDefault(m => m.Name == "Handle" && m.GetParameters()[0].ParameterType == eventType);
                method?.Invoke(instance, new object[] { eventDto });
            }
        }

    }
}
