using Dapper;
using Doctork.Application.Abstraction.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Persistence.Repositories;
public class UserRepository : IUserRepository
{
    private readonly IDbConnection _connection;
    public UserRepository(IDbConnection connection)
    {
        _connection = connection;
    }
    public async Task DeleteUserAsync(string id) =>
        await _connection.ExecuteAsync("DeleteUser", new { Id = id } , commandType: CommandType.StoredProcedure);
}
