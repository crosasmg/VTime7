var HT5NNCotizacionPolizaHogarSecuenciaSupport = new function () {

    this.currentRow = {};
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#HT5NNCotizacionPolizaHogarSecuenciaFormId').val(),
            eMail: $('#eMail').val()
        };
        return data;
    };

    this.ObjectToInput = function (data) {
        $('#HT5NNCotizacionPolizaHogarSecuenciaFormId').val(data.InstanceFormId);
        $('#eMail').val(data.eMail);



    };

    this.ControlBehaviour = function () {









    };

    this.ActionProcess = function (data) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
                HT5NNCotizacionPolizaHogarSecuenciaSupport.ObjectToInput(data.d.Data);
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
            url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogarSecuenciaActions.aspx/Initialization"+ (window.location.href.split("?")[1] ? "?" + window.location.href.split("?")[1] : ""),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                id: $('#HT5NNCotizacionPolizaHogarSecuenciaFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            success: function (data) {
                $.LoadingOverlay("hide");

                HT5NNCotizacionPolizaHogarSecuenciaSupport.ActionProcess(data);
                
                if (generalSupport.URLStringValue('fromid') !== '' && window.history.replaceState)
                    window.history.replaceState({}, null, '/fasi/dli/forms/HT5NNCotizacionPolizaHogarSecuencia.aspx?id=' + $('#HT5NNCotizacionPolizaHogarSecuenciaFormId').val());
              
          

            },
            error: function (qXHR, textStatus, errorThrown) {
                $.LoadingOverlay("hide");
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    };




    this.ControlActions = function () {

        $('#button31').click(function (event) {
                var btnLoading = Ladda.create(document.querySelector('#button31'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogarSecuenciaActions.aspx/button31Click",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5NNCotizacionPolizaHogarSecuenciaSupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5NNCotizacionPolizaHogarSecuenciaSupport.ActionProcess(data);
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        btnLoading.stop();
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
            event.preventDefault();
        });
        $('#button34').click(function (event) {
                var btnLoading = Ladda.create(document.querySelector('#button34'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogarSecuenciaActions.aspx/button34Click",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5NNCotizacionPolizaHogarSecuenciaSupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5NNCotizacionPolizaHogarSecuenciaSupport.ActionProcess(data);
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        btnLoading.stop();
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
            event.preventDefault();
        });
        $('#button32').click(function (event) {
                var btnLoading = Ladda.create(document.querySelector('#button32'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogarSecuenciaActions.aspx/button32Click",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5NNCotizacionPolizaHogarSecuenciaSupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5NNCotizacionPolizaHogarSecuenciaSupport.ActionProcess(data);
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        btnLoading.stop();
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
            event.preventDefault();
        });
        $('#button36').click(function (event) {
                var btnLoading = Ladda.create(document.querySelector('#button36'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogarSecuenciaActions.aspx/button36Click",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5NNCotizacionPolizaHogarSecuenciaSupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5NNCotizacionPolizaHogarSecuenciaSupport.ActionProcess(data);
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        btnLoading.stop();
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
            event.preventDefault();
        });
        $('#button19').click(function (event) {
                var btnLoading = Ladda.create(document.querySelector('#button19'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogarSecuenciaActions.aspx/button19Click",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5NNCotizacionPolizaHogarSecuenciaSupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5NNCotizacionPolizaHogarSecuenciaSupport.ActionProcess(data);
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        btnLoading.stop();
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
            event.preventDefault();
        });
        $('#EnviarCotizacionEmail').click(function (event) {
                var btnLoading = Ladda.create(document.querySelector('#EnviarCotizacionEmail'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogarSecuenciaActions.aspx/EnviarCotizacionEmailClick",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5NNCotizacionPolizaHogarSecuenciaSupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5NNCotizacionPolizaHogarSecuenciaSupport.ActionProcess(data);
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        btnLoading.stop();
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
            event.preventDefault();
        });
        $('#eMail').change(function () {
         if ($('#eMail').val() !== null && $('#eMail').val() !== $('#eMail').data('oldValue')) {
             $('#eMail').data('oldValue', $('#eMail').val() );             
           $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogarSecuenciaActions.aspx/eMailChange",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({
                    instance: HT5NNCotizacionPolizaHogarSecuenciaSupport.InputToObject()
                }),
                success: function (data) {
                    HT5NNCotizacionPolizaHogarSecuenciaSupport.ActionProcess(data);
                },
                error: function (qXHR, textStatus, errorThrown) {
                    generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                }
            });
      }          
    });
        $('#Accept').click(function (event) {
            var formInstance = $("#HT5NNCotizacionPolizaHogarSecuenciaMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#Accept'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogarSecuenciaActions.aspx/AcceptClick",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5NNCotizacionPolizaHogarSecuenciaSupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5NNCotizacionPolizaHogarSecuenciaSupport.ActionProcess(data);
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
        $('#btnSalirSinGuardar').click(function (event) {
            var formInstance = $("#HT5NNCotizacionPolizaHogarSecuenciaMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#btnSalirSinGuardar'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogarSecuenciaActions.aspx/btnSalirSinGuardarClick",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5NNCotizacionPolizaHogarSecuenciaSupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5NNCotizacionPolizaHogarSecuenciaSupport.ActionProcess(data);
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
    
        $("#HT5NNCotizacionPolizaHogarSecuenciaMainForm").validate({
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
                eMail: {
                    required: true
                }
            },
            messages: {
                eMail: {
                    required: 'El campo es requerido'
                }
            }
        });

    };





};

$(document).ready(function () {
    moment.locale('es');
    
    if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage)
        masterSupport.setPageTitle('HT5Póliza Hogar Seguro - Secuencia');
        

    HT5NNCotizacionPolizaHogarSecuenciaSupport.ControlBehaviour();
    HT5NNCotizacionPolizaHogarSecuenciaSupport.ControlActions();
    HT5NNCotizacionPolizaHogarSecuenciaSupport.ValidateSetup();
    HT5NNCotizacionPolizaHogarSecuenciaSupport.Initialization();





});

