using Application.Interface;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
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
            services.AddScoped<IStoreNikDbContext>(provider=>provider.GetRequiredService<StoreNikDbConText>());
            return services;
        }
    }
}
