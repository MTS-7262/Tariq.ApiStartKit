using Application.Abstractions.Data;
using Infrastructure.Data;
using Infrastructure.Database;
using Infrastructure.Interceptors;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infrastructure;

public static class InfrastructureDependency
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<AuditInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetRequiredService<AuditInterceptor>());
            options.UseNpgsql(configuration.GetConnectionString("Default"));
        });

        services.AddIdentityApiEndpoints<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();


        var infrastructureAssembly = Assembly.GetExecutingAssembly();

        var implementations = infrastructureAssembly.GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false, ContainsGenericParameters: false })
            .ToList();

        foreach (var implementation in implementations)
        {
            var matchingInterface = implementation
                .GetInterfaces()
                .FirstOrDefault(i => i.Name == $"I{implementation.Name}");

            if (matchingInterface != null)
            {
                services.AddScoped(matchingInterface, implementation);
            }
        }

        return services;
    }
}
