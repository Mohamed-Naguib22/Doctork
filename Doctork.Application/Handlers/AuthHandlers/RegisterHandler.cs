using AutoMapper;
using Doctork.Application.Abstraction;
using Doctork.Application.Commands.AuthCommands;
using Doctork.Application.Dtos;
using Doctork.Application.Helpers;
using Doctork.Application.Interfaces;
using Doctork.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Application.Handlers.AuthHandlers;
public class RegisterHandler : IRequestHandler<RegisterCommand, AuthDto>
{
    private readonly IJwtService _jwtService;
    private readonly IAuthentication _authentication;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IEmailSender _emailSender;
    private readonly ICacheService _cacheService;
    private readonly TimeSpan _CodeExpiration = TimeSpan.FromMinutes(5);
    public RegisterHandler(IJwtService jwtService, IAuthentication authentication, ICacheService cacheService,
        IPasswordHasher passwordHasher, IUnitOfWork unitOfWork, IMapper mapper, IEmailSender emailSender)
    {
        _jwtService = jwtService;
        _authentication = authentication;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _emailSender = emailSender;
        _cacheService = cacheService;
    }
    public async Task<AuthDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var registerDto = request.RegisterDto;

        var role = await _authentication.GetRoleByNameAsync("Patient");

        if (role == null)
            return new AuthDto { Succeeded = false, Message = "Invalid Role" };

        if (await _authentication.GetUserByEmailAsync(request.RegisterDto.Email) != null)
            return new AuthDto { Succeeded = false, Message = "This email is already registered" };

        var user = _mapper.Map<User>(registerDto);

        user.Id = Guid.NewGuid().ToString();
        user.PasswordHash = _passwordHasher.Hash(registerDto.Password);
        user.RoleId = role.Id;

        await _authentication.InsertUserAsync(user);

        var verificationCode = RandomCodeGenerator.GenerateCode();

        await _emailSender.SendEmailAsync(user.Email, "Verification Code", $"Your verification code is {verificationCode}");
        _cacheService.Set($"{user.Id}_VerificationCode", verificationCode, _CodeExpiration);
        
        return new AuthDto { Succeeded = true, Message = "Check your email to verify your account" };
    }
}
