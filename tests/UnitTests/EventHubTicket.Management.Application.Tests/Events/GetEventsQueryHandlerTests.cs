using AutoMapper;
using EventHubTicket.Management.Application.Abstractions.Persistence;
using EventHubTicket.Management.Application.Features.Events.Queries.GetEvents;
using EventHubTicket.Management.Application.Mappings;
using EventHubTicket.Management.Domain.Entities;
using Moq;

namespace EventHubTicket.Management.Application.Tests.Events
{
    public class GetEventsQueryHandlerTests
    {
        private readonly Mock<IAsyncRepository<Event>> _eventRepositoryMock;
        private readonly IMapper _mapper;

        public GetEventsQueryHandlerTests()
        {
            _eventRepositoryMock = new Mock<IAsyncRepository<Event>>();
            _mapper = new MapperConfiguration(config => config.AddProfile<MappingProfile>())
                .CreateMapper();
        }

        [Fact]
        public async void Handle_WhenInvoked_ShouldReturnEvents()
        {
            _eventRepositoryMock.Setup(h => h.GetAllAsync()).ReturnsAsync(GetEvents());

            var getEventsQueryHandler = new GetEventsQueryHandler(_mapper, _eventRepositoryMock.Object);

            var result = await getEventsQueryHandler.Handle(new GetEventsQuery(), CancellationToken.None);

            Assert.Multiple(
                () => Assert.NotNull(result),
                () => Assert.Equal(2, result.Count));
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
    }
}
