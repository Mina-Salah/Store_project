using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Store.Data.Context;
using Store.Repository.InterFace;
using Store.Repository.Repository;
using Store.Service.Services.product;
using Store.API.Helper;

namespace Store.API.Extensions
{
    public static class ServiceExtensions
    {
        /// Extension method to add required services to the container.
        public static void AddApplicationServices(this IServiceCollection services)
        {
            // Add DbContext with SQL Server connection
            services.AddDbContext<StoreDbContext>(options =>
                options.UseSqlServer(services.BuildServiceProvider().GetRequiredService<IConfiguration>().GetConnectionString("DefaultConnection")));

            // Add AutoMapper with MappingProfile
            services.AddAutoMapper(typeof(MappingProfile));

            // Add repositories and services for dependency injection
            services.AddScoped(typeof(IGenaricRepo<>), typeof(GenaricRepo<>));

            // Add application-specific services here
        }
    }
}
