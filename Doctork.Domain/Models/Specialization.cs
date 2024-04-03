using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Domain.Models;
public class DoctorSpecialization
{
    public bool IsMain { get; set; }
    public string DoctorId { get; set; }
    public int SpecializationId { get; set; }
}
