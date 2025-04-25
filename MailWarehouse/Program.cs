using FluentValidation.AspNetCore;
using FluentValidation;
using MailWarehouse.Application.Interfaces;
using MailWarehouse.Application.Services;
using MailWarehouse.Application.Validators;
using MailWarehouse.Domain.Interfaces;
using MailWarehouse.Infrastructure.Repositories;
using MailWarehouse.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IPackageRepository, PackageRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPackageService, PackageService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddDbContext<PostalDeliveryDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()
            .MigrationsAssembly("MailWarehouse")
    );
#if DEBUG
    options.LogTo(Console.WriteLine, LogLevel.Information);
    options.EnableSensitiveDataLogging();
#endif
});

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<PackageValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UserValidator>();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization(options =>
        options.DataAnnotationLocalizerProvider = (type, factory) =>
        {
            if (type.Namespace?.StartsWith("MailWarehouse.ViewModels") == true)
            {
                return factory.Create(type.Name, typeof(MailWarehouse.Resources.UserViewModelResource).Assembly.GetName().Name);
            }
            else if (type.Namespace?.StartsWith("MailWarehouse.Controllers") == true && type.Name.Contains("PackageController"))
            {
                return factory.Create(type.Name.Replace("Controller", ""), typeof(MailWarehouse.Resources.PackageControllerResource).Assembly.GetName().Name);
            }
            else if (type.Namespace?.StartsWith("MailWarehouse.Controllers") == true && type.Name.Contains("User"))
            {
                return factory.Create(type.Name.Replace("Controller", ""), typeof(MailWarehouse.Resources.UserControllerResource).Assembly.GetName().Name);
            }
            else if (type.FullName == "MailWarehouse.Resources.SharedResource")
            {
                return factory.Create(type.Name, type.Assembly.GetName().Name);
            }
            else if (type.FullName == "MailWarehouse.Resources.HomeControllerResource")
            {
                return factory.Create(type.Name, type.Assembly.GetName().Name);
            }
            return factory.Create(type);
        });

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("uk-UA")
    };

    options.DefaultRequestCulture = new RequestCulture("uk-UA");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;

    options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
    options.RequestCultureProviders.Insert(1, new CookieRequestCultureProvider());
    options.RequestCultureProviders.Insert(2, new AcceptLanguageHeaderRequestCultureProvider());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddAuthorization();

var app = builder.Build();

var localizationOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;
app.UseRequestLocalization(localizationOptions);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}");

app.MapControllers();

app.Run();