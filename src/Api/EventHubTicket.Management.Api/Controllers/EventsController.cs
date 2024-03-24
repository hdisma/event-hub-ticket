using EventHubTicket.Management.Application.Features.Events.Commands.CreateEvent;
using EventHubTicket.Management.Application.Features.Events.Queries.GetEventDetail;
using EventHubTicket.Management.Application.Features.Events.Queries.GetEvents;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventHubTicket.Management.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "GetEvents")]
        public async Task<IActionResult> GetEvents()
        {
            return Ok(await _mediator.Send(new GetEventsQuery()));
        }

        [HttpGet("{id}", Name = "GetEvent")]
        public async Task<IActionResult> GetEvent(Guid id)
        {
            return Ok(await _mediator.Send(new GetEventDetailQuery() { Id = id }));
        }

        [HttpPost(Name = "CreateEvent")]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventCommand createEventCommand)
        {
            var newEventId = await _mediator.Send(createEventCommand);

            return Created("GetEvent", new { id = newEventId });
        }
    }
}
