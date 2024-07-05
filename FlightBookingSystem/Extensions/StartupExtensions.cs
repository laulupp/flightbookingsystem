using Backend.Persistence.Context;
using Backend.Persistence.Repositories;
using Backend.Persistence.Repositories.Interfaces;
using Backend.Services;
using Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Backend.Middleware;
using Serilog;

namespace Backend.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
    }

    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["ConnectionStrings:DefaultConnection"];
        services.AddDbContext<AppDbContext>(opts =>
            opts.UseNpgsql(connectionString));
    }

    public static void ConfigureRepositoryWrapper(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IFlightRepository, FlightRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<IAircraftRepository, AircraftRepository>();
        services.AddScoped<IAirportRepository, AirportRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<ICompanyRegistrationRequestRepository, CompanyRegistrationRequestRepository>();
        services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
    }

    public static void ConfigureServiceWrapper(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IFlightService, FlightService>();
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<IAircraftService, AircraftService>();
        services.AddScoped<IAirportService, AirportService>();
        services.AddSingleton<IPasswordEncryptionService, PasswordEncryptionService>();
        services.AddSingleton<ITokenService, TokenService>();
    }

    public static void ConfigureAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }

    public static void ConfigureCustomMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseMiddleware<AuthMiddleware>();
    }
}
