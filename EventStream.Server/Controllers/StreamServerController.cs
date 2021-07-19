using System.Threading.Tasks;
using EventStream.Domain;
using EventStream.Server.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace EventStream.Server.Controllers
{
    [Route("api/test")]
    public class StreamServerController: ControllerBase
    {
        private readonly StreamDatabaseContext _dbContext;

        public StreamServerController(StreamDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string name)
        {
            var stream = _dbContext.GetStream(name);
            var events = await stream.Collection.FindAsync(@event => true);
            return Ok(await events.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddEvent(Event @event)
        {
            var stream = _dbContext.GetStream(@event.Stream);
            await stream.AddEvent(@event);
            return Ok();
        }
        
    }
}