using ITQTestApp.Application.Contracts.Persistence;
using ITQTestApp.Infrastructure.Persistence;
using ITQTestApp.Infrastructure.Persistence.Repositories;
using ITQTestApp.Infrastructure.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ITQTestApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Default")));

            services.AddScoped<IReferenceItemRepository, ReferenceItemRepository>();
            services.AddScoped<IUnitOfWork, EfUnitOfWork>();

            return services;
        }
    }
}
