using DeltaApi.Application.DTOs.Claims;
using FluentValidation;

namespace DeltaApi.Application.Validators.Claims;

public class CreateClaimRequestValidator : AbstractValidator<CreateClaimRequest>
{
    public CreateClaimRequestValidator()
    {
        RuleFor(x => x.CaseId)
            .GreaterThan(0)
            .WithMessage("El ID del caso debe ser mayor que 0");

        RuleFor(x => x.ClaimTypeId)
            .GreaterThan(0)
            .WithMessage("El ID del tipo de reclamación debe ser mayor que 0");

        RuleFor(x => x.ClaimantId)
            .GreaterThan(0)
            .WithMessage("El ID del claimant debe ser mayor que 0");

        RuleFor(x => x.ClientId)
            .GreaterThan(0)
            .WithMessage("El ID del cliente debe ser mayor que 0");

        RuleFor(x => x.PolicyNumber)
            .NotEmpty()
            .WithMessage("El número de póliza es requerido")
            .MaximumLength(100)
            .WithMessage("El número de póliza no puede exceder 100 caracteres");

        RuleFor(x => x.DeltaFileNumber)
            .MaximumLength(50)
            .WithMessage("El número de archivo Delta no puede exceder 50 caracteres")
            .When(x => !string.IsNullOrEmpty(x.DeltaFileNumber));

        RuleFor(x => x.ClientFileNumber)
            .MaximumLength(50)
            .WithMessage("El número de archivo del cliente no puede exceder 50 caracteres")
            .When(x => !string.IsNullOrEmpty(x.ClientFileNumber));

        RuleFor(x => x.AllegedInjury)
            .MaximumLength(255)
            .WithMessage("La lesión alegada no puede exceder 255 caracteres")
            .When(x => !string.IsNullOrEmpty(x.AllegedInjury));

        RuleFor(x => x.Liability)
            .MaximumLength(255)
            .WithMessage("La responsabilidad no puede exceder 255 caracteres")
            .When(x => !string.IsNullOrEmpty(x.Liability));

        RuleFor(x => x.Exposure)
            .GreaterThanOrEqualTo(0)
            .WithMessage("La exposición debe ser mayor o igual a 0")
            .When(x => x.Exposure.HasValue);
    }
}


