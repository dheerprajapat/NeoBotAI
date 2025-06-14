package com.neo.NeoBotAIServer.session;

import com.neo.NeoBotAIServer.models.CreateSessionModel;
import dev.langchain4j.data.message.ChatMessage;
import org.springframework.http.ResponseEntity;

import java.util.*;

import static dev.langchain4j.data.message.ChatMessageDeserializer.messagesFromJson;
import static dev.langchain4j.data.message.ChatMessageSerializer.messagesToJson;

public class SessionManager
{
    private final static Map<UUID,UserSession> activeSessions = new HashMap<>();

    public static  UserSession createSession(CreateSessionModel sessionModel)
    {
        var uuid = UUID.randomUUID();

        if(activeSessions.containsKey(uuid))
            uuid = UUID.randomUUID();

        var session = new UserSession(uuid,sessionModel.vectorDbName(),sessionModel.chatHistory());
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

    public static List<String> getActiveSessions(){
        List<String> activeSessionsList = new ArrayList<>();
        if(activeSessions.isEmpty()) return activeSessionsList;

        for(UserSession userSession : activeSessions.values()){
            activeSessionsList.add(userSession.getSessionId().toString());
        }
        return activeSessionsList;
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

    public static String getMessages(String sessionId) {
        try {
            UUID id = UUID.fromString(sessionId);
            if (activeSessions.containsKey(id)) {
                return messagesToJson(activeSessions.get(id).getChatMessages());
            }
            return null;
        } catch (Exception e) {
            return null;
        }
    }
}
