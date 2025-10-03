using DeltaApi.Application.DTOs.Claims;
using DeltaApi.Application.Interfaces.Claims;
using DeltaApi.Domain.Repositories;
using DeltaApi.Domain.Services;
using DeltaApi.Domain.ValueObjects;

namespace DeltaApi.Application.UseCases.Claims;

public class UpdateClaimUseCase : IUpdateClaimUseCase
{
    private readonly IClaimRepository _claimRepository;
    private readonly IClaimDomainService _claimDomainService;

    public UpdateClaimUseCase(
        IClaimRepository claimRepository,
        IClaimDomainService claimDomainService)
    {
        _claimRepository = claimRepository ?? throw new ArgumentNullException(nameof(claimRepository));
        _claimDomainService = claimDomainService ?? throw new ArgumentNullException(nameof(claimDomainService));
    }

    public async Task<ClaimResponse> ExecuteAsync(UpdateClaimRequest request, CancellationToken cancellationToken = default)
    {
        // 1. Obtener el claim existente
        var claim = await _claimRepository.GetByIdAsync(request.Id, cancellationToken);
        if (claim == null)
        {
            throw new InvalidOperationException("La reclamación no existe");
        }

        // 2. Verificar que el número de póliza sea único (si cambió)
        if (claim.PolicyInfo.PolicyNumber != request.PolicyNumber)
        {
            if (!await _claimDomainService.IsPolicyNumberUniqueAsync(request.PolicyNumber, request.Id, cancellationToken))
            {
                throw new InvalidOperationException("El número de póliza ya existe");
            }
        }

        // 3. Actualizar información de póliza
        var policyInfo = new PolicyInfo(
            request.PolicyNumber,
            request.DeltaFileNumber,
            request.ClientFileNumber
        );
        claim.UpdatePolicyInfo(policyInfo, 1); // TODO: Obtener del contexto del usuario

        // 4. Actualizar información de lesiones
        var injuryInfo = new InjuryInfo(
            request.AllegedInjury,
            request.InjuryDescription,
            request.AttorneyRepresentation,
            request.Liability,
            request.WorkersCompensation,
            request.Exposure
        );
        claim.UpdateInjuryInfo(injuryInfo, 1); // TODO: Obtener del contexto del usuario

        // 5. Actualizar doctor si se proporciona
        if (request.DoctorId.HasValue)
        {
            if (await _claimDomainService.CanAssignDoctorAsync(request.DoctorId.Value, request.Id, cancellationToken))
            {
                claim.AssignDoctor(request.DoctorId.Value, 1); // TODO: Obtener del contexto del usuario
            }
            else
            {
                throw new InvalidOperationException("No se puede asignar el médico especificado");
            }
        }

        // 6. Actualizar estado de pérdida si se proporciona
        if (request.StateOfLossId.HasValue)
        {
            if (await _claimDomainService.CanAssignStateOfLossAsync(request.StateOfLossId.Value, request.Id, cancellationToken))
            {
                claim.AssignStateOfLoss(request.StateOfLossId.Value, 1); // TODO: Obtener del contexto del usuario
            }
            else
            {
                throw new InvalidOperationException("No se puede asignar el estado de pérdida especificado");
            }
        }

        // 7. Persistir cambios
        await _claimRepository.UpdateAsync(claim, cancellationToken);

        // 8. Retornar respuesta
        return MapToResponse(claim);
    }

    private static ClaimResponse MapToResponse(Domain.Claims.Claim claim)
    {
        return new ClaimResponse
        {
            Id = claim.Id,
            CaseId = claim.CaseId,
            ClaimTypeId = claim.ClaimTypeId,
            ClaimantId = claim.ClaimantId,
            ClientId = claim.ClientId,
            PolicyNumber = claim.PolicyInfo.PolicyNumber,
            DeltaFileNumber = claim.PolicyInfo.DeltaFileNumber,
            ClientFileNumber = claim.PolicyInfo.ClientFileNumber,
            DoctorId = claim.DoctorId,
            StateOfLossId = claim.StateOfLossId,
            AllegedInjury = claim.InjuryInfo?.AllegedInjury,
            InjuryDescription = claim.InjuryInfo?.InjuryDescription,
            AttorneyRepresentation = claim.InjuryInfo?.AttorneyRepresentation ?? false,
            Liability = claim.InjuryInfo?.Liability,
            WorkersCompensation = claim.InjuryInfo?.WorkersCompensation ?? false,
            Exposure = claim.InjuryInfo?.Exposure,
            CreatedOn = claim.CreatedOn,
            ModifiedOn = claim.ModifiedOn,
            Documents = claim.Documents.Select(d => new ClaimDocumentResponse
            {
                Id = d.Id,
                ClaimId = d.ClaimId,
                DocumentType = d.DocumentType,
                FilePath = d.FilePath,
                CreatedOn = d.CreatedOn
            }).ToList(),
            Events = claim.Events.Select(e => new ClaimEventResponse
            {
                Id = e.Id,
                ClaimId = e.ClaimId,
                EventType = e.EventType,
                EventDate = e.EventDate,
                Notes = e.Notes,
                CreatedOn = e.CreatedOn
            }).ToList()
        };
    }
}

