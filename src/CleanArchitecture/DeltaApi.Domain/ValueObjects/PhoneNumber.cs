namespace DeltaApi.Domain.ValueObjects;

public sealed record PhoneNumber
{
    public string Value { get; }
    public PhoneType Type { get; }

    public PhoneNumber(string value, PhoneType type)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El número de teléfono no puede estar vacío", nameof(value));

        Value = value;
        Type = type;
    }

    public static implicit operator string(PhoneNumber phone) => phone.Value;
}

public enum PhoneType
{
    Mobile,
    Home,
    Work,
    Other
}
