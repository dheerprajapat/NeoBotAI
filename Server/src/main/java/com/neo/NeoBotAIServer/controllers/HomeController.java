package com.neo.NeoBotAIServer.controllers;

import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import rag.RagEngine;

@RestController
@RequestMapping("/")
public class HomeController {

    @GetMapping
    public String home()
    {
        return "Hello";
    }
}
