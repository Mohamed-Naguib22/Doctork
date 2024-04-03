using Dapper;
using Doctork.Application.Abstraction.Repositories;
using Doctork.Application.Dtos.ClinicDtos;
using Doctork.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Persistence.Repositories;
public class ClinicRepository : IClinicRepository
{
    private readonly IDbConnection _connection;
    public ClinicRepository(IDbConnection connection)
    {
        _connection = connection;
    }
    public async Task<IEnumerable<GetClinicDto>> GetAllClinicsAsync()
    {
        return await _connection.QueryAsync<GetClinicDto>("SELECT Id, Name FROM Clinics");
    }
    public async Task<int> AddClinicAsync(Clinic clinic) 
    {
        return await _connection.QuerySingleOrDefaultAsync<int>("AddClinic", clinic, commandType: CommandType.StoredProcedure);
    }
    public async Task<bool> ClinicExistsAsync(int clinicId)
    {
        return await _connection.QueryFirstOrDefaultAsync<int>
            ("SELECT COUNT(*) FROM Clinics WHERE Id = @Id", new { Id = clinicId }) > 0;
    }

    public async Task AddInsuranceToClinicAsync(IEnumerable<ClinicInsurance> clinicInsurances)
    {
        await _connection.ExecuteAsync("AddClinicInsurance", clinicInsurances, commandType: CommandType.StoredProcedure);
    }
}
