using Doctork.Application.Dtos;
using Doctork.Application.Dtos.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Application.Commands.AuthCommands;
public class RegisterCommand : IRequest<AuthDto>
{
    public RegisterDto RegisterDto { get; }
    public RegisterCommand(RegisterDto registerDto)
    {
        RegisterDto = registerDto;
    }
}
