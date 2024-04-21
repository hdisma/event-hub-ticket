using EventHubTicket.Management.Application.Abstractions.Infrastructure;
using EventHubTicket.Management.Application.Abstractions.Persistence;
using EventHubTicket.Management.Application.Features.Events.Queries.GetEventsExport;
using EventHubTicket.Management.Domain.Entities;
using Moq;
using System.Text;

namespace EventHubTicket.Management.Application.Tests.Events
{
    public class GetEventsExportQueryHandlerTests
    {
        private readonly Mock<IAsyncRepository<Event>> _eventRepositoryMock;
        private readonly Mock<ICsvExporter> _csvExporterMock;

        public GetEventsExportQueryHandlerTests()
        {
            _eventRepositoryMock = new Mock<IAsyncRepository<Event>>();
            _csvExporterMock = new Mock<ICsvExporter>();
        }

        [Fact]
        public async void Handle_WhenInvoked_DataShouldNotBeNullOrEmpty()
        {
            _eventRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(GetEvents());

            _csvExporterMock.Setup(e => e.ExportToCsv(It.IsAny<IReadOnlyList<Event>>())).Returns(GetExportToCsvMock);

            var getEventsExportQueryHandler = new GetEventsExportQueryHandler(_eventRepositoryMock.Object,
                _csvExporterMock.Object);

            var result = await getEventsExportQueryHandler.Handle(new GetEventsExportQuery(), CancellationToken.None);

            Assert.Multiple(
                () => Assert.NotNull(result),
                () => Assert.NotNull(result.Data),
                () => Assert.NotEmpty(result.Data!));
        }

        private static byte[] GetExportToCsvMock(IReadOnlyList<Event> data)
        {
            var dummyCsvBuilder = new StringBuilder()
                    .AppendLine("Id,Name,Price,Organizer,CategoryId");

            foreach (var @event in data)
            {
                dummyCsvBuilder.AppendLine($"{@event.Id},{@event.Name},{@event.Price},{@event.Organizer},{@event.CategoryId}");
            }

            using var memoryStream = new MemoryStream();
            using (var writer = new StreamWriter(memoryStream))
            {
                writer.Write(dummyCsvBuilder.ToString());
            }
            return memoryStream.ToArray();
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
