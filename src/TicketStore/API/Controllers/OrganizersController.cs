using Application.Organizer.Commands;
using Core.Mediator;
using Domain.Organizer.DTOs;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class OrganizersController : ControllerBase
{
    private readonly IMediatorHandler _mediator;

    public OrganizersController(IMediatorHandler mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(List<Error>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post(RegisterOrganizerDTO organizer, CancellationToken cancellationToken)
    {
        var result = await _mediator.SendCommand<RegisterOrganizerCommand, ErrorOr<Guid>>(
            new RegisterOrganizerCommand(organizer), cancellationToken);

        if (result.IsError)
        {
            return BadRequest(result.Errors);
        }

        return Created(string.Empty, new { Id = result.Value });
    }
}
