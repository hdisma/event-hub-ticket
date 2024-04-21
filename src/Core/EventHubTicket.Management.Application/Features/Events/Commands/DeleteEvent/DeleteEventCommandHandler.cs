using EventHubTicket.Management.Application.Abstractions.Persistence;
using EventHubTicket.Management.Domain.Entities;
using MediatR;

namespace EventHubTicket.Management.Application.Features.Events.Commands.DeleteEvent
{
    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand>
    {
        private readonly IAsyncRepository<Event> _eventRepository;

        public DeleteEventCommandHandler(IAsyncRepository<Event> eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var eventToDelete = await _eventRepository.GetByIdAsync(request.EventId);

            if (eventToDelete is null)
            {
                return;
            }

            await _eventRepository.DeleteAsync(eventToDelete!);
        }
    }
}
