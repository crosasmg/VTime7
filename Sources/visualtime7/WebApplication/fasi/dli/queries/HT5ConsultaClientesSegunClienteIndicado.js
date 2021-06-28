var HT5ConsultaClientesSegunClienteIndicadoSupport = new function () {

    this.currentRow = {};
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#HT5ConsultaClientesSegunClienteIndicadoFormId').val(),
            Client: ($('#Client').data('code') !== undefined) ? $('#Client').data('code') : '',
            RecordEffectiveDate: generalSupport.DatePickerValue('#RecordEffectiveDate')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        $('#HT5ConsultaClientesSegunClienteIndicadoFormId').val(data.InstanceFormId);
        $('#Client').data('code', data.Client);
        clientSupport.CompleteClientName('#Client', data.Client);
        $('#RecordEffectiveDate').val(generalSupport.ToJavaScriptDateCustom(data.RecordEffectiveDate, generalSupport.DateFormat()));

        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForROLESnRole(source);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForROLESnStatusRol(source);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForROLESsSexClien(source);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForROLESnTypeRisk(source);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCOVERnModulec(source);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCOVERnCover(source);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCOVERnRole(source);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCOVERnCurrency(source);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForBENEFICIARnModulec(source);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForBENEFICIARnRelation(source);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCLAIM_CASEnDeman_type(source);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCLAIM_CASEsStaCase(source);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCL_COVERnDeman_type(source);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCL_COVERnCurrency(source);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCL_COVERsFrantype(source);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCLAIM_ATTMnDeman_type(source);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCLAIM_ATTMsIllness(source);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCLAIM_HISnOper_type(source);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCLAIM_HISnPay_type(source);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCLAIM_HISnPay_form(source);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCLAIM_HISnCurrency(source);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForRELATIONSnRelaship(source);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCHEQUESSREQUEST_TY(source);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCHEQUESNCURRENCYPAY(source);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCHEQUESNCONCEPT(source);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCHEQUESNSTA_CHEQUE(source);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCHEQUESNOFFICE(source);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCHEQUESNOFFICEAGEN(source);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCHEQUESNTYPESUPPORT(source);

        if (data.Curren_pol_Curren_pol !== null)
            $('#Curren_polTbl').bootstrapTable('load', data.Curren_pol_Curren_pol);
        if (data.ROLES_ROLES !== null)
            $('#ROLESTbl').bootstrapTable('load', data.ROLES_ROLES);
        if (data.SUM_INSUR_SUM_INSUR !== null)
            $('#SUM_INSURTbl').bootstrapTable('load', data.SUM_INSUR_SUM_INSUR);
        if (data.COVER_COVER !== null)
            $('#COVERTbl').bootstrapTable('load', data.COVER_COVER);
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
        if (data.BENEFICIAR_BENEFICIAR !== null)
            $('#BENEFICIARTbl').bootstrapTable('load', data.BENEFICIAR_BENEFICIAR);
        if (data.Clause_Clause !== null)
            $('#ClauseTbl').bootstrapTable('load', data.Clause_Clause);
        if (data.POLICY_HIS_POLICY_HIS !== null)
            $('#POLICY_HISTbl').bootstrapTable('load', data.POLICY_HIS_POLICY_HIS);
        if (data.Certificat_Certificat !== null)
            $('#CertificatTbl').bootstrapTable('load', data.Certificat_Certificat);
        if (data.Detail_pre_Detail_pre !== null)
            $('#Detail_preTbl').bootstrapTable('load', data.Detail_pre_Detail_pre);
        if (data.Commiss_pr_Commiss_pr !== null)
            $('#Commiss_prTbl').bootstrapTable('load', data.Commiss_pr_Commiss_pr);
        if (data.Premium_mo_Premium_mo !== null)
            $('#Premium_moTbl').bootstrapTable('load', data.Premium_mo_Premium_mo);
        if (data.FINANC_DRA_FINANC_DRA !== null)
            $('#FINANC_DRATbl').bootstrapTable('load', data.FINANC_DRA_FINANC_DRA);
        if (data.FINANCE_CO_FINANCE_CO !== null)
            $('#FINANCE_COTbl').bootstrapTable('load', data.FINANCE_CO_FINANCE_CO);
        if (data.Premium_Premium !== null)
            $('#PremiumTbl').bootstrapTable('load', data.Premium_Premium);
        if (data.CL_COVER_CL_COVER !== null)
            $('#CL_COVERTbl').bootstrapTable('load', data.CL_COVER_CL_COVER);
        if (data.CLAIM_ATTM_CLAIM_ATTM !== null)
            $('#CLAIM_ATTMTbl').bootstrapTable('load', data.CLAIM_ATTM_CLAIM_ATTM);
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
        if (data.CLAIM_HIS_CLAIM_HIS !== null)
            $('#CLAIM_HISTbl').bootstrapTable('load', data.CLAIM_HIS_CLAIM_HIS);
        if (data.CLAIM_CASE_CLAIM_CASE !== null)
            $('#CLAIM_CASETbl').bootstrapTable('load', data.CLAIM_CASE_CLAIM_CASE);
        if (data.Claim_Claim !== null)
            $('#ClaimTbl').bootstrapTable('load', data.Claim_Claim);
        if (data.PHONES_PHONES !== null)
            $('#PHONESTbl').bootstrapTable('load', data.PHONES_PHONES);
        if (data.Address_Address !== null)
            $('#AddressTbl').bootstrapTable('load', data.Address_Address);
        if (data.CLIDOCUMENTS_CLIDOCUMENTS !== null)
            $('#CLIDOCUMENTSTbl').bootstrapTable('load', data.CLIDOCUMENTS_CLIDOCUMENTS);
        if (data.DIR_DEBIT_CLI_DIR_DEBIT_CLI !== null)
            $('#DIR_DEBIT_CLITbl').bootstrapTable('load', data.DIR_DEBIT_CLI_DIR_DEBIT_CLI);
        if (data.Bk_account_Bk_account !== null)
            $('#Bk_accountTbl').bootstrapTable('load', data.Bk_account_Bk_account);
        if (data.Cred_card_Cred_card !== null)
            $('#Cred_cardTbl').bootstrapTable('load', data.Cred_card_Cred_card);
        if (data.Curr_acc_Curr_acc !== null)
            $('#Curr_accTbl').bootstrapTable('load', data.Curr_acc_Curr_acc);
        if (data.Sport_Sport !== null)
            $('#SportTbl').bootstrapTable('load', data.Sport_Sport);
        if (data.Hobby_Hobby !== null)
            $('#HobbyTbl').bootstrapTable('load', data.Hobby_Hobby);
        if (data.Financ_cli_Financ_cli !== null)
            $('#Financ_cliTbl').bootstrapTable('load', data.Financ_cli_Financ_cli);
        if (data.RELATIONS_RELATIONS !== null)
            $('#RELATIONSTbl').bootstrapTable('load', data.RELATIONS_RELATIONS);
        if (data.CHEQUES_CHEQUES !== null)
            $('#CHEQUESTbl').bootstrapTable('load', data.CHEQUES_CHEQUES);
        HT5ConsultaClientesSegunClienteIndicadoSupport.ItemsTblRequest();
        if (data.Items_Item !== null)
            $('#ItemsTbl').bootstrapTable('load', data.Items_Item);

    };

    this.ControlBehaviour = function () {





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
                HT5ConsultaClientesSegunClienteIndicadoSupport.ObjectToInput(data.d.Data, source);
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };



    this.Items_Item1_Actions = function (row, $modal) {
    generalSupport.CallBackOfficePage('BC003_K', '&tctClient='+ row.SCLIENT +'&tctClient_Digit='+ row.SDIGIT +'');

    };
    this.Certificat_ShowValidation = function (row) {
            var returnData;
            var countData;
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/CertificatSelectCommandActionCERTIFICAT",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 CERTIFICATSCLIENT1: row.SCLIENT,
                CERTIFICATSCLIENT5: row.SCLIENT }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
        return returnData;
    };
    this.Curren_pol_ShowValidation = function (row) {
            var returnData;
            var countData;
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/Curren_polSelectCommandActionCURREN_POL",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 CURRENPOLSCERTYPE1: row.SCERTYPE,
                CURRENPOLNBRANCH2: row.nBranch,
                CURRENPOLNPRODUCT3: row.nProduct,
                CURRENPOLNPOLICY4: row.nPolicy,
                CURRENPOLNCERTIF5: row.NCERTIF }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
        return returnData;
    };
    this.ROLES_ShowValidation = function (row) {
            var returnData;
            var countData;
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/ROLESSelectCommandActionROLES",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 ROLESSCERTYPE1: row.SCERTYPE,
                ROLESNBRANCH2: row.nBranch,
                ROLESNPRODUCT3: row.nProduct,
                ROLESNPOLICY4: row.nPolicy,
                ROLESNCERTIF5: row.NCERTIF }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
        return returnData;
    };
    this.SUM_INSUR_ShowValidation = function (row) {
            var returnData;
            var countData;
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/SUM_INSURSelectCommandActionSUM_INSUR",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 SUMINSURSCERTYPE1: row.SCERTYPE,
                SUMINSURNBRANCH2: row.nBranch,
                SUMINSURNPRODUCT3: row.nProduct,
                SUMINSURNPOLICY4: row.nPolicy,
                SUMINSURNCERTIF5: row.NCERTIF }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
        return returnData;
    };
    this.COVER_ShowValidation = function (row) {
            var returnData;
            var countData;
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/COVERSelectCommandActionCOVER",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 COVERSCERTYPE1: row.SCERTYPE,
                COVERNBRANCH2: row.nBranch,
                COVERNPRODUCT3: row.nProduct,
                COVERNPOLICY4: row.nPolicy,
                COVERNCERTIF5: row.NCERTIF }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
        return returnData;
    };
    this.REINSURAN_ShowValidation = function (row) {
            var returnData;
            var countData;
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/REINSURANSelectCommandActionREINSURAN",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 REINSURANSCERTYPE3: row.SCERTYPE,
                REINSURANNBRANCH4: row.nBranch,
                REINSURANNPRODUCT5: row.nProduct,
                REINSURANNPOLICY6: row.nPolicy,
                REINSURANNCERTIF7: row.NCERTIF }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
        return returnData;
    };
    this.REINSURAN2_ShowValidation = function (row) {
            var returnData;
            var countData;
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/REINSURAN2SelectCommandActionREINSURAN",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 REINSURANSCERTYPE3: row.SCERTYPE,
                REINSURANNBRANCH4: row.nBranch,
                REINSURANNPRODUCT5: row.nProduct,
                REINSURANNPOLICY6: row.nPolicy,
                REINSURANNCERTIF7: row.NCERTIF }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
        return returnData;
    };
    this.Disc_xprem_ShowValidation = function (row) {
            var returnData;
            var countData;
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/Disc_xpremSelectCommandActionDISC_XPREM",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 DISCXPREMSCERTYPE1: row.SCERTYPE,
                DISCXPREMNBRANCH2: row.nBranch,
                DISCXPREMNPRODUCT3: row.nProduct,
                DISCXPREMNPOLICY4: row.nPolicy,
                DISCXPREMNCERTIF5: row.NCERTIF }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
        return returnData;
    };
    this.Auto_ShowValidation = function (row) {
            var returnData;
            var countData;
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/AutoSelectCommandActionAUTO",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 AUTOSCERTYPE1: row.SCERTYPE,
                AUTONBRANCH2: row.nBranch,
                AUTONPRODUCT3: row.nProduct,
                AUTONPOLICY4: row.nPolicy,
                AUTONCERTIF5: row.NCERTIF }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
        return returnData;
    };
    this.Fire_ShowValidation = function (row) {
            var returnData;
            var countData;
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/FireSelectCommandActionFIRE",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 FIRESCERTYPE1: row.SCERTYPE,
                FIRENBRANCH2: row.nBranch,
                FIRENPRODUCT3: row.nProduct,
                FIRENPOLICY4: row.nPolicy,
                FIRENCERTIF5: row.NCERTIF }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
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
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/HomeOwnerSelectCommandActionHOMEOWNER",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 HOMEOWNERSCERTYPE1: row.SCERTYPE,
                HOMEOWNERNBRANCH2: row.nBranch,
                HOMEOWNERNPRODUCT3: row.nProduct,
                HOMEOWNERNPOLICY4: row.nPolicy,
                HOMEOWNERNCERTIF5: row.NCERTIF }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
        return returnData;
    };
    this.Life_ShowValidation = function (row) {
            var returnData;
            var countData;
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/LifeSelectCommandActionLIFE",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 LIFESCERTYPE1: row.SCERTYPE,
                LIFENBRANCH2: row.nBranch,
                LIFENPRODUCT3: row.nProduct,
                LIFENPOLICY4: row.nPolicy,
                LIFENCERTIF5: row.NCERTIF }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
        return returnData;
    };
    this.THEFT_ShowValidation = function (row) {
            var returnData;
            var countData;
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/THEFTSelectCommandActionTHEFT",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 THEFTSCERTYPE1: row.SCERTYPE,
                THEFTNBRANCH2: row.nBranch,
                THEFTNPRODUCT3: row.nProduct,
                THEFTNPOLICY4: row.nPolicy,
                THEFTNCERTIF5: row.NCERTIF }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
        return returnData;
    };
    this.FINANCIAL_INSTRUMENTS_ShowValidation = function (row) {
            var returnData;
            var countData;
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/FINANCIAL_INSTRUMENTSSelectCommandActionFINANCIAL_INSTRUMENTS",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 FINANCIALINSTRUMENTSSCERTYPE1: row.SCERTYPE,
                FINANCIALINSTRUMENTSNBRANCH2: row.nBranch,
                FINANCIALINSTRUMENTSNPRODUCT3: row.nProduct,
                FINANCIALINSTRUMENTSNPOLICY4: row.nPolicy,
                FINANCIALINSTRUMENTSNCERTIF5: row.NCERTIF }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
        return returnData;
    };
    this.HEALTH_ShowValidation = function (row) {
            var returnData;
            var countData;
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/HEALTHSelectCommandActionHEALTH",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 HEALTHSCERTYPE1: row.SCERTYPE,
                HEALTHNBRANCH2: row.nBranch,
                HEALTHNPRODUCT3: row.nProduct,
                HEALTHNPOLICY4: row.nPolicy,
                HEALTHNCERTIF5: row.NCERTIF }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
        return returnData;
    };
    this.ROLES2_ShowValidation = function (row) {
            var returnData;
            var countData;
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/ROLES2SelectCommandActionROLES",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 ROLESSCERTYPE3: row.SCERTYPE,
                ROLESNBRANCH4: row.NBRANCH,
                ROLESNPRODUCT5: row.NPRODUCT,
                ROLESNPOLICY6: row.NPOLICY,
                ROLESNCERTIF7: row.NCERTIF }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
        return returnData;
    };
    this.ROLES2_Item1_Actions = function (row, $modal) {
    generalSupport.CallBackOfficePage('', '&PLineOfBusinessId='+ ROLESNBRANCH +'&PProductId='+ ROLESNPRODUCT +'&PPolicyId='+ ROLESNPOLICY +'&PCertificateId='+ ROLESNCERTIF +'&PInsuredAffected=');

    };
    this.BENEFICIAR_ShowValidation = function (row) {
            var returnData;
            var countData;
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/BENEFICIARSelectCommandActionBENEFICIAR",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 BENEFICIARSCERTYPE1: row.SCERTYPE,
                BENEFICIARNBRANCH2: row.nBranch,
                BENEFICIARNPRODUCT3: row.nProduct,
                BENEFICIARNPOLICY4: row.nPolicy,
                BENEFICIARNCERTIF5: row.NCERTIF }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
        return returnData;
    };
    this.Clause_ShowValidation = function (row) {
            var returnData;
            var countData;
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/ClauseSelectCommandActionCLAUSE",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 CLAUSESCERTYPE1: row.SCERTYPE,
                CLAUSENBRANCH2: row.nBranch,
                CLAUSENPRODUCT3: row.nProduct,
                CLAUSENPOLICY4: row.nPolicy,
                CLAUSENCERTIF5: row.NCERTIF }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
        return returnData;
    };
    this.POLICY_HIS_ShowValidation = function (row) {
            var returnData;
            var countData;
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/POLICY_HISSelectCommandActionPOLICY_HIS",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 POLICYHISSCERTYPE1: row.SCERTYPE,
                POLICYHISNBRANCH2: row.nBranch,
                POLICYHISNPRODUCT3: row.nProduct,
                POLICYHISNPOLICY4: row.nPolicy,
                POLICYHISNCERTIF5: row.NCERTIF }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
        return returnData;
    };
    this.Certificat_Item1_Actions = function (row, $modal) {
    window.open('/fasi/dli/forms/NNClaimDeclarationDemoPopup.html?Policy='+ row.nPolicy +'','_blank','scrollbars=no,resizable=no,toolbar=no,location=no,directories=no,status=yes,menubar=no,copyhistory=no,width=0,height=0,left=0,top=0');

    };
    this.Certificat_Item2_Actions = function (row, $modal) {
    window.open('/fasi/dli/forms/RequestOfPrintingPolicyPopup.html?LineOfBusinessToPrint='+ row.nBranch +'&ProductToPrint='+ row.nProduct +'&PolicyToPrint='+ row.nPolicy +'&CertificateToPrint='+ row.NCERTIF +'&PProcessDate='+ generalSupport.ToJavaScriptDateCustom(row.dChangdat, generalSupport.DateFormat())  +'','_blank','scrollbars=no,resizable=no,toolbar=no,location=no,directories=no,status=yes,menubar=no,copyhistory=no,width=0,height=0,left=0,top=0');

    };
    this.Premium_ShowValidation = function (row) {
            var returnData;
            var countData;
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/PremiumSelectCommandActionPREMIUM",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 PREMIUMSCLIENT3: row.SCLIENT }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
        return returnData;
    };
    this.Detail_pre_ShowValidation = function (row) {
            var returnData;
            var countData;
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/Detail_preSelectCommandActionDETAIL_PRE",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 DETAILPRESCERTYPE1: row.SCERTYPE,
                DETAILPRENBRANCH2: row.nBranch,
                DETAILPRENPRODUCT3: row.nProduct,
                DETAILPRENRECEIPT4: row.nReceipt,
                DETAILPRENDIGIT5: row.NDIGIT,
                DETAILPRENPAYNUMBE6: row.NPAYNUMBE }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
        return returnData;
    };
    this.Commiss_pr_ShowValidation = function (row) {
            var returnData;
            var countData;
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/Commiss_prSelectCommandActionCOMMISS_PR",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 COMMISSPRSCERTYPE1: row.SCERTYPE,
                COMMISSPRNBRANCH2: row.nBranch,
                COMMISSPRNPRODUCT3: row.nProduct,
                COMMISSPRNRECEIPT4: row.nReceipt,
                COMMISSPRNDIGIT5: row.NDIGIT,
                COMMISSPRNPAYNUMBE6: row.NPAYNUMBE }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
        return returnData;
    };
    this.Premium_mo_ShowValidation = function (row) {
            var returnData;
            var countData;
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/Premium_moSelectCommandActionPREMIUM_MO",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 PREMIUMMOSCERTYPE1: row.SCERTYPE,
                PREMIUMMONBRANCH2: row.nBranch,
                PREMIUMMONPRODUCT3: row.nProduct,
                PREMIUMMONRECEIPT4: row.nReceipt,
                PREMIUMMONDIGIT5: row.NDIGIT,
                PREMIUMMONPAYNUMBE6: row.NPAYNUMBE }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
        return returnData;
    };
    this.FINANCE_CO_ShowValidation = function (row) {
            var returnData;
            var countData;
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/FINANCE_COSelectCommandActionFINANCE_CO",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 FINANCECONCONTRAT1: row.NCONTRAT,
                FINANCECODEFFECDATE2: row.DEFFECDATE }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
        return returnData;
    };
    this.FINANC_DRA_ShowValidation = function (row) {
            var returnData;
            var countData;
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/FINANC_DRASelectCommandActionFINANC_DRA",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 FINANCDRANCONTRAT1: row.NCONTRAT }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
        return returnData;
    };
    this.Claim_ShowValidation = function (row) {
            var returnData;
            var countData;
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/ClaimSelectCommandActionCLAIM",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 CLAIMSCLIENT1: row.SCLIENT }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
        return returnData;
    };
    this.CLAIM_CASE_ShowValidation = function (row) {
            var returnData;
            var countData;
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/CLAIM_CASESelectCommandActionCLAIM_CASE",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 CLAIMCASENCLAIM1: row.nClaim }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
        return returnData;
    };
    this.CL_COVER_ShowValidation = function (row) {
            var returnData;
            var countData;
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/CL_COVERSelectCommandActionCL_COVER",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 CLCOVERNCLAIM1: row.NCLAIM,
                CLCOVERNCASENUM2: row.nCase_num,
                CLCOVERNDEMANTYPE3: row.nDeman_type }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
        return returnData;
    };
    this.CLAIM_ATTM_ShowValidation = function (row) {
            var returnData;
            var countData;
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/CLAIM_ATTMSelectCommandActionCLAIM_ATTM",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 CLAIMATTMNCLAIM1: row.NCLAIM,
                CLAIMATTMNCASENUM2: row.nCase_num,
                CLAIMATTMNDEMANTYPE3: row.nDeman_type }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
        return returnData;
    };
    this.Claim_auto_ShowValidation = function (row) {
            var returnData;
            var countData;
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/Claim_autoSelectCommandActionCLAIM_AUTO",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 CLAIMAUTONCLAIM1: row.NCLAIM,
                CLAIMAUTONCASENUM2: row.nCase_num,
                CLAIMAUTONDEMANTYPE3: row.nDeman_type }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
        return returnData;
    };
    this.CLAIM_DAMA_ShowValidation = function (row) {
            var returnData;
            var countData;
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/CLAIM_DAMASelectCommandActionCLAIM_DAMA",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 CLAIMDAMANCLAIM1: row.NCLAIM,
                CLAIMDAMANCASENUM2: row.nCase_num,
                CLAIMDAMANDEMANTYPE3: row.nDeman_type }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
        return returnData;
    };
    this.CLAIM_THIR_ShowValidation = function (row) {
            var returnData;
            var countData;
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/CLAIM_THIRSelectCommandActionCLAIM_THIR",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 CLAIMTHIRNCLAIM1: row.NCLAIM,
                CLAIMTHIRNCASENUM2: row.nCase_num,
                CLAIMTHIRNDEMANTYPE3: row.nDeman_type }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
        return returnData;
    };
    this.Life_claim_ShowValidation = function (row) {
            var returnData;
            var countData;
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/Life_claimSelectCommandActionLIFE_CLAIM",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 LIFECLAIMNCLAIM1: row.NCLAIM,
                LIFECLAIMNCASENUM2: row.nCase_num,
                LIFECLAIMNDEMANTYPE3: row.nDeman_type }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
        return returnData;
    };
    this.CLAIMBENEF_ShowValidation = function (row) {
            var returnData;
            var countData;
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/CLAIMBENEFSelectCommandActionCLAIMBENEF",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 CLAIMBENEFNCLAIM1: row.NCLAIM,
                CLAIMBENEFNCASENUM2: row.nCase_num,
                CLAIMBENEFNDEMANTYPE3: row.nDeman_type }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
        return returnData;
    };
    this.CLAIM_HIS_ShowValidation = function (row) {
            var returnData;
            var countData;
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/CLAIM_HISSelectCommandActionCLAIM_HIS",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 CLAIMHISNCLAIM1: row.NCLAIM,
                CLAIMHISNCASENUM2: row.nCase_num,
                CLAIMHISNDEMANTYPE3: row.nDeman_type }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
        return returnData;
    };
    this.Address_ShowValidation = function (row) {
            var returnData;
            var countData;
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/AddressSelectCommandActionADDRESS",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 ADDRESSSCLIENT1: row.SCLIENT }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
        return returnData;
    };
    this.PHONES_ShowValidation = function (row) {
            var returnData;
            var countData;
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/PHONESSelectCommandActionPHONES",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 PHONESNRECOWNER1: row.NRECOWNER,
                PHONESSKEYADDRESS2: row.SKEYADDRESS,
                PHONESDEFFECDATE3: row.DEFFECDATE }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
        return returnData;
    };
    this.CLIDOCUMENTS_ShowValidation = function (row) {
            var returnData;
            var countData;
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/CLIDOCUMENTSSelectCommandActionCLIDOCUMENTS",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 CLIDOCUMENTSSCLIENT1: row.SCLIENT }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
        return returnData;
    };
    this.DIR_DEBIT_CLI_ShowValidation = function (row) {
            var returnData;
            var countData;
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/DIR_DEBIT_CLISelectCommandActionDIR_DEBIT_CLI",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 DIRDEBITCLISCLIENT1: row.SCLIENT }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
        return returnData;
    };
    this.Bk_account_ShowValidation = function (row) {
            var returnData;
            var countData;
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/Bk_accountSelectCommandActionBK_ACCOUNT",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 BKACCOUNTSCLIENT1: row.SCLIENT }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
        return returnData;
    };
    this.Cred_card_ShowValidation = function (row) {
            var returnData;
            var countData;
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/Cred_cardSelectCommandActionCRED_CARD",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 CREDCARDSCLIENT1: row.SCLIENT }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
        return returnData;
    };
    this.Curr_acc_ShowValidation = function (row) {
            var returnData;
            var countData;
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/Curr_accSelectCommandActionCURR_ACC",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 CURRACCSCLIENT1: row.SCLIENT }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
        return returnData;
    };
    this.Sport_ShowValidation = function (row) {
            var returnData;
            var countData;
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/SportSelectCommandActionSPORT",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 SPORTSCLIENT1: row.SCLIENT }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
        return returnData;
    };
    this.Hobby_ShowValidation = function (row) {
            var returnData;
            var countData;
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/HobbySelectCommandActionHOBBY",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 HOBBYSCLIENT1: row.SCLIENT }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
        return returnData;
    };
    this.Financ_cli_ShowValidation = function (row) {
            var returnData;
            var countData;
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/Financ_cliSelectCommandActionFINANC_CLI",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 FINANCCLISCLIENT1: row.SCLIENT }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
        return returnData;
    };
    this.RELATIONS_ShowValidation = function (row) {
            var returnData;
            var countData;
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/RELATIONSSelectCommandActionRELATIONS",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 RELATIONSSCLIENT1: row.SCLIENT }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
        return returnData;
    };
    this.CHEQUES_ShowValidation = function (row) {
            var returnData;
            var countData;
               $.ajax({
                    type: "POST",
                    url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/CHEQUESSelectCommandActionCHEQUES",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({                 CHEQUESSCLIENT1: row.SCLIENT }),
                    success: function (data) {
                    
                        if (data.d.Success === true) {
                            if (data.d.Count !== 0)
                                countData = data.d.Data.Result;
                        }
                        else
                            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            if (countData >= 1){
                returnData = true;
                }                
                else {
                returnData = false;

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                });
        return returnData;
    };

    this.ControlActions = function () {

        $('#btnOk').click(function (event) {
            var formInstance = $("#HT5ConsultaClientesSegunClienteIndicadoMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#btnOk'));
                btnLoading.start();
                HT5ConsultaClientesSegunClienteIndicadoSupport.ItemsTblRequest();
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();
    
        $("#HT5ConsultaClientesSegunClienteIndicadoMainForm").validate({
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
                    DatePicker: 'La fecha indicada no es válida'
                }
            }
        });

    };
    this.LookUpForROLESnRoleFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#ROLESnRole>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForROLESnRole = function (defaultValue, source) {
        var ctrol = $('#ROLESnRole');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/LookUpForROLESnRole",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5ConsultaClientesSegunClienteIndicadoFormId').val()}),
                
                success: function (data) {
                    ctrol.children().remove();
                    if (data.d.Success === true) {
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue);
                        else
                            ctrol.val(0);
                           
                           if(source !== 'Initialization')
                               ctrol.change();
                               
                            
                            
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
                if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                    ctrol.val(defaultValue);
					          
                    if(source!=='Initialization')
                       ctrol.change();
				       }
    };
    this.LookUpForROLESnStatusRolFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#ROLESnStatusRol>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForROLESnStatusRol = function (defaultValue, source) {
        var ctrol = $('#ROLESnStatusRol');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/LookUpForROLESnStatusRol",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5ConsultaClientesSegunClienteIndicadoFormId').val()}),
                
                success: function (data) {
                    ctrol.children().remove();
                    if (data.d.Success === true) {
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue);
                        else
                            ctrol.val(0);
                           
                           if(source !== 'Initialization')
                               ctrol.change();
                               
                            
                            
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
                if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                    ctrol.val(defaultValue);
					          
                    if(source!=='Initialization')
                       ctrol.change();
				       }
    };
    this.LookUpForROLESsSexClienFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#ROLESsSexClien>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForROLESsSexClien = function (defaultValue, source) {
        var ctrol = $('#ROLESsSexClien');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/LookUpForROLESsSexClien",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5ConsultaClientesSegunClienteIndicadoFormId').val()}),
                
                success: function (data) {
                    ctrol.children().remove();
                    if (data.d.Success === true) {
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue);
                        else
                            ctrol.val(0);
                           
                           if(source !== 'Initialization')
                               ctrol.change();
                               
                            
                            
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
                if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                    ctrol.val(defaultValue);
					          
                    if(source!=='Initialization')
                       ctrol.change();
				       }
    };
    this.LookUpForROLESnTypeRiskFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#ROLESnTypeRisk>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForROLESnTypeRisk = function (defaultValue, source) {
        var ctrol = $('#ROLESnTypeRisk');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/LookUpForROLESnTypeRisk",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5ConsultaClientesSegunClienteIndicadoFormId').val()}),
                
                success: function (data) {
                    ctrol.children().remove();
                    if (data.d.Success === true) {
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue);
                        else
                            ctrol.val(0);
                           
                           if(source !== 'Initialization')
                               ctrol.change();
                               
                            
                            
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
                if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                    ctrol.val(defaultValue);
					          
                    if(source!=='Initialization')
                       ctrol.change();
				       }
    };
    this.LookUpForCOVERnModulecFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#COVERnModulec>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForCOVERnModulec = function (defaultValue, source) {
        var ctrol = $('#COVERnModulec');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/LookUpForCOVERnModulec",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5ConsultaClientesSegunClienteIndicadoFormId').val()}),
                
                success: function (data) {
                    ctrol.children().remove();
                    if (data.d.Success === true) {
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue);
                        else
                            ctrol.val(0);
                           
                           if(source !== 'Initialization')
                               ctrol.change();
                               
                            
                            
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
                if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                    ctrol.val(defaultValue);
					          
                    if(source!=='Initialization')
                       ctrol.change();
				       }
    };
    this.LookUpForCOVERnCoverFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#COVERnCover>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForCOVERnCover = function (defaultValue, source) {
        var ctrol = $('#COVERnCover');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/LookUpForCOVERnCover",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5ConsultaClientesSegunClienteIndicadoFormId').val()}),
                
                success: function (data) {
                    ctrol.children().remove();
                    if (data.d.Success === true) {
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue);
                        else
                            ctrol.val(0);
                           
                           if(source !== 'Initialization')
                               ctrol.change();
                               
                            
                            
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
                if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                    ctrol.val(defaultValue);
					          
                    if(source!=='Initialization')
                       ctrol.change();
				       }
    };
    this.LookUpForCOVERnRoleFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#COVERnRole>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForCOVERnRole = function (defaultValue, source) {
        var ctrol = $('#COVERnRole');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/LookUpForCOVERnRole",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5ConsultaClientesSegunClienteIndicadoFormId').val()}),
                
                success: function (data) {
                    ctrol.children().remove();
                    if (data.d.Success === true) {
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue);
                        else
                            ctrol.val(0);
                           
                           if(source !== 'Initialization')
                               ctrol.change();
                               
                            
                            
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
                if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                    ctrol.val(defaultValue);
					          
                    if(source!=='Initialization')
                       ctrol.change();
				       }
    };
    this.LookUpForCOVERnCurrencyFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#COVERnCurrency>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForCOVERnCurrency = function (defaultValue, source) {
        var ctrol = $('#COVERnCurrency');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/LookUpForCOVERnCurrency",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5ConsultaClientesSegunClienteIndicadoFormId').val()}),
                
                success: function (data) {
                    ctrol.children().remove();
                    if (data.d.Success === true) {
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue);
                        else
                            ctrol.val(0);
                           
                           if(source !== 'Initialization')
                               ctrol.change();
                               
                            
                            
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
                if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                    ctrol.val(defaultValue);
					          
                    if(source!=='Initialization')
                       ctrol.change();
				       }
    };
    this.LookUpForBENEFICIARnModulecFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#BENEFICIARnModulec>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForBENEFICIARnModulec = function (defaultValue, source) {
        var ctrol = $('#BENEFICIARnModulec');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/LookUpForBENEFICIARnModulec",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5ConsultaClientesSegunClienteIndicadoFormId').val()}),
                
                success: function (data) {
                    ctrol.children().remove();
                    if (data.d.Success === true) {
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue);
                        else
                            ctrol.val(0);
                           
                           if(source !== 'Initialization')
                               ctrol.change();
                               
                            
                            
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
                if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                    ctrol.val(defaultValue);
					          
                    if(source!=='Initialization')
                       ctrol.change();
				       }
    };
    this.LookUpForBENEFICIARnRelationFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#BENEFICIARnRelation>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForBENEFICIARnRelation = function (defaultValue, source) {
        var ctrol = $('#BENEFICIARnRelation');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/LookUpForBENEFICIARnRelation",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5ConsultaClientesSegunClienteIndicadoFormId').val()}),
                
                success: function (data) {
                    ctrol.children().remove();
                    if (data.d.Success === true) {
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue);
                        else
                            ctrol.val(0);
                           
                           if(source !== 'Initialization')
                               ctrol.change();
                               
                            
                            
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
                if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                    ctrol.val(defaultValue);
					          
                    if(source!=='Initialization')
                       ctrol.change();
				       }
    };
    this.LookUpForPremiumnTypeFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#PremiumnType>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForPremiumsManautiFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#PremiumsManauti>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForCLAIM_CASEnDeman_typeFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#CLAIM_CASEnDeman_type>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForCLAIM_CASEnDeman_type = function (defaultValue, source) {
        var ctrol = $('#CLAIM_CASEnDeman_type');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/LookUpForCLAIM_CASEnDeman_type",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5ConsultaClientesSegunClienteIndicadoFormId').val()}),
                
                success: function (data) {
                    ctrol.children().remove();
                    if (data.d.Success === true) {
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue);
                        else
                            ctrol.val(0);
                           
                           if(source !== 'Initialization')
                               ctrol.change();
                               
                            
                            
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
                if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                    ctrol.val(defaultValue);
					          
                    if(source!=='Initialization')
                       ctrol.change();
				       }
    };
    this.LookUpForCLAIM_CASEsStaCaseFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#CLAIM_CASEsStaCase>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForCLAIM_CASEsStaCase = function (defaultValue, source) {
        var ctrol = $('#CLAIM_CASEsStaCase');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/LookUpForCLAIM_CASEsStaCase",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5ConsultaClientesSegunClienteIndicadoFormId').val()}),
                
                success: function (data) {
                    ctrol.children().remove();
                    if (data.d.Success === true) {
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue);
                        else
                            ctrol.val(0);
                           
                           if(source !== 'Initialization')
                               ctrol.change();
                               
                            
                            
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
                if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                    ctrol.val(defaultValue);
					          
                    if(source!=='Initialization')
                       ctrol.change();
				       }
    };
    this.LookUpForCL_COVERnDeman_typeFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#CL_COVERnDeman_type>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForCL_COVERnDeman_type = function (defaultValue, source) {
        var ctrol = $('#CL_COVERnDeman_type');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/LookUpForCL_COVERnDeman_type",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5ConsultaClientesSegunClienteIndicadoFormId').val()}),
                
                success: function (data) {
                    ctrol.children().remove();
                    if (data.d.Success === true) {
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue);
                        else
                            ctrol.val(0);
                           
                           if(source !== 'Initialization')
                               ctrol.change();
                               
                            
                            
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
                if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                    ctrol.val(defaultValue);
					          
                    if(source!=='Initialization')
                       ctrol.change();
				       }
    };
    this.LookUpForCL_COVERnCurrencyFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#CL_COVERnCurrency>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForCL_COVERnCurrency = function (defaultValue, source) {
        var ctrol = $('#CL_COVERnCurrency');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/LookUpForCL_COVERnCurrency",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5ConsultaClientesSegunClienteIndicadoFormId').val()}),
                
                success: function (data) {
                    ctrol.children().remove();
                    if (data.d.Success === true) {
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue);
                        else
                            ctrol.val(0);
                           
                           if(source !== 'Initialization')
                               ctrol.change();
                               
                            
                            
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
                if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                    ctrol.val(defaultValue);
					          
                    if(source!=='Initialization')
                       ctrol.change();
				       }
    };
    this.LookUpForCL_COVERsFrantypeFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#CL_COVERsFrantype>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForCL_COVERsFrantype = function (defaultValue, source) {
        var ctrol = $('#CL_COVERsFrantype');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/LookUpForCL_COVERsFrantype",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5ConsultaClientesSegunClienteIndicadoFormId').val()}),
                
                success: function (data) {
                    ctrol.children().remove();
                    if (data.d.Success === true) {
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue);
                        else
                            ctrol.val(0);
                           
                           if(source !== 'Initialization')
                               ctrol.change();
                               
                            
                            
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
                if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                    ctrol.val(defaultValue);
					          
                    if(source!=='Initialization')
                       ctrol.change();
				       }
    };
    this.LookUpForCLAIM_ATTMnDeman_typeFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#CLAIM_ATTMnDeman_type>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForCLAIM_ATTMnDeman_type = function (defaultValue, source) {
        var ctrol = $('#CLAIM_ATTMnDeman_type');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/LookUpForCLAIM_ATTMnDeman_type",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5ConsultaClientesSegunClienteIndicadoFormId').val()}),
                
                success: function (data) {
                    ctrol.children().remove();
                    if (data.d.Success === true) {
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue);
                        else
                            ctrol.val(0);
                           
                           if(source !== 'Initialization')
                               ctrol.change();
                               
                            
                            
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
                if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                    ctrol.val(defaultValue);
					          
                    if(source!=='Initialization')
                       ctrol.change();
				       }
    };
    this.LookUpForCLAIM_ATTMsIllnessFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#CLAIM_ATTMsIllness>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForCLAIM_ATTMsIllness = function (defaultValue, source) {
        var ctrol = $('#CLAIM_ATTMsIllness');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/LookUpForCLAIM_ATTMsIllness",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5ConsultaClientesSegunClienteIndicadoFormId').val()}),
                
                success: function (data) {
                    ctrol.children().remove();
                    if (data.d.Success === true) {
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue);
                        else
                            ctrol.val(0);
                           
                           if(source !== 'Initialization')
                               ctrol.change();
                               
                            
                            
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
                if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                    ctrol.val(defaultValue);
					          
                    if(source!=='Initialization')
                       ctrol.change();
				       }
    };
    this.LookUpForCLAIM_HISnOper_typeFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#CLAIM_HISnOper_type>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForCLAIM_HISnOper_type = function (defaultValue, source) {
        var ctrol = $('#CLAIM_HISnOper_type');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/LookUpForCLAIM_HISnOper_type",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5ConsultaClientesSegunClienteIndicadoFormId').val()}),
                
                success: function (data) {
                    ctrol.children().remove();
                    if (data.d.Success === true) {
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue);
                        else
                            ctrol.val(0);
                           
                           if(source !== 'Initialization')
                               ctrol.change();
                               
                            
                            
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
                if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                    ctrol.val(defaultValue);
					          
                    if(source!=='Initialization')
                       ctrol.change();
				       }
    };
    this.LookUpForCLAIM_HISnPay_typeFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#CLAIM_HISnPay_type>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForCLAIM_HISnPay_type = function (defaultValue, source) {
        var ctrol = $('#CLAIM_HISnPay_type');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/LookUpForCLAIM_HISnPay_type",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5ConsultaClientesSegunClienteIndicadoFormId').val()}),
                
                success: function (data) {
                    ctrol.children().remove();
                    if (data.d.Success === true) {
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue);
                        else
                            ctrol.val(0);
                           
                           if(source !== 'Initialization')
                               ctrol.change();
                               
                            
                            
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
                if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                    ctrol.val(defaultValue);
					          
                    if(source!=='Initialization')
                       ctrol.change();
				       }
    };
    this.LookUpForCLAIM_HISnPay_formFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#CLAIM_HISnPay_form>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForCLAIM_HISnPay_form = function (defaultValue, source) {
        var ctrol = $('#CLAIM_HISnPay_form');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/LookUpForCLAIM_HISnPay_form",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5ConsultaClientesSegunClienteIndicadoFormId').val()}),
                
                success: function (data) {
                    ctrol.children().remove();
                    if (data.d.Success === true) {
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue);
                        else
                            ctrol.val(0);
                           
                           if(source !== 'Initialization')
                               ctrol.change();
                               
                            
                            
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
                if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                    ctrol.val(defaultValue);
					          
                    if(source!=='Initialization')
                       ctrol.change();
				       }
    };
    this.LookUpForCLAIM_HISnCurrencyFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#CLAIM_HISnCurrency>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForCLAIM_HISnCurrency = function (defaultValue, source) {
        var ctrol = $('#CLAIM_HISnCurrency');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/LookUpForCLAIM_HISnCurrency",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5ConsultaClientesSegunClienteIndicadoFormId').val()}),
                
                success: function (data) {
                    ctrol.children().remove();
                    if (data.d.Success === true) {
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue);
                        else
                            ctrol.val(0);
                           
                           if(source !== 'Initialization')
                               ctrol.change();
                               
                            
                            
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
                if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                    ctrol.val(defaultValue);
					          
                    if(source!=='Initialization')
                       ctrol.change();
				       }
    };
    this.LookUpForRELATIONSnRelashipFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#RELATIONSnRelaship>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForRELATIONSnRelaship = function (defaultValue, source) {
        var ctrol = $('#RELATIONSnRelaship');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/LookUpForRELATIONSnRelaship",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5ConsultaClientesSegunClienteIndicadoFormId').val()}),
                
                success: function (data) {
                    ctrol.children().remove();
                    if (data.d.Success === true) {
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue);
                        else
                            ctrol.val(0);
                           
                           if(source !== 'Initialization')
                               ctrol.change();
                               
                            
                            
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
                if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                    ctrol.val(defaultValue);
					          
                    if(source!=='Initialization')
                       ctrol.change();
				       }
    };
    this.LookUpForCHEQUESSREQUEST_TYFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#CHEQUESSREQUEST_TY>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForCHEQUESSREQUEST_TY = function (defaultValue, source) {
        var ctrol = $('#CHEQUESSREQUEST_TY');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/LookUpForCHEQUESSREQUEST_TY",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5ConsultaClientesSegunClienteIndicadoFormId').val()}),
                
                success: function (data) {
                    ctrol.children().remove();
                    if (data.d.Success === true) {
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue);
                        else
                            ctrol.val(0);
                           
                           if(source !== 'Initialization')
                               ctrol.change();
                               
                            
                            
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
                if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                    ctrol.val(defaultValue);
					          
                    if(source!=='Initialization')
                       ctrol.change();
				       }
    };
    this.LookUpForCHEQUESNCURRENCYPAYFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#CHEQUESNCURRENCYPAY>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForCHEQUESNCURRENCYPAY = function (defaultValue, source) {
        var ctrol = $('#CHEQUESNCURRENCYPAY');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/LookUpForCHEQUESNCURRENCYPAY",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5ConsultaClientesSegunClienteIndicadoFormId').val()}),
                
                success: function (data) {
                    ctrol.children().remove();
                    if (data.d.Success === true) {
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue);
                        else
                            ctrol.val(0);
                           
                           if(source !== 'Initialization')
                               ctrol.change();
                               
                            
                            
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
                if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                    ctrol.val(defaultValue);
					          
                    if(source!=='Initialization')
                       ctrol.change();
				       }
    };
    this.LookUpForCHEQUESNCONCEPTFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#CHEQUESNCONCEPT>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForCHEQUESNCONCEPT = function (defaultValue, source) {
        var ctrol = $('#CHEQUESNCONCEPT');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/LookUpForCHEQUESNCONCEPT",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5ConsultaClientesSegunClienteIndicadoFormId').val()}),
                
                success: function (data) {
                    ctrol.children().remove();
                    if (data.d.Success === true) {
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue);
                        else
                            ctrol.val(0);
                           
                           if(source !== 'Initialization')
                               ctrol.change();
                               
                            
                            
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
                if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                    ctrol.val(defaultValue);
					          
                    if(source!=='Initialization')
                       ctrol.change();
				       }
    };
    this.LookUpForCHEQUESNSTA_CHEQUEFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#CHEQUESNSTA_CHEQUE>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForCHEQUESNSTA_CHEQUE = function (defaultValue, source) {
        var ctrol = $('#CHEQUESNSTA_CHEQUE');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/LookUpForCHEQUESNSTA_CHEQUE",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5ConsultaClientesSegunClienteIndicadoFormId').val()}),
                
                success: function (data) {
                    ctrol.children().remove();
                    if (data.d.Success === true) {
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue);
                        else
                            ctrol.val(0);
                           
                           if(source !== 'Initialization')
                               ctrol.change();
                               
                            
                            
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
                if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                    ctrol.val(defaultValue);
					          
                    if(source!=='Initialization')
                       ctrol.change();
				       }
    };
    this.LookUpForCHEQUESNOFFICEFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#CHEQUESNOFFICE>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForCHEQUESNOFFICE = function (defaultValue, source) {
        var ctrol = $('#CHEQUESNOFFICE');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/LookUpForCHEQUESNOFFICE",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5ConsultaClientesSegunClienteIndicadoFormId').val()}),
                
                success: function (data) {
                    ctrol.children().remove();
                    if (data.d.Success === true) {
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue);
                        else
                            ctrol.val(0);
                           
                           if(source !== 'Initialization')
                               ctrol.change();
                               
                            
                            
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
                if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                    ctrol.val(defaultValue);
					          
                    if(source!=='Initialization')
                       ctrol.change();
				       }
    };
    this.LookUpForCHEQUESNOFFICEAGENFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#CHEQUESNOFFICEAGEN>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForCHEQUESNOFFICEAGEN = function (defaultValue, source) {
        var ctrol = $('#CHEQUESNOFFICEAGEN');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/LookUpForCHEQUESNOFFICEAGEN",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5ConsultaClientesSegunClienteIndicadoFormId').val()}),
                
                success: function (data) {
                    ctrol.children().remove();
                    if (data.d.Success === true) {
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue);
                        else
                            ctrol.val(0);
                           
                           if(source !== 'Initialization')
                               ctrol.change();
                               
                            
                            
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
                if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                    ctrol.val(defaultValue);
					          
                    if(source!=='Initialization')
                       ctrol.change();
				       }
    };
    this.LookUpForCHEQUESNTYPESUPPORTFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#CHEQUESNTYPESUPPORT>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForCHEQUESNTYPESUPPORT = function (defaultValue, source) {
        var ctrol = $('#CHEQUESNTYPESUPPORT');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "POST",
                url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/LookUpForCHEQUESNTYPESUPPORT",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: JSON.stringify({id: $('#HT5ConsultaClientesSegunClienteIndicadoFormId').val()}),
                
                success: function (data) {
                    ctrol.children().remove();
                    if (data.d.Success === true) {
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue);
                        else
                            ctrol.val(0);
                           
                           if(source !== 'Initialization')
                               ctrol.change();
                               
                            
                            
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
                if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                    ctrol.val(defaultValue);
					          
                    if(source!=='Initialization')
                       ctrol.change();
				       }
    };

    this.ItemsTblSetup = function (table) {
        table.bootstrapTable({
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            detailView: true,
            onExpandRow: HT5ConsultaClientesSegunClienteIndicadoSupport.ItemsTblExpandRow,
            columns: [{
                field: 'SCLIENT',
                title: 'Código de Cliente',
                sortable: true,
                halign: 'center'
            }, {
                field: 'sCliename',
                title: 'Nombre Completo del Cliente',
                sortable: true,
                halign: 'center'
            }, {
                field: 'sFirstname',
                title: 'Primer Nombre',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'sLastname',
                title: 'Apellido Paterno',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'sLastName2',
                title: 'Apellido Materno',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'sSexClien',
                title: 'Sexo del cliente',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'sSexClienDesc',
                title: 'Sexo del cliente',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nCivilSta',
                title: 'Estado Civil',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.ClientnCivilSta_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nCivilStaDesc',
                title: 'Estado Civil',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nWeight',
                title: 'Peso',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.ClientnWeight_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nHeight',
                title: 'Altura',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.ClientnHeight_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'dBirthdat',
                title: 'Fecha de Nacimiento',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'dDeathdat',
                title: 'Fecha de Muerte',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'sSmoking',
                title: 'Fumador',
                formatter: 'tableHelperSupport.IsCheck',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nLanguage',
                title: 'Lenguaje',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.ClientnLanguage_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nLanguageDesc',
                title: 'Lenguaje',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nMailingPref',
                title: 'Preferencia para El Correo',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.ClientnMailingPref_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nMailingPrefDesc',
                title: 'Preferencia para El Correo',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nTitle',
                title: 'Profesión',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.ClientnTitle_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nTitleDesc',
                title: 'Profesión',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nSpeciality',
                title: 'Especialidad',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.ClientnSpeciality_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nSpecialityDesc',
                title: 'Especialidad',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nNationality',
                title: 'Nacionalidad',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.ClientnNationality_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nNationalityDesc',
                title: 'Nacionalidad',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nClass',
                title: 'Clasificación del Cliente',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.ClientnClass_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nClassDesc',
                title: 'Clasificación del Cliente',
                sortable: true,
                halign: 'center'
            }, {
                field: 'dDependant',
                title: 'Fecha Trabajador Dependiente',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'dIndependant',
                title: 'Fecha de Independiente',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'dInpdate',
                title: 'Fecha de Ingreso',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'SDIGIT',
                title: 'SDIGIT',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'SCLIENT',
                title: 'ClientePagador',
                sortable: true,
                halign: 'center',
                visible: false
            }]
        });

        table.bootstrapTable('refreshOptions', {
            contextMenu: '#ItemsContextMenu',
            contextMenuButton: '',
            beforeContextMenuRow: function (e, row, buttonElement) {
                HT5ConsultaClientesSegunClienteIndicadoSupport.ItemsRowToInput(row);
                if (!buttonElement ) {

                    table.bootstrapTable('showContextMenu', {
                        event: e,
                        contextMenu: '#ItemsContextMenu',
                        buttonElement: buttonElement
                    });
                }
                return false;
            },
            onContextMenuItem: function (row, $el) {
                HT5ConsultaClientesSegunClienteIndicadoSupport.ItemsRowToInput(row); 
                switch ($el.data("item")) {
                    case 'Items_Item1':
                        HT5ConsultaClientesSegunClienteIndicadoSupport.Items_Item1_Actions(row, null);
                        break;
                }
            }
        });


    };


    this.ItemsRowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
        $('#ClientSCLIENT').val(row.SCLIENT);
        $('#ClientsCliename').val(row.sCliename);
        $('#ClientsFirstname').val(row.sFirstname);
        $('#ClientsLastname').val(row.sLastname);
        $('#ClientsLastName2').val(row.sLastName2);
        $('#ClientsSexClien').val(row.sSexClien);
        $('#ClientsSexClienDesc').val(row.sSexClienDesc);
        AutoNumeric.set('#ClientnCivilSta', row.nCivilSta);
        $('#ClientnCivilStaDesc').val(row.nCivilStaDesc);
        AutoNumeric.set('#ClientnWeight', row.nWeight);
        AutoNumeric.set('#ClientnHeight', row.nHeight);
        $('#ClientdBirthdat').val(generalSupport.ToJavaScriptDateCustom(row.dBirthdat, generalSupport.DateFormat()));
        $('#ClientdDeathdat').val(generalSupport.ToJavaScriptDateCustom(row.dDeathdat, generalSupport.DateFormat()));
        $('#ClientsSmoking').prop("checked", row.sSmoking);
        AutoNumeric.set('#ClientnLanguage', row.nLanguage);
        $('#ClientnLanguageDesc').val(row.nLanguageDesc);
        AutoNumeric.set('#ClientnMailingPref', row.nMailingPref);
        $('#ClientnMailingPrefDesc').val(row.nMailingPrefDesc);
        AutoNumeric.set('#ClientnTitle', row.nTitle);
        $('#ClientnTitleDesc').val(row.nTitleDesc);
        AutoNumeric.set('#ClientnSpeciality', row.nSpeciality);
        $('#ClientnSpecialityDesc').val(row.nSpecialityDesc);
        AutoNumeric.set('#ClientnNationality', row.nNationality);
        $('#ClientnNationalityDesc').val(row.nNationalityDesc);
        AutoNumeric.set('#ClientnClass', row.nClass);
        $('#ClientnClassDesc').val(row.nClassDesc);
        $('#ClientdDependant').val(generalSupport.ToJavaScriptDateCustom(row.dDependant, generalSupport.DateFormat()));
        $('#ClientdIndependant').val(generalSupport.ToJavaScriptDateCustom(row.dIndependant, generalSupport.DateFormat()));
        $('#ClientdInpdate').val(generalSupport.ToJavaScriptDateCustom(row.dInpdate, generalSupport.DateFormat()));
        $('#ClientSDIGIT').val(row.SDIGIT);
        $('#ClientSCLIENT').val(row.SCLIENT);

    };
    this.ItemsTblRequest = function (params) {
        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/ItemsTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                                CLIENTSCLIENT1: ($('#Client').data('code') !== undefined) ? $('#Client').data('code') : ''
            }),
            success: function (data) {
                if (data.d.Success === true) {
                    $('#ItemsTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

                }
                else
                    generalSupport.NotifyFail(data.d.Reason, data.d.Code);
            },
            error: function (qXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    };
    this.Curren_polTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],

            columns: [{
                field: 'nCurrency',
                title: 'Moneda',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Curren_polnCurrency_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nCurrencyDesc',
                title: 'Moneda',
                sortable: true,
                halign: 'center'
            }]
        });

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.Curren_polTblRequest();
      };

    this.Curren_polRowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#Curren_polnCurrency', row.nCurrency);
        $('#Curren_polnCurrencyDesc').val(row.nCurrencyDesc);

    };
    this.Curren_polTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/Curren_polTblDataLoad",
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
    this.ROLESTblSetup = function (table) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForROLESnRole('');
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForROLESnStatusRol('');
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForROLESsSexClien('');
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForROLESnTypeRisk('');
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],

            columns: [{
                field: 'nRole',
                title: 'Figura',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForROLESnRoleFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SCLIENAME',
                title: 'Cliente',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nStatusRol',
                title: 'Estado',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForROLESnStatusRolFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'sSexClien',
                title: 'Sexo',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForROLESsSexClienFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'dBirthDate',
                title: 'Fecha de nacimiento',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'nTypeRisk',
                title: 'Clasificación',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForROLESnTypeRiskFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nRating',
                title: 'Rating del asegurado.',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.ROLESnRating_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }]
        });

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.ROLESTblRequest();
      };

    this.ROLESRowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForROLESnRole(row.nRole, source);
        $('#ROLESSCLIENAME').val(row.SCLIENAME);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForROLESnStatusRol(row.nStatusRol, source);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForROLESsSexClien(row.sSexClien, source);
        $('#ROLESdBirthDate').val(generalSupport.ToJavaScriptDateCustom(row.dBirthDate, generalSupport.DateFormat()));
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForROLESnTypeRisk(row.nTypeRisk, source);
        AutoNumeric.set('#ROLESnRating', row.nRating);

    };
    this.ROLESTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/ROLESTblDataLoad",
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
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],

            columns: [{
                field: 'NSUMINS_COD',
                title: 'Capital Asegurado',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.SUM_INSURNSUMINS_COD_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NSUMINS_CODDesc',
                title: 'Capital Asegurado',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NSUMINS_REAL',
                title: 'Valor Real del Bien',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.SUM_INSURNSUMINS_REAL_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NCOINSURAN',
                title: 'Coaseguro Pactado',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.SUM_INSURNCOINSURAN_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NSUM_INSUR',
                title: 'Valor Asegurado',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.SUM_INSURNSUM_INSUR_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }]
        });

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.SUM_INSURTblRequest();
      };

    this.SUM_INSURRowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
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
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/SUM_INSURTblDataLoad",
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
    this.COVERTblSetup = function (table) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCOVERnModulec('');
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCOVERnCover('');
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCOVERnRole('');
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCOVERnCurrency('');
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],

            columns: [{
                field: 'nModulec',
                title: 'Módulo',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCOVERnModulecFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nCover',
                title: 'Cobertura',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCOVERnCoverFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nRole',
                title: 'Figura del cliente',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCOVERnRoleFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SCLIENAME',
                title: 'Cliente',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nCurrency',
                title: 'Moneda',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCOVERnCurrencyFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nCapital',
                title: 'Capital Asegurado',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.COVERnCapital_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nPremium',
                title: 'Prima Anual',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.COVERnPremium_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }]
        });

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.COVERTblRequest();
      };

    this.COVERRowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCOVERnModulec(row.nModulec, source);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCOVERnCover(row.nCover, source);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCOVERnRole(row.nRole, source);
        $('#COVERSCLIENAME').val(row.SCLIENAME);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCOVERnCurrency(row.nCurrency, source);
        AutoNumeric.set('#COVERnCapital', row.nCapital);
        AutoNumeric.set('#COVERnPremium', row.nPremium);

    };
    this.COVERTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/COVERTblDataLoad",
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
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],

            columns: [{
                field: 'NBRANCH_REI',
                title: 'Ramo de Reaseguro',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.REINSURANNBRANCH_REI_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NBRANCH_REIDesc',
                title: 'Ramo de Reaseguro',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NTYPE_REIN',
                title: 'Contrato',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.REINSURANNTYPE_REIN_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NTYPE_REINDesc',
                title: 'Contrato',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NCAPITAL',
                title: 'Capital Cedido',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.REINSURANNCAPITAL_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NCURRENCY',
                title: 'Moneda',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.REINSURANNCURRENCY_FormatterMaskData',
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
                field: 'NSHARE',
                title: '%Participación',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.REINSURANNSHARE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }]
        });

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.REINSURANTblRequest();
      };

    this.REINSURANRowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
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
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/REINSURANTblDataLoad",
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
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],

            columns: [{
                field: 'NBRANCH_REI',
                title: 'Ramo de Reaseguro',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.REINSURAN2NBRANCH_REI_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NBRANCH_REIDesc',
                title: 'Ramo de Reaseguro',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NCOMPANY',
                title: 'Compañía',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.REINSURAN2NCOMPANY_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NCAPITAL',
                title: 'Capital Cedido',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.REINSURAN2NCAPITAL_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NCURRENCY',
                title: 'Moneda',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.REINSURAN2NCURRENCY_FormatterMaskData',
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
                field: 'NSHARE',
                title: '%Participación',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.REINSURAN2NSHARE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NCOMMISSI',
                title: '%Comisión',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.REINSURAN2NCOMMISSI_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }]
        });

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.REINSURAN2TblRequest();
      };

    this.REINSURAN2RowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
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
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/REINSURAN2TblDataLoad",
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
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],

            columns: [{
                field: 'nDisc_code',
                title: 'Recargo/Descuento/Impuesto',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Disc_xpremnDisc_code_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nDisc_codeDesc',
                title: 'Recargo/Descuento/Impuesto',
                sortable: true,
                halign: 'center'
            }, {
                field: 'sAgree',
                title: 'Recargo Aceptado',
                formatter: 'tableHelperSupport.IsCheck',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nCause',
                title: 'Causa',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Disc_xpremnCause_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nCauseDesc',
                title: 'Causa',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nPercent',
                title: 'Porcentaje',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Disc_xpremnPercent_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nCurrency',
                title: 'Moneda',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Disc_xpremnCurrency_FormatterMaskData',
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
                field: 'nAmount',
                title: 'Monto fijo',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Disc_xpremnAmount_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }]
        });

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.Disc_xpremTblRequest();
      };

    this.Disc_xpremRowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
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
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/Disc_xpremTblDataLoad",
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
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],

            columns: [{
                field: 'sRegist',
                title: 'Licencia',
                sortable: true,
                halign: 'center'
            }, {
                field: 'sLicense_ty',
                title: 'Tipo de Matrícula',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'sLicense_tyDesc',
                title: 'Tipo de Matrícula',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nVehType',
                title: 'Tipo de Vehículo',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.AutonVehType_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nVehTypeDesc',
                title: 'Tipo de Vehículo',
                sortable: true,
                halign: 'center'
            }, {
                field: 'sMotor',
                title: 'Serial del Motor',
                sortable: true,
                halign: 'center'
            }, {
                field: 'sChassis',
                title: 'Chasis',
                sortable: true,
                halign: 'center'
            }, {
                field: 'sColor',
                title: 'Color',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nCapital',
                title: 'Capital Asegurado',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.AutonCapital_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nVeh_valor',
                title: 'Valor del Vehículo',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.AutonVeh_valor_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nYear',
                title: 'Año de Fabricación',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.AutonYear_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nAutoZone',
                title: 'Zona de Circulación',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.AutonAutoZone_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nAutoZoneDesc',
                title: 'Zona de Circulación',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nUse',
                title: 'Uso del Vehículo',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.AutonUse_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }]
        });

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.AutoTblRequest();
      };

    this.AutoRowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
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

    };
    this.AutoTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/AutoTblDataLoad",
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
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],

            columns: [{
                field: 'nConstCat',
                title: 'Categoría de Construcción',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.FirenConstCat_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nConstCatDesc',
                title: 'Categoría de Construcción',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nActivityCat',
                title: 'Categoría de Actividad',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.FirenActivityCat_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nActivityCatDesc',
                title: 'Categoría de Actividad',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nFloor_quan',
                title: 'Pisos del Edificio',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.FirenFloor_quan_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nRoofType',
                title: 'Tipo de Techo',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.FirenRoofType_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nRoofTypeDesc',
                title: 'Tipo de Techo',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nSeismicZone',
                title: 'Zona Sísmica',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.FirenSeismicZone_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nSeismicZoneDesc',
                title: 'Zona Sísmica',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nBuildType',
                title: 'Tipo de Construcción Sísmica',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.FirenBuildType_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nBuildTypeDesc',
                title: 'Tipo de Construcción Sísmica',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nSpCombType',
                title: 'Tipo de Combustión Espontánea',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.FirenSpCombType_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nSpCombTypeDesc',
                title: 'Tipo de Combustión Espontánea',
                sortable: true,
                halign: 'center'
            }, {
                field: 'sDescBussi',
                title: 'Descripción Específica',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nHurrican_zone',
                title: 'Zona de Huracán',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.FirenHurrican_zone_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nSideCloseType',
                title: 'Cerramiento de Costado',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.FirenSideCloseType_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nSideCloseTypeDesc',
                title: 'Cerramiento de Costado',
                sortable: true,
                halign: 'center'
            }]
        });
        table.bootstrapTable('refreshOptions', {
            contextMenu: '#FireContextMenu',
            contextMenuButton: '',
            beforeContextMenuRow: function (e, row, buttonElement) {
                HT5ConsultaClientesSegunClienteIndicadoSupport.FireRowToInput(row);
                if (!buttonElement ) {

                    table.bootstrapTable('showContextMenu', {
                        event: e,
                        contextMenu: '#FireContextMenu',
                        buttonElement: buttonElement
                    });
                }
                return false;
            },
            onContextMenuItem: function (row, $el) {
                HT5ConsultaClientesSegunClienteIndicadoSupport.FireRowToInput(row); 
                switch ($el.data("item")) {
                    case 'Fire_Item1':
                        HT5ConsultaClientesSegunClienteIndicadoSupport.Fire_Item1_Actions(row, null);
                        break;
                }
            }
        });

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.FireTblRequest();
      };

    this.FireRowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
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
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/FireTblDataLoad",
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
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],

            columns: [{
                field: 'nOwnerShip',
                title: 'Ocupación',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.HomeOwnernOwnerShip_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nOwnerShipDesc',
                title: 'Ocupación',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nDwellingType',
                title: 'Tipo de Vivienda',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.HomeOwnernDwellingType_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nDwellingTypeDesc',
                title: 'Tipo de Vivienda',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NSEISMICZONE',
                title: 'Zona Sísmica',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.HomeOwnerNSEISMICZONE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nYear_built',
                title: 'Año de Construcción',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.HomeOwnernYear_built_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'dDate_purch',
                title: 'Fecha de Compra de la Vivienda',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center',
                visible: false
            }, {
                field: 'nPrice_purch',
                title: 'Precio de Compra',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.HomeOwnernPrice_purch_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nCurrency_purch',
                title: 'Moneda de Precio de Compra',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.HomeOwnernCurrency_purch_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nCurrency_purchDesc',
                title: 'Moneda de Precio de Compra',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nLandSuper',
                title: 'Superficie del Terreno',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.HomeOwnernLandSuper_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nHomeSuper',
                title: 'Superficie',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.HomeOwnernHomeSuper_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nRoofType',
                title: 'Tipo de Techo',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.HomeOwnernRoofType_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nRoofTypeDesc',
                title: 'Tipo de Techo',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'nRoofYear',
                title: 'Año del Techo',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.HomeOwnernRoofYear_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nFloodZone',
                title: 'Zona de Inundación',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.HomeOwnernFloodZone_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nFloodZoneDesc',
                title: 'Zona de Inundación',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nFoundType',
                title: 'Fundación',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.HomeOwnernFoundType_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'sSprinkSys',
                title: 'Posee Sistema de Riego',
                formatter: 'tableHelperSupport.IsCheck',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'nAirType',
                title: 'Aire Acondicionado',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.HomeOwnernAirType_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nAirTypeDesc',
                title: 'Aire Acondicionado',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'nStories',
                title: 'Pisos',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.HomeOwnernStories_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nHalfBath',
                title: 'Medios Baños',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.HomeOwnernHalfBath_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nFullBath',
                title: 'Baños',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.HomeOwnernFullBath_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nBedrooms',
                title: 'Habitaciones',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.HomeOwnernBedrooms_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nFirePlace',
                title: 'Chimeneas',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.HomeOwnernFirePlace_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nGarage',
                title: 'Cantidad de Vehículos',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.HomeOwnernGarage_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'sAnimalsDes',
                title: 'Mascotas',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'nDist_Fire',
                title: 'Distancia Bomberos',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.HomeOwnernDist_Fire_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'sAlarm_comp',
                title: 'Proveedor de Sistema de Alarma',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'sFencePool',
                title: 'Piscina Con Cerca',
                formatter: 'tableHelperSupport.IsCheck',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'nSwimPool',
                title: 'Ubicación de la Piscina',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.HomeOwnernSwimPool_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nSwimPoolDesc',
                title: 'Ubicación de la Piscina',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'nFenceHeight',
                title: 'Altura de la Cerca',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.HomeOwnernFenceHeight_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'sPolicy_other',
                title: 'Otra Póliza Sobre El Riesgo',
                formatter: 'tableHelperSupport.IsCheck',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'nCap_other',
                title: 'Capital de la Otra Póliza',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.HomeOwnernCap_other_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nCurrency_other',
                title: 'Moneda de la Otra Póliza',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.HomeOwnernCurrency_other_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nCurrency_otherDesc',
                title: 'Moneda de la Otra Póliza',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'dExpir_other',
                title: 'Vencimiento de la Otra Póliza',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center',
                visible: false
            }]
        });

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.HomeOwnerTblRequest();
      };

    this.HomeOwnerRowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
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
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/HomeOwnerTblDataLoad",
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
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],

            columns: [{
                field: 'nTypDurPay',
                title: 'Tipo de Duración de Pagos',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.LifenTypDurPay_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nTypDurPayDesc',
                title: 'Tipo de Duración de Pagos',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nPay_time',
                title: 'Duración de Pagos',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.LifenPay_time_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nTypDurIns',
                title: 'Tipo de Duración del Seguro',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.LifenTypDurIns_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nTypDurInsDesc',
                title: 'Tipo de Duración del Seguro',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nInsur_time',
                title: 'Duración del Seguro',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.LifenInsur_time_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nXprem_time',
                title: 'Duración de Recargos',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.LifenXprem_time_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nAge_limit',
                title: 'Edad Límite',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.LifenAge_limit_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NAGE_REINSU',
                title: 'Edad Actuarial',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.LifeNAGE_REINSU_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NAGE',
                title: 'Edad del Asegurado',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.LifeNAGE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }]
        });

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.LifeTblRequest();
      };

    this.LifeRowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
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
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/LifeTblDataLoad",
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
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],

            columns: [{
                field: 'DSTARTDATE',
                title: 'Fecha de Inicio',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'DEXPIRDAT',
                title: 'Fecha de Vencimiento',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'NCOMMERGRP',
                title: 'Grupo comercial',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.THEFTNCOMMERGRP_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NCOMMERGRPDesc',
                title: 'Grupo comercial',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SDESCBUSSI',
                title: 'Descripción',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NINSURED',
                title: '%Primer Riesgo',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.THEFTNINSURED_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NEMPLOYEES',
                title: 'Transportistas',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.THEFTNEMPLOYEES_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NAREA',
                title: 'Área',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.THEFTNAREA_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NVIGILANCE',
                title: 'Vigilantes',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.THEFTNVIGILANCE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NCAPITAL',
                title: 'Capital Asegurado',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.THEFTNCAPITAL_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'DNULLDATE',
                title: 'Fecha de Anulación',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'NNULLCODE',
                title: 'Código de Anulación',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.THEFTNNULLCODE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }]
        });

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.THEFTTblRequest();
      };

    this.THEFTRowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
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
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/THEFTTblDataLoad",
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
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],

            columns: [{
                field: 'NCONSECUTIVE',
                title: '#',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.FINANCIAL_INSTRUMENTSNCONSECUTIVE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NINSTRUMENT_TY',
                title: 'Tipo',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.FINANCIAL_INSTRUMENTSNINSTRUMENT_TY_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NINSTRUMENT_TYDesc',
                title: 'Tipo',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NBANK_CODE',
                title: 'Banco',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.FINANCIAL_INSTRUMENTSNBANK_CODE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NBANK_CODEDesc',
                title: 'Banco',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NCARD_TYPE',
                title: 'Tipo de Tarjeta',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.FINANCIAL_INSTRUMENTSNCARD_TYPE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NCARD_TYPEDesc',
                title: 'Tipo de Tarjeta',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SNUMBER',
                title: '#Tarjeta de crédito.',
                sortable: true,
                halign: 'center'
            }, {
                field: 'DCARDEXPIR',
                title: 'F.Vencimiento tarjeta',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'DSTARTDATE',
                title: 'Inicio de vigencia del Crédito',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'DTERM_DATE',
                title: 'Vencimiento del Crédito',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'NQUOTA',
                title: 'Cuotas',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.FINANCIAL_INSTRUMENTSNQUOTA_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NAMOUNT',
                title: 'Importe del crédito',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.FINANCIAL_INSTRUMENTSNAMOUNT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NCURRENCY',
                title: 'Moneda',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.FINANCIAL_INSTRUMENTSNCURRENCY_FormatterMaskData',
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

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.FINANCIAL_INSTRUMENTSTblRequest();
      };

    this.FINANCIAL_INSTRUMENTSRowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
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
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/FINANCIAL_INSTRUMENTSTblDataLoad",
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
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],

            columns: [{
                field: 'NROLE',
                title: 'Figura',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'SCLIENAME',
                title: 'Asegurado',
                sortable: true,
                halign: 'center'
            }]
        });
        table.bootstrapTable('refreshOptions', {
            contextMenu: '#ROLES2ContextMenu',
            contextMenuButton: '',
            beforeContextMenuRow: function (e, row, buttonElement) {
                HT5ConsultaClientesSegunClienteIndicadoSupport.ROLES2RowToInput(row);
                if (!buttonElement ) {

                    table.bootstrapTable('showContextMenu', {
                        event: e,
                        contextMenu: '#ROLES2ContextMenu',
                        buttonElement: buttonElement
                    });
                }
                return false;
            },
            onContextMenuItem: function (row, $el) {
                HT5ConsultaClientesSegunClienteIndicadoSupport.ROLES2RowToInput(row); 
                switch ($el.data("item")) {
                    case 'ROLES2_Item1':
                        HT5ConsultaClientesSegunClienteIndicadoSupport.ROLES2_Item1_Actions(row, null);
                        break;
                }
            }
        });

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.ROLES2TblRequest();
      };

    this.ROLES2RowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
        $('#ROLES2NROLE').val(row.NROLE);
        $('#ROLES2SCLIENAME').val(row.SCLIENAME);

    };
    this.ROLES2TblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/ROLES2TblDataLoad",
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

        var detailShow = HT5ConsultaClientesSegunClienteIndicadoSupport.ROLES2_ShowValidation(row);
        if (detailShow)
        html.push('<table id="ROLES2Tbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Asegurados</caption></table>');

        $detail.html(html.join(""));
        if (detailShow)
        HT5ConsultaClientesSegunClienteIndicadoSupport.ROLES2TblSetup($detail.find('#ROLES2Tbl-' + index));

    };
    this.HEALTHTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            detailView: true,
            onExpandRow: HT5ConsultaClientesSegunClienteIndicadoSupport.HEALTHTblExpandRow,

            columns: [{
                field: 'DEFFECDATE',
                title: 'Fecha de Efecto del Registro',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'DEXPIRDAT',
                title: 'Fecha de Vencimiento',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'NCAPITAL',
                title: 'Capital Asegurado',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.HEALTHNCAPITAL_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NPREMIUM',
                title: 'Monto de prima.',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.HEALTHNPREMIUM_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'SCERTYPE',
                title: 'TipoDeRegistro',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'NBRANCH',
                title: 'RamoComercial',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.HEALTHNBRANCH_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NPRODUCT',
                title: 'CódigoDelProducto',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.HEALTHNPRODUCT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NPOLICY',
                title: 'NúmeroDeLaPóliza',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.HEALTHNPOLICY_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NCERTIF',
                title: 'Número del certificado',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.HEALTHNCERTIF_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }]
        });

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.HEALTHTblRequest();
      };

    this.HEALTHRowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
        $('#HEALTHDEFFECDATE').val(generalSupport.ToJavaScriptDateCustom(row.DEFFECDATE, generalSupport.DateFormat()));
        $('#HEALTHDEXPIRDAT').val(generalSupport.ToJavaScriptDateCustom(row.DEXPIRDAT, generalSupport.DateFormat()));
        AutoNumeric.set('#HEALTHNCAPITAL', row.NCAPITAL);
        AutoNumeric.set('#HEALTHNPREMIUM', row.NPREMIUM);
        $('#HEALTHSCERTYPE').val(row.SCERTYPE);
        AutoNumeric.set('#HEALTHNBRANCH', row.NBRANCH);
        AutoNumeric.set('#HEALTHNPRODUCT', row.NPRODUCT);
        AutoNumeric.set('#HEALTHNPOLICY', row.NPOLICY);
        AutoNumeric.set('#HEALTHNCERTIF', row.NCERTIF);

    };
    this.HEALTHTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/HEALTHTblDataLoad",
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
    this.BENEFICIARTblSetup = function (table) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForBENEFICIARnModulec('');
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForBENEFICIARnRelation('');
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],

            columns: [{
                field: 'SCLIENAME',
                title: 'Beneficiario',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nModulec',
                title: 'Módulo',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForBENEFICIARnModulecFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nCover',
                title: 'Cobertura',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.BENEFICIARnCover_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nRelation',
                title: 'Nexo',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForBENEFICIARnRelationFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'sIrrevoc',
                title: 'Beneficiario Irrevocable',
                formatter: 'tableHelperSupport.IsCheck',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nParticip',
                title: 'Porcentaje de Participación',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.BENEFICIARnParticip_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }]
        });

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.BENEFICIARTblRequest();
      };

    this.BENEFICIARRowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
        $('#BENEFICIARSCLIENAME').val(row.SCLIENAME);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForBENEFICIARnModulec(row.nModulec, source);
        AutoNumeric.set('#BENEFICIARnCover', row.nCover);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForBENEFICIARnRelation(row.nRelation, source);
        $('#BENEFICIARsIrrevoc').prop("checked", row.sIrrevoc);
        AutoNumeric.set('#BENEFICIARnParticip', row.nParticip);

    };
    this.BENEFICIARTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/BENEFICIARTblDataLoad",
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
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],

            columns: [{
                field: 'nClause',
                title: 'Cláusula',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.ClausenClause_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nNotenum',
                title: 'Nota',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.ClausenNotenum_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }]
        });

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.ClauseTblRequest();
      };

    this.ClauseRowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#ClausenClause', row.nClause);
        AutoNumeric.set('#ClausenNotenum', row.nNotenum);

    };
    this.ClauseTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/ClauseTblDataLoad",
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
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],

            columns: [{
                field: 'NMOVEMENT',
                title: 'Movimiento',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.POLICY_HISNMOVEMENT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NTYPE_HIST',
                title: 'Tipo',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.POLICY_HISNTYPE_HIST_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NTYPE_HISTDesc',
                title: 'Tipo',
                sortable: true,
                halign: 'center'
            }, {
                field: 'DEFFECDATE',
                title: 'Fecha de Efecto',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'NTRANSACTIO',
                title: 'Nro. de transacción',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.POLICY_HISNTRANSACTIO_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NRECEIPT',
                title: 'Recibo',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.POLICY_HISNRECEIPT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'SNULL_MOVE',
                title: 'Movimiento Anulado',
                formatter: 'tableHelperSupport.IsCheck',
                sortable: true,
                halign: 'center'
            }, {
                field: 'DNULLDATE',
                title: 'Fecha de Anulación',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }]
        });

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.POLICY_HISTblRequest();
      };

    this.POLICY_HISRowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
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
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/POLICY_HISTblDataLoad",
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
    this.CertificatTblExpandRow = function (index, row, $detail) {
       var tblparentid = $detail.parent().parent().parent()[0].id;
       var html = [];

        var detailShow = HT5ConsultaClientesSegunClienteIndicadoSupport.Curren_pol_ShowValidation(row);
        if (detailShow)
        html.push('<table id="Curren_polTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Moneda</caption></table>');
        html.push('<table id="ROLESTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Roles</caption></table>');
        html.push('<table id="SUM_INSURTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Capital Asegurado</caption></table>');
        html.push('<table id="COVERTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Cobertura</caption></table>');
        html.push('<table id="REINSURANTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Reaseguro</caption></table>');
        html.push('<table id="REINSURAN2Tbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Reaseguro facultativo</caption></table>');
        html.push('<table id="Disc_xpremTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Descuentos/recargos</caption></table>');
        html.push('<table id="AutoTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Auto</caption></table>');
        html.push('<table id="FireTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Incendio</caption></table>');
        html.push('<table id="HomeOwnerTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Hogar</caption></table>');
        html.push('<table id="LifeTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Vida</caption></table>');
        html.push('<table id="THEFTTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Robo</caption></table>');
        html.push('<table id="FINANCIAL_INSTRUMENTSTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Instrumento Financiero</caption></table>');
        html.push('<table id="HEALTHTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Salud</caption></table>');
        html.push('<table id="BENEFICIARTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Beneficiario</caption></table>');
        html.push('<table id="ClauseTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Cláusula</caption></table>');
        html.push('<table id="POLICY_HISTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Historia</caption></table>');

        $detail.html(html.join(""));
        if (detailShow)
        HT5ConsultaClientesSegunClienteIndicadoSupport.Curren_polTblSetup($detail.find('#Curren_polTbl-' + index));
        HT5ConsultaClientesSegunClienteIndicadoSupport.ROLESTblSetup($detail.find('#ROLESTbl-' + index));
        HT5ConsultaClientesSegunClienteIndicadoSupport.SUM_INSURTblSetup($detail.find('#SUM_INSURTbl-' + index));
        HT5ConsultaClientesSegunClienteIndicadoSupport.COVERTblSetup($detail.find('#COVERTbl-' + index));
        HT5ConsultaClientesSegunClienteIndicadoSupport.REINSURANTblSetup($detail.find('#REINSURANTbl-' + index));
        HT5ConsultaClientesSegunClienteIndicadoSupport.REINSURAN2TblSetup($detail.find('#REINSURAN2Tbl-' + index));
        HT5ConsultaClientesSegunClienteIndicadoSupport.Disc_xpremTblSetup($detail.find('#Disc_xpremTbl-' + index));
        HT5ConsultaClientesSegunClienteIndicadoSupport.AutoTblSetup($detail.find('#AutoTbl-' + index));
        HT5ConsultaClientesSegunClienteIndicadoSupport.FireTblSetup($detail.find('#FireTbl-' + index));
        HT5ConsultaClientesSegunClienteIndicadoSupport.HomeOwnerTblSetup($detail.find('#HomeOwnerTbl-' + index));
        HT5ConsultaClientesSegunClienteIndicadoSupport.LifeTblSetup($detail.find('#LifeTbl-' + index));
        HT5ConsultaClientesSegunClienteIndicadoSupport.THEFTTblSetup($detail.find('#THEFTTbl-' + index));
        HT5ConsultaClientesSegunClienteIndicadoSupport.FINANCIAL_INSTRUMENTSTblSetup($detail.find('#FINANCIAL_INSTRUMENTSTbl-' + index));
        HT5ConsultaClientesSegunClienteIndicadoSupport.HEALTHTblSetup($detail.find('#HEALTHTbl-' + index));
        HT5ConsultaClientesSegunClienteIndicadoSupport.BENEFICIARTblSetup($detail.find('#BENEFICIARTbl-' + index));
        HT5ConsultaClientesSegunClienteIndicadoSupport.ClauseTblSetup($detail.find('#ClauseTbl-' + index));
        HT5ConsultaClientesSegunClienteIndicadoSupport.POLICY_HISTblSetup($detail.find('#POLICY_HISTbl-' + index));

    };
    this.CertificatTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            search: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            detailView: true,
            onExpandRow: HT5ConsultaClientesSegunClienteIndicadoSupport.CertificatTblExpandRow,

            columns: [{
                field: 'nBranch',
                title: 'Ramo',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.CertificatnBranch_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nBranchDesc',
                title: 'Ramo',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nProduct',
                title: 'Producto',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.CertificatnProduct_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nProductDesc',
                title: 'Producto',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nPolicy',
                title: 'Póliza',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nCapital',
                title: 'Capital Asegurado',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.CertificatnCapital_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nPayfreq',
                title: 'Frecuencia de Pago',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.CertificatnPayfreq_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nPayfreqDesc',
                title: 'Frecuencia de Pago',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'sStatusva',
                title: 'Estado',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'sStatusvaDesc',
                title: 'Estado',
                sortable: true,
                halign: 'center'
            }, {
                field: 'dIssuedat',
                title: 'Fecha de Emisión',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'nWait_code',
                title: 'Causa de Pendiente',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.CertificatnWait_code_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nWait_codeDesc',
                title: 'Causa de Pendiente',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'dStartdate',
                title: 'Fecha de Inicio',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center',
                visible: false
            }, {
                field: 'sRenewal',
                title: 'Renovación Automática',
                formatter: 'tableHelperSupport.IsCheck',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'dNulldate',
                title: 'Fecha de Anulación',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center',
                visible: false
            }, {
                field: 'nNullcode',
                title: 'Código de Anulación',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.CertificatnNullcode_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nNullcodeDesc',
                title: 'Código de Anulación',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'dExpirdat',
                title: 'Fecha de Vencimiento',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center',
                visible: false
            }, {
                field: 'dChangdat',
                title: 'Fecha de Último Cambio',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center',
                visible: false
            }, {
                field: 'SCERTYPE',
                title: 'TipoDeRegistro',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'NCERTIF',
                title: 'Número del certificado',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.CertificatNCERTIF_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }]
        });
        table.bootstrapTable('refreshOptions', {
            contextMenu: '#CertificatContextMenu',
            contextMenuButton: '',
            beforeContextMenuRow: function (e, row, buttonElement) {
                HT5ConsultaClientesSegunClienteIndicadoSupport.CertificatRowToInput(row);
                if (!buttonElement ) {

                    table.bootstrapTable('showContextMenu', {
                        event: e,
                        contextMenu: '#CertificatContextMenu',
                        buttonElement: buttonElement
                    });
                }
                return false;
            },
            onContextMenuItem: function (row, $el) {
                HT5ConsultaClientesSegunClienteIndicadoSupport.CertificatRowToInput(row); 
                switch ($el.data("item")) {
                    case 'Certificat_Item1':
                        HT5ConsultaClientesSegunClienteIndicadoSupport.Certificat_Item1_Actions(row, null);
                        break;
                    case 'Certificat_Item2':
                        HT5ConsultaClientesSegunClienteIndicadoSupport.Certificat_Item2_Actions(row, null);
                        break;
                }
            }
        });

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.CertificatTblRequest();
      };

    this.CertificatRowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
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
        AutoNumeric.set('#CertificatNCERTIF', row.NCERTIF);

    };
    this.CertificatTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/CertificatTblDataLoad",
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
    this.Detail_preTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],

            columns: [{
                field: 'nReceipt',
                title: 'Número del Recibo',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Detail_prenReceipt_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'sType_detai',
                title: 'Tipo',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nBill_item',
                title: 'Concepto de Facturación',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Detail_prenBill_item_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nBill_itemDesc',
                title: 'Concepto de Facturación',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nPremium',
                title: 'Monto de prima',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Detail_prenPremium_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nCommision',
                title: 'Monto de Comisión',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Detail_prenCommision_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nPremAnual',
                title: 'Prima Anual',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Detail_prenPremAnual_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nPremiumE',
                title: 'Prima Exenta',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Detail_prenPremiumE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nPremiumA',
                title: 'Prima Afecta',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Detail_prenPremiumA_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nDescAmount',
                title: 'Monto de Descuento',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Detail_prenDescAmount_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nRecAmount',
                title: 'Monto de Recargo',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Detail_prenRecAmount_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nTaxAmount',
                title: 'Monto de Impuestos',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Detail_prenTaxAmount_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nComAnual',
                title: 'Monto de Comisión Anual',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Detail_prenComAnual_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }]
        });

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.Detail_preTblRequest();
      };

    this.Detail_preRowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#Detail_prenReceipt', row.nReceipt);
        $('#Detail_presType_detai').val(row.sType_detai);
        AutoNumeric.set('#Detail_prenBill_item', row.nBill_item);
        $('#Detail_prenBill_itemDesc').val(row.nBill_itemDesc);
        AutoNumeric.set('#Detail_prenPremium', row.nPremium);
        AutoNumeric.set('#Detail_prenCommision', row.nCommision);
        AutoNumeric.set('#Detail_prenPremAnual', row.nPremAnual);
        AutoNumeric.set('#Detail_prenPremiumE', row.nPremiumE);
        AutoNumeric.set('#Detail_prenPremiumA', row.nPremiumA);
        AutoNumeric.set('#Detail_prenDescAmount', row.nDescAmount);
        AutoNumeric.set('#Detail_prenRecAmount', row.nRecAmount);
        AutoNumeric.set('#Detail_prenTaxAmount', row.nTaxAmount);
        AutoNumeric.set('#Detail_prenComAnual', row.nComAnual);

    };
    this.Detail_preTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/Detail_preTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                DETAILPRESCERTYPE1: row.SCERTYPE,
                DETAILPRENBRANCH2: row.nBranch,
                DETAILPRENPRODUCT3: row.nProduct,
                DETAILPRENRECEIPT4: row.nReceipt,
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
    this.Commiss_prTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],

            columns: [{
                field: 'nIntermed',
                title: 'Código de Productor',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Commiss_prnIntermed_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nRole',
                title: 'Tipo de Productor',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Commiss_prnRole_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nRoleDesc',
                title: 'Tipo de Productor',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nShare',
                title: 'Participación',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Commiss_prnShare_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nPercent',
                title: 'Porcentaje de Comisión',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Commiss_prnPercent_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nAmount',
                title: 'Monto de Comisión',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Commiss_prnAmount_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }]
        });

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.Commiss_prTblRequest();
      };

    this.Commiss_prRowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#Commiss_prnIntermed', row.nIntermed);
        AutoNumeric.set('#Commiss_prnRole', row.nRole);
        $('#Commiss_prnRoleDesc').val(row.nRoleDesc);
        AutoNumeric.set('#Commiss_prnShare', row.nShare);
        AutoNumeric.set('#Commiss_prnPercent', row.nPercent);
        AutoNumeric.set('#Commiss_prnAmount', row.nAmount);

    };
    this.Commiss_prTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/Commiss_prTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                COMMISSPRSCERTYPE1: row.SCERTYPE,
                COMMISSPRNBRANCH2: row.nBranch,
                COMMISSPRNPRODUCT3: row.nProduct,
                COMMISSPRNRECEIPT4: row.nReceipt,
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
    this.Premium_moTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],

            columns: [{
                field: 'nId',
                title: 'Consecutivo',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Premium_monId_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nTransac',
                title: 'Número de transacción',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Premium_monTransac_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nType',
                title: 'Tipo de movimiento',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Premium_monType_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nTypeDesc',
                title: 'Tipo de movimiento',
                sortable: true,
                halign: 'center'
            }, {
                field: 'dCompdate',
                title: 'Fecha',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'nAmount',
                title: 'Monto de prima',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Premium_monAmount_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nCurrency',
                title: 'Moneda',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Premium_monCurrency_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nCurrencyDesc',
                title: 'Moneda',
                sortable: true,
                halign: 'center'
            }]
        });

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.Premium_moTblRequest();
      };

    this.Premium_moRowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#Premium_monId', row.nId);
        AutoNumeric.set('#Premium_monTransac', row.nTransac);
        AutoNumeric.set('#Premium_monType', row.nType);
        $('#Premium_monTypeDesc').val(row.nTypeDesc);
        $('#Premium_modCompdate').val(generalSupport.ToJavaScriptDateCustom(row.dCompdate, generalSupport.DateFormat()));
        AutoNumeric.set('#Premium_monAmount', row.nAmount);
        AutoNumeric.set('#Premium_monCurrency', row.nCurrency);
        $('#Premium_monCurrencyDesc').val(row.nCurrencyDesc);

    };
    this.Premium_moTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/Premium_moTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                PREMIUMMOSCERTYPE1: row.SCERTYPE,
                PREMIUMMONBRANCH2: row.nBranch,
                PREMIUMMONPRODUCT3: row.nProduct,
                PREMIUMMONRECEIPT4: row.nReceipt,
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
    this.FINANC_DRATblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],

            columns: [{
                field: 'NDRAFT',
                title: 'Número de Cuota',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.FINANC_DRANDRAFT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NSTAT_DRAFT',
                title: 'Estado',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.FINANC_DRANSTAT_DRAFT_FormatterMaskData',
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
                field: 'NAMOUNT',
                title: 'Monto',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.FINANC_DRANAMOUNT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }]
        });

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.FINANC_DRATblRequest();
      };

    this.FINANC_DRARowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#FINANC_DRANDRAFT', row.NDRAFT);
        AutoNumeric.set('#FINANC_DRANSTAT_DRAFT', row.NSTAT_DRAFT);
        $('#FINANC_DRANSTAT_DRAFTDesc').val(row.NSTAT_DRAFTDesc);
        AutoNumeric.set('#FINANC_DRANAMOUNT', row.NAMOUNT);

    };
    this.FINANC_DRATblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/FINANC_DRATblDataLoad",
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

        var detailShow = HT5ConsultaClientesSegunClienteIndicadoSupport.FINANC_DRA_ShowValidation(row);
        if (detailShow)
        html.push('<table id="FINANC_DRATbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Giro de Contrato Financiero</caption></table>');

        $detail.html(html.join(""));
        if (detailShow)
        HT5ConsultaClientesSegunClienteIndicadoSupport.FINANC_DRATblSetup($detail.find('#FINANC_DRATbl-' + index));

    };
    this.FINANCE_COTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            detailView: true,
            onExpandRow: HT5ConsultaClientesSegunClienteIndicadoSupport.FINANCE_COTblExpandRow,

            columns: [{
                field: 'NCONTRAT',
                title: 'Contrato',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.FINANCE_CONCONTRAT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NQ_DRAFT',
                title: 'Cantidad de Cuotas',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.FINANCE_CONQ_DRAFT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NAMOUNT',
                title: 'Monto de prima a financiar',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.FINANCE_CONAMOUNT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NCURRENCY',
                title: 'Moneda',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.FINANCE_CONCURRENCY_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NFRECUENCY',
                title: 'Frecuencia de Giros',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.FINANCE_CONFRECUENCY_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NFRECUENCYDesc',
                title: 'Frecuencia de Giros',
                sortable: true,
                halign: 'center'
            }]
        });

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.FINANCE_COTblRequest();
      };

    this.FINANCE_CORowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#FINANCE_CONCONTRAT', row.NCONTRAT);
        AutoNumeric.set('#FINANCE_CONQ_DRAFT', row.NQ_DRAFT);
        AutoNumeric.set('#FINANCE_CONAMOUNT', row.NAMOUNT);
        AutoNumeric.set('#FINANCE_CONCURRENCY', row.NCURRENCY);
        AutoNumeric.set('#FINANCE_CONFRECUENCY', row.NFRECUENCY);
        $('#FINANCE_CONFRECUENCYDesc').val(row.NFRECUENCYDesc);

    };
    this.FINANCE_COTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/FINANCE_COTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                FINANCECONCONTRAT1: row.NCONTRAT,
                FINANCECODEFFECDATE2: row.DEFFECDATE
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
    this.PremiumTblExpandRow = function (index, row, $detail) {
       var tblparentid = $detail.parent().parent().parent()[0].id;
       var html = [];

        var detailShow = HT5ConsultaClientesSegunClienteIndicadoSupport.Detail_pre_ShowValidation(row);
        if (detailShow)
        html.push('<table id="Detail_preTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Detalle</caption></table>');
        html.push('<table id="Commiss_prTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Comisiones</caption></table>');
        html.push('<table id="Premium_moTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Movimiento</caption></table>');
        html.push('<table id="FINANCE_COTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Contrato de Financiamiento</caption></table>');

        $detail.html(html.join(""));
        if (detailShow)
        HT5ConsultaClientesSegunClienteIndicadoSupport.Detail_preTblSetup($detail.find('#Detail_preTbl-' + index));
        HT5ConsultaClientesSegunClienteIndicadoSupport.Commiss_prTblSetup($detail.find('#Commiss_prTbl-' + index));
        HT5ConsultaClientesSegunClienteIndicadoSupport.Premium_moTblSetup($detail.find('#Premium_moTbl-' + index));
        HT5ConsultaClientesSegunClienteIndicadoSupport.FINANCE_COTblSetup($detail.find('#FINANCE_COTbl-' + index));

    };
    this.PremiumTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            search: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            detailView: true,
            onExpandRow: HT5ConsultaClientesSegunClienteIndicadoSupport.PremiumTblExpandRow,

            columns: [{
                field: 'nBranch',
                title: 'Ramo',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.PremiumnBranch_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nBranchDesc',
                title: 'Ramo',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nProduct',
                title: 'Producto',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.PremiumnProduct_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nProductDesc',
                title: 'Producto',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nPolicy',
                title: 'Póliza',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.PremiumnPolicy_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nReceipt',
                title: 'Recibo',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.PremiumnReceipt_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nTratypei',
                title: 'Origen del recibo',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.PremiumnTratypei_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nTratypeiDesc',
                title: 'Origen del recibo',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nPremium',
                title: 'Monto de prima',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.PremiumnPremium_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nCurrency',
                title: 'Moneda',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.PremiumnCurrency_FormatterMaskData',
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
                field: 'nCollector',
                title: 'Encargado de Cobro',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.PremiumnCollector_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'sReject',
                title: 'Indicador de rechazo',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'dLimitDate',
                title: 'Fecha Límite de Pago',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center',
                visible: false
            }, {
                field: 'nWay_pay',
                title: 'Vía de Pago',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.PremiumnWay_pay_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nWay_payDesc',
                title: 'Vía de Pago',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'NINTERMED',
                title: 'Código de Productor',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.PremiumNINTERMED_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nParticip',
                title: 'Porcentaje de participación',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.PremiumnParticip_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nComamou',
                title: 'Monto de comisión',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.PremiumnComamou_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'dIssuedat',
                title: 'Fecha de Emisión',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center',
                visible: false
            }, {
                field: 'dExpirdat',
                title: 'Fecha de Vencimiento Recibo',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center',
                visible: false
            }, {
                field: 'nType',
                title: 'Tipo de factura',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForPremiumnTypeFormatter',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'sStatusva',
                title: 'Estado del Registro',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'sStatusvaDesc',
                title: 'Estado del Registro',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'nStatus_pre',
                title: 'Estado de la Factura',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.PremiumnStatus_pre_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nStatus_preDesc',
                title: 'Estado de la Factura',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'sManauti',
                title: 'Recibo manual o automático',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForPremiumsManautiFormatter',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'SCERTYPE',
                title: 'TipoDeRegistro',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'NDIGIT',
                title: 'DígitoDeControl',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.PremiumNDIGIT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NPAYNUMBE',
                title: 'NúmeroDePago',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.PremiumNPAYNUMBE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NCONTRAT',
                title: 'ContratoDeFinanciamiento',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.PremiumNCONTRAT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'DEFFECDATE',
                title: 'FechaDeEfectoDelRegistro',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center',
                visible: false
            }]
        });

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.PremiumTblRequest();
      };

    this.PremiumRowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#PremiumnBranch', row.nBranch);
        $('#PremiumnBranchDesc').val(row.nBranchDesc);
        AutoNumeric.set('#PremiumnProduct', row.nProduct);
        $('#PremiumnProductDesc').val(row.nProductDesc);
        AutoNumeric.set('#PremiumnPolicy', row.nPolicy);
        AutoNumeric.set('#PremiumnReceipt', row.nReceipt);
        AutoNumeric.set('#PremiumnTratypei', row.nTratypei);
        $('#PremiumnTratypeiDesc').val(row.nTratypeiDesc);
        AutoNumeric.set('#PremiumnPremium', row.nPremium);
        AutoNumeric.set('#PremiumnCurrency', row.nCurrency);
        $('#PremiumnCurrencyDesc').val(row.nCurrencyDesc);
        AutoNumeric.set('#PremiumnCollector', row.nCollector);
        $('#PremiumsReject').val(row.sReject);
        $('#PremiumdLimitDate').val(generalSupport.ToJavaScriptDateCustom(row.dLimitDate, generalSupport.DateFormat()));
        AutoNumeric.set('#PremiumnWay_pay', row.nWay_pay);
        $('#PremiumnWay_payDesc').val(row.nWay_payDesc);
        AutoNumeric.set('#PremiumNINTERMED', row.NINTERMED);
        AutoNumeric.set('#PremiumnParticip', row.nParticip);
        AutoNumeric.set('#PremiumnComamou', row.nComamou);
        $('#PremiumdIssuedat').val(generalSupport.ToJavaScriptDateCustom(row.dIssuedat, generalSupport.DateFormat()));
        $('#PremiumdExpirdat').val(generalSupport.ToJavaScriptDateCustom(row.dExpirdat, generalSupport.DateFormat()));
        $('#PremiumsStatusva').val(row.sStatusva);
        $('#PremiumsStatusvaDesc').val(row.sStatusvaDesc);
        AutoNumeric.set('#PremiumnStatus_pre', row.nStatus_pre);
        $('#PremiumnStatus_preDesc').val(row.nStatus_preDesc);
        $('#PremiumSCERTYPE').val(row.SCERTYPE);
        AutoNumeric.set('#PremiumNDIGIT', row.NDIGIT);
        AutoNumeric.set('#PremiumNPAYNUMBE', row.NPAYNUMBE);
        AutoNumeric.set('#PremiumNCONTRAT', row.NCONTRAT);
        $('#PremiumDEFFECDATE').val(generalSupport.ToJavaScriptDateCustom(row.DEFFECDATE, generalSupport.DateFormat()));

    };
    this.PremiumTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/PremiumTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                filter: '',
                PREMIUMSCLIENT3: row.SCLIENT
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
    this.CL_COVERTblSetup = function (table) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCL_COVERnDeman_type('');
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCL_COVERnCurrency('');
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCL_COVERsFrantype('');
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],

            columns: [{
                field: 'nCase_num',
                title: 'Caso',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.CL_COVERnCase_num_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nModulec',
                title: 'Módulo',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.CL_COVERnModulec_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nCover',
                title: 'Cobertura',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.CL_COVERnCover_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nDeman_type',
                title: 'Tipo de Reclamo',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCL_COVERnDeman_typeFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SCLIENAME',
                title: 'Cliente',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nDamProf',
                title: 'Estimado Profesional',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.CL_COVERnDamProf_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'sReservstat',
                title: 'Estado de Reserva',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'nReserve',
                title: 'Reserva',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.CL_COVERnReserve_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nCurrency',
                title: 'Moneda',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCL_COVERnCurrencyFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nRec_amount',
                title: 'Monto Recuperado',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.CL_COVERnRec_amount_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nPay_amount',
                title: 'Monto Pagado de la Reserva',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.CL_COVERnPay_amount_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nLoc_cos_re',
                title: 'Total de Gastos de Recuperación',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.CL_COVERnLoc_cos_re_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'sFrantype',
                title: 'Franquicia o Deducible',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCL_COVERsFrantypeFormatter',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'nFra_amount',
                title: 'Monto de Franquicia o Deducible',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.CL_COVERnFra_amount_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }]
        });

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.CL_COVERTblRequest();
      };

    this.CL_COVERRowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#CL_COVERnCase_num', row.nCase_num);
        AutoNumeric.set('#CL_COVERnModulec', row.nModulec);
        AutoNumeric.set('#CL_COVERnCover', row.nCover);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCL_COVERnDeman_type(row.nDeman_type, source);
        $('#CL_COVERSCLIENAME').val(row.SCLIENAME);
        AutoNumeric.set('#CL_COVERnDamProf', row.nDamProf);
        $('#CL_COVERsReservstat').val(row.sReservstat);
        AutoNumeric.set('#CL_COVERnReserve', row.nReserve);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCL_COVERnCurrency(row.nCurrency, source);
        AutoNumeric.set('#CL_COVERnRec_amount', row.nRec_amount);
        AutoNumeric.set('#CL_COVERnPay_amount', row.nPay_amount);
        AutoNumeric.set('#CL_COVERnLoc_cos_re', row.nLoc_cos_re);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCL_COVERsFrantype(row.sFrantype, source);
        AutoNumeric.set('#CL_COVERnFra_amount', row.nFra_amount);

    };
    this.CL_COVERTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/CL_COVERTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                CLCOVERNCLAIM1: row.NCLAIM,
                CLCOVERNCASENUM2: row.nCase_num,
                CLCOVERNDEMANTYPE3: row.nDeman_type
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
    this.CLAIM_ATTMTblSetup = function (table) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCLAIM_ATTMnDeman_type('');
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCLAIM_ATTMsIllness('');
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],

            columns: [{
                field: 'nCase_num',
                title: 'Número de Caso',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.CLAIM_ATTMnCase_num_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nDeman_type',
                title: 'Tipo de Reclamo',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCLAIM_ATTMnDeman_typeFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'sClient',
                title: 'Código de Cliente',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nService',
                title: 'Servicio',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.CLAIM_ATTMnService_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'sIllness',
                title: 'Enfermedad',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCLAIM_ATTMsIllnessFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'sClientProf',
                title: 'Código de Cliente del Médico',
                sortable: true,
                halign: 'center'
            }]
        });

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.CLAIM_ATTMTblRequest();
      };

    this.CLAIM_ATTMRowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#CLAIM_ATTMnCase_num', row.nCase_num);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCLAIM_ATTMnDeman_type(row.nDeman_type, source);
        $('#CLAIM_ATTMsClient').val(row.sClient);
        AutoNumeric.set('#CLAIM_ATTMnService', row.nService);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCLAIM_ATTMsIllness(row.sIllness, source);
        $('#CLAIM_ATTMsClientProf').val(row.sClientProf);

    };
    this.CLAIM_ATTMTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/CLAIM_ATTMTblDataLoad",
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
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],

            columns: [{
                field: 'nCase_num',
                title: 'Número de Caso',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Claim_autonCase_num_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nDeman_type',
                title: 'Tipo de Reclamo',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Claim_autonDeman_type_FormatterMaskData',
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
                field: 'sAlcoholic',
                title: 'Exceso de Alcohol',
                formatter: 'tableHelperSupport.IsCheck',
                sortable: true,
                halign: 'center'
            }, {
                field: 'sPoliceDem',
                title: 'Denuncia Policial',
                formatter: 'tableHelperSupport.IsCheck',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nInfraction',
                title: 'Indicador de Infracción',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Claim_autonInfraction_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nAuto_quant',
                title: 'Número de Vehículos Envueltos',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Claim_autonAuto_quant_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nNotenum',
                title: 'Número de Nota',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Claim_autonNotenum_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }]
        });

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.Claim_autoTblRequest();
      };

    this.Claim_autoRowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
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
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/Claim_autoTblDataLoad",
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
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],

            columns: [{
                field: 'NDAMAGE_COD',
                title: 'Repuesto',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.CLAIM_DAMANDAMAGE_COD_FormatterMaskData',
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
                title: 'Magnitud del Daño',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.CLAIM_DAMANMAG_DAM_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NMAG_DAMDesc',
                title: 'Magnitud del Daño',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NAMOUNT',
                title: 'Monto aproximado',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.CLAIM_DAMANAMOUNT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }]
        });

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.CLAIM_DAMATblRequest();
      };

    this.CLAIM_DAMARowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
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
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/CLAIM_DAMATblDataLoad",
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
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],

            columns: [{
                field: 'SLICENSE_TY',
                title: 'Tipo de Matrícula',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'SLICENSE_TYDesc',
                title: 'Tipo de Matrícula',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SREGIST',
                title: 'Licencia',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SCHASSIS',
                title: 'Chasis Tercero',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SMOTOR',
                title: 'Serial del Motor',
                sortable: true,
                halign: 'center'
            }, {
                field: 'STHIR_POLIC',
                title: 'Número de Póliza en Asegurador del Tercero',
                sortable: true,
                halign: 'center'
            }, {
                field: 'STHIR_CLAIM',
                title: 'Número de Siniestro en Asegurador del Tercero',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SRECOV_IND',
                title: 'Posibilidad de Recobro',
                formatter: 'tableHelperSupport.IsCheck',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NTHIR_COMP',
                title: 'Asegurador del Tercero',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.CLAIM_THIRNTHIR_COMP_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NBLAME',
                title: 'Culpabilidad',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.CLAIM_THIRNBLAME_FormatterMaskData',
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

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.CLAIM_THIRTblRequest();
      };

    this.CLAIM_THIRRowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
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
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/CLAIM_THIRTblDataLoad",
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
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],

            columns: [{
                field: 'nCase_num',
                title: 'Número de Caso',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Life_claimnCase_num_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nDeman_type',
                title: 'Tipo de Reclamo',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Life_claimnDeman_type_FormatterMaskData',
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
                field: 'nIn_lif_typ',
                title: 'Tipo de Indemnización',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Life_claimnIn_lif_typ_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nIn_lif_typDesc',
                title: 'Tipo de Indemnización',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nCla_li_typ',
                title: 'Tipo de Siniestro de Vida',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Life_claimnCla_li_typ_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nCla_li_typDesc',
                title: 'Tipo de Siniestro de Vida',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nMonth_amou',
                title: 'Pago Mensual',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Life_claimnMonth_amou_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'dEnd_date',
                title: 'Fecha Fin de Pagos',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }]
        });

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.Life_claimTblRequest();
      };

    this.Life_claimRowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
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
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/Life_claimTblDataLoad",
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
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],

            columns: [{
                field: 'NBENE_TYPE',
                title: 'Tipo de Beneficiario',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.CLAIMBENEFNBENE_TYPE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NBENE_TYPEDesc',
                title: 'Tipo de Beneficiario',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SCLIENT',
                title: 'Código de Cliente',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NRELATION',
                title: 'Nexo',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.CLAIMBENEFNRELATION_FormatterMaskData',
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
                title: 'Porcentaje de Participación',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.CLAIMBENEFNPARTICIP_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'SDEMANDANT',
                title: 'Indicador de Reclamante',
                formatter: 'tableHelperSupport.IsCheck',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NOFFICE_PAY',
                title: 'Enviar Cheque a Sucursal',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.CLAIMBENEFNOFFICE_PAY_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NOFFICE_PAYDesc',
                title: 'Enviar Cheque a Sucursal',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SCLIENT_REP',
                title: 'Representante Legal',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NOFFICEAGEN_PAY',
                title: 'Oficina',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.CLAIMBENEFNOFFICEAGEN_PAY_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NOFFICEAGEN_PAYDesc',
                title: 'Oficina',
                sortable: true,
                halign: 'center'
            }]
        });

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.CLAIMBENEFTblRequest();
      };

    this.CLAIMBENEFRowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#CLAIMBENEFNBENE_TYPE', row.NBENE_TYPE);
        $('#CLAIMBENEFNBENE_TYPEDesc').val(row.NBENE_TYPEDesc);
        $('#CLAIMBENEFSCLIENT').val(row.SCLIENT);
        AutoNumeric.set('#CLAIMBENEFNRELATION', row.NRELATION);
        $('#CLAIMBENEFNRELATIONDesc').val(row.NRELATIONDesc);
        AutoNumeric.set('#CLAIMBENEFNPARTICIP', row.NPARTICIP);
        $('#CLAIMBENEFSDEMANDANT').prop("checked", row.SDEMANDANT);
        AutoNumeric.set('#CLAIMBENEFNOFFICE_PAY', row.NOFFICE_PAY);
        $('#CLAIMBENEFNOFFICE_PAYDesc').val(row.NOFFICE_PAYDesc);
        $('#CLAIMBENEFSCLIENT_REP').val(row.SCLIENT_REP);
        AutoNumeric.set('#CLAIMBENEFNOFFICEAGEN_PAY', row.NOFFICEAGEN_PAY);
        $('#CLAIMBENEFNOFFICEAGEN_PAYDesc').val(row.NOFFICEAGEN_PAYDesc);

    };
    this.CLAIMBENEFTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/CLAIMBENEFTblDataLoad",
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
    this.CLAIM_HISTblSetup = function (table) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCLAIM_HISnOper_type('');
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCLAIM_HISnPay_type('');
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCLAIM_HISnPay_form('');
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCLAIM_HISnCurrency('');
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],

            columns: [{
                field: 'nCase_num',
                title: 'Caso',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.CLAIM_HISnCase_num_FormatterMaskData',
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
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.CLAIM_HISnTransac_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'SCLIENAME',
                title: 'Cliente',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nOper_type',
                title: 'Tipo de Operación',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCLAIM_HISnOper_typeFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nPay_type',
                title: 'Tipo de Pago',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCLAIM_HISnPay_typeFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nPay_form',
                title: 'Forma de Pago',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCLAIM_HISnPay_formFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nServ_order',
                title: 'Orden de Servicio',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.CLAIM_HISnServ_order_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'sInd_order',
                title: 'Orden de Pago',
                formatter: 'tableHelperSupport.IsCheck',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'sOrder_num',
                title: 'Número de Orden de Pago o cheque',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'sExecuted',
                title: 'Pago Realizado',
                formatter: 'tableHelperSupport.IsCheck',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'nAmount',
                title: 'Monto',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.CLAIM_HISnAmount_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nCurrency',
                title: 'Moneda',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCLAIM_HISnCurrencyFormatter',
                sortable: true,
                halign: 'center',
                visible: false
            }]
        });

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.CLAIM_HISTblRequest();
      };

    this.CLAIM_HISRowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#CLAIM_HISnCase_num', row.nCase_num);
        $('#CLAIM_HISdOperdate').val(generalSupport.ToJavaScriptDateCustom(row.dOperdate, generalSupport.DateFormat()));
        AutoNumeric.set('#CLAIM_HISnTransac', row.nTransac);
        $('#CLAIM_HISSCLIENAME').val(row.SCLIENAME);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCLAIM_HISnOper_type(row.nOper_type, source);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCLAIM_HISnPay_type(row.nPay_type, source);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCLAIM_HISnPay_form(row.nPay_form, source);
        AutoNumeric.set('#CLAIM_HISnServ_order', row.nServ_order);
        $('#CLAIM_HISsInd_order').prop("checked", row.sInd_order);
        $('#CLAIM_HISsOrder_num').val(row.sOrder_num);
        $('#CLAIM_HISsExecuted').prop("checked", row.sExecuted);
        AutoNumeric.set('#CLAIM_HISnAmount', row.nAmount);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCLAIM_HISnCurrency(row.nCurrency, source);

    };
    this.CLAIM_HISTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/CLAIM_HISTblDataLoad",
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
    this.CLAIM_CASETblExpandRow = function (index, row, $detail) {
       var tblparentid = $detail.parent().parent().parent()[0].id;
       var html = [];

        var detailShow = HT5ConsultaClientesSegunClienteIndicadoSupport.CL_COVER_ShowValidation(row);
        if (detailShow)
        html.push('<table id="CL_COVERTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Cobertura afectada</caption></table>');
        html.push('<table id="CLAIM_ATTMTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Atención Médica</caption></table>');
        html.push('<table id="Claim_autoTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Automóvil</caption></table>');
        html.push('<table id="CLAIM_DAMATbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Auto</caption></table>');
        html.push('<table id="CLAIM_THIRTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Terceros-Auto</caption></table>');
        html.push('<table id="Life_claimTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Vida</caption></table>');
        html.push('<table id="CLAIMBENEFTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Beneficiario</caption></table>');
        html.push('<table id="CLAIM_HISTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Historia</caption></table>');

        $detail.html(html.join(""));
        if (detailShow)
        HT5ConsultaClientesSegunClienteIndicadoSupport.CL_COVERTblSetup($detail.find('#CL_COVERTbl-' + index));
        HT5ConsultaClientesSegunClienteIndicadoSupport.CLAIM_ATTMTblSetup($detail.find('#CLAIM_ATTMTbl-' + index));
        HT5ConsultaClientesSegunClienteIndicadoSupport.Claim_autoTblSetup($detail.find('#Claim_autoTbl-' + index));
        HT5ConsultaClientesSegunClienteIndicadoSupport.CLAIM_DAMATblSetup($detail.find('#CLAIM_DAMATbl-' + index));
        HT5ConsultaClientesSegunClienteIndicadoSupport.CLAIM_THIRTblSetup($detail.find('#CLAIM_THIRTbl-' + index));
        HT5ConsultaClientesSegunClienteIndicadoSupport.Life_claimTblSetup($detail.find('#Life_claimTbl-' + index));
        HT5ConsultaClientesSegunClienteIndicadoSupport.CLAIMBENEFTblSetup($detail.find('#CLAIMBENEFTbl-' + index));
        HT5ConsultaClientesSegunClienteIndicadoSupport.CLAIM_HISTblSetup($detail.find('#CLAIM_HISTbl-' + index));

    };
    this.CLAIM_CASETblSetup = function (table) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCLAIM_CASEnDeman_type('');
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCLAIM_CASEsStaCase('');
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            detailView: true,
            onExpandRow: HT5ConsultaClientesSegunClienteIndicadoSupport.CLAIM_CASETblExpandRow,

            columns: [{
                field: 'nCase_num',
                title: 'Número de Caso',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.CLAIM_CASEnCase_num_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nDeman_type',
                title: 'Tipo de Reclamo',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCLAIM_CASEnDeman_typeFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'sStaCase',
                title: 'Estado del Caso',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCLAIM_CASEsStaCaseFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nNoteDama',
                title: 'Número de Nota',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.CLAIM_CASEnNoteDama_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NCLAIM',
                title: 'Número del siniestro.',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.CLAIM_CASENCLAIM_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }]
        });

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.CLAIM_CASETblRequest();
      };

    this.CLAIM_CASERowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#CLAIM_CASEnCase_num', row.nCase_num);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCLAIM_CASEnDeman_type(row.nDeman_type, source);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCLAIM_CASEsStaCase(row.sStaCase, source);
        AutoNumeric.set('#CLAIM_CASEnNoteDama', row.nNoteDama);
        AutoNumeric.set('#CLAIM_CASENCLAIM', row.NCLAIM);

    };
    this.CLAIM_CASETblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/CLAIM_CASETblDataLoad",
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
    this.ClaimTblExpandRow = function (index, row, $detail) {
       var tblparentid = $detail.parent().parent().parent()[0].id;
       var html = [];

        var detailShow = HT5ConsultaClientesSegunClienteIndicadoSupport.CLAIM_CASE_ShowValidation(row);
        if (detailShow)
        html.push('<table id="CLAIM_CASETbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Caso del Siniestro</caption></table>');

        $detail.html(html.join(""));
        if (detailShow)
        HT5ConsultaClientesSegunClienteIndicadoSupport.CLAIM_CASETblSetup($detail.find('#CLAIM_CASETbl-' + index));

    };
    this.ClaimTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            search: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            detailView: true,
            onExpandRow: HT5ConsultaClientesSegunClienteIndicadoSupport.ClaimTblExpandRow,

            columns: [{
                field: 'nBranch',
                title: 'Ramo',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.ClaimnBranch_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nProduct',
                title: 'Producto',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.ClaimnProduct_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nPolicy',
                title: 'Póliza',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.ClaimnPolicy_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nClaim',
                title: 'Siniestro',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.ClaimnClaim_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'dDecladat',
                title: 'Fecha de Declaración',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'sClaimtyp',
                title: 'Tipo de Pérdida',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'sClaimtypDesc',
                title: 'Tipo de Pérdida',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nCausecod',
                title: 'Causa del Siniestro',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.ClaimnCausecod_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nCausecodDesc',
                title: 'Causa del Siniestro',
                sortable: true,
                halign: 'center'
            }, {
                field: 'sStaclaim',
                title: 'Estado del Siniestro',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'sStaclaimDesc',
                title: 'Estado del Siniestro',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nUnaccode',
                title: 'Causa de Rechazo de Siniestro',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.ClaimnUnaccode_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nUnaccodeDesc',
                title: 'Causa de Rechazo de Siniestro',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'dPrescdat',
                title: 'Fecha de Prescripción',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center',
                visible: false
            }, {
                field: 'dOccurdat',
                title: 'Fecha de Ocurrencia',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center',
                visible: false
            }, {
                field: 'nLoc_reserv',
                title: 'Reserva Actual',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.ClaimnLoc_reserv_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nLoc_rec_am',
                title: 'Monto Recuperado',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.ClaimnLoc_rec_am_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nLoc_pay_am',
                title: 'Monto Pagado',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.ClaimnLoc_pay_am_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nLoc_out_am',
                title: 'Reserva Pendiente',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.ClaimnLoc_out_am_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nLoc_cos_re',
                title: 'Total de Gastos de Recuperación',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.ClaimnLoc_cos_re_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'dCompdate',
                title: 'Fecha de Actualización del Registro',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center',
                visible: false
            }]
        });

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.ClaimTblRequest();
      };

    this.ClaimRowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#ClaimnBranch', row.nBranch);
        AutoNumeric.set('#ClaimnProduct', row.nProduct);
        AutoNumeric.set('#ClaimnPolicy', row.nPolicy);
        AutoNumeric.set('#ClaimnClaim', row.nClaim);
        $('#ClaimdDecladat').val(generalSupport.ToJavaScriptDateCustom(row.dDecladat, generalSupport.DateFormat()));
        $('#ClaimsClaimtyp').val(row.sClaimtyp);
        $('#ClaimsClaimtypDesc').val(row.sClaimtypDesc);
        AutoNumeric.set('#ClaimnCausecod', row.nCausecod);
        $('#ClaimnCausecodDesc').val(row.nCausecodDesc);
        $('#ClaimsStaclaim').val(row.sStaclaim);
        $('#ClaimsStaclaimDesc').val(row.sStaclaimDesc);
        AutoNumeric.set('#ClaimnUnaccode', row.nUnaccode);
        $('#ClaimnUnaccodeDesc').val(row.nUnaccodeDesc);
        $('#ClaimdPrescdat').val(generalSupport.ToJavaScriptDateCustom(row.dPrescdat, generalSupport.DateFormat()));
        $('#ClaimdOccurdat').val(generalSupport.ToJavaScriptDateCustom(row.dOccurdat, generalSupport.DateFormat()));
        AutoNumeric.set('#ClaimnLoc_reserv', row.nLoc_reserv);
        AutoNumeric.set('#ClaimnLoc_rec_am', row.nLoc_rec_am);
        AutoNumeric.set('#ClaimnLoc_pay_am', row.nLoc_pay_am);
        AutoNumeric.set('#ClaimnLoc_out_am', row.nLoc_out_am);
        AutoNumeric.set('#ClaimnLoc_cos_re', row.nLoc_cos_re);
        $('#ClaimdCompdate').val(generalSupport.ToJavaScriptDateCustom(row.dCompdate, generalSupport.DateFormat()));

    };
    this.ClaimTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/ClaimTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                filter: '',
                CLAIMSCLIENT1: row.SCLIENT
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
    this.PHONESTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],

            columns: [{
                field: 'NPHONE_TYPE',
                title: 'Tipo de Teléfono',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.PHONESNPHONE_TYPE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NPHONE_TYPEDesc',
                title: 'Tipo de Teléfono',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NAREA_CODE',
                title: 'Área',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.PHONESNAREA_CODE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'SPHONE',
                title: 'Número telefónico',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NEXTENS1',
                title: 'Número de extensión telefónica',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.PHONESNEXTENS1_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NEXTENS2',
                title: 'Número de extensión telefónica',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.PHONESNEXTENS2_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NBESTTIMETOCALL',
                title: 'Mejor Hora para Llamar',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.PHONESNBESTTIMETOCALL_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NBESTTIMETOCALLDesc',
                title: 'Mejor Hora para Llamar',
                sortable: true,
                halign: 'center'
            }]
        });

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.PHONESTblRequest();
      };

    this.PHONESRowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#PHONESNPHONE_TYPE', row.NPHONE_TYPE);
        $('#PHONESNPHONE_TYPEDesc').val(row.NPHONE_TYPEDesc);
        AutoNumeric.set('#PHONESNAREA_CODE', row.NAREA_CODE);
        $('#PHONESSPHONE').val(row.SPHONE);
        AutoNumeric.set('#PHONESNEXTENS1', row.NEXTENS1);
        AutoNumeric.set('#PHONESNEXTENS2', row.NEXTENS2);
        AutoNumeric.set('#PHONESNBESTTIMETOCALL', row.NBESTTIMETOCALL);
        $('#PHONESNBESTTIMETOCALLDesc').val(row.NBESTTIMETOCALLDesc);

    };
    this.PHONESTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/PHONESTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                PHONESNRECOWNER1: row.NRECOWNER,
                PHONESSKEYADDRESS2: row.SKEYADDRESS,
                PHONESDEFFECDATE3: row.DEFFECDATE
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
    this.AddressTblExpandRow = function (index, row, $detail) {
       var tblparentid = $detail.parent().parent().parent()[0].id;
       var html = [];

        var detailShow = HT5ConsultaClientesSegunClienteIndicadoSupport.PHONES_ShowValidation(row);
        if (detailShow)
        html.push('<table id="PHONESTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Teléfono</caption></table>');

        $detail.html(html.join(""));
        if (detailShow)
        HT5ConsultaClientesSegunClienteIndicadoSupport.PHONESTblSetup($detail.find('#PHONESTbl-' + index));

    };
    this.AddressTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            detailView: true,
            onExpandRow: HT5ConsultaClientesSegunClienteIndicadoSupport.AddressTblExpandRow,

            columns: [{
                field: 'SRECTYPE',
                title: 'Tipo de dirección',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nProvince',
                title: 'Región',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.AddressnProvince_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nProvinceDesc',
                title: 'Región',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nLocal',
                title: 'Ciudad',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.AddressnLocal_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nLocalDesc',
                title: 'Ciudad',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nCountry',
                title: 'País',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.AddressnCountry_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nCountryDesc',
                title: 'País',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nMunicipality',
                title: 'Municipalidad',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.AddressnMunicipality_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nMunicipalityDesc',
                title: 'Municipalidad',
                sortable: true,
                halign: 'center'
            }, {
                field: 'sDescAdd',
                title: 'Dirección Completa',
                sortable: true,
                halign: 'center'
            }, {
                field: 'sE_mail',
                title: 'Email',
                sortable: true,
                halign: 'center'
            }, {
                field: 'sStreet',
                title: 'Calle o URL',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'sStreet1',
                title: 'Calle',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'sBuild',
                title: 'Edificio',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'sDepartment',
                title: 'Apartamento',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'sPobox',
                title: 'Buzón de Correo',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'NRECOWNER',
                title: 'PropietarioDelRegistro',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.AddressNRECOWNER_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'SKEYADDRESS',
                title: 'ClaveDeAccesoADirección',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'DEFFECDATE',
                title: 'FechaDeEfectoDelRegistro',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center',
                visible: false
            }]
        });

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.AddressTblRequest();
      };

    this.AddressRowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
        $('#AddressSRECTYPE').val(row.SRECTYPE);
        AutoNumeric.set('#AddressnProvince', row.nProvince);
        $('#AddressnProvinceDesc').val(row.nProvinceDesc);
        AutoNumeric.set('#AddressnLocal', row.nLocal);
        $('#AddressnLocalDesc').val(row.nLocalDesc);
        AutoNumeric.set('#AddressnCountry', row.nCountry);
        $('#AddressnCountryDesc').val(row.nCountryDesc);
        AutoNumeric.set('#AddressnMunicipality', row.nMunicipality);
        $('#AddressnMunicipalityDesc').val(row.nMunicipalityDesc);
        $('#AddresssDescAdd').val(row.sDescAdd);
        $('#AddresssE_mail').val(row.sE_mail);
        $('#AddresssStreet').val(row.sStreet);
        $('#AddresssStreet1').val(row.sStreet1);
        $('#AddresssBuild').val(row.sBuild);
        $('#AddresssDepartment').val(row.sDepartment);
        $('#AddresssPobox').val(row.sPobox);
        AutoNumeric.set('#AddressNRECOWNER', row.NRECOWNER);
        $('#AddressSKEYADDRESS').val(row.SKEYADDRESS);
        $('#AddressDEFFECDATE').val(generalSupport.ToJavaScriptDateCustom(row.DEFFECDATE, generalSupport.DateFormat()));

    };
    this.AddressTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/AddressTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                ADDRESSSCLIENT1: row.SCLIENT
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
    this.CLIDOCUMENTSTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],

            columns: [{
                field: 'NTYPCLIENTDOC',
                title: 'Tipo de documento',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.CLIDOCUMENTSNTYPCLIENTDOC_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'SCLINUMDOCU',
                title: 'Número o código de documento',
                sortable: true,
                halign: 'center'
            }, {
                field: 'DISSUEDAT',
                title: 'Fecha de emisión',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'DEXPIRDAT',
                title: 'Fecha de vencimiento',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }]
        });

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.CLIDOCUMENTSTblRequest();
      };

    this.CLIDOCUMENTSRowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#CLIDOCUMENTSNTYPCLIENTDOC', row.NTYPCLIENTDOC);
        $('#CLIDOCUMENTSSCLINUMDOCU').val(row.SCLINUMDOCU);
        $('#CLIDOCUMENTSDISSUEDAT').val(generalSupport.ToJavaScriptDateCustom(row.DISSUEDAT, generalSupport.DateFormat()));
        $('#CLIDOCUMENTSDEXPIRDAT').val(generalSupport.ToJavaScriptDateCustom(row.DEXPIRDAT, generalSupport.DateFormat()));

    };
    this.CLIDOCUMENTSTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/CLIDOCUMENTSTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                CLIDOCUMENTSSCLIENT1: row.SCLIENT
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
    this.DIR_DEBIT_CLITblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],

            columns: [{
                field: 'NBANKEXT',
                title: 'Banco',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.DIR_DEBIT_CLINBANKEXT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NBANKEXTDesc',
                title: 'Banco',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SACCOUNT',
                title: 'Cuenta',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NBILL_DAY',
                title: 'Día de pago',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.DIR_DEBIT_CLINBILL_DAY_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'DNULLDATE',
                title: 'Fecha de Anulación',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'DEFFECDATE',
                title: 'Fecha de Efecto',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }]
        });

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.DIR_DEBIT_CLITblRequest();
      };

    this.DIR_DEBIT_CLIRowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#DIR_DEBIT_CLINBANKEXT', row.NBANKEXT);
        $('#DIR_DEBIT_CLINBANKEXTDesc').val(row.NBANKEXTDesc);
        $('#DIR_DEBIT_CLISACCOUNT').val(row.SACCOUNT);
        AutoNumeric.set('#DIR_DEBIT_CLINBILL_DAY', row.NBILL_DAY);
        $('#DIR_DEBIT_CLIDNULLDATE').val(generalSupport.ToJavaScriptDateCustom(row.DNULLDATE, generalSupport.DateFormat()));
        $('#DIR_DEBIT_CLIDEFFECDATE').val(generalSupport.ToJavaScriptDateCustom(row.DEFFECDATE, generalSupport.DateFormat()));

    };
    this.DIR_DEBIT_CLITblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/DIR_DEBIT_CLITblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                DIRDEBITCLISCLIENT1: row.SCLIENT
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
    this.Bk_accountTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],

            columns: [{
                field: 'nTyp_acc',
                title: 'Tipo de Cuenta Bancaria',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Bk_accountnTyp_acc_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nTyp_accDesc',
                title: 'Tipo de Cuenta Bancaria',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nBankExt',
                title: 'Banco',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Bk_accountnBankExt_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nBankExtDesc',
                title: 'Banco',
                sortable: true,
                halign: 'center'
            }, {
                field: 'sAccount',
                title: 'Cuenta Bancaria',
                sortable: true,
                halign: 'center'
            }, {
                field: 'sStatregt',
                title: 'Estado del Registro',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'sStatregtDesc',
                title: 'Estado del Registro',
                sortable: true,
                halign: 'center'
            }]
        });

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.Bk_accountTblRequest();
      };

    this.Bk_accountRowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#Bk_accountnTyp_acc', row.nTyp_acc);
        $('#Bk_accountnTyp_accDesc').val(row.nTyp_accDesc);
        AutoNumeric.set('#Bk_accountnBankExt', row.nBankExt);
        $('#Bk_accountnBankExtDesc').val(row.nBankExtDesc);
        $('#Bk_accountsAccount').val(row.sAccount);
        $('#Bk_accountsStatregt').val(row.sStatregt);
        $('#Bk_accountsStatregtDesc').val(row.sStatregtDesc);

    };
    this.Bk_accountTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/Bk_accountTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                BKACCOUNTSCLIENT1: row.SCLIENT
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
    this.Cred_cardTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],

            columns: [{
                field: 'sCredi_Card',
                title: 'Tarjeta de Crédito',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nCard_type',
                title: 'Tipo',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Cred_cardnCard_type_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nCard_typeDesc',
                title: 'Tipo',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nBankExt',
                title: 'Banco',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Cred_cardnBankExt_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nBankExtDesc',
                title: 'Banco',
                sortable: true,
                halign: 'center'
            }, {
                field: 'dCardExpir',
                title: 'Fecha de Vencimiento',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }]
        });

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.Cred_cardTblRequest();
      };

    this.Cred_cardRowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
        $('#Cred_cardsCredi_Card').val(row.sCredi_Card);
        AutoNumeric.set('#Cred_cardnCard_type', row.nCard_type);
        $('#Cred_cardnCard_typeDesc').val(row.nCard_typeDesc);
        AutoNumeric.set('#Cred_cardnBankExt', row.nBankExt);
        $('#Cred_cardnBankExtDesc').val(row.nBankExtDesc);
        $('#Cred_carddCardExpir').val(generalSupport.ToJavaScriptDateCustom(row.dCardExpir, generalSupport.DateFormat()));

    };
    this.Cred_cardTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/Cred_cardTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                CREDCARDSCLIENT1: row.SCLIENT
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
    this.Curr_accTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],

            columns: [{
                field: 'nTyp_acco',
                title: 'Tipo de Cuenta Corriente',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Curr_accnTyp_acco_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nTyp_accoDesc',
                title: 'Tipo de Cuenta Corriente',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nDebit',
                title: 'Monto de débito',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Curr_accnDebit_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nCredit',
                title: 'Monto de crédito',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Curr_accnCredit_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nBalance',
                title: 'Balance',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Curr_accnBalance_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nCurrency',
                title: 'Moneda',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Curr_accnCurrency_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nCurrencyDesc',
                title: 'Moneda',
                sortable: true,
                halign: 'center'
            }]
        });

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.Curr_accTblRequest();
      };

    this.Curr_accRowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#Curr_accnTyp_acco', row.nTyp_acco);
        $('#Curr_accnTyp_accoDesc').val(row.nTyp_accoDesc);
        AutoNumeric.set('#Curr_accnDebit', row.nDebit);
        AutoNumeric.set('#Curr_accnCredit', row.nCredit);
        AutoNumeric.set('#Curr_accnBalance', row.nBalance);
        AutoNumeric.set('#Curr_accnCurrency', row.nCurrency);
        $('#Curr_accnCurrencyDesc').val(row.nCurrencyDesc);

    };
    this.Curr_accTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/Curr_accTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                CURRACCSCLIENT1: row.SCLIENT
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
    this.SportTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],

            columns: [{
                field: 'nSport',
                title: 'Deporte',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.SportnSport_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nSportDesc',
                title: 'Deporte',
                sortable: true,
                halign: 'center'
            }]
        });

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.SportTblRequest();
      };

    this.SportRowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#SportnSport', row.nSport);
        $('#SportnSportDesc').val(row.nSportDesc);

    };
    this.SportTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/SportTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                SPORTSCLIENT1: row.SCLIENT
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
    this.HobbyTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],

            columns: [{
                field: 'nHobby',
                title: 'Hobby',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.HobbynHobby_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nHobbyDesc',
                title: 'Hobby',
                sortable: true,
                halign: 'center'
            }]
        });

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.HobbyTblRequest();
      };

    this.HobbyRowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#HobbynHobby', row.nHobby);
        $('#HobbynHobbyDesc').val(row.nHobbyDesc);

    };
    this.HobbyTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/HobbyTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                HOBBYSCLIENT1: row.SCLIENT
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
    this.Financ_cliTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],

            columns: [{
                field: 'nConcept',
                title: 'Concepto',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Financ_clinConcept_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nConceptDesc',
                title: 'Concepto',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nAmount',
                title: 'Monto',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Financ_clinAmount_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nCurrency',
                title: 'Moneda',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Financ_clinCurrency_FormatterMaskData',
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
                field: 'nUnits',
                title: 'Unidades',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Financ_clinUnits_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nFinanStat',
                title: 'Estado',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.Financ_clinFinanStat_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nFinanStatDesc',
                title: 'Estado',
                sortable: true,
                halign: 'center'
            }]
        });

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.Financ_cliTblRequest();
      };

    this.Financ_cliRowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#Financ_clinConcept', row.nConcept);
        $('#Financ_clinConceptDesc').val(row.nConceptDesc);
        AutoNumeric.set('#Financ_clinAmount', row.nAmount);
        AutoNumeric.set('#Financ_clinCurrency', row.nCurrency);
        $('#Financ_clinCurrencyDesc').val(row.nCurrencyDesc);
        AutoNumeric.set('#Financ_clinUnits', row.nUnits);
        AutoNumeric.set('#Financ_clinFinanStat', row.nFinanStat);
        $('#Financ_clinFinanStatDesc').val(row.nFinanStatDesc);

    };
    this.Financ_cliTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/Financ_cliTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                FINANCCLISCLIENT1: row.SCLIENT
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
    this.RELATIONSTblSetup = function (table) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForRELATIONSnRelaship('');
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],

            columns: [{
                field: 'nRelaship',
                title: 'Nexo',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForRELATIONSnRelashipFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SCLIENAME',
                title: 'Cliente relacionado',
                sortable: true,
                halign: 'center'
            }]
        });

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.RELATIONSTblRequest();
      };

    this.RELATIONSRowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForRELATIONSnRelaship(row.nRelaship, source);
        $('#RELATIONSSCLIENAME').val(row.SCLIENAME);

    };
    this.RELATIONSTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/RELATIONSTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                RELATIONSSCLIENT1: row.SCLIENT
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
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCHEQUESSREQUEST_TY('');
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCHEQUESNCURRENCYPAY('');
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCHEQUESNCONCEPT('');
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCHEQUESNSTA_CHEQUE('');
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCHEQUESNOFFICE('');
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCHEQUESNOFFICEAGEN('');
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCHEQUESNTYPESUPPORT('');
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            showExport: true,
            exportDataType: 'all',
            exportOptions: { maxNestedTables: 0 },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],

            columns: [{
                field: 'SREQUEST_TY',
                title: 'Tipo',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCHEQUESSREQUEST_TYFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NREQUEST_NU',
                title: 'Orden de Pago',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.CHEQUESNREQUEST_NU_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'SCHEQUE',
                title: 'Cheque',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NAMOUNT',
                title: 'Monto',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.CHEQUESNAMOUNT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NCURRENCYPAY',
                title: 'Moneda del Pago',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCHEQUESNCURRENCYPAYFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NCONCEPT',
                title: 'Concepto',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCHEQUESNCONCEPTFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NSTA_CHEQUE',
                title: 'Estado',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCHEQUESNSTA_CHEQUEFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SCLIENAME',
                title: 'Beneficiario del pago',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NOFFICE',
                title: 'Sucursal de Entrega',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCHEQUESNOFFICEFormatter',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'NOFFICEAGEN',
                title: 'Oficina de Entrega',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCHEQUESNOFFICEAGENFormatter',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'NAGENCY',
                title: 'Agencia de Entrega',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.CHEQUESNAGENCY_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NTYPESUPPORT',
                title: 'Documento de Soporte',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCHEQUESNTYPESUPPORTFormatter',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'NDOCSUPPORT',
                title: 'Número de Documento',
                formatter: 'HT5ConsultaClientesSegunClienteIndicadoSupport.CHEQUESNDOCSUPPORT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'DSTAT_DATE',
                title: 'Fecha del estado',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center',
                visible: false
            }, {
                field: 'SDESCRIPT',
                title: 'Descripción Breve de Razón de Pago',
                sortable: true,
                halign: 'center',
                visible: false
            }]
        });

        HT5ConsultaClientesSegunClienteIndicadoSupport.$el = table;
        HT5ConsultaClientesSegunClienteIndicadoSupport.CHEQUESTblRequest();
      };

    this.CHEQUESRowToInput = function (row) {
        HT5ConsultaClientesSegunClienteIndicadoSupport.currentRow = row;
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCHEQUESSREQUEST_TY(row.SREQUEST_TY, source);
        AutoNumeric.set('#CHEQUESNREQUEST_NU', row.NREQUEST_NU);
        $('#CHEQUESSCHEQUE').val(row.SCHEQUE);
        AutoNumeric.set('#CHEQUESNAMOUNT', row.NAMOUNT);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCHEQUESNCURRENCYPAY(row.NCURRENCYPAY, source);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCHEQUESNCONCEPT(row.NCONCEPT, source);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCHEQUESNSTA_CHEQUE(row.NSTA_CHEQUE, source);
        $('#CHEQUESSCLIENAME').val(row.SCLIENAME);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCHEQUESNOFFICE(row.NOFFICE, source);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCHEQUESNOFFICEAGEN(row.NOFFICEAGEN, source);
        AutoNumeric.set('#CHEQUESNAGENCY', row.NAGENCY);
        HT5ConsultaClientesSegunClienteIndicadoSupport.LookUpForCHEQUESNTYPESUPPORT(row.NTYPESUPPORT, source);
        AutoNumeric.set('#CHEQUESNDOCSUPPORT', row.NDOCSUPPORT);
        $('#CHEQUESDSTAT_DATE').val(generalSupport.ToJavaScriptDateCustom(row.DSTAT_DATE, generalSupport.DateFormat()));
        $('#CHEQUESSDESCRIPT').val(row.SDESCRIPT);

    };
    this.CHEQUESTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultaClientesSegunClienteIndicadoActions.aspx/CHEQUESTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                CHEQUESSCLIENT1: row.SCLIENT
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

        var detailShow = HT5ConsultaClientesSegunClienteIndicadoSupport.Certificat_ShowValidation(row);
        if (detailShow)
        html.push('<table id="CertificatTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Póliza</caption></table>');
        html.push('<table id="PremiumTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Primas</caption></table>');
        html.push('<table id="ClaimTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Siniestro</caption></table>');
        html.push('<table id="AddressTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Dirección</caption></table>');
        html.push('<table id="CLIDOCUMENTSTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Documento</caption></table>');
        html.push('<table id="DIR_DEBIT_CLITbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Pago Automático</caption></table>');
        html.push('<table id="Bk_accountTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Cuenta Bancaria</caption></table>');
        html.push('<table id="Cred_cardTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Tarjeta de Crédito</caption></table>');
        html.push('<table id="Curr_accTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Cuenta Corriente</caption></table>');
        html.push('<table id="SportTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Deporte</caption></table>');
        html.push('<table id="HobbyTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Hobby</caption></table>');
        html.push('<table id="Financ_cliTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Informacion Financiera</caption></table>');
        html.push('<table id="RELATIONSTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Relación con otros clientes</caption></table>');
        html.push('<table id="CHEQUESTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Cheques</caption></table>');

        $detail.html(html.join(""));
        if (detailShow)
        HT5ConsultaClientesSegunClienteIndicadoSupport.CertificatTblSetup($detail.find('#CertificatTbl-' + index));
        HT5ConsultaClientesSegunClienteIndicadoSupport.PremiumTblSetup($detail.find('#PremiumTbl-' + index));
        HT5ConsultaClientesSegunClienteIndicadoSupport.ClaimTblSetup($detail.find('#ClaimTbl-' + index));
        HT5ConsultaClientesSegunClienteIndicadoSupport.AddressTblSetup($detail.find('#AddressTbl-' + index));
        HT5ConsultaClientesSegunClienteIndicadoSupport.CLIDOCUMENTSTblSetup($detail.find('#CLIDOCUMENTSTbl-' + index));
        HT5ConsultaClientesSegunClienteIndicadoSupport.DIR_DEBIT_CLITblSetup($detail.find('#DIR_DEBIT_CLITbl-' + index));
        HT5ConsultaClientesSegunClienteIndicadoSupport.Bk_accountTblSetup($detail.find('#Bk_accountTbl-' + index));
        HT5ConsultaClientesSegunClienteIndicadoSupport.Cred_cardTblSetup($detail.find('#Cred_cardTbl-' + index));
        HT5ConsultaClientesSegunClienteIndicadoSupport.Curr_accTblSetup($detail.find('#Curr_accTbl-' + index));
        HT5ConsultaClientesSegunClienteIndicadoSupport.SportTblSetup($detail.find('#SportTbl-' + index));
        HT5ConsultaClientesSegunClienteIndicadoSupport.HobbyTblSetup($detail.find('#HobbyTbl-' + index));
        HT5ConsultaClientesSegunClienteIndicadoSupport.Financ_cliTblSetup($detail.find('#Financ_cliTbl-' + index));
        HT5ConsultaClientesSegunClienteIndicadoSupport.RELATIONSTblSetup($detail.find('#RELATIONSTbl-' + index));
        HT5ConsultaClientesSegunClienteIndicadoSupport.CHEQUESTblSetup($detail.find('#CHEQUESTbl-' + index));

    };


    this.ClientnCivilSta_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.ClientnWeight_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 2,
            minimumValue: "-99999"
        });
      };
    this.ClientnHeight_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "9999",
            decimalPlaces: 2,
            minimumValue: "-9999"
        });
      };
    this.ClientnLanguage_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.ClientnMailingPref_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.ClientnTitle_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.ClientnSpeciality_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "9999999999",
            decimalPlaces: 0,
            minimumValue: "-9999999999"
        });
      };
    this.ClientnNationality_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.ClientnClass_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.CertificatnBranch_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.CertificatnProduct_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.CertificatnCapital_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999999999999"
        });
      };
    this.CertificatnPayfreq_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.CertificatnWait_code_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.CertificatnNullcode_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.CertificatNCERTIF_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "9",
            decimalPlaces: 0,
            minimumValue: "-9"
        });
      };
    this.Curren_polnCurrency_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.ROLESnRating_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.SUM_INSURNSUMINS_COD_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.SUM_INSURNSUMINS_REAL_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999999999999"
        });
      };
    this.SUM_INSURNCOINSURAN_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 2,
            minimumValue: "-99999"
        });
      };
    this.SUM_INSURNSUM_INSUR_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999999999999"
        });
      };
    this.COVERnCapital_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 0,
            minimumValue: "-999999999999999999"
        });
      };
    this.COVERnPremium_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999999999999"
        });
      };
    this.REINSURANNBRANCH_REI_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.REINSURANNTYPE_REIN_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.REINSURANNCAPITAL_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999999999999"
        });
      };
    this.REINSURANNCURRENCY_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.REINSURANNSHARE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999"
        });
      };
    this.REINSURAN2NBRANCH_REI_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.REINSURAN2NCOMPANY_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.REINSURAN2NCAPITAL_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999999999999"
        });
      };
    this.REINSURAN2NCURRENCY_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.REINSURAN2NSHARE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999"
        });
      };
    this.REINSURAN2NCOMMISSI_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "9999",
            decimalPlaces: 2,
            minimumValue: "-9999"
        });
      };
    this.Disc_xpremnDisc_code_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.Disc_xpremnCause_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.Disc_xpremnPercent_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 2,
            minimumValue: "-99999"
        });
      };
    this.Disc_xpremnCurrency_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.Disc_xpremnAmount_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999999999999"
        });
      };
    this.AutonVehType_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.AutonCapital_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999999999999"
        });
      };
    this.AutonVeh_valor_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999999999999"
        });
      };
    this.AutonYear_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.AutonAutoZone_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "9999999999",
            decimalPlaces: 0,
            minimumValue: "-9999999999"
        });
      };
    this.AutonUse_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.FirenConstCat_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.FirenActivityCat_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.FirenFloor_quan_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.FirenRoofType_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.FirenSeismicZone_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.FirenBuildType_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.FirenSpCombType_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.FirenHurrican_zone_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.FirenSideCloseType_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.HomeOwnernOwnerShip_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.HomeOwnernDwellingType_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.HomeOwnerNSEISMICZONE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.HomeOwnernYear_built_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "9999",
            decimalPlaces: 0,
            minimumValue: "-9999"
        });
      };
    this.HomeOwnernPrice_purch_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999999999999"
        });
      };
    this.HomeOwnernCurrency_purch_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.HomeOwnernLandSuper_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999999",
            decimalPlaces: 2,
            minimumValue: "-99999999"
        });
      };
    this.HomeOwnernHomeSuper_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999999",
            decimalPlaces: 2,
            minimumValue: "-99999999"
        });
      };
    this.HomeOwnernRoofType_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.HomeOwnernRoofYear_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "9999",
            decimalPlaces: 0,
            minimumValue: "-9999"
        });
      };
    this.HomeOwnernFloodZone_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99",
            decimalPlaces: 0,
            minimumValue: "-99"
        });
      };
    this.HomeOwnernFoundType_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.HomeOwnernAirType_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.HomeOwnernStories_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.HomeOwnernHalfBath_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.HomeOwnernFullBath_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.HomeOwnernBedrooms_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.HomeOwnernFirePlace_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.HomeOwnernGarage_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.HomeOwnernDist_Fire_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999",
            decimalPlaces: 2,
            minimumValue: "-999999"
        });
      };
    this.HomeOwnernSwimPool_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.HomeOwnernFenceHeight_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.HomeOwnernCap_other_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999",
            decimalPlaces: 0,
            minimumValue: "-999999999999"
        });
      };
    this.HomeOwnernCurrency_other_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.LifenTypDurPay_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.LifenPay_time_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.LifenTypDurIns_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.LifenInsur_time_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.LifenXprem_time_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.LifenAge_limit_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.LifeNAGE_REINSU_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.LifeNAGE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.THEFTNCOMMERGRP_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999",
            decimalPlaces: 0,
            minimumValue: "-999"
        });
      };
    this.THEFTNINSURED_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 2,
            minimumValue: "-99999"
        });
      };
    this.THEFTNEMPLOYEES_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.THEFTNAREA_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.THEFTNVIGILANCE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.THEFTNCAPITAL_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999",
            decimalPlaces: 0,
            minimumValue: "-999999999999"
        });
      };
    this.THEFTNNULLCODE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.FINANCIAL_INSTRUMENTSNCONSECUTIVE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.FINANCIAL_INSTRUMENTSNINSTRUMENT_TY_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.FINANCIAL_INSTRUMENTSNBANK_CODE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "9999999999",
            decimalPlaces: 0,
            minimumValue: "-9999999999"
        });
      };
    this.FINANCIAL_INSTRUMENTSNCARD_TYPE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.FINANCIAL_INSTRUMENTSNQUOTA_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.FINANCIAL_INSTRUMENTSNAMOUNT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999999999999"
        });
      };
    this.FINANCIAL_INSTRUMENTSNCURRENCY_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "9999999999",
            decimalPlaces: 0,
            minimumValue: "-9999999999"
        });
      };
    this.HEALTHNCAPITAL_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999",
            decimalPlaces: 0,
            minimumValue: "-999999999999"
        });
      };
    this.HEALTHNPREMIUM_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "9999999999",
            decimalPlaces: 2,
            minimumValue: "-9999999999"
        });
      };
    this.HEALTHNBRANCH_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "9",
            decimalPlaces: 0,
            minimumValue: "-9"
        });
      };
    this.HEALTHNPRODUCT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "9",
            decimalPlaces: 0,
            minimumValue: "-9"
        });
      };
    this.HEALTHNPOLICY_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "9",
            decimalPlaces: 0,
            minimumValue: "-9"
        });
      };
    this.HEALTHNCERTIF_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "9",
            decimalPlaces: 0,
            minimumValue: "-9"
        });
      };
    this.BENEFICIARnCover_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.BENEFICIARnParticip_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 2,
            minimumValue: "-99999"
        });
      };
    this.ClausenClause_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.ClausenNotenum_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "9999999999",
            decimalPlaces: 0,
            minimumValue: "-9999999999"
        });
      };
    this.POLICY_HISNMOVEMENT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "9999999999",
            decimalPlaces: 0,
            minimumValue: "-9999999999"
        });
      };
    this.POLICY_HISNTYPE_HIST_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.POLICY_HISNTRANSACTIO_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.POLICY_HISNRECEIPT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "9999999999",
            decimalPlaces: 0,
            minimumValue: "-9999999999"
        });
      };
    this.PremiumnBranch_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.PremiumnProduct_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.PremiumnPolicy_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: "",
            maximumValue: "9999999999",
            decimalPlaces: 0,
            minimumValue: "-9999999999"
        });
      };
    this.PremiumnReceipt_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: "",
            maximumValue: "9999999999",
            decimalPlaces: 0,
            minimumValue: "-9999999999"
        });
      };
    this.PremiumnTratypei_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.PremiumnPremium_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999999999999"
        });
      };
    this.PremiumnCurrency_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.PremiumnCollector_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "9999999999",
            decimalPlaces: 0,
            minimumValue: "-9999999999"
        });
      };
    this.PremiumnWay_pay_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.PremiumNINTERMED_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "9999999999",
            decimalPlaces: 0,
            minimumValue: "-9999999999"
        });
      };
    this.PremiumnParticip_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 2,
            minimumValue: "-99999"
        });
      };
    this.PremiumnComamou_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999999999999"
        });
      };
    this.PremiumnStatus_pre_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.PremiumNDIGIT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "9",
            decimalPlaces: 0,
            minimumValue: "-9"
        });
      };
    this.PremiumNPAYNUMBE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "9",
            decimalPlaces: 0,
            minimumValue: "-9"
        });
      };
    this.PremiumNCONTRAT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "9",
            decimalPlaces: 0,
            minimumValue: "-9"
        });
      };
    this.Detail_prenReceipt_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "9999999999",
            decimalPlaces: 0,
            minimumValue: "-9999999999"
        });
      };
    this.Detail_prenBill_item_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.Detail_prenPremium_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999999999999"
        });
      };
    this.Detail_prenCommision_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999999999999"
        });
      };
    this.Detail_prenPremAnual_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999999999999"
        });
      };
    this.Detail_prenPremiumE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999999999999"
        });
      };
    this.Detail_prenPremiumA_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999999999999"
        });
      };
    this.Detail_prenDescAmount_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999999999999"
        });
      };
    this.Detail_prenRecAmount_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999999999999"
        });
      };
    this.Detail_prenTaxAmount_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999999999999"
        });
      };
    this.Detail_prenComAnual_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999999999999"
        });
      };
    this.Commiss_prnIntermed_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: "",
            maximumValue: "9999999999",
            decimalPlaces: 0,
            minimumValue: "-9999999999"
        });
      };
    this.Commiss_prnRole_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.Commiss_prnShare_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 2,
            minimumValue: "-99999"
        });
      };
    this.Commiss_prnPercent_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "9999",
            decimalPlaces: 2,
            minimumValue: "-9999"
        });
      };
    this.Commiss_prnAmount_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999999999999"
        });
      };
    this.Premium_monId_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "9999999999",
            decimalPlaces: 0,
            minimumValue: "-9999999999"
        });
      };
    this.Premium_monTransac_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.Premium_monType_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.Premium_monAmount_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999999999999"
        });
      };
    this.Premium_monCurrency_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.FINANCE_CONCONTRAT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "9999999999",
            decimalPlaces: 0,
            minimumValue: "-9999999999"
        });
      };
    this.FINANCE_CONQ_DRAFT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.FINANCE_CONAMOUNT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999999999999"
        });
      };
    this.FINANCE_CONCURRENCY_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.FINANCE_CONFRECUENCY_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.FINANC_DRANDRAFT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.FINANC_DRANSTAT_DRAFT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.FINANC_DRANAMOUNT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999999999999"
        });
      };
    this.ClaimnBranch_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.ClaimnProduct_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: "",
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.ClaimnPolicy_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "9999999999",
            decimalPlaces: 0,
            minimumValue: "-9999999999"
        });
      };
    this.ClaimnClaim_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: "",
            maximumValue: "9999999999",
            decimalPlaces: 0,
            minimumValue: "-9999999999"
        });
      };
    this.ClaimnCausecod_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.ClaimnUnaccode_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.ClaimnLoc_reserv_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999999999999"
        });
      };
    this.ClaimnLoc_rec_am_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999999999999"
        });
      };
    this.ClaimnLoc_pay_am_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999999999999"
        });
      };
    this.ClaimnLoc_out_am_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999999999999"
        });
      };
    this.ClaimnLoc_cos_re_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999999999999"
        });
      };
    this.CLAIM_CASEnCase_num_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: "",
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.CLAIM_CASEnNoteDama_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "9999999999",
            decimalPlaces: 0,
            minimumValue: "-9999999999"
        });
      };
    this.CLAIM_CASENCLAIM_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "9",
            decimalPlaces: 0,
            minimumValue: "-9"
        });
      };
    this.CL_COVERnCase_num_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.CL_COVERnModulec_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.CL_COVERnCover_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.CL_COVERnDamProf_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999999999999"
        });
      };
    this.CL_COVERnReserve_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999999999999"
        });
      };
    this.CL_COVERnRec_amount_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999999999999"
        });
      };
    this.CL_COVERnPay_amount_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999999999999"
        });
      };
    this.CL_COVERnLoc_cos_re_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999999999999"
        });
      };
    this.CL_COVERnFra_amount_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999999999999"
        });
      };
    this.CLAIM_ATTMnCase_num_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.CLAIM_ATTMnService_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.Claim_autonCase_num_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.Claim_autonDeman_type_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.Claim_autonInfraction_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.Claim_autonAuto_quant_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.Claim_autonNotenum_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "9999999999",
            decimalPlaces: 0,
            minimumValue: "-9999999999"
        });
      };
    this.CLAIM_DAMANDAMAGE_COD_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "9999999999",
            decimalPlaces: 0,
            minimumValue: "-9999999999"
        });
      };
    this.CLAIM_DAMANMAG_DAM_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.CLAIM_DAMANAMOUNT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999999999999"
        });
      };
    this.CLAIM_THIRNTHIR_COMP_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.CLAIM_THIRNBLAME_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.Life_claimnCase_num_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.Life_claimnDeman_type_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.Life_claimnIn_lif_typ_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.Life_claimnCla_li_typ_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.Life_claimnMonth_amou_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999999999999"
        });
      };
    this.CLAIMBENEFNBENE_TYPE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.CLAIMBENEFNRELATION_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.CLAIMBENEFNPARTICIP_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 2,
            minimumValue: "-99999"
        });
      };
    this.CLAIMBENEFNOFFICE_PAY_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.CLAIMBENEFNOFFICEAGEN_PAY_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.CLAIM_HISnCase_num_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.CLAIM_HISnTransac_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.CLAIM_HISnServ_order_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "9999999999",
            decimalPlaces: 0,
            minimumValue: "-9999999999"
        });
      };
    this.CLAIM_HISnAmount_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999999999999"
        });
      };
    this.AddressnProvince_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.AddressnLocal_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.AddressnCountry_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.AddressnMunicipality_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.AddressNRECOWNER_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "9",
            decimalPlaces: 0,
            minimumValue: "-9"
        });
      };
    this.PHONESNPHONE_TYPE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.PHONESNAREA_CODE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.PHONESNEXTENS1_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.PHONESNEXTENS2_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.PHONESNBESTTIMETOCALL_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "9999999999",
            decimalPlaces: 0,
            minimumValue: "-9999999999"
        });
      };
    this.CLIDOCUMENTSNTYPCLIENTDOC_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.DIR_DEBIT_CLINBANKEXT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "9999999999",
            decimalPlaces: 0,
            minimumValue: "-9999999999"
        });
      };
    this.DIR_DEBIT_CLINBILL_DAY_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.Bk_accountnTyp_acc_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.Bk_accountnBankExt_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "9999999999",
            decimalPlaces: 0,
            minimumValue: "-9999999999"
        });
      };
    this.Cred_cardnCard_type_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.Cred_cardnBankExt_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "9999999999",
            decimalPlaces: 0,
            minimumValue: "-9999999999"
        });
      };
    this.Curr_accnTyp_acco_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.Curr_accnDebit_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999999999999"
        });
      };
    this.Curr_accnCredit_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999999999999"
        });
      };
    this.Curr_accnBalance_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999999999999"
        });
      };
    this.Curr_accnCurrency_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.SportnSport_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.HobbynHobby_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.Financ_clinConcept_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "9999999999",
            decimalPlaces: 0,
            minimumValue: "-9999999999"
        });
      };
    this.Financ_clinAmount_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999999999999"
        });
      };
    this.Financ_clinCurrency_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.Financ_clinUnits_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999999999999"
        });
      };
    this.Financ_clinFinanStat_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.CHEQUESNREQUEST_NU_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "9999999999",
            decimalPlaces: 0,
            minimumValue: "-9999999999"
        });
      };
    this.CHEQUESNAMOUNT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "999999999999999999",
            decimalPlaces: 6,
            minimumValue: "-999999999999999999"
        });
      };
    this.CHEQUESNAGENCY_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "99999",
            decimalPlaces: 0,
            minimumValue: "-99999"
        });
      };
    this.CHEQUESNDOCSUPPORT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: "9999999999",
            decimalPlaces: 0,
            minimumValue: "-9999999999"
        });
      };


};

$(document).ready(function () {
    moment.locale(generalSupport.UserContext().languageName);
    
    if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage)
        masterSupport.setPageTitle('HT5Cliente');
        

    HT5ConsultaClientesSegunClienteIndicadoSupport.ControlBehaviour();
    HT5ConsultaClientesSegunClienteIndicadoSupport.ControlActions();
    HT5ConsultaClientesSegunClienteIndicadoSupport.ValidateSetup();

    $('#RecordEffectiveDate').val(generalSupport.URLDateValue('RecordEffectiveDate'));

    $("#ItemsTblPlaceHolder").replaceWith('<table id="ItemsTbl"><caption >Cliente</caption></table>');
    HT5ConsultaClientesSegunClienteIndicadoSupport.ItemsTblSetup($('#ItemsTbl'));

        HT5ConsultaClientesSegunClienteIndicadoSupport.ItemsTblRequest();



});

