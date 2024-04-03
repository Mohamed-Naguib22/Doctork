using Doctork.Application.Dtos.DoctorDtos;
using Doctork.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Application.Abstraction.Repositories;
public interface IDoctorRepository
{
    Task AddClinicToDoctorAsync(DoctorClinic doctorClinic);
    Task RegisterDoctorAsync(Doctor doctor, int clinic, IEnumerable<TimeSlot> schedule);
    Task RegisterDoctorWithClinicAsync(RegisterDoctorWithClinic doctor, IEnumerable<TimeSlot> Schedule);
    Task<bool> IsVerifiedAsync(string doctorId);
    Task<bool> DoctorExistsAsync(string email);
    Task ConfirmDoctorEmailAsync(string email);
    Task DeleteDoctorAsync(string id);
}
