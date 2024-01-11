using EventHubTicket.Management.Application.Dtos;

namespace EventHubTicket.Management.Application.ViewModels
{
    public class CategoryEventListViewModel
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<CategoryEventDto>? Events { get; set; }
    }
}
