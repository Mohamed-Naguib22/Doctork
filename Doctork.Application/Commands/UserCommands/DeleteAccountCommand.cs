using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Application.Commands.UserCommands;
public class DeleteAccountCommand : IRequest<bool>
{
    public string RefreshToken { get; }
    public DeleteAccountCommand(string refreshToken)
    {
        RefreshToken = refreshToken;
    }
}
