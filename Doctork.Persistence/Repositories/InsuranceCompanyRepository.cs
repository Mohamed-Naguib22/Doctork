using Dapper;
using Doctork.Application.Abstraction.Repositories;
using Doctork.Application.Dtos.InsuranceCompanyDtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Persistence.Repositories;
public class InsuranceCompanyRepository : IInsuranceCompanyRepository
{
    private readonly IDbConnection _connection;
    public InsuranceCompanyRepository(IDbConnection connection)
    {
        _connection = connection;
    }
    public async Task<IEnumerable<GetInsuranceCompanyDto>> GetAllInsuranceCompaniesAsync() =>
        await _connection.QueryAsync<GetInsuranceCompanyDto>("SELECT Id, Name FROM InsuranceCompanies");
}
