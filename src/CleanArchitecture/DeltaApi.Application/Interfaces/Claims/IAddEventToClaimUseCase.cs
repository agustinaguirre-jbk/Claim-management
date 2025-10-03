using DeltaApi.Application.DTOs.Claims;

namespace DeltaApi.Application.Interfaces.Claims;

public interface IAddEventToClaimUseCase
{
    Task<ClaimEventResponse> ExecuteAsync(AddEventToClaimRequest request, CancellationToken cancellationToken = default);
}

