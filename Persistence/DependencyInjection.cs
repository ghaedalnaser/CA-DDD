
using Application;
using Domain.Items;
using Domain.Mission;
using Domain.Primitives;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;
using Domain.Movers;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextFactory<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")),
                ServiceLifetime.Scoped);

            services.AddScoped<IIdempotencyService, IdempotencyService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IItemRepository, ItemRepository>();

            services.AddScoped<IMoverRepository, MoverRepository>();

            services.AddScoped<IMissionRepository, MissionRepository>();

            return services;
        }
    }
}