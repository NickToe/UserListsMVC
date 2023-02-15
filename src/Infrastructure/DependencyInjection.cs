using Application.Abstractions;
using Application.DTOs;
using Application.Events;
using Domain;
using Domain.Entities;
using Infrastructure.Api;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Infrastructure.Services.UserListHelpers;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("InMemoryDb"));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        }

        services.AddDefaultIdentity<ApplicationUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = true;
            options.Password.RequiredLength = 5;
            options.Password.RequireUppercase = true;
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.User.RequireUniqueEmail = true;
        })
            .AddEntityFrameworkStores<ApplicationDbContext>();

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        services.AddAutoMapper(typeof(InfrastructureMappingProfile));

        services.AddTransient<IEmailSender, EmailSender>();

        services.AddScoped<IUserInitService, UserInitService>();
        services.AddScoped<IUserService, UserService>();

        services.AddSingleton<IWebApi<GameDTO>, WebApiGame>();
        services.AddSingleton<IWebApi<MovieDTO>, WebApiMovie>();
        services.AddScoped(typeof(IItemService<>), typeof(ItemService<>));

        services.AddScoped(typeof(IUserListService<>), typeof(UserListService<>));
        services.AddScoped<IUserListHelper<FollowlistItem>, FollowlistHelper>();
        services.AddScoped<IUserListHelper<WishlistItem>, WishlistHelper>();
        services.AddScoped<IUserListHelper<CustomListItem>, CustomListHelper>();

        services.AddScoped<IItemInfoService, ItemInfoService>();

        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IEmailNotificationService, EmailNotificationService>();
        services.AddScoped<IEmailBugReportService, EmailBugReportService>();

        services.AddScoped<IEventProcessor, EventProcessor>();

        services.AddHostedService<DailyHostedService>();

        return services;
    }
}
