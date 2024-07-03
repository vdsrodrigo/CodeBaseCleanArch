using System.Text;
using System.Text.Json;
using Confluent.Kafka;
using Domain.AccrualAggregate.Events;
using Infrastructure.Config;
using Infrastructure.Kafka.Messages;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Kafka.Producers;

internal class AccrualDeletedNotifier : INotificationHandler<AccrualDeletedEvent>
{
    private readonly IProducer<string, string> _producer;
    private readonly ILogger<AccrualDeletedNotifier> _logger;

    public AccrualDeletedNotifier(KafkaConfig kafkaConfig, ILogger<AccrualDeletedNotifier> logger)
    {
        _logger = logger;
        var config = new ProducerConfig
        {
            BootstrapServers = kafkaConfig.BootstrapServers,
            SecurityProtocol = kafkaConfig.SecurityProtocol,
            SaslMechanism = kafkaConfig.SaslMechanisms,
            SaslUsername = kafkaConfig.SaslUsername,
            SaslPassword = kafkaConfig.SaslPassword
        };

        _producer = new ProducerBuilder<string, string>(config).Build();
    }

    public async Task Handle(AccrualDeletedEvent domainEvent, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling Accrual Deleted event for {accrualId}", domainEvent.AccrualId);
        _logger.LogInformation("Produzindo mensagem para o tópico de exclusão de acúmulo");

        var accrualDeletedMessage = new AccrualDeletedMessage(domainEvent.AccrualId, DateTime.Today);
        var messageValue = JsonSerializer.Serialize(accrualDeletedMessage);

        var envelope = new Message<string, string>
        {
            Key = Guid.NewGuid().ToString(),
            Value = messageValue,
            Headers =
            [
                new Header("correlationId", Encoding.UTF8.GetBytes(Guid.NewGuid().ToString())),
                new Header("origin", "ms-transactions"u8.ToArray())
            ]
        };

        try
        {
            await _producer.ProduceAsync("transactions.accrual.deleted", envelope, cancellationToken);
        }
        catch (Exception e)
        {
            // Coloque sua regra aqui, mais tentativas, logar erro ou salvar em banco de dados para analise posterior
            // de qualquer forma se faz necessário realizar uma ação para identificar que consumidores externos não foram informados do evento
            Console.WriteLine(e);
            throw;
        }

        _logger.LogInformation("mensagem criada com sucesso!");
    }
}