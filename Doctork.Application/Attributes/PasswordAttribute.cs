using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Application.Attributes;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class PasswordAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        var password = value as string;
        var errorMessage = string.Empty;

        if (string.IsNullOrEmpty(password))
            return true;

        if (!Regex.IsMatch(password, @"[A-Z]")) 
            errorMessage += "password must contain at least one uppercase letter. ";

        if (!Regex.IsMatch(password, @"[a-z]"))
            errorMessage += "password must contain at least one lowercase letter. ";

        if (!Regex.IsMatch(password, @"[!@#$%^&*()_+=\[{\]};:<>|./?,-]"))
            errorMessage += "password must contain at least one special character. ";

        if (password.Length < 6)
            errorMessage += "password must be at least 6 characters long.";

        if (!string.IsNullOrEmpty(errorMessage))
        {
            ErrorMessage = errorMessage;
            return false;
        }

        return true;
    }
}

