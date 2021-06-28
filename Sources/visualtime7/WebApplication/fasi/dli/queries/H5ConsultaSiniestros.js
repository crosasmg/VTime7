var H5ConsultaSiniestrosSupport = new function () {

    this.currentRow = {};
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#H5ConsultaSiniestrosFormId').val(),
            Siniestro: generalSupport.NumericValue('#Siniestro', -9999999999, 9999999999),
            RecordEffectiveDate: generalSupport.DatePickerValueInputToObject('#RecordEffectiveDate')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        $('#H5ConsultaSiniestrosFormId').val(data.InstanceFormId);
        AutoNumeric.set('#Siniestro', data.Siniestro);
        $('#RecordEffectiveDate').val(generalSupport.ToJavaScriptDateCustom(data.RecordEffectiveDate, generalSupport.DateFormat()));


        if (data.Cl_cover_Cl_cover !== null)
            $('#Cl_coverTbl').bootstrapTable('load', data.Cl_cover_Cl_cover);
        if (data.Claim_attm_Claim_attm !== null)
            $('#Claim_attmTbl').bootstrapTable('load', data.Claim_attm_Claim_attm);
        if (data.Claim_auto_Claim_auto !== null)
            $('#Claim_autoTbl').bootstrapTable('load', data.Claim_auto_Claim_auto);
        if (data.CLAIM_DAMA_CLAIM_DAMA !== null)
            $('#CLAIM_DAMATbl').bootstrapTable('load', data.CLAIM_DAMA_CLAIM_DAMA);
        if (data.CLAIM_THIR_CLAIM_THIR !== null)
            $('#CLAIM_THIRTbl').bootstrapTable('load', data.CLAIM_THIR_CLAIM_THIR);
        if (data.Life_claim_Life_claim !== null)
            $('#Life_claimTbl').bootstrapTable('load', data.Life_claim_Life_claim);
        if (data.CLAIMBENEF_CLAIMBENEF !== null)
            $('#CLAIMBENEFTbl').bootstrapTable('load', data.CLAIMBENEF_CLAIMBENEF);
        if (data.Claim_his_Claim_his !== null)
            $('#Claim_hisTbl').bootstrapTable('load', data.Claim_his_Claim_his);
        if (data.Claim_case_Claim_case !== null)
            $('#Claim_caseTbl').bootstrapTable('load', data.Claim_case_Claim_case);
        if (data.CHEQUES_CHEQUES !== null)
            $('#CHEQUESTbl').bootstrapTable('load', data.CHEQUES_CHEQUES);
        if (data.CLAIM_HIS2_CLAIM_HIS2 !== null)
            $('#CLAIM_HIS2Tbl').bootstrapTable('load', data.CLAIM_HIS2_CLAIM_HIS2);
        H5ConsultaSiniestrosSupport.ItemsTblRequest();
        if (data.Items_Item !== null)
            $('#ItemsTbl').bootstrapTable('load', data.Items_Item);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#Siniestro', {
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
                H5ConsultaSiniestrosSupport.ObjectToInput(data.d.Data, source);
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };



    this.Claim_case_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5ConsultaSiniestrosActions.aspx/Claim_caseSelectCommandActionCLAIM_CASE", false,
            JSON.stringify({                 CLAIMCASENCLAIM1: row.nClaim }),
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
    this.Cl_cover_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5ConsultaSiniestrosActions.aspx/Cl_coverSelectCommandActionCL_COVER", false,
            JSON.stringify({                 CLCOVERNCLAIM1: row.NCLAIM,
                CLCOVERNCASENUM2: row.nCase_num,
                CLCOVERNDEMANTYPE3: row.nDeman_type,
                CLCOVERNCLAIM4: row.NCLAIM,
                CLCOVERNCASENUM5: row.nCase_num,
                CLCOVERNDEMANTYPE6: row.nDeman_type }),
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
    this.Claim_attm_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5ConsultaSiniestrosActions.aspx/Claim_attmSelectCommandActionCLAIM_ATTM", false,
            JSON.stringify({                 CLAIMATTMNCLAIM1: row.NCLAIM,
                CLAIMATTMNCASENUM2: row.nCase_num,
                CLAIMATTMNDEMANTYPE3: row.nDeman_type }),
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
    this.Claim_auto_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5ConsultaSiniestrosActions.aspx/Claim_autoSelectCommandActionCLAIM_AUTO", false,
            JSON.stringify({                 CLAIMAUTONCLAIM1: row.NCLAIM,
                CLAIMAUTONCASENUM2: row.nCase_num,
                CLAIMAUTONDEMANTYPE3: row.nDeman_type }),
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
    this.CLAIM_DAMA_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5ConsultaSiniestrosActions.aspx/CLAIM_DAMASelectCommandActionCLAIM_DAMA", false,
            JSON.stringify({                 CLAIMDAMANCLAIM1: row.NCLAIM,
                CLAIMDAMANCASENUM2: row.nCase_num,
                CLAIMDAMANDEMANTYPE3: row.nDeman_type }),
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
    this.CLAIM_THIR_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5ConsultaSiniestrosActions.aspx/CLAIM_THIRSelectCommandActionCLAIM_THIR", false,
            JSON.stringify({                 CLAIMTHIRNCLAIM1: row.NCLAIM,
                CLAIMTHIRNCASENUM2: row.nCase_num,
                CLAIMTHIRNDEMANTYPE3: row.nDeman_type }),
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
    this.Life_claim_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5ConsultaSiniestrosActions.aspx/Life_claimSelectCommandActionLIFE_CLAIM", false,
            JSON.stringify({                 LIFECLAIMNCLAIM1: row.NCLAIM,
                LIFECLAIMNCASENUM2: row.nCase_num,
                LIFECLAIMNDEMANTYPE3: row.nDeman_type }),
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
    this.CLAIMBENEF_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5ConsultaSiniestrosActions.aspx/CLAIMBENEFSelectCommandActionCLAIMBENEF", false,
            JSON.stringify({                 CLAIMBENEFNCLAIM1: row.NCLAIM,
                CLAIMBENEFNCASENUM2: row.nCase_num,
                CLAIMBENEFNDEMANTYPE3: row.nDeman_type }),
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
    this.Claim_his_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5ConsultaSiniestrosActions.aspx/Claim_hisSelectCommandActionCLAIM_HIS", false,
            JSON.stringify({                 CLAIMHISNCLAIM1: row.NCLAIM,
                CLAIMHISNCASENUM2: row.nCase_num,
                CLAIMHISNDEMANTYPE3: row.nDeman_type }),
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
    this.CHEQUES_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5ConsultaSiniestrosActions.aspx/CHEQUESSelectCommandActionCHEQUES", false,
            JSON.stringify({                 CHEQUESNCLAIM1: row.nClaim }),
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
    this.CLAIM_HIS2_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5ConsultaSiniestrosActions.aspx/CLAIM_HIS2SelectCommandActionCLAIM_HIS", false,
            JSON.stringify({                 CLAIMHISNCLAIM1: row.nClaim }),
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
            var formInstance = $("#H5ConsultaSiniestrosMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#btnOk'));
                btnLoading.start();
                H5ConsultaSiniestrosSupport.ItemsTblRequest();
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();
    
        $("#H5ConsultaSiniestrosMainForm").validate({
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
                Siniestro: {
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
                Siniestro: {
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

    this.ItemsTblSetup = function (table) {
        table.bootstrapTable({
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            detailView: true,
            onExpandRow: H5ConsultaSiniestrosSupport.ItemsTblExpandRow,
            columns: [{
                field: 'nClaim',
                title: 'Siniestro',
                formatter: 'H5ConsultaSiniestrosSupport.ClaimnClaim_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NBRANCH',
                title: 'Ramo',
                formatter: 'H5ConsultaSiniestrosSupport.ClaimNBRANCH_FormatterMaskData',
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
                formatter: 'H5ConsultaSiniestrosSupport.ClaimNPRODUCT_FormatterMaskData',
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
                field: 'nPolicy',
                title: 'Póliza',
                formatter: 'H5ConsultaSiniestrosSupport.ClaimnPolicy_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NCERTIF',
                title: 'Certificado',
                formatter: 'H5ConsultaSiniestrosSupport.ClaimNCERTIF_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'DDECLADAT',
                title: 'F.Declaración',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'SCLAIMTYP',
                title: 'Tipo de pérdida',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'SCLAIMTYPDesc',
                title: 'Tipo de pérdida',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NCAUSECOD',
                title: 'Causa del siniestro',
                formatter: 'H5ConsultaSiniestrosSupport.ClaimNCAUSECOD_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NCAUSECODDesc',
                title: 'Causa del siniestro',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SSTACLAIM',
                title: 'Estado',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'SSTACLAIMDesc',
                title: 'Estado',
                sortable: true,
                halign: 'center'
            }, {
                field: 'DPRESCDAT',
                title: 'F.Prescripción',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center',
                visible: false
            }, {
                field: 'DOCCURDAT',
                title: 'F.Ocurrencia',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center',
                visible: false
            }, {
                field: 'NLOC_RESERV',
                title: 'Reserva actual',
                formatter: 'H5ConsultaSiniestrosSupport.ClaimNLOC_RESERV_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NLOC_REC_AM',
                title: 'Monto recuperado',
                formatter: 'H5ConsultaSiniestrosSupport.ClaimNLOC_REC_AM_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NLOC_PAY_AM',
                title: 'Monto pagado',
                formatter: 'H5ConsultaSiniestrosSupport.ClaimNLOC_PAY_AM_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NLOC_OUT_AM',
                title: 'Reserva pendiente',
                formatter: 'H5ConsultaSiniestrosSupport.ClaimNLOC_OUT_AM_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NLOC_COS_RE',
                title: 'Gastos de recuperación',
                formatter: 'H5ConsultaSiniestrosSupport.ClaimNLOC_COS_RE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NIDCATAS',
                title: 'Evento catastrófico',
                formatter: 'H5ConsultaSiniestrosSupport.ClaimNIDCATAS_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }]
        });



    };


    this.ItemsRowToInput = function (row) {
        H5ConsultaSiniestrosSupport.currentRow = row;
        AutoNumeric.set('#ClaimnClaim', row.nClaim);
        AutoNumeric.set('#ClaimNBRANCH', row.NBRANCH);
        $('#ClaimNBRANCHDesc').val(row.NBRANCHDesc);
        AutoNumeric.set('#ClaimNPRODUCT', row.NPRODUCT);
        $('#ClaimNPRODUCTDesc').val(row.NPRODUCTDesc);
        AutoNumeric.set('#ClaimnPolicy', row.nPolicy);
        AutoNumeric.set('#ClaimNCERTIF', row.NCERTIF);
        $('#ClaimDDECLADAT').val(generalSupport.ToJavaScriptDateCustom(row.DDECLADAT, generalSupport.DateFormat()));
        $('#ClaimSCLAIMTYP').val(row.SCLAIMTYP);
        $('#ClaimSCLAIMTYPDesc').val(row.SCLAIMTYPDesc);
        AutoNumeric.set('#ClaimNCAUSECOD', row.NCAUSECOD);
        $('#ClaimNCAUSECODDesc').val(row.NCAUSECODDesc);
        $('#ClaimSSTACLAIM').val(row.SSTACLAIM);
        $('#ClaimSSTACLAIMDesc').val(row.SSTACLAIMDesc);
        $('#ClaimDPRESCDAT').val(generalSupport.ToJavaScriptDateCustom(row.DPRESCDAT, generalSupport.DateFormat()));
        $('#ClaimDOCCURDAT').val(generalSupport.ToJavaScriptDateCustom(row.DOCCURDAT, generalSupport.DateFormat()));
        AutoNumeric.set('#ClaimNLOC_RESERV', row.NLOC_RESERV);
        AutoNumeric.set('#ClaimNLOC_REC_AM', row.NLOC_REC_AM);
        AutoNumeric.set('#ClaimNLOC_PAY_AM', row.NLOC_PAY_AM);
        AutoNumeric.set('#ClaimNLOC_OUT_AM', row.NLOC_OUT_AM);
        AutoNumeric.set('#ClaimNLOC_COS_RE', row.NLOC_COS_RE);
        AutoNumeric.set('#ClaimNIDCATAS', row.NIDCATAS);

    };
    this.ItemsTblRequest = function (params) {
        app.core.AsyncWebMethod("/fasi/dli/queries/H5ConsultaSiniestrosActions.aspx/ItemsTblDataLoad", false,
            JSON.stringify({
                                SINIESTRONCLAIM1: generalSupport.NumericValue('#Siniestro', -9999999999, 9999999999)
            }),
            function (data) {
                    $('#ItemsTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);


            });
    };
    this.Cl_coverTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'nCase_num',
                title: 'Caso',
                formatter: 'H5ConsultaSiniestrosSupport.Cl_covernCase_num_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nModulec',
                title: 'Módulo',
                formatter: 'H5ConsultaSiniestrosSupport.Cl_covernModulec_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NCOVER',
                title: 'Cobertura',
                formatter: 'H5ConsultaSiniestrosSupport.Cl_coverNCOVER_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NCOVERDesc',
                title: 'Cobertura',
                sortable: true,
                halign: 'center'
            }, {
                field: 'sClient',
                title: 'Cliente',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'sClientDesc',
                title: 'Cliente',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nDeman_type',
                title: 'Tipo de reclamo',
                formatter: 'H5ConsultaSiniestrosSupport.Cl_covernDeman_type_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nDeman_typeDesc',
                title: 'Tipo de reclamo',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nDamProf',
                title: 'Estimado profesional',
                formatter: 'H5ConsultaSiniestrosSupport.Cl_covernDamProf_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'sReservstat',
                title: 'Estado de reserva',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'nReserve',
                title: 'Reserva',
                formatter: 'H5ConsultaSiniestrosSupport.Cl_covernReserve_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nRec_amount',
                title: 'Monto recuperado',
                formatter: 'H5ConsultaSiniestrosSupport.Cl_covernRec_amount_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nPay_amount',
                title: 'Monto pagado',
                formatter: 'H5ConsultaSiniestrosSupport.Cl_covernPay_amount_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nLoc_cos_re',
                title: 'Gastos de recuperación',
                formatter: 'H5ConsultaSiniestrosSupport.Cl_covernLoc_cos_re_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'sFrantype',
                title: 'Franquicia/Deducible',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'sFrantypeDesc',
                title: 'Franquicia/Deducible',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'nFra_amount',
                title: 'Monto Franquicia/Deducible',
                formatter: 'H5ConsultaSiniestrosSupport.Cl_covernFra_amount_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nCurrency',
                title: 'Moneda',
                formatter: 'H5ConsultaSiniestrosSupport.Cl_covernCurrency_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nCurrencyDesc',
                title: 'Moneda',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NBRANCH',
                title: 'Ramo Comercial',
                formatter: 'H5ConsultaSiniestrosSupport.Cl_coverNBRANCH_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NPRODUCT',
                title: 'Producto',
                formatter: 'H5ConsultaSiniestrosSupport.Cl_coverNPRODUCT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }]
        });

        H5ConsultaSiniestrosSupport.$el = table;
        H5ConsultaSiniestrosSupport.Cl_coverTblRequest();
      };

    this.Cl_coverRowToInput = function (row) {
        H5ConsultaSiniestrosSupport.currentRow = row;
        AutoNumeric.set('#Cl_covernCase_num', row.nCase_num);
        AutoNumeric.set('#Cl_covernModulec', row.nModulec);
        AutoNumeric.set('#Cl_coverNCOVER', row.NCOVER);
        $('#Cl_coverNCOVERDesc').val(row.NCOVERDesc);
        $('#Cl_coversClient').val(row.sClient);
        $('#Cl_coversClientDesc').val(row.sClientDesc);
        AutoNumeric.set('#Cl_covernDeman_type', row.nDeman_type);
        $('#Cl_covernDeman_typeDesc').val(row.nDeman_typeDesc);
        AutoNumeric.set('#Cl_covernDamProf', row.nDamProf);
        $('#Cl_coversReservstat').val(row.sReservstat);
        AutoNumeric.set('#Cl_covernReserve', row.nReserve);
        AutoNumeric.set('#Cl_covernRec_amount', row.nRec_amount);
        AutoNumeric.set('#Cl_covernPay_amount', row.nPay_amount);
        AutoNumeric.set('#Cl_covernLoc_cos_re', row.nLoc_cos_re);
        $('#Cl_coversFrantype').val(row.sFrantype);
        $('#Cl_coversFrantypeDesc').val(row.sFrantypeDesc);
        AutoNumeric.set('#Cl_covernFra_amount', row.nFra_amount);
        AutoNumeric.set('#Cl_covernCurrency', row.nCurrency);
        $('#Cl_covernCurrencyDesc').val(row.nCurrencyDesc);
        AutoNumeric.set('#Cl_coverNBRANCH', row.NBRANCH);
        AutoNumeric.set('#Cl_coverNPRODUCT', row.NPRODUCT);

    };
    this.Cl_coverTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5ConsultaSiniestrosActions.aspx/Cl_coverTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                CLCOVERNCLAIM1: row.NCLAIM,
                CLCOVERNCASENUM2: row.nCase_num,
                CLCOVERNDEMANTYPE3: row.nDeman_type,
                CLCOVERNCLAIM4: row.NCLAIM,
                CLCOVERNCASENUM5: row.nCase_num,
                CLCOVERNDEMANTYPE6: row.nDeman_type
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
    this.Claim_attmTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'nCase_num',
                title: 'Caso',
                formatter: 'H5ConsultaSiniestrosSupport.Claim_attmnCase_num_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nDeman_type',
                title: 'Tipo de reclamo',
                formatter: 'H5ConsultaSiniestrosSupport.Claim_attmnDeman_type_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nDeman_typeDesc',
                title: 'Tipo de reclamo',
                sortable: true,
                halign: 'center'
            }, {
                field: 'sClient',
                title: 'Cliente',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'sClientDesc',
                title: 'Cliente',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nService',
                title: 'Orden de servicio',
                formatter: 'H5ConsultaSiniestrosSupport.Claim_attmnService_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'sIllness',
                title: 'Enfermedad',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'sIllnessDesc',
                title: 'Enfermedad',
                sortable: true,
                halign: 'center'
            }, {
                field: 'sClientProf',
                title: 'Médico',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'sClientProfDesc',
                title: 'Médico',
                sortable: true,
                halign: 'center'
            }]
        });

        H5ConsultaSiniestrosSupport.$el = table;
        H5ConsultaSiniestrosSupport.Claim_attmTblRequest();
      };

    this.Claim_attmRowToInput = function (row) {
        H5ConsultaSiniestrosSupport.currentRow = row;
        AutoNumeric.set('#Claim_attmnCase_num', row.nCase_num);
        AutoNumeric.set('#Claim_attmnDeman_type', row.nDeman_type);
        $('#Claim_attmnDeman_typeDesc').val(row.nDeman_typeDesc);
        $('#Claim_attmsClient').val(row.sClient);
        $('#Claim_attmsClientDesc').val(row.sClientDesc);
        AutoNumeric.set('#Claim_attmnService', row.nService);
        $('#Claim_attmsIllness').val(row.sIllness);
        $('#Claim_attmsIllnessDesc').val(row.sIllnessDesc);
        $('#Claim_attmsClientProf').val(row.sClientProf);
        $('#Claim_attmsClientProfDesc').val(row.sClientProfDesc);

    };
    this.Claim_attmTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5ConsultaSiniestrosActions.aspx/Claim_attmTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                CLAIMATTMNCLAIM1: row.NCLAIM,
                CLAIMATTMNCASENUM2: row.nCase_num,
                CLAIMATTMNDEMANTYPE3: row.nDeman_type
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
    this.Claim_autoTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'nCase_num',
                title: 'Caso',
                formatter: 'H5ConsultaSiniestrosSupport.Claim_autonCase_num_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nDeman_type',
                title: 'Tipo de reclamo',
                formatter: 'H5ConsultaSiniestrosSupport.Claim_autonDeman_type_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nDeman_typeDesc',
                title: 'Tipo de reclamo',
                sortable: true,
                halign: 'center'
            }, {
                field: 'sAlcoholic',
                title: 'Exceso de alcohol',
                formatter: 'tableHelperSupport.IsCheck',
                sortable: true,
                halign: 'center'
            }, {
                field: 'sPoliceDem',
                title: 'Denuncia policial',
                formatter: 'tableHelperSupport.IsCheck',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nInfraction',
                title: 'Indicador de infracción',
                formatter: 'H5ConsultaSiniestrosSupport.Claim_autonInfraction_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nAuto_quant',
                title: 'Nro.Vehículos envueltos',
                formatter: 'H5ConsultaSiniestrosSupport.Claim_autonAuto_quant_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nNotenum',
                title: 'Nro.Nota',
                formatter: 'H5ConsultaSiniestrosSupport.Claim_autonNotenum_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }]
        });

        H5ConsultaSiniestrosSupport.$el = table;
        H5ConsultaSiniestrosSupport.Claim_autoTblRequest();
      };

    this.Claim_autoRowToInput = function (row) {
        H5ConsultaSiniestrosSupport.currentRow = row;
        AutoNumeric.set('#Claim_autonCase_num', row.nCase_num);
        AutoNumeric.set('#Claim_autonDeman_type', row.nDeman_type);
        $('#Claim_autonDeman_typeDesc').val(row.nDeman_typeDesc);
        $('#Claim_autosAlcoholic').prop("checked", row.sAlcoholic);
        $('#Claim_autosPoliceDem').prop("checked", row.sPoliceDem);
        AutoNumeric.set('#Claim_autonInfraction', row.nInfraction);
        AutoNumeric.set('#Claim_autonAuto_quant', row.nAuto_quant);
        AutoNumeric.set('#Claim_autonNotenum', row.nNotenum);

    };
    this.Claim_autoTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5ConsultaSiniestrosActions.aspx/Claim_autoTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                CLAIMAUTONCLAIM1: row.NCLAIM,
                CLAIMAUTONCASENUM2: row.nCase_num,
                CLAIMAUTONDEMANTYPE3: row.nDeman_type
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
    this.CLAIM_DAMATblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'NDAMAGE_COD',
                title: 'Repuesto',
                formatter: 'H5ConsultaSiniestrosSupport.CLAIM_DAMANDAMAGE_COD_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NDAMAGE_CODDesc',
                title: 'Repuesto',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NMAG_DAM',
                title: 'Magnitud del daño',
                formatter: 'H5ConsultaSiniestrosSupport.CLAIM_DAMANMAG_DAM_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NMAG_DAMDesc',
                title: 'Magnitud del daño',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NAMOUNT',
                title: 'Monto aproximado',
                formatter: 'H5ConsultaSiniestrosSupport.CLAIM_DAMANAMOUNT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }]
        });

        H5ConsultaSiniestrosSupport.$el = table;
        H5ConsultaSiniestrosSupport.CLAIM_DAMATblRequest();
      };

    this.CLAIM_DAMARowToInput = function (row) {
        H5ConsultaSiniestrosSupport.currentRow = row;
        AutoNumeric.set('#CLAIM_DAMANDAMAGE_COD', row.NDAMAGE_COD);
        $('#CLAIM_DAMANDAMAGE_CODDesc').val(row.NDAMAGE_CODDesc);
        AutoNumeric.set('#CLAIM_DAMANMAG_DAM', row.NMAG_DAM);
        $('#CLAIM_DAMANMAG_DAMDesc').val(row.NMAG_DAMDesc);
        AutoNumeric.set('#CLAIM_DAMANAMOUNT', row.NAMOUNT);

    };
    this.CLAIM_DAMATblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5ConsultaSiniestrosActions.aspx/CLAIM_DAMATblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                CLAIMDAMANCLAIM1: row.NCLAIM,
                CLAIMDAMANCASENUM2: row.nCase_num,
                CLAIMDAMANDEMANTYPE3: row.nDeman_type
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
    this.CLAIM_THIRTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'SLICENSE_TY',
                title: 'Tipo de matrícula',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'SLICENSE_TYDesc',
                title: 'Tipo de matrícula',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SREGIST',
                title: 'Licencia',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SCHASSIS',
                title: 'Chasis tercero',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SMOTOR',
                title: 'Serial del motor',
                sortable: true,
                halign: 'center'
            }, {
                field: 'STHIR_POLIC',
                title: 'Póliza en asegurador del tercero',
                sortable: true,
                halign: 'center'
            }, {
                field: 'STHIR_CLAIM',
                title: 'Siniestro en asegurador del tercero',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SRECOV_IND',
                title: 'Posibilidad de recobro',
                formatter: 'tableHelperSupport.IsCheck',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NTHIR_COMP',
                title: 'Asegurador del tercero',
                formatter: 'H5ConsultaSiniestrosSupport.CLAIM_THIRNTHIR_COMP_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NBLAME',
                title: 'Culpabilidad',
                formatter: 'H5ConsultaSiniestrosSupport.CLAIM_THIRNBLAME_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NBLAMEDesc',
                title: 'Culpabilidad',
                sortable: true,
                halign: 'center'
            }]
        });

        H5ConsultaSiniestrosSupport.$el = table;
        H5ConsultaSiniestrosSupport.CLAIM_THIRTblRequest();
      };

    this.CLAIM_THIRRowToInput = function (row) {
        H5ConsultaSiniestrosSupport.currentRow = row;
        $('#CLAIM_THIRSLICENSE_TY').val(row.SLICENSE_TY);
        $('#CLAIM_THIRSLICENSE_TYDesc').val(row.SLICENSE_TYDesc);
        $('#CLAIM_THIRSREGIST').val(row.SREGIST);
        $('#CLAIM_THIRSCHASSIS').val(row.SCHASSIS);
        $('#CLAIM_THIRSMOTOR').val(row.SMOTOR);
        $('#CLAIM_THIRSTHIR_POLIC').val(row.STHIR_POLIC);
        $('#CLAIM_THIRSTHIR_CLAIM').val(row.STHIR_CLAIM);
        $('#CLAIM_THIRSRECOV_IND').prop("checked", row.SRECOV_IND);
        AutoNumeric.set('#CLAIM_THIRNTHIR_COMP', row.NTHIR_COMP);
        AutoNumeric.set('#CLAIM_THIRNBLAME', row.NBLAME);
        $('#CLAIM_THIRNBLAMEDesc').val(row.NBLAMEDesc);

    };
    this.CLAIM_THIRTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5ConsultaSiniestrosActions.aspx/CLAIM_THIRTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                CLAIMTHIRNCLAIM1: row.NCLAIM,
                CLAIMTHIRNCASENUM2: row.nCase_num,
                CLAIMTHIRNDEMANTYPE3: row.nDeman_type
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
    this.Life_claimTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'nCase_num',
                title: 'Caso',
                formatter: 'H5ConsultaSiniestrosSupport.Life_claimnCase_num_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nDeman_type',
                title: 'Tipo de reclamo',
                formatter: 'H5ConsultaSiniestrosSupport.Life_claimnDeman_type_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nDeman_typeDesc',
                title: 'Tipo de reclamo',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nIn_lif_typ',
                title: 'Tipo de indemnización',
                formatter: 'H5ConsultaSiniestrosSupport.Life_claimnIn_lif_typ_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nIn_lif_typDesc',
                title: 'Tipo de indemnización',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nCla_li_typ',
                title: 'Tipo de siniestro',
                formatter: 'H5ConsultaSiniestrosSupport.Life_claimnCla_li_typ_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nCla_li_typDesc',
                title: 'Tipo de siniestro',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nMonth_amou',
                title: 'Pago mensual',
                formatter: 'H5ConsultaSiniestrosSupport.Life_claimnMonth_amou_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'dEnd_date',
                title: 'F.Fin de pagos',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }]
        });

        H5ConsultaSiniestrosSupport.$el = table;
        H5ConsultaSiniestrosSupport.Life_claimTblRequest();
      };

    this.Life_claimRowToInput = function (row) {
        H5ConsultaSiniestrosSupport.currentRow = row;
        AutoNumeric.set('#Life_claimnCase_num', row.nCase_num);
        AutoNumeric.set('#Life_claimnDeman_type', row.nDeman_type);
        $('#Life_claimnDeman_typeDesc').val(row.nDeman_typeDesc);
        AutoNumeric.set('#Life_claimnIn_lif_typ', row.nIn_lif_typ);
        $('#Life_claimnIn_lif_typDesc').val(row.nIn_lif_typDesc);
        AutoNumeric.set('#Life_claimnCla_li_typ', row.nCla_li_typ);
        $('#Life_claimnCla_li_typDesc').val(row.nCla_li_typDesc);
        AutoNumeric.set('#Life_claimnMonth_amou', row.nMonth_amou);
        $('#Life_claimdEnd_date').val(generalSupport.ToJavaScriptDateCustom(row.dEnd_date, generalSupport.DateFormat()));

    };
    this.Life_claimTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5ConsultaSiniestrosActions.aspx/Life_claimTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                LIFECLAIMNCLAIM1: row.NCLAIM,
                LIFECLAIMNCASENUM2: row.nCase_num,
                LIFECLAIMNDEMANTYPE3: row.nDeman_type
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
    this.CLAIMBENEFTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'NBENE_TYPE',
                title: 'Tipo',
                formatter: 'H5ConsultaSiniestrosSupport.CLAIMBENEFNBENE_TYPE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NBENE_TYPEDesc',
                title: 'Tipo',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SCLIENT',
                title: 'Beneficiario',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'SCLIENTDesc',
                title: 'Beneficiario',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NRELATION',
                title: 'Nexo',
                formatter: 'H5ConsultaSiniestrosSupport.CLAIMBENEFNRELATION_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NRELATIONDesc',
                title: 'Nexo',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NPARTICIP',
                title: '%Participación',
                formatter: 'H5ConsultaSiniestrosSupport.CLAIMBENEFNPARTICIP_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'SDEMANDANT',
                title: 'Reclamante',
                formatter: 'tableHelperSupport.IsCheck',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NOFFICE_PAY',
                title: 'Enviar cheque a sucursal',
                formatter: 'H5ConsultaSiniestrosSupport.CLAIMBENEFNOFFICE_PAY_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NOFFICE_PAYDesc',
                title: 'Enviar cheque a sucursal',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SCLIENT_REP',
                title: 'Representante legal',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'SCLIENT_REPDesc',
                title: 'Representante legal',
                sortable: true,
                halign: 'center'
            }]
        });

        H5ConsultaSiniestrosSupport.$el = table;
        H5ConsultaSiniestrosSupport.CLAIMBENEFTblRequest();
      };

    this.CLAIMBENEFRowToInput = function (row) {
        H5ConsultaSiniestrosSupport.currentRow = row;
        AutoNumeric.set('#CLAIMBENEFNBENE_TYPE', row.NBENE_TYPE);
        $('#CLAIMBENEFNBENE_TYPEDesc').val(row.NBENE_TYPEDesc);
        $('#CLAIMBENEFSCLIENT').val(row.SCLIENT);
        $('#CLAIMBENEFSCLIENTDesc').val(row.SCLIENTDesc);
        AutoNumeric.set('#CLAIMBENEFNRELATION', row.NRELATION);
        $('#CLAIMBENEFNRELATIONDesc').val(row.NRELATIONDesc);
        AutoNumeric.set('#CLAIMBENEFNPARTICIP', row.NPARTICIP);
        $('#CLAIMBENEFSDEMANDANT').prop("checked", row.SDEMANDANT);
        AutoNumeric.set('#CLAIMBENEFNOFFICE_PAY', row.NOFFICE_PAY);
        $('#CLAIMBENEFNOFFICE_PAYDesc').val(row.NOFFICE_PAYDesc);
        $('#CLAIMBENEFSCLIENT_REP').val(row.SCLIENT_REP);
        $('#CLAIMBENEFSCLIENT_REPDesc').val(row.SCLIENT_REPDesc);

    };
    this.CLAIMBENEFTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5ConsultaSiniestrosActions.aspx/CLAIMBENEFTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                CLAIMBENEFNCLAIM1: row.NCLAIM,
                CLAIMBENEFNCASENUM2: row.nCase_num,
                CLAIMBENEFNDEMANTYPE3: row.nDeman_type
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
    this.Claim_hisTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'nCase_num',
                title: 'Caso',
                formatter: 'H5ConsultaSiniestrosSupport.Claim_hisnCase_num_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'dOperdate',
                title: 'Fecha',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'nTransac',
                title: 'Movimiento',
                formatter: 'H5ConsultaSiniestrosSupport.Claim_hisnTransac_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'sClient',
                title: 'Cliente',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'sClientDesc',
                title: 'Cliente',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nOper_type',
                title: 'Operación',
                formatter: 'H5ConsultaSiniestrosSupport.Claim_hisnOper_type_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nOper_typeDesc',
                title: 'Operación',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nPay_type',
                title: 'Tipo de pago',
                formatter: 'H5ConsultaSiniestrosSupport.Claim_hisnPay_type_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nPay_typeDesc',
                title: 'Tipo de pago',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nPay_form',
                title: 'Forma de pago',
                formatter: 'H5ConsultaSiniestrosSupport.Claim_hisnPay_form_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nPay_formDesc',
                title: 'Forma de pago',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nServ_order',
                title: 'Orden de servicio',
                formatter: 'H5ConsultaSiniestrosSupport.Claim_hisnServ_order_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'sInd_order',
                title: 'Orden de pago',
                formatter: 'tableHelperSupport.IsCheck',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'sOrder_num',
                title: 'Orden de Pago/Cheque',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'sExecuted',
                title: 'Pago realizado',
                formatter: 'tableHelperSupport.IsCheck',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'nAmount',
                title: 'Monto',
                formatter: 'H5ConsultaSiniestrosSupport.Claim_hisnAmount_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nCurrency',
                title: 'Moneda',
                formatter: 'H5ConsultaSiniestrosSupport.Claim_hisnCurrency_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nCurrencyDesc',
                title: 'Moneda',
                sortable: true,
                halign: 'center',
                visible: false
            }]
        });

        H5ConsultaSiniestrosSupport.$el = table;
        H5ConsultaSiniestrosSupport.Claim_hisTblRequest();
      };

    this.Claim_hisRowToInput = function (row) {
        H5ConsultaSiniestrosSupport.currentRow = row;
        AutoNumeric.set('#Claim_hisnCase_num', row.nCase_num);
        $('#Claim_hisdOperdate').val(generalSupport.ToJavaScriptDateCustom(row.dOperdate, generalSupport.DateFormat()));
        AutoNumeric.set('#Claim_hisnTransac', row.nTransac);
        $('#Claim_hissClient').val(row.sClient);
        $('#Claim_hissClientDesc').val(row.sClientDesc);
        AutoNumeric.set('#Claim_hisnOper_type', row.nOper_type);
        $('#Claim_hisnOper_typeDesc').val(row.nOper_typeDesc);
        AutoNumeric.set('#Claim_hisnPay_type', row.nPay_type);
        $('#Claim_hisnPay_typeDesc').val(row.nPay_typeDesc);
        AutoNumeric.set('#Claim_hisnPay_form', row.nPay_form);
        $('#Claim_hisnPay_formDesc').val(row.nPay_formDesc);
        AutoNumeric.set('#Claim_hisnServ_order', row.nServ_order);
        $('#Claim_hissInd_order').prop("checked", row.sInd_order);
        $('#Claim_hissOrder_num').val(row.sOrder_num);
        $('#Claim_hissExecuted').prop("checked", row.sExecuted);
        AutoNumeric.set('#Claim_hisnAmount', row.nAmount);
        AutoNumeric.set('#Claim_hisnCurrency', row.nCurrency);
        $('#Claim_hisnCurrencyDesc').val(row.nCurrencyDesc);

    };
    this.Claim_hisTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5ConsultaSiniestrosActions.aspx/Claim_hisTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                CLAIMHISNCLAIM1: row.NCLAIM,
                CLAIMHISNCASENUM2: row.nCase_num,
                CLAIMHISNDEMANTYPE3: row.nDeman_type
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
    this.Claim_caseTblExpandRow = function (index, row, $detail) {
       var tblparentid = $detail.parent().parent().parent()[0].id;
       var html = [];

        var detailShow = H5ConsultaSiniestrosSupport.Cl_cover_ShowValidation(row);
        if (detailShow)
        html.push('<table id="Cl_coverTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Coberturas afectadas</caption></table>');
        html.push('<table id="Claim_attmTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Atención Médica</caption></table>');
        html.push('<table id="Claim_autoTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Auto</caption></table>');
        html.push('<table id="CLAIM_DAMATbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Daños de Auto</caption></table>');
        html.push('<table id="CLAIM_THIRTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Terceros de Autos</caption></table>');
        html.push('<table id="Life_claimTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Vida</caption></table>');
        html.push('<table id="CLAIMBENEFTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Beneficiarios</caption></table>');
        html.push('<table id="Claim_hisTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Historia del Siniestro</caption></table>');

        $detail.html(html.join(""));
        if (detailShow)
        H5ConsultaSiniestrosSupport.Cl_coverTblSetup($detail.find('#Cl_coverTbl-' + index));
        H5ConsultaSiniestrosSupport.Claim_attmTblSetup($detail.find('#Claim_attmTbl-' + index));
        H5ConsultaSiniestrosSupport.Claim_autoTblSetup($detail.find('#Claim_autoTbl-' + index));
        H5ConsultaSiniestrosSupport.CLAIM_DAMATblSetup($detail.find('#CLAIM_DAMATbl-' + index));
        H5ConsultaSiniestrosSupport.CLAIM_THIRTblSetup($detail.find('#CLAIM_THIRTbl-' + index));
        H5ConsultaSiniestrosSupport.Life_claimTblSetup($detail.find('#Life_claimTbl-' + index));
        H5ConsultaSiniestrosSupport.CLAIMBENEFTblSetup($detail.find('#CLAIMBENEFTbl-' + index));
        H5ConsultaSiniestrosSupport.Claim_hisTblSetup($detail.find('#Claim_hisTbl-' + index));

    };
    this.Claim_caseTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            detailView: true,
            onExpandRow: H5ConsultaSiniestrosSupport.Claim_caseTblExpandRow,

            columns: [{
                field: 'nCase_num',
                title: 'Número',
                formatter: 'H5ConsultaSiniestrosSupport.Claim_casenCase_num_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nDeman_type',
                title: 'Tipo de Reclamo',
                formatter: 'H5ConsultaSiniestrosSupport.Claim_casenDeman_type_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nDeman_typeDesc',
                title: 'Tipo de Reclamo',
                sortable: true,
                halign: 'center'
            }, {
                field: 'sStaCase',
                title: 'Estado',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'sStaCaseDesc',
                title: 'Estado',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nNoteDama',
                title: 'Nro.Nota',
                formatter: 'H5ConsultaSiniestrosSupport.Claim_casenNoteDama_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NCLAIM',
                title: 'Número del siniestro.',
                formatter: 'H5ConsultaSiniestrosSupport.Claim_caseNCLAIM_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NCLAIM',
                title: 'Número del siniestro.',
                formatter: 'H5ConsultaSiniestrosSupport.Claim_caseNCLAIM_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }]
        });

        H5ConsultaSiniestrosSupport.$el = table;
        H5ConsultaSiniestrosSupport.Claim_caseTblRequest();
      };

    this.Claim_caseRowToInput = function (row) {
        H5ConsultaSiniestrosSupport.currentRow = row;
        AutoNumeric.set('#Claim_casenCase_num', row.nCase_num);
        AutoNumeric.set('#Claim_casenDeman_type', row.nDeman_type);
        $('#Claim_casenDeman_typeDesc').val(row.nDeman_typeDesc);
        $('#Claim_casesStaCase').val(row.sStaCase);
        $('#Claim_casesStaCaseDesc').val(row.sStaCaseDesc);
        AutoNumeric.set('#Claim_casenNoteDama', row.nNoteDama);
        AutoNumeric.set('#Claim_caseNCLAIM', row.NCLAIM);
        AutoNumeric.set('#Claim_caseNCLAIM', row.NCLAIM);

    };
    this.Claim_caseTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5ConsultaSiniestrosActions.aspx/Claim_caseTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                CLAIMCASENCLAIM1: row.nClaim
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
    this.CHEQUESTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'SREQUEST_TY',
                title: 'Tipo',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'SREQUEST_TYDesc',
                title: 'Tipo',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NREQUEST_NU',
                title: 'Orden de pago',
                formatter: 'H5ConsultaSiniestrosSupport.CHEQUESNREQUEST_NU_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'SCHEQUE',
                title: 'Cheque',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NCONCEPT',
                title: 'Concepto',
                formatter: 'H5ConsultaSiniestrosSupport.CHEQUESNCONCEPT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NCONCEPTDesc',
                title: 'Concepto',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NAMOUNT',
                title: 'Monto',
                formatter: 'H5ConsultaSiniestrosSupport.CHEQUESNAMOUNT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NCURRENCYPAY',
                title: 'Moneda',
                formatter: 'H5ConsultaSiniestrosSupport.CHEQUESNCURRENCYPAY_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NSTA_CHEQUE',
                title: 'Estado',
                formatter: 'H5ConsultaSiniestrosSupport.CHEQUESNSTA_CHEQUE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NSTA_CHEQUEDesc',
                title: 'Estado',
                sortable: true,
                halign: 'center'
            }, {
                field: 'DSTAT_DATE',
                title: 'F.Estado',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'NOFFICEAGEN',
                title: 'Oficina de entrega',
                formatter: 'H5ConsultaSiniestrosSupport.CHEQUESNOFFICEAGEN_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NOFFICEAGENDesc',
                title: 'Oficina de entrega',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NAGENCY',
                title: 'Agencia de entrega',
                formatter: 'H5ConsultaSiniestrosSupport.CHEQUESNAGENCY_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NTYPESUPPORT',
                title: 'Documento de soporte',
                formatter: 'H5ConsultaSiniestrosSupport.CHEQUESNTYPESUPPORT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NTYPESUPPORTDesc',
                title: 'Documento de soporte',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'NDOCSUPPORT',
                title: 'Número de documento',
                formatter: 'H5ConsultaSiniestrosSupport.CHEQUESNDOCSUPPORT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'SINTER_PAY',
                title: 'Beneficiario del pago',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'SINTER_PAYDesc',
                title: 'Beneficiario del pago',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'NUSER_SOL',
                title: 'Solicitante',
                formatter: 'H5ConsultaSiniestrosSupport.CHEQUESNUSER_SOL_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NUSER_SOLDesc',
                title: 'Solicitante',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'NQ_PAYS',
                title: 'Cantidad de pagos',
                formatter: 'H5ConsultaSiniestrosSupport.CHEQUESNQ_PAYS_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'DNULLDATE',
                title: 'F.Anulación',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center',
                visible: false
            }, {
                field: 'DISSUE_DAT',
                title: 'F.Emisión',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center',
                visible: false
            }, {
                field: 'SDESCRIPT',
                title: 'Razón de Pago',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'DDAT_PROPOS',
                title: 'F.Solicitud del pago',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center',
                visible: false
            }]
        });

        H5ConsultaSiniestrosSupport.$el = table;
        H5ConsultaSiniestrosSupport.CHEQUESTblRequest();
      };

    this.CHEQUESRowToInput = function (row) {
        H5ConsultaSiniestrosSupport.currentRow = row;
        $('#CHEQUESSREQUEST_TY').val(row.SREQUEST_TY);
        $('#CHEQUESSREQUEST_TYDesc').val(row.SREQUEST_TYDesc);
        AutoNumeric.set('#CHEQUESNREQUEST_NU', row.NREQUEST_NU);
        $('#CHEQUESSCHEQUE').val(row.SCHEQUE);
        AutoNumeric.set('#CHEQUESNCONCEPT', row.NCONCEPT);
        $('#CHEQUESNCONCEPTDesc').val(row.NCONCEPTDesc);
        AutoNumeric.set('#CHEQUESNAMOUNT', row.NAMOUNT);
        AutoNumeric.set('#CHEQUESNCURRENCYPAY', row.NCURRENCYPAY);
        AutoNumeric.set('#CHEQUESNSTA_CHEQUE', row.NSTA_CHEQUE);
        $('#CHEQUESNSTA_CHEQUEDesc').val(row.NSTA_CHEQUEDesc);
        $('#CHEQUESDSTAT_DATE').val(generalSupport.ToJavaScriptDateCustom(row.DSTAT_DATE, generalSupport.DateFormat()));
        AutoNumeric.set('#CHEQUESNOFFICEAGEN', row.NOFFICEAGEN);
        $('#CHEQUESNOFFICEAGENDesc').val(row.NOFFICEAGENDesc);
        AutoNumeric.set('#CHEQUESNAGENCY', row.NAGENCY);
        AutoNumeric.set('#CHEQUESNTYPESUPPORT', row.NTYPESUPPORT);
        $('#CHEQUESNTYPESUPPORTDesc').val(row.NTYPESUPPORTDesc);
        AutoNumeric.set('#CHEQUESNDOCSUPPORT', row.NDOCSUPPORT);
        $('#CHEQUESSINTER_PAY').val(row.SINTER_PAY);
        $('#CHEQUESSINTER_PAYDesc').val(row.SINTER_PAYDesc);
        AutoNumeric.set('#CHEQUESNUSER_SOL', row.NUSER_SOL);
        $('#CHEQUESNUSER_SOLDesc').val(row.NUSER_SOLDesc);
        AutoNumeric.set('#CHEQUESNQ_PAYS', row.NQ_PAYS);
        $('#CHEQUESDNULLDATE').val(generalSupport.ToJavaScriptDateCustom(row.DNULLDATE, generalSupport.DateFormat()));
        $('#CHEQUESDISSUE_DAT').val(generalSupport.ToJavaScriptDateCustom(row.DISSUE_DAT, generalSupport.DateFormat()));
        $('#CHEQUESSDESCRIPT').val(row.SDESCRIPT);
        $('#CHEQUESDDAT_PROPOS').val(generalSupport.ToJavaScriptDateCustom(row.DDAT_PROPOS, generalSupport.DateFormat()));

    };
    this.CHEQUESTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5ConsultaSiniestrosActions.aspx/CHEQUESTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                CHEQUESNCLAIM1: row.nClaim
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
    this.CLAIM_HIS2TblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'NID',
                title: 'Consecutivo',
                formatter: 'H5ConsultaSiniestrosSupport.CLAIM_HIS2NID_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NCASE_NUM',
                title: 'Caso',
                formatter: 'H5ConsultaSiniestrosSupport.CLAIM_HIS2NCASE_NUM_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NTRANSAC',
                title: 'Movimiento',
                formatter: 'H5ConsultaSiniestrosSupport.CLAIM_HIS2NTRANSAC_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NOPER_TYPE',
                title: 'Operación',
                formatter: 'H5ConsultaSiniestrosSupport.CLAIM_HIS2NOPER_TYPE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NOPER_TYPEDesc',
                title: 'Operación',
                sortable: true,
                halign: 'center'
            }, {
                field: 'DOPERDATE',
                title: 'F.Transacción',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'NTRANSCLAIMREV',
                title: 'Movimiento reversado',
                formatter: 'H5ConsultaSiniestrosSupport.CLAIM_HIS2NTRANSCLAIMREV_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }]
        });

        H5ConsultaSiniestrosSupport.$el = table;
        H5ConsultaSiniestrosSupport.CLAIM_HIS2TblRequest();
      };

    this.CLAIM_HIS2RowToInput = function (row) {
        H5ConsultaSiniestrosSupport.currentRow = row;
        AutoNumeric.set('#CLAIM_HIS2NID', row.NID);
        AutoNumeric.set('#CLAIM_HIS2NCASE_NUM', row.NCASE_NUM);
        AutoNumeric.set('#CLAIM_HIS2NTRANSAC', row.NTRANSAC);
        AutoNumeric.set('#CLAIM_HIS2NOPER_TYPE', row.NOPER_TYPE);
        $('#CLAIM_HIS2NOPER_TYPEDesc').val(row.NOPER_TYPEDesc);
        $('#CLAIM_HIS2DOPERDATE').val(generalSupport.ToJavaScriptDateCustom(row.DOPERDATE, generalSupport.DateFormat()));
        AutoNumeric.set('#CLAIM_HIS2NTRANSCLAIMREV', row.NTRANSCLAIMREV);

    };
    this.CLAIM_HIS2TblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5ConsultaSiniestrosActions.aspx/CLAIM_HIS2TblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                CLAIMHISNCLAIM1: row.nClaim
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

        var detailShow = H5ConsultaSiniestrosSupport.Claim_case_ShowValidation(row);
        if (detailShow)
        html.push('<table id="Claim_caseTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Casos</caption></table>');
        html.push('<table id="CHEQUESTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Cheques</caption></table>');
        html.push('<table id="CLAIM_HIS2Tbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Historia</caption></table>');

        $detail.html(html.join(""));
        if (detailShow)
        H5ConsultaSiniestrosSupport.Claim_caseTblSetup($detail.find('#Claim_caseTbl-' + index));
        H5ConsultaSiniestrosSupport.CHEQUESTblSetup($detail.find('#CHEQUESTbl-' + index));
        H5ConsultaSiniestrosSupport.CLAIM_HIS2TblSetup($detail.find('#CLAIM_HIS2Tbl-' + index));

    };


    this.ClaimnClaim_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.ClaimNBRANCH_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.ClaimNPRODUCT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.ClaimnPolicy_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.ClaimNCERTIF_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.ClaimNCAUSECOD_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.ClaimNLOC_RESERV_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.ClaimNLOC_REC_AM_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.ClaimNLOC_PAY_AM_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.ClaimNLOC_OUT_AM_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.ClaimNLOC_COS_RE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.ClaimNIDCATAS_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.Claim_casenCase_num_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: "",
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.Claim_casenDeman_type_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.Claim_casenNoteDama_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.Claim_caseNCLAIM_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9,
            decimalPlaces: 0,
            minimumValue: -9
        });
      };
    this.Claim_caseNCLAIM_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9,
            decimalPlaces: 0,
            minimumValue: -9
        });
      };
    this.Cl_covernCase_num_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.Cl_covernModulec_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.Cl_coverNCOVER_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.Cl_covernDeman_type_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.Cl_covernDamProf_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.Cl_covernReserve_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.Cl_covernRec_amount_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.Cl_covernPay_amount_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.Cl_covernLoc_cos_re_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.Cl_covernFra_amount_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.Cl_covernCurrency_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.Cl_coverNBRANCH_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.Cl_coverNPRODUCT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.Claim_attmnCase_num_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.Claim_attmnDeman_type_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.Claim_attmnService_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.Claim_autonCase_num_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.Claim_autonDeman_type_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.Claim_autonInfraction_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.Claim_autonAuto_quant_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.Claim_autonNotenum_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.CLAIM_DAMANDAMAGE_COD_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.CLAIM_DAMANMAG_DAM_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.CLAIM_DAMANAMOUNT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.CLAIM_THIRNTHIR_COMP_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.CLAIM_THIRNBLAME_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.Life_claimnCase_num_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.Life_claimnDeman_type_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.Life_claimnIn_lif_typ_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.Life_claimnCla_li_typ_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.Life_claimnMonth_amou_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.CLAIMBENEFNBENE_TYPE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.CLAIMBENEFNRELATION_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.CLAIMBENEFNPARTICIP_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 2,
            minimumValue: -99999
        });
      };
    this.CLAIMBENEFNOFFICE_PAY_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.Claim_hisnCase_num_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.Claim_hisnTransac_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.Claim_hisnOper_type_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.Claim_hisnPay_type_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.Claim_hisnPay_form_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.Claim_hisnServ_order_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.Claim_hisnAmount_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.Claim_hisnCurrency_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.CHEQUESNREQUEST_NU_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.CHEQUESNCONCEPT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.CHEQUESNAMOUNT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.CHEQUESNCURRENCYPAY_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.CHEQUESNSTA_CHEQUE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.CHEQUESNOFFICEAGEN_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.CHEQUESNAGENCY_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.CHEQUESNTYPESUPPORT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.CHEQUESNDOCSUPPORT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.CHEQUESNUSER_SOL_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.CHEQUESNQ_PAYS_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.CLAIM_HIS2NID_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.CLAIM_HIS2NCASE_NUM_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.CLAIM_HIS2NTRANSAC_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.CLAIM_HIS2NOPER_TYPE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.CLAIM_HIS2NTRANSCLAIMREV_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };


};

$(document).ready(function () {
    moment.locale(generalSupport.UserContext().languageName);
    
    if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage)
        masterSupport.setPageTitle('H5Información del siniestro');
        

    H5ConsultaSiniestrosSupport.ControlBehaviour();
    H5ConsultaSiniestrosSupport.ControlActions();
    H5ConsultaSiniestrosSupport.ValidateSetup();

    AutoNumeric.set('#Siniestro', generalSupport.URLNumericValue('Siniestro'));
    $('#RecordEffectiveDate').val(generalSupport.URLDateValue('RecordEffectiveDate'));

    $("#ItemsTblPlaceHolder").replaceWith('<table id="ItemsTbl"><caption >Siniestros</caption></table>');
    H5ConsultaSiniestrosSupport.ItemsTblSetup($('#ItemsTbl'));

    $('#RecordEffectiveDate').val(moment().format(generalSupport.DateFormat()));
        H5ConsultaSiniestrosSupport.ItemsTblRequest();



});

