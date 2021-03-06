using System;
using System.Net;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EventStream.Domain.Models
{
    public class Client
    {
        [BsonId] 
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public IPAddress IpAddress { get; set; }
        public Guid ConnectionId { get; set; }
    }
}