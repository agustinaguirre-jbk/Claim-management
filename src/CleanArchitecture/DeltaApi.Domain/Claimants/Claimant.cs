using DeltaApi.Domain.Abstractions;
using DeltaApi.Domain.ValueObjects;

namespace DeltaApi.Domain.Claimants;

//sealed class means that the class cannot be inherited, (principio de responsabilidad unica)
public sealed class Claimant : Entity
{
    private readonly List<ClaimantEmail> _emails = new();
    private readonly List<ClaimantPhone> _phones = new();
    private readonly List<ClaimantAddress> _addresses = new();
    private readonly List<ClaimantVehicle> _vehicles = new();
    private readonly List<ClaimantSocialMedia> _socialMedia = new();
    private readonly List<ClaimantEmployment> _employments = new();
    private readonly List<ClaimantRelation> _relations = new();

    public Claimant(
        Guid id,
        PersonalInfo personalInfo,
        string? additionalDetail = null,
        string? additionalDetailInternal = null,
        DateTime? lastBackgroundCheck = null,
        bool isInsured = false,
        bool isSubject = false,
        string? aliasDisplay = null,
        Guid? parentClaimantId = null,
        int? tokenId = null) : base(id)
    {
        PersonalInfo = personalInfo ?? throw new ArgumentNullException(nameof(personalInfo));
        AdditionalDetail = additionalDetail;
        AdditionalDetailInternal = additionalDetailInternal;
        LastBackgroundCheck = lastBackgroundCheck;
        IsInsured = isInsured;
        IsSubject = isSubject;
        AliasDisplay = aliasDisplay;
        ParentClaimantId = parentClaimantId;
        TokenId = tokenId;
    }

    public PersonalInfo PersonalInfo { get; private set; }
    public string? AdditionalDetail { get; private set; }
    public string? AdditionalDetailInternal { get; private set; }
    public DateTime? LastBackgroundCheck { get; private set; }
    public bool IsInsured { get; private set; }
    public bool IsSubject { get; private set; }
    public string? AliasDisplay { get; private set; }
    public Guid? ParentClaimantId { get; private set; }
    public int? TokenId { get; private set; }

    // Collections
    public IReadOnlyCollection<ClaimantEmail> Emails => _emails.AsReadOnly();
    public IReadOnlyCollection<ClaimantPhone> Phones => _phones.AsReadOnly();
    public IReadOnlyCollection<ClaimantAddress> Addresses => _addresses.AsReadOnly();
    public IReadOnlyCollection<ClaimantVehicle> Vehicles => _vehicles.AsReadOnly();
    public IReadOnlyCollection<ClaimantSocialMedia> SocialMedia => _socialMedia.AsReadOnly();
    public IReadOnlyCollection<ClaimantEmployment> Employments => _employments.AsReadOnly();
    public IReadOnlyCollection<ClaimantRelation> Relations => _relations.AsReadOnly();

    // Business methods
    public void AddEmail(Email email)
    {
        if (_emails.Any(e => e.EmailAddress == email))
            throw new InvalidOperationException("El email ya existe para este claimant");

        _emails.Add(new ClaimantEmail(Guid.NewGuid(), Id, email));
    }

    public void AddPhone(PhoneNumber phone)
    {
        _phones.Add(new ClaimantPhone(Guid.NewGuid(), Id, phone));
    }

    public void AddAddress(Address address, AddressType addressType, bool isPrimary = false)
    {
        if (isPrimary)
        {
            // Remove primary flag from other addresses
            foreach (var existingAddress in _addresses)
            {
                existingAddress.SetPrimary(false);
            }
        }

        _addresses.Add(new ClaimantAddress(Guid.NewGuid(), Id, address, addressType, isPrimary));
    }

    public void AddVehicle(ClaimantVehicle vehicle)
    {
        _vehicles.Add(vehicle);
    }

    public void UpdatePersonalInfo(PersonalInfo personalInfo)
    {
        PersonalInfo = personalInfo ?? throw new ArgumentNullException(nameof(personalInfo));
    }

    public void UpdateBackgroundCheck(DateTime lastBackgroundCheck)
    {
        LastBackgroundCheck = lastBackgroundCheck;
    }
}