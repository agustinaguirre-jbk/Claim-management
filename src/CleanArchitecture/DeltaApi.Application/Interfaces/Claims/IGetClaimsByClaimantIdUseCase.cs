using DeltaApi.Application.DTOs.Claims;

namespace DeltaApi.Application.Interfaces.Claims;

public interface IGetClaimsByClaimantIdUseCase
{
    Task<IEnumerable<ClaimResponse>> ExecuteAsync(int claimantId, CancellationToken cancellationToken = default);
}

