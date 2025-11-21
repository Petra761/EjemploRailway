using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
var url = Environment.GetEnvironmentVariable("DATABASE_URL");
Console.WriteLine("estamos conectado en" + url);

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<RailwayContext>(options => options.UseNpgsql(url));

// Add services to the container.
builder.WebHost.UseUrls("http://0.0.0.0:8080"); // Configurar la aplicaci�n para escuchar en el puerto 8080

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<RailwayContext>();
    db.Database.Migrate();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
