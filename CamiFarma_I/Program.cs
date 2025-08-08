using CamiFarma_I.Data;
using CamiFarma_I.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Leer variables de entorno
string sqlServer = Environment.GetEnvironmentVariable("SQL_SERVER") ?? "localhost";
string sqlUser = Environment.GetEnvironmentVariable("SQL_USER") ?? "sa";
string sqlPass = Environment.GetEnvironmentVariable("SQL_PASS") ?? "";

// Construir la cadena de conexión
string connectionString = $"Server={sqlServer};Database=FarmaciaDB;User Id={sqlUser};Password={sqlPass};Encrypt=False;TrustServerCertificate=True;";

// Registrar DbContext con SQL Server
builder.Services.AddDbContext<FarmaciaContext>(options =>
    options.UseSqlServer(connectionString)
);

// Registrar servicios
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddScoped<ProductoService>();

var app = builder.Build();

// Configuración del pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
