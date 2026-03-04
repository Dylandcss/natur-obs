using Microsoft.EntityFrameworkCore;
using ObservationService.Data;
using ObservationService.Repositories;
using ObservationService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string ConnectionString = builder.Configuration.GetConnectionString("default");

builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString)));
builder.Services.AddScoped<IObservationRepository, ObservationRepository>();
builder.Services.AddScoped<IObservationService, ServiceObservation>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();