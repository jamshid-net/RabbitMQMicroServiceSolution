using MassTransit;
using RabbitMQMicroServiceSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Consumer;
public class MessageConsumer : IConsumer<Message>
{
    public Task Consume(ConsumeContext<Message> context)
    {
        
        var message = context.Message;
        Console.WriteLine($"Title:{message.Title}\nContent:{message.Content}\nDate:{context.SentTime}");
        return Task.CompletedTask;  
    }
}
