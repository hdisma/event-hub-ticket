using EventHubTicket.Management.Application.ViewModels;
using MediatR;

namespace EventHubTicket.Management.Application.Features.Events.Queries.GetEventDetail
{
    public class GetEventDetailQuery : IRequest<EventDetailViewModel>
    {
        public Guid Id { get; set; }

    }
}
