namespace DeltaApi.Domain.ValueObjects;

public sealed record InjuryInfo
{
    public string? AllegedInjury { get; }
    public string? InjuryDescription { get; }
    public bool AttorneyRepresentation { get; }
    public string? Liability { get; }
    public bool WorkersCompensation { get; }
    public decimal? Exposure { get; }

    public InjuryInfo(
        string? allegedInjury = null,
        string? injuryDescription = null,
        bool attorneyRepresentation = false,
        string? liability = null,
        bool workersCompensation = false,
        decimal? exposure = null)
    {
        AllegedInjury = allegedInjury;
        InjuryDescription = injuryDescription;
        AttorneyRepresentation = attorneyRepresentation;
        Liability = liability;
        WorkersCompensation = workersCompensation;
        Exposure = exposure;
    }
}
