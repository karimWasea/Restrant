using DataAcessLayers;

using Microsoft.EntityFrameworkCore;
 using Microsoft.AspNetCore.Identity;
using Servess;
using Cf_Atomapper;
using Interfaces;
using Microsoft.AspNetCore.Identity.UI.Services;
using C_Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
IServiceCollection serviceCollection =
    builder.Services.AddDbContext<ApplicationDBcontext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddIdentity<Applicaionuser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
.AddEntityFrameworkStores<ApplicationDBcontext>().AddDefaultTokenProviders();

builder.Services.AddTransient<UnitOfWork>();
//builder.Services.AddTransient<ICustomerTypeServess>();
builder.Services.AddTransient<lookupServess>();
//builder.Services.AddTransient<CustomerType>();
//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDBcontext>();
builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddScoped<NotPayedmoneyHistoryServess>();
builder.Services.AddTransient<CategoryServess>();
builder.Services.AddTransient<ProductService>();
 builder.Services.AddRazorPages();
builder.Services.AddScoped<IEmailSender, Emailsender>();
builder.Services.AddTransient<Imgoperation>();
builder.Services.AddTransient<PriceProductebytypesServess>();
builder.Services.AddTransient<ApplicationUserService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddAutoMapper(typeof(Program));


builder.Services.ConfigureApplicationCookie(option =>
{

    option.AccessDeniedPath = $"/Identity/Account/AccessDenied";
    option.LogoutPath = $"/Identity/Account/Logout";
    option.LoginPath = $"/Identity/Account/Login";

});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Index");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
builder.Services.AddDistributedMemoryCache();
app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

app.MapControllerRoute(
       name: "Home",
       pattern: "{area=Admin}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
