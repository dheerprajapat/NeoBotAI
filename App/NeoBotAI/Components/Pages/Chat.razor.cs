using System.Collections.Specialized;
using NeoBotAI.Models;
using NeoBotAI.Session;

namespace NeoBotAI.Components.Pages;

public partial class Chat:IAsyncDisposable
{
    private static UserSession? session;
    string question = string.Empty;

    private SemaphoreSlim chatSemaphore = new SemaphoreSlim(1);

    private static SemaphoreSlim pageRenderSemaphoreSlim = new SemaphoreSlim(1,1);

    private bool showProgress = false;

    protected override async Task OnInitializedAsync()
    {
        if (pageRenderSemaphoreSlim.CurrentCount==0)
        {
            return;
        }

        try
        {
            await pageRenderSemaphoreSlim.WaitAsync();

            session = SessionManager.Current;

            //create a new session
            if (session == null)
            {
                SessionManager.Current = session = await SessionManager.CreateSessionAsync("i510.json", "");
            }
            else
            {
                SessionManager.Current = session = await SessionManager.ResumeSessionAsync(session);
            }

            if (session != null)
            {
                session.ChatMessages.CollectionChanged += OnChatMessageCollectionChanged;
            }
        }
        finally
        {
            pageRenderSemaphoreSlim.Release();
        }

        StateHasChanged();
    }

    void OnChatMessageCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        StateHasChanged();
    }

    async Task ChatAsync()
    {
        if (session == null || string.IsNullOrWhiteSpace(question) || chatSemaphore.CurrentCount==0)
            return;

        try
        {
            await chatSemaphore.WaitAsync();
            //reset question 
            var questionCopy = question;
            question = string.Empty;
            showProgress = true;
            StateHasChanged();

            await session.ChatAsync(questionCopy);
        }
        finally
        {
            chatSemaphore.Release();
            showProgress = false;
            StateHasChanged();
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

        if (pageRenderSemaphoreSlim.CurrentCount == 0)
            pageRenderSemaphoreSlim.Release();
    }
}