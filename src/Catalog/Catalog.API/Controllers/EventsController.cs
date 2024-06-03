using Application.Event.Commands;
using Application.Event.Queries;
using Core.Mediator;
using Domain.Event.DTOs;
using ErrorOr;
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

    [HttpPost("/{promoterId}/[controller]")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(List<Error>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post(Guid promoterId, CreateEventDTO eventDTO, CancellationToken cancellationToken)
    {
        var result = await _mediator.SendCommand<CreateEventCommand, ErrorOr<Guid>>(
            new CreateEventCommand(promoterId, eventDTO), cancellationToken);

        if (result.IsError)
        {
            return BadRequest(result.Errors);
        }

        return Created(string.Empty, new { Id = result.Value });
    }

    [HttpPost("/{promoterId}/[controller]/{eventId}/publish")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(List<Error>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PostPublish(
        Guid promoterId,
        Guid eventId, [FromBody] PublishEventDTO publishEvent,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.SendCommand<PublishEventCommand, ErrorOr<Guid>>(
            new PublishEventCommand(promoterId, eventId, publishEvent.PublishAt), cancellationToken);

        if (result.IsError)
        {
            return BadRequest(result.Errors);
        }

        return Ok();
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<EventsResponseDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(
        CancellationToken cancellationToken)
    {
        var result = await _mediator.SendQuery<GetEventsQuery, List<EventsResponseDTO>>(new GetEventsQuery(), cancellationToken);

        if (result.Count > 0)
        {
            return Ok(result);
        }

        return NoContent();
    }
}
