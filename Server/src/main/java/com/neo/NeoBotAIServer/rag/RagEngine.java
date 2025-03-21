package com.neo.NeoBotAIServer.rag;

import com.neo.NeoBotAIServer.models.Assistant;
import dev.langchain4j.data.document.Document;
import dev.langchain4j.data.document.loader.FileSystemDocumentLoader;
import dev.langchain4j.data.segment.TextSegment;
import dev.langchain4j.model.chat.ChatLanguageModel;
import dev.langchain4j.model.ollama.OllamaChatModel;
import dev.langchain4j.rag.content.retriever.ContentRetriever;
import dev.langchain4j.rag.content.retriever.EmbeddingStoreContentRetriever;
import dev.langchain4j.service.AiServices;
import dev.langchain4j.store.embedding.EmbeddingStoreIngestor;
import dev.langchain4j.store.embedding.inmemory.InMemoryEmbeddingStore;
import opennlp.tools.parser.Cons;

import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.List;
import java.util.concurrent.CompletableFuture;

public class RagEngine
{
    private static  InMemoryEmbeddingStore<TextSegment> embeddingStore;
    private  static  Assistant assistant;

    public  static CompletableFuture<Boolean> createVectorDatabase()
    {
        return CompletableFuture.supplyAsync(()->
        {
            try {

                String filePath = ".\\Embeddings\\embeddingStore.json";

                if(Files.exists(Path.of(filePath)))
                {
                    embeddingStore = InMemoryEmbeddingStore.fromFile(filePath);
                    System.out.println("Vector database already exists");
                    return true;
                }

                var path = ".\\Data\\";
                List<Document> documents = FileSystemDocumentLoader.loadDocuments(path);

                embeddingStore = new InMemoryEmbeddingStore<>();
                EmbeddingStoreIngestor.ingest(documents, embeddingStore);

                embeddingStore.serializeToFile(filePath);

                System.out.println("Vector database created successfully");

                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        });
    }

    public  static Assistant getAssistant()
    {
        return  assistant;
    }

    public  static Assistant createAssistant()
    {
        if(embeddingStore == null)
            return null;


        ChatLanguageModel model = OllamaChatModel.builder()
                .baseUrl("http://localhost:11434")
                .modelName("llama3.1:latest").build();

        assistant = AiServices.builder(Assistant.class)
                .chatLanguageModel(model)
                .contentRetriever(EmbeddingStoreContentRetriever.from(embeddingStore))
                .build();

        return assistant;
    }

}
