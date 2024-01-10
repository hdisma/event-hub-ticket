using EventHubTicket.Management.Application.ViewModels;
using MediatR;

namespace EventHubTicket.Management.Application.Features.Events.Queries.GetEvents
{
    public class GetEventsQuery : IRequest<List<EventListViewModel>>
    {
    }
}
