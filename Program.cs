using AutoMapper;
using Store.API.Extensions;
using Store.API.Dtos;
using Store.API.Helper;
using Store.Data.Context;
using Store.Repository.SeedData;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Store.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Set up the web application builder
            var builder = WebApplication.CreateBuilder(args);

            #region Configuration Services
            // Add application-specific services using the extension method
            builder.Services.AddApplicationServices();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            #endregion Configuration Services

            // Build the app
            var app = builder.Build();

            // Apply database migrations and seed data
            var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var dbContext = services.GetRequiredService<StoreDbContext>();
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                // Apply database migrations
                await dbContext.Database.MigrateAsync();

                // Seed initial data
                await StoreContextSeed.SeedAsync(dbContext);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "Error occurred while migrating the database.");
            }

            #region Middleware Configuration
            if (app.Environment.IsDevelopment())
            {
                // Enable Swagger UI for development
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // General middleware setup
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthorization();
            app.MapControllers();
            #endregion Middleware Configuration

            // Run the application
            app.Run();
        }
    }
}
