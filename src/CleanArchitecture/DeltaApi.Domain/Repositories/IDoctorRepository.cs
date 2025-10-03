using DeltaApi.Domain.Claims;

namespace DeltaApi.Domain.Repositories;

public interface IDoctorRepository
{
    Task<Doctor?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Doctor>> SearchByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<IEnumerable<Doctor>> SearchBySpecialtyAsync(string specialty, CancellationToken cancellationToken = default);
    Task<Doctor> AddAsync(Doctor doctor, CancellationToken cancellationToken = default);
    Task UpdateAsync(Doctor doctor, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
