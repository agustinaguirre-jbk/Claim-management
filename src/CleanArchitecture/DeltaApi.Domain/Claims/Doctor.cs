using DeltaApi.Domain.Abstractions;
using DeltaApi.Domain.ValueObjects;

namespace DeltaApi.Domain.Claims;

public sealed class Doctor : Entity
{
    public Doctor(
        Guid id,
        DoctorInfo doctorInfo,
        int createdByUser = 1) : base(id, createdByUser)
    {
        DoctorInfo = doctorInfo ?? throw new ArgumentNullException(nameof(doctorInfo));
    }

    public DoctorInfo DoctorInfo { get; private set; }

    public void UpdateDoctorInfo(DoctorInfo doctorInfo, int modifiedByUser)
    {
        DoctorInfo = doctorInfo ?? throw new ArgumentNullException(nameof(doctorInfo));
        MarkAsModified(modifiedByUser);
    }
}
