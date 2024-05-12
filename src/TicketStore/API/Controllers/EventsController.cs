using Application.Event.Commands;
using Application.Organizer.Commands;
using Core.Mediator;
using Domain.Event.DTOs;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class EventsController : ControllerBase
{
    private readonly IMediatorHandler _mediator;

    public EventsController(IMediatorHandler mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("{organizerId}")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(List<Error>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post(Guid organizerId, CreateEventDTO eventDTO, CancellationToken cancellationToken)
    {
        var result = await _mediator.SendCommand<CreateEventCommand, ErrorOr<Guid>>(
            new CreateEventCommand(organizerId, eventDTO), cancellationToken);

        if (result.IsError)
        {
            return BadRequest(result.Errors);
        }

        return Created(string.Empty, new { Id = result.Value });
    }
}
