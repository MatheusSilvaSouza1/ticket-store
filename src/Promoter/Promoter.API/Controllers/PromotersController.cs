using Application.Promoter.Commands;
using Core.Mediator;
using Domain.Promoter.DTOs;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class PromotersController : ControllerBase
{
    private readonly IMediatorHandler _mediator;

    public PromotersController(IMediatorHandler mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(List<Error>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post(RegisterPromoterDTO promoter, CancellationToken cancellationToken)
    {
        var result = await _mediator.SendCommand<RegisterPromoterCommand, ErrorOr<Guid>>(
            new RegisterPromoterCommand(promoter), cancellationToken);

        if (result.IsError)
        {
            return BadRequest(result.Errors);
        }

        return Created(string.Empty, new { Id = result.Value });
    }
}
