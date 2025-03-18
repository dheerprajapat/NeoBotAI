using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace NeoBotAI.Components.Shared;

public partial class RangeSlider
{
    [Parameter]
    public long Max { get; set; } = 100;
    [Parameter]
    public long Min { get; set; } = 0;
    [Parameter]
    public double Step { get; set; } = 1;
    [Parameter]
    public decimal Value { get; set; } = 0;

    [Parameter]
    public EventCallback<double> ValueChanged { get; set; }

    private ElementReference element;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
            return;
        await Runtime.InvokeVoidAsync("initRangeSlider", DotNetObjectReference.Create(this), element);
    }


    [JSInvokable("onChange")]
    public void OnChange(double value)
    {
        ValueChanged.InvokeAsync(value);
    }
}