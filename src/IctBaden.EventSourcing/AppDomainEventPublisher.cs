using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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

        public Task Publish<T>(T eventDto, CancellationToken cancellationToken = default) where T : Event
        {
            var eventType = eventDto.GetType();
            var handlers = _handlers[eventType];

            if (!handlers.Any())
            {
                throw new NotSupportedException($"Handler for event type {eventType.Name} not defined.");
            }

            foreach (var handler in handlers)
            {
                object instance;

                var ctor0 = handler.GetConstructors()
                    .FirstOrDefault(c => c.GetParameters().Length == 0);
                if (ctor0 != null)
                {
                    instance = Activator.CreateInstance(handler);
                }
                else
                {
                    var ctor1 = handler.GetConstructors()
                        .FirstOrDefault(c => c.GetParameters().Length == 1 && c.GetParameters()[0].ParameterType == typeof(EventContext));
                    if (ctor1 != null)
                    {
                        instance = Activator.CreateInstance(handler, Context);
                    }
                    else
                    {
                        throw new NotSupportedException($"Missing ctor fot handler type {handler.Name}.");
                    }
                }
                var method = handler.GetMethods()
                    .FirstOrDefault(m => m.Name == "Handle" && m.GetParameters()[0].ParameterType == eventType);
                method?.Invoke(instance, new object[] {eventDto, cancellationToken});
            }

            return Task.CompletedTask;
        }
    }
}
