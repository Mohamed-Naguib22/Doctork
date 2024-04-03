using Dapper;
using Doctork.Application.Abstraction.Repositories;
using Doctork.Application.Dtos.DoctorDtos;
using Doctork.Domain.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Doctork.Persistence.Repositories;
public class DoctorRepository : IDoctorRepository
{
    private readonly IDbConnection _connection;
    public DoctorRepository(IDbConnection connection)
    {
        _connection = connection;
    }
    public async Task AddClinicToDoctorAsync(DoctorClinic doctorClinic)
    {
        await _connection.ExecuteAsync("AddDoctorClinic", doctorClinic, commandType: CommandType.StoredProcedure);
    }
    public async Task RegisterDoctorWithClinicAsync(RegisterDoctorWithClinic doctor, IEnumerable<TimeSlot> schedule)
    {
        _connection.Open();
        try 
        {
            using var transaction = _connection.BeginTransaction();
            var clinicId = await _connection.QuerySingleOrDefaultAsync<int>("RegisterDoctorWithClinic", doctor, transaction);

            foreach (var timeSlot in schedule)
            {
                timeSlot.DoctorId = doctor.DoctorId;
                timeSlot.ClinicId = clinicId;
                await _connection.ExecuteAsync("AddTimeSlot", timeSlot, transaction);
            }

            transaction.Commit();
        }
        finally
        {
            _connection.Close();
        }
    }
    public async Task RegisterDoctorAsync(Doctor doctor, int clinicId, IEnumerable<TimeSlot> schedule)
    {
        _connection.Open();
        try
        {
            using var transaction = _connection.BeginTransaction();
            await _connection.ExecuteAsync("RegisterDoctor", doctor, transaction);

            foreach (var timeSlot in schedule)
            {
                timeSlot.DoctorId = doctor.DoctorId;
                timeSlot.ClinicId = clinicId;
                await _connection.ExecuteAsync("AddTimeSlot", timeSlot, transaction);
            }

            transaction.Commit();
        }
        finally 
        { 
            _connection.Close(); 
        }
    }
    public async Task<bool> IsVerifiedAsync(string doctorId) =>
        await _connection.QuerySingleOrDefaultAsync<bool>("SELECT IsVerified FROM Doctors WHERE Id = @Id", new { Id = doctorId });

    public async Task<bool> DoctorExistsAsync(string email) =>
        await _connection.QuerySingleOrDefaultAsync<int>("SELECT COUNT(*) FROM Doctors JOIN Users ON Doctors.Id = Users.Id WHERE Email = @Email", new { Email = email }) > 0;

    public async Task ConfirmDoctorEmailAsync(string email)
    {
        _connection.Open();
        try
        {
            using var transaction = _connection.BeginTransaction();

            await _connection.ExecuteAsync("ConfirmDoctor", new { Email = email }, transaction);

            transaction.Commit();
        }
        finally
        {
            _connection.Close();
        }
    }
}
