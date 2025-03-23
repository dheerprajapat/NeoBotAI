using NeoBotAI.Models;
using NeoBotAI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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

        var session = new UserSession(sessionId,vectorDbName,DateTime.Now);

        return session;
    }

    public async ValueTask<UserSession?> ResumeSessionAsync(UserSession session)
    {
        var sessionId = await AIService.CreateSessionAsync(session.VectorDbName, JsonSerializer.Serialize(session.ChatMessages));
        if (sessionId == null) return null;

        var newSession = new UserSession(sessionId, session.VectorDbName, session.TimeStamp);

        newSession.ChatMessages = session.ChatMessages;

        SessionHub.Instance.ChangeSessionId(session.Id, newSession);

        return newSession;
    }
}
