using System.Net;
using System.Threading;
using System.Threading.Tasks;
using EventStream.Domain;
using EventStream.Server.Socket;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EventStream.Server.HostedService
{
    public class StreamServerHost: BackgroundService
    {
        private readonly ILogger<StreamServerHost> _logger;
        private readonly StreamServerOptions _options;

        private readonly Protocol _protocol;
        private  StreamServer _streamServer;

        public StreamServerHost(ILogger<StreamServerHost> logger, IOptions<StreamServerOptions> options, Protocol protocol)
        {
            _logger = logger;
            _options = options.Value;
            _protocol = protocol;
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetupServer(stoppingToken);
        }
        
        private void SetupServer(CancellationToken stoppingToken)
        {
            _streamServer = new StreamServer(_protocol, _options, stoppingToken);
            _streamServer.Listen(IPAddress.Any);
        }
    }
}