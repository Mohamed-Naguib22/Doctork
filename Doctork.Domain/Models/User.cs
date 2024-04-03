using Doctork.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Domain.Models;
public class User
{
    public string Id { get; set; }
    [EmailAddress, MaxLength(128)]
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    [MaxLength(128)]
    public string FirstName { get; set; }
    [MaxLength(128)]
    public string LastName { get; set; }
    [MaxLength(128)]
    public string PhoneNumber { get; set; }
    public Gender Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public bool IsEmailConfirmed { get; set; }
    public string RoleId { get; set; }
}
