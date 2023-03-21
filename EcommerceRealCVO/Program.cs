using EcommerceRealCVO.Datos;
using EcommerceRealCVO.Servicios;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Configuraci�n a SQL SERVER

builder.Services.AddDbContext<AplicationDBContext>(opciones => opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSQL")));


//Agregar el servicio a la aplicaci�n 
//Se coloca al usuario y al rol pues es lo que se requiere para el uso de Identity 
//Inyecci�n de independencia
//Se a�ade el AddDefaultTokenProviders -> Para generar un link de acceso a la recuperaci�n de contrase�a
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AplicationDBContext>().AddDefaultTokenProviders();

//Esto es para el URL y el retroceso 
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = new PathString("/Cuentas/Acceso");
});

//Opciones de configuraci�n para Identity, exclusivo para administraci�n de usuarios

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireLowercase = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);
    options.Lockout.MaxFailedAccessAttempts = 3;
});

//EmailSender -> Para el env�o de correos electr�nicos
//Se manda a llmar la clase creada dentro de la Carpeta de Servicios para traer los param�tros de MailSender
builder.Services.AddTransient<IEmailSender, MailJetSender>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
//Se agrega la autenticaci�n
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");


app.Run();
