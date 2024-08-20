using Application.Interface;
using Infrastructure.Data;
using Infrastructure.Data.Seeders;
using Infrastructure.Services;
using Infrastructure.Services.Email;
using Infrastructure.Services.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure
{
    public static class DependencyInjection
    {
        public async static Task<IServiceCollection> AddInfrastructure(this IServiceCollection services, IConfiguration configuration) 
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<StoreNikDbConText>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.AddTransient<UserExitsHandlingMiddleware>();
            services.AddScoped<IAccountManager, AccountManagerServices>();
            services.AddTransient<IEmail,EmailServices>();  
            services.AddScoped<UserTokenProvideServices>();
            services.AddScoped<ITokenClaims, TokenClaimServices>();
            services.AddScoped<IStoreNikDbContext>(provider=>provider.GetRequiredService<StoreNikDbConText>());
            services.AddTransient<DataSeeder>();
            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
                await seeder.SeederAsync();
            }
            return services;
        }
    }
}
