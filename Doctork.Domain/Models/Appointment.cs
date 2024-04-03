using Doctork.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Domain.Models;
public class Appointment
{
    public int Id { get; set; }
    public string PatientId { get; set; }
    public string DoctorId { get; set; }
    public int ClinicId { get; set; }
    public DateTime Time { get; set; }
}
