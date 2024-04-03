using Doctork.Application.Queries.SpecializationQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Doctork.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class SpecializationController : ControllerBase
{

    private readonly IMediator _mediator;
    public SpecializationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("get")]
    public async Task<IActionResult> GetAllSpecializationsAsync()
    {
        var query = new GetAllSpecializationsQuery();

        return Ok(await _mediator.Send(query));
    }
}
