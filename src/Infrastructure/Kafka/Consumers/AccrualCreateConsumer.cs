using System.Text;
using System.Text.Json;
using Confluent.Kafka;
using Domain.UseCases.Accruals.Create;
using Infrastructure.Config;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Kafka.Consumers;

public class AccrualCreateConsumer : BackgroundService
{
    private readonly ILogger<AccrualCreateConsumer> _logger;
    private readonly IConsumer<string, string> _consumer;
    private readonly IProducer<string, string> _producer;
    private readonly IServiceProvider _serviceProvider;

    public AccrualCreateConsumer(ILogger<AccrualCreateConsumer> logger, KafkaConfig kafkaConfig,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;

        var config = new ConsumerConfig
        {
            BootstrapServers = kafkaConfig.BootstrapServers,
            SecurityProtocol = kafkaConfig.SecurityProtocol,
            SaslMechanism = kafkaConfig.SaslMechanisms,
            SaslUsername = kafkaConfig.SaslUsername,
            SaslPassword = kafkaConfig.SaslPassword,
            GroupId = "ms-transaction",
            AutoOffsetReset = AutoOffsetReset.Latest
        };

        _consumer = new ConsumerBuilder<string, string>(config).Build();
        _producer = new ProducerBuilder<string, string>(config).Build();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _consumer.Subscribe("transactions.accrual.create");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var consumeResult = _consumer.Consume(TimeSpan.FromSeconds(1));

                if (consumeResult != null)
                {
                    _logger.LogInformation($"Received message: {consumeResult.Message.Value}");

                    using var scope = _serviceProvider.CreateScope();
                    try
                    {
                        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                        var command = JsonSerializer.Deserialize<CreateAccrualCommand>(consumeResult.Message.Value);
                        await mediator.Send(command, stoppingToken);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error processing message");

                        var dlqMessage = new Message<string, string>
                        {
                            Key = consumeResult.Message.Key,
                            Value = consumeResult.Message.Value,
                            Headers =
                            [
                                new Header("correlationId", Encoding.UTF8.GetBytes(Guid.NewGuid().ToString())),
                                new Header("origin", "ms-transactions"u8.ToArray())
                            ]
                        };

                        _producer.Produce("transactions.accrual.create.dlq", dlqMessage);
                    }
                }
            }
            catch (ConsumeException ex)
            {
                _logger.LogError(ex, "Error consuming message");
            }

            await Task.Delay(10, stoppingToken);
        }

        _consumer.Close();
        _producer.Flush(TimeSpan
            .FromSeconds(10));

        _producer.Dispose();
    }
}