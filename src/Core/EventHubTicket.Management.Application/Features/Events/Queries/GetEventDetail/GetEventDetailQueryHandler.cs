using AutoMapper;
using EventHubTicket.Management.Application.Abstractions.Persistence;
using EventHubTicket.Management.Application.Dtos;
using EventHubTicket.Management.Application.ViewModels;
using EventHubTicket.Management.Domain.Entities;
using MediatR;

namespace EventHubTicket.Management.Application.Features.Events.Queries.GetEventDetail
{
    public class GetEventDetailQueryHandler : IRequestHandler<GetEventDetailQuery,
        EventDetailViewModel>
    {
        private readonly IAsyncRepository<Event> _eventRepository;
        private readonly IAsyncRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public GetEventDetailQueryHandler(
            IAsyncRepository<Event> eventRepository,
            IAsyncRepository<Category> categoryRepository,
            IMapper mapper)
        {
            _eventRepository = eventRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<EventDetailViewModel> Handle(GetEventDetailQuery request, CancellationToken cancellationToken)
        {
            var @event = await _eventRepository.GetByIdAsync(request.Id);
            var eventDetailViewModel = _mapper.Map<EventDetailViewModel>(@event);

            var category = await _categoryRepository.GetByIdAsync(@event!.CategoryId);

            eventDetailViewModel.Category = _mapper.Map<CategoryDto>(category);

            return eventDetailViewModel;
        }
    }
}
