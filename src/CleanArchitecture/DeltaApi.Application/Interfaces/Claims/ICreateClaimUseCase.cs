using DeltaApi.Application.DTOs.Claims;

namespace DeltaApi.Application.Interfaces.Claims;

public interface ICreateClaimUseCase
{
    Task<ClaimResponse> ExecuteAsync(CreateClaimRequest request, CancellationToken cancellationToken = default);
}

