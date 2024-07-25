using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) 
        {
            var connectionString = configuration.GetConnectionString("DefaultConnect");
            services.AddDbContext<StoreNikDbConText>((sp, options) =>
            {
                options.UseSqlServer(connectionString);
            });
            //services.AddIdentityCore<ApplicationUser>()
            //    .AddEntityFrameworkStores<StoreNikDbConText>();
            return services;
        }
    }
}
