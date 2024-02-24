using System.Runtime.InteropServices.ComTypes;
using System.Text.Json.Serialization;
using Clone_Main_Project_0710;
using Clone_Main_Project_0710.Hubs;
using Clone_Main_Project_0710.Models;
using Clone_Main_Project_0710.Repository;
using HHSocialNetwork_Project.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("MyConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<SocialContext>(options =>
options.UseMySQL(connectionString));

builder.Services.AddScoped<IRepository<User>, UsersRepository>();
builder.Services.AddScoped<IRepository<UserImage>, UserImagesRepository>();
builder.Services.AddScoped<IRepository<UserFriend>, UserFriendsRepository>();
builder.Services.AddScoped<IRepository<UserPost>, UserPostsRepository>();
builder.Services.AddScoped<IRepository<Notification>, NotifitcationRepository>();
builder.Services.AddScoped<IRepository<UserMessage>, UserMessagesRepository>();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Users/Index";
                    options.LogoutPath = "/Users/LogoutUser";
                    options.Cookie.SameSite = SameSiteMode.None;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    options.Cookie.IsEssential = true;
                });

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSignalR();

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
app.UseAuthorization();
app.UseSession();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// app.MapControllerRoute(
//     name: "CustomRoute",
//     pattern: "{Controller}/{action}/{title?}/{id?}/{name?}"
// );
// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Users}/{action=Profile}/{id?}");

// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Searchs}/{action=All}/{userId?}");

app.MapHub<StockHub>("stock-hub");

app.Run();
