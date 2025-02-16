using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ToDo.Data.Services;

public static class DataSetupService
{
    public static IServiceCollection AddDataServices(IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(o => o.UseSqlServer(connectionString));

        services.AddDefaultIdentity<AppUser>(o => o.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>() // Not sure if this is needed or set by AddDefaultIdentity.
            .AddDefaultTokenProviders(); // Not sure if this is needed or set by AddDefaultIdentity.

        services.AddMediatR(o => o.RegisterServicesFromAssembly(System.Reflection.Assembly.GetExecutingAssembly()));

        return services;
    }
}
