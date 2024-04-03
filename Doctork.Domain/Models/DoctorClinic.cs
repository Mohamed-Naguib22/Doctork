using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Domain.Models;
public class DoctorClinic
{
    public decimal Fees { get; set; }
    public string DoctorId { get; set; }
    public int ClinicId { get; set; }
}
