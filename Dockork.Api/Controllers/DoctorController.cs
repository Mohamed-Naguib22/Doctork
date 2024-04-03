using Doctork.Application.Abstraction;
using Doctork.Application.Commands.AuthCommands;
using Doctork.Application.Commands.DoctorCommands;
using Doctork.Application.Dtos.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Doctork.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class DoctorController : ControllerBase
{
    private readonly IMediator _mediator;
    public DoctorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("confirm-doctor")]
    public async Task<IActionResult> VerifyDoctor(EmailDto emailDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = new ConfirmDoctorCommand(emailDto.Email);

        var result = await _mediator.Send(command);

        return result ? Ok("Doctor account confirmed successfully") : NotFound("Doctor account is not found");
    }
}
