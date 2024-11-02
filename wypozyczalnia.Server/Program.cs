using Microsoft.EntityFrameworkCore;
using wypozyczalnia.Server.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<VehiclesContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("Devconnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
<<<<<<< HEAD
        builder.WithOrigins("http://localhost:4200", "https://storageforrentalfront.z36.web.core.windows.net")
=======
        builder.WithOrigins("http://localhost:4200", "https://kind-forest-0308cdb03.5.azurestaticapps.net")
>>>>>>> master
               .AllowAnyMethod()
               .AllowAnyHeader();

    });
});

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.MapFallbackToFile("/index.html");

app.Run();
