using Doctork.Application.Attributes;
using Doctork.Application.Dtos.ClinicDtos;
using Doctork.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Application.Dtos.Auth;
public class RegisterDoctorDto : RegisterDto
{
    [Required, StringLength(128)]
    public string MainSpecialization { get; set; }
    [Required, StringLength(128)]
    public string SecondSpecialization { get; set; }
    [Required, StringLength(128)]
    public string Certificate { get; set; }
    [Required, StringLength(128)]
    public string JobTitle { get; set; }
    [Required, Range(0, double.MaxValue, ErrorMessage = "Please enter a valid fees")]
    public decimal Fees { get; set; }
    public IFormFile? PracticeLicence { get; set; }
    public AddClinicDto? Clinic { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid Id")]
    public int? ClinicId { get; set; }
    public IEnumerable<int>? InsuranceIds { get; set; }
    [Required]
    public IEnumerable<TimeSlot> Schedule { get; set; }
}
