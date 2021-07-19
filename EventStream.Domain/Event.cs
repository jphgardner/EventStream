using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace EventStream.Domain
{
    public class Event
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Stream { get; set; }
        public string Subject { get; set; }
        public string Payload { get; set; }

        public DateTime DateTime { get; set; }
        // public EventHeaders Headers = new EventHeaders();

        public Event()
        {
        }
        
        public Event(string stream, string subject, object payload)
        {
            Stream = stream;
            Subject = subject;
            Payload = JsonConvert.SerializeObject(payload);
            DateTime = DateTime.UtcNow;
        }

        public override string ToString() => JsonConvert.SerializeObject(this);
        
    }
}