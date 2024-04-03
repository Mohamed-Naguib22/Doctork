using Doctork.Application.Abstraction;
using Doctork.Application.Dtos.SpecializationDtos;
using Doctork.Application.Queries.SpecializationQueries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Application.Handlers.SpecializationHandlers;
public class GetAllSpecializationHandler : IRequestHandler<GetAllSpecializationsQuery, IEnumerable<GetSpecializationDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetAllSpecializationHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<IEnumerable<GetSpecializationDto>> Handle(GetAllSpecializationsQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Specializations.GetAllSpecializationsAsync();
    }
}
