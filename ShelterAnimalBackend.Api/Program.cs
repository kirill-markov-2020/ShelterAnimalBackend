using Microsoft.EntityFrameworkCore;
using ShelterAnimalBackend.Application.Services;
using ShelterAnimalBackend.Core.Interfaces;
using ShelterAnimalBackend.Infrastructure.Data;
using ShelterAnimalBackend.Infrastructure.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add DbContext
builder.Services.AddDbContext<AnimalShelterDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IAnimalRepository, AnimalRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAdoptionRepository, AdoptionRepository>();
builder.Services.AddScoped<IAdoptionApplicationRepository, AdoptionApplicationRepository>();
builder.Services.AddScoped<ITemporaryAccommodationRepository, TemporaryAccommodationRepository>();
builder.Services.AddScoped<AnimalService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AdoptionService>();
builder.Services.AddScoped<AdoptionApplicationService>();

builder.Services.AddScoped<TemporaryAccommodationService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors(policy => policy
    .WithOrigins("http://localhost:3000") // React app address
    .AllowAnyMethod()
    .AllowAnyHeader());
app.MapControllers();

app.Run();