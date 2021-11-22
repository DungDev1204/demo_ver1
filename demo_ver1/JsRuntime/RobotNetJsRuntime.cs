using AntDesign.JsInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace demo_ver1.JsRuntime;

public static class RobotNetJsRuntime
{
    public static ValueTask<DomRect> GetBoundingClientRect(this IJSRuntime jsRuntime, ElementReference element)
        => jsRuntime.InvokeAsync<DomRect>("MapViewerGetBoundingClientRectOfElement", element);

    public static ValueTask RegisterEventListenerWindow<T>(this IJSRuntime jsRuntime, DotNetObjectReference<T> objRef, string evt, string evtFunc) where T : class
        => jsRuntime.InvokeVoidAsync("MapViewerAddEventListenerWindow", objRef, evt, evtFunc);

    public static ValueTask ScrollToBottom(this IJSRuntime jsRuntime, ElementReference element)
        => jsRuntime.InvokeVoidAsync("ScrollToBottom", element);

    public static ValueTask<bool> Confirm(this IJSRuntime jsRuntime, string message)
        => jsRuntime.InvokeAsync<bool>("confirm", message);
}

