using DeltaApi.Application.DTOs.Claims;
using FluentValidation;

namespace DeltaApi.Application.Validators.Claims;

public class AddDocumentToClaimRequestValidator : AbstractValidator<AddDocumentToClaimRequest>
{
    public AddDocumentToClaimRequestValidator()
    {
        RuleFor(x => x.ClaimId)
            .NotEmpty()
            .WithMessage("El ID de la reclamación es requerido");

        RuleFor(x => x.DocumentType)
            .NotEmpty()
            .WithMessage("El tipo de documento es requerido")
            .MaximumLength(100)
            .WithMessage("El tipo de documento no puede exceder 100 caracteres");

        RuleFor(x => x.FilePath)
            .NotEmpty()
            .WithMessage("La ruta del archivo es requerida")
            .MaximumLength(1000)
            .WithMessage("La ruta del archivo no puede exceder 1000 caracteres");
    }
}


