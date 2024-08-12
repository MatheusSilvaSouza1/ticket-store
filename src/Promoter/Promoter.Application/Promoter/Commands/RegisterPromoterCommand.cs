using Core.Messages;
using Domain.Promoter.DTOs;
using FluentResults;

namespace Application.Promoter.Commands;

public class RegisterPromoterCommand : ICommand<Result<Guid>>
{
    public RegisterPromoterDTO PromoterDTO { get; init; }

    public RegisterPromoterCommand(RegisterPromoterDTO promoterDTO)
    {
        PromoterDTO = promoterDTO;
    }
}