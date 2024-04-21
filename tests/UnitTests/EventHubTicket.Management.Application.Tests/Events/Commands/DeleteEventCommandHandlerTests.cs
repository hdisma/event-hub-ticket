using EventHubTicket.Management.Application.Abstractions.Persistence;
using EventHubTicket.Management.Application.Features.Events.Commands.DeleteEvent;
using EventHubTicket.Management.Domain.Entities;
using Moq;

namespace EventHubTicket.Management.Application.Tests.Events.Commands
{
    public class DeleteEventCommandHandlerTests
    {
        private readonly Mock<IEventRepository> _eventRepositoryMock;

        public DeleteEventCommandHandlerTests()
        {
            _eventRepositoryMock = new Mock<IEventRepository>();
        }

        [Fact]
        public async Task Handle_WhenEventToDeleteExists_ShouldInvokeDeleteAsync()
        {
            var events = GetEvents();
            var eventIdToDelete = Guid.Parse("3cec3a9e-d8b0-4e7d-b42c-4446eff9f67b");
            var eventToDelete = events.FirstOrDefault(e => e.Id == eventIdToDelete);

            _eventRepositoryMock.Setup(er => er.GetAllAsync()).ReturnsAsync(events);
            _eventRepositoryMock.Setup(er => er.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Guid eventId) =>
            {
                return eventToDelete;
            });

            _eventRepositoryMock.Setup(er => er.DeleteAsync(It.IsAny<Event>())).Callback((Event @event) =>
            {
                events.Remove(@event);
            });

            var deleteEventCommandHandler = new DeleteEventCommandHandler(_eventRepositoryMock.Object);
            var deleteEventCommand = new DeleteEventCommand { EventId = eventIdToDelete };

            await deleteEventCommandHandler.Handle(deleteEventCommand, CancellationToken.None);

            Assert.Multiple(
                () => _eventRepositoryMock.Verify(er => er.GetByIdAsync(It.Is<Guid>(h => h.Equals(eventIdToDelete))), Times.Once),
                () => _eventRepositoryMock.Verify(er => er.DeleteAsync(It.Is<Event>(h => h.Equals(eventToDelete))), Times.Once),
                () => Assert.Single(events));
        }

        [Fact]
        public async Task Handle_WhenEventToDeleteDoesNotExists_ShouldInvokeDeleteAsync()
        {
            var events = GetEvents();
            var invalidEventIdToDelete = Guid.NewGuid();
            var eventToDelete = events.FirstOrDefault(e => e.Id == invalidEventIdToDelete);

            _eventRepositoryMock.Setup(er => er.GetAllAsync()).ReturnsAsync(events);
            _eventRepositoryMock.Setup(er => er.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Guid eventId) =>
            {
                return eventToDelete;
            });

            _eventRepositoryMock.Setup(er => er.DeleteAsync(It.IsAny<Event>())).Callback((Event @event) =>
            {
                events.Remove(@event);
            });

            var deleteEventCommandHandler = new DeleteEventCommandHandler(_eventRepositoryMock.Object);
            var deleteEventCommand = new DeleteEventCommand { EventId = invalidEventIdToDelete };

            await deleteEventCommandHandler.Handle(deleteEventCommand, CancellationToken.None);

            Assert.Multiple(
                () => _eventRepositoryMock.Verify(er => er.GetByIdAsync(It.Is<Guid>(h => h.Equals(invalidEventIdToDelete))), Times.Once),
                () => _eventRepositoryMock.Verify(er => er.DeleteAsync(It.Is<Event>(h => h == null)), Times.Never),
                () => Assert.Equal(2, events.Count));
        }

        private List<Event> GetEvents()
        {
            return new List<Event>()
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
            };
        }
    }
}
