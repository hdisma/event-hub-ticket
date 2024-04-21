using AutoMapper;
using EventHubTicket.Management.Application.Abstractions.Persistence;
using EventHubTicket.Management.Domain.Entities;
using MediatR;

namespace EventHubTicket.Management.Application.Features.Events.Commands.UpdateEvent
{
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand>
    {
        private readonly IAsyncRepository<Event> _eventRepository;
        private readonly IMapper _mapper;

        public UpdateEventCommandHandler(IAsyncRepository<Event> eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var eventToUpdate = await _eventRepository.GetByIdAsync(request.EventId);

            if (eventToUpdate is null)
            {
                return;
            }

            _mapper.Map(
                request,
                eventToUpdate,
                typeof(UpdateEventCommand),
                typeof(Event));

            await _eventRepository.UpdateAsync(eventToUpdate!);
        }
    }
}
