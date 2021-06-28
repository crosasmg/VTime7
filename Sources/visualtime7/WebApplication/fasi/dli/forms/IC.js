var ICSupport = new function () {
    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];

    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#ICFormId').val(),
            Name: $('#Name').val()
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#ICFormId').val(data.InstanceFormId);
        $('#Name').val(data.Name);
    };

    this.ControlBehaviour = function () {
    };

    this.ActionProcess = function (data, source) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
                ICSupport.ObjectToInput(data.d.Data.Instance, source);

            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };

    this.Initialization = function () {
        app.core.AsyncWebMethod("/fasi/dli/forms/ICActions.aspx/Initialization" + (window.location.href.split("?")[1] ? "?" + window.location.href.split("?")[1] : ""), true,
            JSON.stringify({
                id: $('#ICFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            function (data) {
                if (data.d.Success)
                    $('#ICFormId').val(data.d.Data.Instance.InstanceFormId);

                ICSupport.CallRenderLookUps(data);

                ICSupport.ActionProcess(data, 'Initialization');

                if (generalSupport.URLStringValue('fromid') !== '' && window.history.replaceState)
                    window.history.replaceState({}, null, '/fasi/dli/forms/' + location.pathname.substring(location.pathname.lastIndexOf("/") + 1) + '?id=' + $('#ICFormId').val());
            });
    };

    this.CallRenderLookUps = function (data) {
        if (data.d.Success === true && data.d.Data.LookUps) {
            data.d.Data.LookUps.forEach(function (elementSource) {
                generalSupport.RenderLookUp(elementSource.Key, data.d.Data.Instance[elementSource.Key], 'Initialization', elementSource.Items, false);
            });
        }
    };

    this.ControlActions = function () {
        $('#button0').click(function (event) {
            var formInstance = $("#ICMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#button0'));
                btnLoading.start();
                data = app.core.SyncWebMethod("/fasi/dli/forms/ICActions.aspx/button01SelectCommandActionUSERMEMBER", false,
                    JSON.stringify({}));
                btnLoading.stop();
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();

        $("#ICMainForm").validate({
            errorPlacement: function (error, element) {
                var name = $(element).attr("name");
                var $obj = $("#" + name + "_validate");
                if ($obj.length) {
                    error.appendTo($obj);
                }
                else {
                    error.insertAfter(element);
                }
            },

            rules: {
                Name: {
                    maxlength: 15
                }
            },
            messages: {
                Name: {
                    maxlength: 'El campo permite 15 caracteres máximo'
                }
            }
        });
    };

    this.Init = function () {
        moment.locale(app.user.languageName);

        if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage)
            masterSupport.setPageTitle('IC');

        ICSupport.ControlBehaviour();
        ICSupport.ControlActions();
        ICSupport.ValidateSetup();

        ICSupport.Initialization();
    };
};

$(document).ready(function () {
    app.security.PageSetup({
        Pathname: window.location.pathname,
        roles: [],
        Element: $("#ICMainForm"),
        CallBack: ICSupport.Init
    });
});