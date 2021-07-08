using System.ComponentModel;
using EventStream.Domain;
using MongoDB.Driver;

namespace EventStream.Server.Repositories
{
    public class StreamCollection
    {
        public readonly string Name;
        public readonly IMongoCollection<Event> Collection;
        
        public StreamCollection(string name, IMongoCollection<Event> collection)
        {
            Name = name;
            Collection = collection;
        }

        public void AddEvent(Event @event)
        {
            Collection.InsertOne(@event);
        }
    }
}