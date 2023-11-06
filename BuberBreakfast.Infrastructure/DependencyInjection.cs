using BuberBreakfast.Application.Common.Interfaces.Authentication;
using BuberBreakfast.Application.Common.Interfaces.Persistance;
using BuberBreakfast.Application.Common.Interfaces.Services;
using BuberBreakfast.Infrastructure.Authentication;
using BuberBreakfast.Infrastructure.Persistence;
using BuberBreakfast.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuberBreakfast.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration
    )
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}