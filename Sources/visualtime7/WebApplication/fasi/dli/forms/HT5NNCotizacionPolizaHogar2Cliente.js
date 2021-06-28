var HT5NNCotizacionPolizaHogar2ClienteSupport = new function () {

    this.currentRow = {};
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#HT5NNCotizacionPolizaHogar2ClienteFormId').val(),
            RiskInformationEffectiveDate: $('#EffectiveDate').val() !== '' ? moment($('#EffectiveDate').val(), 'DD/MM/YYYY').utc().format('YYYY-MM-DD') : moment(new Date('0001-01-01T00:00:00')).utc().format('YYYY-MM-DD'),
            RiskInformationProductCode: parseInt(0 + $('#ProductCode').val(), 10),
            RiskInformationLineOfBusiness: parseInt(0 + $('#LineOfBusiness').val(), 10),
            RiskInformationPrimaryInsuredClientClientID: $('#ClientID').val(),
            RiskInformationPrimaryInsuredClientBirthDate: $('#BirthDate').val() !== '' ? moment($('#BirthDate').val(), 'DD/MM/YYYY').utc().format('YYYY-MM-DD') : moment(new Date('0001-01-01T00:00:00')).utc().format('YYYY-MM-DD'),
            RiskInformationPrimaryInsuredClientFirstName: $('#FirstName').val(),
            RiskInformationPrimaryInsuredClientLastName: $('#LastName').val(),
            RiskInformationPrimaryInsuredClientLastName2: $('#LastName2').val(),
            AddressPhysicalAddressDLI: AddressSupport.GetLocalAddressBySelector("physicaladdress0"),
            AddresseMailDLIeMailAddresseMail: $('#eMailCliente').val()
        };
        return data;
    };

    this.ObjectToInput = function (data) {
        $('#HT5NNCotizacionPolizaHogar2ClienteFormId').val(data.InstanceFormId);
        $('#EffectiveDate').val(generalSupport.ToJavaScriptDateCustom(data.RiskInformationEffectiveDate, 'DD/MM/YYYY'));
        $('#uwcaseid').html(data.uwcaseid);
        $('#ClientID').val(data.RiskInformationPrimaryInsuredClientClientID);
        $('#BirthDate').val(generalSupport.ToJavaScriptDateCustom(data.RiskInformationPrimaryInsuredClientBirthDate, 'DD/MM/YYYY'));
        $('#FirstName').val(data.RiskInformationPrimaryInsuredClientFirstName);
        $('#LastName').val(data.RiskInformationPrimaryInsuredClientLastName);
        $('#LastName2').val(data.RiskInformationPrimaryInsuredClientLastName2);
        AddressSupport.Initialization('physicaladdress0', data.AddressPhysicalAddressDLI, true, false);
        $('#eMailCliente').val(data.AddresseMailDLIeMailAddresseMail);

        HT5NNCotizacionPolizaHogar2ClienteSupport.LookUpForLineOfBusiness(data.RiskInformationLineOfBusiness);
        HT5NNCotizacionPolizaHogar2ClienteSupport.LookUpForProductCode(data.RiskInformationProductCode, data.RiskInformationLineOfBusiness);


    };

    this.ControlBehaviour = function () {






        $('#LineOfBusiness').on('change', function () {
            var value = $('#LineOfBusiness').val();

            if (value !== null && value !== '0') {
                var skipData = $('#LineOfBusiness').data("skip");

                if (skipData !== undefined && skipData === true)
                    $('#LineOfBusiness').data("skip", false);
                else
                    HT5NNCotizacionPolizaHogar2ClienteSupport.LookUpForProductCode(null, parseInt(0 + $('#LineOfBusiness').val(), 10));
            }
            else
                if($('#LineOfBusiness').val() !== $('#ProductCode').data("parentId1"))
                   $('#ProductCode').children().remove();
        });

        $('#EffectiveDate_group').datetimepicker({
            format: 'DD/MM/YYYY',
            locale: 'es'
        });
        $('#BirthDate_group').datetimepicker({
            format: 'DD/MM/YYYY',
            locale: 'es'
        });


    };

    this.ActionProcess = function (data) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
                HT5NNCotizacionPolizaHogar2ClienteSupport.ObjectToInput(data.d.Data);
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
            url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogar2ClienteActions.aspx/Initialization"+ (window.location.href.split("?")[1] ? "?" + window.location.href.split("?")[1] : ""),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                id: $('#HT5NNCotizacionPolizaHogar2ClienteFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            success: function (data) {
                $.LoadingOverlay("hide");

                HT5NNCotizacionPolizaHogar2ClienteSupport.ActionProcess(data);
                
                if (generalSupport.URLStringValue('fromid') !== '' && window.history.replaceState)
                    window.history.replaceState({}, null, '/fasi/dli/forms/HT5NNCotizacionPolizaHogar2Cliente.aspx?id=' + $('#HT5NNCotizacionPolizaHogar2ClienteFormId').val());
              
          

            },
            error: function (qXHR, textStatus, errorThrown) {
                $.LoadingOverlay("hide");
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    };




    this.ControlActions = function () {

        $('#ClientID').change(function () {
         if ($('#ClientID').val() !== null && $('#ClientID').val() !== $('#ClientID').data('oldValue')) {
             $('#ClientID').data('oldValue', $('#ClientID').val() );             
           $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogar2ClienteActions.aspx/ClientIDChange",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({
                    instance: HT5NNCotizacionPolizaHogar2ClienteSupport.InputToObject()
                }),
                success: function (data) {
                    HT5NNCotizacionPolizaHogar2ClienteSupport.ActionProcess(data);
                },
                error: function (qXHR, textStatus, errorThrown) {
                    generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                }
            });
      }          
    });
        $('#button17').click(function (event) {
            var formInstance = $("#HT5NNCotizacionPolizaHogar2ClienteMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#button17'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogar2ClienteActions.aspx/button17Click",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5NNCotizacionPolizaHogar2ClienteSupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5NNCotizacionPolizaHogar2ClienteSupport.ActionProcess(data);
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
    
        $("#HT5NNCotizacionPolizaHogar2ClienteMainForm").validate({
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
                ClientID: {
                    required: true
                },
                BirthDate: {
                    required: true
                },
                FirstName: {
                    required: true
                },
                LastName: {
                    required: true
                },
                LastName2: {
                    required: true
                },
                eMailCliente: {
                    required: true
                }
            },
            messages: {
                ProductCode: {
                    required: 'El campo es requerido.'
                },
                LineOfBusiness: {
                    required: 'El campo es requerido.'
                },
                ClientID: {
                    required: 'El campo es requerido.'
                },
                BirthDate: {
                    required: 'El campo es requerido.'
                },
                FirstName: {
                    required: 'El campo es requerido.'
                },
                LastName: {
                    required: 'El campo es requerido.'
                },
                LastName2: {
                    required: 'El campo es requerido.'
                },
                eMailCliente: {
                    required: 'El campo es requerido.'
                }
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
                url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogar2ClienteActions.aspx/LookUpForProductCode",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({
                    id: $('#HT5NNCotizacionPolizaHogar2ClienteFormId').val(),
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
                url: "/fasi/dli/forms/HT5NNCotizacionPolizaHogar2ClienteActions.aspx/LookUpForLineOfBusiness",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5NNCotizacionPolizaHogar2ClienteFormId').val()}),
                
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
        

    HT5NNCotizacionPolizaHogar2ClienteSupport.ControlBehaviour();
    HT5NNCotizacionPolizaHogar2ClienteSupport.ControlActions();
    HT5NNCotizacionPolizaHogar2ClienteSupport.ValidateSetup();
    HT5NNCotizacionPolizaHogar2ClienteSupport.Initialization();





});

