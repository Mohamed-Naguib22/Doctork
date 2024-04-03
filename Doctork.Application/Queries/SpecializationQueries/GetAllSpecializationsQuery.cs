using Doctork.Application.Dtos.ClinicDtos;
using Doctork.Application.Dtos.SpecializationDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Application.Queries.SpecializationQueries;
public class GetAllSpecializationsQuery : IRequest<IEnumerable<GetSpecializationDto>>
{
    public GetAllSpecializationsQuery()
    {

    }
}

