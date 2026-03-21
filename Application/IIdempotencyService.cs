namespace Application;

public interface IIdempotencyService
{
    Task<string?> GetCachedResponseAsync(string key, CancellationToken cancellationToken = default);
    Task StoreResponseAsync(string key, string requestName, string response, CancellationToken cancellationToken = default);
}
