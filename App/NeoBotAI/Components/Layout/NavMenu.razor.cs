using Microsoft.AspNetCore.Components;
using NeoBotAI.Models;

namespace NeoBotAI.Components.Layout;

public partial class NavMenu
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private List<NavMenuItem> navMenuItems = [];

    protected override void OnInitialized()
    {
        navMenuItems = [
            new ("Home", "/images/home.svg", "/"),
            new ("Chat", "/images/chat.svg", "/chat"),
            new ("History", "/images/history.svg", "/history"),
            new ("Settings", "/images/settings.svg", "/settings")
        ];
    }

    private void GoToPage(string tabLink)
    {
        NavManager.NavigateTo(tabLink);
    }
}