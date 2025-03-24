package com.neo.NeoBotAIServer.session;

import com.neo.NeoBotAIServer.models.Assistant;
import com.neo.NeoBotAIServer.rag.RagEngine;
import dev.langchain4j.data.message.ChatMessage;
import dev.langchain4j.memory.ChatMemory;

import java.util.List;
import java.util.UUID;

public class UserSession
{
    private final UUID sessionId;

    public String getVectorDbName() {
        return vectorDbName;
    }

    private final String vectorDbName;

    private final Assistant assistant;
    private  final ChatMemory chatMemory;

    public Assistant getAssistant() {
        return assistant;
    }

    public UUID getSessionId() {
        return sessionId;
    }

    public UserSession(UUID _sessionId, String _vectorDbName, String history)
    {
        sessionId = _sessionId;
        vectorDbName = _vectorDbName;

        var result = RagEngine.createAssistant(vectorDbName,history,true);

        if(result!=null)
        {
            assistant = result.component1();
            chatMemory = result.component2();
        }
        else
        {
            assistant = null;
            chatMemory = null;
        }
    }

    public String chat(String question)
    {
        return assistant.chat(question);
    }

    public  List<ChatMessage> getChatMessages()
    {
        return  chatMemory.messages();
    }
}
