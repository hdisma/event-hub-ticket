using AutoMapper;
using EventHubTicket.Management.Application.Abstractions.Persistence;
using EventHubTicket.Management.Application.ViewModels;
using EventHubTicket.Management.Domain.Entities;
using MediatR;

namespace EventHubTicket.Management.Application.Features.Categories.Queries.GetCategories
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery,
        List<CategoryListViewModel>>
    {
        private readonly IAsyncRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public GetCategoriesQueryHandler(IAsyncRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<List<CategoryListViewModel>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = (await _categoryRepository.GetAllAsync())
                .OrderBy(c => c.Name);

            return _mapper.Map<List<CategoryListViewModel>>(categories);
        }
    }
}
