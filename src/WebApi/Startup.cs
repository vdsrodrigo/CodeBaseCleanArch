using System.Reflection;
using System.Text.Json.Serialization;
using Application.UseCases.Accrual.Create;
using Ardalis.SharedKernel;
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

        ConfigureMediatR(services);

        services.AddInfrastructureServices(Configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

        app.UseFastEndpoints(config => config.Serializer.Options.Converters.Add(new JsonStringEnumConverter()))
            .UseSwaggerGen(); // Includes AddFileServer and static files middleware

        app.UseHttpsRedirection();
    }

    private void ConfigureMediatR(IServiceCollection services)
    {
        var mediatRAssemblies = new[]
        {
            Assembly.GetAssembly(typeof(Domain.AccrualAggregate.Accrual)), // Core
            Assembly.GetAssembly(typeof(CreateAccrualCommand)) // UseCases
        };
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatRAssemblies!));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>();
    }
}

public interface IStartup
{
    IConfiguration Configuration { get; }
    void ConfigureServices(IServiceCollection services);
    void Configure(IApplicationBuilder app, IWebHostEnvironment env);
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
        startup.Configure(app, builder.Environment);
        app.Run();
        return builder;
    }
}