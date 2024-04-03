using Dapper;
using Doctork.Application.Abstraction.Repositories;
using Doctork.Application.Dtos.ClinicDtos;
using Doctork.Application.Dtos.SpecializationDtos;
using Doctork.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Persistence.Repositories;
public class SpecializationRepository : ISpecializationRepository
{
    private readonly IDbConnection _connection;
    public SpecializationRepository(IDbConnection connection)
    {
        _connection = connection;
    }
    public async Task<IEnumerable<GetSpecializationDto>> GetAllSpecializationsAsync() =>
        await _connection.QueryAsync<GetSpecializationDto>("SELECT Id, Name FROM Specializations");

    public async Task<int> GetSpecializationIdAsync(string specialization) =>
        await _connection.QuerySingleOrDefaultAsync<int>("GetSpecializationId", new { Name = specialization }, commandType: CommandType.StoredProcedure);
}
