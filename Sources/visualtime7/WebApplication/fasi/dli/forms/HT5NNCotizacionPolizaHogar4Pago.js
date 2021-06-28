var HT5NNCotizacionPolizaHogar4PagoSupport = new function () {

    this.currentRow = {};
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#HT5NNCotizacionPolizaHogar4PagoFormId').val(),
            RiskInformationEffectiveDate: $('#EffectiveDate').val() !== '' ? moment($('#EffectiveDate').val(), 'DD/MM/YYYY').utc().format('YYYY-MM-DD') : moment(new Date('0001-01-01T00:00:00')).utc().format('YYYY-MM-DD'),
            RiskInformationProductCode: parseInt(0 + $('#ProductCode').val(), 10),
            RiskInformationLineOfBusiness: parseInt(0 + $('#LineOfBusiness').val(), 10),
            RiskInformationAutomaticPaymentPolicyCreditCardType: $('input:radio[name=CreditCardType]:checked').val(),
            RiskInformationAutomaticPaymentPolicyBankCode: parseInt(0 + $('#BankCode').val(), 10),
            RiskInformationAutomaticPaymentPolicyCreditCardNumber: $('#CreditCardNumber').val(),
            LocalMonth: AutoNumeric.getNumber('#LocalMonth'),
            RiskInformationAutomaticPaymentPolicyYear: AutoNumeric.getNumber('#Year'),
            RiskInformationAutomaticPaymentPolicyAuthorizationNumber: AutoNumeric.getNumber('#AuthorizationNumber')
        };
        return data;
    };

    this.ObjectToInput = function (data) {
        $('#HT5NNCotizacionPolizaHogar4PagoFormId').val(data.InstanceFormId);
        $('#EffectiveDate').val(generalSupport.ToJavaScriptDateCustom(data.RiskInformationEffectiveDate, 'DD/MM/YYYY'));
        $('#uwcaseid').html(data.uwcaseid);
        HT5NNCotizacionPolizaHogar4PagoSupport.LookUpForCreditCardType(data.RiskInformationAutomaticPaymentPolicyCreditCardType);

        $('#CreditCardNumber').val(data.RiskInformationAutomaticPaymentPolicyCreditCardNumber);
        AutoNumeric.set('#LocalMonth', data.LocalMonth);
        AutoNumeric.set('#Year', data.RiskInformationAutomaticPaymentPolicyYear);
        AutoNumeric.set('#AuthorizationNumber', data.RiskInformationAutomaticPaymentPolicyAuthorizationNumber);

        HT5NNCotizacionPolizaHogar4PagoSupport.LookUpForLineOfBusiness(data.RiskInformationLineOfBusiness);
        HT5NNCotizacionPolizaHogar4PagoSupport.LookUpForBankCode(data.RiskInformationAutomaticPaymentPolicyBankCode);
        HT5NNCotizacionPolizaHogar4PagoSupport.LookUpForProductCode(data.RiskInformationProductCode, data.RiskInformationLineOfBusiness);


    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#LocalMonth', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      new AutoNumeric('#Year', {
            decimalCharacter: ",",
            digitGroupSeparator: "",
            maximumValue: "9999",
            decimalPlaces: 0,
            minimumValue: "-9999"
        });
      new AutoNumeric('#AuthorizationNumber', {
            decimalCharacter: ",",
            digitGroupSeparator: "",
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "0"
        });



        $('#LineOfBusiness').on('change', function () {
            var value = $('#LineOfBusiness').val();

            if (value !== null && value !== '0') {
                var skipData = $('#LineOfBusiness').data("skip");

                if (skipData !== undefined && skipData === true)
                    $('#LineOfBusiness').data("skip", false);
                else
                    HT5NNCotizacionPolizaHogar4PagoSupport.LookUpForProductCode(null, parseInt(0 + $('#LineOfBusiness').val(), 10));
            }
            else
                if($('#LineOfBusiness').val() !== $('#ProductCode').data("parentId1"))
                   $('#ProductCode').children().remove();
        });

        $('#EffectiveDate_group').datetimepicker({
            format: 'DD/MM/YYYY',
            locale: 'es'
        });


    };

    this.ActionProcess = function (data) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
                HT5NNCotizacionPolizaHogar4PagoSupport.ObjectToInput(data.d.Data);
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
            url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogar4PagoActions.aspx/Initialization"+ (window.location.href.split("?")[1] ? "?" + window.location.href.split("?")[1] : ""),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                id: $('#HT5NNCotizacionPolizaHogar4PagoFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            success: function (data) {
                $.LoadingOverlay("hide");

                HT5NNCotizacionPolizaHogar4PagoSupport.ActionProcess(data);
                
                if (generalSupport.URLStringValue('fromid') !== '' && window.history.replaceState)
                    window.history.replaceState({}, null, '/fasi/dli/forms/HT5NNCotizacionPolizaHogar4Pago.aspx?id=' + $('#HT5NNCotizacionPolizaHogar4PagoFormId').val());
              
          

            },
            error: function (qXHR, textStatus, errorThrown) {
                $.LoadingOverlay("hide");
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    };




    this.ControlActions = function () {

        $('#buttonGPago').click(function (event) {
            var formInstance = $("#HT5NNCotizacionPolizaHogar4PagoMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#buttonGPago'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogar4PagoActions.aspx/buttonGPagoClick",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5NNCotizacionPolizaHogar4PagoSupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5NNCotizacionPolizaHogar4PagoSupport.ActionProcess(data);
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
    
        $("#HT5NNCotizacionPolizaHogar4PagoMainForm").validate({
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
                ProductCode: {
                    required: true
                },
                LineOfBusiness: {
                    required: true
                },
                CreditCardType: {
                    required: true
                },
                BankCode: {
                    required: true
                },
                CreditCardNumber: {
                    required: true
                },
                LocalMonth: {
                    AutoNumericRequired: true                },
                Year: {
                    AutoNumericRequired: true                },
                AuthorizationNumber: {
                    AutoNumericRequired: true                }
            },
            messages: {
                ProductCode: {
                    required: 'El campo es requerido.'
                },
                LineOfBusiness: {
                    required: 'El campo es requerido.'
                },
                CreditCardType: {
                    required: 'El campo es requerido'
                },
                BankCode: {
                    required: 'El campo es requerido'
                },
                CreditCardNumber: {
                    required: 'El campo es requerido'
                },
                LocalMonth: {
                    AutoNumericRequired: 'El campo es requerido'                },
                Year: {
                    AutoNumericRequired: 'El campo es requerido'                },
                AuthorizationNumber: {
                    AutoNumericRequired: 'El campo es requerido'                }
            }
        });

    };
    this.LookUpForProductCode = function (defaultValue, value1) {
        var ctrol = $('#ProductCode');
        var parentId1 = ctrol.data("parentId1");
        
        if (typeof parentId1 == 'undefined' || parentId1 !== value1) {
            ctrol.data("parentId1", value1);

            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogar4PagoActions.aspx/LookUpForProductCode",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({
                    id: $('#HT5NNCotizacionPolizaHogar4PagoFormId').val(),
                    RiskInformationLineOfBusiness: value1
                }),
                success: function (data) {
                    ctrol.children().remove();
                    if (data.d.Success === true) {
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue).change();
                        else
                            ctrol.val(0).change();
                            
                            
                    }
                    else
                        generalSupport.NotifyFail(data.d.Reason, data.d.Code);
                },
                error: function (qXHR, textStatus, errorThrown) {
                    generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                }
            });
        }
        else
            if (typeof defaultValue !== 'undefined' && defaultValue !== null)
                if (defaultValue.toString() !== (ctrol.val() || '0').toString())
                    ctrol.val(defaultValue).change();
    };
    this.LookUpForLineOfBusiness = function (defaultValue) {
        var ctrol = $('#LineOfBusiness');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogar4PagoActions.aspx/LookUpForLineOfBusiness",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5NNCotizacionPolizaHogar4PagoFormId').val()}),
                
                success: function (data) {
                    ctrol.children().remove();
                    if (data.d.Success === true) {
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue).change();
                        else
                            ctrol.val(0).change();
                            
                            
                    }
                    else
                        generalSupport.NotifyFail(data.d.Reason, data.d.Code);
                },
                error: function (qXHR, textStatus, errorThrown) {
                    generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                }
            });
        }
        else
            if (typeof defaultValue !== 'undefined' && defaultValue !== null)
                if (defaultValue.toString() !== (ctrol.val() || '0').toString())
                    ctrol.val(defaultValue).change();
    };
    this.LookUpForCreditCardType = function (defaultValue) {
        var ctrol = $('#CreditCardType_Dynamic');
        
        if (ctrol.children().length === 0) {
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogar4PagoActions.aspx/LookUpForCreditCardType",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5NNCotizacionPolizaHogar4PagoFormId').val()}),
                
                success: function (data) {
                    ctrol.children().remove();
                    if (data.d.Success === true) {
                        $.each(data.d.Data, function () {
                            ctrol.append("<div class='radio'><label><input type='radio' name='CreditCardType' id='CreditCardType_" + this['Code'] + "' value='" + this['Code'] + "'/>" + this['Description'] + "</label></div>");
                        });
                        if (defaultValue !== null)
                            $($('input:radio[name=CreditCardType][value=' + defaultValue + ']')).prop('checked', true);
                    }
                    else
                        generalSupport.NotifyFail(data.d.Reason, data.d.Code);
                },
                error: function (qXHR, textStatus, errorThrown) {
                    generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                }
            });
        }
        else
            if (typeof defaultValue !== 'undefined' && defaultValue !== null)
                if (defaultValue.toString() !== ctrol.val())
                    $($('input:radio[name=CreditCardType][value=' + defaultValue + ']')).prop('checked', true);
    };
    this.LookUpForBankCode = function (defaultValue) {
        var ctrol = $('#BankCode');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogar4PagoActions.aspx/LookUpForBankCode",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5NNCotizacionPolizaHogar4PagoFormId').val()}),
                
                success: function (data) {
                    ctrol.children().remove();
                    if (data.d.Success === true) {
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue).change();
                        else
                            ctrol.val(0).change();
                            
                            
                    }
                    else
                        generalSupport.NotifyFail(data.d.Reason, data.d.Code);
                },
                error: function (qXHR, textStatus, errorThrown) {
                    generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                }
            });
        }
        else
            if (typeof defaultValue !== 'undefined' && defaultValue !== null)
                if (defaultValue.toString() !== (ctrol.val() || '0').toString())
                    ctrol.val(defaultValue).change();
    };





};

$(document).ready(function () {
    moment.locale('es');
    
    if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage)
        masterSupport.setPageTitle('HT5Póliza Hogar Seguro');
        

    HT5NNCotizacionPolizaHogar4PagoSupport.ControlBehaviour();
    HT5NNCotizacionPolizaHogar4PagoSupport.ControlActions();
    HT5NNCotizacionPolizaHogar4PagoSupport.ValidateSetup();
    HT5NNCotizacionPolizaHogar4PagoSupport.Initialization();





});

