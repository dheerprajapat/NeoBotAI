using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using NeoBotAI.Models;

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

    public async ValueTask<string?> CreateSessionAsync(string vectorDbName,string chatHistory)
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
                return await res.Content.ReadFromJsonAsync<string>();
            }

            return null;
        }
        catch
        {
            return null;
        }
    }

    public async ValueTask<string?> ChatAsync(string question, string sessionId)
    {
        try
        {
            var res = await httpClient.PostAsJsonAsync("/chat", new
            {
                question,
                sessionId
            });

            if (res.IsSuccessStatusCode)
            {
                return await res.Content.ReadAsStringAsync();
            }

            return null;
        }
        catch 
        {
            return null;
        }
    }

    public async ValueTask<bool> CloseSessionAsync(string sessionId)
    {
        try
        {
            var res = await httpClient.GetAsync($"/closeSession/{sessionId}");

            if (res.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }
}
