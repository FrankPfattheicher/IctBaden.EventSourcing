using System;
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace IctBaden.EventSourcing
{
    public abstract class Event
    {
        public string Id { get; set; }
        public DateTime Timestamp { get; set; }

        protected Event()
        {
            Id = Guid.NewGuid().ToString("N");
            Timestamp = DateTime.Now;
        }
    }
}
