var H5ConsultaPolizasClienteSegunClienteIndicadoSupport = new function () {

    this.currentRow = {};
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#H5ConsultaPolizasClienteSegunClienteIndicadoFormId').val(),
            Client: ($('#Client').data('code') !== undefined) ? $('#Client').data('code') : '',
            RecordEffectiveDate: generalSupport.DatePickerValueInputToObject('#RecordEffectiveDate')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        $('#H5ConsultaPolizasClienteSegunClienteIndicadoFormId').val(data.InstanceFormId);
        $('#Client').data('code', data.Client);
        clientSupport.CompleteClientName('#Client', data.Client);
        $('#RecordEffectiveDate').val(generalSupport.ToJavaScriptDateCustom(data.RecordEffectiveDate, generalSupport.DateFormat()));


        if (data.Curren_pol_Curren_pol !== null)
            $('#Curren_polTbl').bootstrapTable('load', data.Curren_pol_Curren_pol);
        if (data.Roles_Roles !== null)
            $('#RolesTbl').bootstrapTable('load', data.Roles_Roles);
        if (data.SUM_INSUR_SUM_INSUR !== null)
            $('#SUM_INSURTbl').bootstrapTable('load', data.SUM_INSUR_SUM_INSUR);
        if (data.Cover_Cover !== null)
            $('#CoverTbl').bootstrapTable('load', data.Cover_Cover);
        if (data.REINSURAN_REINSURAN !== null)
            $('#REINSURANTbl').bootstrapTable('load', data.REINSURAN_REINSURAN);
        if (data.REINSURAN2_REINSURAN2 !== null)
            $('#REINSURAN2Tbl').bootstrapTable('load', data.REINSURAN2_REINSURAN2);
        if (data.Disc_xprem_Disc_xprem !== null)
            $('#Disc_xpremTbl').bootstrapTable('load', data.Disc_xprem_Disc_xprem);
        if (data.Auto_Auto !== null)
            $('#AutoTbl').bootstrapTable('load', data.Auto_Auto);
        if (data.Fire_Fire !== null)
            $('#FireTbl').bootstrapTable('load', data.Fire_Fire);
        if (data.HomeOwner_HomeOwner !== null)
            $('#HomeOwnerTbl').bootstrapTable('load', data.HomeOwner_HomeOwner);
        if (data.Life_Life !== null)
            $('#LifeTbl').bootstrapTable('load', data.Life_Life);
        if (data.THEFT_THEFT !== null)
            $('#THEFTTbl').bootstrapTable('load', data.THEFT_THEFT);
        if (data.FINANCIAL_INSTRUMENTS_FINANCIAL_INSTRUMENTS !== null)
            $('#FINANCIAL_INSTRUMENTSTbl').bootstrapTable('load', data.FINANCIAL_INSTRUMENTS_FINANCIAL_INSTRUMENTS);
        if (data.ROLES2_ROLES2 !== null)
            $('#ROLES2Tbl').bootstrapTable('load', data.ROLES2_ROLES2);
        if (data.HEALTH_HEALTH !== null)
            $('#HEALTHTbl').bootstrapTable('load', data.HEALTH_HEALTH);
        if (data.Beneficiar_Beneficiar !== null)
            $('#BeneficiarTbl').bootstrapTable('load', data.Beneficiar_Beneficiar);
        if (data.Clause_Clause !== null)
            $('#ClauseTbl').bootstrapTable('load', data.Clause_Clause);
        if (data.POLICY_HIS_POLICY_HIS !== null)
            $('#POLICY_HISTbl').bootstrapTable('load', data.POLICY_HIS_POLICY_HIS);
        if (data.DIR_DEBIT_DIR_DEBIT !== null)
            $('#DIR_DEBITTbl').bootstrapTable('load', data.DIR_DEBIT_DIR_DEBIT);
        if (data.Certificat_Certificat !== null)
            $('#CertificatTbl').bootstrapTable('load', data.Certificat_Certificat);
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.ItemsTblRequest();
        if (data.Items_Item !== null)
            $('#ItemsTbl').bootstrapTable('load', data.Items_Item);

    };

    this.ControlBehaviour = function () {

       this.Items_ClientSCLIENT_Item1 = function (row) {
           window.open('/fasi/dli/forms/ChangeCreditCardsOfClientPopup.html?VieneDeConsulta=true&PCli='+ row.SCLIENT +'','_blank','scrollbars=no,resizable=no,toolbar=no,location=no,directories=no,status=yes,menubar=no,copyhistory=no,width=0,height=0,left=0,top=0');

            return true;
        };
       this.Items_ClientSCLIENT_Item2 = function (row) {
           window.open('/fasi/dli/forms/ChangePhoneOfClientPopup.html?VieneDeConsulta=true&PCli='+ row.SCLIENT +'','_blank','scrollbars=yes,resizable=no,toolbar=no,location=no,directories=no,status=yes,menubar=no,copyhistory=no,width=0,height=0,left=0,top=0');

            return true;
        };
       this.Items_ClientSCLIENT_Item3 = function (row) {
           window.open('/fasi/dli/forms/ChangeAddressOfClientPopup.html?VieneDeConsulta=true&PCli='+ row.SCLIENT +'','_blank','scrollbars=yes,resizable=no,toolbar=no,location=no,directories=no,status=yes,menubar=no,copyhistory=no,width=0,height=0,left=0,top=0');

            return true;
        };
       this.Certificat_CertificatnPolicy_Item1 = function (row) {
           window.open('/fasi/dli/forms/RequestOfPrintingPolicyPopup.html?LineOfBusinessToPrint='+ row.nBranch +'&ProductToPrint='+ row.nProduct +'&PolicyToPrint='+ row.nPolicy +'&CertificateToPrint='+ row.NCERTIF +'&PProcessDate='+ generalSupport.ToJavaScriptDateCustom(row.dChangdat, generalSupport.DateFormat())  +'','_blank','scrollbars=no,resizable=no,toolbar=no,location=no,directories=no,status=yes,menubar=no,copyhistory=no,width=0,height=0,left=0,top=0');

            return true;
        };
       this.Certificat_CertificatnPolicy_Item2 = function (row) {
           window.location.href = '/fasi/dli/forms/NNClaimDeclarationDemo.aspx?Policy='+ row.nPolicy +'';

            return true;
        };
       this.Fire_FirenConstCatDesc_Item1 = function (row) {
           window.location.href = '/fasi/dli/forms/ClaimDeclarationMortgage.aspx?PLineOfBusinessId='+ FireNBRANCH +'&PProductId='+ FireNPRODUCT +'&PPolicyId='+ FireNPOLICY +'&PCertificateId='+ FireNCERTIF +'';

            return true;
        };
       this.ROLES2_ROLES2NROLEDesc_Item1 = function (row) {
           generalSupport.CallBackOfficePage('', '&PLineOfBusinessId='+ ROLESNBRANCH +'&PProductId='+ ROLESNPRODUCT +'&PPolicyId='+ ROLESNPOLICY +'&PCertificateId='+ ROLESNCERTIF +'&PInsuredAffected=');

            return true;
        };
       this.DIR_DEBIT_DIR_DEBITNBANKEXTDesc_Item1 = function (row) {
           generalSupport.CallBackOfficePage('', '&VieneDeConsulta=true&PPol='+ row.NPOLICY +'');

            return true;
        };




        $("#Client").autocomplete({
            source: function (request, response) {
                clientSupport.AutoCompleteSource('#Client', request, response);
            },
            select: function (event, ui) {
                $('#Client').data('code', ui.item.code);
            }
        });


        $('#RecordEffectiveDate_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });


    };

    this.ActionProcess = function (data, source) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
                H5ConsultaPolizasClienteSegunClienteIndicadoSupport.ObjectToInput(data.d.Data, source);
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };



    this.Certificat_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5ConsultaPolizasClienteSegunClienteIndicadoActions.aspx/CertificatSelectCommandActionCERTIFICAT", false,
            JSON.stringify({                 CERTIFICATSCLIENT1: row.SCLIENT,
                CERTIFICATSCLIENT5: row.SCLIENT }),
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
    this.Curren_pol_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5ConsultaPolizasClienteSegunClienteIndicadoActions.aspx/Curren_polSelectCommandActionCURREN_POL", false,
            JSON.stringify({                 CURRENPOLSCERTYPE1: row.SCERTYPE,
                CURRENPOLNBRANCH2: row.nBranch,
                CURRENPOLNPRODUCT3: row.nProduct,
                CURRENPOLNPOLICY4: row.nPolicy,
                CURRENPOLNCERTIF5: row.NCERTIF }),
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
    this.Roles_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5ConsultaPolizasClienteSegunClienteIndicadoActions.aspx/RolesSelectCommandActionROLES", false,
            JSON.stringify({                 ROLESSCERTYPE1: row.SCERTYPE,
                ROLESNBRANCH2: row.nBranch,
                ROLESNPRODUCT3: row.nProduct,
                ROLESNPOLICY4: row.nPolicy,
                ROLESNCERTIF5: row.NCERTIF }),
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
    this.SUM_INSUR_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5ConsultaPolizasClienteSegunClienteIndicadoActions.aspx/SUM_INSURSelectCommandActionSUM_INSUR", false,
            JSON.stringify({                 SUMINSURSCERTYPE1: row.SCERTYPE,
                SUMINSURNBRANCH2: row.nBranch,
                SUMINSURNPRODUCT3: row.nProduct,
                SUMINSURNPOLICY4: row.nPolicy,
                SUMINSURNCERTIF5: row.NCERTIF }),
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
    this.Cover_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5ConsultaPolizasClienteSegunClienteIndicadoActions.aspx/CoverSelectCommandActionCOVER", false,
            JSON.stringify({                 COVERSCERTYPE1: row.SCERTYPE,
                COVERNBRANCH2: row.nBranch,
                COVERNPRODUCT3: row.nProduct,
                COVERNPOLICY4: row.nPolicy,
                COVERNCERTIF5: row.NCERTIF }),
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
    this.REINSURAN_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5ConsultaPolizasClienteSegunClienteIndicadoActions.aspx/REINSURANSelectCommandActionREINSURAN", false,
            JSON.stringify({                 REINSURANSCERTYPE3: row.SCERTYPE,
                REINSURANNBRANCH4: row.nBranch,
                REINSURANNPRODUCT5: row.nProduct,
                REINSURANNPOLICY6: row.nPolicy,
                REINSURANNCERTIF7: row.NCERTIF }),
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
    this.REINSURAN2_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5ConsultaPolizasClienteSegunClienteIndicadoActions.aspx/REINSURAN2SelectCommandActionREINSURAN", false,
            JSON.stringify({                 REINSURANSCERTYPE3: row.SCERTYPE,
                REINSURANNBRANCH4: row.nBranch,
                REINSURANNPRODUCT5: row.nProduct,
                REINSURANNPOLICY6: row.nPolicy,
                REINSURANNCERTIF7: row.NCERTIF }),
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
    this.Disc_xprem_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5ConsultaPolizasClienteSegunClienteIndicadoActions.aspx/Disc_xpremSelectCommandActionDISC_XPREM", false,
            JSON.stringify({                 DISCXPREMSCERTYPE1: row.SCERTYPE,
                DISCXPREMNBRANCH2: row.nBranch,
                DISCXPREMNPRODUCT3: row.nProduct,
                DISCXPREMNPOLICY4: row.nPolicy,
                DISCXPREMNCERTIF5: row.NCERTIF }),
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
    this.Auto_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5ConsultaPolizasClienteSegunClienteIndicadoActions.aspx/AutoSelectCommandActionAUTO", false,
            JSON.stringify({                 AUTOSCERTYPE1: row.SCERTYPE,
                AUTONBRANCH2: row.nBranch,
                AUTONPRODUCT3: row.nProduct,
                AUTONPOLICY4: row.nPolicy,
                AUTONCERTIF5: row.NCERTIF }),
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
    this.Fire_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5ConsultaPolizasClienteSegunClienteIndicadoActions.aspx/FireSelectCommandActionFIRE", false,
            JSON.stringify({                 FIRESCERTYPE1: row.SCERTYPE,
                FIRENBRANCH2: row.nBranch,
                FIRENPRODUCT3: row.nProduct,
                FIRENPOLICY4: row.nPolicy,
                FIRENCERTIF5: row.NCERTIF }),
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
    this.Fire_Item1_Actions = function (row, $modal) {
    window.location.href = '/fasi/dli/forms/ClaimDeclarationMortgage.aspx?PLineOfBusinessId='+ FireNBRANCH +'&PProductId='+ FireNPRODUCT +'&PPolicyId='+ FireNPOLICY +'&PCertificateId='+ FireNCERTIF +'';

    };
    this.HomeOwner_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5ConsultaPolizasClienteSegunClienteIndicadoActions.aspx/HomeOwnerSelectCommandActionHOMEOWNER", false,
            JSON.stringify({                 HOMEOWNERSCERTYPE1: row.SCERTYPE,
                HOMEOWNERNBRANCH2: row.nBranch,
                HOMEOWNERNPRODUCT3: row.nProduct,
                HOMEOWNERNPOLICY4: row.nPolicy,
                HOMEOWNERNCERTIF5: row.NCERTIF }),
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
    this.Life_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5ConsultaPolizasClienteSegunClienteIndicadoActions.aspx/LifeSelectCommandActionLIFE", false,
            JSON.stringify({                 LIFESCERTYPE1: row.SCERTYPE,
                LIFENBRANCH2: row.nBranch,
                LIFENPRODUCT3: row.nProduct,
                LIFENPOLICY4: row.nPolicy,
                LIFENCERTIF5: row.NCERTIF }),
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
    this.THEFT_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5ConsultaPolizasClienteSegunClienteIndicadoActions.aspx/THEFTSelectCommandActionTHEFT", false,
            JSON.stringify({                 THEFTSCERTYPE1: row.SCERTYPE,
                THEFTNBRANCH2: row.nBranch,
                THEFTNPRODUCT3: row.nProduct,
                THEFTNPOLICY4: row.nPolicy,
                THEFTNCERTIF5: row.NCERTIF }),
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
    this.FINANCIAL_INSTRUMENTS_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5ConsultaPolizasClienteSegunClienteIndicadoActions.aspx/FINANCIAL_INSTRUMENTSSelectCommandActionFINANCIAL_INSTRUMENTS", false,
            JSON.stringify({                 FINANCIALINSTRUMENTSSCERTYPE1: row.SCERTYPE,
                FINANCIALINSTRUMENTSNBRANCH2: row.nBranch,
                FINANCIALINSTRUMENTSNPRODUCT3: row.nProduct,
                FINANCIALINSTRUMENTSNPOLICY4: row.nPolicy,
                FINANCIALINSTRUMENTSNCERTIF5: row.NCERTIF }),
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
    this.HEALTH_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5ConsultaPolizasClienteSegunClienteIndicadoActions.aspx/HEALTHSelectCommandActionHEALTH", false,
            JSON.stringify({                 HEALTHSCERTYPE1: row.SCERTYPE,
                HEALTHNBRANCH2: row.nBranch,
                HEALTHNPRODUCT3: row.nProduct,
                HEALTHNPOLICY4: row.nPolicy,
                HEALTHNCERTIF5: row.NCERTIF }),
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
    this.ROLES2_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5ConsultaPolizasClienteSegunClienteIndicadoActions.aspx/ROLES2SelectCommandActionROLES", false,
            JSON.stringify({                 ROLESSCERTYPE3: row.SCERTYPE,
                ROLESNBRANCH4: row.NBRANCH,
                ROLESNPRODUCT5: row.NPRODUCT,
                ROLESNPOLICY6: row.NPOLICY,
                ROLESNCERTIF7: row.NCERTIF }),
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
    this.ROLES2_Item1_Actions = function (row, $modal) {
    generalSupport.CallBackOfficePage('', '&PLineOfBusinessId='+ ROLESNBRANCH +'&PProductId='+ ROLESNPRODUCT +'&PPolicyId='+ ROLESNPOLICY +'&PCertificateId='+ ROLESNCERTIF +'&PInsuredAffected=');

    };
    this.Beneficiar_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5ConsultaPolizasClienteSegunClienteIndicadoActions.aspx/BeneficiarSelectCommandActionBENEFICIAR", false,
            JSON.stringify({                 BENEFICIARSCERTYPE1: row.SCERTYPE,
                BENEFICIARNBRANCH2: row.nBranch,
                BENEFICIARNPRODUCT3: row.nProduct,
                BENEFICIARNPOLICY4: row.nPolicy,
                BENEFICIARNCERTIF5: row.NCERTIF }),
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
    this.Clause_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5ConsultaPolizasClienteSegunClienteIndicadoActions.aspx/ClauseSelectCommandActionCLAUSE", false,
            JSON.stringify({                 CLAUSESCERTYPE1: row.SCERTYPE,
                CLAUSENBRANCH2: row.nBranch,
                CLAUSENPRODUCT3: row.nProduct,
                CLAUSENPOLICY4: row.nPolicy,
                CLAUSENCERTIF5: row.NCERTIF }),
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
    this.POLICY_HIS_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5ConsultaPolizasClienteSegunClienteIndicadoActions.aspx/POLICY_HISSelectCommandActionPOLICY_HIS", false,
            JSON.stringify({                 POLICYHISSCERTYPE1: row.SCERTYPE,
                POLICYHISNBRANCH2: row.nBranch,
                POLICYHISNPRODUCT3: row.nProduct,
                POLICYHISNPOLICY4: row.nPolicy,
                POLICYHISNCERTIF5: row.NCERTIF }),
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
    this.DIR_DEBIT_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5ConsultaPolizasClienteSegunClienteIndicadoActions.aspx/DIR_DEBITSelectCommandActionDIR_DEBIT", false,
            JSON.stringify({                 DIRDEBITSCERTYPE2: row.SCERTYPE,
                DIRDEBITNBRANCH3: row.nBranch,
                DIRDEBITNPRODUCT4: row.nProduct,
                DIRDEBITNPOLICY5: row.nPolicy,
                DIRDEBITNCERTIF6: row.NCERTIF }),
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
    this.DIR_DEBIT_Item1_Actions = function (row, $modal) {
    generalSupport.CallBackOfficePage('', '&VieneDeConsulta=true&PPol='+ row.NPOLICY +'');

    };
    this.Certificat_Item1_Actions = function (row, $modal) {
    window.open('/fasi/dli/forms/RequestOfPrintingPolicyPopup.html?LineOfBusinessToPrint='+ row.nBranch +'&ProductToPrint='+ row.nProduct +'&PolicyToPrint='+ row.nPolicy +'&CertificateToPrint='+ row.NCERTIF +'&PProcessDate='+ generalSupport.ToJavaScriptDateCustom(row.dChangdat, generalSupport.DateFormat())  +'','_blank','scrollbars=no,resizable=no,toolbar=no,location=no,directories=no,status=yes,menubar=no,copyhistory=no,width=0,height=0,left=0,top=0');

    };
    this.Certificat_Item2_Actions = function (row, $modal) {
    window.location.href = '/fasi/dli/forms/NNClaimDeclarationDemo.aspx?Policy='+ row.nPolicy +'';

    };

    this.ControlActions = function () {

        $('#btnOk').click(function (event) {
            var formInstance = $("#H5ConsultaPolizasClienteSegunClienteIndicadoMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#btnOk'));
                btnLoading.start();
                H5ConsultaPolizasClienteSegunClienteIndicadoSupport.ItemsTblRequest();
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();
    
        $("#H5ConsultaPolizasClienteSegunClienteIndicadoMainForm").validate({
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
                Client: {
                    required: true
                },
                RecordEffectiveDate: {
                    required: true,
                    DatePicker: true
                }
            },
            messages: {
                Client: {
                    required: 'El campo es requerido'
                },
                RecordEffectiveDate: {
                    required: 'El campo es requerido',
                    DatePicker: 'The indicated date is not valid'
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
            onExpandRow: H5ConsultaPolizasClienteSegunClienteIndicadoSupport.ItemsTblExpandRow,
            columns: [{
                field: 'SCLIENT',
                title: 'Client ID',
                formatter: 'tableHelperSupport.IsContextMenu',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SCLIENAME',
                title: 'Complete Client Name',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SCLIENT',
                title: 'PayerClientID',
                sortable: true,
                halign: 'center',
                visible: false
            }]
        });

        table.bootstrapTable('refreshOptions', {
            contextMenu: '#ItemsContextMenu',
            contextMenuButton: '.menu-SCLIENT',
            beforeContextMenuRow: function (e, row, buttonElement) {
                H5ConsultaPolizasClienteSegunClienteIndicadoSupport.ItemsRowToInput(row);
                if (buttonElement && $(buttonElement).hasClass('menu-SCLIENT')) {

                    $('#ItemsTbl').bootstrapTable('showContextMenu', {
                        event: e,
                        contextMenu: '#Items_ClientSCLIENTContextMenu',
                        buttonElement: buttonElement
                    });
                }
                return false;
            },
            onContextMenuItem: function (row, $el) {
                H5ConsultaPolizasClienteSegunClienteIndicadoSupport.ItemsRowToInput(row);
                switch ($el.data("item")) {
                    case 'Items_ClientSCLIENT_Item1':
                        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.Items_ClientSCLIENT_Item1(row);
                        break;
                    case 'Items_ClientSCLIENT_Item2':
                        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.Items_ClientSCLIENT_Item2(row);
                        break;
                    case 'Items_ClientSCLIENT_Item3':
                        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.Items_ClientSCLIENT_Item3(row);
                        break;
                }
            }
        });


    };


    this.ItemsRowToInput = function (row) {
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.currentRow = row;
        $('#ClientSCLIENT').val(row.SCLIENT);
        $('#ClientSCLIENAME').val(row.SCLIENAME);
        $('#ClientSCLIENT').val(row.SCLIENT);

    };
    this.ItemsTblRequest = function (params) {
        app.core.AsyncWebMethod("/fasi/dli/queries/H5ConsultaPolizasClienteSegunClienteIndicadoActions.aspx/ItemsTblDataLoad", false,
            JSON.stringify({
                                CLIENTSCLIENT1: ($('#Client').data('code') !== undefined) ? $('#Client').data('code') : ''
            }),
            function (data) {
                    $('#ItemsTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);


            });
    };
    this.Curren_polTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'nCurrency',
                title: 'Currency',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.Curren_polnCurrency_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nCurrencyDesc',
                title: 'Currency',
                sortable: true,
                halign: 'center'
            }]
        });

        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.$el = table;
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.Curren_polTblRequest();
      };

    this.Curren_polRowToInput = function (row) {
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#Curren_polnCurrency', row.nCurrency);
        $('#Curren_polnCurrencyDesc').val(row.nCurrencyDesc);

    };
    this.Curren_polTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5ConsultaPolizasClienteSegunClienteIndicadoActions.aspx/Curren_polTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                CURRENPOLSCERTYPE1: row.SCERTYPE,
                CURRENPOLNBRANCH2: row.nBranch,
                CURRENPOLNPRODUCT3: row.nProduct,
                CURRENPOLNPOLICY4: row.nPolicy,
                CURRENPOLNCERTIF5: row.NCERTIF
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
    this.RolesTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'nRole',
                title: 'Role',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.RolesnRole_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nRoleDesc',
                title: 'Role',
                sortable: true,
                halign: 'center'
            }, {
                field: 'sClient',
                title: 'Client',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'sClientDesc',
                title: 'Client',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nStatusRol',
                title: 'Status',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.RolesnStatusRol_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nStatusRolDesc',
                title: 'Status',
                sortable: true,
                halign: 'center'
            }, {
                field: 'sSexClien',
                title: 'Gender',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'sSexClienDesc',
                title: 'Gender',
                sortable: true,
                halign: 'center'
            }, {
                field: 'dBirthDate',
                title: 'Birth Date',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'nTypeRisk',
                title: 'Risk Type',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.RolesnTypeRisk_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nTypeRiskDesc',
                title: 'Risk Type',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nRating',
                title: 'Rating',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.RolesnRating_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }]
        });

        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.$el = table;
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.RolesTblRequest();
      };

    this.RolesRowToInput = function (row) {
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#RolesnRole', row.nRole);
        $('#RolesnRoleDesc').val(row.nRoleDesc);
        $('#RolessClient').val(row.sClient);
        $('#RolessClientDesc').val(row.sClientDesc);
        AutoNumeric.set('#RolesnStatusRol', row.nStatusRol);
        $('#RolesnStatusRolDesc').val(row.nStatusRolDesc);
        $('#RolessSexClien').val(row.sSexClien);
        $('#RolessSexClienDesc').val(row.sSexClienDesc);
        $('#RolesdBirthDate').val(generalSupport.ToJavaScriptDateCustom(row.dBirthDate, generalSupport.DateFormat()));
        AutoNumeric.set('#RolesnTypeRisk', row.nTypeRisk);
        $('#RolesnTypeRiskDesc').val(row.nTypeRiskDesc);
        AutoNumeric.set('#RolesnRating', row.nRating);

    };
    this.RolesTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5ConsultaPolizasClienteSegunClienteIndicadoActions.aspx/RolesTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                ROLESSCERTYPE1: row.SCERTYPE,
                ROLESNBRANCH2: row.nBranch,
                ROLESNPRODUCT3: row.nProduct,
                ROLESNPOLICY4: row.nPolicy,
                ROLESNCERTIF5: row.NCERTIF
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
    this.SUM_INSURTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'NSUMINS_COD',
                title: 'Insured Amount',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.SUM_INSURNSUMINS_COD_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NSUMINS_CODDesc',
                title: 'Insured Amount',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NSUMINS_REAL',
                title: 'Real Value of Insured Property',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.SUM_INSURNSUMINS_REAL_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NCOINSURAN',
                title: 'Coinsurance',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.SUM_INSURNCOINSURAN_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NSUM_INSUR',
                title: 'Insured Value',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.SUM_INSURNSUM_INSUR_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }]
        });

        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.$el = table;
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.SUM_INSURTblRequest();
      };

    this.SUM_INSURRowToInput = function (row) {
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#SUM_INSURNSUMINS_COD', row.NSUMINS_COD);
        $('#SUM_INSURNSUMINS_CODDesc').val(row.NSUMINS_CODDesc);
        AutoNumeric.set('#SUM_INSURNSUMINS_REAL', row.NSUMINS_REAL);
        AutoNumeric.set('#SUM_INSURNCOINSURAN', row.NCOINSURAN);
        AutoNumeric.set('#SUM_INSURNSUM_INSUR', row.NSUM_INSUR);

    };
    this.SUM_INSURTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5ConsultaPolizasClienteSegunClienteIndicadoActions.aspx/SUM_INSURTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                SUMINSURSCERTYPE1: row.SCERTYPE,
                SUMINSURNBRANCH2: row.nBranch,
                SUMINSURNPRODUCT3: row.nProduct,
                SUMINSURNPOLICY4: row.nPolicy,
                SUMINSURNCERTIF5: row.NCERTIF
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
    this.CoverTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'nModulec',
                title: 'Module',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.CovernModulec_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nModulecDesc',
                title: 'Module',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nCover',
                title: 'Coverage',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.CovernCover_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nCoverDesc',
                title: 'Coverage',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nRole',
                title: 'Client Role',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.CovernRole_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nRoleDesc',
                title: 'Client Role',
                sortable: true,
                halign: 'center'
            }, {
                field: 'sClient',
                title: 'Client',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'sClientDesc',
                title: 'Client',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nCurrency',
                title: 'Currency',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.CovernCurrency_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nCurrencyDesc',
                title: 'Currency',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nCapital',
                title: 'Insured Amount',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.CovernCapital_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nPremium',
                title: 'Annual Premium',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.CovernPremium_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }]
        });

        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.$el = table;
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.CoverTblRequest();
      };

    this.CoverRowToInput = function (row) {
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#CovernModulec', row.nModulec);
        $('#CovernModulecDesc').val(row.nModulecDesc);
        AutoNumeric.set('#CovernCover', row.nCover);
        $('#CovernCoverDesc').val(row.nCoverDesc);
        AutoNumeric.set('#CovernRole', row.nRole);
        $('#CovernRoleDesc').val(row.nRoleDesc);
        $('#CoversClient').val(row.sClient);
        $('#CoversClientDesc').val(row.sClientDesc);
        AutoNumeric.set('#CovernCurrency', row.nCurrency);
        $('#CovernCurrencyDesc').val(row.nCurrencyDesc);
        AutoNumeric.set('#CovernCapital', row.nCapital);
        AutoNumeric.set('#CovernPremium', row.nPremium);

    };
    this.CoverTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5ConsultaPolizasClienteSegunClienteIndicadoActions.aspx/CoverTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                COVERSCERTYPE1: row.SCERTYPE,
                COVERNBRANCH2: row.nBranch,
                COVERNPRODUCT3: row.nProduct,
                COVERNPOLICY4: row.nPolicy,
                COVERNCERTIF5: row.NCERTIF
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
    this.REINSURANTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'NBRANCH_REI',
                title: 'Reinsurance Line of Business',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.REINSURANNBRANCH_REI_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NBRANCH_REIDesc',
                title: 'Reinsurance Line of Business',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NTYPE_REIN',
                title: 'Treaty',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.REINSURANNTYPE_REIN_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NTYPE_REINDesc',
                title: 'Treaty',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NCAPITAL',
                title: 'Insured Amount Ceded',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.REINSURANNCAPITAL_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NCURRENCY',
                title: 'Currency',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.REINSURANNCURRENCY_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NCURRENCYDesc',
                title: 'Currency',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NSHARE',
                title: 'Share',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.REINSURANNSHARE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }]
        });

        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.$el = table;
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.REINSURANTblRequest();
      };

    this.REINSURANRowToInput = function (row) {
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#REINSURANNBRANCH_REI', row.NBRANCH_REI);
        $('#REINSURANNBRANCH_REIDesc').val(row.NBRANCH_REIDesc);
        AutoNumeric.set('#REINSURANNTYPE_REIN', row.NTYPE_REIN);
        $('#REINSURANNTYPE_REINDesc').val(row.NTYPE_REINDesc);
        AutoNumeric.set('#REINSURANNCAPITAL', row.NCAPITAL);
        AutoNumeric.set('#REINSURANNCURRENCY', row.NCURRENCY);
        $('#REINSURANNCURRENCYDesc').val(row.NCURRENCYDesc);
        AutoNumeric.set('#REINSURANNSHARE', row.NSHARE);

    };
    this.REINSURANTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5ConsultaPolizasClienteSegunClienteIndicadoActions.aspx/REINSURANTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                REINSURANSCERTYPE3: row.SCERTYPE,
                REINSURANNBRANCH4: row.nBranch,
                REINSURANNPRODUCT5: row.nProduct,
                REINSURANNPOLICY6: row.nPolicy,
                REINSURANNCERTIF7: row.NCERTIF
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
    this.REINSURAN2TblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'NBRANCH_REI',
                title: 'Reinsurance Line of Business',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.REINSURAN2NBRANCH_REI_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NBRANCH_REIDesc',
                title: 'Reinsurance Line of Business',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NCOMPANY',
                title: 'Company Code',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.REINSURAN2NCOMPANY_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NCAPITAL',
                title: 'Insured Amount Ceded',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.REINSURAN2NCAPITAL_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NCURRENCY',
                title: 'Currency Code',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.REINSURAN2NCURRENCY_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NCURRENCYDesc',
                title: 'Currency Code',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NSHARE',
                title: 'Share',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.REINSURAN2NSHARE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NCOMMISSI',
                title: 'Commission Percentage',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.REINSURAN2NCOMMISSI_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }]
        });

        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.$el = table;
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.REINSURAN2TblRequest();
      };

    this.REINSURAN2RowToInput = function (row) {
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#REINSURAN2NBRANCH_REI', row.NBRANCH_REI);
        $('#REINSURAN2NBRANCH_REIDesc').val(row.NBRANCH_REIDesc);
        AutoNumeric.set('#REINSURAN2NCOMPANY', row.NCOMPANY);
        AutoNumeric.set('#REINSURAN2NCAPITAL', row.NCAPITAL);
        AutoNumeric.set('#REINSURAN2NCURRENCY', row.NCURRENCY);
        $('#REINSURAN2NCURRENCYDesc').val(row.NCURRENCYDesc);
        AutoNumeric.set('#REINSURAN2NSHARE', row.NSHARE);
        AutoNumeric.set('#REINSURAN2NCOMMISSI', row.NCOMMISSI);

    };
    this.REINSURAN2TblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5ConsultaPolizasClienteSegunClienteIndicadoActions.aspx/REINSURAN2TblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                REINSURANSCERTYPE3: row.SCERTYPE,
                REINSURANNBRANCH4: row.nBranch,
                REINSURANNPRODUCT5: row.nProduct,
                REINSURANNPOLICY6: row.nPolicy,
                REINSURANNCERTIF7: row.NCERTIF
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
    this.Disc_xpremTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'nDisc_code',
                title: 'Extra Premium Discount or Tax Code',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.Disc_xpremnDisc_code_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nDisc_codeDesc',
                title: 'Extra Premium Discount or Tax Code',
                sortable: true,
                halign: 'center'
            }, {
                field: 'sAgree',
                title: 'Accepted Surcharge',
                formatter: 'tableHelperSupport.IsCheck',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nCause',
                title: 'Reason',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.Disc_xpremnCause_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nCauseDesc',
                title: 'Reason',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nPercent',
                title: 'Percentage',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.Disc_xpremnPercent_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nCurrency',
                title: 'Currency',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.Disc_xpremnCurrency_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nCurrencyDesc',
                title: 'Currency',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nAmount',
                title: 'Fixed Amount',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.Disc_xpremnAmount_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }]
        });

        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.$el = table;
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.Disc_xpremTblRequest();
      };

    this.Disc_xpremRowToInput = function (row) {
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#Disc_xpremnDisc_code', row.nDisc_code);
        $('#Disc_xpremnDisc_codeDesc').val(row.nDisc_codeDesc);
        $('#Disc_xpremsAgree').prop("checked", row.sAgree);
        AutoNumeric.set('#Disc_xpremnCause', row.nCause);
        $('#Disc_xpremnCauseDesc').val(row.nCauseDesc);
        AutoNumeric.set('#Disc_xpremnPercent', row.nPercent);
        AutoNumeric.set('#Disc_xpremnCurrency', row.nCurrency);
        $('#Disc_xpremnCurrencyDesc').val(row.nCurrencyDesc);
        AutoNumeric.set('#Disc_xpremnAmount', row.nAmount);

    };
    this.Disc_xpremTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5ConsultaPolizasClienteSegunClienteIndicadoActions.aspx/Disc_xpremTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                DISCXPREMSCERTYPE1: row.SCERTYPE,
                DISCXPREMNBRANCH2: row.nBranch,
                DISCXPREMNPRODUCT3: row.nProduct,
                DISCXPREMNPOLICY4: row.nPolicy,
                DISCXPREMNCERTIF5: row.NCERTIF
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
    this.AutoTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'sRegist',
                title: 'License Plate',
                sortable: true,
                halign: 'center'
            }, {
                field: 'sLicense_ty',
                title: 'License Plate Type',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'sLicense_tyDesc',
                title: 'License Plate Type',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nVehType',
                title: 'Vehicle Type',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.AutonVehType_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nVehTypeDesc',
                title: 'Vehicle Type',
                sortable: true,
                halign: 'center'
            }, {
                field: 'sMotor',
                title: 'Engine Serial Number',
                sortable: true,
                halign: 'center'
            }, {
                field: 'sChassis',
                title: 'Chassis',
                sortable: true,
                halign: 'center'
            }, {
                field: 'sColor',
                title: 'Color',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nCapital',
                title: 'Sum Insured',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.AutonCapital_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nVeh_valor',
                title: 'Value of The Vehicle',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.AutonVeh_valor_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nYear',
                title: 'Year of Manufactured',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.AutonYear_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nAutoZone',
                title: 'Driving Zone',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.AutonAutoZone_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nAutoZoneDesc',
                title: 'Driving Zone',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nUse',
                title: 'Use of Vehicle',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.AutonUse_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nUseDesc',
                title: 'Use of Vehicle',
                sortable: true,
                halign: 'center'
            }]
        });

        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.$el = table;
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.AutoTblRequest();
      };

    this.AutoRowToInput = function (row) {
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.currentRow = row;
        $('#AutosRegist').val(row.sRegist);
        $('#AutosLicense_ty').val(row.sLicense_ty);
        $('#AutosLicense_tyDesc').val(row.sLicense_tyDesc);
        AutoNumeric.set('#AutonVehType', row.nVehType);
        $('#AutonVehTypeDesc').val(row.nVehTypeDesc);
        $('#AutosMotor').val(row.sMotor);
        $('#AutosChassis').val(row.sChassis);
        $('#AutosColor').val(row.sColor);
        AutoNumeric.set('#AutonCapital', row.nCapital);
        AutoNumeric.set('#AutonVeh_valor', row.nVeh_valor);
        AutoNumeric.set('#AutonYear', row.nYear);
        AutoNumeric.set('#AutonAutoZone', row.nAutoZone);
        $('#AutonAutoZoneDesc').val(row.nAutoZoneDesc);
        AutoNumeric.set('#AutonUse', row.nUse);
        $('#AutonUseDesc').val(row.nUseDesc);

    };
    this.AutoTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5ConsultaPolizasClienteSegunClienteIndicadoActions.aspx/AutoTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                AUTOSCERTYPE1: row.SCERTYPE,
                AUTONBRANCH2: row.nBranch,
                AUTONPRODUCT3: row.nProduct,
                AUTONPOLICY4: row.nPolicy,
                AUTONCERTIF5: row.NCERTIF
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
    this.FireTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'nConstCat',
                title: 'Construction Class',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.FirenConstCat_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nConstCatDesc',
                title: 'Construction Class',
                formatter: 'tableHelperSupport.IsContextMenu',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nActivityCat',
                title: 'Activity Category',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.FirenActivityCat_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nActivityCatDesc',
                title: 'Activity Category',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nFloor_quan',
                title: 'Number of Floors',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.FirenFloor_quan_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nRoofType',
                title: 'Roof Type',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.FirenRoofType_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nRoofTypeDesc',
                title: 'Roof Type',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nSeismicZone',
                title: 'Seismic Zone',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.FirenSeismicZone_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nSeismicZoneDesc',
                title: 'Seismic Zone',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nBuildType',
                title: 'Type of Seismic Construction',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.FirenBuildType_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nBuildTypeDesc',
                title: 'Type of Seismic Construction',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nSpCombType',
                title: 'Spontaneous Combustion Type',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.FirenSpCombType_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nSpCombTypeDesc',
                title: 'Spontaneous Combustion Type',
                sortable: true,
                halign: 'center'
            }, {
                field: 'sDescBussi',
                title: 'Specific Description',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nHurrican_zone',
                title: 'Hurrican Zone',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.FirenHurrican_zone_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nSideCloseType',
                title: 'Side Closure Type',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.FirenSideCloseType_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nSideCloseTypeDesc',
                title: 'Side Closure Type',
                sortable: true,
                halign: 'center'
            }]
        });
        table.bootstrapTable('refreshOptions', {
            contextMenu: '#FireContextMenu',
            contextMenuButton: '.menu-nConstCatDesc',
            beforeContextMenuRow: function (e, row, buttonElement) {
                H5ConsultaPolizasClienteSegunClienteIndicadoSupport.FireRowToInput(row);
                if (!buttonElement ) {

                    table.bootstrapTable('showContextMenu', {
                        event: e,
                        contextMenu: '#FireContextMenu',
                        buttonElement: buttonElement
                    });
                }
                if (buttonElement && $(buttonElement).hasClass('menu-nConstCatDesc')) {

                    $('#FireTbl').bootstrapTable('showContextMenu', {
                        event: e,
                        contextMenu: '#Fire_FirenConstCatDescContextMenu',
                        buttonElement: buttonElement
                    });
                }
                return false;
            },
            onContextMenuItem: function (row, $el) {
                H5ConsultaPolizasClienteSegunClienteIndicadoSupport.FireRowToInput(row);
                switch ($el.data("item")) {
                    case 'Fire_Item1':
                        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.Fire_Item1_Actions(row, null);
                        break;
                    case 'Fire_FirenConstCatDesc_Item1':
                        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.Fire_FirenConstCatDesc_Item1(row);
                        break;
                }
            }
        });

        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.$el = table;
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.FireTblRequest();
      };

    this.FireRowToInput = function (row) {
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#FirenConstCat', row.nConstCat);
        $('#FirenConstCatDesc').val(row.nConstCatDesc);
        AutoNumeric.set('#FirenActivityCat', row.nActivityCat);
        $('#FirenActivityCatDesc').val(row.nActivityCatDesc);
        AutoNumeric.set('#FirenFloor_quan', row.nFloor_quan);
        AutoNumeric.set('#FirenRoofType', row.nRoofType);
        $('#FirenRoofTypeDesc').val(row.nRoofTypeDesc);
        AutoNumeric.set('#FirenSeismicZone', row.nSeismicZone);
        $('#FirenSeismicZoneDesc').val(row.nSeismicZoneDesc);
        AutoNumeric.set('#FirenBuildType', row.nBuildType);
        $('#FirenBuildTypeDesc').val(row.nBuildTypeDesc);
        AutoNumeric.set('#FirenSpCombType', row.nSpCombType);
        $('#FirenSpCombTypeDesc').val(row.nSpCombTypeDesc);
        $('#FiresDescBussi').val(row.sDescBussi);
        AutoNumeric.set('#FirenHurrican_zone', row.nHurrican_zone);
        AutoNumeric.set('#FirenSideCloseType', row.nSideCloseType);
        $('#FirenSideCloseTypeDesc').val(row.nSideCloseTypeDesc);

    };
    this.FireTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5ConsultaPolizasClienteSegunClienteIndicadoActions.aspx/FireTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                FIRESCERTYPE1: row.SCERTYPE,
                FIRENBRANCH2: row.nBranch,
                FIRENPRODUCT3: row.nProduct,
                FIRENPOLICY4: row.nPolicy,
                FIRENCERTIF5: row.NCERTIF
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
    this.HomeOwnerTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'nOwnerShip',
                title: 'Ownership',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.HomeOwnernOwnerShip_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nOwnerShipDesc',
                title: 'Ownership',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nDwellingType',
                title: 'Dwelling Type',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.HomeOwnernDwellingType_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nDwellingTypeDesc',
                title: 'Dwelling Type',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NSEISMICZONE',
                title: 'Seismic Area',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.HomeOwnerNSEISMICZONE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nYear_built',
                title: 'Year Built',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.HomeOwnernYear_built_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'dDate_purch',
                title: 'Date of Purchase',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center',
                visible: false
            }, {
                field: 'nPrice_purch',
                title: 'Purchase Price',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.HomeOwnernPrice_purch_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nCurrency_purch',
                title: 'Currency of Purchase Price',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.HomeOwnernCurrency_purch_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nCurrency_purchDesc',
                title: 'Currency of Purchase Price',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nLandSuper',
                title: 'Land Area',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.HomeOwnernLandSuper_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nHomeSuper',
                title: 'Area',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.HomeOwnernHomeSuper_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nRoofType',
                title: 'Roof Type',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.HomeOwnernRoofType_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nRoofTypeDesc',
                title: 'Roof Type',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'nRoofYear',
                title: 'Roof Year',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.HomeOwnernRoofYear_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nFloodZone',
                title: 'Flood Zone Type',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.HomeOwnernFloodZone_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nFloodZoneDesc',
                title: 'Flood Zone Type',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nFoundType',
                title: 'Foundation',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.HomeOwnernFoundType_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'sSprinkSys',
                title: 'Sprinklers',
                formatter: 'tableHelperSupport.IsCheck',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'nAirType',
                title: 'Air Conditioning Type',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.HomeOwnernAirType_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nAirTypeDesc',
                title: 'Air Conditioning Type',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'nStories',
                title: 'Stories',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.HomeOwnernStories_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nHalfBath',
                title: 'Half Bathrooms',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.HomeOwnernHalfBath_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nFullBath',
                title: 'Full Bathrooms',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.HomeOwnernFullBath_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nBedrooms',
                title: 'Bedrooms',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.HomeOwnernBedrooms_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nFirePlace',
                title: 'Chimneys',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.HomeOwnernFirePlace_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nGarage',
                title: 'Number of Cars',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.HomeOwnernGarage_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'sAnimalsDes',
                title: 'Animals Descriptions',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'nDist_Fire',
                title: 'Distance to Fire Department',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.HomeOwnernDist_Fire_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'sAlarm_comp',
                title: 'Company Monitoring The Alarm',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'sFencePool',
                title: 'Fenced Pool',
                formatter: 'tableHelperSupport.IsCheck',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'nSwimPool',
                title: 'Ubication of Swimming Pool',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.HomeOwnernSwimPool_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nSwimPoolDesc',
                title: 'Ubication of Swimming Pool',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'nFenceHeight',
                title: 'Fence Height',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.HomeOwnernFenceHeight_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'sPolicy_other',
                title: 'Another Policy Indicator',
                formatter: 'tableHelperSupport.IsCheck',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'nCap_other',
                title: 'Sum Insured of The Other Policy',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.HomeOwnernCap_other_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nCurrency_other',
                title: 'Currency Other Policy',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.HomeOwnernCurrency_other_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nCurrency_otherDesc',
                title: 'Currency Other Policy',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'dExpir_other',
                title: 'Expiration of The Other Policy',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center',
                visible: false
            }]
        });

        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.$el = table;
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.HomeOwnerTblRequest();
      };

    this.HomeOwnerRowToInput = function (row) {
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#HomeOwnernOwnerShip', row.nOwnerShip);
        $('#HomeOwnernOwnerShipDesc').val(row.nOwnerShipDesc);
        AutoNumeric.set('#HomeOwnernDwellingType', row.nDwellingType);
        $('#HomeOwnernDwellingTypeDesc').val(row.nDwellingTypeDesc);
        AutoNumeric.set('#HomeOwnerNSEISMICZONE', row.NSEISMICZONE);
        AutoNumeric.set('#HomeOwnernYear_built', row.nYear_built);
        $('#HomeOwnerdDate_purch').val(generalSupport.ToJavaScriptDateCustom(row.dDate_purch, generalSupport.DateFormat()));
        AutoNumeric.set('#HomeOwnernPrice_purch', row.nPrice_purch);
        AutoNumeric.set('#HomeOwnernCurrency_purch', row.nCurrency_purch);
        $('#HomeOwnernCurrency_purchDesc').val(row.nCurrency_purchDesc);
        AutoNumeric.set('#HomeOwnernLandSuper', row.nLandSuper);
        AutoNumeric.set('#HomeOwnernHomeSuper', row.nHomeSuper);
        AutoNumeric.set('#HomeOwnernRoofType', row.nRoofType);
        $('#HomeOwnernRoofTypeDesc').val(row.nRoofTypeDesc);
        AutoNumeric.set('#HomeOwnernRoofYear', row.nRoofYear);
        AutoNumeric.set('#HomeOwnernFloodZone', row.nFloodZone);
        $('#HomeOwnernFloodZoneDesc').val(row.nFloodZoneDesc);
        AutoNumeric.set('#HomeOwnernFoundType', row.nFoundType);
        $('#HomeOwnersSprinkSys').prop("checked", row.sSprinkSys);
        AutoNumeric.set('#HomeOwnernAirType', row.nAirType);
        $('#HomeOwnernAirTypeDesc').val(row.nAirTypeDesc);
        AutoNumeric.set('#HomeOwnernStories', row.nStories);
        AutoNumeric.set('#HomeOwnernHalfBath', row.nHalfBath);
        AutoNumeric.set('#HomeOwnernFullBath', row.nFullBath);
        AutoNumeric.set('#HomeOwnernBedrooms', row.nBedrooms);
        AutoNumeric.set('#HomeOwnernFirePlace', row.nFirePlace);
        AutoNumeric.set('#HomeOwnernGarage', row.nGarage);
        $('#HomeOwnersAnimalsDes').val(row.sAnimalsDes);
        AutoNumeric.set('#HomeOwnernDist_Fire', row.nDist_Fire);
        $('#HomeOwnersAlarm_comp').val(row.sAlarm_comp);
        $('#HomeOwnersFencePool').prop("checked", row.sFencePool);
        AutoNumeric.set('#HomeOwnernSwimPool', row.nSwimPool);
        $('#HomeOwnernSwimPoolDesc').val(row.nSwimPoolDesc);
        AutoNumeric.set('#HomeOwnernFenceHeight', row.nFenceHeight);
        $('#HomeOwnersPolicy_other').prop("checked", row.sPolicy_other);
        AutoNumeric.set('#HomeOwnernCap_other', row.nCap_other);
        AutoNumeric.set('#HomeOwnernCurrency_other', row.nCurrency_other);
        $('#HomeOwnernCurrency_otherDesc').val(row.nCurrency_otherDesc);
        $('#HomeOwnerdExpir_other').val(generalSupport.ToJavaScriptDateCustom(row.dExpir_other, generalSupport.DateFormat()));

    };
    this.HomeOwnerTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5ConsultaPolizasClienteSegunClienteIndicadoActions.aspx/HomeOwnerTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                HOMEOWNERSCERTYPE1: row.SCERTYPE,
                HOMEOWNERNBRANCH2: row.nBranch,
                HOMEOWNERNPRODUCT3: row.nProduct,
                HOMEOWNERNPOLICY4: row.nPolicy,
                HOMEOWNERNCERTIF5: row.NCERTIF
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
    this.LifeTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'nTypDurPay',
                title: 'Type Off Payments Period',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.LifenTypDurPay_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nTypDurPayDesc',
                title: 'Type Off Payments Period',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nPay_time',
                title: 'Duration of Payments',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.LifenPay_time_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nTypDurIns',
                title: 'Type of Insurance Period',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.LifenTypDurIns_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nTypDurInsDesc',
                title: 'Type of Insurance Period',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nInsur_time',
                title: 'Insurance Duration',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.LifenInsur_time_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nXprem_time',
                title: 'Duration of Extra Premiums',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.LifenXprem_time_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nAge_limit',
                title: 'Maximum Age',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.LifenAge_limit_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NAGE_REINSU',
                title: 'Actuarial Age',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.LifeNAGE_REINSU_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NAGE',
                title: 'Insured Age',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.LifeNAGE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }]
        });

        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.$el = table;
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.LifeTblRequest();
      };

    this.LifeRowToInput = function (row) {
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#LifenTypDurPay', row.nTypDurPay);
        $('#LifenTypDurPayDesc').val(row.nTypDurPayDesc);
        AutoNumeric.set('#LifenPay_time', row.nPay_time);
        AutoNumeric.set('#LifenTypDurIns', row.nTypDurIns);
        $('#LifenTypDurInsDesc').val(row.nTypDurInsDesc);
        AutoNumeric.set('#LifenInsur_time', row.nInsur_time);
        AutoNumeric.set('#LifenXprem_time', row.nXprem_time);
        AutoNumeric.set('#LifenAge_limit', row.nAge_limit);
        AutoNumeric.set('#LifeNAGE_REINSU', row.NAGE_REINSU);
        AutoNumeric.set('#LifeNAGE', row.NAGE);

    };
    this.LifeTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5ConsultaPolizasClienteSegunClienteIndicadoActions.aspx/LifeTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                LIFESCERTYPE1: row.SCERTYPE,
                LIFENBRANCH2: row.nBranch,
                LIFENPRODUCT3: row.nProduct,
                LIFENPOLICY4: row.nPolicy,
                LIFENCERTIF5: row.NCERTIF
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
    this.THEFTTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'DSTARTDATE',
                title: 'Effective Date',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'DEXPIRDAT',
                title: 'Ending Date',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'NCOMMERGRP',
                title: 'Commercial Group',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.THEFTNCOMMERGRP_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NCOMMERGRPDesc',
                title: 'Commercial Group',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SDESCBUSSI',
                title: 'Specific Description',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NINSURED',
                title: 'First Risk Percentage',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.THEFTNINSURED_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NEMPLOYEES',
                title: 'Number of Employees Transporting The Values',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.THEFTNEMPLOYEES_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NAREA',
                title: 'Area',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.THEFTNAREA_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NVIGILANCE',
                title: 'Number of Watchmen',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.THEFTNVIGILANCE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NCAPITAL',
                title: 'Insured Amount',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.THEFTNCAPITAL_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'DNULLDATE',
                title: 'Cancellation Date',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'NNULLCODE',
                title: 'Cancellation Code',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.THEFTNNULLCODE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }]
        });

        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.$el = table;
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.THEFTTblRequest();
      };

    this.THEFTRowToInput = function (row) {
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.currentRow = row;
        $('#THEFTDSTARTDATE').val(generalSupport.ToJavaScriptDateCustom(row.DSTARTDATE, generalSupport.DateFormat()));
        $('#THEFTDEXPIRDAT').val(generalSupport.ToJavaScriptDateCustom(row.DEXPIRDAT, generalSupport.DateFormat()));
        AutoNumeric.set('#THEFTNCOMMERGRP', row.NCOMMERGRP);
        $('#THEFTNCOMMERGRPDesc').val(row.NCOMMERGRPDesc);
        $('#THEFTSDESCBUSSI').val(row.SDESCBUSSI);
        AutoNumeric.set('#THEFTNINSURED', row.NINSURED);
        AutoNumeric.set('#THEFTNEMPLOYEES', row.NEMPLOYEES);
        AutoNumeric.set('#THEFTNAREA', row.NAREA);
        AutoNumeric.set('#THEFTNVIGILANCE', row.NVIGILANCE);
        AutoNumeric.set('#THEFTNCAPITAL', row.NCAPITAL);
        $('#THEFTDNULLDATE').val(generalSupport.ToJavaScriptDateCustom(row.DNULLDATE, generalSupport.DateFormat()));
        AutoNumeric.set('#THEFTNNULLCODE', row.NNULLCODE);

    };
    this.THEFTTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5ConsultaPolizasClienteSegunClienteIndicadoActions.aspx/THEFTTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                THEFTSCERTYPE1: row.SCERTYPE,
                THEFTNBRANCH2: row.nBranch,
                THEFTNPRODUCT3: row.nProduct,
                THEFTNPOLICY4: row.nPolicy,
                THEFTNCERTIF5: row.NCERTIF
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
    this.FINANCIAL_INSTRUMENTSTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'NCONSECUTIVE',
                title: 'Identifier that makes single record',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.FINANCIAL_INSTRUMENTSNCONSECUTIVE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NINSTRUMENT_TY',
                title: 'Type of financial instrument',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.FINANCIAL_INSTRUMENTSNINSTRUMENT_TY_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NINSTRUMENT_TYDesc',
                title: 'Type of financial instrument',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NBANK_CODE',
                title: 'Bank Internal Code',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.FINANCIAL_INSTRUMENTSNBANK_CODE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NBANK_CODEDesc',
                title: 'Bank Internal Code',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NCARD_TYPE',
                title: 'Credit Card Type',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.FINANCIAL_INSTRUMENTSNCARD_TYPE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NCARD_TYPEDesc',
                title: 'Credit Card Type',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SNUMBER',
                title: 'Number of the Credit Card.',
                sortable: true,
                halign: 'center'
            }, {
                field: 'DCARDEXPIR',
                title: 'Expiry date of the credit card',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'DSTARTDATE',
                title: 'Effective date of the Credit',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'DTERM_DATE',
                title: 'Ending date of the Credit',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'NQUOTA',
                title: 'Quantity of drafts',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.FINANCIAL_INSTRUMENTSNQUOTA_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NAMOUNT',
                title: 'Amount of credit',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.FINANCIAL_INSTRUMENTSNAMOUNT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NCURRENCY',
                title: 'Currency Code',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.FINANCIAL_INSTRUMENTSNCURRENCY_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NCURRENCYDesc',
                title: 'Currency Code',
                sortable: true,
                halign: 'center'
            }]
        });

        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.$el = table;
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.FINANCIAL_INSTRUMENTSTblRequest();
      };

    this.FINANCIAL_INSTRUMENTSRowToInput = function (row) {
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#FINANCIAL_INSTRUMENTSNCONSECUTIVE', row.NCONSECUTIVE);
        AutoNumeric.set('#FINANCIAL_INSTRUMENTSNINSTRUMENT_TY', row.NINSTRUMENT_TY);
        $('#FINANCIAL_INSTRUMENTSNINSTRUMENT_TYDesc').val(row.NINSTRUMENT_TYDesc);
        AutoNumeric.set('#FINANCIAL_INSTRUMENTSNBANK_CODE', row.NBANK_CODE);
        $('#FINANCIAL_INSTRUMENTSNBANK_CODEDesc').val(row.NBANK_CODEDesc);
        AutoNumeric.set('#FINANCIAL_INSTRUMENTSNCARD_TYPE', row.NCARD_TYPE);
        $('#FINANCIAL_INSTRUMENTSNCARD_TYPEDesc').val(row.NCARD_TYPEDesc);
        $('#FINANCIAL_INSTRUMENTSSNUMBER').val(row.SNUMBER);
        $('#FINANCIAL_INSTRUMENTSDCARDEXPIR').val(generalSupport.ToJavaScriptDateCustom(row.DCARDEXPIR, generalSupport.DateFormat()));
        $('#FINANCIAL_INSTRUMENTSDSTARTDATE').val(generalSupport.ToJavaScriptDateCustom(row.DSTARTDATE, generalSupport.DateFormat()));
        $('#FINANCIAL_INSTRUMENTSDTERM_DATE').val(generalSupport.ToJavaScriptDateCustom(row.DTERM_DATE, generalSupport.DateFormat()));
        AutoNumeric.set('#FINANCIAL_INSTRUMENTSNQUOTA', row.NQUOTA);
        AutoNumeric.set('#FINANCIAL_INSTRUMENTSNAMOUNT', row.NAMOUNT);
        AutoNumeric.set('#FINANCIAL_INSTRUMENTSNCURRENCY', row.NCURRENCY);
        $('#FINANCIAL_INSTRUMENTSNCURRENCYDesc').val(row.NCURRENCYDesc);

    };
    this.FINANCIAL_INSTRUMENTSTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5ConsultaPolizasClienteSegunClienteIndicadoActions.aspx/FINANCIAL_INSTRUMENTSTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                FINANCIALINSTRUMENTSSCERTYPE1: row.SCERTYPE,
                FINANCIALINSTRUMENTSNBRANCH2: row.nBranch,
                FINANCIALINSTRUMENTSNPRODUCT3: row.nProduct,
                FINANCIALINSTRUMENTSNPOLICY4: row.nPolicy,
                FINANCIALINSTRUMENTSNCERTIF5: row.NCERTIF
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
    this.ROLES2TblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'NROLE',
                title: 'Client Role',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.ROLES2NROLE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NROLEDesc',
                title: 'Client Role',
                formatter: 'tableHelperSupport.IsContextMenu',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SCLIENT',
                title: 'Client ID',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'SCLIENTDesc',
                title: 'Client ID',
                sortable: true,
                halign: 'center'
            }]
        });
        table.bootstrapTable('refreshOptions', {
            contextMenu: '#ROLES2ContextMenu',
            contextMenuButton: '.menu-NROLEDesc',
            beforeContextMenuRow: function (e, row, buttonElement) {
                H5ConsultaPolizasClienteSegunClienteIndicadoSupport.ROLES2RowToInput(row);
                if (!buttonElement ) {

                    table.bootstrapTable('showContextMenu', {
                        event: e,
                        contextMenu: '#ROLES2ContextMenu',
                        buttonElement: buttonElement
                    });
                }
                if (buttonElement && $(buttonElement).hasClass('menu-NROLEDesc')) {

                    $('#ROLES2Tbl').bootstrapTable('showContextMenu', {
                        event: e,
                        contextMenu: '#ROLES2_ROLES2NROLEDescContextMenu',
                        buttonElement: buttonElement
                    });
                }
                return false;
            },
            onContextMenuItem: function (row, $el) {
                H5ConsultaPolizasClienteSegunClienteIndicadoSupport.ROLES2RowToInput(row);
                switch ($el.data("item")) {
                    case 'ROLES2_Item1':
                        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.ROLES2_Item1_Actions(row, null);
                        break;
                    case 'ROLES2_ROLES2NROLEDesc_Item1':
                        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.ROLES2_ROLES2NROLEDesc_Item1(row);
                        break;
                }
            }
        });

        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.$el = table;
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.ROLES2TblRequest();
      };

    this.ROLES2RowToInput = function (row) {
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#ROLES2NROLE', row.NROLE);
        $('#ROLES2NROLEDesc').val(row.NROLEDesc);
        $('#ROLES2SCLIENT').val(row.SCLIENT);
        $('#ROLES2SCLIENTDesc').val(row.SCLIENTDesc);

    };
    this.ROLES2TblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5ConsultaPolizasClienteSegunClienteIndicadoActions.aspx/ROLES2TblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                ROLESSCERTYPE3: row.SCERTYPE,
                ROLESNBRANCH4: row.NBRANCH,
                ROLESNPRODUCT5: row.NPRODUCT,
                ROLESNPOLICY6: row.NPOLICY,
                ROLESNCERTIF7: row.NCERTIF
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
    this.HEALTHTblExpandRow = function (index, row, $detail) {
       var tblparentid = $detail.parent().parent().parent()[0].id;
       var html = [];

        var detailShow = H5ConsultaPolizasClienteSegunClienteIndicadoSupport.ROLES2_ShowValidation(row);
        if (detailShow)
        html.push('<table id="ROLES2Tbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Insured Persons</caption></table>');

        $detail.html(html.join(""));
        if (detailShow)
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.ROLES2TblSetup($detail.find('#ROLES2Tbl-' + index));

    };
    this.HEALTHTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            detailView: true,
            onExpandRow: H5ConsultaPolizasClienteSegunClienteIndicadoSupport.HEALTHTblExpandRow,

            columns: [{
                field: 'DEFFECDATE',
                title: 'Record Effective Date',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'DEXPIRDAT',
                title: 'Ending Date',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'NCAPITAL',
                title: 'Insured Amount',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.HEALTHNCAPITAL_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NPREMIUM',
                title: 'Premium',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.HEALTHNPREMIUM_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'SCERTYPE',
                title: 'RecordType',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'NBRANCH',
                title: 'LineOfBusiness',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'NPRODUCT',
                title: 'ProductCode',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'NPOLICY',
                title: 'PolicyID',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'NCERTIF',
                title: 'CertificateID',
                sortable: true,
                halign: 'center',
                visible: false
            }]
        });

        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.$el = table;
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.HEALTHTblRequest();
      };

    this.HEALTHRowToInput = function (row) {
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.currentRow = row;
        $('#HEALTHDEFFECDATE').val(generalSupport.ToJavaScriptDateCustom(row.DEFFECDATE, generalSupport.DateFormat()));
        $('#HEALTHDEXPIRDAT').val(generalSupport.ToJavaScriptDateCustom(row.DEXPIRDAT, generalSupport.DateFormat()));
        AutoNumeric.set('#HEALTHNCAPITAL', row.NCAPITAL);
        AutoNumeric.set('#HEALTHNPREMIUM', row.NPREMIUM);
        $('#HEALTHSCERTYPE').val(row.SCERTYPE);
        $('#HEALTHNBRANCH').val(row.NBRANCH);
        $('#HEALTHNPRODUCT').val(row.NPRODUCT);
        $('#HEALTHNPOLICY').val(row.NPOLICY);
        $('#HEALTHNCERTIF').val(row.NCERTIF);

    };
    this.HEALTHTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5ConsultaPolizasClienteSegunClienteIndicadoActions.aspx/HEALTHTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                HEALTHSCERTYPE1: row.SCERTYPE,
                HEALTHNBRANCH2: row.nBranch,
                HEALTHNPRODUCT3: row.nProduct,
                HEALTHNPOLICY4: row.nPolicy,
                HEALTHNCERTIF5: row.NCERTIF
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
    this.BeneficiarTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'nModulec',
                title: 'Coverage Module',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.BeneficiarnModulec_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nModulecDesc',
                title: 'Coverage Module',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nCover',
                title: 'Coverage Code',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.BeneficiarnCover_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'sClient',
                title: 'Client Code',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'sClientDesc',
                title: 'Client Code',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nRelation',
                title: 'Relationship',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.BeneficiarnRelation_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nRelationDesc',
                title: 'Relationship',
                sortable: true,
                halign: 'center'
            }, {
                field: 'sIrrevoc',
                title: 'Irrevocable Beneficiary',
                formatter: 'tableHelperSupport.IsCheck',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nParticip',
                title: 'Percentage Share',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.BeneficiarnParticip_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }]
        });

        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.$el = table;
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.BeneficiarTblRequest();
      };

    this.BeneficiarRowToInput = function (row) {
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#BeneficiarnModulec', row.nModulec);
        $('#BeneficiarnModulecDesc').val(row.nModulecDesc);
        AutoNumeric.set('#BeneficiarnCover', row.nCover);
        $('#BeneficiarsClient').val(row.sClient);
        $('#BeneficiarsClientDesc').val(row.sClientDesc);
        AutoNumeric.set('#BeneficiarnRelation', row.nRelation);
        $('#BeneficiarnRelationDesc').val(row.nRelationDesc);
        $('#BeneficiarsIrrevoc').prop("checked", row.sIrrevoc);
        AutoNumeric.set('#BeneficiarnParticip', row.nParticip);

    };
    this.BeneficiarTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5ConsultaPolizasClienteSegunClienteIndicadoActions.aspx/BeneficiarTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                BENEFICIARSCERTYPE1: row.SCERTYPE,
                BENEFICIARNBRANCH2: row.nBranch,
                BENEFICIARNPRODUCT3: row.nProduct,
                BENEFICIARNPOLICY4: row.nPolicy,
                BENEFICIARNCERTIF5: row.NCERTIF
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
    this.ClauseTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'nClause',
                title: 'Clause',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.ClausenClause_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nNotenum',
                title: 'Note ID',
                sortable: true,
                halign: 'center',
                align: 'right'
            }]
        });

        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.$el = table;
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.ClauseTblRequest();
      };

    this.ClauseRowToInput = function (row) {
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#ClausenClause', row.nClause);
        $('#ClausenNotenum').val(row.nNotenum);

    };
    this.ClauseTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5ConsultaPolizasClienteSegunClienteIndicadoActions.aspx/ClauseTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                CLAUSESCERTYPE1: row.SCERTYPE,
                CLAUSENBRANCH2: row.nBranch,
                CLAUSENPRODUCT3: row.nProduct,
                CLAUSENPOLICY4: row.nPolicy,
                CLAUSENCERTIF5: row.NCERTIF
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
    this.POLICY_HISTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'NMOVEMENT',
                title: 'Policy Entry Number',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.POLICY_HISNMOVEMENT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NTYPE_HIST',
                title: 'Transaction Type',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.POLICY_HISNTYPE_HIST_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NTYPE_HISTDesc',
                title: 'Transaction Type',
                sortable: true,
                halign: 'center'
            }, {
                field: 'DEFFECDATE',
                title: 'Record Effective Date',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'NTRANSACTIO',
                title: 'Transaction',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.POLICY_HISNTRANSACTIO_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NRECEIPT',
                title: 'Bill Number',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.POLICY_HISNRECEIPT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'SNULL_MOVE',
                title: 'Cancelled Entry',
                formatter: 'tableHelperSupport.IsCheck',
                sortable: true,
                halign: 'center'
            }, {
                field: 'DNULLDATE',
                title: 'Cancellation Date',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }]
        });

        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.$el = table;
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.POLICY_HISTblRequest();
      };

    this.POLICY_HISRowToInput = function (row) {
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#POLICY_HISNMOVEMENT', row.NMOVEMENT);
        AutoNumeric.set('#POLICY_HISNTYPE_HIST', row.NTYPE_HIST);
        $('#POLICY_HISNTYPE_HISTDesc').val(row.NTYPE_HISTDesc);
        $('#POLICY_HISDEFFECDATE').val(generalSupport.ToJavaScriptDateCustom(row.DEFFECDATE, generalSupport.DateFormat()));
        AutoNumeric.set('#POLICY_HISNTRANSACTIO', row.NTRANSACTIO);
        AutoNumeric.set('#POLICY_HISNRECEIPT', row.NRECEIPT);
        $('#POLICY_HISSNULL_MOVE').prop("checked", row.SNULL_MOVE);
        $('#POLICY_HISDNULLDATE').val(generalSupport.ToJavaScriptDateCustom(row.DNULLDATE, generalSupport.DateFormat()));

    };
    this.POLICY_HISTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5ConsultaPolizasClienteSegunClienteIndicadoActions.aspx/POLICY_HISTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                POLICYHISSCERTYPE1: row.SCERTYPE,
                POLICYHISNBRANCH2: row.nBranch,
                POLICYHISNPRODUCT3: row.nProduct,
                POLICYHISNPOLICY4: row.nPolicy,
                POLICYHISNCERTIF5: row.NCERTIF
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
    this.DIR_DEBITTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'NBANKEXT',
                title: 'Bank',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.DIR_DEBITNBANKEXT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NBANKEXTDesc',
                title: 'Bank',
                formatter: 'tableHelperSupport.IsContextMenu',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NTYP_CRECARD',
                title: 'Type',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.DIR_DEBITNTYP_CRECARD_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NTYP_CRECARDDesc',
                title: 'Type',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SCREDI_CARD',
                title: 'Number',
                sortable: true,
                halign: 'center'
            }, {
                field: 'DCARDEXPIR',
                title: 'Expiration Date',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'NPOLICY',
                title: 'Policy ID',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.DIR_DEBITNPOLICY_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }]
        });
        table.bootstrapTable('refreshOptions', {
            contextMenu: '#DIR_DEBITContextMenu',
            contextMenuButton: '.menu-NBANKEXTDesc',
            beforeContextMenuRow: function (e, row, buttonElement) {
                H5ConsultaPolizasClienteSegunClienteIndicadoSupport.DIR_DEBITRowToInput(row);
                if (!buttonElement ) {

                    table.bootstrapTable('showContextMenu', {
                        event: e,
                        contextMenu: '#DIR_DEBITContextMenu',
                        buttonElement: buttonElement
                    });
                }
                if (buttonElement && $(buttonElement).hasClass('menu-NBANKEXTDesc')) {

                    $('#DIR_DEBITTbl').bootstrapTable('showContextMenu', {
                        event: e,
                        contextMenu: '#DIR_DEBIT_DIR_DEBITNBANKEXTDescContextMenu',
                        buttonElement: buttonElement
                    });
                }
                return false;
            },
            onContextMenuItem: function (row, $el) {
                H5ConsultaPolizasClienteSegunClienteIndicadoSupport.DIR_DEBITRowToInput(row);
                switch ($el.data("item")) {
                    case 'DIR_DEBIT_Item1':
                        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.DIR_DEBIT_Item1_Actions(row, null);
                        break;
                    case 'DIR_DEBIT_DIR_DEBITNBANKEXTDesc_Item1':
                        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.DIR_DEBIT_DIR_DEBITNBANKEXTDesc_Item1(row);
                        break;
                }
            }
        });

        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.$el = table;
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.DIR_DEBITTblRequest();
      };

    this.DIR_DEBITRowToInput = function (row) {
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#DIR_DEBITNBANKEXT', row.NBANKEXT);
        $('#DIR_DEBITNBANKEXTDesc').val(row.NBANKEXTDesc);
        AutoNumeric.set('#DIR_DEBITNTYP_CRECARD', row.NTYP_CRECARD);
        $('#DIR_DEBITNTYP_CRECARDDesc').val(row.NTYP_CRECARDDesc);
        $('#DIR_DEBITSCREDI_CARD').val(row.SCREDI_CARD);
        $('#DIR_DEBITDCARDEXPIR').val(generalSupport.ToJavaScriptDateCustom(row.DCARDEXPIR, generalSupport.DateFormat()));
        AutoNumeric.set('#DIR_DEBITNPOLICY', row.NPOLICY);

    };
    this.DIR_DEBITTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5ConsultaPolizasClienteSegunClienteIndicadoActions.aspx/DIR_DEBITTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                DIRDEBITSCERTYPE2: row.SCERTYPE,
                DIRDEBITNBRANCH3: row.nBranch,
                DIRDEBITNPRODUCT4: row.nProduct,
                DIRDEBITNPOLICY5: row.nPolicy,
                DIRDEBITNCERTIF6: row.NCERTIF
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
    this.CertificatTblExpandRow = function (index, row, $detail) {
       var tblparentid = $detail.parent().parent().parent()[0].id;
       var html = [];

        var detailShow = H5ConsultaPolizasClienteSegunClienteIndicadoSupport.Curren_pol_ShowValidation(row);
        if (detailShow)
        html.push('<table id="Curren_polTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Currency Policies</caption></table>');
        html.push('<table id="RolesTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Roles</caption></table>');
        html.push('<table id="SUM_INSURTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Basic Insured Amounts</caption></table>');
        html.push('<table id="CoverTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Coverages</caption></table>');
        html.push('<table id="REINSURANTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Reinsurance Distributions</caption></table>');
        html.push('<table id="REINSURAN2Tbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Reinsurance Distributions</caption></table>');
        html.push('<table id="Disc_xpremTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Discount Extra Premium Tax of Policies</caption></table>');
        html.push('<table id="AutoTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Automobile</caption></table>');
        html.push('<table id="FireTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Fire</caption></table>');
        html.push('<table id="HomeOwnerTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Home Line of Business</caption></table>');
        html.push('<table id="LifeTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Life</caption></table>');
        html.push('<table id="THEFTTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Thefts</caption></table>');
        html.push('<table id="FINANCIAL_INSTRUMENTSTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Finantial Instruments</caption></table>');
        html.push('<table id="HEALTHTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Health</caption></table>');
        html.push('<table id="BeneficiarTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Beneficiaries</caption></table>');
        html.push('<table id="ClauseTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Clauses</caption></table>');
        html.push('<table id="POLICY_HISTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Policy History</caption></table>');
        html.push('<table id="DIR_DEBITTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Automatic Payment Policies</caption></table>');

        $detail.html(html.join(""));
        if (detailShow)
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.Curren_polTblSetup($detail.find('#Curren_polTbl-' + index));
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.RolesTblSetup($detail.find('#RolesTbl-' + index));
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.SUM_INSURTblSetup($detail.find('#SUM_INSURTbl-' + index));
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.CoverTblSetup($detail.find('#CoverTbl-' + index));
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.REINSURANTblSetup($detail.find('#REINSURANTbl-' + index));
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.REINSURAN2TblSetup($detail.find('#REINSURAN2Tbl-' + index));
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.Disc_xpremTblSetup($detail.find('#Disc_xpremTbl-' + index));
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.AutoTblSetup($detail.find('#AutoTbl-' + index));
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.FireTblSetup($detail.find('#FireTbl-' + index));
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.HomeOwnerTblSetup($detail.find('#HomeOwnerTbl-' + index));
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.LifeTblSetup($detail.find('#LifeTbl-' + index));
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.THEFTTblSetup($detail.find('#THEFTTbl-' + index));
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.FINANCIAL_INSTRUMENTSTblSetup($detail.find('#FINANCIAL_INSTRUMENTSTbl-' + index));
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.HEALTHTblSetup($detail.find('#HEALTHTbl-' + index));
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.BeneficiarTblSetup($detail.find('#BeneficiarTbl-' + index));
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.ClauseTblSetup($detail.find('#ClauseTbl-' + index));
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.POLICY_HISTblSetup($detail.find('#POLICY_HISTbl-' + index));
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.DIR_DEBITTblSetup($detail.find('#DIR_DEBITTbl-' + index));

    };
    this.CertificatTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            search: true,
            detailView: true,
            onExpandRow: H5ConsultaPolizasClienteSegunClienteIndicadoSupport.CertificatTblExpandRow,

            columns: [{
                field: 'nBranch',
                title: 'Line of Business',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.CertificatnBranch_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nBranchDesc',
                title: 'Line of Business',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'nProduct',
                title: 'Product',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.CertificatnProduct_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nProductDesc',
                title: 'Product',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nPolicy',
                title: 'Policy ID',
                formatter: 'tableHelperSupport.IsContextMenu',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nCapital',
                title: 'Insured Amount',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.CertificatnCapital_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nPayfreq',
                title: 'Payment Frequency',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.CertificatnPayfreq_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nPayfreqDesc',
                title: 'Payment Frequency',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'sStatusva',
                title: 'Status of Policy Certificate',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'sStatusvaDesc',
                title: 'Status of Policy Certificate',
                sortable: true,
                halign: 'center'
            }, {
                field: 'dIssuedat',
                title: 'Issue Date',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'nWait_code',
                title: 'Reason for Outstanding Status',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.CertificatnWait_code_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nWait_codeDesc',
                title: 'Reason for Outstanding Status',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'dStartdate',
                title: 'Effective Date',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center',
                visible: false
            }, {
                field: 'sRenewal',
                title: 'Automatic Renewal',
                formatter: 'tableHelperSupport.IsCheck',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'dNulldate',
                title: 'Cancellation Date',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center',
                visible: false
            }, {
                field: 'nNullcode',
                title: 'Cancellation Code',
                formatter: 'H5ConsultaPolizasClienteSegunClienteIndicadoSupport.CertificatnNullcode_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nNullcodeDesc',
                title: 'Cancellation Code',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'dExpirdat',
                title: 'Ending Date',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center',
                visible: false
            }, {
                field: 'dChangdat',
                title: 'Last Modification Date',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center',
                visible: false
            }, {
                field: 'SCERTYPE',
                title: 'RecordType',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'NCERTIF',
                title: 'CertificateID',
                sortable: true,
                halign: 'center',
                visible: false
            }]
        });
        table.bootstrapTable('refreshOptions', {
            contextMenu: '#CertificatContextMenu',
            contextMenuButton: '.menu-nPolicy',
            beforeContextMenuRow: function (e, row, buttonElement) {
                H5ConsultaPolizasClienteSegunClienteIndicadoSupport.CertificatRowToInput(row);
                if (!buttonElement ) {

                    table.bootstrapTable('showContextMenu', {
                        event: e,
                        contextMenu: '#CertificatContextMenu',
                        buttonElement: buttonElement
                    });
                }
                if (buttonElement && $(buttonElement).hasClass('menu-nPolicy')) {

                    $('#CertificatTbl').bootstrapTable('showContextMenu', {
                        event: e,
                        contextMenu: '#Certificat_CertificatnPolicyContextMenu',
                        buttonElement: buttonElement
                    });
                }
                return false;
            },
            onContextMenuItem: function (row, $el) {
                H5ConsultaPolizasClienteSegunClienteIndicadoSupport.CertificatRowToInput(row);
                switch ($el.data("item")) {
                    case 'Certificat_Item1':
                        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.Certificat_Item1_Actions(row, null);
                        break;
                    case 'Certificat_Item2':
                        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.Certificat_Item2_Actions(row, null);
                        break;
                    case 'Certificat_CertificatnPolicy_Item1':
                        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.Certificat_CertificatnPolicy_Item1(row);
                        break;
                    case 'Certificat_CertificatnPolicy_Item2':
                        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.Certificat_CertificatnPolicy_Item2(row);
                        break;
                }
            }
        });

        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.$el = table;
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.CertificatTblRequest();
      };

    this.CertificatRowToInput = function (row) {
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#CertificatnBranch', row.nBranch);
        $('#CertificatnBranchDesc').val(row.nBranchDesc);
        AutoNumeric.set('#CertificatnProduct', row.nProduct);
        $('#CertificatnProductDesc').val(row.nProductDesc);
        $('#CertificatnPolicy').val(row.nPolicy);
        AutoNumeric.set('#CertificatnCapital', row.nCapital);
        AutoNumeric.set('#CertificatnPayfreq', row.nPayfreq);
        $('#CertificatnPayfreqDesc').val(row.nPayfreqDesc);
        $('#CertificatsStatusva').val(row.sStatusva);
        $('#CertificatsStatusvaDesc').val(row.sStatusvaDesc);
        $('#CertificatdIssuedat').val(generalSupport.ToJavaScriptDateCustom(row.dIssuedat, generalSupport.DateFormat()));
        AutoNumeric.set('#CertificatnWait_code', row.nWait_code);
        $('#CertificatnWait_codeDesc').val(row.nWait_codeDesc);
        $('#CertificatdStartdate').val(generalSupport.ToJavaScriptDateCustom(row.dStartdate, generalSupport.DateFormat()));
        $('#CertificatsRenewal').prop("checked", row.sRenewal);
        $('#CertificatdNulldate').val(generalSupport.ToJavaScriptDateCustom(row.dNulldate, generalSupport.DateFormat()));
        AutoNumeric.set('#CertificatnNullcode', row.nNullcode);
        $('#CertificatnNullcodeDesc').val(row.nNullcodeDesc);
        $('#CertificatdExpirdat').val(generalSupport.ToJavaScriptDateCustom(row.dExpirdat, generalSupport.DateFormat()));
        $('#CertificatdChangdat').val(generalSupport.ToJavaScriptDateCustom(row.dChangdat, generalSupport.DateFormat()));
        $('#CertificatSCERTYPE').val(row.SCERTYPE);
        $('#CertificatNCERTIF').val(row.NCERTIF);

    };
    this.CertificatTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5ConsultaPolizasClienteSegunClienteIndicadoActions.aspx/CertificatTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                filter: '',
                CERTIFICATSCLIENT1: row.SCLIENT,
                CERTIFICATSCLIENT5: row.SCLIENT
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

        var detailShow = H5ConsultaPolizasClienteSegunClienteIndicadoSupport.Certificat_ShowValidation(row);
        if (detailShow)
        html.push('<table id="CertificatTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Policies</caption></table>');

        $detail.html(html.join(""));
        if (detailShow)
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.CertificatTblSetup($detail.find('#CertificatTbl-' + index));

    };


    this.CertificatnBranch_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.CertificatnProduct_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.CertificatnCapital_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.CertificatnPayfreq_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.CertificatnWait_code_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.CertificatnNullcode_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.Curren_polnCurrency_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.RolesnRole_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.RolesnStatusRol_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.RolesnTypeRisk_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99,
            decimalPlaces: 0,
            minimumValue: -99
        });
      };
    this.RolesnRating_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.SUM_INSURNSUMINS_COD_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.SUM_INSURNSUMINS_REAL_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.SUM_INSURNCOINSURAN_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 2,
            minimumValue: -99999
        });
      };
    this.SUM_INSURNSUM_INSUR_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.CovernModulec_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.CovernCover_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.CovernRole_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.CovernCurrency_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.CovernCapital_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 0,
            minimumValue: -999999999999999999
        });
      };
    this.CovernPremium_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.REINSURANNBRANCH_REI_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.REINSURANNTYPE_REIN_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.REINSURANNCAPITAL_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.REINSURANNCURRENCY_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.REINSURANNSHARE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999,
            decimalPlaces: 6,
            minimumValue: -999999999
        });
      };
    this.REINSURAN2NBRANCH_REI_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.REINSURAN2NCOMPANY_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.REINSURAN2NCAPITAL_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.REINSURAN2NCURRENCY_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.REINSURAN2NSHARE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999,
            decimalPlaces: 6,
            minimumValue: -999999999
        });
      };
    this.REINSURAN2NCOMMISSI_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999,
            decimalPlaces: 2,
            minimumValue: -9999
        });
      };
    this.Disc_xpremnDisc_code_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.Disc_xpremnCause_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.Disc_xpremnPercent_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 2,
            minimumValue: -99999
        });
      };
    this.Disc_xpremnCurrency_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.Disc_xpremnAmount_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.AutonVehType_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.AutonCapital_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.AutonVeh_valor_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.AutonYear_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.AutonAutoZone_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.AutonUse_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.FirenConstCat_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.FirenActivityCat_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.FirenFloor_quan_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.FirenRoofType_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.FirenSeismicZone_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.FirenBuildType_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.FirenSpCombType_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.FirenHurrican_zone_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.FirenSideCloseType_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.HomeOwnernOwnerShip_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.HomeOwnernDwellingType_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.HomeOwnerNSEISMICZONE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.HomeOwnernYear_built_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999,
            decimalPlaces: 0,
            minimumValue: -9999
        });
      };
    this.HomeOwnernPrice_purch_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.HomeOwnernCurrency_purch_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.HomeOwnernLandSuper_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999999,
            decimalPlaces: 2,
            minimumValue: -99999999
        });
      };
    this.HomeOwnernHomeSuper_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999999,
            decimalPlaces: 2,
            minimumValue: -99999999
        });
      };
    this.HomeOwnernRoofType_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.HomeOwnernRoofYear_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999,
            decimalPlaces: 0,
            minimumValue: -9999
        });
      };
    this.HomeOwnernFloodZone_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99,
            decimalPlaces: 0,
            minimumValue: -99
        });
      };
    this.HomeOwnernFoundType_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.HomeOwnernAirType_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.HomeOwnernStories_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.HomeOwnernHalfBath_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.HomeOwnernFullBath_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.HomeOwnernBedrooms_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.HomeOwnernFirePlace_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.HomeOwnernGarage_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.HomeOwnernDist_Fire_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999,
            decimalPlaces: 2,
            minimumValue: -999999
        });
      };
    this.HomeOwnernSwimPool_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.HomeOwnernFenceHeight_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.HomeOwnernCap_other_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: -999999999999
        });
      };
    this.HomeOwnernCurrency_other_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.LifenTypDurPay_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.LifenPay_time_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.LifenTypDurIns_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.LifenInsur_time_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.LifenXprem_time_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.LifenAge_limit_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.LifeNAGE_REINSU_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.LifeNAGE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.THEFTNCOMMERGRP_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999,
            decimalPlaces: 0,
            minimumValue: -999
        });
      };
    this.THEFTNINSURED_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 2,
            minimumValue: -99999
        });
      };
    this.THEFTNEMPLOYEES_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.THEFTNAREA_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.THEFTNVIGILANCE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.THEFTNCAPITAL_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: -999999999999
        });
      };
    this.THEFTNNULLCODE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.FINANCIAL_INSTRUMENTSNCONSECUTIVE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.FINANCIAL_INSTRUMENTSNINSTRUMENT_TY_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.FINANCIAL_INSTRUMENTSNBANK_CODE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.FINANCIAL_INSTRUMENTSNCARD_TYPE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.FINANCIAL_INSTRUMENTSNQUOTA_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.FINANCIAL_INSTRUMENTSNAMOUNT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.FINANCIAL_INSTRUMENTSNCURRENCY_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.HEALTHNCAPITAL_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999,
            decimalPlaces: 0,
            minimumValue: -999999999999
        });
      };
    this.HEALTHNPREMIUM_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 2,
            minimumValue: -9999999999
        });
      };
    this.ROLES2NROLE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.BeneficiarnModulec_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.BeneficiarnCover_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.BeneficiarnRelation_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.BeneficiarnParticip_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 2,
            minimumValue: -99999
        });
      };
    this.ClausenClause_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.POLICY_HISNMOVEMENT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.POLICY_HISNTYPE_HIST_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.POLICY_HISNTRANSACTIO_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.POLICY_HISNRECEIPT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.DIR_DEBITNBANKEXT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.DIR_DEBITNTYP_CRECARD_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.DIR_DEBITNPOLICY_FormatterMaskData = function (value, row, index) {          
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
        masterSupport.setPageTitle('Pólizas de un Cliente');
        

    H5ConsultaPolizasClienteSegunClienteIndicadoSupport.ControlBehaviour();
    H5ConsultaPolizasClienteSegunClienteIndicadoSupport.ControlActions();
    H5ConsultaPolizasClienteSegunClienteIndicadoSupport.ValidateSetup();

    $('#RecordEffectiveDate').val(generalSupport.URLDateValue('RecordEffectiveDate'));

    $("#ItemsTblPlaceHolder").replaceWith('<table id="ItemsTbl"><caption >Clients</caption></table>');
    H5ConsultaPolizasClienteSegunClienteIndicadoSupport.ItemsTblSetup($('#ItemsTbl'));

    $('#RecordEffectiveDate').val(moment().format(generalSupport.DateFormat()));
        H5ConsultaPolizasClienteSegunClienteIndicadoSupport.ItemsTblRequest();



});

