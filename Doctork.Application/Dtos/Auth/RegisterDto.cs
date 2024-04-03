using Doctork.Application.Attributes;
using Doctork.Application.Dtos.DoctorDtos;
using Doctork.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Application.Dtos.Auth;
public class RegisterDto
{
    [EmailAddress, StringLength(128)]
    public string Email { get; set; }
    [Password]
    public string Password { get; set; }
    [StringLength(128)]
    public string FirstName { get; set; }
    [StringLength(128)]
    public string LastName { get; set; }
    [RegularExpression("^(010|011|012|015)\\d{8}$", ErrorMessage = "Please enter a valid phone number")]
    public string PhoneNumber { get; set; }
    [RegularExpression("^(?i)(male|female)$", ErrorMessage = "Please enter valid gender")]
    public string Gender { get; set; }
    [DateOfBirth]
    public DateTime DateOfBirth { get; set; }
}
