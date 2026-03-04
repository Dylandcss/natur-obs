using Microsoft.EntityFrameworkCore;
using TravelLogService.Data;
using TravelLogService.Repositories;
using TravelLogService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string ConnectionString = builder.Configuration.GetConnectionString("default");

builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString)));
builder.Services.AddScoped<ITravelLogRepository, TravelLogRepository>();
builder.Services.AddScoped<ITravelLogService, ServiceTravelLog>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();