using UserListsMVC.DataLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserListsMVC.ServiceLayer;
using UserListsMVC.DataLayer.Repo;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using UserListsMVC.ApiLayer;

namespace UserListsMVC;

public class Program
{
  public async static Task Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);

    var logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext().CreateLogger();
    builder.Logging.ClearProviders();
    builder.Logging.AddSerilog(logger);

    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
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

    builder.Services.AddHttpContextAccessor();

    builder.Services.AddDateOnlyTimeOnlyStringConverters();

    builder.Services.AddScoped<IUserListStore, UserListStore>();
    builder.Services.AddScoped<IUserRepo, UserRepo>();

    builder.Services.AddSingleton<IWebApi<Game>, WebApiGame>();
    builder.Services.AddSingleton<IWebApi<Movie>, WebApiMovie>();
    builder.Services.AddScoped(typeof(IItemService<>), typeof(ItemService<>));

    builder.Services.AddScoped(typeof(IUserListService<>), typeof(UserListService<>));
    builder.Services.AddScoped<IUserListRepo<FollowlistItem>, FollowlistRepo>();
    builder.Services.AddScoped<IUserListRepo<WishlistItem>, WishlistRepo>();
    builder.Services.AddScoped<IUserListRepo<CustomListItem>, CustomListRepo>();

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

    app.Run();
  }
}