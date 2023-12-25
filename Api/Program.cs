using System.Reflection;
using Api.Extensions;
using Api.Helpers;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .ReadFrom
    .Configuration(builder.Configuration)
    .Enrich
    .FromLogContext()
    .CreateLogger();

builder
    .Services
    .AddDbContext<SecurityContext>(options =>
    {
        string connectionString = builder.Configuration.GetConnectionString("ConexMysql");
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    });

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddControllers();
builder.Services.AddApplicationServices();
builder.Services.AddJwt(builder.Configuration);
builder.Services.ConfigureApiVersioning();
builder.Services.ConfigureCors();
builder.Services.ConfigureRatelimiting();
builder.Logging.AddSerilog(logger);
builder.Services.AddDataProtection().PersistKeysToDbContext<SecurityContext>().UseCryptographicAlgorithms(new AuthenticatedEncryptorConfiguration()
{
    EncryptionAlgorithm = EncryptionAlgorithm.AES_256_GCM,
    ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
});

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseStatusCodePagesWithReExecute("/errors/{0}");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = services.GetRequiredService<SecurityContext>();
        await context.Database.MigrateAsync();
    }
    catch (Exception ex)
    {
        var _logger = loggerFactory.CreateLogger<Program>();
        _logger.LogError(ex, "Ocurrio un error durante la migracion");
    }
}

app.MapControllers();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors("CorsPolicy");

app.UseIpRateLimiting();

app.UseDefaultFiles();

app.UseStaticFiles();

app.Run();
