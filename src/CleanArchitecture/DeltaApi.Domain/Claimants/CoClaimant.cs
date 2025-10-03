using DeltaApi.Domain.Abstractions;
using DeltaApi.Domain.ValueObjects;

namespace DeltaApi.Domain.Claimants;

public sealed class CoClaimant : Entity
{
    public CoClaimant(
        Guid id,
        int caseId,
        string firstName,
        string lastName,
        Guid? claimantId = null,
        string? ssn = null,
        DateTime? dateOfBirth = null,
        DateTime? dateOfLoss = null,
        string? injury = null,
        string? imageUrl = null,
        Race? race = null,
        int? weight = null,
        int? heightInInches = null,
        Gender? gender = null,
        string? address = null,
        string? phoneNumber = null,
        int? type = null) : base(id)
    {
        CaseId = caseId;
        FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
        LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        ClaimantId = claimantId;
        SSN = ssn;
        DateOfBirth = dateOfBirth;
        DateOfLoss = dateOfLoss;
        Injury = injury;
        ImageUrl = imageUrl;
        Race = race;
        Weight = weight;
        HeightInInches = heightInInches;
        Gender = gender;
        Address = address;
        PhoneNumber = phoneNumber;
        Type = type;
    }

    public int CaseId { get; private set; }
    public Guid? ClaimantId { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string? SSN { get; private set; }
    public DateTime? DateOfBirth { get; private set; }
    public DateTime? DateOfLoss { get; private set; }
    public string? Injury { get; private set; }
    public string? ImageUrl { get; private set; }
    public Race? Race { get; private set; }
    public int? Weight { get; private set; }
    public int? HeightInInches { get; private set; }
    public Gender? Gender { get; private set; }
    public string? Address { get; private set; }
    public string? PhoneNumber { get; private set; }
    public int? Type { get; private set; }

    public string FullName => $"{FirstName} {LastName}";

    public void UpdatePersonalInfo(string firstName, string lastName, string? ssn = null, DateTime? dateOfBirth = null)
    {
        FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
        LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        SSN = ssn;
        DateOfBirth = dateOfBirth;
    }
}
