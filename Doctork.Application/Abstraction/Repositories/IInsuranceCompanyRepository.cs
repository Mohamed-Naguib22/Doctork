using Doctork.Application.Dtos.InsuranceCompanyDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Application.Abstraction.Repositories;
public interface IInsuranceCompanyRepository
{
    Task<IEnumerable<GetInsuranceCompanyDto>> GetAllInsuranceCompaniesAsync();
}
