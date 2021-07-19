using System.ComponentModel;
using System.Threading.Tasks;
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

        public async Task AddEvent(Event @event)
        {
            await Collection.InsertOneAsync(@event);
        }
    }
}