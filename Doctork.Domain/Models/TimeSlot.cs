using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Doctork.Domain.Models;
public class TimeSlot
{
    [RegularExpression("^(?i)(saturday||sunday||monday||tuesday||wednesday||thursday||friday)$", ErrorMessage = "Please enter valid day")]
    public string Day { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    [ValidateNever, JsonIgnore]
    public string DoctorId { get; set; }
    [ValidateNever, JsonIgnore]
    public int ClinicId { get; set; }
}
