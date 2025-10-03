using DeltaApi.Application.DTOs.Claims;

namespace DeltaApi.Application.Interfaces.Claims;

public interface IAddDocumentToClaimUseCase
{
    Task<ClaimDocumentResponse> ExecuteAsync(AddDocumentToClaimRequest request, CancellationToken cancellationToken = default);
}

