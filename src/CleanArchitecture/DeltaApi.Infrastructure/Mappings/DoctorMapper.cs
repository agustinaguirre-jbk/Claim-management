using DeltaApi.Domain.Claims;
using DeltaApi.Domain.ValueObjects;

namespace DeltaApi.Infrastructure.Mappings;

public static class DoctorMapper
{
    public static Doctor MapToDoctor(dynamic row)
    {
        var doctorInfo = new DoctorInfo(
            doctorName: row.DoctorName,
            doctorSpecialty: row.DoctorSpecialty,
            doctorPhone: row.DoctorPhone,
            doctorAddress: row.DoctorAddress
        );

        return new Doctor(
            id: row.DoctorId,
            doctorInfo: doctorInfo,
            createdByUser: row.CreatedByUser
        );
    }
}
