package com.neo.NeoBotAIServer;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import com.neo.NeoBotAIServer.rag.RagEngine;

import java.util.concurrent.ExecutionException;

@SpringBootApplication
public class NeoBotAiServerApplication {

	public static void main(String[] args) throws ExecutionException, InterruptedException {
		if(!RagEngine.createVectorDatabase().get())
		{
			System.out.println("Failed to create vector databases,exiting");
			return;
		}


		SpringApplication.run(NeoBotAiServerApplication.class, args);
	}

}
