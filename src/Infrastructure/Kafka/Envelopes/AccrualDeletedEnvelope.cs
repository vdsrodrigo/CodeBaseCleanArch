using Infrastructure.Kafka.Messages;

namespace Infrastructure.Kafka.Envelopes;

public class AccrualDeletedEnvelope(AccrualDeletedMessage message)
{
    public string GroupId => "accrual-deleted-group";
    public string TopicName => "accrual-deleted";
    public IDictionary<string, byte[]> Headers { get; set; }
    public string Key { get; set; }
    public AccrualDeletedMessage Value { get; set; } = message;
    public bool UseDeadLetter => true;
    public bool IsRetrying { get; set; }
}