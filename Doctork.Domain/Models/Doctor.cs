using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Domain.Models;
public class Doctor
{
    public string DoctorId { get; set; }
    [MaxLength(128)]
    public string JobTitle { get; set; }
    [MaxLength(128)]
    public string Certificate { get; set; }
    public bool IsVerified { get; set; }
    public string? Biography { get; set; }
    public string PracticeLicenceUrl { get; set; }
    [Range(0, double.MaxValue)]
    public decimal Fees { get; set; }
    [MaxLength(128)]
    public string MainSpecialization { get; set; }
    [MaxLength(128)]
    public string SecondSpecialization { get; set; }
}
