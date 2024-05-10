using Application.Organizer.Commands;
using Core.Mediator;
using Domain.Organizer.DTOs;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class OrganizerController : ControllerBase
{
    private readonly IMediatorHandler _mediator;

    public OrganizerController(IMediatorHandler mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Post(RegisterOrganizerDTO organizer, CancellationToken cancellationToken)
    {
        var result = await _mediator.SendCommand<RegisterOrganizerCommand, ErrorOr<Guid>>(
            new RegisterOrganizerCommand(organizer), cancellationToken);

        if (result.IsError)
        {
            return BadRequest(result.Errors);
        }

        return Created(string.Empty, result.Value);
    }
}
