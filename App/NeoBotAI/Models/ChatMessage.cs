using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NeoBotAI.Models;

public class ChatMessage
{
    [JsonPropertyName("contents")]
    public List<Content> Contents { get; set; } = [];

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; }
}

public class Content
{
    [JsonPropertyName("text")]
    public string Text { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }
}

