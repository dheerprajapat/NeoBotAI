using System.Collections.Specialized;
using NeoBotAI.Models;
using NeoBotAI.Session;

namespace NeoBotAI.Components.Pages;

public partial class Chat:IDisposable
{
    UserSession? session;
    string question = string.Empty;

    private  SemaphoreSlim semaphore = new SemaphoreSlim(1);

    private string IsLast(ChatMessage message)
    {
        return session is null ? "" : session.ChatMessages.LastOrDefault() == message?"text":"";
    }

    protected override async Task OnInitializedAsync()
    {
        session = await SessionManager.CreateSessionAsync("i510.json", "");

        if (session != null)
        {
            session.ChatMessages.CollectionChanged += OnChatMessageCollectionChanged;
        }
    }

    void OnChatMessageCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        StateHasChanged();
    }

    async Task ChatAsync()
    {
        if (session == null || string.IsNullOrWhiteSpace(question) || semaphore.CurrentCount==0)
            return;

        try
        {
            await semaphore.WaitAsync();
            //reset question 
            var questionCopy = question;
            question = string.Empty;
            StateHasChanged();

            await session.ChatAsync(questionCopy);
        }
        finally
        {
            semaphore.Release();
        }
    }

    public void Dispose()
    {
        if(session is not null)
            session.ChatMessages.CollectionChanged -= OnChatMessageCollectionChanged;
    }
}