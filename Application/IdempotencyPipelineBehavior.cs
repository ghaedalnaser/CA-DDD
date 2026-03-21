using Domain.Primitives;
using MediatR;
using System.Text.Json;

namespace Application;

public sealed class IdempotencyPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IIdempotentCommand
{
    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        Converters = { new ResultJsonConverterFactory() }
    };

    private readonly IIdempotencyService _idempotencyService;

    public IdempotencyPipelineBehavior(IIdempotencyService idempotencyService)
    {
        _idempotencyService = idempotencyService;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.IdempotencyKey))
            return await next();

        var cachedJson = await _idempotencyService.GetCachedResponseAsync(
            request.IdempotencyKey, cancellationToken);

        if (cachedJson is not null)
        {
            return JsonSerializer.Deserialize<TResponse>(cachedJson, _jsonOptions)!;
        }

        var response = await next();

        if (response is Result { IsSuccess: true })
        {
            var responseJson = JsonSerializer.Serialize(response, _jsonOptions);
            await _idempotencyService.StoreResponseAsync(
                request.IdempotencyKey,
                typeof(TRequest).Name,
                responseJson,
                cancellationToken);
        }

        return response;
    }
}
