using Doctork.Application.Abstraction;
using Doctork.Application.Dtos;
using Doctork.Application.Interfaces;
using Doctork.Application.Queries.AuthQueries;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Doctork.Application.Helpers;

namespace Doctork.Application.Handlers.AuthHandlers;
internal class ResendCodeHandler : IRequestHandler<ResendCodeQuery, AuthDto>
{
    private readonly ICacheService _cacheService;
    private readonly IAuthentication _authentication;
    private readonly IEmailSender _emailSender;
    private readonly TimeSpan _CodeExpiration = TimeSpan.FromMinutes(5);

    public ResendCodeHandler(ICacheService cacheService, IAuthentication authentication, IEmailSender emailSender)
    {
        _cacheService = cacheService;
        _authentication = authentication;
        _emailSender = emailSender;
    }
    public async Task<AuthDto> Handle(ResendCodeQuery request, CancellationToken cancellationToken)
    {
        var user = await _authentication.GetUserByEmailAsync(request.EmailDto.Email);

        if (user == null)
            return new AuthDto { Succeeded = false, Message = "Email is incorrect" };

        var key = $"{user.Id}_VerificationCode";

        var cachedCode = _cacheService.Get<string>(key);

        if (cachedCode != null)
            _cacheService.Remove(key);

        var code = RandomCodeGenerator.GenerateCode();

        await _emailSender.SendEmailAsync(user.Email, "Verification Code", $"Your verification code is {code}");

        _cacheService.Set(key, code, _CodeExpiration);

        return new AuthDto { Succeeded = true, Message = "Verificatoin code sent successfully" };
    }
}
