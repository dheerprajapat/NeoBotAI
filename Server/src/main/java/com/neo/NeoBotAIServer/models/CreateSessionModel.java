package com.neo.NeoBotAIServer.models;

import dev.langchain4j.data.message.ChatMessage;
import dev.langchain4j.memory.ChatMemory;
import jakarta.validation.constraints.NotEmpty;
import jakarta.validation.constraints.NotNull;

import java.util.List;

public record CreateSessionModel(@NotNull @NotEmpty String vectorDbName, @NotNull String chatHistory)
{

}

