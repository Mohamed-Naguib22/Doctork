using AutoMapper;
using Doctork.Application.Abstraction;
using Doctork.Application.Commands.AuthCommands;
using Doctork.Application.Dtos;
using Doctork.Application.Dtos.DoctorDtos;
using Doctork.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Application.Handlers.AuthHandlers;
public class RegisterDoctorHandler : IRequestHandler<RegisterDoctorCommand, AuthDto>
{
    private readonly IAuthentication _authentication;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IImageService _imageService;
    private readonly IEmailSender _emailSender;
    public RegisterDoctorHandler(IAuthentication authentication,
        IPasswordHasher passwordHasher, IUnitOfWork unitOfWork, IMapper mapper, 
        IImageService imageService, IEmailSender emailSender)
    {
        _authentication = authentication;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _imageService = imageService;
        _emailSender = emailSender;
    }
    public async Task<AuthDto> Handle(RegisterDoctorCommand request, CancellationToken cancellationToken)
    {
        var registerDto = request.RegisterDto;

        var role = await _authentication.GetRoleByNameAsync("Doctor");

        if (role == null)
            return new AuthDto { Succeeded = false, Message = "Invalid Role" };

        if (await _authentication.GetUserByEmailAsync(request.RegisterDto.Email) != null)
            return new AuthDto { Succeeded = false, Message = "This email is already registered" };

        var user = _mapper.Map<User>(registerDto);

        user.Id = Guid.NewGuid().ToString();
        user.PasswordHash = _passwordHasher.Hash(registerDto.Password);
        user.RoleId = role.Id;

        await _authentication.InsertUserAsync(user);

        if (registerDto.Clinic != null && registerDto.ClinicId == null)
        {
            var doctor = _mapper.Map<RegisterDoctorWithClinic>(registerDto);

            doctor.DoctorId = user.Id;
            doctor.InsuranceCompanyIds = string.Join(",", registerDto.InsuranceIds);
            //doctor.PracticeLicenceUrl = _imageService.SetImage(registerDto.PracticeLicence);
            doctor.PracticeLicenceUrl = "test";

            await _unitOfWork.Doctors.RegisterDoctorWithClinicAsync(doctor, registerDto.Schedule);
        }
        else if (registerDto.ClinicId != null && registerDto.Clinic == null)
        {
            var doctor = _mapper.Map<Doctor>(registerDto);
            doctor.DoctorId = user.Id;
            //doctor.PracticeLicenceUrl = _imageService.SetImage(registerDto.PracticeLicence);
            doctor.PracticeLicenceUrl = "test";

            await _unitOfWork.Doctors.RegisterDoctorAsync(doctor, (int)registerDto.ClinicId, registerDto.Schedule);

            var clinic = new DoctorClinic
            {
                ClinicId = (int)registerDto.ClinicId,
                DoctorId = user.Id,
                Fees = registerDto.Fees
            };
            await _unitOfWork.Doctors.AddClinicToDoctorAsync(clinic);
        }
        else
        {
            await _unitOfWork.Users.DeleteUserAsync(user.Id);
            return new AuthDto { Succeeded = false, Message = "Please enter the ClinicId or Clinic correctly" };
        }

        await _emailSender.SendEmailAsync(user.Email, "Welcome to Doctork! Verification Needed",
            $"Dear Dr. {user.FirstName} ,Thanks for joining Doctork! To ensure patient safety, we verify all doctors. " +
            "We'll check your background & license and may request additional info. We'll notify you once your account is active.");

        return new AuthDto { Succeeded = true, Message = "User registered successfully" };
    }
}
