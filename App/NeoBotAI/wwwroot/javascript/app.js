window.calculateBoundingBox = function (viewfinderWidth, viewfinderHeight) {
    let minEdgePercentage = 0.999;
    let minEdgeSize = Math.min(viewfinderWidth, viewfinderHeight);
    let qrboxSize = Math.floor(minEdgeSize * minEdgePercentage);
    return {
        width: qrboxSize,
        height: 150,
    };
};

window.initRangeSlider = function (dotnet, slider) {
    slider.addEventListener('ionInput', ({ detail }) => {
        dotnet.invokeMethodAsync("onChange", detail.value);
    });
}

window.clickTab = (el)=>
{
    let e = document.getElementById(el);
    if (e == undefined || e == null)
        return;

    e.click();
};