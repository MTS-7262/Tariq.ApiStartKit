using Application.Abstractions;
using Domain.Abstractions;
using Microsoft.Extensions.Logging;

namespace Application.Pipelines;

public sealed class NoRequestLoggingDecorator<TResponse>(
    ILogger<NoRequestLoggingDecorator<TResponse>> logger,
    IHandler<TResponse> innerHandler) : IHandler<TResponse>
{
    public async Task<TResponse> HandleAsync(CancellationToken cancellationToken)
    {
        var handlerName = innerHandler.GetType().Name;

        logger.LogInformation("Executing handler {HandlerName}", handlerName);

        var response = await innerHandler.HandleAsync(cancellationToken);

        if (response is Result result && result.IsFailure)
        {
            logger.LogWarning(
                "Handler {HandlerName} failed with error code {ErrorCode}",
                handlerName,
                result.Error.Code);
        }
        else
        {
            logger.LogInformation("Successfully executed handler {HandlerName}", handlerName);
        }

        return response;
    }
}