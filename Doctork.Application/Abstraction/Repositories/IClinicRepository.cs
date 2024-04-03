using Doctork.Application.Dtos.ClinicDtos;
using Doctork.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Application.Abstraction.Repositories;
public interface IClinicRepository
{
    Task<IEnumerable<GetClinicDto>> GetAllClinicsAsync();
    Task<int> AddClinicAsync(Clinic clinic);
    Task AddInsuranceToClinicAsync(IEnumerable<ClinicInsurance> clinicInsurance);
    Task<bool> ClinicExistsAsync(int clinicId);
}
