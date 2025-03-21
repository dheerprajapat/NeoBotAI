package com.neo.NeoBotAIServer.models;

import jakarta.validation.constraints.NotEmpty;
import jakarta.validation.constraints.NotNull;
import lombok.Data;

import java.util.UUID;

@Data
public class QueryQuestionModel {
    @NotNull
    @NotEmpty
    private String sessionId;

    @NotNull
    @NotEmpty
    private String question;
}
