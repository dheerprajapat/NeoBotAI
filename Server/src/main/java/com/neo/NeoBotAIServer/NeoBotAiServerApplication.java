package com.neo.NeoBotAIServer;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import rag.RagEngine;

@SpringBootApplication
public class NeoBotAiServerApplication {

	public static void main(String[] args) {

		SpringApplication.run(NeoBotAiServerApplication.class, args);
		RagEngine.createVectorDatabase();
	}

}
