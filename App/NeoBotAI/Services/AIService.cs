using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace NeoBotAI.Services;

public class AIService
{
    private static HttpClient httpClient = new HttpClient()
    {
        BaseAddress = new Uri("http://localhost:8080/")
    };

    public AIService()
    {

    }

    public async ValueTask<Guid?> CreateSessionAsync(string vectorDbName,string chatHistory)
    {
        try
        {
            var res = await httpClient.PostAsJsonAsync("/createSession", new
            {
                vectorDbName,
                chatHistory
            });

            if(res.IsSuccessStatusCode)
            {
                return Guid.Parse((await res.Content.ReadFromJsonAsync<string>())!);
            }

            return null;
        }
        catch
        {
            return null;
        }
    }

}
