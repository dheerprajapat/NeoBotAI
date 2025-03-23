using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NeoBotAI.Models;


public class ChatMessage
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("contents")]
    public List<Content>? Contents { get; set; } = null;


    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("type")]
    public string? Type { get; set; } = null;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("text")]
    public string? Text { get; set; } = null;

    public static ChatMessage AIMessage(string response)
    {
        return new ChatMessage()
        {
            Text = response,
            Type = "AI"
        };
    }


    public static ChatMessage UserMessage(string text)
    {
        return new ChatMessage()
        {
            Type = "USER",
            Contents = new List<Content>()
            {
                new Content()
                {
                    Text = text,
                    Type = "TEXT"
                }
            }
        };
    }

    [JsonIgnore]
    public MessageRole Role => (Contents == null || Contents.Count == 0) ? MessageRole.AI : MessageRole.USER;
}

public class Content
{
    [JsonPropertyName("text")]
    public string? Text { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }
}

public enum MessageRole
{
    AI,
    USER
}