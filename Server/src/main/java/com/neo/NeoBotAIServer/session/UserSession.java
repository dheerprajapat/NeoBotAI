package com.neo.NeoBotAIServer.session;

import com.neo.NeoBotAIServer.models.Assistant;
import com.neo.NeoBotAIServer.rag.RagEngine;
import org.apache.catalina.User;

import java.util.UUID;

public class UserSession
{
    private final UUID sessionId;

    public String getVectorDbName() {
        return vectorDbName;
    }

    private final String vectorDbName;

    private final Assistant assistant;

    public Assistant getAssistant() {
        return assistant;
    }

    public UUID getSessionId() {
        return sessionId;
    }

    public UserSession(UUID _sessionId,String _vectorDbName)
    {
        sessionId = _sessionId;
        vectorDbName = _vectorDbName;

        assistant = RagEngine.createAssistant(vectorDbName);
    }

    public String chat(String question)
    {
        return assistant.chat(question);
    }
}
