using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace IctBaden.EventSourcing.EventStore
{
    public class FileEventStore : IEventStore
    {
        private readonly string _basePath;

        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            TypeNameHandling = TypeNameHandling.All
        };

        public FileEventStore(string basePath)
        {
            _basePath = basePath; 
        }

        public void Dispose()
        {
        }

        private string GetStreamDirectory(string eventStream)
        {
            var path = Path.Combine(_basePath, eventStream);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }

        private string GetEventFileName(string eventStream, Event eventDto)
        {
            var fileName = $"{eventDto.Timestamp.ToFileTimeUtc():D12}.json";
            return Path.Combine(GetStreamDirectory(eventStream), fileName);
        }

        public bool Save(string eventStream, Event eventDto)
        {
            var json = JsonConvert.SerializeObject(eventDto, Settings);
            File.WriteAllText(GetEventFileName(eventStream, eventDto), json);
            return true;
        }

        public bool Save(string eventStream, Event[] events)
        {
            foreach (var ev in events)
            {
                if (!Save(eventStream, ev)) return false;
            }
            return true;
        }

        public IEnumerable<Event> Replay(string eventStream)
        {
            var path = GetStreamDirectory(eventStream);
            var files = Directory.EnumerateFiles(path, "*.json")
                .OrderBy(n => n)
                .ToList();
            foreach (var file in files)
            {
                var json = File.ReadAllText(file);
                var ev = (Event)JsonConvert.DeserializeObject(json, Settings);
                yield return ev;
            }
        }

        public IEnumerable<Event> Replay(string eventStream, Type[] eventTypes)
        {
            return Replay(eventStream)
                .Where(ev => eventTypes.Contains(ev.GetType()));
        }
    }
}
