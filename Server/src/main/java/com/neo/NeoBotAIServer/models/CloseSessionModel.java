package com.neo.NeoBotAIServer.models;

import jakarta.validation.constraints.NotEmpty;
import jakarta.validation.constraints.NotNull;
import lombok.Data;

@Data
public class CloseSessionModel
{
    @NotNull
    @NotEmpty
    private String sessionId;
}