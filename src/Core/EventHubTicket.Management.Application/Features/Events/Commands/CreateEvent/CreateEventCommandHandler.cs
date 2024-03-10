using AutoMapper;
using EventHubTicket.Management.Application.Abstractions.Infrastructure;
using EventHubTicket.Management.Application.Abstractions.Persistence;
using EventHubTicket.Management.Application.Exceptions;
using EventHubTicket.Management.Application.Models.Mail;
using EventHubTicket.Management.Domain.Entities;
using MediatR;

namespace EventHubTicket.Management.Application.Features.Events.Commands.CreateEvent
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IEmailService _emailService;
        private IMapper _mapper;

        public CreateEventCommandHandler(IEventRepository eventRepository,
            IEmailService emailService,
            IMapper mapper)
        {
            _eventRepository = eventRepository;
            _emailService = emailService;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateEventCommandValidator(_eventRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.Errors.Any())
            {
                throw new ValidationException(validationResult);
            }

            var @event = _mapper.Map<Event>(request);
            var email = new Email
            {
                To = "test@test.test",
                Body = $"A new event was created: {request}",
                Subject = "A new event was created"
            };

            try
            {
                await _emailService.SendEmailAsync(email);
            }
            catch (Exception)
            {
                // TODO: Log the exception
                // An error when sending the email shouldn't stop this command execution.
            }

            @event = await _eventRepository.CreateAsync(@event);

            return @event.Id;
        }
    }
}
