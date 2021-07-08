using System;
using Newtonsoft.Json;

namespace EventStream.Domain
{
    public class Event
    {
        public string Stream { get; set; }
        public string Name { get; set; }
        private string _payload;
        public object Payload
        {
            get => _payload;
            set => _payload = JsonConvert.SerializeObject(value);
        }

        public DateTime DateTime { get; }
        // public EventHeaders Headers = new EventHeaders();

        public Event(string stream, string name, object payload)
        {
            Stream = stream;
            Name = name;
            Payload = payload;
            DateTime = DateTime.UtcNow;
        }

        public override string ToString() => JsonConvert.SerializeObject(this);
        
    }
}