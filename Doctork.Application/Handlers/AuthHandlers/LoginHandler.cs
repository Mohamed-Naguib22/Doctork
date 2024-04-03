using Doctork.Application.Abstraction;
using Doctork.Application.Dtos;
using Doctork.Application.Queries.AuthQueries;
using Doctork.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Application.Handlers.AuthHandlers;
internal class LoginHandler : IRequestHandler<LoginQuery, AuthDto>
{
    private readonly IJwtService _jwtService;
    private readonly IAuthentication _authentication;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;
    public LoginHandler(IJwtService jwtService, IAuthentication authentication, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork)
    {
        _jwtService = jwtService;
        _authentication = authentication;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
    }
    public async Task<AuthDto> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var loginDto = request.loginDto;

        var user = await _authentication.GetUserByEmailAsync(loginDto.Email);

        if (user == null || !_passwordHasher.Verify(user.PasswordHash, loginDto.Password) || !user.IsEmailConfirmed)
            return new AuthDto { Succeeded = false, Message = "Email or password is incorrect" };

        var userRole = await _authentication.GetRoleNameAsync(user.RoleId);

        if (userRole == "Doctor")
        {
            if (!await _unitOfWork.Doctors.IsVerifiedAsync(user.Id))
                return new AuthDto { Succeeded = false, Message = "Account verification required" };
        }

        var tokenDto = await _jwtService.GenerateAccessToken(user);
        
        var refreshToken = await _authentication.GetActiveTokenAsync(user.Id);
        
        var authDto = new AuthDto
        {
            Succeeded = true,
            IsAuthenticated = true,
            Token = tokenDto.Token,
            ExpiresOn = tokenDto.ExpiresOn,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            Role = userRole
        };

        if (refreshToken != null)
        {
            authDto.RefreshToken = refreshToken.Token;
            authDto.RefreshTokenExpiration = refreshToken.ExpiresOn;
        }
        else
        {
            var newRefreshToken = await _jwtService.GenerateRefreshToken(user.Id);
            authDto.RefreshToken = newRefreshToken.Token;
            authDto.RefreshTokenExpiration = newRefreshToken.ExpiresOn;
        }

        return authDto;
    }
}
