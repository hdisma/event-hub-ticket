using EventHubTicket.Management.Application.ViewModels;
using MediatR;

namespace EventHubTicket.Management.Application.Features.Categories.Queries.GetCategories
{
    public class GetCategoriesQuery : IRequest<List<CategoryListViewModel>>
    {
    }
}
