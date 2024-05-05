using Application.Organizer.Commands;
using Domain.Organizer.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class OrganizerController : ControllerBase
{
    private readonly IMediator _handler;

    public OrganizerController(IMediator handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateOrganizerDTO organizer, CancellationToken cancellationToken)
    {
        var result = await _handler.Send(new RegisterOrganizerCommand(organizer), cancellationToken);
        if (result.IsError)
        {
            return BadRequest(result.Errors);
        }

        return Created(string.Empty, result.Value);
    }
}
