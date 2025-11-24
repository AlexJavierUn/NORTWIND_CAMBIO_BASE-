namespace NorthWind.Sales.Backend.DataContext.EFCore.Services;

internal class NorthWindSalesCommandsDataContext : INorthWindSalesCommandDataContext
{
    private readonly IDbConnection _connection;
    private IDbTransaction? _transaction; 

    public NorthWindSalesCommandsDataContext(IOptions<DBOptions> dbOptions)
    {
        _connection = new NpgsqlConnection(dbOptions.Value.ConnectionString);
        _connection.Open();
        _transaction = null;
    }

    public async Task AddOrderAsync(Order order)
    {
        if (order == null) throw new ArgumentNullException(nameof(order));

        var sql = @"
    INSERT INTO ""Orders"" (
        ""CustomerId"", ""ShipAddress"", ""ShipCity"", ""ShipCountry"", ""ShipPostalCode"",
        ""ShippingType"", ""DiscountType"", ""Discount"", ""OrderDate""
    )
    VALUES (
        @CustomerId, @ShipAddress, @ShipCity, @ShipCountry, @ShipPostalCode,
        @ShippingType, @DiscountType, @Discount, @OrderDate
    )
    RETURNING ""Id"";";


        var orderId = await _connection.QuerySingleAsync<int>(sql, new
        {
            CustomerId = order.CustomerId,
            ShipAddress = order.ShipAddress,
            ShipCity = order.ShipCity ?? "",
            ShipCountry = order.ShipCountry ?? "",
            ShipPostalCode = order.ShipPostalCode,
            ShippingType = (int)order.ShippingType,
            DiscountType = (int)order.DiscountType,
            Discount = order.Discount,
            OrderDate = order.OrderDate
        }, _transaction);

        order.Id = orderId;
    }

    public async Task AddOrderDetailsAsync(IEnumerable<OrderDetail> orderDetails)
    {
        if (orderDetails == null) throw new ArgumentNullException(nameof(orderDetails));

        var sql = @"
           INSERT INTO ""OrderDetails"" (""OrderId"", ""ProductId"", ""Quantity"", ""UnitPrice"")
            VALUES (@OrderId, @ProductId, @Quantity, @UnitPrice);";

        foreach (var detail in orderDetails)
        {
            await _connection.ExecuteAsync(sql, new
            {
                OrderId = detail.Order.Id,
                ProductId = detail.ProductId,
                Quantity = detail.Quantity,
                UnitPrice = detail.UnitPrice
            }, _transaction);
        }
    }

    public Task SaveChangesAsync()
    {
        if (_transaction == null)
        {
            _transaction = _connection.BeginTransaction();
        }

        try
        {
            _transaction.Commit();
        }
        catch
        {
            _transaction.Rollback();
            throw;
        }
        finally
        {
            _transaction.Dispose();
            _transaction = null;
        }

        return Task.CompletedTask;
    }


    public IDbConnection GetDbConnection() => _connection;
}

