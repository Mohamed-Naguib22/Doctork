using Doctork.Application.Abstraction;
using Doctork.Application.Abstraction.Repositories;
using Doctork.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Persistence.Services;
public class UnitOfWork : IUnitOfWork
{
    private readonly IDbConnection _connection;
    public IDoctorRepository Doctors { get; private set; }
    public IClinicRepository Clinics { get; private set; }
    public ISpecializationRepository Specializations { get; private set; }
    public IInsuranceCompanyRepository InsuranceCompanies { get; private set; }
    public IUserRepository Users { get; private set; }
    public UnitOfWork(IDbConnection connection)
    {   
        _connection = connection;
        Doctors = new DoctorRepository(_connection);
        Users = new UserRepository(_connection);
        Specializations = new SpecializationRepository(_connection);
        Clinics = new ClinicRepository(_connection);
        InsuranceCompanies = new InsuranceCompanyRepository(_connection);
    }

    public void Dispose()
    {
        _connection.Dispose();
    }
}
