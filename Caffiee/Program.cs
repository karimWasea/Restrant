using DataAcessLayers;

using Microsoft.EntityFrameworkCore;
 using Microsoft.AspNetCore.Identity;
using Servess;
using Cf_Atomapper;
using Interfaces;
using Microsoft.AspNetCore.Identity.UI.Services;
using C_Utilities;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
IServiceCollection serviceCollection =
    builder.Services.AddDbContext<ApplicationDBcontext>(options =>
    options.UseSqlServer(connectionString));

 
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { new CultureInfo("en-US"), new CultureInfo("fr-FR") };
    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});
builder.Services.AddIdentity<Applicaionuser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
.AddEntityFrameworkStores<ApplicationDBcontext>().AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{
    // Customizing username validation to allow any characters
    options.User.AllowedUserNameCharacters = null;
});

// Replace the default IUserValidator with your custom validator
builder.Services.AddScoped<IUserValidator<Applicaionuser>, CustomUserValidator<Applicaionuser>>();


builder.Services.AddTransient<UnitOfWork>();
 builder.Services.AddTransient<lookupServess>();
builder.Services.AddTransient<FinancialUserCashHistoryServess>();
 
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
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});


builder.Services.ConfigureApplicationCookie(option =>
{

    option.AccessDeniedPath = $"/Identity/Account/AccessDenied";
    option.LogoutPath = $"/Identity/Account/Logout";
    option.LoginPath = $"/Identity/Account/Login";

});




builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SuperAdminOrSalesMan", policy =>
        policy.RequireRole(ConstsntValuse.SuperAdmin, ConstsntValuse.SalesMan));
});

// Add policy for SuperAdmin and SalesManager roles
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SuperAdminOrSalesManager", policy =>
        policy.RequireRole(ConstsntValuse.SuperAdmin, ConstsntValuse.SalessManger));
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SuperAdminOrSalesManagerSalesMan", policy =>
        policy.RequireRole(ConstsntValuse.SuperAdmin, ConstsntValuse.SalessManger , ConstsntValuse.SalesMan));
});
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
}
else
{
    app.UseExceptionHandler("/Home/Index");
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
app.UseCors("MyCorsPolicy");

app.Run();
