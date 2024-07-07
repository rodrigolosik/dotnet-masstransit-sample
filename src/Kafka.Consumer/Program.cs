using Kafka.Consumer;
using Kafka.Shared;
using MassTransit;

var services = new ServiceCollection();

services.AddMassTransit(x =>
{
    const string topicName = "my-topic";
    const string consumerGroup = "KafkaExampleConsumer";
    const string kafkaBrokerServers = "localhost:9092";

    x.UsingInMemory((context, cfg) => cfg.ConfigureEndpoints(context));

    x.AddRider(rider =>
    {
        rider.AddConsumer<KafkaMessageConsumer>();
        rider.UsingKafka((context, k) =>
        {
            k.Host(kafkaBrokerServers);

            k.TopicEndpoint<IMessage>(topicName, consumerGroup, e =>
            {
                e.ConfigureConsumer<KafkaMessageConsumer>(context);
                e.CreateIfMissing();
            });
        });
    });
});

var provider = services.BuildServiceProvider();

var busControl = provider.GetRequiredService<IBusControl>();

await busControl.StartAsync(new CancellationTokenSource(TimeSpan.FromSeconds(10)).Token);

Console.WriteLine("Started...");
Console.ReadKey();