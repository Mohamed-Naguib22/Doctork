using Dapper;
using Doctork.Application.Abstraction;
using Doctork.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Infrastructure.Services;
public class Authentication : IAuthentication
{
    private readonly IDbConnection _connection;
    public Authentication(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task AddRoleAsync(Role role) =>
        await _connection.ExecuteAsync("AddRole", role, commandType: CommandType.StoredProcedure);

    public async Task ConfirmEmailAsync(string userId) =>
        await _connection.ExecuteAsync("UPDATE Users SET IsEmailConfirmed = 1 WHERE Id = @Id", new { Id = userId } );

    public async Task<Role> GetRoleByNameAsync(string roleName) =>
        await _connection.QuerySingleOrDefaultAsync<Role>("GetRoleByName", new { Name = roleName }, commandType: CommandType.StoredProcedure);

    public async Task<User> GetUserByEmailAsync(string email) =>
        await _connection.QuerySingleOrDefaultAsync<User>("GetUserByEmail", new { Email = email }, commandType: CommandType.StoredProcedure);

    public async Task InsertUserAsync(User user) =>
        await _connection.ExecuteAsync("AddUser", user, commandType: CommandType.StoredProcedure);

    public async Task<string> GetRoleNameAsync(string id) =>
        await _connection.QuerySingleOrDefaultAsync<string>("GetRoleName", new { Id = id });

    public async Task<RefreshToken> GetActiveTokenAsync(string userId) =>
        await _connection.QuerySingleOrDefaultAsync<RefreshToken>("GetActiveRefreshToken", new { UserId = userId });

    public async Task AddRefreshTokenAsync(RefreshToken refreshToken) =>
        await _connection.ExecuteAsync("AddRefreshToken", refreshToken, commandType: CommandType.StoredProcedure);

    public async Task<User> GetUserFromRefreshTokenAsync(string token) =>
        await _connection.QuerySingleOrDefaultAsync<User>("GetUserFromRefreshToken", new { Token = token }, commandType: CommandType.StoredProcedure);
}
