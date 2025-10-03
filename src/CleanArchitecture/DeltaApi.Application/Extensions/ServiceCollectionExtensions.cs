using DeltaApi.Application.Interfaces.Claims;
using DeltaApi.Application.Services;
using DeltaApi.Application.UseCases.Claims;
using DeltaApi.Application.Validators.Claims;
using DeltaApi.Domain.Repositories;
using DeltaApi.Domain.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DeltaApi.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Registrar casos de uso
        services.AddScoped<ICreateClaimUseCase, CreateClaimUseCase>();
        services.AddScoped<IGetClaimByIdUseCase, GetClaimByIdUseCase>();
        services.AddScoped<IUpdateClaimUseCase, UpdateClaimUseCase>();
        services.AddScoped<IGetClaimsByClaimantIdUseCase, GetClaimsByClaimantIdUseCase>();
        services.AddScoped<IAddDocumentToClaimUseCase, AddDocumentToClaimUseCase>();
        services.AddScoped<IAddEventToClaimUseCase, AddEventToClaimUseCase>();

        // Registrar servicios de aplicación
        services.AddScoped<ClaimApplicationService>();

        // Registrar validadores
        services.AddScoped<CreateClaimRequestValidator>();
        services.AddScoped<UpdateClaimRequestValidator>();
        services.AddScoped<AddDocumentToClaimRequestValidator>();
        services.AddScoped<AddEventToClaimRequestValidator>();

        // Registrar servicios de dominio
        services.AddScoped<IClaimDomainService, ClaimDomainService>();

        return services;
    }
}


