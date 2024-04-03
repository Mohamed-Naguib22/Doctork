using Doctork.Application.Abstraction;
using Doctork.Application.Commands.DoctorCommands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Application.Handlers.DoctorHandlers;
public class ConfirmDoctorHandler : IRequestHandler<ConfirmDoctorCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    public ConfirmDoctorHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<bool> Handle(ConfirmDoctorCommand request, CancellationToken cancellationToken)
    {
        if (!await _unitOfWork.Doctors.DoctorExistsAsync(request.Email))
            return false;

        await _unitOfWork.Doctors.ConfirmDoctorEmailAsync(request.Email);

        return true;
    }
}
