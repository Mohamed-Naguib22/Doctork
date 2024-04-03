using Doctork.Application.Abstraction;
using Doctork.Application.Commands.DoctorCommands;
using Doctork.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Application.Handlers.DoctorHandlers;
public class ConfirmDoctorHandler : IRequestHandler<ConfirmDoctorCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailSender _emailSender;
    public ConfirmDoctorHandler(IUnitOfWork unitOfWork, IEmailSender emailSender)
    {
        _unitOfWork = unitOfWork;
        _emailSender = emailSender;
    }
    public async Task<bool> Handle(ConfirmDoctorCommand request, CancellationToken cancellationToken)
    {
        if (!await _unitOfWork.Doctors.DoctorExistsAsync(request.Email))
            return false;

        await _unitOfWork.Doctors.ConfirmDoctorEmailAsync(request.Email);

        await _emailSender.SendEmailAsync(request.Email, "Doctor Data Validation Complete for Doctork",
            $"We're happy to inform you that your data validation for Doctork is complete! you can now access all the features of our website.");

        return true;
    }
}
