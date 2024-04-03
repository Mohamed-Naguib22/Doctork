using Doctork.Application.Abstraction;
using Doctork.Application.Dtos.InsuranceCompanyDtos;
using Doctork.Application.Queries.InsuranceCompanyQueries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Application.Handlers.InsuranceCompanyHandlers;
public class GetAllInsuranceCompaniesHandler : IRequestHandler<GetAllInsuranceCompaniesQuery, IEnumerable<GetInsuranceCompanyDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetAllInsuranceCompaniesHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<IEnumerable<GetInsuranceCompanyDto>> Handle(GetAllInsuranceCompaniesQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.InsuranceCompanies.GetAllInsuranceCompaniesAsync();
    }
}
