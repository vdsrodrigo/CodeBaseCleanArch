namespace Infrastructure.Kafka.Messages;

public record AccrualDeletedMessage(int Id, DateTime DeletedAt);