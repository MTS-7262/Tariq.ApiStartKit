namespace Application.Abstractions;

public interface IHandler<in TRequest, TResponse>
{
    Task<TResponse> HandleAsync(TRequest command, CancellationToken cancellationToken);
}

public interface IHandler<TResponse>
{
    Task<TResponse> HandleAsync(CancellationToken cancellationToken);
}
