using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using UserListsAPI.DTOs;
using UserListsMVC.ApiLayer;
using UserListsMVC.DataLayer;
using UserListsMVC.DataLayer.Repo.Implementation;
using UserListsMVC.DataLayer.Repo.Interface;
using UserListsMVC.Events;
using UserListsMVC.ServiceLayer.Implementation;
using UserListsMVC.ServiceLayer.Interface;
using UserListsMVC.Services.Implementation;
using UserListsMVC.Services.Interface;

namespace UserListsMVC;

public class Program
{
    public async static Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext().CreateLogger();
        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(logger);

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' was not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
        }

        builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();

        builder.Services.AddResponseCaching();
        builder.Services.AddControllersWithViews(options =>
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

        builder.Services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequiredLength = 5;
            options.Password.RequireUppercase = true;
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.User.RequireUniqueEmail = true;
        });

        builder.Configuration.AddEnvironmentVariables().AddUserSecrets(Assembly.GetExecutingAssembly(), true);

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddDateOnlyTimeOnlyStringConverters();

        builder.Services.AddAutoMapper(typeof(AppMappingProfile));

        builder.Services.AddTransient<IEmailSender, EmailSender>();
        builder.Services.AddScoped<IUserInitService, UserInitService>();
        builder.Services.AddScoped<IUserRepo, UserRepo>();

        builder.Services.AddSingleton<IWebApi<Game>, WebApiGame>();
        builder.Services.AddSingleton<IWebApi<Movie>, WebApiMovie>();
        builder.Services.AddScoped(typeof(IItemService<>), typeof(ItemService<>));

        builder.Services.AddScoped(typeof(IUserListService<>), typeof(UserListService<>));
        builder.Services.AddScoped<IUserListRepo<FollowlistItem>, FollowlistRepo>();
        builder.Services.AddScoped<IUserListRepo<WishlistItem>, WishlistRepo>();
        builder.Services.AddScoped<IUserListRepo<CustomListItem>, CustomListRepo>();

        builder.Services.AddScoped<IItemInfoService, ItemInfoService>();

        builder.Services.AddScoped<IItemInfoRepo, ItemInfoRepo>();
        builder.Services.AddScoped<ITextRepo<Comment>, CommentRepo>();
        builder.Services.AddScoped<ITextRepo<Reply>, ReplyRepo>();

        builder.Services.AddScoped<IVoteRepo<ItemVote>, ItemVoteRepo>();
        builder.Services.AddScoped<IVoteRepo<ReplyVote>, ReplyVoteRepo>();
        builder.Services.AddScoped<IVoteRepo<CommentVote>, CommentVoteRepo>();

        builder.Services.AddScoped<IViewCounterRepo, ViewCounterRepo>();

        builder.Services.AddScoped<INotificationService, NotificationService>();
        builder.Services.AddScoped<IEmailNotificationService, EmailNotificationService>();
        builder.Services.AddScoped<IEmailBugReportService, EmailBugReportService>();

        builder.Services.AddScoped<IEventProcessor, EventProcessor>();

        builder.Services.AddHostedService<DailyHostedService>();

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseResponseCaching();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapDefaultControllerRoute();

        app.MapRazorPages();

        await app.RunAsync();
    }
}