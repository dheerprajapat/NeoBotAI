package com.neo.NeoBotAIServer.session;

import org.apache.catalina.User;

import java.util.UUID;

public class UserSession
{
    private final UUID sessionId;

    public String getVectorDbName() {
        return vectorDbName;
    }

    private final String vectorDbName;

    public UUID getSessionId() {
        return sessionId;
    }

    public UserSession(UUID _sessionId,String _vectorDbName)
    {
        sessionId = _sessionId;
        vectorDbName = _vectorDbName;
    }
}
