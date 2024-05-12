using Application.Organizer.Commands;
using Core.Messages;
using Domain.Organizer;
using Domain.Organizer.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Organizer
{
    public class OrganizerHandler : CommandHandler, IRequestHandler<RegisterOrganizerCommand, ErrorOr<Guid>>
    {
        private readonly IOrganizerRepository _organizerRepository;

        public OrganizerHandler(IOrganizerRepository organizerRepository)
        {
            _organizerRepository = organizerRepository;
        }

        public async Task<ErrorOr<Guid>> Handle(RegisterOrganizerCommand request, CancellationToken cancellationToken)
        {
            var organizer = Organizers.Register(request.OrganizerDTO);
            if (organizer.IsError)
            {
                return organizer.Errors;
            }

            _organizerRepository.Create(organizer.Value);

            await _organizerRepository.UnitOfWork.CommitAsync(cancellationToken);

            return organizer.Value.Id;
        }
    }
}