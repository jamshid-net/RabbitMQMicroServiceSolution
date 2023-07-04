using MassTransit;
using MassTransit.MultiBus;
using RabbitMQAbstraction;

namespace RabbitMQMicroServiceSolution;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container. 
        

        builder.Services.AddMassTransit(options=>
        {
            var rabbitMqSection = builder.Configuration.GetSection("RabbitMQ");
            var _rabbitMqSettings = rabbitMqSection.Get<RabbitMqSettings>();
            
            options.SetKebabCaseEndpointNameFormatter();
            options.UsingRabbitMq((context, config) =>
            {
                config.Host(_rabbitMqSettings.Host, host =>
                {
                    host.Username(_rabbitMqSettings.Username);
                    host.Password(_rabbitMqSettings.Password);
                });
                config.ConfigureEndpoints(context); 
            });
        });
       

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
