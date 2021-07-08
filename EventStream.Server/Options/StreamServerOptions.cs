using System.Net;

namespace EventStream.Domain
{
    public class StreamServerOptions
    {
        public IPAddress IP { get; set; }
        public int Port { get; set; }
        public int BufferSize { get; set; }
        public int Backlog { get; set; }
    }
}