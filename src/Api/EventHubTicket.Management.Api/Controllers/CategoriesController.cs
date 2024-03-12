using EventHubTicket.Management.Application.Features.Categories.Queries.GetCategories;
using EventHubTicket.Management.Application.Features.Categories.Queries.GetCategoriesWithEvents;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventHubTicket.Management.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        public readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            return Ok(await _mediator.Send(new GetCategoriesQuery()));
        }

        [HttpGet("events", Name = "GetCategoriesWithEvents")]
        public async Task<IActionResult> GetCategoriesWithEvents([FromQuery] bool includeHistory)
        {
            var getCategoriesWithEventsQuery = new GetCategoriesWithEventsQuery()
            {
                IncludeHistory = includeHistory
            };

            return Ok(await _mediator.Send(getCategoriesWithEventsQuery));
        }
    }
}
