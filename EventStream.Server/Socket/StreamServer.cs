using System;
using System.Net;
using System.Net.Sockets;
using System.Reactive.Linq;
using System.Threading;
using EventStream.Domain;

namespace EventStream.Server.Socket
{
    public class StreamServer
    {
        private readonly Protocol _protocol;
        private readonly StreamServerOptions _options;
        private readonly CancellationToken _cancellation;
        private TcpListener _server;
        private IDisposable _acceptObs;

        public StreamServer(Protocol protocol, StreamServerOptions options, CancellationToken cancellation)
        {
            _protocol = protocol;
            _options = options;
            _cancellation = cancellation;
        }

        public void Listen(IPAddress ip)
        {
            _server = new TcpListener(ip, _options.Port);
            _server.Start(_options.Backlog);

            Console.WriteLine("EventStream Started on: {0}", _server.LocalEndpoint);

            _cancellation.Register(Stop);

            _acceptObs = Observable.While(() => !_cancellation.IsCancellationRequested,
                Observable.FromAsync(_server.AcceptTcpClientAsync)).Subscribe(async client =>
            {
                await _protocol.HandleClient(client);
            }, (exception) =>
            {
                _protocol.DisconnectClients();
                Console.WriteLine("EventStream Stopped");
            }, () =>
            {
                Console.WriteLine("EventStream2 Stopped");
            });
        }

        private void Stop()
        {
            _server.Stop();
            _acceptObs.Dispose();
        }
    }
}