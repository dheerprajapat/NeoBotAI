package models;

public class QueryQuestionModel {
    private String sessionId;
    private String question;

    public QueryQuestionModel(String sessionId, String question) {
        this.sessionId = sessionId;
        this.question = question;
    }

    // Getters and Setters
    public String getSessionId() {
        return sessionId;
    }

    public void setSessionId(String sessionId) {
        this.sessionId = sessionId;
    }

    public String getQuestion() {
        return question;
    }

    public void setQuestion(String question) {
        this.question = question;
    }
}
