var HT5NNCotizacionPolizaHogarFinalSupport = new function () {

    this.currentRow = {};
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#HT5NNCotizacionPolizaHogarFinalFormId').val()
        };
        return data;
    };

    this.ObjectToInput = function (data) {
        $('#HT5NNCotizacionPolizaHogarFinalFormId').val(data.InstanceFormId);



    };

    this.ControlBehaviour = function () {









    };

    this.ActionProcess = function (data) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
                HT5NNCotizacionPolizaHogarFinalSupport.ObjectToInput(data.d.Data);
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
            url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogarFinalActions.aspx/Initialization"+ (window.location.href.split("?")[1] ? "?" + window.location.href.split("?")[1] : ""),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                id: $('#HT5NNCotizacionPolizaHogarFinalFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            success: function (data) {
                $.LoadingOverlay("hide");

                HT5NNCotizacionPolizaHogarFinalSupport.ActionProcess(data);
                
                if (generalSupport.URLStringValue('fromid') !== '' && window.history.replaceState)
                    window.history.replaceState({}, null, '/fasi/dli/forms/HT5NNCotizacionPolizaHogarFinal.aspx?id=' + $('#HT5NNCotizacionPolizaHogarFinalFormId').val());
              
          

            },
            error: function (qXHR, textStatus, errorThrown) {
                $.LoadingOverlay("hide");
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    };




    this.ControlActions = function () {


    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();
    
        $("#HT5NNCotizacionPolizaHogarFinalMainForm").validate({
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

            },
            messages: {

            }
        });

    };





};

$(document).ready(function () {
    moment.locale('es');
    
    if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage)
        masterSupport.setPageTitle('HT5Póliza Hogar Seguro - Final');
        

    HT5NNCotizacionPolizaHogarFinalSupport.ControlBehaviour();
    HT5NNCotizacionPolizaHogarFinalSupport.ControlActions();
    HT5NNCotizacionPolizaHogarFinalSupport.ValidateSetup();
    HT5NNCotizacionPolizaHogarFinalSupport.Initialization();





});

