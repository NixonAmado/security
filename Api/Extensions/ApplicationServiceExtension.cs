using Application.UnitOfWork;
using Domain.Interfaces;

namespace Api.Extensions;

public static class ApplicationServiceExtension
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
