using System.Threading.Tasks;
using EventStream.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventStream.Client.Controllers
{
    [Route("")]
    public class IndexController: ControllerBase
    {
        private readonly IEventStreamClient _client;

        public IndexController(IEventStreamClient client)
        {
            _client = client;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }
    }
}