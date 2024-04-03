using AutoMapper;
using Doctork.Application.Abstraction;
using Doctork.Application.Commands.AuthCommands;
using Doctork.Application.Commands.UserCommands;
using Doctork.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Application.Handlers.UserHandlers;
public class DeleteAccountHandler : IRequestHandler<DeleteAccountCommand, bool>
{
    private readonly IAuthentication _authentication;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteAccountHandler(IAuthentication authentication, IUnitOfWork unitOfWork)
    {
        _authentication = authentication;
        _unitOfWork = unitOfWork;
    }
    public async Task<bool> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        var user = await _authentication.GetUserFromRefreshTokenAsync(request.RefreshToken);

        if (user.RoleId == (await _authentication.GetRoleByNameAsync("Doctor")).Id)
            await _unitOfWork.Doctors.DeleteDoctorAsync(user.Id);

        if (user == null)
            return false;

        await _unitOfWork.Users.DeleteUserAsync(user.Id);
        
        return true;
    }
}
