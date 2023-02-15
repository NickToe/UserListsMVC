using System.Reflection;
using Serilog;
using UserListsMVC.Infrastructure;

namespace UserListsMVC;

public class Program
{
    public async static Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.AddEnvironmentVariables().AddUserSecrets(Assembly.GetExecutingAssembly(), true);

        var logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext().CreateLogger();
        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(logger);

        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
        }

        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddWebUIServices(builder.Configuration);

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