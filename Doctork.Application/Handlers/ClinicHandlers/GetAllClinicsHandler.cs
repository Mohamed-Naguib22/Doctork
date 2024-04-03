using AutoMapper;
using Doctork.Application.Abstraction;
using Doctork.Application.Commands.AuthCommands;
using Doctork.Application.Dtos;
using Doctork.Application.Dtos.ClinicDtos;
using Doctork.Application.Queries.ClinicQueries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Application.Handlers.ClinicHandlers;
public class GetAllClinicsHandler : IRequestHandler<GetAllClinicsQuery, IEnumerable<GetClinicDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetAllClinicsHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<IEnumerable<GetClinicDto>> Handle(GetAllClinicsQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Clinics.GetAllClinicsAsync();
    }
}
