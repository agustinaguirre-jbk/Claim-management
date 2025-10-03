using DeltaApi.Application.DTOs.Claims;

namespace DeltaApi.Application.Interfaces.Claims;

public interface IUpdateClaimUseCase
{
    Task<ClaimResponse> ExecuteAsync(UpdateClaimRequest request, CancellationToken cancellationToken = default);
}

