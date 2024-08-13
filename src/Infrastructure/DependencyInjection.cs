using Application.Interface;
using Application.Mappings;
using Infrastructure.Data;
using Infrastructure.Services;
using Infrastructure.Services.Email;
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
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.AddScoped<IAccountManager, AccountManagerServices>();
            services.AddTransient<IEmail,EmailServices>();  
            services.AddScoped<ITokenClaims, TokenClaimServices>();
            services.AddScoped<IStoreNikDbContext>(provider=>provider.GetRequiredService<StoreNikDbConText>());
            return services;
        }
    }
}
