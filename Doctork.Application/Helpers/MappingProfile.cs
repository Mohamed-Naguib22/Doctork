using AutoMapper;
using Doctork.Application.Dtos.Auth;
using Doctork.Application.Dtos.DoctorDtos;
using Doctork.Domain.Enums;
using Doctork.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Application.Helpers;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterDoctorDto, RegisterDoctorWithClinic>()
            .ForMember(dest => dest.ClinicName, opt => opt.MapFrom(src => src.Clinic.Name))
            .ForMember(dest => dest.ClinicStreet, opt => opt.MapFrom(src => src.Clinic.Street))
            .ForMember(dest => dest.ClinicArea, opt => opt.MapFrom(src => src.Clinic.Area))
            .ForMember(dest => dest.ClinicCity, opt => opt.MapFrom(src => src.Clinic.City))
            .ForMember(dest => dest.ClinicContactNumber, opt => opt.MapFrom(src => src.Clinic.ContactNumber));
        
        CreateMap<RegisterDoctorDto, Doctor>();

        CreateMap<RegisterDto, User>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => Enum.Parse<Gender>(src.Gender.ToLower())));
    }
}
