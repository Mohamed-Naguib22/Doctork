using Doctork.Application.Dtos.InsuranceCompanyDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Application.Queries.InsuranceCompanyQueries;
public class GetAllInsuranceCompaniesQuery : IRequest<IEnumerable<GetInsuranceCompanyDto>>
{
    public GetAllInsuranceCompaniesQuery()
    {

    }
}
