using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PJATK_APBD_Cw4_s32103.Entities;

namespace PJATK_APBD_Cw4_s32103.DAL;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<PC> PCs { get; set; } = null!;
    public DbSet<ComponentType> ComponentTypes { get; set; } = null!;
    public DbSet<ComponentManufacturer> ComponentManufacturers { get; set; } = null!;
    public DbSet<Component> Components { get; set; } = null!;
    public DbSet<PCComponent> PCComponents { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
