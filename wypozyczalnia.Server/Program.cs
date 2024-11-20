using Microsoft.EntityFrameworkCore;
using wypozyczalnia.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using wypozyczalnia.Server.Interfaces;
using wypozyczalnia.Server.Repositories;
using wypozyczalnia.Server.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRentalInterface, RentalRepository>();
builder.Services.AddSingleton<RabbitListener>();

builder.Services.AddDbContext<VehiclesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Devconnection")));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Devconnection")));
builder.Services.AddDbContext<RentalsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Devconnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

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

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "RentalApp API", Version = "v1" });
});

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

var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");
//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
//app.MapFallbackToFile("/index.html");
app.UseRabbitListener();
app.Run();

public static class ApplicationBuilderExtensions
{
    private static RabbitListener? _listener;

    public static IApplicationBuilder UseRabbitListener(this IApplicationBuilder app)
    {
        _listener = app.ApplicationServices.GetService<RabbitListener>();

        var lifetime = app.ApplicationServices.GetService<IHostApplicationLifetime>();

        // Register event for when the application has started
        lifetime?.ApplicationStarted.Register(OnStarted);

        // Register event for when the application is stopping
        lifetime?.ApplicationStopping.Register(OnStopping);

        return app;
    }

    private static void OnStarted()
    {
        _listener?.Register();
    }

    private static void OnStopping()
    {
        _listener?.Deregister();
    }
}