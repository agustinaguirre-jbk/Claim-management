using DeltaApi.Domain.Abstractions;

namespace DeltaApi.Domain.Claims;

public sealed class StateOfLoss : Entity
{
    public StateOfLoss(
        Guid id,
        string stateName,
        string stateCode,
        int createdByUser = 1) : base(id, createdByUser)
    {
        StateName = stateName ?? throw new ArgumentNullException(nameof(stateName));
        StateCode = stateCode ?? throw new ArgumentNullException(nameof(stateCode));
    }

    public string StateName { get; private set; }
    public string StateCode { get; private set; }

    public void UpdateState(string stateName, string stateCode, int modifiedByUser)
    {
        StateName = stateName ?? throw new ArgumentNullException(nameof(stateName));
        StateCode = stateCode ?? throw new ArgumentNullException(nameof(stateCode));
        MarkAsModified(modifiedByUser);
    }
}
