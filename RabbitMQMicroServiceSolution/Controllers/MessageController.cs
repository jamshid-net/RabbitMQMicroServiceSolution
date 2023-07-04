using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQMicroServiceSolution.Models;

namespace RabbitMQMicroServiceSolution.Controllers;
[Route("api/[controller]")]
[ApiController]
public class MessageController : ControllerBase
{
    private readonly IBus _bus;

    public MessageController(IBus bus)
    {
        _bus = bus;
    }

    [HttpPost] 
    public async Task<IActionResult> SendMessage(Message message)
    {
        message.CreatedAt = DateTime.Now;
        await _bus.Publish<Message>(message, x =>
        {
            
            x.SetRoutingKey("InfoKey2");
        });
        return Ok();
    }

}
