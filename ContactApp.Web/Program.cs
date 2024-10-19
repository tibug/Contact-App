using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using ContactApp.DataAccess.Data;
using ContactApp.DataAccess.IRepository;
using ContactApp.DataAccess.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 52428800;
    options.ValueLengthLimit = int.MaxValue;
    options.MultipartHeadersLengthLimit = int.MaxValue;
});
builder.Services.AddAntiforgery(options => options.HeaderName = "X-CSRF-TOKEN");
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowGitHubCDN",
        builder => builder.WithOrigins("https://raw.githubusercontent.com"));
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
{
    option.LoginPath = "/login/Index";
    option.ExpireTimeSpan = TimeSpan.FromDays(7);
    option.LogoutPath = "/login/Logout";
}

);




// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(o => o.UseSqlServer(
        builder.Configuration.GetConnectionString("Conn")
    )
);

builder.Services.AddScoped<IAuthentication, Authentication>();
builder.Services.AddScoped<IUsersEntry, UsersEntry>();
//builder.Services.AddScoped<IContact, Contact>();
builder.Services.AddScoped<IContactRepository>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("Conn");
    var logger = provider.GetRequiredService<ILogger<ContactRepository>>();
    return new ContactRepository(connectionString, logger);
});
builder.Services.AddScoped<ICompanyRepository>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("Conn");
    var logger = provider.GetRequiredService<ILogger<CompanyRepository>>();
    return new CompanyRepository(connectionString, logger);
});
builder.Services.AddScoped<ILookupRepository>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("Conn");
    var cache = provider.GetRequiredService<IMemoryCache>();
    var logger = provider.GetRequiredService<ILogger<LookupRepository>>();
    return new LookupRepository(connectionString, cache, logger);
});

builder.Services.AddNotyf(config => { config.DurationInSeconds = 3; config.IsDismissable = true; config.Position = NotyfPosition.TopRight; });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseNotyf();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("AllowGitHubCDN");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");
ApplyMigration();
app.Run();


void ApplyMigration()
{
    using (var scope = app.Services.CreateScope())
    {
        var _db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (_db.Database.GetPendingMigrations().Count() > 0)
        {
            _db.Database.Migrate();
        }
    }
}