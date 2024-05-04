using AutoMapper;
using EventHubTicket.Management.Application.Abstractions.Persistence;
using EventHubTicket.Management.Domain.Entities;
using MediatR;

namespace EventHubTicket.Management.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, UpdateCategoryCommandResponse>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<UpdateCategoryCommandResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var updateCategoryCommandResponse = new UpdateCategoryCommandResponse();
            var categoryToUpdate = await _categoryRepository.GetByIdAsync(request.Id);

            var validationResult = await new UpdateCategoryCommandValidator().ValidateAsync(request, cancellationToken);

            if (validationResult.Errors.Any())
            {
                updateCategoryCommandResponse.Success = false;
                updateCategoryCommandResponse.ValidationErrors = new();

                foreach (var error in validationResult.Errors)
                {
                    updateCategoryCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                }
            }

            if (categoryToUpdate is null)
            {
                throw new ArgumentException($"Category {request.Id} not found.");
            }

            if (updateCategoryCommandResponse.Success)
            {
                _mapper.Map(
                    request,
                    categoryToUpdate,
                    typeof(UpdateCategoryCommand),
                    typeof(Category));

                await _categoryRepository.UpdateAsync(categoryToUpdate!);
            }

            return updateCategoryCommandResponse;
        }
    }
}
