package session;

import models.CreateSessionModel;

import java.util.HashMap;
import java.util.Map;
import java.util.UUID;

public class SessionManager
{
    private final static Map<UUID,UserSession> activeSessions = new HashMap<>();

    public static  void createSession(CreateSessionModel sessionModel)
    {
        var session = new UserSession(UUID.randomUUID(),sessionModel.getVectorDbName());
        activeSessions.put(session.getSessionId(),session);
    }
}
