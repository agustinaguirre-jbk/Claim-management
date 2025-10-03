using DeltaApi.Application.DTOs.Claims;
using DeltaApi.Application.Interfaces.Claims;
using DeltaApi.Application.UseCases.Claims;
using DeltaApi.Application.Validators.Claims;
using FluentValidation;

namespace DeltaApi.Application.Services;

public class ClaimApplicationService
{
    private readonly ICreateClaimUseCase _createClaimUseCase;
    private readonly IGetClaimByIdUseCase _getClaimByIdUseCase;
    private readonly IUpdateClaimUseCase _updateClaimUseCase;
    private readonly IGetClaimsByClaimantIdUseCase _getClaimsByClaimantIdUseCase;
    private readonly IAddDocumentToClaimUseCase _addDocumentToClaimUseCase;
    private readonly IAddEventToClaimUseCase _addEventToClaimUseCase;
    private readonly CreateClaimRequestValidator _createClaimValidator;
    private readonly UpdateClaimRequestValidator _updateClaimValidator;
    private readonly AddDocumentToClaimRequestValidator _addDocumentValidator;
    private readonly AddEventToClaimRequestValidator _addEventValidator;

    public ClaimApplicationService(
        ICreateClaimUseCase createClaimUseCase,
        IGetClaimByIdUseCase getClaimByIdUseCase,
        IUpdateClaimUseCase updateClaimUseCase,
        IGetClaimsByClaimantIdUseCase getClaimsByClaimantIdUseCase,
        IAddDocumentToClaimUseCase addDocumentToClaimUseCase,
        IAddEventToClaimUseCase addEventToClaimUseCase,
        CreateClaimRequestValidator createClaimValidator,
        UpdateClaimRequestValidator updateClaimValidator,
        AddDocumentToClaimRequestValidator addDocumentValidator,
        AddEventToClaimRequestValidator addEventValidator)
    {
        _createClaimUseCase = createClaimUseCase ?? throw new ArgumentNullException(nameof(createClaimUseCase));
        _getClaimByIdUseCase = getClaimByIdUseCase ?? throw new ArgumentNullException(nameof(getClaimByIdUseCase));
        _updateClaimUseCase = updateClaimUseCase ?? throw new ArgumentNullException(nameof(updateClaimUseCase));
        _getClaimsByClaimantIdUseCase = getClaimsByClaimantIdUseCase ?? throw new ArgumentNullException(nameof(getClaimsByClaimantIdUseCase));
        _addDocumentToClaimUseCase = addDocumentToClaimUseCase ?? throw new ArgumentNullException(nameof(addDocumentToClaimUseCase));
        _addEventToClaimUseCase = addEventToClaimUseCase ?? throw new ArgumentNullException(nameof(addEventToClaimUseCase));
        _createClaimValidator = createClaimValidator ?? throw new ArgumentNullException(nameof(createClaimValidator));
        _updateClaimValidator = updateClaimValidator ?? throw new ArgumentNullException(nameof(updateClaimValidator));
        _addDocumentValidator = addDocumentValidator ?? throw new ArgumentNullException(nameof(addDocumentValidator));
        _addEventValidator = addEventValidator ?? throw new ArgumentNullException(nameof(addEventValidator));
    }

    public async Task<ClaimResponse> CreateClaimAsync(CreateClaimRequest request, CancellationToken cancellationToken = default)
    {
        // Validar request
        var validationResult = await _createClaimValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        return await _createClaimUseCase.ExecuteAsync(request, cancellationToken);
    }

    public async Task<ClaimResponse?> GetClaimByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _getClaimByIdUseCase.ExecuteAsync(id, cancellationToken);
    }

    public async Task<ClaimResponse> UpdateClaimAsync(UpdateClaimRequest request, CancellationToken cancellationToken = default)
    {
        // Validar request
        var validationResult = await _updateClaimValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        return await _updateClaimUseCase.ExecuteAsync(request, cancellationToken);
    }

    public async Task<IEnumerable<ClaimResponse>> GetClaimsByClaimantIdAsync(int claimantId, CancellationToken cancellationToken = default)
    {
        return await _getClaimsByClaimantIdUseCase.ExecuteAsync(claimantId, cancellationToken);
    }

    public async Task<ClaimDocumentResponse> AddDocumentToClaimAsync(AddDocumentToClaimRequest request, CancellationToken cancellationToken = default)
    {
        // Validar request
        var validationResult = await _addDocumentValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        return await _addDocumentToClaimUseCase.ExecuteAsync(request, cancellationToken);
    }

    public async Task<ClaimEventResponse> AddEventToClaimAsync(AddEventToClaimRequest request, CancellationToken cancellationToken = default)
    {
        // Validar request
        var validationResult = await _addEventValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        return await _addEventToClaimUseCase.ExecuteAsync(request, cancellationToken);
    }
}
