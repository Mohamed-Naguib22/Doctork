using Doctork.Application.Queries.InsuranceCompanyQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Doctork.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InsuranceCompanyController : ControllerBase
{
    private readonly IMediator _mediator;
    public InsuranceCompanyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("get")]
    public async Task<IActionResult> GetAllInsuranceCompaniesAsync()
    {
        var query = new GetAllInsuranceCompaniesQuery();

        return Ok(await _mediator.Send(query));
    }
}
