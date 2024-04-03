using Doctork.Application.Abstraction.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Application.Abstraction;
public interface IUnitOfWork : IDisposable
{
    IDoctorRepository Doctors { get; }
    IClinicRepository Clinics { get; }
    ISpecializationRepository Specializations { get; }
    IUserRepository Users { get; }
}
