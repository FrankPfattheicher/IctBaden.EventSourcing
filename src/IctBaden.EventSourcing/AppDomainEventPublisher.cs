using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace IctBaden.EventSourcing
{
    public class AppDomainEventPublisher : IEventPublisher
    {
        private readonly Dictionary<Type, List<Type>> _eventHandlers = new Dictionary<Type, List<Type>>();
        private readonly Dictionary<Type, List<Type>> _commandHandlers = new Dictionary<Type, List<Type>>();

        public AppDomainEventPublisher()
            : this(new Assembly[0])
        {
        }
        // ReSharper disable once MemberCanBePrivate.Global
        public AppDomainEventPublisher(IEnumerable<Assembly> additionalAssemblies)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            assemblies.AddRange(additionalAssemblies);
            var types = assemblies
                .SelectMany(a => a.GetTypes())
                .ToList();
            
            var eventHandlers = types
                .Where(t => t.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEventHandler<>)))
                .ToList();

            foreach (var handler in eventHandlers)
            {
                var eventTypes = handler.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEventHandler<>))
                    .Select(i => i.GenericTypeArguments.First())
                    .ToList();

                foreach (var eventType in eventTypes)
                {
                    if (_eventHandlers.ContainsKey(eventType))
                    {
                        _eventHandlers[eventType].Add(handler);
                    }
                    else
                    {
                        _eventHandlers.Add(eventType, new List<Type>{ handler });
                    }
                }
            }
            
            var commandHandlers = types
                .Where(t => t.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ICommandHandler<>)))
                .ToList();

            foreach (var handler in commandHandlers)
            {
                var commandTypes = handler.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<>))
                    .Select(i => i.GenericTypeArguments.First())
                    .ToList();

                foreach (var commandType in commandTypes)
                {
                    if (_commandHandlers.ContainsKey(commandType))
                    {
                        _commandHandlers[commandType].Add(handler);
                    }
                    else
                    {
                        _commandHandlers.Add(commandType, new List<Type>{ handler });
                    }
                }
            }
        }

        public void Publish<T>(EventContext context, T eventDto) where T : Event
        {
            var eventType = eventDto.GetType();
            if (!_eventHandlers.ContainsKey(eventType))
            {
                Debug.WriteLine($"Handler for event type {eventType.Name} not defined.");
                return;
            }

            var handlers = _eventHandlers[eventType];
            if (!handlers.Any())
            {
                Debug.WriteLine($"Handler for event type {eventType.Name} not defined.");
                return;
            }

            foreach (var handler in handlers)
            {
                var instance = context.GetContextInstance(handler);
                var method = handler.GetMethods()
                    .FirstOrDefault(m => m.Name == "Apply" && m.GetParameters()[0].ParameterType == eventType);
                method?.Invoke(instance, new object[] { eventDto });
            }
        }

        public void Handle<T>(EventContext context, T commandDto) where T : Command
        {
            var commandType = commandDto.GetType();
            if (!_commandHandlers.ContainsKey(commandType))
            {
                Debug.WriteLine($"Handler for command type {commandType.Name} not defined.");
                return;
            }

            var handlers = _commandHandlers[commandType];
            if (!handlers.Any())
            {
                Debug.WriteLine($"Handler for command type {commandType.Name} not defined.");
                return;
            }

            foreach (var handler in handlers)
            {
                var instance = context.GetContextInstance(handler);
                var method = handler.GetMethods()
                    .FirstOrDefault(m => m.Name == "Handle" && m.GetParameters()[0].ParameterType == commandType);
                method?.Invoke(instance, new object[] { commandDto });
            }
        }
    }
}
