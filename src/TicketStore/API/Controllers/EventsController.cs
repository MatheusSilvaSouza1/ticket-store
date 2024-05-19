using Application.Event.Commands;
using Core.Mediator;
using Domain.Event.DTOs;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("{organizerId}/[controller]")]
public class EventsController : ControllerBase
{
    private readonly IMediatorHandler _mediator;

    public EventsController(IMediatorHandler mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
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

    [HttpPost("{eventId}/publish")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(List<Error>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PostPublish(
        Guid organizerId,
        Guid eventId, [FromBody] PublishEventDTO publishEvent,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.SendCommand<PublishEventCommand, ErrorOr<Guid>>(
            new PublishEventCommand(organizerId, eventId, publishEvent.PublishAt), cancellationToken);

        if (result.IsError)
        {
            return BadRequest(result.Errors);
        }

        return Ok();
    }
}
