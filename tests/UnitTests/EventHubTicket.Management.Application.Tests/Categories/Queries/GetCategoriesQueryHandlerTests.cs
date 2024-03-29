using AutoMapper;
using EventHubTicket.Management.Application.Abstractions.Persistence;
using EventHubTicket.Management.Application.Features.Categories.Queries.GetCategories;
using EventHubTicket.Management.Application.Mappings;
using EventHubTicket.Management.Domain.Entities;
using Moq;

namespace EventHubTicket.Management.Application.Tests.Categories.Queries
{
    public class GetCategoriesQueryHandlerTests
    {
        private readonly Mock<IAsyncRepository<Category>> _categoryRepositoryMock;
        private readonly IMapper _mapper;

        public GetCategoriesQueryHandlerTests()
        {
            _categoryRepositoryMock = new Mock<IAsyncRepository<Category>>();
            _mapper = new MapperConfiguration(config => config.AddProfile<MappingProfile>())
                .CreateMapper();
        }

        [Fact]
        public async void Handle_WhenInvoked_ShouldReturnCategoriesSorted()
        {
            _categoryRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(GetCategories());

            var getCategoriesQueryHandler = new GetCategoriesQueryHandler(_categoryRepositoryMock.Object, _mapper);

            var result = await getCategoriesQueryHandler.Handle(new GetCategoriesQuery(), CancellationToken.None);

            Assert.Multiple(
                () => Assert.Equal(3, result.Count),
                () => Assert.Equal("Concerts", result.First().Name));
        }

        private IReadOnlyList<Category> GetCategories()
        {
            return new List<Category>
            {
                new Category
                {
                    Id = Guid.Parse("b4e08da5-230c-4151-96d9-f8e6d1f1de97"),
                    Name = "Musicals"
                },
                new Category
                {
                    Id = Guid.Parse("8079d26a-1272-4ef5-9e9b-26d7ac4fdb7c"),
                    Name = "Concerts"
                },
                new Category
                {
                    Id = Guid.Parse("3fad44b5-a5d2-41a7-bbb4-d3390a3a2dfb"),
                    Name = "Conferences"
                }
            };
        }
    }
}
