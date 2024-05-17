using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Paris2024.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Fh - Add Identity Service
builder.Services
    .AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();


builder.Services.AddControllersWithViews();

//builder.Services.AddTransient<IAdminOfferRepository, AdminOfferRepository>();
builder.Services.AddTransient<IAdminOfferTypeRepository, AdminOfferTypeRepository>();

var app = builder.Build();

// Fh - Seed Roles ans Admin profile
//using (var scope = app.Services.CreateScope())
//{
//    await DbSeederRoles.SeedDefaultData(scope.ServiceProvider);
//}

// Fh - Seed Models offer and offerType
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DbSeederOfferType.SeedOfferTypeData(scope.ServiceProvider);
    await DbSeederOffer.SeedOfferData(scope.ServiceProvider);
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
app.MapRazorPages();

app.Run();
