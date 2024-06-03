using Application.Promoter.Commands;
using Core.Messages;
using Domain.Promoter;
using Domain.Promoter.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Promoter
{
    public class PromoterCommandHandler : CommandHandler, IRequestHandler<RegisterPromoterCommand, ErrorOr<Guid>>
    {
        private readonly IPromoterRepository _promoterRepository;

        public PromoterCommandHandler(IPromoterRepository promoterRepository)
        {
            _promoterRepository = promoterRepository;
        }

        public async Task<ErrorOr<Guid>> Handle(RegisterPromoterCommand request, CancellationToken cancellationToken)
        {
            var exists = await _promoterRepository.ExistsAsync(e => e.Cnpj == request.PromoterDTO.Cnpj);

            var promoter = Promoters.Register(promoterDTO: request.PromoterDTO, promoterAlreadyExists: exists);
            if (promoter.IsError)
            {
                return promoter.Errors;
            }

            _promoterRepository.Create(promoter.Value);

            await _promoterRepository.UnitOfWork.CommitAsync(cancellationToken);

            return promoter.Value.Id;
        }
    }
}