using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Application.Abstraction.Repositories;
public interface IUserRepository
{
    Task DeleteUserAsync(string id);
}
