using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Interfaces.Repositories;
using Shop.Application.Interfaces.Services;
using Shop.Application.Mappings;
using Shop.Application.Services;
using Shop.Infrastructure.Data;
using Shop.Infrastructure.Repositories;
using Shop.Models.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add Connection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration
           .GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // password settings
    options.Password.RequiredLength = 8;
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;

    // lock settings
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);

    // User settings
    options.User.RequireUniqueEmail = true;
})
   .AddEntityFrameworkStores<AppDbContext>()
   .AddDefaultTokenProviders();

// ========== REPOSITORIES ==========
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();

// ========== SERVICES ==============
builder.Services.AddScoped<IAuthService,AuthService>();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

//Cookie settings
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Auth/Login";
    options.LogoutPath = "/Auth/Logout";
    options.AccessDeniedPath = "/Auth/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromDays(7);
});
// Make the controller look for views inside the Web folder
builder.Services.AddControllersWithViews()
    .AddRazorOptions(options =>
    {
        // MVC will now also look in Web/<Controller>/<View>.cshtml
        options.ViewLocationFormats.Add("/Web/Views/{1}/{0}.cshtml");
        options.ViewLocationFormats.Add("/Web/Views/Shared/{0}.cshtml"); // for shared layouts
    });

var app = builder.Build();

// Seed role after app is built
using (var scope = app.Services.CreateScope())
{
    var roleMaager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    string[] roles = { "Admin", "Customer" };

    foreach(var role in roles)
    {
        if (!await roleMaager.RoleExistsAsync(role))
            await roleMaager.CreateAsync(new IdentityRole(role));
    }
}

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
