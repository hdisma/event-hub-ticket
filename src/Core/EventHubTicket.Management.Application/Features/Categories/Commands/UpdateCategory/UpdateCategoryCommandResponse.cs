using EventHubTicket.Management.Application.Dtos;
using EventHubTicket.Management.Application.Responses;

namespace EventHubTicket.Management.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandResponse : BaseResponse
    {
        public UpdateCategoryCommandResponse() : base() { }

        public UpdateCategoryDto Category { get; set; } = default!;
    }
}
