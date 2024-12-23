using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Store.API.Dtos;
using Store.API.Helper;
using Store.Data.Context;
using Store.Data.Entity;
using Store.Repository.InterFace;
using Store.Repository.Repository;
using Store.Repository.SeedData;

namespace Store.API
{
    public class Program
	{
        [Obsolete]
        public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);


			#region configuration services
			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			
			/// Connecting to DataBase
			builder.Services.AddDbContext<StoreDbContext>(Options =>
				{
					Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
				});
			//builder.Services.AddAutoMapper(m => m.AddProfile(new MappingProfile()));
			builder.Services.AddAutoMapper(typeof(MappingProfile));
            #endregion configuration services

            #region Allow Dependancy injection
            builder.Services.AddScoped(typeof(IGenaricRepo<>), typeof(GenaricRepo<>));
			#endregion

			var app = builder.Build();
			//to update database by defult
		    var scope = app.Services.CreateScope();
			var Services = scope.ServiceProvider;
			var _dbContext = Services. GetRequiredService<StoreDbContext>();
			// exption looger 
			var loggerFactory = Services.GetRequiredService<ILoggerFactory>();


			try
			{
			    await _dbContext.Database.MigrateAsync(); //apply migration
				await StoreContextSeed.SeedAsync(_dbContext);//apply Seeding
            }
			catch (Exception Ex)
			{

				var logger = loggerFactory.CreateLogger<Program>();
				logger.LogError(Ex, "Error in Migration");
			}
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseStaticFiles();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
