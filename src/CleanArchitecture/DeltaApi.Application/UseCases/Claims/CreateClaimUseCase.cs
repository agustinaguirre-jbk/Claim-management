using DeltaApi.Application.DTOs.Claims;
using DeltaApi.Application.Interfaces.Claims;
using DeltaApi.Domain.Claims;
using DeltaApi.Domain.Repositories;
using DeltaApi.Domain.Services;
using DeltaApi.Domain.ValueObjects;

namespace DeltaApi.Application.UseCases.Claims;

public class CreateClaimUseCase : ICreateClaimUseCase
{
    private readonly IClaimRepository _claimRepository;
    private readonly IClaimDomainService _claimDomainService;

    public CreateClaimUseCase(
        IClaimRepository claimRepository,
        IClaimDomainService claimDomainService)
    {
        _claimRepository = claimRepository ?? throw new ArgumentNullException(nameof(claimRepository));
        _claimDomainService = claimDomainService ?? throw new ArgumentNullException(nameof(claimDomainService));
    }

    public async Task<ClaimResponse> ExecuteAsync(CreateClaimRequest request, CancellationToken cancellationToken = default)
    {
        // 1. Validar reglas de negocio
        if (!await _claimDomainService.CanCreateClaimAsync(request.ClaimantId, request.ClientId, cancellationToken))
        {
            throw new InvalidOperationException("No se puede crear la reclamación con los parámetros proporcionados");
        }

        // 2. Verificar que el número de póliza sea único
        if (!await _claimDomainService.IsPolicyNumberUniqueAsync(request.PolicyNumber, null, cancellationToken))
        {
            throw new InvalidOperationException("El número de póliza ya existe");
        }

        // 3. Crear entidad del dominio
        var claim = await _claimDomainService.CreateClaimAsync(
            request.CaseId,
            request.ClaimTypeId,
            request.ClaimantId,
            request.ClientId,
            request.PolicyNumber,
            cancellationToken
        );

        // 4. Configurar información adicional si se proporciona
        if (!string.IsNullOrEmpty(request.DeltaFileNumber) || !string.IsNullOrEmpty(request.ClientFileNumber))
        {
            var policyInfo = new PolicyInfo(
                request.PolicyNumber,
                request.DeltaFileNumber,
                request.ClientFileNumber
            );
            claim.UpdatePolicyInfo(policyInfo, 1); // TODO: Obtener del contexto del usuario
        }

        if (request.AllegedInjury != null || request.InjuryDescription != null || 
            request.AttorneyRepresentation || request.Liability != null || 
            request.WorkersCompensation || request.Exposure.HasValue)
        {
            var injuryInfo = new InjuryInfo(
                request.AllegedInjury,
                request.InjuryDescription,
                request.AttorneyRepresentation,
                request.Liability,
                request.WorkersCompensation,
                request.Exposure
            );
            claim.UpdateInjuryInfo(injuryInfo, 1); // TODO: Obtener del contexto del usuario
        }

        if (request.DoctorId.HasValue)
        {
            claim.AssignDoctor(request.DoctorId.Value, 1); // TODO: Obtener del contexto del usuario
        }

        if (request.StateOfLossId.HasValue)
        {
            claim.AssignStateOfLoss(request.StateOfLossId.Value, 1); // TODO: Obtener del contexto del usuario
        }

        // 5. Persistir en la base de datos
        await _claimRepository.AddAsync(claim, cancellationToken);

        // 6. Retornar respuesta
        return MapToResponse(claim);
    }

    private static ClaimResponse MapToResponse(Claim claim)
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

