using DeltaApi.Application.DTOs.Claims;
using FluentValidation;

namespace DeltaApi.Application.Validators.Claims;

public class AddEventToClaimRequestValidator : AbstractValidator<AddEventToClaimRequest>
{
    public AddEventToClaimRequestValidator()
    {
        RuleFor(x => x.ClaimId)
            .NotEmpty()
            .WithMessage("El ID de la reclamación es requerido");

        RuleFor(x => x.EventType)
            .NotEmpty()
            .WithMessage("El tipo de evento es requerido")
            .MaximumLength(100)
            .WithMessage("El tipo de evento no puede exceder 100 caracteres");

        RuleFor(x => x.EventDate)
            .NotEmpty()
            .WithMessage("La fecha del evento es requerida")
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("La fecha del evento no puede ser futura");

        RuleFor(x => x.Notes)
            .MaximumLength(1000)
            .WithMessage("Las notas no pueden exceder 1000 caracteres")
            .When(x => !string.IsNullOrEmpty(x.Notes));
    }
}


