namespace NorthWind.Sales.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var builder = WebApplication.CreateBuilder(args);
            //var app = builder.Build();

            //app.MapGet("/", () => "Hello World!");

            //app.Run();

            WebApplication.CreateBuilder(args)
                .CreateWebApplication()
                .ConfigureWebApplication()
                .Run();
        }
    }
}
