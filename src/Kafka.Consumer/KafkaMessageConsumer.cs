using Confluent.Kafka;
using Kafka.Shared;
using MassTransit;
using MassTransit.KafkaIntegration;

namespace Kafka.Consumer;

public class KafkaMessageConsumer : IConsumer<IMessage>
{
    public async Task Consume(ConsumeContext<IMessage> context)
    {
        var ctx = (context.ReceiveContext as KafkaReceiveContext<Ignore, IMessage>);
        Console.WriteLine($"Message: {context.Message.Text}, Offset: {ctx?.Offset}");
        await Task.CompletedTask;
    }
}
