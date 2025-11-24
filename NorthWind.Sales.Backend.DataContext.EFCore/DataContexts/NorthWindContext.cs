//using System.Reflection;

using NorthWind.Sales.Backend.DataContext.EFCore.Options;
using Npgsql;
namespace NorthWind.Sales.Backend.DataContext.EFCore.DataContexts;

internal class NorthWindContext: DbContext
{
    public NorthWindContext() { }

    public NorthWindContext(DbContextOptions<NorthWindContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=BillingDBPrueba_1;Username=postgres;Password=alex123seven");

        }
    }

    public DbSet<Order> Orders { get; set; }
    public DbSet<Repositories.Entities.OrderDetail> OrderDetails { get; set; }

    // Permite a las herramientas del EntityFramework Core aplicar la configuración de la entidades
    // es decir migrar las clases en tablas
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        //base.OnModelCreating(modelBuilder);
    }




}
