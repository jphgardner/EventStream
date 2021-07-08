using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using EventStream.Domain.Events;
using EventStream.Domain.Interfaces;
using EventStream.Domain.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace EventStream.Domain
{
    public class EventStreamClient : IEventStreamClient
    {
        private readonly TcpClient _tcpClient = new();
        private readonly EventStreamOptions _options;
        private NetworkStream _stream;
        private EventStreamConnection _connection;

        public EventStreamClient(IOptions<EventStreamOptions> options)
        {
            _options = options.Value;
            Connect();
        }

        private async void Connect()
        {
            try
            {
                Console.WriteLine("Connecting");
                await _tcpClient.ConnectAsync(_options.Host, _options.Port);
                Console.WriteLine("Connected");

                _stream = _tcpClient.GetStream();

                _connection = new EventStreamConnection(_stream);
                await Authenticate();

                await Task.Run(ReadLoop);
            }
            catch
            {
                Console.WriteLine("something went wrong");
            }
        }

        private async Task Authenticate()
        {
            await _stream.Send(FrameId.Connect,
                JsonConvert.SerializeObject(new ConnectEvent
                    { Username = _options.Username, Password = _options.Password }));
            Console.WriteLine($"Authenticating...");
        }

        private async Task ReadLoop()
        {
            while (!_connection.Cancellation.IsCancellationRequested)
            {
                try
                {
                    var message = await _stream.ReadRawMessage();

                    if (message == null)
                    {
                        //Server closed Connection
                        Console.WriteLine($"Server closed Connection");
                    }

                    switch ((FrameId) message[0])
                    {
                        case FrameId.Connected:
                            var connectionId = Encoding.UTF8.GetString(message, 1, message.Length - 1);
                            _connection.ConnectionId = Guid.Parse(connectionId);
                            Console.WriteLine($"Connected, {_connection.ConnectionId}");
                            break;
                    }
                }
                catch (Exception e)
                {
                    _connection.Cancellation.Cancel();
                    Console.WriteLine($"Exception {e.Message}");
                }
            }
        }
    }
}