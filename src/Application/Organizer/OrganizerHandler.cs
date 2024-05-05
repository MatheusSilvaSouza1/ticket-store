using Application.Organizer.Commands;
using Domain.Organizer;
using ErrorOr;
using MediatR;

namespace Application.Organizer
{
    public class OrganizerHandler : IRequestHandler<RegisterOrganizerCommand, ErrorOr<Guid>>
    {
        // private readonly IOrganizerRepository _organizerRepository;

        // public OrganizerHandler(IOrganizerRepository organizerRepository)
        // {
        //     _organizerRepository = organizerRepository;
        // }

        public async Task<ErrorOr<Guid>> Handle(RegisterOrganizerCommand request, CancellationToken cancellationToken)
        {
            // verificar se o cnpj foi cadastrado

            // validar parametros

            var organizer = Organizers.Create(request.OrganizerDTO);
            if (organizer.IsError)
            {
                return organizer.Errors;
            }

            return organizer.Value.Id;
        }
    }
}