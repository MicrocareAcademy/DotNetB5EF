using AcademyWebEF.BusinessEntities;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net.Mail;
using System.Net;
using AcademyWebEF.Services;

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

//Fluent Email
string email = "", password = "";

builder.Services.AddFluentEmail(email, "Microcare Academy")
                .AddSmtpSender(new SmtpClient("smtp.office365.com", 587)
                {
                    UseDefaultCredentials = false,
                    EnableSsl = true,
                    Credentials = new NetworkCredential(email, password)
                });

var app = builder.Build();

// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment())
{
    //This middleware catches exceptions thrown in production environment. 
    app.UseExceptionHandler("/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    //This middleware is used reports app runtime errors in development environment.  
    app.UseDeveloperExceptionPage();
}

//This middleware is used to redirects HTTP requests to HTTPS. 
app.UseHttpsRedirection();

//This middleware is used to returns static files and short-circuits further request processing.  
app.UseStaticFiles();

app.UseSession();

//This middleware is used to route requests. 
app.UseRouting();

// router patterns
app.MapControllerRoute(
   name: "default",
   pattern: "{controller=Account}/{action=Login}/{id?}"
);

//This middleware is used to authenticate a user
//It looks at who's trying to access your site and checks if they're legit (authenticated) or not
app.UseAuthentication();

//This middleware is used to authorizes a user to access secure resources. 
app.UseAuthorization();

//This middleware is used to add Razor Pages endpoints to the request pipeline.
app.MapRazorPages();

app.Run();
