var HT5NNCotizacionMiSaludValeOroSupport = new function () {

    this.currentRow = {};
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#HT5NNCotizacionMiSaludValeOroFormId').val(),
            RiskInformationEffectiveDate: $('#RiskInformationEffectiveDate').val() !== '' ? moment($('#RiskInformationEffectiveDate').val(), 'DD/MM/YYYY').utc().format('YYYY-MM-DD') : moment(new Date('0001-01-01T00:00:00')).utc().format('YYYY-MM-DD'),
            uwcaseid: parseInt(0 + $('#uwcaseid').val(), 10),
            RiskInformationHealthLineOfBusinessInsuredAmount: AutoNumeric.getNumber('#InsuredAmount1'),
            Currency: parseInt(0 + $('#Currency').val(), 10),
            RiskInformationPolicyPaymentFrequency: parseInt(0 + $('#PaymentFrequency').val(), 10),
            RiskInformationPaymentMethod: parseInt(0 + $('#PaymentMethod').val(), 10),
            ClientIncludedGender: $('input:radio[name=Gender]:checked').val(),
            ClientIncludedSmokerIndicator: $('input:radio[name=SmokerIndicator]:checked').val(),
            ClientIncludedBirthDate: $('#BirthDate').val() !== '' ? moment($('#BirthDate').val(), 'DD/MM/YYYY').utc().format('YYYY-MM-DD') : moment(new Date('0001-01-01T00:00:00')).utc().format('YYYY-MM-DD'),
            AseguradoProcesar: parseInt(0 + $('#AseguradoProcesar').val(), 10),
            RoleAdicionalClientRole: parseInt(0 + $('#ClientRolea').val(), 10),
            RoleAdicionalClientBirthDate: $('#BirthDatea').val() !== '' ? moment($('#BirthDatea').val(), 'DD/MM/YYYY').utc().format('YYYY-MM-DD') : moment(new Date('0001-01-01T00:00:00')).utc().format('YYYY-MM-DD'),
            RoleAdicionalClientGender: $('input:radio[name=Gendera]:checked').val(),
            RiskInformationPrimaryInsuredClientClientID: $('#ClientID').val(),
            RiskInformationPrimaryInsuredClientFirstName: $('#FirstName').val(),
            RiskInformationPrimaryInsuredClientLastName: $('#LastName').val(),
            RiskInformationPrimaryInsuredClientLastName2: $('#LastName2').val(),
            AddressPhysicalAddressDLI: AddressSupport.GetLocalAddressBySelector("physicaladdress0"),
            AddresseMailDLIeMailAddresseMail: $('#eMailclient').val(),
            AseguradoCompletar: parseInt(0 + $('#AseguradoCompletar').val(), 10),
            RoleCompletarClientRole: parseInt(0 + $('#ClientRoleCompletar').val(), 10),
            RoleCompletarClientBirthDate: $('#BirthDateCompletar').val() !== '' ? moment($('#BirthDateCompletar').val(), 'DD/MM/YYYY').utc().format('YYYY-MM-DD') : moment(new Date('0001-01-01T00:00:00')).utc().format('YYYY-MM-DD'),
            RoleCompletarClientGender: $('#GenderCompletar').val(),
            RoleCompletarClientClientID: $('#ClientIDCompletar').val(),
            RoleCompletarClientFirstName: $('#FirstNameCompletar').val(),
            RoleCompletarClientLastName: $('#LastNameCompletar').val(),
            RoleCompletarClientLastName2: $('#LastName2Completar').val(),
            BadHealth: $('input:radio[name=BadHealth]:checked').val(),
            RiskInformationLineOfBusiness: parseInt(0 + $('#LineOfBusiness').val(), 10),
            RiskInformationProductCode: parseInt(0 + $('#ProductCode').val(), 10)
        };
        return data;
    };

    this.ObjectToInput = function (data) {
        $('#HT5NNCotizacionMiSaludValeOroFormId').val(data.InstanceFormId);
        $('#ProductMasterDescription').html(data.ProductMasterDescription);
        $('#RiskInformationEffectiveDate').val(generalSupport.ToJavaScriptDateCustom(data.RiskInformationEffectiveDate, 'DD/MM/YYYY'));
        $('#uwcaseid').val(data.uwcaseid);
        AutoNumeric.set('#InsuredAmount1', data.RiskInformationHealthLineOfBusinessInsuredAmount);
        if($('input:radio[name=Gender][value=' + data.ClientIncludedGender +']').length===0)
           $('input:radio[name=Gender]').prop('checked', false);
        else
           $($('input:radio[name=Gender][value=' + data.ClientIncludedGender +']')).prop('checked', true);
        $('#Gender').data('oldValue', data.ClientIncludedGender);
        $('#Gender').val(data.ClientIncludedGender);

        if($('input:radio[name=SmokerIndicator][value=' + data.ClientIncludedSmokerIndicator +']').length===0)
           $('input:radio[name=SmokerIndicator]').prop('checked', false);
        else
           $($('input:radio[name=SmokerIndicator][value=' + data.ClientIncludedSmokerIndicator +']')).prop('checked', true);
        $('#SmokerIndicator').data('oldValue', data.ClientIncludedSmokerIndicator);
        $('#SmokerIndicator').val(data.ClientIncludedSmokerIndicator);

        $('#BirthDate').val(generalSupport.ToJavaScriptDateCustom(data.ClientIncludedBirthDate, 'DD/MM/YYYY'));
        $('#BirthDatea').val(generalSupport.ToJavaScriptDateCustom(data.RoleAdicionalClientBirthDate, 'DD/MM/YYYY'));
        HT5NNCotizacionMiSaludValeOroSupport.LookUpForGendera(data.RoleAdicionalClientGender);

        $('#ClientID').val(data.RiskInformationPrimaryInsuredClientClientID);
        $('#FirstName').val(data.RiskInformationPrimaryInsuredClientFirstName);
        $('#LastName').val(data.RiskInformationPrimaryInsuredClientLastName);
        $('#LastName2').val(data.RiskInformationPrimaryInsuredClientLastName2);
        AddressSupport.Initialization('physicaladdress0', data.AddressPhysicalAddressDLI, true, false);
        $('#eMailclient').val(data.AddresseMailDLIeMailAddresseMail);
        $('#BirthDateCompletar').val(generalSupport.ToJavaScriptDateCustom(data.RoleCompletarClientBirthDate, 'DD/MM/YYYY'));
        $('#ClientIDCompletar').val(data.RoleCompletarClientClientID);
        $('#FirstNameCompletar').val(data.RoleCompletarClientFirstName);
        $('#LastNameCompletar').val(data.RoleCompletarClientLastName);
        $('#LastName2Completar').val(data.RoleCompletarClientLastName2);
        if($('input:radio[name=BadHealth][value=' + data.BadHealth +']').length===0)
           $('input:radio[name=BadHealth]').prop('checked', false);
        else
           $($('input:radio[name=BadHealth][value=' + data.BadHealth +']')).prop('checked', true);
        $('#BadHealth').data('oldValue', data.BadHealth);
        $('#BadHealth').val(data.BadHealth);


        HT5NNCotizacionMiSaludValeOroSupport.LookUpForCurrency(data.Currency);
        HT5NNCotizacionMiSaludValeOroSupport.LookUpForPaymentMethod(data.RiskInformationPaymentMethod);
        HT5NNCotizacionMiSaludValeOroSupport.LookUpForClientRolegrid();
        HT5NNCotizacionMiSaludValeOroSupport.LookUpForGendergg();
        HT5NNCotizacionMiSaludValeOroSupport.LookUpForAseguradoProcesar(data.AseguradoProcesar);
        HT5NNCotizacionMiSaludValeOroSupport.LookUpForClientRolea(data.RoleAdicionalClientRole);
        HT5NNCotizacionMiSaludValeOroSupport.LookUpForClientRolecompt();
        HT5NNCotizacionMiSaludValeOroSupport.LookUpForGendercomp();
        HT5NNCotizacionMiSaludValeOroSupport.LookUpForAseguradoCompletar(data.AseguradoCompletar);
        HT5NNCotizacionMiSaludValeOroSupport.LookUpForClientRoleCompletar(data.RoleCompletarClientRole);
        HT5NNCotizacionMiSaludValeOroSupport.LookUpForGenderCompletar(data.RoleCompletarClientGender);
        HT5NNCotizacionMiSaludValeOroSupport.LookUpForLineOfBusiness(data.RiskInformationLineOfBusiness);
        HT5NNCotizacionMiSaludValeOroSupport.LookUpForProductCode(data.RiskInformationProductCode, data.RiskInformationLineOfBusiness);
        HT5NNCotizacionMiSaludValeOroSupport.LookUpForPaymentFrequency(data.RiskInformationPolicyPaymentFrequency, data.RiskInformationLineOfBusiness, data.RiskInformationProductCode, data.RiskInformationPaymentMethod);

        if (data.Rolegrid_Role !== null)
            $('#RolegridTbl').bootstrapTable('load', data.Rolegrid_Role.filter(function(filterColumns) {return (filterColumns.ClientRole != 1 && filterColumns.ClientRole != 2 && filterColumns.ClientRole != 13 && filterColumns.UserCode != 0 && filterColumns.IsDeletedMark != true);}));
        if (data.CoverageWithCalculatedPremium_CoverageWithCalculatedPremium !== null)
            $('#CoverageWithCalculatedPremiumTbl').bootstrapTable('load', data.CoverageWithCalculatedPremium_CoverageWithCalculatedPremium.filter(function(filterColumns) {return filterColumns.SelectedByDefault == true;}));
        if (data.RoleCompletar_Role !== null)
            $('#RoleCompletarTbl').bootstrapTable('load', data.RoleCompletar_Role.filter(function(filterColumns) {return (filterColumns.ClientRole != 1 && filterColumns.ClientRole != 2 && filterColumns.ClientRole != 13);}));
        if (data.QuestionnaireByInsuredPerson_QuestionnaireByInsuredPerson !== null)
            $('#QuestionnaireByInsuredPersonTbl').bootstrapTable('load', data.QuestionnaireByInsuredPerson_QuestionnaireByInsuredPerson);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#InsuredAmount1', {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "999999999999",
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
                    HT5NNCotizacionMiSaludValeOroSupport.LookUpForPaymentFrequency(null, parseInt(0 + $('#LineOfBusiness').val(), 10), parseInt(0 + $('#ProductCode').val(), 10), parseInt(0 + $('#PaymentMethod').val(), 10));
            }
            else
                if($('#LineOfBusiness').val() !== $('#PaymentFrequency').data("parentId1") || $('#ProductCode').val() !== $('#PaymentFrequency').data("parentId2") || $('#PaymentMethod').val() !== $('#PaymentFrequency').data("parentId3"))
                   $('#PaymentFrequency').children().remove();
        });
        $('#ProductCode').on('change', function () {
            var value = $('#ProductCode').val();

            if (value !== null && value !== '0') {
                var skipData = $('#ProductCode').data("skip");

                if (skipData !== undefined && skipData === true)
                    $('#ProductCode').data("skip", false);
                else
                    HT5NNCotizacionMiSaludValeOroSupport.LookUpForPaymentFrequency(null, parseInt(0 + $('#LineOfBusiness').val(), 10), parseInt(0 + $('#ProductCode').val(), 10), parseInt(0 + $('#PaymentMethod').val(), 10));
            }
            else
                if($('#LineOfBusiness').val() !== $('#PaymentFrequency').data("parentId1") || $('#ProductCode').val() !== $('#PaymentFrequency').data("parentId2") || $('#PaymentMethod').val() !== $('#PaymentFrequency').data("parentId3"))
                   $('#PaymentFrequency').children().remove();
        });
        $('#PaymentMethod').on('change', function () {
            var value = $('#PaymentMethod').val();

            if (value !== null && value !== '0') {
                var skipData = $('#PaymentMethod').data("skip");

                if (skipData !== undefined && skipData === true)
                    $('#PaymentMethod').data("skip", false);
                else
                    HT5NNCotizacionMiSaludValeOroSupport.LookUpForPaymentFrequency(null, parseInt(0 + $('#LineOfBusiness').val(), 10), parseInt(0 + $('#ProductCode').val(), 10), parseInt(0 + $('#PaymentMethod').val(), 10));
            }
            else
                if($('#LineOfBusiness').val() !== $('#PaymentFrequency').data("parentId1") || $('#ProductCode').val() !== $('#PaymentFrequency').data("parentId2") || $('#PaymentMethod').val() !== $('#PaymentFrequency').data("parentId3"))
                   $('#PaymentFrequency').children().remove();
        });
   this.LookUpForClientRolegrid = function (defaultValue) {
        var ctrol = $('#ClientRolegrid');
        
       //if (ctrol.children().length === 0) {
        ctrol.children().remove();
        ctrol.append($('<option />').val('0').text(' Cargando...'));
	
        return $.ajax({
            type: "POST",
            url: "/fasi/dli/forms/HT5NNCotizacionMiSaludValeOroActions.aspx/LookUpForClientRolegrid",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            data: JSON.stringify({
                formId: $('#HT5NNCotizacionMiSaludValeOroFormId').val()
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
       //}
       //else
       //    if (typeof defaultValue !== 'undefined' && defaultValue !== null)
       //        if (defaultValue.toString() !== ctrol.val())
       //            ctrol.val(defaultValue).change();
    };
   this.LookUpForAseguradoProcesar = function (defaultValue) {
        var ctrol = $('#AseguradoProcesar');
        
       //if (ctrol.children().length === 0) {
        ctrol.children().remove();
        ctrol.append($('<option />').val('0').text(' Cargando...'));
	
        return $.ajax({
            type: "POST",
            url: "/fasi/dli/forms/HT5NNCotizacionMiSaludValeOroActions.aspx/LookUpForAseguradoProcesar",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                formId: $('#HT5NNCotizacionMiSaludValeOroFormId').val()
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
       //}
       //else
       //    if (typeof defaultValue !== 'undefined' && defaultValue !== null)
       //        if (defaultValue.toString() !== ctrol.val())
       //            ctrol.val(defaultValue).change();
    };
   this.LookUpForClientRolea = function (defaultValue) {
        var ctrol = $('#ClientRolea');
        
       //if (ctrol.children().length === 0) {
        ctrol.children().remove();
        ctrol.append($('<option />').val('0').text(' Cargando...'));
	
        return $.ajax({
            type: "POST",
            url: "/fasi/dli/forms/HT5NNCotizacionMiSaludValeOroActions.aspx/LookUpForClientRolea",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                formId: $('#HT5NNCotizacionMiSaludValeOroFormId').val()
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
       //}
       //else
       //    if (typeof defaultValue !== 'undefined' && defaultValue !== null)
       //        if (defaultValue.toString() !== ctrol.val())
       //            ctrol.val(defaultValue).change();
    };
   this.LookUpForAseguradoCompletar = function (defaultValue) {
        var ctrol = $('#AseguradoCompletar');
        
       //if (ctrol.children().length === 0) {
        ctrol.children().remove();
        ctrol.append($('<option />').val('0').text(' Cargando...'));
	
        return $.ajax({
            type: "POST",
            url: "/fasi/dli/forms/HT5NNCotizacionMiSaludValeOroActions.aspx/LookUpForAseguradoCompletar",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                formId: $('#HT5NNCotizacionMiSaludValeOroFormId').val()
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
       //}
       //else
       //    if (typeof defaultValue !== 'undefined' && defaultValue !== null)
       //        if (defaultValue.toString() !== ctrol.val())
       //            ctrol.val(defaultValue).change();
    };
        $('#LineOfBusiness').on('change', function () {
            var value = $('#LineOfBusiness').val();

            if (value !== null && value !== '0') {
                var skipData = $('#LineOfBusiness').data("skip");

                if (skipData !== undefined && skipData === true)
                    $('#LineOfBusiness').data("skip", false);
                else
                    HT5NNCotizacionMiSaludValeOroSupport.LookUpForProductCode(null, parseInt(0 + $('#LineOfBusiness').val(), 10));
            }
            else
                if($('#LineOfBusiness').val() !== $('#ProductCode').data("parentId1"))
                   $('#ProductCode').children().remove();
        });

        $('#RiskInformationEffectiveDate_group').datetimepicker({
            format: 'DD/MM/YYYY',
            locale: 'es'
        });
        $('#BirthDate_group').datetimepicker({
            format: 'DD/MM/YYYY',
            locale: 'es'
        });
        $('#BirthDatea_group').datetimepicker({
            format: 'DD/MM/YYYY',
            locale: 'es'
        });
        $('#BirthDateCompletar_group').datetimepicker({
            format: 'DD/MM/YYYY',
            locale: 'es'
        });


    };

    this.ActionProcess = function (data) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
                HT5NNCotizacionMiSaludValeOroSupport.ObjectToInput(data.d.Data);
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
            url: "/fasi/dli/forms/HT5NNCotizacionMiSaludValeOroActions.aspx/Initialization"+ (window.location.href.split("?")[1] ? "?" + window.location.href.split("?")[1] : ""),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                id: $('#HT5NNCotizacionMiSaludValeOroFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            success: function (data) {
                $.LoadingOverlay("hide");

                HT5NNCotizacionMiSaludValeOroSupport.ActionProcess(data);
                
                if (generalSupport.URLStringValue('fromid') !== '' && window.history.replaceState)
                    window.history.replaceState({}, null, '/fasi/dli/forms/HT5NNCotizacionMiSaludValeOro.aspx?id=' + $('#HT5NNCotizacionMiSaludValeOroFormId').val());
              
          

            },
            error: function (qXHR, textStatus, errorThrown) {
                $.LoadingOverlay("hide");
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    };




    this.ControlActions = function () {

        $('#btnEliAA').click(function (event) {
            var formInstance = $("#HT5NNCotizacionMiSaludValeOroMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#btnEliAA'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5NNCotizacionMiSaludValeOroActions.aspx/btnEliAAClick",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5NNCotizacionMiSaludValeOroSupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5NNCotizacionMiSaludValeOroSupport.ActionProcess(data);
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
        $('#btnModAA').click(function (event) {
            var formInstance = $("#HT5NNCotizacionMiSaludValeOroMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#btnModAA'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5NNCotizacionMiSaludValeOroActions.aspx/btnModAAClick",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5NNCotizacionMiSaludValeOroSupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5NNCotizacionMiSaludValeOroSupport.ActionProcess(data);
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
        $('#btnAgrAA').click(function (event) {
            var formInstance = $("#HT5NNCotizacionMiSaludValeOroMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#btnAgrAA'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5NNCotizacionMiSaludValeOroActions.aspx/btnAgrAAClick",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5NNCotizacionMiSaludValeOroSupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5NNCotizacionMiSaludValeOroSupport.ActionProcess(data);
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
        $('#btnRechAgr').click(function (event) {
                var btnLoading = Ladda.create(document.querySelector('#btnRechAgr'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5NNCotizacionMiSaludValeOroActions.aspx/btnRechAgrClick",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5NNCotizacionMiSaludValeOroSupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5NNCotizacionMiSaludValeOroSupport.ActionProcess(data);
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        btnLoading.stop();
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
            event.preventDefault();
        });
        $('#btnListoAA').click(function (event) {
            var formInstance = $("#HT5NNCotizacionMiSaludValeOroMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#btnListoAA'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5NNCotizacionMiSaludValeOroActions.aspx/btnListoAAClick",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5NNCotizacionMiSaludValeOroSupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5NNCotizacionMiSaludValeOroSupport.ActionProcess(data);
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
        $('#Cotizar').click(function (event) {
            var formInstance = $("#HT5NNCotizacionMiSaludValeOroMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#Cotizar'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5NNCotizacionMiSaludValeOroActions.aspx/CotizarClick",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5NNCotizacionMiSaludValeOroSupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5NNCotizacionMiSaludValeOroSupport.ActionProcess(data);
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
        $('#Acepto').click(function (event) {
            var formInstance = $("#HT5NNCotizacionMiSaludValeOroMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#Acepto'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5NNCotizacionMiSaludValeOroActions.aspx/AceptoClick",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5NNCotizacionMiSaludValeOroSupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5NNCotizacionMiSaludValeOroSupport.ActionProcess(data);
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
        $('#ClientID').change(function () {
         if ($('#ClientID').val() !== null && $('#ClientID').val() !== $('#ClientID').data('oldValue')) {
             $('#ClientID').data('oldValue', $('#ClientID').val() );             
           $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionMiSaludValeOroActions.aspx/ClientIDChange",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({
                    instance: HT5NNCotizacionMiSaludValeOroSupport.InputToObject()
                }),
                success: function (data) {
                    HT5NNCotizacionMiSaludValeOroSupport.ActionProcess(data);
                },
                error: function (qXHR, textStatus, errorThrown) {
                    generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                }
            });
      }          
    });
        $('#button8').click(function (event) {
            var formInstance = $("#HT5NNCotizacionMiSaludValeOroMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#button8'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5NNCotizacionMiSaludValeOroActions.aspx/button8Click",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5NNCotizacionMiSaludValeOroSupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5NNCotizacionMiSaludValeOroSupport.ActionProcess(data);
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
        $('#ClientIDcomp').change(function () {
         if ($('#ClientIDcomp').val() !== null && $('#ClientIDcomp').val() !== $('#ClientIDcomp').data('oldValue')) {
             $('#ClientIDcomp').data('oldValue', $('#ClientIDcomp').val() );             
           $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionMiSaludValeOroActions.aspx/ClientIDcompChange",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({
                    instance: HT5NNCotizacionMiSaludValeOroSupport.InputToObject()
                }),
                success: function (data) {
                    HT5NNCotizacionMiSaludValeOroSupport.ActionProcess(data);
                },
                error: function (qXHR, textStatus, errorThrown) {
                    generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                }
            });
      }          
    });
        $('#btnCompletar').click(function (event) {
            var formInstance = $("#HT5NNCotizacionMiSaludValeOroMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#btnCompletar'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5NNCotizacionMiSaludValeOroActions.aspx/btnCompletarClick",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5NNCotizacionMiSaludValeOroSupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5NNCotizacionMiSaludValeOroSupport.ActionProcess(data);
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
        $('#ClientIDCompletar').change(function () {
         if ($('#ClientIDCompletar').val() !== null && $('#ClientIDCompletar').val() !== $('#ClientIDCompletar').data('oldValue')) {
             $('#ClientIDCompletar').data('oldValue', $('#ClientIDCompletar').val() );             
           $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionMiSaludValeOroActions.aspx/ClientIDCompletarChange",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({
                    instance: HT5NNCotizacionMiSaludValeOroSupport.InputToObject()
                }),
                success: function (data) {
                    HT5NNCotizacionMiSaludValeOroSupport.ActionProcess(data);
                },
                error: function (qXHR, textStatus, errorThrown) {
                    generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                }
            });
      }          
    });
        $('#button38').click(function (event) {
            var formInstance = $("#HT5NNCotizacionMiSaludValeOroMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#button38'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5NNCotizacionMiSaludValeOroActions.aspx/button38Click",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5NNCotizacionMiSaludValeOroSupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5NNCotizacionMiSaludValeOroSupport.ActionProcess(data);
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
        $('#btdAAGyS').click(function (event) {
            var formInstance = $("#HT5NNCotizacionMiSaludValeOroMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#btdAAGyS'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5NNCotizacionMiSaludValeOroActions.aspx/btdAAGySClick",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5NNCotizacionMiSaludValeOroSupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5NNCotizacionMiSaludValeOroSupport.ActionProcess(data);
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
        $('#btnSaludGS').click(function (event) {
            var formInstance = $("#HT5NNCotizacionMiSaludValeOroMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#btnSaludGS'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5NNCotizacionMiSaludValeOroActions.aspx/btnSaludGSClick",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5NNCotizacionMiSaludValeOroSupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5NNCotizacionMiSaludValeOroSupport.ActionProcess(data);
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
        $('#button14').click(function (event) {
            var formInstance = $("#HT5NNCotizacionMiSaludValeOroMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#button14'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5NNCotizacionMiSaludValeOroActions.aspx/button14Click",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5NNCotizacionMiSaludValeOroSupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5NNCotizacionMiSaludValeOroSupport.ActionProcess(data);
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
        $('#Rechazar').click(function (event) {
                var btnLoading = Ladda.create(document.querySelector('#Rechazar'));
                btnLoading.start();

                $.ajax({
                    type: "POST",
                    url: "/fasi/dli/forms/HT5NNCotizacionMiSaludValeOroActions.aspx/RechazarClick",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        instance: HT5NNCotizacionMiSaludValeOroSupport.InputToObject()
                    }),
                    success: function (data) {
                        btnLoading.stop();

                        HT5NNCotizacionMiSaludValeOroSupport.ActionProcess(data);
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        btnLoading.stop();
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
            event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();
    
        $("#HT5NNCotizacionMiSaludValeOroMainForm").validate({
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
                RiskInformationEffectiveDate: {
                    required: true
                },
                InsuredAmount1: {
                    AutoNumericRequired: true                },
                Gender: {
                    required: true
                },
                SmokerIndicator: {
                    required: true
                },
                BirthDate: {
                    required: true
                },
                ClientRolea: {
                    required: true
                },
                BirthDatea: {
                    required: true
                },
                Gendera: {
                    required: true
                },
                ClientID: {
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
                eMailclient: {
                    required: true
                },
                ClientRoleCompletar: {
                    required: true
                },
                ClientIDCompletar: {
                    required: true
                },
                FirstNameCompletar: {
                    required: true
                },
                LastNameCompletar: {
                    required: true
                },
                LastName2Completar: {
                    required: true
                },
                LineOfBusiness: {
                    required: true
                },
                ProductCode: {
                    required: true
                }
            },
            messages: {
                RiskInformationEffectiveDate: {
                    required: 'El campo es requerido'
                },
                InsuredAmount1: {
                    AutoNumericRequired: 'Información requerida'                },
                Gender: {
                    required: 'El campo es requerido.'
                },
                SmokerIndicator: {
                    required: 'El campo es requerido.'
                },
                BirthDate: {
                    required: 'El campo es requerido.'
                },
                ClientRolea: {
                    required: 'El campo es requerido.'
                },
                BirthDatea: {
                    required: 'El campo es requerido'
                },
                Gendera: {
                    required: 'El campo es requerido'
                },
                ClientID: {
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
                eMailclient: {
                    required: 'El campo es requerido.'
                },
                ClientRoleCompletar: {
                    required: 'El campo es requerido.'
                },
                ClientIDCompletar: {
                    required: 'El campo es requerido.'
                },
                FirstNameCompletar: {
                    required: 'El campo es requerido'
                },
                LastNameCompletar: {
                    required: 'El campo es requerido'
                },
                LastName2Completar: {
                    required: 'El campo es requerido'
                },
                LineOfBusiness: {
                    required: 'El campo es requerido.'
                },
                ProductCode: {
                    required: 'El campo es requerido.'
                }
            }
        });

    };
    this.LookUpForCurrency = function (defaultValue) {
        var ctrol = $('#Currency');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionMiSaludValeOroActions.aspx/LookUpForCurrency",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5NNCotizacionMiSaludValeOroFormId').val()}),
                
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
    this.LookUpForPaymentFrequency = function (defaultValue, value1, value2, value3) {
        var ctrol = $('#PaymentFrequency');
        var parentId1 = ctrol.data("parentId1");
        var parentId2 = ctrol.data("parentId2");
        var parentId3 = ctrol.data("parentId3");
        
        if ((typeof parentId1 == 'undefined' || parentId1 !== value1) || (typeof parentId2 == 'undefined' || parentId2 !== value2) || (typeof parentId3 == 'undefined' || parentId3 !== value3)) {
            ctrol.data("parentId1", value1);
            ctrol.data("parentId2", value2);
            ctrol.data("parentId3", value3);

            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionMiSaludValeOroActions.aspx/LookUpForPaymentFrequency",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({
                    id: $('#HT5NNCotizacionMiSaludValeOroFormId').val(),
                    RiskInformationLineOfBusiness: value1,
                     RiskInformationProductCode: value2,
                     RiskInformationPaymentMethod: value3
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
    this.LookUpForPaymentMethod = function (defaultValue) {
        var ctrol = $('#PaymentMethod');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionMiSaludValeOroActions.aspx/LookUpForPaymentMethod",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5NNCotizacionMiSaludValeOroFormId').val()}),
                
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
    this.LookUpForClientRolegridFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#ClientRolegrid>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForGenderggFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#Gendergg>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForGendergg = function (defaultValue) {
        var ctrol = $('#Gendergg');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionMiSaludValeOroActions.aspx/LookUpForGendergg",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5NNCotizacionMiSaludValeOroFormId').val()}),
                
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
    this.LookUpForGendera = function (defaultValue) {
        var ctrol = $('#Gendera_Dynamic');
        
        if (ctrol.children().length === 0) {
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionMiSaludValeOroActions.aspx/LookUpForGendera",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5NNCotizacionMiSaludValeOroFormId').val()}),
                
                success: function (data) {
                    ctrol.children().remove();
                    if (data.d.Success === true) {
                        $.each(data.d.Data, function () {
                            ctrol.append("<div class='radio'><label><input type='radio' name='Gendera' id='Gendera_" + this['Code'] + "' value='" + this['Code'] + "'/>" + this['Description'] + "</label></div>");
                        });
                        if (defaultValue !== null)
                            $($('input:radio[name=Gendera][value=' + defaultValue + ']')).prop('checked', true);
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
                    $($('input:radio[name=Gendera][value=' + defaultValue + ']')).prop('checked', true);
    };
    this.LookUpForClientRolecomptFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#ClientRolecompt>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForClientRolecompt = function (defaultValue) {
        var ctrol = $('#ClientRolecompt');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionMiSaludValeOroActions.aspx/LookUpForClientRolecompt",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5NNCotizacionMiSaludValeOroFormId').val()}),
                
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
    this.LookUpForGendercompFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#Gendercomp>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForGendercomp = function (defaultValue) {
        var ctrol = $('#Gendercomp');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionMiSaludValeOroActions.aspx/LookUpForGendercomp",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5NNCotizacionMiSaludValeOroFormId').val()}),
                
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
    this.LookUpForClientRoleCompletar = function (defaultValue) {
        var ctrol = $('#ClientRoleCompletar');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionMiSaludValeOroActions.aspx/LookUpForClientRoleCompletar",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5NNCotizacionMiSaludValeOroFormId').val()}),
                
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
    this.LookUpForGenderCompletar = function (defaultValue) {
        var ctrol = $('#GenderCompletar');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionMiSaludValeOroActions.aspx/LookUpForGenderCompletar",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5NNCotizacionMiSaludValeOroFormId').val()}),
                
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
                url: "/fasi/dli/forms/HT5NNCotizacionMiSaludValeOroActions.aspx/LookUpForLineOfBusiness",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5NNCotizacionMiSaludValeOroFormId').val()}),
                
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
    this.LookUpForProductCode = function (defaultValue, value1) {
        var ctrol = $('#ProductCode');
        var parentId1 = ctrol.data("parentId1");
        
        if (typeof parentId1 == 'undefined' || parentId1 !== value1) {
            ctrol.data("parentId1", value1);

            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/forms/HT5NNCotizacionMiSaludValeOroActions.aspx/LookUpForProductCode",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({
                    id: $('#HT5NNCotizacionMiSaludValeOroFormId').val(),
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

    this.RolegridTblSetup = function (table) {
        HT5NNCotizacionMiSaludValeOroSupport.LookUpForGendergg();
        table.bootstrapTable({
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            uniqueId: 'UserCode',
            columns: [{
                field: 'ClientRole',
                title: 'Figura',
                formatter: 'HT5NNCotizacionMiSaludValeOroSupport.LookUpForClientRolegridFormatter',
                sortable: false,
                halign: 'center'
            }, {
                field: 'BirthDate',
                title: 'Fecha de Nacimiento',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: false,
                halign: 'center',
                align: 'center'
            }, {
                field: 'ClientGender',
                title: 'Género',
                formatter: 'HT5NNCotizacionMiSaludValeOroSupport.LookUpForGenderggFormatter',
                sortable: false,
                halign: 'center'
            }]
        });



    };


    this.RolegridRowToInput = function (row) {
        HT5NNCotizacionMiSaludValeOroSupport.currentRow = row;
        HT5NNCotizacionMiSaludValeOroSupport.LookUpForClientRolegrid(row.ClientRole);
        $('#BirthDategg').val(generalSupport.ToJavaScriptDateCustom(row.BirthDate, 'DD/MM/YYYY'));
        HT5NNCotizacionMiSaludValeOroSupport.LookUpForGendergg(row.ClientGender);

    };
    this.CoverageWithCalculatedPremiumTblSetup = function (table) {
        table.bootstrapTable({
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            uniqueId: 'CoverageCode',
            columns: [{
                field: 'Description',
                title: 'Cobertura',
                sortable: false,
                halign: 'center'
            }, {
                field: 'InsuredAmount',
                title: 'Capital asegurado',
                formatter: 'HT5NNCotizacionMiSaludValeOroSupport.InsuredAmount_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'AnnualPremium',
                title: 'Prima anual',
                formatter: 'HT5NNCotizacionMiSaludValeOroSupport.AnnualPremium_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }]
        });



    };


    this.CoverageWithCalculatedPremiumRowToInput = function (row) {
        HT5NNCotizacionMiSaludValeOroSupport.currentRow = row;
        $('#Description').val(row.Description);
        AutoNumeric.set('#InsuredAmount', row.InsuredAmount);
        AutoNumeric.set('#AnnualPremium', row.AnnualPremium);

    };
    this.RoleCompletarTblSetup = function (table) {
        HT5NNCotizacionMiSaludValeOroSupport.LookUpForClientRolecompt();
        HT5NNCotizacionMiSaludValeOroSupport.LookUpForGendercomp();
        table.bootstrapTable({
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            uniqueId: 'UserCode',
            columns: [{
                field: 'ClientRole',
                title: 'Figura',
                formatter: 'HT5NNCotizacionMiSaludValeOroSupport.LookUpForClientRolecomptFormatter',
                sortable: false,
                halign: 'center'
            }, {
                field: 'ClientID',
                title: 'Código',
                sortable: false,
                halign: 'center'
            }, {
                field: 'ClientFirstName',
                title: 'Nombre(s)',
                sortable: false,
                halign: 'center'
            }, {
                field: 'ClientLastName',
                title: 'Apellido paterno',
                sortable: false,
                halign: 'center'
            }, {
                field: 'ClientLastName2',
                title: 'Apellido materno',
                sortable: false,
                halign: 'center'
            }, {
                field: 'BirthDate',
                title: 'Fecha de nacimiento',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: false,
                halign: 'center',
                align: 'center'
            }, {
                field: 'ClientGender',
                title: 'Género',
                formatter: 'HT5NNCotizacionMiSaludValeOroSupport.LookUpForGendercompFormatter',
                sortable: false,
                halign: 'center'
            }]
        });



    };


    this.RoleCompletarRowToInput = function (row) {
        HT5NNCotizacionMiSaludValeOroSupport.currentRow = row;
        HT5NNCotizacionMiSaludValeOroSupport.LookUpForClientRolecompt(row.ClientRole);
        $('#ClientIDcomp').val(row.ClientID);
        $('#FirstNamecomp').val(row.ClientFirstName);
        $('#LastNamecomp').val(row.ClientLastName);
        $('#LastName2comp').val(row.ClientLastName2);
        $('#BirthDatecomp').val(generalSupport.ToJavaScriptDateCustom(row.BirthDate, 'DD/MM/YYYY'));
        HT5NNCotizacionMiSaludValeOroSupport.LookUpForGendercomp(row.ClientGender);

    };
    this.QuestionnaireByInsuredPersonTblSetup = function (table) {
        table.bootstrapTable({
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            uniqueId: 'ClientID',
            columns: [{
                field: 'CompleteCliename',
                title: 'Asegurado',
                sortable: false,
                halign: 'center'
            }]
        });



    };


    this.QuestionnaireByInsuredPersonRowToInput = function (row) {
        HT5NNCotizacionMiSaludValeOroSupport.currentRow = row;
        $('#CompleteClienameQS').val(row.CompleteCliename);

    };


    this.InsuredAmount_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "999999999999999999",
            decimalPlaces: 2,
            minimumValue: "-999999999999999999"
        });
      };
    this.AnnualPremium_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: ",",
            digitGroupSeparator: ".",
            maximumValue: "999999999999999999",
            decimalPlaces: 2,
            minimumValue: "-999999999999999999"
        });
      };


};

$(document).ready(function () {
    moment.locale('es');
    
    if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage)
        masterSupport.setPageTitle('HT5Mi Salud Vale Oro');
        

    HT5NNCotizacionMiSaludValeOroSupport.ControlBehaviour();
    HT5NNCotizacionMiSaludValeOroSupport.ControlActions();
    HT5NNCotizacionMiSaludValeOroSupport.ValidateSetup();
    HT5NNCotizacionMiSaludValeOroSupport.Initialization();

    $("#RolegridTblPlaceHolder").replaceWith('<table id="RolegridTbl"></table>');
    HT5NNCotizacionMiSaludValeOroSupport.RolegridTblSetup($('#RolegridTbl'));
    $("#CoverageWithCalculatedPremiumTblPlaceHolder").replaceWith('<table id="CoverageWithCalculatedPremiumTbl"><caption >Coberturas y prima anual</caption></table>');
    HT5NNCotizacionMiSaludValeOroSupport.CoverageWithCalculatedPremiumTblSetup($('#CoverageWithCalculatedPremiumTbl'));
    $("#RoleCompletarTblPlaceHolder").replaceWith('<table id="RoleCompletarTbl"></table>');
    HT5NNCotizacionMiSaludValeOroSupport.RoleCompletarTblSetup($('#RoleCompletarTbl'));
    $("#QuestionnaireByInsuredPersonTblPlaceHolder").replaceWith('<table id="QuestionnaireByInsuredPersonTbl"></table>');
    HT5NNCotizacionMiSaludValeOroSupport.QuestionnaireByInsuredPersonTblSetup($('#QuestionnaireByInsuredPersonTbl'));




});

