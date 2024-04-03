using Doctork.Application.Abstraction;
using Doctork.Application.Queries.ClinicQueries;
using Doctork.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Doctork.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClinicController : ControllerBase
{

    private readonly IMediator _mediator;
    public ClinicController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("get")]
    public async Task<IActionResult> GetAllClinicsAsync()
    {
        var query = new GetAllClinicsQuery();

        return Ok(await _mediator.Send(query));
    }
}