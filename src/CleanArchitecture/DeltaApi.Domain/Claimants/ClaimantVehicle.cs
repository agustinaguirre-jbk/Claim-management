using DeltaApi.Domain.Abstractions;

namespace DeltaApi.Domain.Claimants;

public sealed class ClaimantVehicle : Entity
{
    public ClaimantVehicle(
        Guid id,
        Guid claimantId,
        string? year = null,
        string? make = null,
        string? model = null,
        string? color = null,
        string? tagNumber = null,
        string? registrationState = null,
        string? vin = null,
        string? information = null,
        string? internalNotes = null,
        bool includeInReport = true,
        int? caseId = null) : base(id)
    {
        ClaimantId = claimantId;
        Year = year;
        Make = make;
        Model = model;
        Color = color;
        TagNumber = tagNumber;
        RegistrationState = registrationState;
        VIN = vin;
        Information = information;
        InternalNotes = internalNotes;
        IncludeInReport = includeInReport;
        CaseId = caseId;
    }

    public Guid ClaimantId { get; private set; }
    public int? CaseId { get; private set; }
    public string? Year { get; private set; }
    public string? Make { get; private set; }
    public string? Model { get; private set; }
    public string? Color { get; private set; }
    public string? TagNumber { get; private set; }
    public string? RegistrationState { get; private set; }
    public string? VIN { get; private set; }
    public string? Information { get; private set; }
    public string? InternalNotes { get; private set; }
    public bool IncludeInReport { get; private set; }

    public void UpdateVehicleInfo(string? year, string? make, string? model, string? color)
    {
        Year = year;
        Make = make;
        Model = model;
        Color = color;
    }

    public void UpdateRegistration(string? tagNumber, string? registrationState, string? vin)
    {
        TagNumber = tagNumber;
        RegistrationState = registrationState;
        VIN = vin;
    }
}
