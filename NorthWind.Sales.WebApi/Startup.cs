namespace NorthWind.Sales.WebApi;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using NorthWind.Sales.Backend.BusinessObjects.Interfaces.Repositories;
using NorthWind.Sales.Backend.DataContext.EFCore.Options;
using NorthWind.Sales.Backend.Ioc;

using Swashbuckle.AspNetCore;
using System.Data;

// Clase auxiliar para configurar los servicios del proyecto.
internal static class Startup
{
    public static WebApplication CreateWebApplication(this WebApplicationBuilder builder)
    {
        // Configurar el componente que permite explorar Web API en el navegador usando Swagger 
        builder.Services.AddEndpointsApiExplorer();


        // Agregar el componente que construye los objetos para documentar los endpoints de Swagger
        // con la funcionalidad de ApiExplorer
        // Genera la documentación de Swagger/OpenAPI. Permite probar la API desde el navegador.
        builder.Services.AddSwaggerGen();

        // Registrar los servicios de la aplicación
        builder.Services.AddNorthWindSalesServices(dbOptions => builder.Configuration
        .GetSection(DBOptions.SectionKey)
        .Bind(dbOptions));
        builder.Services.AddScoped<IDbConnection>(sp =>
        {
            var dbOptions = sp.GetRequiredService<IOptions<DBOptions>>().Value;
            var connection = new SqlConnection(dbOptions.ConnectionString);
            connection.Open(); // opcional
            return connection;
        });

        
        // Agregar el soport para CORS para que clientes que se ejecutan en el navegador Web
        // (como por ejemplo: React, Vue, Angular, Svelte, Solid, Next, Blazor, etc) puedan
        // utilizar la Web API
        builder.Services.AddCors(options => 
        {
            options.AddDefaultPolicy(config =>
            {
                config.AllowAnyMethod();
                config.AllowAnyHeader();
                config.AllowAnyOrigin();
            });
        });

        return builder.Build();
    }

    public static WebApplication ConfigureWebApplication(this WebApplication app)
    {
        // Habilitar el middeware para exponer el documento JSON
        // generado y la interfaz UI de Swagger si se esta en modo de desarrollo

        // Solo si está en modo Desarrollo se habilita Swagger.
        if (app.Environment.IsDevelopment())
        {
            // expone el JSON de la especificación OpenAPI. 
            app.UseSwagger();
            /// proporciona la interfaz visual en / swagger. "Muestra la página de Swagger"
            app.UseSwaggerUI();
        }

        // Registrar los endpoints del proyecto
        app.MapNorthWindSalesEndPoints();

        // Agregar el Middleware CORS
        app.UseCors();

        return app;
    }
}
