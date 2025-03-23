using NeoBotAI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoBotAI.Models;

public record UserSession(string Id, AIService AIService)
{
    public List<ChatMessage> chatMessages = new List<ChatMessage>();

    public async ValueTask<string?> ChatAsync(string question)
    {
        return await AIService.ChatAsync(question, Id);
    }

    public async ValueTask<bool> CloseSession()
    {
        return await AIService.CloseSessionAsync(Id);
    }
}
