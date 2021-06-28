var iframeSupport = new function () {
    this.load = function (widgetData) {
        if (widgetData.State && widgetData.State != null) {
            var state = JSON.parse(widgetData.State);            
            $('#' + widgetData.Id + ' .ibox-content #iFrameWidget').prop('src', state.url + '?state=' + widgetData.State);
        }
    };
};