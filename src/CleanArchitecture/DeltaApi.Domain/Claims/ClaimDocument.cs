using DeltaApi.Domain.Abstractions;

namespace DeltaApi.Domain.Claims;

public sealed class ClaimDocument : Entity
{
    public ClaimDocument(
        Guid id,
        Guid claimId,
        string documentType,
        string filePath,
        int createdByUser = 1) : base(id, createdByUser)
    {
        ClaimId = claimId;
        DocumentType = documentType ?? throw new ArgumentNullException(nameof(documentType));
        FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
    }

    public Guid ClaimId { get; private set; }
    public string DocumentType { get; private set; }
    public string FilePath { get; private set; }

    public void UpdateDocument(string documentType, string filePath, int modifiedByUser)
    {
        DocumentType = documentType ?? throw new ArgumentNullException(nameof(documentType));
        FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
        MarkAsModified(modifiedByUser);
    }
}
