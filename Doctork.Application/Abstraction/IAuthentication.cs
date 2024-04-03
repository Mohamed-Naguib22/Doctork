using Doctork.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Application.Abstraction;
public interface IAuthentication
{
    Task AddRoleAsync(Role role);
    Task ConfirmEmailAsync(string userId);
    Task<Role> GetRoleByNameAsync(string roleName);
    Task InsertUserAsync(User user);
    Task<User> GetUserByEmailAsync(string email);
    Task<string> GetRoleNameAsync(string id);
    Task<RefreshToken> GetActiveTokenAsync(string userId);
    Task AddRefreshTokenAsync(RefreshToken refreshToken);
    Task<User> GetUserFromRefreshTokenAsync(string token);
}
