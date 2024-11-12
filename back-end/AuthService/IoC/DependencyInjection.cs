using AuthService.Data;
using AuthService.Data.Context;
using AuthService.Data.Repos;
using AuthService.Domain.Contracts;
using AuthService.Domain.Contracts.Repos;
using AuthService.Domain.VMs;
using Microsoft.EntityFrameworkCore;

namespace AuthService.IoC
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

            services.AddDbContext<AuthContext>(
              options => options.UseMySql(connectionString,
                                           ServerVersion.AutoDetect(connectionString),
                                             x =>
                                             {
                                                 x.MigrationsAssembly(typeof(AuthContext).Assembly.FullName);
                                             }).LogTo(Console.WriteLine, LogLevel.Information));
            #endregion

            #region Repository pattern
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion

            #region Repos
            services.AddScoped<IUserRepo, UserRepo>();
            #endregion

            #region Mediatr
            services.AddMediatR(options => options.RegisterServicesFromAssembly(AppDomain.CurrentDomain.Load("AuthService")));
            #endregion

            #region AutoMapper
            services.AddAutoMapper(typeof(MapProfile));
            #endregion

            return services;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var ctx = scope.ServiceProvider.GetRequiredService<AuthContext>();

            ctx.Database.Migrate();

            return app;
        }
    }
}
