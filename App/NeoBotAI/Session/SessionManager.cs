using NeoBotAI.Models;
using NeoBotAI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NeoBotAI.Session;

public class SessionManager
{
    public SessionManager(AIService aIService)
    {
        AIService = aIService;
    }

    public AIService AIService { get; }

    public async ValueTask<UserSession?> CreateSessionAsync(string vectorDbName, string chatHistory)
    {
        var sessionId = await AIService.CreateSessionAsync(vectorDbName, chatHistory);
        if (sessionId == null) return null;

        return new UserSession(sessionId, AIService);
    }
}
