package com.neo.NeoBotAIServer.controllers;

import com.neo.NeoBotAIServer.models.CreateSessionModel;
import com.neo.NeoBotAIServer.models.QueryQuestionModel;
import com.neo.NeoBotAIServer.rag.RagEngine;
import com.neo.NeoBotAIServer.session.SessionManager;
import org.apache.poi.hslf.record.CString;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.UUID;

@RestController
@RequestMapping("/")
public class HomeController {

    @GetMapping
    public String home()
    {
        return "Hello";
    }

    @PostMapping("/chat")
    public ResponseEntity<?> chat(@RequestBody QueryQuestionModel queryQuestionModel)
    {
        var result = SessionManager.chat(queryQuestionModel.getSessionId(),queryQuestionModel.getQuestion());
        if(result==null)
            return ResponseEntity.badRequest().body("Bad Request, no session  with given id");

        return ResponseEntity.ok(result);
    }

    @PostMapping("/createSession")
    public UUID createSession(@RequestBody CreateSessionModel createSessionModel)
    {
        var  session = SessionManager.createSession(createSessionModel);
        return  session.getSessionId();
    }
}
