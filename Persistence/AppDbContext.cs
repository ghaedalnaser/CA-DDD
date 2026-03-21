using Domain.ActivityLogs;
using Domain.Items;
using Domain.Mission;
using Domain.Movers;
using Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence
{
    public class AppDbContext : DbContext, IUnitOfWork 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Item> Items => Set<Item>();
        public DbSet<Mover> Movers => Set<Mover>();
        public DbSet<ActivityLog> ActivityLogs => Set<ActivityLog>();
        public DbSet<Mission> Missions => Set<Mission>();
        public DbSet<IdempotencyKey> IdempotencyKeys => Set<IdempotencyKey>();

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ConcurrencyException("A concurrency conflict occurred.", ex);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

    }
}
