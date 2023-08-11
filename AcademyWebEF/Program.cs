using AcademyWebEF.BusinessEntities;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Enabling Sessions State Management Technique
builder.Services.AddSession(); 

// Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                 .AddCookie(options =>
                 {
                     options.LoginPath = "/Account/Login";
                     options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                     options.SlidingExpiration = true;
                     options.AccessDeniedPath = "/Account/AccessDenied";
                 });


// Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(Roles.Admin, policy => policy.RequireRole(Roles.Admin));
    options.AddPolicy(Roles.Student, policy => policy.RequireRole(Roles.Student));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.MapControllerRoute(  // router patterns
   name: "default",
   pattern: "{controller=Account}/{action=Login}/{id?}"
);

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
