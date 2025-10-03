using DeltaApi.Domain.Abstractions;

namespace DeltaApi.Domain.Claimants;

public sealed class ClaimantEmployment : Entity
{
    public ClaimantEmployment(Guid id, Guid claimantId, string employmentDetail) : base(id)
    {
        ClaimantId = claimantId;
        EmploymentDetail = employmentDetail ?? throw new ArgumentNullException(nameof(employmentDetail));
    }

    public Guid ClaimantId { get; private set; }
    public string EmploymentDetail { get; private set; }

    public void UpdateEmploymentDetail(string employmentDetail)
    {
        EmploymentDetail = employmentDetail ?? throw new ArgumentNullException(nameof(employmentDetail));
    }
}
