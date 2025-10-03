using Dapper;
using DeltaApi.Domain.Claims;
using DeltaApi.Domain.Repositories;
using DeltaApi.Infrastructure.Data;
using DeltaApi.Infrastructure.Mappings;

namespace DeltaApi.Infrastructure.Repositories;

public class DoctorRepository : IDoctorRepository
{
    private readonly IConnectionFactory _connectionFactory;

    public DoctorRepository(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
    }

    public async Task<Doctor?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT 
                doctor_id, doctor_name, doctor_specialty, doctor_phone, doctor_address,
                deleted, created_by_user, created_on, modified_by_user, modified_on
            FROM claims.doctor 
            WHERE doctor_id = @Id AND deleted = false";

        using var connection = _connectionFactory.CreateConnection();
        var result = await connection.QueryFirstOrDefaultAsync<dynamic>(sql, new { Id = id });
        
        return result != null ? DoctorMapper.MapToDoctor(result) : null;
    }

    public async Task<IEnumerable<Doctor>> SearchByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT 
                doctor_id, doctor_name, doctor_specialty, doctor_phone, doctor_address,
                deleted, created_by_user, created_on, modified_by_user, modified_on
            FROM claims.doctor 
            WHERE doctor_name LIKE @Name AND deleted = false";

        using var connection = _connectionFactory.CreateConnection();
        var results = await connection.QueryAsync<dynamic>(sql, new { Name = $"%{name}%" });
        
        return results.Select(DoctorMapper.MapToDoctor);
    }

    public async Task<IEnumerable<Doctor>> SearchBySpecialtyAsync(string specialty, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT 
                doctor_id, doctor_name, doctor_specialty, doctor_phone, doctor_address,
                deleted, created_by_user, created_on, modified_by_user, modified_on
            FROM claims.doctor 
            WHERE doctor_specialty LIKE @Specialty AND deleted = false";

        using var connection = _connectionFactory.CreateConnection();
        var results = await connection.QueryAsync<dynamic>(sql, new { Specialty = $"%{specialty}%" });
        
        return results.Select(DoctorMapper.MapToDoctor);
    }

    public async Task<Doctor> AddAsync(Doctor doctor, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            INSERT INTO claims.doctor (
                doctor_id, doctor_name, doctor_specialty, doctor_phone, doctor_address,
                deleted, created_by_user, created_on, modified_by_user, modified_on
            ) VALUES (
                @DoctorId, @DoctorName, @DoctorSpecialty, @DoctorPhone, @DoctorAddress,
                @Deleted, @CreatedByUser, @CreatedOn, @ModifiedByUser, @ModifiedOn
            )";

        var parameters = new
        {
            DoctorId = doctor.Id,
            DoctorName = doctor.DoctorInfo.DoctorName,
            DoctorSpecialty = doctor.DoctorInfo.DoctorSpecialty,
            DoctorPhone = doctor.DoctorInfo.DoctorPhone,
            DoctorAddress = doctor.DoctorInfo.DoctorAddress,
            Deleted = doctor.Deleted,
            CreatedByUser = doctor.CreatedByUser,
            CreatedOn = doctor.CreatedOn,
            ModifiedByUser = doctor.ModifiedByUser,
            ModifiedOn = doctor.ModifiedOn
        };

        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(sql, parameters);
        
        return doctor;
    }

    public async Task UpdateAsync(Doctor doctor, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            UPDATE claims.doctor SET
                doctor_name = @DoctorName,
                doctor_specialty = @DoctorSpecialty,
                doctor_phone = @DoctorPhone,
                doctor_address = @DoctorAddress,
                modified_by_user = @ModifiedByUser,
                modified_on = @ModifiedOn
            WHERE doctor_id = @DoctorId";

        var parameters = new
        {
            DoctorId = doctor.Id,
            DoctorName = doctor.DoctorInfo.DoctorName,
            DoctorSpecialty = doctor.DoctorInfo.DoctorSpecialty,
            DoctorPhone = doctor.DoctorInfo.DoctorPhone,
            DoctorAddress = doctor.DoctorInfo.DoctorAddress,
            ModifiedByUser = doctor.ModifiedByUser,
            ModifiedOn = doctor.ModifiedOn
        };

        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(sql, parameters);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        const string sql = "UPDATE claims.doctor SET deleted = true WHERE doctor_id = @Id";

        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(sql, new { Id = id });
    }
}
