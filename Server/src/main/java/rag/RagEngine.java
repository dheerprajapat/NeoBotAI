package rag;

import dev.langchain4j.data.document.Document;
import dev.langchain4j.data.document.loader.FileSystemDocumentLoader;
import dev.langchain4j.data.segment.TextSegment;
import dev.langchain4j.store.embedding.EmbeddingStoreIngestor;
import dev.langchain4j.store.embedding.inmemory.InMemoryEmbeddingStore;

import java.util.List;
import java.util.concurrent.CompletableFuture;

public class RagEngine
{
    public  static CompletableFuture<Boolean> createVectorDatabase()
    {
        CompletableFuture<Boolean> result = CompletableFuture.supplyAsync(()->
        {
            try {


                List<Document> documents = FileSystemDocumentLoader.loadDocuments("..\\..\\..\\..\\Data\\");

                InMemoryEmbeddingStore<TextSegment> embeddingStore = new InMemoryEmbeddingStore<>();
                EmbeddingStoreIngestor.ingest(documents, embeddingStore);

                String filePath = "..\\..\\..\\..\\Data\\embeddingStore.json";
                embeddingStore.serializeToFile(filePath);
                //InMemoryEmbeddingStore<TextSegment> deserializedStore = InMemoryEmbeddingStore.fromFile(filePath);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        });

        return result;
    }

}
