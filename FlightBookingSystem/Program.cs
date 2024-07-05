using Backend.Extensions;
using Microsoft.AspNetCore.Builder;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureCors();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryWrapper();
builder.Services.ConfigureServiceWrapper();
builder.Services.ConfigureAutoMapper();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

var app = builder.Build();

// Configure the HTTP request pipeline.


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.ConfigureCustomMiddleware();

app.MapControllers();

Log.Information("Starting up the service");
app.Run();
