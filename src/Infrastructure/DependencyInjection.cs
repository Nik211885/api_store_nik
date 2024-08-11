using Application.Interface;
using ApplicationCore.Entities;
using ApplicationCore.Interface;
using Infrastructure.Authentication;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) 
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<StoreNikDbConText>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            services.AddScoped<ITokenClaims, TokenClaimServices>();
            services.AddScoped<IStoreNikDbContext>(provider=>provider.GetRequiredService<StoreNikDbConText>());
            return services;
        }
    }
}
