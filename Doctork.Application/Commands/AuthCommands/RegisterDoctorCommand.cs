using Doctork.Application.Dtos;
using Doctork.Application.Dtos.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Application.Commands.AuthCommands;
public class RegisterDoctorCommand : IRequest<AuthDto>
{
    public RegisterDoctorDto RegisterDto { get; }
    public RegisterDoctorCommand(RegisterDoctorDto registerDto)
    {
        RegisterDto = registerDto;
    }
}
