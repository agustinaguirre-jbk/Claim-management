namespace DeltaApi.Domain.ValueObjects;

public sealed record Address
{
    public string Name { get; }
    public string StreetAddress { get; }
    public string SecondaryAddress { get; }
    public string City { get; }
    public string State { get; }
    public string PostalCode { get; }
    public string Country { get; }
    public decimal? Latitude { get; }
    public decimal? Longitude { get; }
    public string TimeZoneWindows { get; }
    public string TimeZoneLinux { get; }

    public Address(
        string name,
        string streetAddress,
        string city,
        string state,
        string postalCode,
        string country,
        string? secondaryAddress = null,
        decimal? latitude = null,
        decimal? longitude = null,
        string? timeZoneWindows = null,
        string? timeZoneLinux = null)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        StreetAddress = streetAddress ?? throw new ArgumentNullException(nameof(streetAddress));
        City = city ?? throw new ArgumentNullException(nameof(city));
        State = state ?? throw new ArgumentNullException(nameof(state));
        PostalCode = postalCode ?? throw new ArgumentNullException(nameof(postalCode));
        Country = country ?? throw new ArgumentNullException(nameof(country));
        SecondaryAddress = secondaryAddress;
        Latitude = latitude;
        Longitude = longitude;
        TimeZoneWindows = timeZoneWindows;
        TimeZoneLinux = timeZoneLinux;
    }
}
