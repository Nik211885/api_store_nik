using Application;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Identity;
using Infrastructure.Services.Email;
using Microsoft.AspNetCore.Identity;
using WebApi.Services;
using static Org.BouncyCastle.Math.EC.ECCurve;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthenticationSwaggerServices(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(cfg =>
    {
        cfg.User.RequireUniqueEmail = true;
        cfg.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
    })
    .AddDefaultTokenProviders()
    .AddApiEndpoints()
    .AddEntityFrameworkStores<StoreNikDbConText>();

Console.WriteLine(TokenOptions.DefaultEmailProvider);
builder.Services.AddApplication();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
