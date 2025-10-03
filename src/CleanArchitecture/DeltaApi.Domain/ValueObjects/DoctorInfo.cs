namespace DeltaApi.Domain.ValueObjects;

public sealed record DoctorInfo
{
    public string DoctorName { get; }
    public string? DoctorSpecialty { get; }
    public string? DoctorPhone { get; }
    public string? DoctorAddress { get; }

    public DoctorInfo(
        string doctorName,
        string? doctorSpecialty = null,
        string? doctorPhone = null,
        string? doctorAddress = null)
    {
        DoctorName = doctorName ?? throw new ArgumentNullException(nameof(doctorName));
        DoctorSpecialty = doctorSpecialty;
        DoctorPhone = doctorPhone;
        DoctorAddress = doctorAddress;
    }
}
