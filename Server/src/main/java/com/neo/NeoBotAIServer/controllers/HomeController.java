package com.neo.NeoBotAIServer.controllers;

import com.neo.NeoBotAIServer.models.CreateSessionModel;
import com.neo.NeoBotAIServer.models.QueryQuestionModel;
import com.neo.NeoBotAIServer.rag.RagEngine;
import com.neo.NeoBotAIServer.session.SessionManager;
import jakarta.validation.Valid;
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
    public ResponseEntity<?> chat(@Valid @RequestBody QueryQuestionModel queryQuestionModel)
    {
        var result = SessionManager.chat(UUID.fromString(queryQuestionModel.sessionId()),queryQuestionModel.question());
        if(result==null)
            return ResponseEntity.badRequest().body("Bad Request, no session  with given id");

        return ResponseEntity.ok(result);
    }

    @GetMapping("/getActiveSessions")
    public ResponseEntity<?> getActiveSessions(){
        var activeSessions = SessionManager.getActiveSessions();
        return activeSessions.isEmpty() ? ResponseEntity.notFound().build() : ResponseEntity.ok(activeSessions);
    }

    @PostMapping("/createSession")
    public UUID createSession(@Valid @RequestBody CreateSessionModel createSessionModel)
    {
        var  session = SessionManager.createSession(createSessionModel);
        return  session.getSessionId();
    }

    @GetMapping("/closeSession/{sessionId}")
    public ResponseEntity<?> closeSession(@PathVariable String sessionId){
        var result = SessionManager.removeSession(sessionId);
        return result ? ResponseEntity.ok("Session closed")
                : ResponseEntity.badRequest().body("Session not found");
    }

    @GetMapping("/getDBNames")
    public ResponseEntity<?> getDBNames(){
        var dbNames = RagEngine.getDBNames();
        return dbNames.isEmpty() ? ResponseEntity.notFound().build() : ResponseEntity.ok(dbNames);
    }
}
