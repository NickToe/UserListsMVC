using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using UserListsMVC.DataLayer.Entities;

namespace UserListsMVC.DataLayer;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

  public ApplicationDbContext() { }

  protected override void OnConfiguring(DbContextOptionsBuilder contextBuilder)
  {
    IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
    contextBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
  }
}