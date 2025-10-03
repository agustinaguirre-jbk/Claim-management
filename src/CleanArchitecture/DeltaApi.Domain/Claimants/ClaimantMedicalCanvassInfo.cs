using DeltaApi.Domain.Abstractions;

namespace DeltaApi.Domain.Claimants;

public sealed class ClaimantMedicalCanvassInfo : Entity
{
    public ClaimantMedicalCanvassInfo(
        Guid id,
        Guid claimantId,
        int? caseId = null,
        bool futureAppointment = false,
        bool excessiveTreatment = false,
        bool preDateOfLoss = false) : base(id)
    {
        ClaimantId = claimantId;
        CaseId = caseId;
        FutureAppointment = futureAppointment;
        ExcessiveTreatment = excessiveTreatment;
        PreDateOfLoss = preDateOfLoss;
    }

    public Guid ClaimantId { get; private set; }
    public int? CaseId { get; private set; }
    public bool FutureAppointment { get; private set; }
    public bool ExcessiveTreatment { get; private set; }
    public bool PreDateOfLoss { get; private set; }

    public void UpdateMedicalCanvassInfo(
        bool futureAppointment,
        bool excessiveTreatment,
        bool preDateOfLoss)
    {
        FutureAppointment = futureAppointment;
        ExcessiveTreatment = excessiveTreatment;
        PreDateOfLoss = preDateOfLoss;
    }
}
