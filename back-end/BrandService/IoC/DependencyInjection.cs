using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BrandService.Data;
using BrandService.Data.Context;
using BrandService.Data.Repos;
using BrandService.Domain.Contracts;
using BrandService.Domain.Contracts.Context;
using BrandService.Domain.Contracts.Repos;
using BrandService.Domain.Contracts.Services;
using BrandService.Domain.ViewModels;

namespace BrandService.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            #region Conexão com MySql
            var host = config["DB:HOST"];
            var port = config["DB:PORT"];
            var user = config["DB:USER"];
            var password = config["DB:PASSWORD"];
            var database = config["DB:DATABASE"];

            var connectionString = $"server={host};port={port};database={database};userid={user};pwd={password}";

            services.AddDbContext<BrandsContext>(
              options => options.UseMySql(connectionString,
                                           ServerVersion.AutoDetect(connectionString),
                                             x =>
                                             {
                                                 x.MigrationsAssembly(typeof(BrandsContext).Assembly.FullName);
                                             }).LogTo(Console.WriteLine, LogLevel.Information));
            #endregion

            #region Repository pattern
            services.AddScoped<IDataContext, BrandsContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion

            #region Repos
            services.AddScoped<IBrandRepo, BrandRepo>();
            #endregion

            #region Mediatr
            services.AddMediatR(x => x.RegisterServicesFromAssembly(AppDomain.CurrentDomain.Load("BrandService")));
            #endregion

            #region AutoMapper
            services.AddAutoMapper(typeof(MapProfile));
            #endregion

            #region Auth
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.SaveToken = true;
                        options.TokenValidationParameters = GetValidationParameters();
                    });
            #endregion

            #region IntegrationServices
            services.AddHttpClient();
            services.AddHttpContextAccessor();
            #endregion


            return services;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var ctx = scope.ServiceProvider.GetRequiredService<BrandsContext>();

            ctx.Database.Migrate();

            return app;
        }

        private static TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = false,
                RequireExpirationTime = true,

                ValidIssuer = "br.com",
                ValidAudience = "br.com",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("V3h!cLe@2os3TesT")),
            };
        }
    }
}
