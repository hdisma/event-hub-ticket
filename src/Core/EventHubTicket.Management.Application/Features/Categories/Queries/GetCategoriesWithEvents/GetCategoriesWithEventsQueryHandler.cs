using AutoMapper;
using EventHubTicket.Management.Application.Abstractions.Persistence;
using EventHubTicket.Management.Application.ViewModels;
using MediatR;

namespace EventHubTicket.Management.Application.Features.Categories.Queries.GetCategoriesWithEvents
{
    public class GetCategoriesWithEventsQueryHandler : IRequestHandler<GetCategoriesWithEventsQuery,
        List<CategoryEventListViewModel>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetCategoriesWithEventsQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<List<CategoryEventListViewModel>> Handle(
            GetCategoriesWithEventsQuery request,
            CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetCategoriesWithEvents(request.IncludeHistory);

            return _mapper.Map<List<CategoryEventListViewModel>>(categories);
        }
    }
}
