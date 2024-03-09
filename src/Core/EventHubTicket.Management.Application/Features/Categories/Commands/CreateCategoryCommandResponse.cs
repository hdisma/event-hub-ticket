using EventHubTicket.Management.Application.Dtos;
using EventHubTicket.Management.Application.Responses;

namespace EventHubTicket.Management.Application.Features.Categories.Commands
{
    public class CreateCategoryCommandResponse : BaseResponse
    {
        public CreateCategoryCommandResponse() : base() { }

        public CreateCategoryDto Category { get; set; } = default!;
    }
}
