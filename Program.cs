using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Data;
using LibraryManagement.Areas.Identity.Data;
using LibraryManagement.Database;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<LibraryManagementContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("AuthenticationDatabase"), new MySqlServerVersion(new Version(8, 0, 34))));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("ApplicationDatabase"), new MySqlServerVersion(new Version(8, 0, 34))));

builder.Services.AddDefaultIdentity<LibraryManagementUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<LibraryManagementContext>();

builder.Services.Configure<IdentityOptions>(options =>
    options.Password.RequireUppercase = true
);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();


using (var scope = app.Services.CreateScope())
{
    var roleManager =
        scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roles = new[] { "Admin", "Member" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

using (var scope = app.Services.CreateScope())
{
    var userManager =
        scope.ServiceProvider.GetRequiredService<UserManager<LibraryManagementUser>>();

    string email = "admin@admin.com";
    string password = "Test1234,";
    string firstname = "Admin";
    string lastname = "Admin";

    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new LibraryManagementUser();
        user.UserName = email;
        user.Email = email;
        user.FirstName = firstname;
        user.LastName = lastname;

        await userManager.CreateAsync(user, password);

        await userManager.AddToRoleAsync(user, "Admin");
    }


}
app.Run();
