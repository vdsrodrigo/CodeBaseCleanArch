namespace Infrastructure.Kafka.Messages;

public record AccrualDeletedMessage(int Id, string MemberNumber, DateTime DeletedAt);