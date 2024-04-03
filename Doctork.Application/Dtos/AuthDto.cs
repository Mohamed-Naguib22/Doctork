using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Doctork.Application.Dtos;
public class AuthDto
{
    public bool IsAuthenticated { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Role { get; set; }
    public string Token { get; set; }
    public DateTime ExpiresOn { get; set; }
    [JsonIgnore]
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiration { get; set; }
    [JsonIgnore]
    public bool Succeeded { get; set; }
    [JsonIgnore]
    public string? Message { get; set; }
}
