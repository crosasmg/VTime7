var HT5AuthorizationToObtainDiscloseInformationUWSupport = new function () {

    this.currentRow = {};
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#HT5AuthorizationToObtainDiscloseInformationUWFormId').val(),
            ClientName: $('#ClientName').val(),
            uwcaseid: $('#uwcaseid').val(),
            AuthorizationObtainDiscloseInformationAcceptanceIndicator: $('input:radio[name=AcceptanceIndicator]:checked').val(),
            AuthorizationObtainDiscloseInformationDateReceived: $('#DateReceived').val() !== '' ? moment($('#DateReceived').val(), 'DD/MM/YYYY').utc().format('YYYY-MM-DD') : moment(new Date('0001-01-01T00:00:00')).utc().format('YYYY-MM-DD')
        };
        return data;
    };

    this.ObjectToInput = function (data) {
        $('#HT5AuthorizationToObtainDiscloseInformationUWFormId').val(data.InstanceFormId);
        $('#ClientName').val(data.ClientName);
        $('#uwcaseid').val(data.uwcaseid);
        if($('input:radio[name=AcceptanceIndicator][value=' + data.AuthorizationObtainDiscloseInformationAcceptanceIndicator +']').length===0)
           $('input:radio[name=AcceptanceIndicator]').prop('checked', false);
        else
           $($('input:radio[name=AcceptanceIndicator][value=' + data.AuthorizationObtainDiscloseInformationAcceptanceIndicator +']')).prop('checked', true);
        $('#AcceptanceIndicator').data('oldValue', data.AuthorizationObtainDiscloseInformationAcceptanceIndicator);
        $('#AcceptanceIndicator').val(data.AuthorizationObtainDiscloseInformationAcceptanceIndicator);

        $('#DateReceived').val(generalSupport.ToJavaScriptDateCustom(data.AuthorizationObtainDiscloseInformationDateReceived, 'DD/MM/YYYY'));



    };

    this.ControlBehaviour = function () {







        $('#DateReceived_group').datetimepicker({
            format: 'DD/MM/YYYY',
            locale: 'es'
        });


    };

    this.ActionProcess = function (data) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
                HT5AuthorizationToObtainDiscloseInformationUWSupport.ObjectToInput(data.d.Data);
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };

    this.Initialization = function () {
        $.LoadingOverlay("show");
        $.ajax({
            type: "POST",
            url: "/fasi/dli/forms/HT5AuthorizationToObtainDiscloseInformationUWActions.aspx/Initialization"+ (window.location.href.split("?")[1] ? "?" + window.location.href.split("?")[1] : ""),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                id: $('#HT5AuthorizationToObtainDiscloseInformationUWFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            success: function (data) {
                $.LoadingOverlay("hide");

                HT5AuthorizationToObtainDiscloseInformationUWSupport.ActionProcess(data);
                
                if (generalSupport.URLStringValue('fromid') !== '' && window.history.replaceState)
                    window.history.replaceState({}, null, '/fasi/dli/forms/HT5AuthorizationToObtainDiscloseInformationUW.aspx?id=' + $('#HT5AuthorizationToObtainDiscloseInformationUWFormId').val());
              
          

            },
            error: function (qXHR, textStatus, errorThrown) {
                $.LoadingOverlay("hide");
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    };




    this.ControlActions = function () {

       $('input:radio[name=AcceptanceIndicator]').change(function () {

                if ($('input:radio[name=AcceptanceIndicator]:checked').val() == 'false'){
                    $('#save').prop('disabled', true);
                    }                    
                    else {
                    $('#save').prop('disabled', false);

                        }


        });
        $('#save').click(function (event) {
            var formInstance = $("#HT5AuthorizationToObtainDiscloseInformationUWMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#save'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5AuthorizationToObtainDiscloseInformationUWActions.aspx/saveClick",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5AuthorizationToObtainDiscloseInformationUWSupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5AuthorizationToObtainDiscloseInformationUWSupport.ActionProcess(data);
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        btnLoading.stop();
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();
    
        $("#HT5AuthorizationToObtainDiscloseInformationUWMainForm").validate({
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
                DateReceived: {
                    required: true
                }
            },
            messages: {
                DateReceived: {
                    required: 'El campo es requerido'
                }
            }
        });

    };





};

$(document).ready(function () {
    moment.locale('es');
    
    if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage)
        masterSupport.setPageTitle('HT5Autorización para obtener información relacionada con su historial médico');
        

    HT5AuthorizationToObtainDiscloseInformationUWSupport.ControlBehaviour();
    HT5AuthorizationToObtainDiscloseInformationUWSupport.ControlActions();
    HT5AuthorizationToObtainDiscloseInformationUWSupport.ValidateSetup();
    HT5AuthorizationToObtainDiscloseInformationUWSupport.Initialization();





});

