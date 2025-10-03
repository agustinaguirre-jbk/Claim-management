using DeltaApi.Application.DTOs.Claims;
using DeltaApi.Application.Interfaces.Claims;
using DeltaApi.Domain.Claims;
using DeltaApi.Domain.Repositories;

namespace DeltaApi.Application.UseCases.Claims;

public class AddEventToClaimUseCase : IAddEventToClaimUseCase
{
    private readonly IClaimRepository _claimRepository;

    public AddEventToClaimUseCase(IClaimRepository claimRepository)
    {
        _claimRepository = claimRepository ?? throw new ArgumentNullException(nameof(claimRepository));
    }

    public async Task<ClaimEventResponse> ExecuteAsync(AddEventToClaimRequest request, CancellationToken cancellationToken = default)
    {
        // 1. Obtener el claim existente
        var claim = await _claimRepository.GetByIdAsync(request.ClaimId, cancellationToken);
        if (claim == null)
        {
            throw new InvalidOperationException("La reclamación no existe");
        }

        // 2. Crear el evento
        var claimEvent = new ClaimEvent(
            id: Guid.NewGuid(),
            claimId: request.ClaimId,
            eventType: request.EventType,
            eventDate: request.EventDate,
            notes: request.Notes,
            createdByUser: 1 // TODO: Obtener del contexto del usuario
        );

        // 3. Agregar el evento al claim
        claim.AddEvent(claimEvent);

        // 4. Persistir cambios
        await _claimRepository.UpdateAsync(claim, cancellationToken);

        // 5. Retornar respuesta
        return new ClaimEventResponse
        {
            Id = claimEvent.Id,
            ClaimId = claimEvent.ClaimId,
            EventType = claimEvent.EventType,
            EventDate = claimEvent.EventDate,
            Notes = claimEvent.Notes,
            CreatedOn = claimEvent.CreatedOn
        };
    }
}


