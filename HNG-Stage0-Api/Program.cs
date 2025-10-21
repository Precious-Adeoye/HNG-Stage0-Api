using HNG_Stage0_Api.Data;
using Microsoft.EntityFrameworkCore;

namespace HNG_Stage0_Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register SQLite DB
            builder.Services.AddDbContext<AppDbContext>(options =>
              options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
            builder.WebHost.UseUrls($"http://0.0.0.0:{port}");     

            builder.Services.AddHealthChecks();

            // Add services to the container.
            builder.Services.AddControllers()
             .AddJsonOptions(options =>
             {
                 options.JsonSerializerOptions.WriteIndented = true;
             });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHttpClient();
     

            var app = builder.Build();

            app.UseHealthChecks("/health");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "HNG Stage 0 API v1");
                    c.RoutePrefix = string.Empty; // makes Swagger open at root URL
                });
            }



            app.UseAuthorization();
 

            app.MapControllers();

            app.MapGet("/", () => "🚀 API is running on PXXL App successfully!");

            // Log successful startup
            Console.WriteLine($"✅ Server running on port {port} in {app.Environment.EnvironmentName} mode");


            try
            {
                app.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Application failed to start: " + ex);
                throw;
            }
        }
    }
}
