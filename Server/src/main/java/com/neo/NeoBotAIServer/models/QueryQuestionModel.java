package com.neo.NeoBotAIServer.models;

import jakarta.validation.constraints.NotEmpty;
import jakarta.validation.constraints.NotNull;

public record QueryQuestionModel(@NotNull @NotEmpty String sessionId,@NotNull @NotEmpty String question)
{
}
