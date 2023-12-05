using CompartilhamentoDeDicasSustentaveis.Data;
using CompartilhamentoDeDicasSustentaveis.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection"));
});

//Marcoratti fez assim
builder.Services.AddIdentity<UsuarioIdentity, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>(); //Configurando o Identity

//o chatgpt me recomendou fazer assim para que eu conseguisse tirar a necessidade de confirmar o email
//tamb�m aicionei esse AddDefaultTokenProviders
/*
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
}).AddEntityFrameworkStores<ApplicationDbContext>();
*/



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

app.UseAuthentication();


app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
