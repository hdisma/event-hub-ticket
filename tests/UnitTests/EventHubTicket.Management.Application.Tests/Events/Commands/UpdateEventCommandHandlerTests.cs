using AutoMapper;
using EventHubTicket.Management.Application.Abstractions.Persistence;
using EventHubTicket.Management.Application.Features.Events.Commands.UpdateEvent;
using EventHubTicket.Management.Application.Mappings;
using EventHubTicket.Management.Domain.Entities;
using Moq;

namespace EventHubTicket.Management.Application.Tests.Events.Commands
{
    public class UpdateEventCommandHandlerTests
    {
        private readonly Mock<IAsyncRepository<Event>> _eventRepository;
        private readonly IMapper _mapper;

        public UpdateEventCommandHandlerTests()
        {
            _eventRepository = new Mock<IAsyncRepository<Event>>();
            _mapper = new MapperConfiguration(config => config.AddProfile<MappingProfile>())
                .CreateMapper();
        }

        [Fact]
        public async Task Handle_WhenEventNotFound_ShouldNotInvokeUpdateAsync()
        {
            var events = GetEvents();
            var notFoundEventId = Guid.NewGuid();

            _eventRepository.Setup(er => er.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid eventId) => events.FirstOrDefault(e => e.Id == eventId));

            var updateEventCommand = new UpdateEventCommand() { EventId = notFoundEventId };
            var updateEventCommandHandler = new UpdateEventCommandHandler(_eventRepository.Object, _mapper);

            await updateEventCommandHandler.Handle(updateEventCommand, CancellationToken.None);

            _eventRepository.Verify(er => er.UpdateAsync(It.IsAny<Event>()), Times.Never);
        }

        [Fact]
        public async Task Handle_WhenEventFound_ShouldInvokeUpdateAsync()
        {
            var events = GetEvents();
            var notFoundEventId = Guid.Parse("3448d5a4-0f72-4dd7-bf15-c14a46b26c00");

            _eventRepository.Setup(er => er.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid eventId) => events.FirstOrDefault(e => e.Id == eventId));

            var updateEventCommand = new UpdateEventCommand() { EventId = notFoundEventId };
            var updateEventCommandHandler = new UpdateEventCommandHandler(_eventRepository.Object, _mapper);

            await updateEventCommandHandler.Handle(updateEventCommand, CancellationToken.None);

            _eventRepository.Verify(er => er.UpdateAsync(It.IsAny<Event>()), Times.Once);
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
