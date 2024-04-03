using Doctork.Application.Dtos.Auth;
using Doctork.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Application.Abstraction;
public interface IJwtService
{
    Task<TokenDto> GenerateAccessToken(User user);
    Task<RefreshToken> GenerateRefreshToken(string userId);
}
