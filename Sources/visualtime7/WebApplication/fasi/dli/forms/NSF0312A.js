var NSF0312ASupport = new function () {

    this.currentRow = {};
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#NSF0312AFormId').val(),
            Parameter1: generalSupport.NumericValue('#Parameter1', -99999, 99999),
            Parameter3: $('#Parameter3').val()
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        $('#NSF0312AFormId').val(data.InstanceFormId);
        AutoNumeric.set('#Parameter1', data.Parameter1);
        $('#Parameter3').val(data.Parameter3);



    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#Parameter1', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });






    };

    this.ActionProcess = function (data, source) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
                NSF0312ASupport.ObjectToInput(data.d.Data, source);
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };

    this.Initialization = function () {
        app.core.AsyncWebMethod("/fasi/dli/forms/NSF0312AActions.aspx/Initialization"+ (window.location.href.split("?")[1] ? "?" + window.location.href.split("?")[1] : ""), true,
            JSON.stringify({
                id: $('#NSF0312AFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            function (data) {

                NSF0312ASupport.ActionProcess(data, 'Initialization');

                if (generalSupport.URLStringValue('fromid') !== '' && window.history.replaceState)
                    window.history.replaceState({}, null, '/fasi/dli/forms/NSF0312A.aspx?id=' + $('#NSF0312AFormId').val());
              
          

            });
    };




    this.ControlActions = function () {

        $('#Parameter3').change(function () {

            var errors;
                notification.alert.info('', 'eeeee');


        });
        $('#button1').click(function (event) {
                var formInstance = $("#NSF0312AMainForm");
                var fvalidate = formInstance.validate();
                
                if (formInstance.valid()) {
                    var btnLoading = Ladda.create(document.querySelector('#button1'));
                    btnLoading.start();

                    app.core.AsyncWebMethod("/fasi/dli/forms/NSF0312AActions.aspx/button1Click", false,
                          JSON.stringify({
                                        instance: NSF0312ASupport.InputToObject()
                          }),
                    function (data) {
                         btnLoading.stop();

                    NSF0312ASupport.ActionProcess(data, 'button1Click');
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
    
        $("#NSF0312AMainForm").validate({
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
                Parameter1: {
                    AutoNumericMinValue: -99999,
                    AutoNumericMaxValue: 99999
                },
                Parameter3: {
                    maxlength: 15
                }
            },
            messages: {
                Parameter1: {
                    AutoNumericMinValue: 'El valor mínimo permitido es -99999',
                    AutoNumericMaxValue: 'El valor máximo permitido es 99999'
                },
                Parameter3: {
                    maxlength: 'El campo permite 15 caracteres máximo'
                }
            }
        });

    };





};

$(document).ready(function () {
    moment.locale(generalSupport.UserContext().languageName);
    
    if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage)
        masterSupport.setPageTitle('NSF0312A');
        

    NSF0312ASupport.ControlBehaviour();
    NSF0312ASupport.ControlActions();
    NSF0312ASupport.ValidateSetup();
    NSF0312ASupport.Initialization();





});

