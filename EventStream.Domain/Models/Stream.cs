using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EventStream.Domain.Models
{
    public class Stream
    {
        [BsonId] 
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        public List<ObjectId> Connections { get; set; }
    }
}