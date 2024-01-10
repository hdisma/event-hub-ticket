using EventHubTicket.Management.Application.Dtos;

namespace EventHubTicket.Management.Application.ViewModels
{
    public class EventDetailViewModel
    {
        public Guid EventId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int Price { get; set; }
        public string? Organizer { get; set; }
        public DateTime Date { get; set; }
        public string? ImageUrl { get; set; }
        public Guid CategoryId { get; set; }
        public CategoryDto Category { get; set; } = default!;
    }
}
