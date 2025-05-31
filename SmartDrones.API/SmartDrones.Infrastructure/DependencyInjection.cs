using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartDrones.Domain.Interfaces;
using SmartDrones.Infrastructure.Data;
using SmartDrones.Infrastructure.Repositories;
using SmartDrones.Application.Interfaces;
using SmartDrones.Application.Services;
using AutoMapper;
using SmartDrones.Application.Mappers;

namespace SmartDrones.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SmartDronesDbContext>(options =>
                options.UseOracle(configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(SmartDronesDbContext).Assembly.FullName)));

            services.AddScoped<IDroneRepository, DroneRepository>();
            services.AddScoped<ISensorDataRepository, SensorDataRepository>();
            services.AddScoped<IAlertRepository, AlertRepository>();

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            services.AddScoped<IDroneService, DroneService>();
            services.AddScoped<ISensorDataService, SensorDataService>();
            services.AddScoped<IAlertService, AlertService>();

            return services;
        }
    }
}