using DeltaApi.Domain.Abstractions;
using DeltaApi.Domain.ValueObjects;

namespace DeltaApi.Domain.Claims;

public sealed class Claim : Entity
{
    private readonly List<ClaimDocument> _documents = new();
    private readonly List<ClaimEvent> _events = new();

    public Claim(
        Guid id,
        int caseId,
        int claimTypeId,
        int claimantId,
        int clientId,
        PolicyInfo policyInfo,
        int? doctorId = null,
        int? stateOfLossId = null,
        InjuryInfo? injuryInfo = null,
        int createdByUser = 1) : base(id, createdByUser)
    {
        CaseId = caseId;
        ClaimTypeId = claimTypeId;
        ClaimantId = claimantId;
        ClientId = clientId;
        PolicyInfo = policyInfo ?? throw new ArgumentNullException(nameof(policyInfo));
        DoctorId = doctorId;
        StateOfLossId = stateOfLossId;
        InjuryInfo = injuryInfo;
    }

    public int CaseId { get; private set; }
    public int ClaimTypeId { get; private set; }
    public int ClaimantId { get; private set; }
    public int ClientId { get; private set; }
    public PolicyInfo PolicyInfo { get; private set; }
    public int? DoctorId { get; private set; }
    public int? StateOfLossId { get; private set; }
    public InjuryInfo? InjuryInfo { get; private set; }

    // Collections
    public IReadOnlyCollection<ClaimDocument> Documents => _documents.AsReadOnly();
    public IReadOnlyCollection<ClaimEvent> Events => _events.AsReadOnly();

    // Business methods
    public void AddDocument(ClaimDocument document)
    {
        if (document == null)
            throw new ArgumentNullException(nameof(document));

        _documents.Add(document);
    }

    public void AddEvent(ClaimEvent claimEvent)
    {
        if (claimEvent == null)
            throw new ArgumentNullException(nameof(claimEvent));

        _events.Add(claimEvent);
    }

    public void UpdatePolicyInfo(PolicyInfo policyInfo, int modifiedByUser)
    {
        PolicyInfo = policyInfo ?? throw new ArgumentNullException(nameof(policyInfo));
        MarkAsModified(modifiedByUser);
    }

    public void UpdateInjuryInfo(InjuryInfo injuryInfo, int modifiedByUser)
    {
        InjuryInfo = injuryInfo ?? throw new ArgumentNullException(nameof(injuryInfo));
        MarkAsModified(modifiedByUser);
    }

    public void AssignDoctor(int doctorId, int modifiedByUser)
    {
        DoctorId = doctorId;
        MarkAsModified(modifiedByUser);
    }

    public void AssignStateOfLoss(int stateOfLossId, int modifiedByUser)
    {
        StateOfLossId = stateOfLossId;
        MarkAsModified(modifiedByUser);
    }
}
