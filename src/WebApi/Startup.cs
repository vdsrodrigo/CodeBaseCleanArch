using System.Reflection;
using System.Text.Json.Serialization;
using Ardalis.SharedKernel;
using Domain.AccrualAggregate;
using Domain.AccrualAggregate.Interfaces;
using Domain.Services;
using Domain.UseCases.Accruals.Create;
using FastEndpoints;
using FastEndpoints.Swagger;
using Infrastructure;
using MediatR;

namespace WebApi;

public class Startup(IConfiguration configuration) : IStartup
{
    public IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        // Configure Web Behavior
        services.Configure<CookiePolicyOptions>(options =>
        {
            options.CheckConsentNeeded = context => true;
            options.MinimumSameSitePolicy = SameSiteMode.None;
        });

        services.AddFastEndpoints()
            .SwaggerDocument(o => { o.ShortSchemaNames = true; });

        services.AddInfrastructureServices(Configuration);
        
        ConfigureMediatR(services);
        services.AddScoped<IDeleteAccrualService, DeleteAccrualService>();
    }
    
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<IStartup> logger)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseDefaultExceptionHandler(); // from FastEndpoints
            app.UseHsts();
        }
        
        app.ConfigureExceptionHandler(logger);


        app.UseFastEndpoints(config => config.Serializer.Options.Converters.Add(new JsonStringEnumConverter()))
            .UseSwaggerGen(); // Includes AddFileServer and static files middleware

        app.UseHttpsRedirection();
    }

    private void ConfigureMediatR(IServiceCollection services)
    {
        var mediatRAssemblies = new[]
        { 
            Assembly.GetAssembly(typeof(Accrual)), //Application
            Assembly.GetAssembly(typeof(CreateAccrualCommand))// UseCases
        };
        
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatRAssemblies!));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>();
    }
}

public interface IStartup
{
    void ConfigureServices(IServiceCollection services);
    void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<IStartup> logger);
}

public static class StartupExtensions
{
    public static WebApplicationBuilder UseStartup<TStartup>(this WebApplicationBuilder builder)
        where TStartup : IStartup
    {
        if (Activator.CreateInstance(typeof(TStartup), builder.Configuration) is not IStartup startup)
            throw new ArgumentNullException("Startup not found");

        startup.ConfigureServices(builder.Services);
        var app = builder.Build();
        startup.Configure(app, builder.Environment, app.Services.GetRequiredService<ILogger<IStartup>>());
        app.Run();
        return builder;
    }
}