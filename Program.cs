using Microsoft.EntityFrameworkCore;

var url = Environment.GetEnvironmentVariable("DATABASE_URL");
Console.WriteLine("estamos conectado en " + url);

var builder = WebApplication.CreateBuilder(args);

// 1) REGISTRAR SERVICIOS ANTES DE Build
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddDbContext<RailwayContext>(options =>
    options.UseNpgsql(url)); // asegúrate de tener el paquete Npgsql EF Core

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Opcional: configurar URL del host (útil en Railway/containers)
builder.WebHost.UseUrls("http://0.0.0.0:8080");

// 2) Build
var app = builder.Build();

// 3) Migraciones / inicialización DB (con scope del app ya creado)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<RailwayContext>();
    db.Database.Migrate();
}

// 4) Middlewares — UseCors debe ir aquí (después de Build)
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
