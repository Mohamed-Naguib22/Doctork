using Doctork.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Application.Dtos.DoctorDtos;
public class RegisterDoctorWithClinic : Doctor
{
    [MaxLength(128)]
    public string ClinicName { get; set; }
    [MaxLength(128)]
    public string ClinicCity { get; set; }
    [MaxLength(128)]
    public string ClinicArea { get; set; }
    [MaxLength(128)]
    public string ClinicStreet { get; set; }
    [MaxLength(128)]
    public string ClinicContactNumber { get; set; }
    public string InsuranceCompanyIds { get; set; }
}
