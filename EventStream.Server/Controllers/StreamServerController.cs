using EventStream.Server.Socket;
using Microsoft.AspNetCore.Mvc;

namespace EventStream.Server.Controllers
{
    [Route("")]
    public class StreamServerController: ControllerBase
    {
        public StreamServerController(Protocol protocol)
        {
            
        }
        
        
    }
}