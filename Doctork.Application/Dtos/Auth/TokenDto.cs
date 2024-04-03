using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Application.Dtos.Auth;
public class TokenDto
{
    public string Token { get; set; }
    public DateTime ExpiresOn { get; set; }
}
