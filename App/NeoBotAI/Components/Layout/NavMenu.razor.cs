using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using NeoBotAI.Models;

namespace NeoBotAI.Components.Layout;

public partial class NavMenu
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private List<NavMenuItem> navMenuItems = [];

    public static NavMenu? Instance;

    protected override void OnInitialized()
    {
        navMenuItems = [
            new ("Home", "/images/home.svg", "/"),
            new ("Chat", "/images/chat.svg", "/chat"),
            new ("History", "/images/history.svg", "/history"),
            new ("Settings", "/images/settings.svg", "/settings")
        ];

        Instance = this;
    }

    private void GoToPage(string tabLink)
    {
        NavManager.NavigateTo(tabLink);
    }

    public async ValueTask GoToTab(string url)
    {
        var item = navMenuItems.FirstOrDefault(x => x.TabLink == url);

        if (item == null)
            return;

        await Runtime.InvokeVoidAsync("clickTab", "tab-button-" + item.TabName);
    }
}