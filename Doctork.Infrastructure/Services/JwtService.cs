using Doctork.Application.Abstraction;
using Doctork.Application.Dtos.Auth;
using Doctork.Domain.Models;
using Doctork.Persistence.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Infrastructure.Services;
public class JwtService : IJwtService
{

    private readonly JWT _jwt;
    private readonly IAuthentication _authentication;
    public JwtService(IOptions<JWT> jwt, IAuthentication authentication)
    {
        _jwt = jwt.Value;
        _authentication = authentication;
    }
    public async Task<TokenDto> GenerateAccessToken(User user)
    {
        var role = await _authentication.GetRoleNameAsync(user.RoleId);

        var userClaims = new List<Claim>();
        var roleClaims = new List<Claim>
        {
            new Claim("roles", role)
        };

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("uid", user.Id)
        }
        .Union(userClaims)
        .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: claims,
            expires: DateTime.Now.AddDays(_jwt.DurationInDays),
            signingCredentials: signingCredentials
        );
        var tokenDto = new TokenDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            ExpiresOn = jwtSecurityToken.ValidTo
        };
        return tokenDto;
    }

    public async Task<RefreshToken> GenerateRefreshToken(string userId)
    {
        var randomNumber = new byte[32];

        using var generator = new RNGCryptoServiceProvider();

        generator.GetBytes(randomNumber);

        var refreshToken = new RefreshToken
        {
            UserId = userId,
            Token = Convert.ToBase64String(randomNumber),
            ExpiresOn = DateTime.Now.AddDays(10),
            CreatedOn = DateTime.Now
        };

        await _authentication.AddRefreshTokenAsync(refreshToken);

        return refreshToken;
    }
}
