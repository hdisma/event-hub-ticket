using AutoMapper;
using EventHubTicket.Management.Application.Abstractions.Persistence;
using EventHubTicket.Management.Application.Features.Categories.Queries.GetCategoriesWithEvents;
using EventHubTicket.Management.Application.Mappings;
using EventHubTicket.Management.Domain.Entities;
using Moq;

namespace EventHubTicket.Management.Application.Tests.Categories.Queries
{
    public class GetCategoriesWithEventsQueryHandlerTests
    {
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly IMapper _mapper;

        public GetCategoriesWithEventsQueryHandlerTests()
        {
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _mapper = new MapperConfiguration(config => config.AddProfile<MappingProfile>())
                .CreateMapper();
        }

        [Fact]
        public async void Handle_WhenInvoked_ShouldReturnCategoriesWithEvents()
        {
            _categoryRepositoryMock.Setup(r => r.GetCategoriesWithEvents(It.IsAny<bool>())).ReturnsAsync(GetCategoriesWithEvents());

            var getCategoriesWithEventsQueryHandler = new GetCategoriesWithEventsQueryHandler(_categoryRepositoryMock.Object, _mapper);
            var getCategoriesWithEventsQuery = new GetCategoriesWithEventsQuery() { IncludeHistory = true };

            var result = await getCategoriesWithEventsQueryHandler.Handle(getCategoriesWithEventsQuery, CancellationToken.None);

            Assert.Multiple(
                () => Assert.NotNull(result),
                () => Assert.NotEmpty(result.Select(c => c.Events)),
                () => Assert.Collection(result, c =>
                {
                    Assert.Collection(c.Events!,
                        e =>
                        {
                            Assert.Equal("3448d5a4-0f72-4dd7-bf15-c14a46b26c00", e.EventId.ToString());
                            Assert.Equal("Led Zeppelin", e.Organizer);
                        },
                        e =>
                        {
                            Assert.Equal("3cec3a9e-d8b0-4e7d-b42c-4446eff9f67b", e.EventId.ToString());
                            Assert.Equal("Pink Floyd", e.Organizer);
                        }
                    );
                })
            );
        }

        private List<Category> GetCategoriesWithEvents()
        {
            return new List<Category>
            {
                new Category
                {
                    Id = Guid.Parse("8079d26a-1272-4ef5-9e9b-26d7ac4fdb7c"),
                    Name = "Concerts",
                    Events = new List <Event>
                    {
                        new Event
                        {
                            Id = Guid.Parse("3448d5a4-0f72-4dd7-bf15-c14a46b26c00"),
                            Name = "The Song Remains the Same",
                            Price = 150,
                            Organizer = "Led Zeppelin",
                            CategoryId = Guid.Parse("8079d26a-1272-4ef5-9e9b-26d7ac4fdb7c")
                        },
                        new Event
                        {
                            Id = Guid.Parse("3cec3a9e-d8b0-4e7d-b42c-4446eff9f67b"),
                            Name = "Live at Pompeii",
                            Price = 200,
                            Organizer = "Pink Floyd",
                            CategoryId = Guid.Parse("8079d26a-1272-4ef5-9e9b-26d7ac4fdb7c")
                        }
                    }
                }
            };
        }
    }
}
