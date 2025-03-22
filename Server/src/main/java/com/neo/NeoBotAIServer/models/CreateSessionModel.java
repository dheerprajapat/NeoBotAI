package com.neo.NeoBotAIServer.models;

import jakarta.validation.constraints.NotEmpty;
import jakarta.validation.constraints.NotNull;

public record CreateSessionModel(@NotNull @NotEmpty String vectorDbName)
{

}

