using Doctork.Application.Abstraction;
using Doctork.Application.Dtos;
using Doctork.Application.Interfaces;
using Doctork.Application.Queries.AuthQueries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Application.Handlers.AuthHandlers;
public class VerifyAccountHandler : IRequestHandler<VerifyAccountQuery, AuthDto>
{
    private readonly ICacheService _cacheService;
    private readonly IAuthentication _authentication;

    public VerifyAccountHandler(ICacheService cacheService, IAuthentication authentication)
    {
        _cacheService = cacheService;
        _authentication = authentication;
    }
    public async Task<AuthDto> Handle(VerifyAccountQuery request, CancellationToken cancellationToken)
    {
        var verifyAccountDto = request.VerifyAccountDto;
        var user = await _authentication.GetUserByEmailAsync(verifyAccountDto.Email);

        if (user == null)
            return new AuthDto { Succeeded = false, Message = "The email is incorrect" };

        var cachedCode = _cacheService.Get<string>($"{user.Id}_VerificationCode");

        if (cachedCode == null || verifyAccountDto.Code != cachedCode)
            return new AuthDto { Succeeded = false, Message = "The verification code is invalid or expired" };

        await _authentication.ConfirmEmailAsync(user.Id);

        return new AuthDto { Succeeded = true, Message = "Account confirmed successfully" };
    }
}

