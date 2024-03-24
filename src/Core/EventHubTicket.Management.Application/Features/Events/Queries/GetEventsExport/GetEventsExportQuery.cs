using MediatR;

namespace EventHubTicket.Management.Application.Features.Events.Queries.GetEventsExport
{
    public class GetEventsExportQuery : IRequest<EventExportFileViewModel>
    {
    }
}
