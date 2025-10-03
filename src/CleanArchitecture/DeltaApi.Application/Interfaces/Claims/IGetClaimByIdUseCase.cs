using DeltaApi.Application.DTOs.Claims;

namespace DeltaApi.Application.Interfaces.Claims;

public interface IGetClaimByIdUseCase
{
    Task<ClaimResponse?> ExecuteAsync(Guid id, CancellationToken cancellationToken = default);
}
