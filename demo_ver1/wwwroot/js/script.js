window.MapViewerGetBoundingClientRectOfElement = (element) => {
    return element.getBoundingClientRect();
}

window.MapViewerAddEventListenerWindow = (dotNetHelper, evt, evtfunc) => {
    window.addEventListener(evt, function () {
        dotNetHelper.invokeMethodAsync(evtfunc);
    });
}

window.ScrollToBottom = (objDiv) => {
    if (objDiv.scrollTop != objDiv.scrollHeight) {
        objDiv.scrollTop = objDiv.scrollHeight;
    }
}