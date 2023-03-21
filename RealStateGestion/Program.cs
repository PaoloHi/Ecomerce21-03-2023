using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RealStateGestion.Datos;

var builder = WebApplication.CreateBuilder(args);

//Configuración a SQL SERVER

builder.Services.AddDbContext<AplicationDBContext>(opciones => opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSQL")));

//Agregar el servicio a la aplicación 
//Se coloca al usuario y al rol pues es lo que se requiere para el uso de Identity 
//Inyección de independencia
//Se añade el AddDefaultTokenProviders -> Para generar un link de acceso a la recuperación de contraseña
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AplicationDBContext>().AddDefaultTokenProviders();



// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Privacy}/{id?}");

app.Run();
