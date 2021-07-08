using System;
using System.Net;

namespace EventStream.Domain.Models
{
    public class Client
    {
        public string Name { get; set; }
        public IPAddress IpAddress { get; set; }
        public Guid ConnectionId { get; set; }
    }
}