package com.neo.NeoBotAIServer.models;

import dev.langchain4j.memory.ChatMemory;

public interface Assistant
{
    public  String chat(String message);
}
