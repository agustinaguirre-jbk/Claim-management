using DeltaApi.Domain.Claims;

namespace DeltaApi.Infrastructure.Mappings;

public static class StateOfLossMapper
{
    public static StateOfLoss MapToStateOfLoss(dynamic row)
    {
        return new StateOfLoss(
            id: row.StateId,
            stateName: row.StateName,
            stateCode: row.StateCode,
            createdByUser: row.CreatedByUser
        );
    }
}
