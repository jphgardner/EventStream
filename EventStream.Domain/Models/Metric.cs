using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EventStream.Domain.Models
{
    public class Metric
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}