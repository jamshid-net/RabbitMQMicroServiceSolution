using MassTransit;
using MassTransit.Transports;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RabbitMQAbstraction;

namespace RabbitMQ.Consumer;

internal class Program
{
    public static IConfiguration _config = null;
    static void Main(string[] args)
    {
        var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: false);
        _config = builder.Build();  
        CreateHostBuilder(args).Build().Run();

    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {

        return Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, service) =>
            {
                var rabbitMqSection = _config.GetSection("RabbitMQ");
                var _rabbitMqSettings = rabbitMqSection.Get<RabbitMqSettings>();
                service.AddMassTransit(options =>
                {
                    options.AddConsumer<MessageConsumer>();
                    options.UsingRabbitMq((context, config) =>
                    {
                        config.Host(_rabbitMqSettings?.Host, host =>
                        {
                            host.Username(_rabbitMqSettings?.Username);
                            host.Password(_rabbitMqSettings?.Password);
                        });
                        
                        config.ConfigureEndpoints(context);
                    });
                });
            });
            
    }
}
