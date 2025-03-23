using Microsoft.AspNetCore.Components;
using NeoBotAI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NeoBotAI.Models;

public record UserSession(string Id,string VectorDbName,DateTime TimeStamp)
{
    [JsonIgnore]
    public AIService AIService => AIService.Instance;

    [JsonIgnore]
    public string? Title
    {
        get
        {
            if(ChatMessages.Count==0)
                return null;
            string? str = ChatMessages.First().Contents?.First().Text;
            return str?.Substring(0, Math.Min(80, str.Length));
        }
    }

    public ObservableCollection<ChatMessage> ChatMessages { get; set; } = new ObservableCollection<ChatMessage>();

    public async ValueTask<string?> ChatAsync(string question)
    {
        var userMessage = ChatMessage.UserMessage(question);

        ChatMessages.Add(userMessage);

        var response =  await AIService.ChatAsync(question, Id);

        if (response==null)
            return null;

        var aiMessage = ChatMessage.AIMessage(response);

        ChatMessages.Add(aiMessage);

        return response;
    }

    public async ValueTask<bool> CloseSessionAsync()
    {
        return await AIService.CloseSessionAsync(Id);
    }

    public void SaveSession()
    {
        try
        {

        }
        catch
        {

        }
    }
}
