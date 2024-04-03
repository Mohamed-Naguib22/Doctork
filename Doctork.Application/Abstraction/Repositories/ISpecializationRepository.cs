using Doctork.Application.Dtos.SpecializationDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Application.Abstraction.Repositories;
public interface ISpecializationRepository
{
    Task<int> GetSpecializationIdAsync(string specialization);
    Task<IEnumerable<GetSpecializationDto>> GetAllSpecializationsAsync();
}
