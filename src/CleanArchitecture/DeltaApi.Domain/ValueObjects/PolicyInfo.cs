namespace DeltaApi.Domain.ValueObjects;

public sealed record PolicyInfo
{
    public string PolicyNumber { get; }
    public string? DeltaFileNumber { get; }
    public string? ClientFileNumber { get; }

    public PolicyInfo(string policyNumber, string? deltaFileNumber = null, string? clientFileNumber = null)
    {
        PolicyNumber = policyNumber ?? throw new ArgumentNullException(nameof(policyNumber));
        DeltaFileNumber = deltaFileNumber;
        ClientFileNumber = clientFileNumber;
    }
}
