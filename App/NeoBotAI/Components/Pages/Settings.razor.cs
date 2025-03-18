using Microsoft.AspNetCore.Components;
using NeoBotAI.Models;

namespace NeoBotAI.Components.Pages;

public partial class Settings
{
    private void OnSliderValueChanged(double value)
    {
        AppSettings.Instance.Temperature.Value = value;
    }

    private void UpdateUserName(ChangeEventArgs e)
    {
        if(e != null)
            AppSettings.Instance.UserName.Value = e.Value!;
    }
}