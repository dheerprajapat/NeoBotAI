package com.neo.NeoBotAIServer.models;

import jakarta.validation.constraints.NotEmpty;
import jakarta.validation.constraints.NotNull;
import lombok.Data;

@Data
public class CreateSessionModel
{
    @NotNull
    @NotEmpty
    private String vectorDbName;

}

