var H5NNConsultaDeReciboSupport = new function () {

    this.currentRow = {};
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#H5NNConsultaDeReciboFormId').val(),
            Recibo: generalSupport.NumericValue('#Recibo', -9999999999, 9999999999),
            RecordEffectiveDate: generalSupport.DatePickerValueInputToObject('#RecordEffectiveDate')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        $('#H5NNConsultaDeReciboFormId').val(data.InstanceFormId);
        AutoNumeric.set('#Recibo', data.Recibo);
        $('#RecordEffectiveDate').val(generalSupport.ToJavaScriptDateCustom(data.RecordEffectiveDate, generalSupport.DateFormat()));

        H5NNConsultaDeReciboSupport.LookUpForCOMMISS_PRNINTERMED(source);

        if (data.PREMIUM_MO_PREMIUM_MO !== null)
            $('#PREMIUM_MOTbl').bootstrapTable('load', data.PREMIUM_MO_PREMIUM_MO);
        if (data.PREMIUM_CE_PREMIUM_CE !== null)
            $('#PREMIUM_CETbl').bootstrapTable('load', data.PREMIUM_CE_PREMIUM_CE);
        if (data.COMMISS_PR_COMMISS_PR !== null)
            $('#COMMISS_PRTbl').bootstrapTable('load', data.COMMISS_PR_COMMISS_PR);
        if (data.DETAIL_PRE_DETAIL_PRE !== null)
            $('#DETAIL_PRETbl').bootstrapTable('load', data.DETAIL_PRE_DETAIL_PRE);
        if (data.AGREEMENT_AGREEMENT !== null)
            $('#AGREEMENTTbl').bootstrapTable('load', data.AGREEMENT_AGREEMENT);
        if (data.FINANC_DRA_FINANC_DRA !== null)
            $('#FINANC_DRATbl').bootstrapTable('load', data.FINANC_DRA_FINANC_DRA);
        if (data.FINANCE_CO_FINANCE_CO !== null)
            $('#FINANCE_COTbl').bootstrapTable('load', data.FINANCE_CO_FINANCE_CO);
        H5NNConsultaDeReciboSupport.ItemsTblRequest();
        if (data.Items_Item !== null)
            $('#ItemsTbl').bootstrapTable('load', data.Items_Item);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#Recibo', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });




        $('#RecordEffectiveDate_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });


    };

    this.ActionProcess = function (data, source) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
                H5NNConsultaDeReciboSupport.ObjectToInput(data.d.Data, source);
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };



    this.PREMIUM_MO_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5NNConsultaDeReciboActions.aspx/PREMIUM_MOSelectCommandActionPREMIUM_MO", false,
            JSON.stringify({                 PREMIUMMOSCERTYPE1: row.SCERTYPE,
                PREMIUMMONBRANCH2: row.NBRANCH,
                PREMIUMMONPRODUCT3: row.NPRODUCT,
                PREMIUMMONRECEIPT4: row.NRECEIPT,
                PREMIUMMONDIGIT5: row.NDIGIT,
                PREMIUMMONPAYNUMBE6: row.NPAYNUMBE }),
            function (data) {
                
                 if (data.d.Count !== 0)
                                countData = data.d.Data.Result; 
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

            });
        return returnData;
    };
    this.PREMIUM_CE_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5NNConsultaDeReciboActions.aspx/PREMIUM_CESelectCommandActionPREMIUM_CE", false,
            JSON.stringify({                 PREMIUMCESCERTYPE1: row.SCERTYPE,
                PREMIUMCENBRANCH2: row.NBRANCH,
                PREMIUMCENPRODUCT3: row.NPRODUCT,
                PREMIUMCENRECEIPT4: row.NRECEIPT,
                PREMIUMCENDIGIT5: row.NDIGIT,
                PREMIUMCENPAYNUMBE6: row.NPAYNUMBE }),
            function (data) {
                
                 if (data.d.Count !== 0)
                                countData = data.d.Data.Result; 
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

            });
        return returnData;
    };
    this.COMMISS_PR_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5NNConsultaDeReciboActions.aspx/COMMISS_PRSelectCommandActionCOMMISS_PR", false,
            JSON.stringify({                 COMMISSPRSCERTYPE1: row.SCERTYPE,
                COMMISSPRNBRANCH2: row.NBRANCH,
                COMMISSPRNPRODUCT3: row.NPRODUCT,
                COMMISSPRNRECEIPT4: row.NRECEIPT,
                COMMISSPRNDIGIT5: row.NDIGIT,
                COMMISSPRNPAYNUMBE6: row.NPAYNUMBE }),
            function (data) {
                
                 if (data.d.Count !== 0)
                                countData = data.d.Data.Result; 
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

            });
        return returnData;
    };
    this.DETAIL_PRE_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5NNConsultaDeReciboActions.aspx/DETAIL_PRESelectCommandActionDETAIL_PRE", false,
            JSON.stringify({                 DETAILPRESCERTYPE1: row.SCERTYPE,
                DETAILPRENBRANCH2: row.NBRANCH,
                DETAILPRENPRODUCT3: row.NPRODUCT,
                DETAILPRENRECEIPT4: row.NRECEIPT,
                DETAILPRENDIGIT5: row.NDIGIT,
                DETAILPRENPAYNUMBE6: row.NPAYNUMBE }),
            function (data) {
                
                 if (data.d.Count !== 0)
                                countData = data.d.Data.Result; 
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

            });
        return returnData;
    };
    this.AGREEMENT_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5NNConsultaDeReciboActions.aspx/AGREEMENTSelectCommandActionAGREEMENT", false,
            JSON.stringify({                 AGREEMENTNCODAGREE1: row.NCOD_AGREE }),
            function (data) {
                
                 if (data.d.Count !== 0)
                                countData = data.d.Data.Result; 
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

            });
        return returnData;
    };
    this.FINANCE_CO_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5NNConsultaDeReciboActions.aspx/FINANCE_COSelectCommandActionFINANCE_CO", false,
            JSON.stringify({                 FINANCECONCONTRAT1: row.NCONTRAT }),
            function (data) {
                
                 if (data.d.Count !== 0)
                                countData = data.d.Data.Result; 
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

            });
        return returnData;
    };
    this.FINANC_DRA_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5NNConsultaDeReciboActions.aspx/FINANC_DRASelectCommandActionFINANC_DRA", false,
            JSON.stringify({                 FINANCDRANCONTRAT1: row.NCONTRAT }),
            function (data) {
                
                 if (data.d.Count !== 0)
                                countData = data.d.Data.Result; 
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

            });
        return returnData;
    };

    this.ControlActions = function () {

        $('#btnOk').click(function (event) {
            var formInstance = $("#H5NNConsultaDeReciboMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#btnOk'));
                btnLoading.start();
                H5NNConsultaDeReciboSupport.ItemsTblRequest();
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();
    
        $("#H5NNConsultaDeReciboMainForm").validate({
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
                Recibo: {
                    AutoNumericRequired: true,
                    AutoNumericMinValue: -9999999999,
                    AutoNumericMaxValue: 9999999999
                },
                RecordEffectiveDate: {
                    required: true,
                    DatePicker: true
                }
            },
            messages: {
                Recibo: {
                    AutoNumericRequired: 'El campo es requerido',
                    AutoNumericMinValue: 'El valor mínimo permitido es -9999999999',
                    AutoNumericMaxValue: 'El valor máximo permitido es 9999999999'
                },
                RecordEffectiveDate: {
                    required: 'El campo es requerido',
                    DatePicker: 'La fecha indicada no es válida'
                }
            }
        });

    };
    this.LookUpForPREMIUMSDIRDEBITFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#PREMIUMSDIRDEBIT>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForCOMMISS_PRNINTERMEDFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#COMMISS_PRNINTERMED>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForCOMMISS_PRNINTERMED = function (defaultValue, source) {
        var ctrol = $('#COMMISS_PRNINTERMED');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.SyncWebMethod("/fasi/dli/queries/H5NNConsultaDeReciboActions.aspx/LookUpForCOMMISS_PRNINTERMED", false,
                JSON.stringify({ id: $('#H5NNConsultaDeReciboFormId').val() }),
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
    this.LookUpForDETAIL_PRESTYPE_DETAIFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#DETAIL_PRESTYPE_DETAI>option[value='" + value + "']").text();
        }
        return result;
    };

    this.ItemsTblSetup = function (table) {
        table.bootstrapTable({
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            detailView: true,
            onExpandRow: H5NNConsultaDeReciboSupport.ItemsTblExpandRow,
            columns: [{
                field: 'NRECEIPT',
                title: 'Recibo',
                formatter: 'H5NNConsultaDeReciboSupport.PREMIUMNRECEIPT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'SLEADINVO',
                title: 'Factura',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NBRANCH',
                title: 'Ramo',
                formatter: 'H5NNConsultaDeReciboSupport.PREMIUMNBRANCH_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NBRANCHDesc',
                title: 'Ramo',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NPRODUCT',
                title: 'Producto',
                formatter: 'H5NNConsultaDeReciboSupport.PREMIUMNPRODUCT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NPRODUCTDesc',
                title: 'Producto',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NPOLICY',
                title: 'Póliza',
                formatter: 'H5NNConsultaDeReciboSupport.PREMIUMNPOLICY_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'SCLIENT',
                title: 'Cliente',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'SCLIENTDesc',
                title: 'Cliente',
                sortable: true,
                halign: 'center'
            }, {
                field: 'DISSUEDAT',
                title: 'F. Emisión',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'DEFFECDATE',
                title: 'Inicio de vigencia',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'DEXPIRDAT',
                title: 'Vencimiento',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'DNULLDATE',
                title: 'F.Anulación',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'NNULLCODE',
                title: 'Causa de anulación',
                formatter: 'H5NNConsultaDeReciboSupport.PREMIUMNNULLCODE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NNULLCODEDesc',
                title: 'Causa de anulación',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NPAYNUMBE',
                title: 'Nro.Pago',
                formatter: 'H5NNConsultaDeReciboSupport.PREMIUMNPAYNUMBE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'SDIRDEBIT',
                title: 'Domiciliada',
                formatter: 'H5NNConsultaDeReciboSupport.LookUpForPREMIUMSDIRDEBITFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NCURRENCY',
                title: 'Moneda',
                formatter: 'H5NNConsultaDeReciboSupport.PREMIUMNCURRENCY_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NCURRENCYDesc',
                title: 'Moneda',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NPREMIUM',
                title: 'Prima total',
                formatter: 'H5NNConsultaDeReciboSupport.PREMIUMNPREMIUM_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NBALANCE',
                title: 'Balance (Saldo pendiente)',
                formatter: 'H5NNConsultaDeReciboSupport.PREMIUMNBALANCE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NCOMAMOU',
                title: 'Comisión',
                formatter: 'H5NNConsultaDeReciboSupport.PREMIUMNCOMAMOU_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NTAXAMOU',
                title: 'Impuesto',
                formatter: 'H5NNConsultaDeReciboSupport.PREMIUMNTAXAMOU_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NSTATUS_PRE',
                title: 'Estado',
                formatter: 'H5NNConsultaDeReciboSupport.PREMIUMNSTATUS_PRE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NSTATUS_PREDesc',
                title: 'Estado',
                sortable: true,
                halign: 'center'
            }, {
                field: 'DSTATDATE',
                title: 'F.Estado',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'DPAYDATE',
                title: 'Fecha de último pago',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'SCERTYPE',
                title: 'Tipo de Registro',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NDIGIT',
                title: 'Dígito de Control',
                formatter: 'H5NNConsultaDeReciboSupport.PREMIUMNDIGIT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NPERIOD',
                title: 'Período',
                formatter: 'H5NNConsultaDeReciboSupport.PREMIUMNPERIOD_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'DLIMITDATE',
                title: 'F.Límite de pago',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'NTRATYPEI',
                title: 'Origen del recibo',
                formatter: 'H5NNConsultaDeReciboSupport.PREMIUMNTRATYPEI_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NTRATYPEIDesc',
                title: 'Origen del recibo',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NCONTRAT',
                title: 'ContratoDeFinanciamiento',
                formatter: 'H5NNConsultaDeReciboSupport.PREMIUMNCONTRAT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NCOD_AGREE',
                title: 'Código del Convenio',
                formatter: 'H5NNConsultaDeReciboSupport.PREMIUMNCOD_AGREE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }]
        });



    };


    this.ItemsRowToInput = function (row) {
        H5NNConsultaDeReciboSupport.currentRow = row;
        AutoNumeric.set('#PREMIUMNRECEIPT', row.NRECEIPT);
        $('#PREMIUMSLEADINVO').val(row.SLEADINVO);
        AutoNumeric.set('#PREMIUMNBRANCH', row.NBRANCH);
        $('#PREMIUMNBRANCHDesc').val(row.NBRANCHDesc);
        AutoNumeric.set('#PREMIUMNPRODUCT', row.NPRODUCT);
        $('#PREMIUMNPRODUCTDesc').val(row.NPRODUCTDesc);
        AutoNumeric.set('#PREMIUMNPOLICY', row.NPOLICY);
        $('#PREMIUMSCLIENT').val(row.SCLIENT);
        $('#PREMIUMSCLIENTDesc').val(row.SCLIENTDesc);
        $('#PREMIUMDISSUEDAT').val(generalSupport.ToJavaScriptDateCustom(row.DISSUEDAT, generalSupport.DateFormat()));
        $('#PREMIUMDEFFECDATE').val(generalSupport.ToJavaScriptDateCustom(row.DEFFECDATE, generalSupport.DateFormat()));
        $('#PREMIUMDEXPIRDAT').val(generalSupport.ToJavaScriptDateCustom(row.DEXPIRDAT, generalSupport.DateFormat()));
        $('#PREMIUMDNULLDATE').val(generalSupport.ToJavaScriptDateCustom(row.DNULLDATE, generalSupport.DateFormat()));
        AutoNumeric.set('#PREMIUMNNULLCODE', row.NNULLCODE);
        $('#PREMIUMNNULLCODEDesc').val(row.NNULLCODEDesc);
        AutoNumeric.set('#PREMIUMNPAYNUMBE', row.NPAYNUMBE);
        AutoNumeric.set('#PREMIUMNCURRENCY', row.NCURRENCY);
        $('#PREMIUMNCURRENCYDesc').val(row.NCURRENCYDesc);
        AutoNumeric.set('#PREMIUMNPREMIUM', row.NPREMIUM);
        AutoNumeric.set('#PREMIUMNBALANCE', row.NBALANCE);
        AutoNumeric.set('#PREMIUMNCOMAMOU', row.NCOMAMOU);
        AutoNumeric.set('#PREMIUMNTAXAMOU', row.NTAXAMOU);
        AutoNumeric.set('#PREMIUMNSTATUS_PRE', row.NSTATUS_PRE);
        $('#PREMIUMNSTATUS_PREDesc').val(row.NSTATUS_PREDesc);
        $('#PREMIUMDSTATDATE').val(generalSupport.ToJavaScriptDateCustom(row.DSTATDATE, generalSupport.DateFormat()));
        $('#PREMIUMDPAYDATE').val(generalSupport.ToJavaScriptDateCustom(row.DPAYDATE, generalSupport.DateFormat()));
        $('#PREMIUMSCERTYPE').val(row.SCERTYPE);
        AutoNumeric.set('#PREMIUMNDIGIT', row.NDIGIT);
        AutoNumeric.set('#PREMIUMNPERIOD', row.NPERIOD);
        $('#PREMIUMDLIMITDATE').val(generalSupport.ToJavaScriptDateCustom(row.DLIMITDATE, generalSupport.DateFormat()));
        AutoNumeric.set('#PREMIUMNTRATYPEI', row.NTRATYPEI);
        $('#PREMIUMNTRATYPEIDesc').val(row.NTRATYPEIDesc);
        AutoNumeric.set('#PREMIUMNCONTRAT', row.NCONTRAT);
        AutoNumeric.set('#PREMIUMNCOD_AGREE', row.NCOD_AGREE);

    };
    this.ItemsTblRequest = function (params) {
        app.core.AsyncWebMethod("/fasi/dli/queries/H5NNConsultaDeReciboActions.aspx/ItemsTblDataLoad", false,
            JSON.stringify({
                                RECIBONRECEIPT1: generalSupport.NumericValue('#Recibo', -9999999999, 9999999999)
            }),
            function (data) {
                    $('#ItemsTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);


            });
    };
    this.PREMIUM_MOTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'NID',
                title: 'Consecutivo',
                formatter: 'H5NNConsultaDeReciboSupport.PREMIUM_MONID_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NTRANSAC',
                title: 'Transacción',
                formatter: 'H5NNConsultaDeReciboSupport.PREMIUM_MONTRANSAC_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NTYPE',
                title: 'Movimiento',
                formatter: 'H5NNConsultaDeReciboSupport.PREMIUM_MONTYPE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NTYPEDesc',
                title: 'Movimiento',
                sortable: true,
                halign: 'center'
            }, {
                field: 'DCOMPDATE',
                title: 'Fecha',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'NAMOUNT',
                title: 'Monto de prima',
                formatter: 'H5NNConsultaDeReciboSupport.PREMIUM_MONAMOUNT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NCURRENCY',
                title: 'Moneda',
                formatter: 'H5NNConsultaDeReciboSupport.PREMIUM_MONCURRENCY_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NCURRENCYDesc',
                title: 'Moneda',
                sortable: true,
                halign: 'center'
            }]
        });

        H5NNConsultaDeReciboSupport.$el = table;
        H5NNConsultaDeReciboSupport.PREMIUM_MOTblRequest();
      };

    this.PREMIUM_MORowToInput = function (row) {
        H5NNConsultaDeReciboSupport.currentRow = row;
        AutoNumeric.set('#PREMIUM_MONID', row.NID);
        AutoNumeric.set('#PREMIUM_MONTRANSAC', row.NTRANSAC);
        AutoNumeric.set('#PREMIUM_MONTYPE', row.NTYPE);
        $('#PREMIUM_MONTYPEDesc').val(row.NTYPEDesc);
        $('#PREMIUM_MODCOMPDATE').val(generalSupport.ToJavaScriptDateCustom(row.DCOMPDATE, generalSupport.DateFormat()));
        AutoNumeric.set('#PREMIUM_MONAMOUNT', row.NAMOUNT);
        AutoNumeric.set('#PREMIUM_MONCURRENCY', row.NCURRENCY);
        $('#PREMIUM_MONCURRENCYDesc').val(row.NCURRENCYDesc);

    };
    this.PREMIUM_MOTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5NNConsultaDeReciboActions.aspx/PREMIUM_MOTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                PREMIUMMOSCERTYPE1: row.SCERTYPE,
                PREMIUMMONBRANCH2: row.NBRANCH,
                PREMIUMMONPRODUCT3: row.NPRODUCT,
                PREMIUMMONRECEIPT4: row.NRECEIPT,
                PREMIUMMONDIGIT5: row.NDIGIT,
                PREMIUMMONPAYNUMBE6: row.NPAYNUMBE
            }),
            success: function (data) {
                if (data.d.Success === true) {
                    table.bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

                }
                else
                    generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            },
            error: function (qXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    };
    this.PREMIUM_CETblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'NCERTIF',
                title: 'Certificado',
                formatter: 'H5NNConsultaDeReciboSupport.PREMIUM_CENCERTIF_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NBILL_ITEM',
                title: 'Concepto',
                formatter: 'H5NNConsultaDeReciboSupport.PREMIUM_CENBILL_ITEM_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NBILL_ITEMDesc',
                title: 'Concepto',
                sortable: true,
                halign: 'center'
            }, {
                field: 'DSTARTDATE',
                title: 'Inicio de vigencia',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'DEXPIRDAT',
                title: 'Vencimiento',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'NPREMIUM',
                title: 'Prima',
                formatter: 'H5NNConsultaDeReciboSupport.PREMIUM_CENPREMIUM_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NTRATYPEI',
                title: 'Origen del recibo',
                formatter: 'H5NNConsultaDeReciboSupport.PREMIUM_CENTRATYPEI_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NTRATYPEIDesc',
                title: 'Origen del recibo',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NCAPITAL',
                title: 'Capital',
                formatter: 'H5NNConsultaDeReciboSupport.PREMIUM_CENCAPITAL_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }]
        });

        H5NNConsultaDeReciboSupport.$el = table;
        H5NNConsultaDeReciboSupport.PREMIUM_CETblRequest();
      };

    this.PREMIUM_CERowToInput = function (row) {
        H5NNConsultaDeReciboSupport.currentRow = row;
        AutoNumeric.set('#PREMIUM_CENCERTIF', row.NCERTIF);
        AutoNumeric.set('#PREMIUM_CENBILL_ITEM', row.NBILL_ITEM);
        $('#PREMIUM_CENBILL_ITEMDesc').val(row.NBILL_ITEMDesc);
        $('#PREMIUM_CEDSTARTDATE').val(generalSupport.ToJavaScriptDateCustom(row.DSTARTDATE, generalSupport.DateFormat()));
        $('#PREMIUM_CEDEXPIRDAT').val(generalSupport.ToJavaScriptDateCustom(row.DEXPIRDAT, generalSupport.DateFormat()));
        AutoNumeric.set('#PREMIUM_CENPREMIUM', row.NPREMIUM);
        AutoNumeric.set('#PREMIUM_CENTRATYPEI', row.NTRATYPEI);
        $('#PREMIUM_CENTRATYPEIDesc').val(row.NTRATYPEIDesc);
        AutoNumeric.set('#PREMIUM_CENCAPITAL', row.NCAPITAL);

    };
    this.PREMIUM_CETblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5NNConsultaDeReciboActions.aspx/PREMIUM_CETblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                PREMIUMCESCERTYPE1: row.SCERTYPE,
                PREMIUMCENBRANCH2: row.NBRANCH,
                PREMIUMCENPRODUCT3: row.NPRODUCT,
                PREMIUMCENRECEIPT4: row.NRECEIPT,
                PREMIUMCENDIGIT5: row.NDIGIT,
                PREMIUMCENPAYNUMBE6: row.NPAYNUMBE
            }),
            success: function (data) {
                if (data.d.Success === true) {
                    table.bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

                }
                else
                    generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            },
            error: function (qXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    };
    this.COMMISS_PRTblSetup = function (table) {
        H5NNConsultaDeReciboSupport.LookUpForCOMMISS_PRNINTERMED('');
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'NINTERMED',
                title: 'Productor',
                formatter: 'H5NNConsultaDeReciboSupport.LookUpForCOMMISS_PRNINTERMEDFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NROLE',
                title: 'Tipo',
                formatter: 'H5NNConsultaDeReciboSupport.COMMISS_PRNROLE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NROLEDesc',
                title: 'Tipo',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NAMOUNT',
                title: 'Comisión',
                formatter: 'H5NNConsultaDeReciboSupport.COMMISS_PRNAMOUNT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NPERCENT',
                title: '%Comisión',
                formatter: 'H5NNConsultaDeReciboSupport.COMMISS_PRNPERCENT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NSHARE',
                title: '%Participación',
                formatter: 'H5NNConsultaDeReciboSupport.COMMISS_PRNSHARE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }]
        });

        H5NNConsultaDeReciboSupport.$el = table;
        H5NNConsultaDeReciboSupport.COMMISS_PRTblRequest();
      };

    this.COMMISS_PRRowToInput = function (row) {
        H5NNConsultaDeReciboSupport.currentRow = row;
        H5NNConsultaDeReciboSupport.LookUpForCOMMISS_PRNINTERMED(row.NINTERMED, '');
        AutoNumeric.set('#COMMISS_PRNROLE', row.NROLE);
        $('#COMMISS_PRNROLEDesc').val(row.NROLEDesc);
        AutoNumeric.set('#COMMISS_PRNAMOUNT', row.NAMOUNT);
        AutoNumeric.set('#COMMISS_PRNPERCENT', row.NPERCENT);
        AutoNumeric.set('#COMMISS_PRNSHARE', row.NSHARE);

    };
    this.COMMISS_PRTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5NNConsultaDeReciboActions.aspx/COMMISS_PRTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                COMMISSPRSCERTYPE1: row.SCERTYPE,
                COMMISSPRNBRANCH2: row.NBRANCH,
                COMMISSPRNPRODUCT3: row.NPRODUCT,
                COMMISSPRNRECEIPT4: row.NRECEIPT,
                COMMISSPRNDIGIT5: row.NDIGIT,
                COMMISSPRNPAYNUMBE6: row.NPAYNUMBE
            }),
            success: function (data) {
                if (data.d.Success === true) {
                    table.bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

                }
                else
                    generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            },
            error: function (qXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    };
    this.DETAIL_PRETblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'NID_BILL',
                title: 'Consecutivo',
                formatter: 'H5NNConsultaDeReciboSupport.DETAIL_PRENID_BILL_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'STYPE_DETAI',
                title: 'Detalle',
                formatter: 'H5NNConsultaDeReciboSupport.LookUpForDETAIL_PRESTYPE_DETAIFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NDET_CODE',
                title: 'Código',
                formatter: 'H5NNConsultaDeReciboSupport.DETAIL_PRENDET_CODE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NBILL_ITEM',
                title: 'Concepto',
                formatter: 'H5NNConsultaDeReciboSupport.DETAIL_PRENBILL_ITEM_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NBILL_ITEMDesc',
                title: 'Concepto',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NPREMIUM',
                title: 'Prima',
                formatter: 'H5NNConsultaDeReciboSupport.DETAIL_PRENPREMIUM_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NRECAMOUNT',
                title: 'Recargo',
                formatter: 'H5NNConsultaDeReciboSupport.DETAIL_PRENRECAMOUNT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NDESCAMOUNT',
                title: 'Descuento',
                formatter: 'H5NNConsultaDeReciboSupport.DETAIL_PRENDESCAMOUNT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NCOMMI_RATE',
                title: '%Comisión',
                formatter: 'H5NNConsultaDeReciboSupport.DETAIL_PRENCOMMI_RATE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NCAPITAL',
                title: 'Capital asegurado',
                formatter: 'H5NNConsultaDeReciboSupport.DETAIL_PRENCAPITAL_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NCOMMISION',
                title: 'Comisión',
                formatter: 'H5NNConsultaDeReciboSupport.DETAIL_PRENCOMMISION_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NTAX',
                title: '%Impuesto',
                formatter: 'H5NNConsultaDeReciboSupport.DETAIL_PRENTAX_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NTAXAMOUNT',
                title: 'Impuestos',
                formatter: 'H5NNConsultaDeReciboSupport.DETAIL_PRENTAXAMOUNT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }]
        });

        H5NNConsultaDeReciboSupport.$el = table;
        H5NNConsultaDeReciboSupport.DETAIL_PRETblRequest();
      };

    this.DETAIL_PRERowToInput = function (row) {
        H5NNConsultaDeReciboSupport.currentRow = row;
        AutoNumeric.set('#DETAIL_PRENID_BILL', row.NID_BILL);
        AutoNumeric.set('#DETAIL_PRENDET_CODE', row.NDET_CODE);
        AutoNumeric.set('#DETAIL_PRENBILL_ITEM', row.NBILL_ITEM);
        $('#DETAIL_PRENBILL_ITEMDesc').val(row.NBILL_ITEMDesc);
        AutoNumeric.set('#DETAIL_PRENPREMIUM', row.NPREMIUM);
        AutoNumeric.set('#DETAIL_PRENRECAMOUNT', row.NRECAMOUNT);
        AutoNumeric.set('#DETAIL_PRENDESCAMOUNT', row.NDESCAMOUNT);
        AutoNumeric.set('#DETAIL_PRENCOMMI_RATE', row.NCOMMI_RATE);
        AutoNumeric.set('#DETAIL_PRENCAPITAL', row.NCAPITAL);
        AutoNumeric.set('#DETAIL_PRENCOMMISION', row.NCOMMISION);
        AutoNumeric.set('#DETAIL_PRENTAX', row.NTAX);
        AutoNumeric.set('#DETAIL_PRENTAXAMOUNT', row.NTAXAMOUNT);

    };
    this.DETAIL_PRETblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5NNConsultaDeReciboActions.aspx/DETAIL_PRETblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                DETAILPRESCERTYPE1: row.SCERTYPE,
                DETAILPRENBRANCH2: row.NBRANCH,
                DETAILPRENPRODUCT3: row.NPRODUCT,
                DETAILPRENRECEIPT4: row.NRECEIPT,
                DETAILPRENDIGIT5: row.NDIGIT,
                DETAILPRENPAYNUMBE6: row.NPAYNUMBE
            }),
            success: function (data) {
                if (data.d.Success === true) {
                    table.bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

                }
                else
                    generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            },
            error: function (qXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    };
    this.AGREEMENTTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'NCOD_AGREE',
                title: 'Código del Convenio',
                formatter: 'H5NNConsultaDeReciboSupport.AGREEMENTNCOD_AGREE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NQ_DRAFT',
                title: 'Cantidad de Cuotas',
                formatter: 'H5NNConsultaDeReciboSupport.AGREEMENTNQ_DRAFT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'DINIT_DATE',
                title: 'Fecha de Inicio',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'DEND_DATE',
                title: 'Vencimiento',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }]
        });

        H5NNConsultaDeReciboSupport.$el = table;
        H5NNConsultaDeReciboSupport.AGREEMENTTblRequest();
      };

    this.AGREEMENTRowToInput = function (row) {
        H5NNConsultaDeReciboSupport.currentRow = row;
        AutoNumeric.set('#AGREEMENTNCOD_AGREE', row.NCOD_AGREE);
        AutoNumeric.set('#AGREEMENTNQ_DRAFT', row.NQ_DRAFT);
        $('#AGREEMENTDINIT_DATE').val(generalSupport.ToJavaScriptDateCustom(row.DINIT_DATE, generalSupport.DateFormat()));
        $('#AGREEMENTDEND_DATE').val(generalSupport.ToJavaScriptDateCustom(row.DEND_DATE, generalSupport.DateFormat()));

    };
    this.AGREEMENTTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5NNConsultaDeReciboActions.aspx/AGREEMENTTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                AGREEMENTNCODAGREE1: row.NCOD_AGREE
            }),
            success: function (data) {
                if (data.d.Success === true) {
                    table.bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

                }
                else
                    generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            },
            error: function (qXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    };
    this.FINANC_DRATblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'NDRAFT',
                title: 'Número',
                formatter: 'H5NNConsultaDeReciboSupport.FINANC_DRANDRAFT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NAMOUNT',
                title: 'Monto',
                formatter: 'H5NNConsultaDeReciboSupport.FINANC_DRANAMOUNT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NSTAT_DRAFT',
                title: 'Estado',
                formatter: 'H5NNConsultaDeReciboSupport.FINANC_DRANSTAT_DRAFT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NSTAT_DRAFTDesc',
                title: 'Estado',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NAMOUNT_NET',
                title: 'Monto Neto',
                formatter: 'H5NNConsultaDeReciboSupport.FINANC_DRANAMOUNT_NET_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'DEXPIRDAT',
                title: 'F.Vencimiento',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }]
        });

        H5NNConsultaDeReciboSupport.$el = table;
        H5NNConsultaDeReciboSupport.FINANC_DRATblRequest();
      };

    this.FINANC_DRARowToInput = function (row) {
        H5NNConsultaDeReciboSupport.currentRow = row;
        AutoNumeric.set('#FINANC_DRANDRAFT', row.NDRAFT);
        AutoNumeric.set('#FINANC_DRANAMOUNT', row.NAMOUNT);
        AutoNumeric.set('#FINANC_DRANSTAT_DRAFT', row.NSTAT_DRAFT);
        $('#FINANC_DRANSTAT_DRAFTDesc').val(row.NSTAT_DRAFTDesc);
        AutoNumeric.set('#FINANC_DRANAMOUNT_NET', row.NAMOUNT_NET);
        $('#FINANC_DRADEXPIRDAT').val(generalSupport.ToJavaScriptDateCustom(row.DEXPIRDAT, generalSupport.DateFormat()));

    };
    this.FINANC_DRATblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5NNConsultaDeReciboActions.aspx/FINANC_DRATblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                FINANCDRANCONTRAT1: row.NCONTRAT
            }),
            success: function (data) {
                if (data.d.Success === true) {
                    table.bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

                }
                else
                    generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            },
            error: function (qXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    };
    this.FINANCE_COTblExpandRow = function (index, row, $detail) {
       var tblparentid = $detail.parent().parent().parent()[0].id;
       var html = [];

        var detailShow = H5NNConsultaDeReciboSupport.FINANC_DRA_ShowValidation(row);
        if (detailShow)
        html.push('<table id="FINANC_DRATbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Giros</caption></table>');

        $detail.html(html.join(""));
        if (detailShow)
        H5NNConsultaDeReciboSupport.FINANC_DRATblSetup($detail.find('#FINANC_DRATbl-' + index));

    };
    this.FINANCE_COTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            detailView: true,
            onExpandRow: H5NNConsultaDeReciboSupport.FINANCE_COTblExpandRow,

            columns: [{
                field: 'NCONTRAT',
                title: 'Contrato',
                formatter: 'H5NNConsultaDeReciboSupport.FINANCE_CONCONTRAT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NSTAT_CONTR',
                title: 'Estado',
                formatter: 'H5NNConsultaDeReciboSupport.FINANCE_CONSTAT_CONTR_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NSTAT_CONTRDesc',
                title: 'Estado',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NCURRENCY',
                title: 'Moneda',
                formatter: 'H5NNConsultaDeReciboSupport.FINANCE_CONCURRENCY_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NCURRENCYDesc',
                title: 'Moneda',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NAMOUNT',
                title: 'Monto de prima a financiar',
                formatter: 'H5NNConsultaDeReciboSupport.FINANCE_CONAMOUNT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NQ_DRAFT',
                title: 'Cantidad de cuotas',
                formatter: 'H5NNConsultaDeReciboSupport.FINANCE_CONQ_DRAFT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NAMOUNT_D',
                title: 'Monto cuotas a financiar',
                formatter: 'H5NNConsultaDeReciboSupport.FINANCE_CONAMOUNT_D_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NDSCTO_AMO',
                title: 'Descuento por pronto pago',
                formatter: 'H5NNConsultaDeReciboSupport.FINANCE_CONDSCTO_AMO_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NFRECUENCY',
                title: 'Frecuencia de giros',
                formatter: 'H5NNConsultaDeReciboSupport.FINANCE_CONFRECUENCY_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NFRECUENCYDesc',
                title: 'Frecuencia de giros',
                sortable: true,
                halign: 'center'
            }, {
                field: 'DFIRST_DRAF',
                title: 'Vencimiento 1ra cuota',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'NBILL_DAY',
                title: 'Día de pago',
                formatter: 'H5NNConsultaDeReciboSupport.FINANCE_CONBILL_DAY_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NWAY_PAY',
                title: 'Vía de Pago',
                formatter: 'H5NNConsultaDeReciboSupport.FINANCE_CONWAY_PAY_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NWAY_PAYDesc',
                title: 'Vía de Pago',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NINITIAL',
                title: 'Monto de la inicial',
                formatter: 'H5NNConsultaDeReciboSupport.FINANCE_CONINITIAL_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NINTEREST',
                title: '% interés',
                formatter: 'H5NNConsultaDeReciboSupport.FINANCE_CONINTEREST_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'DNULLDATE',
                title: 'F.Anulación',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }]
        });

        H5NNConsultaDeReciboSupport.$el = table;
        H5NNConsultaDeReciboSupport.FINANCE_COTblRequest();
      };

    this.FINANCE_CORowToInput = function (row) {
        H5NNConsultaDeReciboSupport.currentRow = row;
        AutoNumeric.set('#FINANCE_CONCONTRAT', row.NCONTRAT);
        AutoNumeric.set('#FINANCE_CONSTAT_CONTR', row.NSTAT_CONTR);
        $('#FINANCE_CONSTAT_CONTRDesc').val(row.NSTAT_CONTRDesc);
        AutoNumeric.set('#FINANCE_CONCURRENCY', row.NCURRENCY);
        $('#FINANCE_CONCURRENCYDesc').val(row.NCURRENCYDesc);
        AutoNumeric.set('#FINANCE_CONAMOUNT', row.NAMOUNT);
        AutoNumeric.set('#FINANCE_CONQ_DRAFT', row.NQ_DRAFT);
        AutoNumeric.set('#FINANCE_CONAMOUNT_D', row.NAMOUNT_D);
        AutoNumeric.set('#FINANCE_CONDSCTO_AMO', row.NDSCTO_AMO);
        AutoNumeric.set('#FINANCE_CONFRECUENCY', row.NFRECUENCY);
        $('#FINANCE_CONFRECUENCYDesc').val(row.NFRECUENCYDesc);
        $('#FINANCE_CODFIRST_DRAF').val(generalSupport.ToJavaScriptDateCustom(row.DFIRST_DRAF, generalSupport.DateFormat()));
        AutoNumeric.set('#FINANCE_CONBILL_DAY', row.NBILL_DAY);
        AutoNumeric.set('#FINANCE_CONWAY_PAY', row.NWAY_PAY);
        $('#FINANCE_CONWAY_PAYDesc').val(row.NWAY_PAYDesc);
        AutoNumeric.set('#FINANCE_CONINITIAL', row.NINITIAL);
        AutoNumeric.set('#FINANCE_CONINTEREST', row.NINTEREST);
        $('#FINANCE_CODNULLDATE').val(generalSupport.ToJavaScriptDateCustom(row.DNULLDATE, generalSupport.DateFormat()));

    };
    this.FINANCE_COTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5NNConsultaDeReciboActions.aspx/FINANCE_COTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                FINANCECONCONTRAT1: row.NCONTRAT
            }),
            success: function (data) {
                if (data.d.Success === true) {
                    table.bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

                }
                else
                    generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            },
            error: function (qXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    };
    this.ItemsTblExpandRow = function (index, row, $detail) {
       var tblparentid = $detail.parent().parent().parent()[0].id;
       var html = [];

        var detailShow = H5NNConsultaDeReciboSupport.PREMIUM_MO_ShowValidation(row);
        if (detailShow)
        html.push('<table id="PREMIUM_MOTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Movimiento de Recibos</caption></table>');
        html.push('<table id="PREMIUM_CETbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Detalle de Certificados Facturado en Recibos</caption></table>');
        html.push('<table id="COMMISS_PRTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Comisiones de Facturas</caption></table>');
        html.push('<table id="DETAIL_PRETbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Detalles</caption></table>');
        html.push('<table id="AGREEMENTTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Convenio pagos</caption></table>');
        html.push('<table id="FINANCE_COTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Financiamiento</caption></table>');

        $detail.html(html.join(""));
        if (detailShow)
        H5NNConsultaDeReciboSupport.PREMIUM_MOTblSetup($detail.find('#PREMIUM_MOTbl-' + index));
        H5NNConsultaDeReciboSupport.PREMIUM_CETblSetup($detail.find('#PREMIUM_CETbl-' + index));
        H5NNConsultaDeReciboSupport.COMMISS_PRTblSetup($detail.find('#COMMISS_PRTbl-' + index));
        H5NNConsultaDeReciboSupport.DETAIL_PRETblSetup($detail.find('#DETAIL_PRETbl-' + index));
        H5NNConsultaDeReciboSupport.AGREEMENTTblSetup($detail.find('#AGREEMENTTbl-' + index));
        H5NNConsultaDeReciboSupport.FINANCE_COTblSetup($detail.find('#FINANCE_COTbl-' + index));

    };


    this.PREMIUMNRECEIPT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.PREMIUMNBRANCH_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.PREMIUMNPRODUCT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.PREMIUMNPOLICY_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.PREMIUMNNULLCODE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.PREMIUMNPAYNUMBE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.PREMIUMNCURRENCY_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.PREMIUMNPREMIUM_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.PREMIUMNBALANCE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.PREMIUMNCOMAMOU_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.PREMIUMNTAXAMOU_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.PREMIUMNSTATUS_PRE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.PREMIUMNDIGIT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.PREMIUMNPERIOD_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.PREMIUMNTRATYPEI_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.PREMIUMNCONTRAT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9,
            decimalPlaces: 0,
            minimumValue: -9
        });
      };
    this.PREMIUMNCOD_AGREE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.PREMIUM_MONID_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.PREMIUM_MONTRANSAC_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.PREMIUM_MONTYPE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.PREMIUM_MONAMOUNT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.PREMIUM_MONCURRENCY_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.PREMIUM_CENCERTIF_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.PREMIUM_CENBILL_ITEM_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.PREMIUM_CENPREMIUM_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.PREMIUM_CENTRATYPEI_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.PREMIUM_CENCAPITAL_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.COMMISS_PRNROLE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.COMMISS_PRNAMOUNT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.COMMISS_PRNPERCENT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999,
            decimalPlaces: 2,
            minimumValue: -9999
        });
      };
    this.COMMISS_PRNSHARE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 2,
            minimumValue: -99999
        });
      };
    this.DETAIL_PRENID_BILL_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.DETAIL_PRENDET_CODE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.DETAIL_PRENBILL_ITEM_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.DETAIL_PRENPREMIUM_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.DETAIL_PRENRECAMOUNT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.DETAIL_PRENDESCAMOUNT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.DETAIL_PRENCOMMI_RATE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999999999,
            decimalPlaces: 6,
            minimumValue: -9999999999999999
        });
      };
    this.DETAIL_PRENCAPITAL_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.DETAIL_PRENCOMMISION_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.DETAIL_PRENTAX_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.DETAIL_PRENTAXAMOUNT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.AGREEMENTNCOD_AGREE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.AGREEMENTNQ_DRAFT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.FINANCE_CONCONTRAT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.FINANCE_CONSTAT_CONTR_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.FINANCE_CONCURRENCY_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.FINANCE_CONAMOUNT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.FINANCE_CONQ_DRAFT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.FINANCE_CONAMOUNT_D_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.FINANCE_CONDSCTO_AMO_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.FINANCE_CONFRECUENCY_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.FINANCE_CONBILL_DAY_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.FINANCE_CONWAY_PAY_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.FINANCE_CONINITIAL_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.FINANCE_CONINTEREST_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999,
            decimalPlaces: 2,
            minimumValue: -9999
        });
      };
    this.FINANC_DRANDRAFT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.FINANC_DRANAMOUNT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.FINANC_DRANSTAT_DRAFT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.FINANC_DRANAMOUNT_NET_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };


};

$(document).ready(function () {
    moment.locale(generalSupport.UserContext().languageName);
    
    if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage)
        masterSupport.setPageTitle('H5Información del recibo');
        

    H5NNConsultaDeReciboSupport.ControlBehaviour();
    H5NNConsultaDeReciboSupport.ControlActions();
    H5NNConsultaDeReciboSupport.ValidateSetup();

    AutoNumeric.set('#Recibo', generalSupport.URLNumericValue('Recibo'));
    $('#RecordEffectiveDate').val(generalSupport.URLDateValue('RecordEffectiveDate'));

    $("#ItemsTblPlaceHolder").replaceWith('<table id="ItemsTbl"><caption >Primas</caption></table>');
    H5NNConsultaDeReciboSupport.ItemsTblSetup($('#ItemsTbl'));

    $('#RecordEffectiveDate').val(moment().format(generalSupport.DateFormat()));
        H5NNConsultaDeReciboSupport.ItemsTblRequest();



});

