using Microsoft.EntityFrameworkCore;
using wypozyczalnia.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Dodaj usługi do kontenera.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<VehiclesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Devconnection")));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Devconnection")));

builder.Services.AddDbContext<RentalsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Devconnection")));

//builder.Services.AddScoped<IRentalInterface, RentalRepository>();

// Dodaj usługi Identity (bez JWT)
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// tu mamy nadawanie tokena
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
        ValidIssuer = "SuperSamochodziki", // zmień na swój issuer
        ValidAudience = "Administrator", // zmień na swój audience
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_super_secret_key_32_characters_long!")) // Klucz głowny tutaj
    };
});

// Konfiguracja Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "RentalApp API", Version = "v1" });
});

// Konfiguracja CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.WithOrigins("https://localhost:4200", "https://kind-forest-0308cdb03.5.azurestaticapps.net")
               .AllowAnyMethod()
               .AllowAnyHeader()
                .AllowCredentials();
    });
});

Console.WriteLine("Podlaczane do bazy");

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

//app.UseHttpsRedirection();

app.UseAuthentication(); // Dodaj uwierzytelnianie
app.UseAuthorization();

app.MapControllers();

//app.MapFallbackToFile("/index.html");

app.Run();
