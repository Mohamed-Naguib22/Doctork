using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Application.Commands.DoctorCommands;
public class ConfirmDoctorCommand : IRequest<bool>
{
    public string Email { get; }
    public ConfirmDoctorCommand(string email)
    {
        Email = email;
    }
}
