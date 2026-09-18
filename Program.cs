using Microsoft.EntityFrameworkCore;
using HeroesWeb.Data;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages(options =>

{

    options.Conventions.AuthorizeFolder("/Heroes");

    options.Conventions.AuthorizeFolder("/SuperPoderes");

});

builder.Services.AddDbContext<HeroesContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("HeroesDb")
        ?? throw new InvalidOperationException("Falta la conexión HeroesDb.")));

var connectionString =

builder.Configuration.GetConnectionString("HeroesDb")

?? throw new InvalidOperationException(

"No se encontró la conexión HeroesDb.");

builder.Services.AddRazorPages();



builder.Services.AddDbContext<HeroesContext>(options =>

options.UseSqlServer(connectionString));



builder.Services.AddDbContext<ApplicationDbContext>(options =>

options.UseSqlServer(connectionString));



builder.Services.AddDefaultIdentity<IdentityUser>(options =>

{

    options.SignIn.RequireConfirmedAccount = false;


    options.User.RequireUniqueEmail = true;


    options.Password.RequiredLength = 8;

    options.Password.RequireDigit = true;

    options.Password.RequireLowercase = true;

    options.Password.RequireUppercase = true;

    options.Password.RequireNonAlphanumeric = true;

})

.AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapStaticAssets();
app.MapRazorPages().WithStaticAssets();
app.Run();