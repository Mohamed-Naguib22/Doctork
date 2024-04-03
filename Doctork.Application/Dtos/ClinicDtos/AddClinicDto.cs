using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Application.Dtos.ClinicDtos;
public class AddClinicDto
{
    [StringLength(128)]
    public string Name { get; set; }
    [StringLength(128)]
    public string City { get; set; }
    [StringLength(128)]
    public string Area { get; set; }
    [StringLength(128)]
    public string Street { get; set; }
    [StringLength(8, MinimumLength = 8, ErrorMessage = "The contact number must be 8 digits")]
    public string ContactNumber { get; set; }
}
