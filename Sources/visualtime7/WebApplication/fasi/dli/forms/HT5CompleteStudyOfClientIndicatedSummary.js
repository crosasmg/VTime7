var HT5CompleteStudyOfClientIndicatedSummarySupport = new function () {

    this.currentRow = {};
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#HT5CompleteStudyOfClientIndicatedSummaryFormId').val(),
            RiskInformationPrimaryInsuredClientClientID: ($('#ClientID').data('code') !== undefined) ? $('#ClientID').data('code') : '',
            ClienteSelIntermediario: $('#ClienteSelIntermediario').val(),
            ProducerID: parseInt(0 + $('#ProducerID').val(), 10)
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        $('#HT5CompleteStudyOfClientIndicatedSummaryFormId').val(data.InstanceFormId);
        $('#ClientID').data('code', data.RiskInformationPrimaryInsuredClientClientID);
        clientSupport.CompleteClientName('#ClientID', data.RiskInformationPrimaryInsuredClientClientID);

        HT5CompleteStudyOfClientIndicatedSummarySupport.LookUpForLineOfBusinessa(source);
        HT5CompleteStudyOfClientIndicatedSummarySupport.LookUpForLineOfBusinessc(source);
        HT5CompleteStudyOfClientIndicatedSummarySupport.LookUpForCancellationCodec(source);
        HT5CompleteStudyOfClientIndicatedSummarySupport.LookUpForProductCodep(source);
        HT5CompleteStudyOfClientIndicatedSummarySupport.LookUpForStatus0(source);
        HT5CompleteStudyOfClientIndicatedSummarySupport.LookUpForStatus1(source);
        HT5CompleteStudyOfClientIndicatedSummarySupport.LookUpForStatus2(source);
        HT5CompleteStudyOfClientIndicatedSummarySupport.LookUpForProducerID(data.ProducerID, source);
        HT5CompleteStudyOfClientIndicatedSummarySupport.LookUpForClienteSelIntermediario(data.ClienteSelIntermediario, data.ProducerID, source);

        if (data.CurrentPolicyCollection_Policy !== null)
            $('#CurrentPolicyCollectionTbl').bootstrapTable('load', data.CurrentPolicyCollection_Policy);
        if (data.CancelledPolicyCollection_Policy !== null)
            $('#CancelledPolicyCollectionTbl').bootstrapTable('load', data.CancelledPolicyCollection_Policy);
        if (data.OutstandingPremium_Policy !== null)
            $('#OutstandingPremiumTbl').bootstrapTable('load', data.OutstandingPremium_Policy);
        if (data.Claim_Claim !== null)
            $('#ClaimTbl').bootstrapTable('load', data.Claim_Claim);
        if (data.PendingUnderwritingCase_UnderwritingCase !== null)
            $('#PendingUnderwritingCaseTbl').bootstrapTable('load', data.PendingUnderwritingCase_UnderwritingCase.filter(function(filterColumns) {return filterColumns.Decision == 1;}));
        if (data.ApprovedUnderwritingCase_UnderwritingCase !== null)
            $('#ApprovedUnderwritingCaseTbl').bootstrapTable('load', data.ApprovedUnderwritingCase_UnderwritingCase.filter(function(filterColumns) {return filterColumns.Decision == 3 || filterColumns.Decision == 4;}));
        if (data.ReviewUnderwritingCase_UnderwritingCase !== null)
            $('#ReviewUnderwritingCaseTbl').bootstrapTable('load', data.ReviewUnderwritingCase_UnderwritingCase.filter(function(filterColumns) {return filterColumns.Decision == 5;}));
        if (data.DeclinedUnderwritingCase_UnderwritingCase !== null)
            $('#DeclinedUnderwritingCaseTbl').bootstrapTable('load', data.DeclinedUnderwritingCase_UnderwritingCase.filter(function(filterColumns) {return filterColumns.Decision == 2;}));

    };

    this.ControlBehaviour = function () {





        $("#ClientID").autocomplete({
            source: function (request, response) {
                clientSupport.AutoCompleteSource('#ClientID', request, response);
            },
            select: function (event, ui) {
                $('#ClientID').data('code', ui.item.code);
            }
        });

        $('#ProducerID').on('change', function () {
            var value = $('#ProducerID').val();

            if (value !== null && value !== '0') {
                var skipData = $('#ProducerID').data("skip");

                if (skipData !== undefined && skipData === true)
                    $('#ProducerID').data("skip", false);
                else
                    HT5CompleteStudyOfClientIndicatedSummarySupport.LookUpForClienteSelIntermediario(null, parseInt(0 + $('#ProducerID').val(), 10));
            }
            else
                if($('#ProducerID').val() !== $('#ClienteSelIntermediario').data("parentId1"))
                   $('#ClienteSelIntermediario').children().remove();
        });



    };

    this.ActionProcess = function (data, source) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
                HT5CompleteStudyOfClientIndicatedSummarySupport.ObjectToInput(data.d.Data, source);
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };

    this.Initialization = function () {
        app.core.AsyncWebMethod("/fasi/dli/forms/HT5CompleteStudyOfClientIndicatedSummaryActions.aspx/Initialization"+ (window.location.href.split("?")[1] ? "?" + window.location.href.split("?")[1] : ""), true,
            JSON.stringify({
                id: $('#HT5CompleteStudyOfClientIndicatedSummaryFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            function (data) {

                HT5CompleteStudyOfClientIndicatedSummarySupport.ActionProcess(data, 'Initialization');

                if (generalSupport.URLStringValue('fromid') !== '' && window.history.replaceState)
                    window.history.replaceState({}, null, '/fasi/dli/forms/HT5CompleteStudyOfClientIndicatedSummary.aspx?id=' + $('#HT5CompleteStudyOfClientIndicatedSummaryFormId').val());
              
          

            });
    };




    this.ControlActions = function () {

        $('#button1').click(function (event) {
                var formInstance = $("#HT5CompleteStudyOfClientIndicatedSummaryMainForm");
                var fvalidate = formInstance.validate();
                
                if (formInstance.valid()) {
                    var btnLoading = Ladda.create(document.querySelector('#button1'));
                    btnLoading.start();

                    app.core.AsyncWebMethod("/fasi/dli/forms/HT5CompleteStudyOfClientIndicatedSummaryActions.aspx/button1Click", false,
                          JSON.stringify({
                                        instance: HT5CompleteStudyOfClientIndicatedSummarySupport.InputToObject()
                          }),
                    function (data) {
                         btnLoading.stop();

                    HT5CompleteStudyOfClientIndicatedSummarySupport.ActionProcess(data, 'button1Click');
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
    
        $("#HT5CompleteStudyOfClientIndicatedSummaryMainForm").validate({
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
                ClientID: {
                    required: true
                },
                ClienteSelIntermediario: {
                    required: true
                }
            },
            messages: {
                ClientID: {
                    required: 'El campo es requerido.'
                },
                ClienteSelIntermediario: {
                    required: 'El campo es requerido.'
                }
            }
        });

    };
    this.LookUpForClienteSelIntermediario = function (defaultValue, value1, source) {
        var ctrol = $('#ClienteSelIntermediario');
        var parentId1 = ctrol.data("parentId1");
        
        if (typeof parentId1 == 'undefined' || parentId1 !== value1) {
            ctrol.data("parentId1", value1);

            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));            
            
            app.core.SyncWebMethod("/fasi/dli/forms/HT5CompleteStudyOfClientIndicatedSummaryActions.aspx/LookUpForClienteSelIntermediario", false,
                JSON.stringify({
                                        id: $('#HT5CompleteStudyOfClientIndicatedSummaryFormId').val(),
                    ProducerID: value1
                }),
                function (data) {
                    ctrol.children().remove();
                    $.each(data.d.Data, function () {
                        ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                    });
                    if (defaultValue !== null)
                        ctrol.val(defaultValue);
                    else
                        ctrol.val(0);

                    if (source !== 'Initialization')
                        ctrol.change();
                        
                        
                });
        }
        else
            if (typeof defaultValue !== 'undefined' && defaultValue !== null)
                if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                    ctrol.val(defaultValue);
					      if(source !== 'Initialization')
                    ctrol.change();
            }
    };
    this.LookUpForLineOfBusinessaFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#LineOfBusinessa>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForLineOfBusinessa = function (defaultValue, source) {
        var ctrol = $('#LineOfBusinessa');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.SyncWebMethod("/fasi/dli/forms/HT5CompleteStudyOfClientIndicatedSummaryActions.aspx/LookUpForLineOfBusinessa", false,
                JSON.stringify({ id: $('#HT5CompleteStudyOfClientIndicatedSummaryFormId').val() }),
                function (data) {
                    ctrol.children().remove();
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue);
                        else
                            ctrol.val(0);

                        if (source !== 'Initialization')
                            ctrol.change();
                            
                            
                });

        }
        else
            if (typeof defaultValue !== 'undefined' && defaultValue !== null)
                if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                    ctrol.val(defaultValue);

                    if (source !== 'Initialization')
                        ctrol.change();
                }
    };
    this.LookUpForLineOfBusinesscFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#LineOfBusinessc>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForLineOfBusinessc = function (defaultValue, source) {
        var ctrol = $('#LineOfBusinessc');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.SyncWebMethod("/fasi/dli/forms/HT5CompleteStudyOfClientIndicatedSummaryActions.aspx/LookUpForLineOfBusinessc", false,
                JSON.stringify({ id: $('#HT5CompleteStudyOfClientIndicatedSummaryFormId').val() }),
                function (data) {
                    ctrol.children().remove();
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue);
                        else
                            ctrol.val(0);

                        if (source !== 'Initialization')
                            ctrol.change();
                            
                            
                });

        }
        else
            if (typeof defaultValue !== 'undefined' && defaultValue !== null)
                if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                    ctrol.val(defaultValue);

                    if (source !== 'Initialization')
                        ctrol.change();
                }
    };
    this.LookUpForCancellationCodecFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#CancellationCodec>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForCancellationCodec = function (defaultValue, source) {
        var ctrol = $('#CancellationCodec');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.SyncWebMethod("/fasi/dli/forms/HT5CompleteStudyOfClientIndicatedSummaryActions.aspx/LookUpForCancellationCodec", false,
                JSON.stringify({ id: $('#HT5CompleteStudyOfClientIndicatedSummaryFormId').val() }),
                function (data) {
                    ctrol.children().remove();
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue);
                        else
                            ctrol.val(0);

                        if (source !== 'Initialization')
                            ctrol.change();
                            
                            
                });

        }
        else
            if (typeof defaultValue !== 'undefined' && defaultValue !== null)
                if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                    ctrol.val(defaultValue);

                    if (source !== 'Initialization')
                        ctrol.change();
                }
    };
    this.LookUpForProductCodepFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#ProductCodep>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForProductCodep = function (defaultValue, source) {
        var ctrol = $('#ProductCodep');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.SyncWebMethod("/fasi/dli/forms/HT5CompleteStudyOfClientIndicatedSummaryActions.aspx/LookUpForProductCodep", false,
                JSON.stringify({ id: $('#HT5CompleteStudyOfClientIndicatedSummaryFormId').val() }),
                function (data) {
                    ctrol.children().remove();
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue);
                        else
                            ctrol.val(0);

                        if (source !== 'Initialization')
                            ctrol.change();
                            
                            
                });

        }
        else
            if (typeof defaultValue !== 'undefined' && defaultValue !== null)
                if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                    ctrol.val(defaultValue);

                    if (source !== 'Initialization')
                        ctrol.change();
                }
    };
    this.LookUpForStatus0Formatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#Status0>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForStatus0 = function (defaultValue, source) {
        var ctrol = $('#Status0');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.SyncWebMethod("/fasi/dli/forms/HT5CompleteStudyOfClientIndicatedSummaryActions.aspx/LookUpForStatus0", false,
                JSON.stringify({ id: $('#HT5CompleteStudyOfClientIndicatedSummaryFormId').val() }),
                function (data) {
                    ctrol.children().remove();
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue);
                        else
                            ctrol.val(0);

                        if (source !== 'Initialization')
                            ctrol.change();
                            
                            
                });

        }
        else
            if (typeof defaultValue !== 'undefined' && defaultValue !== null)
                if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                    ctrol.val(defaultValue);

                    if (source !== 'Initialization')
                        ctrol.change();
                }
    };
    this.LookUpForStatus1Formatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#Status1>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForStatus1 = function (defaultValue, source) {
        var ctrol = $('#Status1');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.SyncWebMethod("/fasi/dli/forms/HT5CompleteStudyOfClientIndicatedSummaryActions.aspx/LookUpForStatus1", false,
                JSON.stringify({ id: $('#HT5CompleteStudyOfClientIndicatedSummaryFormId').val() }),
                function (data) {
                    ctrol.children().remove();
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue);
                        else
                            ctrol.val(0);

                        if (source !== 'Initialization')
                            ctrol.change();
                            
                            
                });

        }
        else
            if (typeof defaultValue !== 'undefined' && defaultValue !== null)
                if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                    ctrol.val(defaultValue);

                    if (source !== 'Initialization')
                        ctrol.change();
                }
    };
    this.LookUpForStatus2Formatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#Status2>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForStatus2 = function (defaultValue, source) {
        var ctrol = $('#Status2');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.SyncWebMethod("/fasi/dli/forms/HT5CompleteStudyOfClientIndicatedSummaryActions.aspx/LookUpForStatus2", false,
                JSON.stringify({ id: $('#HT5CompleteStudyOfClientIndicatedSummaryFormId').val() }),
                function (data) {
                    ctrol.children().remove();
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue);
                        else
                            ctrol.val(0);

                        if (source !== 'Initialization')
                            ctrol.change();
                            
                            
                });

        }
        else
            if (typeof defaultValue !== 'undefined' && defaultValue !== null)
                if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                    ctrol.val(defaultValue);

                    if (source !== 'Initialization')
                        ctrol.change();
                }
    };
    this.LookUpForProducerID = function (defaultValue, source) {
        var ctrol = $('#ProducerID');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.SyncWebMethod("/fasi/dli/forms/HT5CompleteStudyOfClientIndicatedSummaryActions.aspx/LookUpForProducerID", false,
                JSON.stringify({ id: $('#HT5CompleteStudyOfClientIndicatedSummaryFormId').val() }),
                function (data) {
                    ctrol.children().remove();
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue);
                        else
                            ctrol.val(0);

                        if (source !== 'Initialization')
                            ctrol.change();
                            
                            
                });

        }
        else
            if (typeof defaultValue !== 'undefined' && defaultValue !== null)
                if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                    ctrol.val(defaultValue);

                    if (source !== 'Initialization')
                        ctrol.change();
                }
    };

    this.CurrentPolicyCollectionTblSetup = function (table) {
        HT5CompleteStudyOfClientIndicatedSummarySupport.LookUpForLineOfBusinessa('');
        table.bootstrapTable({
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            uniqueId: 'PolicyID',
            columns: [{
                field: 'LineOfBusiness',
                title: 'Ramo',
                formatter: 'HT5CompleteStudyOfClientIndicatedSummarySupport.LookUpForLineOfBusinessaFormatter',
                sortable: false,
                halign: 'center'
            }, {
                field: 'PolicyID',
                title: 'Póliza',
                formatter: 'HT5CompleteStudyOfClientIndicatedSummarySupport.PolicyIDa_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'EffectiveDate',
                title: 'Inicio',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: false,
                halign: 'center',
                align: 'center'
            }, {
                field: 'EndingDate',
                title: 'Vencimiento',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: false,
                halign: 'center',
                align: 'center'
            }, {
                field: 'InsuredAmount',
                title: 'Capital Asegurado',
                formatter: 'HT5CompleteStudyOfClientIndicatedSummarySupport.InsuredAmounta_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }]
        });



    };


    this.CurrentPolicyCollectionRowToInput = function (row) {
        HT5CompleteStudyOfClientIndicatedSummarySupport.currentRow = row;
        HT5CompleteStudyOfClientIndicatedSummarySupport.LookUpForLineOfBusinessa(row.LineOfBusiness, '');
        AutoNumeric.set('#PolicyIDa', row.PolicyID);
        $('#EffectiveDatea').val(generalSupport.ToJavaScriptDateCustom(row.EffectiveDate, generalSupport.DateFormat()));
        $('#EndingDatea').val(generalSupport.ToJavaScriptDateCustom(row.EndingDate, generalSupport.DateFormat()));
        AutoNumeric.set('#InsuredAmounta', row.InsuredAmount);

    };
    this.CancelledPolicyCollectionTblSetup = function (table) {
        HT5CompleteStudyOfClientIndicatedSummarySupport.LookUpForLineOfBusinessc('');
        HT5CompleteStudyOfClientIndicatedSummarySupport.LookUpForCancellationCodec('');
        table.bootstrapTable({
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            uniqueId: 'PolicyID',
            columns: [{
                field: 'LineOfBusiness',
                title: 'Ramo',
                formatter: 'HT5CompleteStudyOfClientIndicatedSummarySupport.LookUpForLineOfBusinesscFormatter',
                sortable: false,
                halign: 'center'
            }, {
                field: 'PolicyID',
                title: 'Póliza',
                formatter: 'HT5CompleteStudyOfClientIndicatedSummarySupport.PolicyIDc_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'EffectiveDate',
                title: 'Inicio',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: false,
                halign: 'center',
                align: 'center'
            }, {
                field: 'EndingDate',
                title: 'Vencimiento',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: false,
                halign: 'center',
                align: 'center'
            }, {
                field: 'CancellationCode',
                title: 'Causa',
                formatter: 'HT5CompleteStudyOfClientIndicatedSummarySupport.LookUpForCancellationCodecFormatter',
                sortable: false,
                halign: 'center'
            }, {
                field: 'CancellationDate',
                title: 'Fecha de Anulación',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: false,
                halign: 'center',
                align: 'center'
            }]
        });



    };


    this.CancelledPolicyCollectionRowToInput = function (row) {
        HT5CompleteStudyOfClientIndicatedSummarySupport.currentRow = row;
        HT5CompleteStudyOfClientIndicatedSummarySupport.LookUpForLineOfBusinessc(row.LineOfBusiness, '');
        AutoNumeric.set('#PolicyIDc', row.PolicyID);
        $('#EffectiveDatec').val(generalSupport.ToJavaScriptDateCustom(row.EffectiveDate, generalSupport.DateFormat()));
        $('#EndingDatec').val(generalSupport.ToJavaScriptDateCustom(row.EndingDate, generalSupport.DateFormat()));
        HT5CompleteStudyOfClientIndicatedSummarySupport.LookUpForCancellationCodec(row.CancellationCode, '');
        $('#CancellationDatec').val(generalSupport.ToJavaScriptDateCustom(row.CancellationDate, generalSupport.DateFormat()));

    };
    this.OutstandingPremiumTblSetup = function (table) {
        HT5CompleteStudyOfClientIndicatedSummarySupport.LookUpForProductCodep('');
        table.bootstrapTable({
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            uniqueId: 'PolicyID',
            columns: [{
                field: 'PolicyID',
                title: 'Recibo',
                formatter: 'HT5CompleteStudyOfClientIndicatedSummarySupport.PolicyIDp_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'EffectiveDate',
                title: 'Inicio',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: false,
                halign: 'center',
                align: 'center'
            }, {
                field: 'EndingDate',
                title: 'Vencimiento',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: false,
                halign: 'center',
                align: 'center'
            }, {
                field: 'AnnualPremium',
                title: 'Prima pendiente',
                formatter: 'HT5CompleteStudyOfClientIndicatedSummarySupport.AnnualPremiump_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'ProductCode',
                title: 'Moneda',
                formatter: 'HT5CompleteStudyOfClientIndicatedSummarySupport.LookUpForProductCodepFormatter',
                sortable: false,
                halign: 'center'
            }]
        });



    };


    this.OutstandingPremiumRowToInput = function (row) {
        HT5CompleteStudyOfClientIndicatedSummarySupport.currentRow = row;
        AutoNumeric.set('#PolicyIDp', row.PolicyID);
        $('#EffectiveDatep').val(generalSupport.ToJavaScriptDateCustom(row.EffectiveDate, generalSupport.DateFormat()));
        $('#EndingDatep').val(generalSupport.ToJavaScriptDateCustom(row.EndingDate, generalSupport.DateFormat()));
        AutoNumeric.set('#AnnualPremiump', row.AnnualPremium);
        HT5CompleteStudyOfClientIndicatedSummarySupport.LookUpForProductCodep(row.ProductCode, '');

    };
    this.ClaimTblSetup = function (table) {
        table.bootstrapTable({
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            uniqueId: 'ClaimID',
            columns: [{
                field: 'ClaimID',
                title: 'Siniestro',
                formatter: 'HT5CompleteStudyOfClientIndicatedSummarySupport.ClaimID_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'ClaimDate',
                title: 'Fecha de Declaración',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: false,
                halign: 'center',
                align: 'center'
            }, {
                field: 'CurrentReserveAmount',
                title: 'Reserva Actual',
                formatter: 'HT5CompleteStudyOfClientIndicatedSummarySupport.CurrentReserveAmount_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'OutstandingReserveAmount',
                title: 'Reserva Pendiente',
                formatter: 'HT5CompleteStudyOfClientIndicatedSummarySupport.OutstandingReserveAmount_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }]
        });



    };


    this.ClaimRowToInput = function (row) {
        HT5CompleteStudyOfClientIndicatedSummarySupport.currentRow = row;
        AutoNumeric.set('#ClaimID', row.ClaimID);
        $('#ClaimDate').val(generalSupport.ToJavaScriptDateCustom(row.ClaimDate, generalSupport.DateFormat()));
        AutoNumeric.set('#CurrentReserveAmount', row.CurrentReserveAmount);
        AutoNumeric.set('#OutstandingReserveAmount', row.OutstandingReserveAmount);

    };
    this.PendingUnderwritingCaseTblSetup = function (table) {
        HT5CompleteStudyOfClientIndicatedSummarySupport.LookUpForStatus0('');
        table.bootstrapTable({
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            uniqueId: 'UnderwritingCaseID',
            columns: [{
                field: 'UnderwritingCaseID',
                title: 'Caso',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'Status',
                title: 'Estado',
                formatter: 'HT5CompleteStudyOfClientIndicatedSummarySupport.LookUpForStatus0Formatter',
                sortable: false,
                halign: 'center'
            }, {
                field: 'OpenDate',
                title: 'Fecha  apertura',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: false,
                halign: 'center',
                align: 'center'
            }]
        });



    };


    this.PendingUnderwritingCaseRowToInput = function (row) {
        HT5CompleteStudyOfClientIndicatedSummarySupport.currentRow = row;
        $('#pUnderwritingCaseID').val(row.UnderwritingCaseID);
        HT5CompleteStudyOfClientIndicatedSummarySupport.LookUpForStatus0(row.Status, '');
        $('#pOpenDate').val(generalSupport.ToJavaScriptDateCustom(row.OpenDate, generalSupport.DateFormat()));

    };
    this.ApprovedUnderwritingCaseTblSetup = function (table) {
        HT5CompleteStudyOfClientIndicatedSummarySupport.LookUpForStatus1('');
        table.bootstrapTable({
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            uniqueId: 'UnderwritingCaseID',
            columns: [{
                field: 'UnderwritingCaseID',
                title: 'Caso',
                formatter: 'HT5CompleteStudyOfClientIndicatedSummarySupport.UnderwritingCaseID1_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'Status',
                title: 'Estado',
                formatter: 'HT5CompleteStudyOfClientIndicatedSummarySupport.LookUpForStatus1Formatter',
                sortable: false,
                halign: 'center'
            }, {
                field: 'OpenDate',
                title: 'Fecha apertura',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: false,
                halign: 'center',
                align: 'center'
            }, {
                field: 'PolicyID',
                title: 'Póliza',
                formatter: 'HT5CompleteStudyOfClientIndicatedSummarySupport.PolicyID_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }]
        });



    };


    this.ApprovedUnderwritingCaseRowToInput = function (row) {
        HT5CompleteStudyOfClientIndicatedSummarySupport.currentRow = row;
        AutoNumeric.set('#UnderwritingCaseID1', row.UnderwritingCaseID);
        HT5CompleteStudyOfClientIndicatedSummarySupport.LookUpForStatus1(row.Status, '');
        $('#OpenDate1').val(generalSupport.ToJavaScriptDateCustom(row.OpenDate, generalSupport.DateFormat()));
        AutoNumeric.set('#PolicyID', row.PolicyID);

    };
    this.ReviewUnderwritingCaseTblSetup = function (table) {
        HT5CompleteStudyOfClientIndicatedSummarySupport.LookUpForStatus2('');
        table.bootstrapTable({
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            uniqueId: 'UnderwritingCaseID',
            columns: [{
                field: 'UnderwritingCaseID',
                title: 'Caso',
                formatter: 'HT5CompleteStudyOfClientIndicatedSummarySupport.UnderwritingCaseID2_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'Status',
                title: 'Estado',
                formatter: 'HT5CompleteStudyOfClientIndicatedSummarySupport.LookUpForStatus2Formatter',
                sortable: false,
                halign: 'center'
            }, {
                field: 'OpenDate',
                title: 'Fecha apertura',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: false,
                halign: 'center',
                align: 'center'
            }]
        });



    };


    this.ReviewUnderwritingCaseRowToInput = function (row) {
        HT5CompleteStudyOfClientIndicatedSummarySupport.currentRow = row;
        AutoNumeric.set('#UnderwritingCaseID2', row.UnderwritingCaseID);
        HT5CompleteStudyOfClientIndicatedSummarySupport.LookUpForStatus2(row.Status, '');
        $('#OpenDate2').val(generalSupport.ToJavaScriptDateCustom(row.OpenDate, generalSupport.DateFormat()));

    };
    this.DeclinedUnderwritingCaseTblSetup = function (table) {
        table.bootstrapTable({
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            uniqueId: 'UnderwritingCaseID',
            columns: [{
                field: 'UnderwritingCaseID',
                title: 'Caso',
                formatter: 'HT5CompleteStudyOfClientIndicatedSummarySupport.UnderwritingCaseID3_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'OpenDate',
                title: 'Fecha apertura',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: false,
                halign: 'center',
                align: 'center'
            }]
        });



    };


    this.DeclinedUnderwritingCaseRowToInput = function (row) {
        HT5CompleteStudyOfClientIndicatedSummarySupport.currentRow = row;
        AutoNumeric.set('#UnderwritingCaseID3', row.UnderwritingCaseID);
        $('#OpenDate3').val(generalSupport.ToJavaScriptDateCustom(row.OpenDate, generalSupport.DateFormat()));

    };


    this.PolicyIDa_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.InsuredAmounta_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: -999999999999
        });
      };
    this.PolicyIDc_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.PolicyIDp_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.AnnualPremiump_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 2,
            minimumValue: -9999999999
        });
      };
    this.ClaimID_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.CurrentReserveAmount_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: -999999999999
        });
      };
    this.OutstandingReserveAmount_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: -999999999999
        });
      };
    this.UnderwritingCaseID1_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.PolicyID_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.UnderwritingCaseID2_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.UnderwritingCaseID3_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };


};

