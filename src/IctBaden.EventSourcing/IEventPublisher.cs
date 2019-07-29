﻿using System.Threading;
using System.Threading.Tasks;

namespace IctBaden.EventSourcing
{
    public interface IEventPublisher
    {
        EventContext Context { get; set; }

        Task Publish<T>(string eventStream, T eventDto, CancellationToken cancellationToken = default) where T : Event;
    }
}