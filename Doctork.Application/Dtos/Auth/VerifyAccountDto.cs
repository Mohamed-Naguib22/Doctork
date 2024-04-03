using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Application.Dtos.Auth;
public class VerifyAccountDto
{
    [Required, EmailAddress]
    public string Email { get; set; }
    [Required, RegularExpression("^\\d{6}$", ErrorMessage = "Code must be 6-digit number")]
    public string Code { get; set; }
}
