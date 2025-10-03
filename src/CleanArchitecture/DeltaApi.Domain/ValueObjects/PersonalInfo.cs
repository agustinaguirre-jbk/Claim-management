namespace DeltaApi.Domain.ValueObjects;

public sealed record PersonalInfo
{
    public string FirstName { get; }
    public string? MiddleName { get; }
    public string LastName { get; }
    public string? Alias { get; }
    public string? SSN { get; }
    public DateTime? DateOfBirth { get; }
    public string? ImageUrl { get; }
    public string? DriversLicense { get; }
    public Race? Race { get; }
    public int? Weight { get; }
    public int? HeightInInches { get; }
    public Gender? Gender { get; }

    public PersonalInfo(
        string firstName,
        string lastName,
        string? middleName = null,
        string? alias = null,
        string? ssn = null,
        DateTime? dateOfBirth = null,
        string? imageUrl = null,
        string? driversLicense = null,
        Race? race = null,
        int? weight = null,
        int? heightInInches = null,
        Gender? gender = null)
    {
        FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
        LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        MiddleName = middleName;
        Alias = alias;
        SSN = ssn;
        DateOfBirth = dateOfBirth;
        ImageUrl = imageUrl;
        DriversLicense = driversLicense;
        Race = race;
        Weight = weight;
        HeightInInches = heightInInches;
        Gender = gender;
    }

    public string FullName => $"{FirstName} {MiddleName} {LastName}".Trim();
}

public enum Race
{
    Unknown = 0,
    White = 1,
    Black = 2,
    Hispanic = 3,
    Asian = 4,
    NativeAmerican = 5,
    PacificIslander = 6,
    Other = 7
}

public enum Gender
{
    Unknown = 0,
    Male = 1,
    Female = 2,
    Other = 3
}
