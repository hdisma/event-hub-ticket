namespace EventHubTicket.Management.Application.Dtos
{
    public class CreateCategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
