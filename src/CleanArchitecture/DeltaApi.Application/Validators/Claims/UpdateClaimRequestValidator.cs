using DeltaApi.Application.DTOs.Claims;
using FluentValidation;

namespace DeltaApi.Application.Validators.Claims;

public class UpdateClaimRequestValidator : AbstractValidator<UpdateClaimRequest>
{
    public UpdateClaimRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("El ID de la reclamación es requerido");

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


