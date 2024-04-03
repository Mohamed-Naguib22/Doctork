using Doctork.Application.Dtos.ClinicDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Application.Queries.ClinicQueries;
public class GetAllClinicsQuery : IRequest<IEnumerable<GetClinicDto>>
{
    public GetAllClinicsQuery()
    {

    }
}
