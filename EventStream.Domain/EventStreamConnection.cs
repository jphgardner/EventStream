using System;
using System.Net.Sockets;
using System.Threading;

namespace EventStream.Domain
{
    public class EventStreamConnection
    {
        public  Guid ConnectionId { get; set; }
        public readonly CancellationTokenSource Cancellation = new CancellationTokenSource();
        public NetworkStream Stream { get; }

        public EventStreamConnection(NetworkStream stream)
        {
            ConnectionId = Guid.NewGuid();
            Stream = stream;
        }
    }
}