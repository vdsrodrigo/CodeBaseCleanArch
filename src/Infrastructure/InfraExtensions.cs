using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using Domain.AccrualAggregate.Events;
using Domain.UseCases.Accruals.List;
using Infrastructure.Config;
using Infrastructure.Data.Queries;
using Infrastructure.Kafka.Consumers;
using Infrastructure.Kafka.Producers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfraExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
    {
        var kafkaConfig = new KafkaConfig();
        config.GetSection("Kafka").Bind(kafkaConfig);
        services.AddSingleton(kafkaConfig);
        
        var connectionString = config.GetConnectionString("PostgresConnection");
        Guard.Against.Null(connectionString);
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));
        
        services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        
        services.AddScoped<IListAccrualsQueryService, ListAccrualsQueryService>();
        services.AddScoped<INotificationHandler<AccrualDeletedEvent>, AccrualDeletedNotifier>();
        services.AddHostedService<AccrualCreateConsumer>();

        return services;
    }
}