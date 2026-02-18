using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MasPatas.Application.Interfaces;
using MasPatas.Application.Services;

namespace MasPatas.Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);
        services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
        services.AddScoped<IProductService, ProductService>();

        return services;
    }
}
