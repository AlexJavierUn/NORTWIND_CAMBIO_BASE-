namespace NorthWind.Sales.Backend.DataContext.EFCore.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthWind.Sales.Backend.Repositories.Entities;

// Crear la tabla: OrderDetails
internal class OrderDetailConfiguration : IEntityTypeConfiguration<Repositories.Entities.OrderDetail>
{
    public void Configure(EntityTypeBuilder<Repositories.Entities.OrderDetail> builder)
    {
        // Crear los campos de la tabla
        builder.HasKey( d => new {d.OrderId, d.ProductId});
        builder.Property(d => d.UnitPrice).HasPrecision(8, 2);
        //builder.Property(d => d.Quantity)
    }
}

