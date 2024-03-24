using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventHubTicket.Management.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
