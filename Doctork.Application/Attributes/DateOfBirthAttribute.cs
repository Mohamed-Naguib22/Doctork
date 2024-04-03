using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Doctork.Application.Attributes;
public class DateOfBirthAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value == null)
            return false;

        var dateOfBirth = (DateTime)value;

        return dateOfBirth < DateTime.Now;
    }
    public override string FormatErrorMessage(string name)
    {
        return "Please enter a valid date of birth";
    }
}
