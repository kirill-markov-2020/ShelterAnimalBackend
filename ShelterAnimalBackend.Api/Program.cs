using Microsoft.EntityFrameworkCore;
using ShelterAnimalBackend.Application.Services;
using ShelterAnimalBackend.Core.Interfaces;
using ShelterAnimalBackend.Core;
using ShelterAnimalBackend.Infrastructure.Data;
using ShelterAnimalBackend.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


builder.Services.AddAuthorization();

builder.Services.AddDbContext<AnimalShelterDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IAnimalRepository, AnimalRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAdoptionRepository, AdoptionRepository>();
builder.Services.AddScoped<IAdoptionApplicationRepository, AdoptionApplicationRepository>();
builder.Services.AddScoped<ITemporaryAccommodationRepository, TemporaryAccommodationRepository>();
builder.Services.AddScoped<AnimalService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ITypeAnimalRepository, TypeAnimalRepository>();
builder.Services.AddScoped<IAnimalStatusRepository, AnimalStatusRepository>();
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings"));

builder.Services.AddScoped<AdoptionService>();

builder.Services.AddScoped<AdoptionApplicationService>();

builder.Services.AddScoped<TemporaryAccommodationService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<AuthService>();

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["validIssuer"],
        ValidAudience = jwtSettings["validAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["securityKey"]))
    };
});



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();
app.UseCors(policy => policy
    .WithOrigins("http://localhost:3000") 
    .AllowAnyMethod()
    .AllowAnyHeader());
app.MapControllers();

app.Run();