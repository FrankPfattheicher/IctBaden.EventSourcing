using System;
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace IctBaden.EventSourcing
{
    public abstract class Command
    {
        public string Id { get; set; }
        public DateTime Timestamp { get; set; }

        protected Command()
        {
            Id = Guid.NewGuid().ToString("N");
            Timestamp = DateTime.Now;
        }
    }
}
