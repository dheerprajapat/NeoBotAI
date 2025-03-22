package com.neo.NeoBotAIServer.models;

import jakarta.validation.constraints.NotEmpty;
import jakarta.validation.constraints.NotNull;

public record CloseSessionModel(@NotNull @NotEmpty String sessionId)
{

}