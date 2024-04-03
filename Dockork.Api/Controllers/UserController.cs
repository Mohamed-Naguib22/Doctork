using Doctork.Application.Commands.UserCommands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Doctork.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpDelete("delete-account")]
    public async Task<IActionResult> SetImageAsync()
    {
        var refreshToken = Request.Cookies["refreshToken"];

        if (refreshToken == null)
            return BadRequest("Invalid Token");

        var command = new DeleteAccountCommand(refreshToken);

        var result = await _mediator.Send(command);

        return result ? Ok("Account deleted successfully") : BadRequest("Invalid Token");
    }
}
