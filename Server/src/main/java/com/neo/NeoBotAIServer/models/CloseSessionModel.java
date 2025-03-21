package com.neo.NeoBotAIServer.models;

public class CloseSessionModel
{
    private String sessionId;

    public CloseSessionModel(String sessionId) {
        this.sessionId = sessionId;
    }

    // Getter
    public String getSessionId() {
        return sessionId;
    }

    // Setter
    public void setSessionId(String sessionId) {
        this.sessionId = sessionId;
    }
}