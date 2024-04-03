using Doctork.Application.Dtos;
using Doctork.Application.Dtos.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Application.Queries.AuthQueries;
public class VerifyAccountQuery : IRequest<AuthDto>
{
    public VerifyAccountDto VerifyAccountDto { get; }
    public VerifyAccountQuery(VerifyAccountDto verifyAccountDto)
    {
        VerifyAccountDto = verifyAccountDto;
    }
}
