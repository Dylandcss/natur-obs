using Microsoft.EntityFrameworkCore;
using SpeciesService.Data;
using SpeciesService.Repositories;
using SpeciesService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string ConnectionString = builder.Configuration.GetConnectionString("default");

builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString)));
builder.Services.AddScoped<ISpeciesRepository, SpeciesRepository>();
builder.Services.AddScoped<ISpeciesService, ServiceSpecies>();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();