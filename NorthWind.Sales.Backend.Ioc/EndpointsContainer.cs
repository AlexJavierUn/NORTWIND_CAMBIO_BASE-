using Microsoft.AspNetCore.Builder;
//using System.Runtime.CompilerServices;

namespace NorthWind.Sales.Backend.Ioc;

// Esta clase permite registrar TODOS los endpoints del proyecto.
public static class EndpointsContainer 
{
    public static WebApplication MapNorthWindSalesEndPoints(this WebApplication app)
    {
        // Registrar el método "GET" del controlador para que sea visible fuera del proyecto
        // y se pueda consumir por una cliente.
        // GET, POST, PUT, DELETE, 
        app.UseCreateOrderController();
        return app;
    }
}
