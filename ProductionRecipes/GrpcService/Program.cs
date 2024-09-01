using ProductionRecipes.Application;
using ProductionRecipes.Contracts;
using ProductionRecipes.DataAccess;
using ProductionRecipes.DataAccess.Contexts;
using ProductionRecipes.DataAccess.Repositories.AccionElements;
using ProductionRecipes.DataAccess.Repositories.Products;
using ProductionRecipes.DataAccess.Repositories.Recipes;
using ProductionRecipes.Services.Services;

namespace ProductionRecipes.Services
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Additional configuration is required to successfully run gRPC on macOS.
            // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

            // Add services to the container.
            builder.Services.AddGrpc();
          

            builder.Services.AddSingleton("ProductionDB.sqlite");
            builder.Services.AddScoped<ApplicationContext>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IAccionElementRepository, AccionElementRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();


            //builder.Services.AddScoped<IPriceRepository, ApplicationRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.MapGrpcService<FaseService>();
            app.MapGrpcService<OperationService>();
            app.MapGrpcService<ProductService>();
            app.MapGrpcService<RecipeService>();

            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}
