package com.neo.NeoBotAIServer.session;

import com.neo.NeoBotAIServer.models.CreateSessionModel;

import java.util.HashMap;
import java.util.Map;
import java.util.UUID;

public class SessionManager
{
    private final static Map<UUID,UserSession> activeSessions = new HashMap<>();

    public static  UserSession createSession(CreateSessionModel sessionModel)
    {
        var uuid = UUID.randomUUID();

        if(activeSessions.containsKey(uuid))
            uuid = UUID.randomUUID();

        var session = new UserSession(uuid,sessionModel.getVectorDbName());
        activeSessions.put(session.getSessionId(),session);
        return session;
    }

    public  static  String chat(UUID sessionId,String question)
    {
        if(activeSessions.containsKey(sessionId))
            return activeSessions.get(sessionId).chat(question);
        else
            return null;
    }

    public static boolean removeSession(String sessionId){
        try {
            UUID id = UUID.fromString(sessionId);
            if (activeSessions.containsKey(id)) {
                activeSessions.remove(id);
                return true;
            }
            return false;
        } catch (Exception e) {
            return false;
        }
    }
}
