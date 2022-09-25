using Api.Model;
using Api2.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/event")]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository eventRepository;
        public EventController(IEventRepository eventRepository)
        {
            this.eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
        }

        [HttpPost]
        public async Task<ActionResult> Index([FromBody]string json)
        {
           await eventRepository.AddEvents(json);
            
            return Ok(json);
        }

        
    }
}
