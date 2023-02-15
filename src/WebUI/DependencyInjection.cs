using Microsoft.AspNetCore.Mvc;

namespace WebUI;

public static class DependencyInjection
{
    public static IServiceCollection AddWebUIServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddResponseCaching();

        services.AddControllersWithViews(options =>
        {
            options.CacheProfiles.Add("Caching", new CacheProfile()
            {
                Duration = 120,
                Location = ResponseCacheLocation.Any,
                VaryByHeader = "cookie"
            });
            options.CacheProfiles.Add("NoCaching", new CacheProfile()
            {
                NoStore = true,
                Location = ResponseCacheLocation.None
            });
        });

        services.AddDateOnlyTimeOnlyStringConverters();

        return services;
    }
}
