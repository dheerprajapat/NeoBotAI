using System.Collections.Specialized;
using NeoBotAI.Models;
using NeoBotAI.Session;

namespace NeoBotAI.Components.Pages;

public partial class Chat:IAsyncDisposable
{
    private static UserSession? session;
    string question = string.Empty;

    private  SemaphoreSlim semaphore = new SemaphoreSlim(1);


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


    public async ValueTask DisposeAsync()
    {

        if (session is not null)
        {
            if (session.ChatMessages.Count != 0)
                SessionHub.Instance.AddOrUpdateSession(session);
            session.ChatMessages.CollectionChanged -= OnChatMessageCollectionChanged;
            await session.CloseSessionAsync();
        }
    }
}