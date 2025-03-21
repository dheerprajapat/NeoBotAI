package com.neo.NeoBotAIServer.models;

import java.util.UUID;

public class QueryQuestionModel {
    private UUID sessionId;
    private String question;

    public QueryQuestionModel(String sessionId, String question) {
        this.sessionId = UUID.fromString(sessionId);
        this.question = question;
    }

    // Getters and Setters
    public UUID getSessionId() {
        return sessionId;
    }


    public String getQuestion() {
        return question;
    }

    public void setQuestion(String question) {
        this.question = question;
    }
}
