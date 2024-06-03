using Core.Messages;
using Domain.Promoter.DTOs;
using ErrorOr;

namespace Application.Promoter.Commands;

public class RegisterPromoterCommand : ICommand<ErrorOr<Guid>>
{
    public RegisterPromoterDTO PromoterDTO { get; init; }

    public RegisterPromoterCommand(RegisterPromoterDTO promoterDTO)
    {
        PromoterDTO = promoterDTO;
    }
}