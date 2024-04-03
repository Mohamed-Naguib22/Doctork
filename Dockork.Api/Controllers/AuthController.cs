using Doctork.Application.Abstraction;
using Doctork.Application.Commands.AuthCommands;
using Doctork.Application.Dtos;
using Doctork.Application.Dtos.Auth;
using Doctork.Application.Queries.AuthQueries;
using Doctork.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dockork.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAuthentication _authentication;
        public AuthController(IMediator mediator, IAuthentication authentication)
        {
            _mediator = mediator;
            _authentication = authentication;
        }

        //[HttpGet("add-role")]
        //public async Task<IActionResult> AddRoleAsync([FromQuery] string name)
        //{
        //    var role = new Role
        //    {
        //        Id = Guid.NewGuid().ToString(),
        //        Name = name,
        //    };

        //    await _authentication.AddRoleAsync(role);
        //    return NoContent();
        //}

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new RegisterCommand(registerDto);

            var result = await _mediator.Send(command);

            return result.Succeeded ? Ok(result.Message) : BadRequest(result.Message);
        }

        [HttpPost("register-doctor")]
        public async Task<IActionResult> RegisterDoctorAsync([FromForm] RegisterDoctorDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new RegisterDoctorCommand(registerDto);

            var result = await _mediator.Send(command);

            return result.Succeeded ? Ok(result.Message) : BadRequest(result.Message);
        }

        [HttpPost("verify-account")]
        public async Task<IActionResult> VerifyAccountAsync([FromBody] VerifyAccountDto verifyAccountDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var query = new VerifyAccountQuery(verifyAccountDto);

            var result = await _mediator.Send(query);

            return result.Succeeded ? Ok(result.Message) : BadRequest(result.Message);
        }

        [HttpPost("resend-code")]
        public async Task<IActionResult> ResendVerificationAsync([FromBody] EmailDto emailDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var query = new ResendCodeQuery(emailDto);

            var result = await _mediator.Send(query);

            return result.Succeeded ? Ok(result.Message) : BadRequest(result.Message);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var query = new LoginQuery(loginDto);

            var result = await _mediator.Send(query);

            if (!result.Succeeded)
                return BadRequest(result.Message);

            SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

            return Ok(result);
        }

        private void SetRefreshTokenInCookie(string refreshToken, DateTime expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expires.ToLocalTime(),
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None
            };

            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
        private string GetRefreshTokenFromCookie()
        {
            return Request.Cookies["refreshToken"];
        }
    }
}