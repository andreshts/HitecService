using FluentValidation;
using FluentValidation.AspNetCore;
using HitecService.Core.Mapper;
using HitecService.Core.Services;
using HitecService.Core.Services.IServices;
using HitecService.Core.Validators.Privilege;
using HitecService.Core.Validators.Rol;
using HitecService.Core.Validators.User;
using HitecService.Data.Database;
using HitecService.Data.Repositories;
using HitecService.Data.Repositories.IRepositories;
using HitecService.Models.Configuration;
using HitecService.Models.Requests.Auth;
using HitecService.Models.Requests.Privileges;
using HitecService.Models.Requests.Rol;
using HitecService.Models.Requests.User;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace HitecService.Ioc;

public static class IocExtension
{
    public static void IocInjectAllDependencies(this IServiceCollection services, string[]? args = null)
    {
        InjectSwagger(services);
        InjectConfiguration(services);
        InjectAuthentication(services);
        InjectControllersAndDocumentation(services);
        InjectDataBases(services);
        InjectRepositories(services);
        InjectServices(services);
        InjectValidators(services);
        InjectPackages(services);
    }

    private static void InjectAuthentication(IServiceCollection services)
        => services.AddAuthentication().AddJwtBearer();

    private static void InjectConfiguration(IServiceCollection services)
    {
        var serviceProvider      = services.BuildServiceProvider();
        var env                  = serviceProvider.GetRequiredService<IHostEnvironment>();
        var lowercaseEnvironment = env.EnvironmentName.ToLower();
        var executableLocation   = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "";
        var builder = new ConfigurationBuilder()
            .SetBasePath(executableLocation)
            .AddJsonFile($"appsettings.{lowercaseEnvironment}.json", false, true)
            .AddEnvironmentVariables();

        var configuration      = builder.Build();
        var appSettingsSection = configuration.GetSection("AppSettings");

        services.Configure<ApplicationConfiguration>(appSettingsSection);
        services.AddSingleton(configuration);
    }

    private static void InjectSingletonsAndFactories(IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddHttpContextAccessor();
    }


    public static void InjectSwagger(this IServiceCollection services)
        => services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "HitecService", Version = "v1" }); });

    private static void InjectDataBases(IServiceCollection services)
    {
        var appConfig = services.BuildServiceProvider().GetRequiredService<IOptions<ApplicationConfiguration>>()
            .Value;

        var connectionString = appConfig.ConnectionStrings?.SqlServerConnection;

        services.AddDbContext<HitecServiceDbContext>(options => { options.UseSqlServer(connectionString); });
    }

    private static void InjectControllersAndDocumentation(IServiceCollection services, int majorVersion = 1, int minorVersion = 0)
    {
        services.AddResponseCompression(options =>
        {
            options.Providers.Add<GzipCompressionProvider>();
            options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "text/plain" });
        });

        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.TypeInfoResolver = new DefaultJsonTypeInfoResolver();
        });

        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        services.AddApiVersioning(config =>
        {
            config.DefaultApiVersion                   = new ApiVersion(majorVersion, minorVersion);
            config.AssumeDefaultVersionWhenUnspecified = true;
        });

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });
    }

    private static void InjectRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IMenuConfigurationRepository, MenuConfigurationRepository>();
        services.AddScoped<IPrivilegeRepository, PrivilegeRepository>();

    }

    private static void InjectServices(IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IMenuConfigurationService, MenuConfigurationService>();
        services.AddScoped<IPrivilegeService, PrivilegeService>();
        services.AddScoped<IMediatorEmailService, MediatorEmailService>();
    }

    private static void InjectValidators(IServiceCollection services)
    {
        services.AddTransient<IValidator<RequestCreateUser>, RequestCreateUserValidator>();
        services.AddTransient<IValidator<RequestRole>, RequestCreateRoleValidator>();
        services.AddTransient<IValidator<RequestPrivilege>, RequestCreatePrivilegeValidator>();
        
    }

    private static void InjectPackages(IServiceCollection services)
        => services.AddAutoMapper(x => { x.AddProfile(new MapperProfile()); });
}