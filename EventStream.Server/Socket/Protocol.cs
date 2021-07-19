using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using EventStream.Domain;
using EventStream.Domain.Events;
using EventStream.Server.Repositories;

namespace EventStream.Server.Socket
{
    public class Protocol
    {
        private readonly StreamDatabaseContext _streamDatabase;
        private readonly Dictionary<Guid, EventStreamConnection> _clients = new();
        private readonly Dictionary<Guid, Task> _clientLoops = new();

        public Protocol(StreamDatabaseContext streamDatabase)
        {
            _streamDatabase = streamDatabase;
        }

        public async Task HandleClient(TcpClient client)
        {
            var connection = new EventStreamConnection(client.GetStream());

            _clients.Add(connection.ConnectionId, connection);
            _clientLoops.Add(connection.ConnectionId, Task.Run(() => ClientLoop(connection)));

            Console.WriteLine($"{connection.ConnectionId} Connected!");
        }

        private bool Close(Guid connectionId)
        {
            var connection = _clients[connectionId];
            connection.Cancellation.Cancel();
            _clients.Remove(connectionId);
            return _clientLoops.Remove(connectionId);
        }

        public void DisconnectClients()
        {
            foreach (var client in _clients)
            {
                client.Value.Stream.Close();
                _clients.Remove(client.Key);
                _clientLoops.Remove(client.Key);
            }

            Console.WriteLine("Clients Disconnected");
        }

        private async void ClientLoop(EventStreamConnection connection)
        {
            Observable.While(() => !connection.Cancellation.IsCancellationRequested,
                Observable.FromAsync(connection.Stream.ReadRawMessage)).Subscribe(async message =>
            {
                if (message == null)
                {
                    Console.WriteLine($"Connection closed");
                    Close(connection.ConnectionId);
                }

                switch ((FrameId) message[0])
                {
                    case FrameId.Connect:
                        Console.WriteLine($"Connect {message.Decode<ConnectEvent>()}");
                        await connection.Stream.Send(FrameId.Connected, connection.ConnectionId.ToString());
                        break;
                    case FrameId.Publish:
                        
                        break;
                    case FrameId.Subscribe:
                        
                        break;
                }
            }, (exception) =>
            {
                connection.Stream.Close();
                Close(connection.ConnectionId);
                Console.WriteLine($"{connection.ConnectionId} Disconnected");
            });
        }
    }
}