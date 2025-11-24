using Microsoft.Extensions.DependencyInjection;
using NorthWind.Sales.Backend.DataContext.EFCore;
using NorthWind.Sales.Backend.DataContext.EFCore.Options;
using NorthWind.Sales.Backend.Presenters;
using NorthWind.Sales.Backend.Repositories;
using NorthWind.Sales.Backend.UseCases;

namespace NorthWind.Sales.Backend.Ioc;

public static class DependencyContainer
{
    public static IServiceCollection AddNorthWindSalesServices (this IServiceCollection services,
        Action<DBOptions> configureDBOptions)
    {
        // Agregar cada uno de los servicios al contenedor global para consolidar los servicios
        // que expone cada uno de los proyectos de la solución Backend.
        // Esto facilita el registro de dependencias en las aplicaciones cliente, por ejemplo
        // en una Web API, app de Consola,app Windows Forms, MVC, MUI.
        services.AddUseCaseServices()
            .AddRepositories()
            .AddDataContexts(configureDBOptions)
            .AddPresenters();

        return services;
    }
}
