using System.Text.Json.Serialization;

namespace RabbitMQMicroServiceSolution.Models;

public class Message
{
    public string Title { get; set; }
    public string Content { get; set; }
    [JsonIgnore]
    public DateTime CreatedAt { get; set; }
}
