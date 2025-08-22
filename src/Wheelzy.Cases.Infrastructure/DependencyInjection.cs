using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wheelzy.Cases.Application.Common.Interfaces;
using Wheelzy.Cases.Infrastructure.Persistence;
using Wheelzy.Cases.Infrastructure.Repositories;
using Wheelzy.Cases.Infrastructure.Services;

namespace Wheelzy.Cases.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<WheelzyDbContext>(opt =>
            opt.UseSqlServer(config.GetConnectionString("WheelzyDb")));
        
        services.AddScoped<ICaseRepository, CaseRepository>();
        services.AddScoped<ICaseService, CaseService>();
        
        return services;
    }
}
