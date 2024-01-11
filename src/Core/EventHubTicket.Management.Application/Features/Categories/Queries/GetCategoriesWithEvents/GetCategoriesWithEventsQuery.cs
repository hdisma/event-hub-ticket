using EventHubTicket.Management.Application.ViewModels;
using MediatR;

namespace EventHubTicket.Management.Application.Features.Categories.Queries.GetCategoriesWithEvents
{
    public class GetCategoriesWithEventsQuery : IRequest<List<CategoryEventListViewModel>>
    {
        public bool IncludeHistory { get; set; }
    }
}
