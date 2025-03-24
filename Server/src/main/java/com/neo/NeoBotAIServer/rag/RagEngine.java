package com.neo.NeoBotAIServer.rag;

import com.neo.NeoBotAIServer.models.Assistant;
import dev.langchain4j.data.document.loader.FileSystemDocumentLoader;
import dev.langchain4j.data.message.ChatMessage;
import dev.langchain4j.data.segment.TextSegment;
import dev.langchain4j.memory.ChatMemory;
import dev.langchain4j.memory.chat.MessageWindowChatMemory;
import dev.langchain4j.model.chat.ChatLanguageModel;
import dev.langchain4j.model.googleai.GoogleAiGeminiChatModel;
import dev.langchain4j.model.ollama.OllamaChatModel;
import dev.langchain4j.rag.content.retriever.EmbeddingStoreContentRetriever;
import dev.langchain4j.service.AiServices;
import dev.langchain4j.store.embedding.EmbeddingStoreIngestor;
import dev.langchain4j.store.embedding.inmemory.InMemoryEmbeddingStore;
import kotlin.Pair;
import org.springframework.beans.factory.annotation.Value;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.concurrent.CompletableFuture;

import static dev.langchain4j.data.message.ChatMessageDeserializer.messagesFromJson;

public class RagEngine
{
    private static Map<String,InMemoryEmbeddingStore<TextSegment>> embeddingStores;
    private  static  Assistant assistant;


    public  static CompletableFuture<Boolean> createVectorDatabase()
    {
        return CompletableFuture.supplyAsync(()->
        {
            embeddingStores = new HashMap<>();

            try {

                for(Path file: Files.list(Paths.get(".\\Data\\")).toArray(Path[]::new))
                {
                    var name = file.getFileName().toString().replace(getFileExtension(file.getFileName().toString()),"json");
                    var fileName = ".\\Embeddings\\"+name;

                    if(Files.exists(Path.of(fileName)))
                    {
                        embeddingStores.put(name,InMemoryEmbeddingStore.fromFile(fileName));
                        System.out.println("Vector database already exists: "+fileName);
                        continue;
                    }

                    var doc = FileSystemDocumentLoader.loadDocument(file);
                    var embeddingStore = new InMemoryEmbeddingStore<TextSegment>();
                    EmbeddingStoreIngestor.ingest(doc,embeddingStore);
                    embeddingStore.serializeToFile(fileName);

                    embeddingStores.put(name,embeddingStore);

                    System.out.println("Vector database created successfully: "+fileName);


                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        });
    }


    public  static Pair<Assistant,ChatMemory> createAssistant(String vectorDbName, String history,Boolean useLocalLLM)
    {
        if(embeddingStores == null)
            return null;

        var embeddingStore = embeddingStores.get(vectorDbName);

        if(embeddingStore == null)
            return null;

        ChatMemory chatMemory = MessageWindowChatMemory.withMaxMessages(30);

        if(history != null && !history.isEmpty())
        {
            for(ChatMessage message:messagesFromJson(history))
            {
                chatMemory.add(message);
            }
        }


        ChatLanguageModel model =null;

        if(useLocalLLM)
            model = OllamaChatModel.builder()
                .baseUrl("http://localhost:11434")
                .modelName("llama3.1:latest").build();
        else
            model = GoogleAiGeminiChatModel.builder()
                    .apiKey(System.getenv("GEMINI_AI_KEY"))
                    .modelName("gemini-2.0-flash-lite")
                    .temperature(0.0)
                    .build();

        assistant = AiServices.builder(Assistant.class)
                .chatLanguageModel(model)
                .chatMemory(chatMemory)
                .contentRetriever(EmbeddingStoreContentRetriever.from(embeddingStore))
                .build();


        return new Pair<>(assistant,chatMemory);
    }


    public static List<String> getDBNames() {
        String path = ".\\Embeddings\\";
        try {
            return Files.list(Path.of(path)).filter(file -> getFileExtension(file.toString()).equals("json"))
                    .map(file -> file.getFileName().toString()).toList();
        } catch (IOException e) {
            return new ArrayList<>();
        }
    }

    public static String getFileExtension(String fileName) {
        // Find the last occurrence of '.' in the filename
        int dotIndex = fileName.lastIndexOf('.');
        // If '.' is not found, return "No extension", otherwise return the substring after '.'
        return (dotIndex == -1) ? "No extension" : fileName.substring(dotIndex + 1);
    }

}
