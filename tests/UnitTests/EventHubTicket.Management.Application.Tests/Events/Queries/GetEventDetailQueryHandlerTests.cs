using AutoMapper;
using EventHubTicket.Management.Application.Abstractions.Persistence;
using EventHubTicket.Management.Application.Features.Events.Queries.GetEventDetail;
using EventHubTicket.Management.Application.Mappings;
using EventHubTicket.Management.Domain.Entities;
using Moq;

namespace EventHubTicket.Management.Application.Tests.Events
{
    public class GetEventDetailQueryHandlerTests
    {
        private readonly Mock<IAsyncRepository<Event>> _eventRepositoryMock;
        private readonly Mock<IAsyncRepository<Category>> _categoryRepositoryMock;
        private readonly IMapper _mapper;

        public GetEventDetailQueryHandlerTests()
        {
            _eventRepositoryMock = new Mock<IAsyncRepository<Event>>();
            _categoryRepositoryMock = new Mock<IAsyncRepository<Category>>();
            _mapper = new MapperConfiguration(config => config.AddProfile<MappingProfile>())
                .CreateMapper();
        }

        [Fact]
        public async Task Handle_WhenInvokedWithValidId_ShouldReturnEventWithDetail()
        {
            _eventRepositoryMock.Setup(h => h.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Guid id) =>
            {
                return GetEvents().Where(e => e.Id == id).FirstOrDefault();
            });

            _categoryRepositoryMock.Setup(h => h.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Guid id) =>
            {
                return GetCategories().Where(e => e.Id == id).FirstOrDefault();
            });

            var getEventDetailQueryHandler = new GetEventDetailQueryHandler(
                _eventRepositoryMock.Object,
                _categoryRepositoryMock.Object,
                _mapper);

            var getEventDetailQuery = new GetEventDetailQuery() { Id = Guid.Parse("3448d5a4-0f72-4dd7-bf15-c14a46b26c00") };

            var result = await getEventDetailQueryHandler.Handle(getEventDetailQuery, CancellationToken.None);

            Assert.Multiple(
                () => Assert.NotNull(result),
                () => Assert.Equal("Led Zeppelin", result.Organizer),
                () => Assert.Equal("Concerts", result.Category.Name));
        }

        private static IReadOnlyList<Event> GetEvents() =>
        [
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
        ];

        private static IReadOnlyList<Category> GetCategories() =>
        [
            new Category
            {
                Id = Guid.Parse("b4e08da5-230c-4151-96d9-f8e6d1f1de97"),
                Name = "Musicals"
            },
            new Category
            {
                Id = Guid.Parse("8079d26a-1272-4ef5-9e9b-26d7ac4fdb7c"),
                Name = "Concerts"
            }
        ];
    }
}