$(document).ready(function () {
    moment.locale(generalSupport.UserContext().languageName);
    
    if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage)
        masterSupport.setPageTitle('HT5 Resumen del cliente');
        

    HT5CompleteStudyOfClientIndicatedSummarySupport.ControlBehaviour();
    HT5CompleteStudyOfClientIndicatedSummarySupport.ControlActions();
    HT5CompleteStudyOfClientIndicatedSummarySupport.ValidateSetup();
    HT5CompleteStudyOfClientIndicatedSummarySupport.Initialization();

    $("#CurrentPolicyCollectionTblPlaceHolder").replaceWith('<table id="CurrentPolicyCollectionTbl"></table>');
    HT5CompleteStudyOfClientIndicatedSummarySupport.CurrentPolicyCollectionTblSetup($('#CurrentPolicyCollectionTbl'));
    $("#CancelledPolicyCollectionTblPlaceHolder").replaceWith('<table id="CancelledPolicyCollectionTbl"></table>');
    HT5CompleteStudyOfClientIndicatedSummarySupport.CancelledPolicyCollectionTblSetup($('#CancelledPolicyCollectionTbl'));
    $("#OutstandingPremiumTblPlaceHolder").replaceWith('<table id="OutstandingPremiumTbl"></table>');
    HT5CompleteStudyOfClientIndicatedSummarySupport.OutstandingPremiumTblSetup($('#OutstandingPremiumTbl'));
    $("#ClaimTblPlaceHolder").replaceWith('<table id="ClaimTbl"></table>');
    HT5CompleteStudyOfClientIndicatedSummarySupport.ClaimTblSetup($('#ClaimTbl'));
    $("#PendingUnderwritingCaseTblPlaceHolder").replaceWith('<table id="PendingUnderwritingCaseTbl"><caption >Pendientes</caption></table>');
    HT5CompleteStudyOfClientIndicatedSummarySupport.PendingUnderwritingCaseTblSetup($('#PendingUnderwritingCaseTbl'));
    $("#ApprovedUnderwritingCaseTblPlaceHolder").replaceWith('<table id="ApprovedUnderwritingCaseTbl"><caption >Aprobadas</caption></table>');
    HT5CompleteStudyOfClientIndicatedSummarySupport.ApprovedUnderwritingCaseTblSetup($('#ApprovedUnderwritingCaseTbl'));
    $("#ReviewUnderwritingCaseTblPlaceHolder").replaceWith('<table id="ReviewUnderwritingCaseTbl"><caption >En revisión</caption></table>');
    HT5CompleteStudyOfClientIndicatedSummarySupport.ReviewUnderwritingCaseTblSetup($('#ReviewUnderwritingCaseTbl'));
    $("#DeclinedUnderwritingCaseTblPlaceHolder").replaceWith('<table id="DeclinedUnderwritingCaseTbl"><caption >Declinadas</caption></table>');
    HT5CompleteStudyOfClientIndicatedSummarySupport.DeclinedUnderwritingCaseTblSetup($('#DeclinedUnderwritingCaseTbl'));




});

