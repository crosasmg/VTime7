var widgetsPopupSupport = new function () {

    // Carga los widgets disponibles al usuario
    this.load = function () {
        var availableWidgets = $('#availableWidgets');

        $.LoadingOverlay("show");
        // Obtiene la lista de widgets disponibles
        ajaxJsonHelper.get(constants.fasiApi.fasi + 'UserAllowWidgets?userId=' + masterSupport.user.userId + '&languageId=' + generalSupport.LanguageId(), null,
            function (data) {
                $.LoadingOverlay("hide");
                $.each(data.Data, function (index, item) {
                    // Agrega el html abajo a cada widget
                    var template = ('<div class="infont col-md-6" title="@@Title@@" >' +
                        '<a href="javascript:widgetsPopupSupport.addWidget(' + item.Id + ');">' +
                        '<i class="' + item.Icon + '"></i> ' +
                        '<span>' + item.Title + '</span>' +
                        '</a>' +
                        '</div>').replace("@@Title@@", item.Title + ' (' + item.Url + ')');
                    availableWidgets.append(template);
                });
            },
            function (qXHR, textStatus, errorThrown) {
                $.LoadingOverlay("hide");
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            });       
    };

    // Agrega widget en la página seleccionada
    this.addWidget = function (idWidget) {
        $.LoadingOverlay("show");
        ajaxJsonHelper.post(constants.fasiApi.fasi + 'WidgetAddInPage?pageId=' + $('.metismenu li.active').prop('id') + '&widgetId=' + idWidget + '&languageId=' + generalSupport.LanguageId(), null,
            function (data) {
                $.LoadingOverlay("hide");

                if (data.Successfully) {    
                    var key = 'Page_' + $('.metismenu li.active').prop('id') + '_' + generalSupport.LanguageId();
                    localStorage.removeItem(key);
                    // Llama el método que agrega el widget en el GridStack
                    defaultPage.addWidget(data.Data);

                    toastr.success(dict.AddWidgetSuccess[generalSupport.LanguageName()], '', { positionClass: "toast-bottom-right" });
                }
            },
            function (qXHR, textStatus, errorThrown) {
                $.LoadingOverlay("hide");
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            });      
    };
}

$(document).ready(function () {
    widgetsPopupSupport.load();
});