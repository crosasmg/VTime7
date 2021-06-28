var AuthorizationToObtainDiscloseInformationUWSupport = new function () {

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#AuthorizationToObtainDiscloseInformationUWFormId').val(),
            ClientName: $('#ClientName').val(),
            uwcaseid: $('#uwcaseid').val(),
            AuthorizationObtainDiscloseInformationAcceptanceIndicator: $('input:radio[name=AcceptanceIndicator]:checked').val(),
            AuthorizationObtainDiscloseInformationDateReceived: generalSupport.DatePickerValueInputToObject('#DateReceived')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#AuthorizationToObtainDiscloseInformationUWFormId').val(data.InstanceFormId);
        $('#ClientName').val(data.ClientName);
        $('#uwcaseid').val(data.uwcaseid);
        if($('input:radio[name=AcceptanceIndicator][value=' + data.AuthorizationObtainDiscloseInformationAcceptanceIndicator +']').length===0){
           $('input:radio[name=AcceptanceIndicator]').prop('checked', false);
           $('input:radio[name=AcceptanceIndicator].default').prop('checked', true);
        }
        else
           $($('input:radio[name=AcceptanceIndicator][value=' + data.AuthorizationObtainDiscloseInformationAcceptanceIndicator +']')).prop('checked', true);
        $('#AcceptanceIndicator').data('oldValue', data.AuthorizationObtainDiscloseInformationAcceptanceIndicator);
        $('#AcceptanceIndicator').val(data.AuthorizationObtainDiscloseInformationAcceptanceIndicator);

        $('#DateReceived').val(generalSupport.ToJavaScriptDateCustom(data.AuthorizationObtainDiscloseInformationDateReceived, generalSupport.DateFormat()));



    };

    this.ControlBehaviour = function () {







        $('#DateReceived_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#DateReceived_group');


    };

    this.ActionProcess = function (data, source) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
               if (source == 'Initialization')
					         AuthorizationToObtainDiscloseInformationUWSupport.ObjectToInput(data.d.Data.Instance, source);
				       else
                   AuthorizationToObtainDiscloseInformationUWSupport.ObjectToInput(data.d.Data, source);
            
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };

    this.Initialization = function () {
        app.core.AsyncWebMethod("/fasi/dli/forms/AuthorizationToObtainDiscloseInformationUWActions.aspx/Initialization"+ (window.location.href.split("?")[1] ? "?" + window.location.href.split("?")[1] : ""), true,
            JSON.stringify({
                id: $('#AuthorizationToObtainDiscloseInformationUWFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            function (data) {
                if (data.d.Success)
                    $('#AuthorizationToObtainDiscloseInformationUWFormId').val(data.d.Data.Instance.InstanceFormId);
                    
                if (data.d.Success === true && data.d.Data.LookUps) {
                    data.d.Data.LookUps.forEach(function (elementSource) {
                        generalSupport.RenderLookUp(elementSource.Key, data.d.Data.Instance[elementSource.Key], 'Initialization', elementSource.Items);
                    });
                }
                







                AuthorizationToObtainDiscloseInformationUWSupport.ActionProcess(data, 'Initialization');

                if (generalSupport.URLStringValue('fromid') !== '' && window.history.replaceState)                    
                    window.history.replaceState({}, null, '/fasi/dli/forms/' + location.pathname.substring(location.pathname.lastIndexOf("/") + 1) + '?id=' + $('#AuthorizationToObtainDiscloseInformationUWFormId').val());
 
              
          

            });
    };




    this.ControlActions =   function () {

       $('input:radio[name=AcceptanceIndicator]').change(function () {
                     var data;
                if ($('input:radio[name=AcceptanceIndicator]:checked').val() === 'false'){
                    $('#save').prop('disabled', true);
                    }                    
                    else {
                    $('#save').prop('disabled', false);

                        }


        });
        $('#save').click(function (event) {
                var formInstance = $("#AuthorizationToObtainDiscloseInformationUWMainForm");
                var fvalidate = formInstance.validate();
                
                if (formInstance.valid()) {
                    var btnLoading = Ladda.create(document.querySelector('#save'));
                    btnLoading.start();

                    app.core.AsyncWebMethod("/fasi/dli/forms/AuthorizationToObtainDiscloseInformationUWActions.aspx/saveClick", false,
                          JSON.stringify({
                                        instance: AuthorizationToObtainDiscloseInformationUWSupport.InputToObject()
                          }),
                    function (data) {
                         btnLoading.stop();

                    AuthorizationToObtainDiscloseInformationUWSupport.ActionProcess(data, 'saveClick');
                    },
                    function () {
                         btnLoading.stop();
                    });
               }
                else
                     generalSupport.NotifyErrorValidate(fvalidate);
                     event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();


        $("#AuthorizationToObtainDiscloseInformationUWMainForm").validate({
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
                ClientName: {
                    maxlength: 30
                },
                uwcaseid: {
                    maxlength: 15
                },
                AcceptanceIndicator: {
                },
                DateReceived: {
                    required: true,
                    DatePicker: true
                }
            },
            messages: {
                ClientName: {
                    maxlength: 'El campo permite 30 caracteres máximo'
                },
                uwcaseid: {
                    maxlength: 'El campo permite 15 caracteres máximo'
                },
                AcceptanceIndicator: {
                },
                DateReceived: {
                    required: 'El campo es requerido',
                    DatePicker: 'La fecha indicada no es válida'
                }
            }
        });

    };











  this.Init = function(){
    
    moment.locale(generalSupport.UserContext().languageName);
    
    if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage)
        masterSupport.setPageTitle('Autorización para obtener información relacionada con su historial médico');
        

    AuthorizationToObtainDiscloseInformationUWSupport.ControlBehaviour();
    AuthorizationToObtainDiscloseInformationUWSupport.ControlActions();
    AuthorizationToObtainDiscloseInformationUWSupport.ValidateSetup();
    AuthorizationToObtainDiscloseInformationUWSupport.Initialization();


  };
};

$(document).ready(function () {
   AuthorizationToObtainDiscloseInformationUWSupport.Init();
});

