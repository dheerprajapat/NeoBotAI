package com.neo.NeoBotAIServer.controllers;

import com.neo.NeoBotAIServer.models.QueryQuestionModel;
import com.neo.NeoBotAIServer.rag.RagEngine;
import org.apache.poi.hslf.record.CString;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/")
public class HomeController {

    @GetMapping
    public String home()
    {
        return "Hello";
    }

    @PostMapping("/chat")
    public  String chat(@RequestBody QueryQuestionModel message)
    {
        return RagEngine.getAssistant().chat(message.getQuestion());
    }
}
