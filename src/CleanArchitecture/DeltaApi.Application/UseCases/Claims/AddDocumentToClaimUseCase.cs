using DeltaApi.Application.DTOs.Claims;
using DeltaApi.Application.Interfaces.Claims;
using DeltaApi.Domain.Claims;
using DeltaApi.Domain.Repositories;

namespace DeltaApi.Application.UseCases.Claims;

public class AddDocumentToClaimUseCase : IAddDocumentToClaimUseCase
{
    private readonly IClaimRepository _claimRepository;

    public AddDocumentToClaimUseCase(IClaimRepository claimRepository)
    {
        _claimRepository = claimRepository ?? throw new ArgumentNullException(nameof(claimRepository));
    }

    public async Task<ClaimDocumentResponse> ExecuteAsync(AddDocumentToClaimRequest request, CancellationToken cancellationToken = default)
    {
        // 1. Obtener el claim existente
        var claim = await _claimRepository.GetByIdAsync(request.ClaimId, cancellationToken);
        if (claim == null)
        {
            throw new InvalidOperationException("La reclamación no existe");
        }

        // 2. Crear el documento
        var document = new ClaimDocument(
            id: Guid.NewGuid(),
            claimId: request.ClaimId,
            documentType: request.DocumentType,
            filePath: request.FilePath,
            createdByUser: 1 // TODO: Obtener del contexto del usuario
        );

        // 3. Agregar el documento al claim
        claim.AddDocument(document);

        // 4. Persistir cambios
        await _claimRepository.UpdateAsync(claim, cancellationToken);

        // 5. Retornar respuesta
        return new ClaimDocumentResponse
        {
            Id = document.Id,
            ClaimId = document.ClaimId,
            DocumentType = document.DocumentType,
            FilePath = document.FilePath,
            CreatedOn = document.CreatedOn
        };
    }
}


