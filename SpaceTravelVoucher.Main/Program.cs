
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SpaceTravelVoucher.DataSpaceTravel.Models;
using SpaceTravelVoucher.Main.Data;
using SpaceTravelVoucher.Main.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AppUserDbContextConnection") ?? throw new InvalidOperationException("Connection string 'AppUserDbContextConnection' not found.");

builder.Services.AddDbContext<AppUserDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppUserDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireUppercase = false;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints => {
    endpoints.MapRazorPages();
});

app.Run();
