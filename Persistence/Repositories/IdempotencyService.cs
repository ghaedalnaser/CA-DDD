using Application;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class IdempotencyService : IIdempotencyService
{
    private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

    public IdempotencyService(IDbContextFactory<AppDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<string?> GetCachedResponseAsync(string key, CancellationToken cancellationToken = default)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var entry = await dbContext.IdempotencyKeys
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Key == key, cancellationToken);

        return entry?.Response;
    }

    public async Task StoreResponseAsync(string key, string requestName, string response, CancellationToken cancellationToken = default)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var entry = new IdempotencyKey
        {
            Id = Guid.NewGuid(),
            Key = key,
            RequestName = requestName,
            Response = response,
            CreatedAt = DateTime.UtcNow
        };

        dbContext.IdempotencyKeys.Add(entry);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
