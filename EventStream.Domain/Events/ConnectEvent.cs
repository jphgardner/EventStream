using EventStream.Domain.Interfaces;

namespace EventStream.Domain.Events
{
    public class ConnectEvent: IEvent
    {
        public string Client { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}