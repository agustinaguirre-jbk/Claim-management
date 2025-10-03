using DeltaApi.Application.DTOs.Claims;
using DeltaApi.Application.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace DeltaApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ClaimsController : ControllerBase
{
    private readonly ClaimApplicationService _claimService;
    private readonly ILogger<ClaimsController> _logger;

    public ClaimsController(ClaimApplicationService claimService, ILogger<ClaimsController> logger)
    {
        _claimService = claimService ?? throw new ArgumentNullException(nameof(claimService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Crea una nueva reclamación
    /// </summary>
    /// <param name="request">Datos de la reclamación a crear</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>La reclamación creada</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ClaimResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ClaimResponse>> CreateClaim(
        [FromBody] CreateClaimRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Creando nueva reclamación para el caso {CaseId}", request.CaseId);
            
            var result = await _claimService.CreateClaimAsync(request, cancellationToken);
            
            _logger.LogInformation("Reclamación creada exitosamente con ID {ClaimId}", result.Id);
            
            return CreatedAtAction(nameof(GetClaimById), new { id = result.Id }, result);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning("Error de validación al crear reclamación: {Errors}", string.Join(", ", ex.Errors.Select(e => e.ErrorMessage)));
            return BadRequest(new { errors = ex.Errors.Select(e => e.ErrorMessage) });
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning("Error de operación al crear reclamación: {Message}", ex.Message);
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado al crear reclamación");
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Obtiene una reclamación por su ID
    /// </summary>
    /// <param name="id">ID de la reclamación</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>La reclamación encontrada</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ClaimResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ClaimResponse>> GetClaimById(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Obteniendo reclamación con ID {ClaimId}", id);
            
            var result = await _claimService.GetClaimByIdAsync(id, cancellationToken);
            
            if (result == null)
            {
                _logger.LogWarning("Reclamación con ID {ClaimId} no encontrada", id);
                return NotFound(new { error = "Reclamación no encontrada" });
            }
            
            _logger.LogInformation("Reclamación con ID {ClaimId} obtenida exitosamente", id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado al obtener reclamación con ID {ClaimId}", id);
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Actualiza una reclamación existente
    /// </summary>
    /// <param name="id">ID de la reclamación</param>
    /// <param name="request">Datos actualizados de la reclamación</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>La reclamación actualizada</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ClaimResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ClaimResponse>> UpdateClaim(
        Guid id,
        [FromBody] UpdateClaimRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Actualizando reclamación con ID {ClaimId}", id);
            
            request.Id = id; // Asegurar que el ID coincida
            var result = await _claimService.UpdateClaimAsync(request, cancellationToken);
            
            _logger.LogInformation("Reclamación con ID {ClaimId} actualizada exitosamente", id);
            return Ok(result);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning("Error de validación al actualizar reclamación {ClaimId}: {Errors}", id, string.Join(", ", ex.Errors.Select(e => e.ErrorMessage)));
            return BadRequest(new { errors = ex.Errors.Select(e => e.ErrorMessage) });
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning("Error de operación al actualizar reclamación {ClaimId}: {Message}", id, ex.Message);
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado al actualizar reclamación con ID {ClaimId}", id);
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }


    /// <summary>
    /// Agrega un documento a una reclamación
    /// </summary>
    /// <param name="request">Datos del documento a agregar</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>El documento agregado</returns>
    [HttpPost("documents")]
    [ProducesResponseType(typeof(ClaimDocumentResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ClaimDocumentResponse>> AddDocument(
        [FromBody] AddDocumentToClaimRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Agregando documento a reclamación {ClaimId}", request.ClaimId);
            
            var result = await _claimService.AddDocumentToClaimAsync(request, cancellationToken);
            
            _logger.LogInformation("Documento agregado exitosamente con ID {DocumentId}", result.Id);
            return CreatedAtAction(nameof(GetClaimById), new { id = request.ClaimId }, result);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning("Error de validación al agregar documento: {Errors}", string.Join(", ", ex.Errors.Select(e => e.ErrorMessage)));
            return BadRequest(new { errors = ex.Errors.Select(e => e.ErrorMessage) });
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning("Error de operación al agregar documento: {Message}", ex.Message);
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado al agregar documento a reclamación {ClaimId}", request.ClaimId);
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Agrega un evento a una reclamación
    /// </summary>
    /// <param name="request">Datos del evento a agregar</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>El evento agregado</returns>
    [HttpPost("events")]
    [ProducesResponseType(typeof(ClaimEventResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ClaimEventResponse>> AddEvent(
        [FromBody] AddEventToClaimRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Agregando evento a reclamación {ClaimId}", request.ClaimId);
            
            var result = await _claimService.AddEventToClaimAsync(request, cancellationToken);
            
            _logger.LogInformation("Evento agregado exitosamente con ID {EventId}", result.Id);
            return CreatedAtAction(nameof(GetClaimById), new { id = request.ClaimId }, result);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning("Error de validación al agregar evento: {Errors}", string.Join(", ", ex.Errors.Select(e => e.ErrorMessage)));
            return BadRequest(new { errors = ex.Errors.Select(e => e.ErrorMessage) });
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning("Error de operación al agregar evento: {Message}", ex.Message);
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado al agregar evento a reclamación {ClaimId}", request.ClaimId);
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }
}
