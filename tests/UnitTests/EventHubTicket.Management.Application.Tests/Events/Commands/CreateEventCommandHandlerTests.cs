using AutoMapper;
using EventHubTicket.Management.Application.Abstractions.Infrastructure;
using EventHubTicket.Management.Application.Abstractions.Persistence;
using EventHubTicket.Management.Application.Exceptions;
using EventHubTicket.Management.Application.Features.Events.Commands.CreateEvent;
using EventHubTicket.Management.Application.Mappings;
using EventHubTicket.Management.Application.Models.Mail;
using EventHubTicket.Management.Domain.Entities;
using Moq;

namespace EventHubTicket.Management.Application.Tests.Events.Commands
{
    public class CreateEventCommandHandlerTests
    {
        private readonly Mock<IEventRepository> _eventRepositoryMock;
        private readonly Mock<IEmailService> _emailServiceMock;
        private readonly IMapper _mapper;

        private List<Event>? _events;

        public CreateEventCommandHandlerTests()
        {
            _eventRepositoryMock = new Mock<IEventRepository>();
            _emailServiceMock = new Mock<IEmailService>();
            _mapper = new MapperConfiguration(config => config.AddProfile<MappingProfile>())
                .CreateMapper();
        }

        [Fact]
        public async Task Handle_WhenValidCreateEventCommand_ShouldReturnCreatedEventId()
        {
            InitializeEvents();
            Guid createdEvent = Guid.NewGuid();
            CreateEventCommand validCreateEventCommand = new()
            {
                Name = "Live Moscow 1991",
                Price = 250,
                Organizer = "Metallica",
                Date = DateTime.UtcNow.AddMonths(2),
                CategoryId = Guid.Parse("8079d26a-1272-4ef5-9e9b-26d7ac4fdb7c")
            };

            _eventRepositoryMock.Setup(er => er.IsEventNameAndDateUnique(It.IsAny<string>(), It.IsAny<DateTime>()))
                .ReturnsAsync(true);

            _eventRepositoryMock.Setup(er => er.CreateAsync(It.IsAny<Event>())).Returns((Event @event) =>
            {
                @event.Id = createdEvent;
                _events!.Add(@event);
                return Task.FromResult(@event);
            });

            _emailServiceMock.Setup(es => es.SendEmailAsync(It.IsAny<Email>())).ReturnsAsync(true);

            var createEventCommandHandler = new CreateEventCommandHandler(_eventRepositoryMock.Object,
                _emailServiceMock.Object,
                _mapper);

            var result = await createEventCommandHandler.Handle(validCreateEventCommand, CancellationToken.None);

            Assert.Equal(createdEvent, result);
        }

        [Fact]
        public async Task Handle_WhenValidCreateEventCommand_ShouldShouldInsertNewEvent()
        {
            InitializeEvents();
            Guid createdEvent = Guid.NewGuid();
            CreateEventCommand validCreateEventCommand = new()
            {
                Name = "Live Moscow 1991",
                Price = 250,
                Organizer = "Metallica",
                Date = DateTime.UtcNow.AddMonths(2),
                CategoryId = Guid.Parse("8079d26a-1272-4ef5-9e9b-26d7ac4fdb7c")
            };

            _eventRepositoryMock.Setup(er => er.IsEventNameAndDateUnique(It.IsAny<string>(), It.IsAny<DateTime>()))
                .ReturnsAsync(true);

            _eventRepositoryMock.Setup(er => er.CreateAsync(It.IsAny<Event>())).Returns((Event @event) =>
            {
                @event.Id = createdEvent;
                _events!.Add(@event);
                return Task.FromResult(@event);
            });

            _emailServiceMock.Setup(es => es.SendEmailAsync(It.IsAny<Email>())).ReturnsAsync(true);

            var createEventCommandHandler = new CreateEventCommandHandler(_eventRepositoryMock.Object,
                _emailServiceMock.Object,
                _mapper);

            await createEventCommandHandler.Handle(validCreateEventCommand, CancellationToken.None);

            Assert.Equal(3, _events!.Count);
        }

        [Fact]
        public async Task Handle_WhenInvalidCreateEventCommand_ShouldThrowValidationException()
        {
            InitializeEvents();
            Guid createdEvent = Guid.NewGuid();
            CreateEventCommand invalidCreateEventCommandWithoutDate = new()
            {
                Name = "Live Moscow 1991",
                Price = 250,
                Organizer = "Metallica",
                CategoryId = Guid.Parse("8079d26a-1272-4ef5-9e9b-26d7ac4fdb7c")
            };

            _eventRepositoryMock.Setup(er => er.IsEventNameAndDateUnique(It.IsAny<string>(), It.IsAny<DateTime>()))
                .ReturnsAsync(true);

            _eventRepositoryMock.Setup(er => er.CreateAsync(It.IsAny<Event>())).Returns((Event @event) =>
            {
                @event.Id = createdEvent;
                _events!.Add(@event);
                return Task.FromResult(@event);
            });

            _emailServiceMock.Setup(es => es.SendEmailAsync(It.IsAny<Email>())).ReturnsAsync(true);

            var createEventCommandHandler = new CreateEventCommandHandler(_eventRepositoryMock.Object,
                _emailServiceMock.Object,
                _mapper);

            await Assert.ThrowsAsync<ValidationException>(async () =>
                await createEventCommandHandler.Handle(invalidCreateEventCommandWithoutDate, CancellationToken.None));
        }

        private void InitializeEvents()
        {
            _events = new List<Event>()
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
