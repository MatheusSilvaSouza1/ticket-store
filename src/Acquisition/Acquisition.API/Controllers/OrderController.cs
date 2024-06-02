namespace Acquisition.API.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    // [ProducesResponseType(typeof(List<Error>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Post()
    {
        return Ok();
    }
}