using Microsoft.EntityFrameworkCore;
using ShelterAnimalBackend.Application.Services;
using ShelterAnimalBackend.Core.Interfaces;
using ShelterAnimalBackend.Core;
using ShelterAnimalBackend.Infrastructure.Data;
using ShelterAnimalBackend.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

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
builder.Services.AddScoped<ITypeAnimalRepository, TypeAnimalRepository>();
builder.Services.AddScoped<IAnimalStatusRepository, AnimalStatusRepository>();

builder.Services.AddScoped<AnimalService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AdoptionService>();
builder.Services.AddScoped<AdoptionApplicationService>();
builder.Services.AddScoped<TemporaryAccommodationService>();
builder.Services.AddScoped<AuthService>();


builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

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


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShelterAnimalBackend API", Version = "v1" });
    c.IgnoreObsoleteActions();
    c.DocInclusionPredicate((docName, apiDesc) =>
    {
        if (apiDesc.ActionDescriptor.RouteValues.ContainsKey("controller"))
        {
            var controllerName = apiDesc.ActionDescriptor.RouteValues["controller"];
            return controllerName != "FileUpload";
        }
        return true;
    });
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShelterAnimalBackend API V1");
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication(); 
app.UseAuthorization();
app.UseCors(policy => policy
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.MapControllers();

app.Run();