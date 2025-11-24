using NorthWind.Sales.Backend.BusinessObjects.Aggregates;
using NorthWind.Sales.Backend.Repositories.Interfaces;
using NorthWind.Sales.Backend.Repositories.Entities;
internal class CommandsRepository(INorthWindSalesCommandDataContext context) : ICommandsRepository
{
    public async Task CreateOrder(OrderAggregate order)
    {
        await context.AddOrderAsync(order);

        await context.AddOrderDetailsAsync(
            order.OrderDetails
            .Select(d => new OrderDetail
            {
                Order = order,
                ProductId = d.ProductId,
                Quantity = d.Quantity,
                UnitPrice = d.UnitPrice
            }).ToArray());
    }

    public async Task SaveChanges() => await context.SaveChangesAsync();
}
