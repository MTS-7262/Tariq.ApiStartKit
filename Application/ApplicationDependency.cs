using Application.Abstractions;
using Application.Extensions;
using Application.Pipelines;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace Application;

public static class ApplicationDependency
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(ApplicationDependency).Assembly;

        _ = services.AddValidatorsFromAssembly(assembly)
                  .AddHandlersFromAssembly(assembly)
                  .RegisterApiEndpointsFromAssembly(assembly)
                  .AddAutoMapperProfilesFromAssembly(assembly);

        return services;
    }

    private static IServiceCollection AddHandlersFromAssembly(
        this IServiceCollection services,
        Assembly assembly)
    {
        var handlerTypes = assembly.GetTypes()
            .Where(t =>
                t is { IsClass: true, IsAbstract: false, ContainsGenericParameters: false })
            .ToList();

        foreach (var implementation in handlerTypes)
        {
            var handlerInterfaces = implementation
                .GetInterfaces()
                .Where(i =>
                    i.IsGenericType &&
                    (i.GetGenericTypeDefinition() == typeof(IHandler<,>) ||
                    i.GetGenericTypeDefinition() == typeof(IHandler<>)));

            foreach (var handlerInterface in handlerInterfaces)
            {
                services.AddScoped(handlerInterface, implementation);
            }
        }
        services.Decorate(typeof(IHandler<,>), typeof(ValidationDecorator<,>));
        services.Decorate(typeof(IHandler<,>), typeof(LoggingDecorator<,>));

        services.Decorate(typeof(IHandler<>), typeof(NoRequestLoggingDecorator<>));

        return services;
    }

    private static IServiceCollection AddAutoMapperProfilesFromAssembly(
        this IServiceCollection services,
        Assembly assembly)
    {
        var profileTypes = assembly.GetTypes()
            .Where(t => typeof(Profile).IsAssignableFrom(t) && t is { IsAbstract: false, IsClass: true });

        var expr = new MapperConfigurationExpression();

        foreach (var type in profileTypes)
        {
            var profileInstance = (Profile)Activator.CreateInstance(type)!;
            expr.AddProfile(profileInstance);
        }

        using var loggerFactory = new LoggerFactory();
        var config = new MapperConfiguration(expr, loggerFactory);
        var mapper = config.CreateMapper();

        services.AddSingleton(mapper);

        return services;
    }
}
