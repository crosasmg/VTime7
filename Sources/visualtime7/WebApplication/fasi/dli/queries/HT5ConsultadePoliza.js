var HT5ConsultadePolizaSupport = new function () {

    this.currentRow = {};
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#HT5ConsultadePolizaFormId').val(),
            PolicyID: generalSupport.NumericValue('#PolicyID', -9999999999, 9999999999),
            RecordEffectiveDate: generalSupport.DatePickerValueInputToObject('#RecordEffectiveDate')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        $('#HT5ConsultadePolizaFormId').val(data.InstanceFormId);
        AutoNumeric.set('#PolicyID', data.PolicyID);
        $('#RecordEffectiveDate').val(generalSupport.ToJavaScriptDateCustom(data.RecordEffectiveDate, generalSupport.DateFormat()));

        HT5ConsultadePolizaSupport.LookUpForREINSURAN2NCOMPANY(source);

        if (data.DIR_DEBIT_DIR_DEBIT !== null)
            $('#DIR_DEBITTbl').bootstrapTable('load', data.DIR_DEBIT_DIR_DEBIT);
        if (data.ROLES_ROLES !== null)
            $('#ROLESTbl').bootstrapTable('load', data.ROLES_ROLES);
        if (data.CURREN_POL_CURREN_POL !== null)
            $('#CURREN_POLTbl').bootstrapTable('load', data.CURREN_POL_CURREN_POL);
        if (data.SUM_INSUR_SUM_INSUR !== null)
            $('#SUM_INSURTbl').bootstrapTable('load', data.SUM_INSUR_SUM_INSUR);
        if (data.COVER_COVER !== null)
            $('#COVERTbl').bootstrapTable('load', data.COVER_COVER);
        if (data.REINSURAN_REINSURAN !== null)
            $('#REINSURANTbl').bootstrapTable('load', data.REINSURAN_REINSURAN);
        if (data.REINSURAN2_REINSURAN2 !== null)
            $('#REINSURAN2Tbl').bootstrapTable('load', data.REINSURAN2_REINSURAN2);
        if (data.DISC_XPREM_DISC_XPREM !== null)
            $('#DISC_XPREMTbl').bootstrapTable('load', data.DISC_XPREM_DISC_XPREM);
        if (data.AUTO_AUTO !== null)
            $('#AUTOTbl').bootstrapTable('load', data.AUTO_AUTO);
        if (data.FIRE_FIRE !== null)
            $('#FIRETbl').bootstrapTable('load', data.FIRE_FIRE);
        if (data.HOMEOWNER_HOMEOWNER !== null)
            $('#HOMEOWNERTbl').bootstrapTable('load', data.HOMEOWNER_HOMEOWNER);
        if (data.LIFE_LIFE !== null)
            $('#LIFETbl').bootstrapTable('load', data.LIFE_LIFE);
        if (data.THEFT_THEFT !== null)
            $('#THEFTTbl').bootstrapTable('load', data.THEFT_THEFT);
        if (data.ROLES2_ROLES2 !== null)
            $('#ROLES2Tbl').bootstrapTable('load', data.ROLES2_ROLES2);
        if (data.HEALTH_HEALTH !== null)
            $('#HEALTHTbl').bootstrapTable('load', data.HEALTH_HEALTH);
        if (data.FINANCIAL_INSTRUMENTS_FINANCIAL_INSTRUMENTS !== null)
            $('#FINANCIAL_INSTRUMENTSTbl').bootstrapTable('load', data.FINANCIAL_INSTRUMENTS_FINANCIAL_INSTRUMENTS);
        if (data.PROTECTION_PROTECTION !== null)
            $('#PROTECTIONTbl').bootstrapTable('load', data.PROTECTION_PROTECTION);
        if (data.BENEFICIAR_BENEFICIAR !== null)
            $('#BENEFICIARTbl').bootstrapTable('load', data.BENEFICIAR_BENEFICIAR);
        if (data.CLAUSE_CLAUSE !== null)
            $('#CLAUSETbl').bootstrapTable('load', data.CLAUSE_CLAUSE);
        if (data.POLICY_HIS_POLICY_HIS !== null)
            $('#POLICY_HISTbl').bootstrapTable('load', data.POLICY_HIS_POLICY_HIS);
        if (data.FINANC_DRA_FINANC_DRA !== null)
            $('#FINANC_DRATbl').bootstrapTable('load', data.FINANC_DRA_FINANC_DRA);
        if (data.FINANC_PRE_FINANC_PRE !== null)
            $('#FINANC_PRETbl').bootstrapTable('load', data.FINANC_PRE_FINANC_PRE);
        if (data.PREMIUM_CE_PREMIUM_CE !== null)
            $('#PREMIUM_CETbl').bootstrapTable('load', data.PREMIUM_CE_PREMIUM_CE);
        if (data.PREMIUM_PREMIUM !== null)
            $('#PREMIUMTbl').bootstrapTable('load', data.PREMIUM_PREMIUM);
        if (data.CLAIM_CLAIM !== null)
            $('#CLAIMTbl').bootstrapTable('load', data.CLAIM_CLAIM);
        if (data.CURR_ACC_CURR_ACC !== null)
            $('#CURR_ACCTbl').bootstrapTable('load', data.CURR_ACC_CURR_ACC);
        HT5ConsultadePolizaSupport.ItemsTblRequest();
        if (data.Items_Item !== null)
            $('#ItemsTbl').bootstrapTable('load', data.Items_Item);

    };

    this.ControlBehaviour = function () {

       this.Items_CERTIFICATNPOLICY_Item1 = function (row) {
           window.location.href = '/fasi/dli/forms/NNClaimDeclarationDemo.aspx?Policy='+ row.NPOLICY +'';

            return true;
        };
       this.ROLES_ROLESSCLIENTDesc_Item1 = function (row) {
           generalSupport.CallBackOfficePage('BC003_K', '&tctClient='+ row.SCLIENT +'&plngMainAction=401&tctClient_Digit=0');

            return true;
        };
       this.ROLES_ROLESSCLIENTDesc_Item2 = function (row) {
           generalSupport.CallBackOfficePage('', '&ClientKey='+ row.SCLIENT +'');

            return true;
        };


      new AutoNumeric('#PolicyID', {
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
                HT5ConsultadePolizaSupport.ObjectToInput(data.d.Data, source);
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };



    this.DIR_DEBIT_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/DIR_DEBITSelectCommandActionDIR_DEBIT", false,
            JSON.stringify({                 DIRDEBITSCERTYPE1: row.SCERTYPE,
                DIRDEBITNBRANCH2: row.NBRANCH,
                DIRDEBITNPRODUCT3: row.NPRODUCT,
                DIRDEBITNPOLICY4: row.NPOLICY,
                DIRDEBITNCERTIF5: row.NCERTIF }),
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
    this.ROLES_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/ROLESSelectCommandActionROLES", false,
            JSON.stringify({                 ROLESSCERTYPE1: row.SCERTYPE,
                ROLESNBRANCH2: row.NBRANCH,
                ROLESNPRODUCT3: row.NPRODUCT,
                ROLESNPOLICY4: row.NPOLICY,
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
    this.ROLES_Item1_Actions = function (row, $modal) {
    generalSupport.CallBackOfficePage('BC003_K', '&tctClient='+ row.SCLIENT +'&plngMainAction=401&tctClient_Digit=0');

    };
    this.ROLES_Item2_Actions = function (row, $modal) {
    generalSupport.CallBackOfficePage('', '&ClientKey='+ row.SCLIENT +'');

    };
    this.CURREN_POL_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/CURREN_POLSelectCommandActionCURREN_POL", false,
            JSON.stringify({                 CURRENPOLSCERTYPE1: row.SCERTYPE,
                CURRENPOLNBRANCH2: row.NBRANCH,
                CURRENPOLNPRODUCT3: row.NPRODUCT,
                CURRENPOLNPOLICY4: row.NPOLICY,
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
    this.SUM_INSUR_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/SUM_INSURSelectCommandActionSUM_INSUR", false,
            JSON.stringify({                 SUMINSURSCERTYPE1: row.SCERTYPE,
                SUMINSURNBRANCH2: row.NBRANCH,
                SUMINSURNPRODUCT3: row.NPRODUCT,
                SUMINSURNPOLICY4: row.NPOLICY,
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
    this.COVER_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/COVERSelectCommandActionCOVER", false,
            JSON.stringify({                 COVERSCERTYPE1: row.SCERTYPE,
                COVERNBRANCH2: row.NBRANCH,
                COVERNPRODUCT3: row.NPRODUCT,
                COVERNPOLICY4: row.NPOLICY,
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
        app.core.SyncWebMethod("/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/REINSURANSelectCommandActionREINSURAN", false,
            JSON.stringify({                 REINSURANSCERTYPE3: row.SCERTYPE,
                REINSURANNBRANCH4: row.NBRANCH,
                REINSURANNPRODUCT5: row.NPRODUCT,
                REINSURANNPOLICY6: row.NPOLICY,
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
        app.core.SyncWebMethod("/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/REINSURAN2SelectCommandActionREINSURAN", false,
            JSON.stringify({                 REINSURANSCERTYPE3: row.SCERTYPE,
                REINSURANNBRANCH4: row.NBRANCH,
                REINSURANNPRODUCT5: row.NPRODUCT,
                REINSURANNPOLICY6: row.NPOLICY,
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
    this.DISC_XPREM_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/DISC_XPREMSelectCommandActionDISC_XPREM", false,
            JSON.stringify({                 DISCXPREMSCERTYPE1: row.SCERTYPE,
                DISCXPREMNBRANCH2: row.NBRANCH,
                DISCXPREMNPRODUCT3: row.NPRODUCT,
                DISCXPREMNPOLICY4: row.NPOLICY,
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
    this.AUTO_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/AUTOSelectCommandActionAUTO", false,
            JSON.stringify({                 AUTOSCERTYPE1: row.SCERTYPE,
                AUTONBRANCH2: row.NBRANCH,
                AUTONPRODUCT3: row.NPRODUCT,
                AUTONPOLICY4: row.NPOLICY,
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
    this.FIRE_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/FIRESelectCommandActionFIRE", false,
            JSON.stringify({                 FIRESCERTYPE1: row.SCERTYPE,
                FIRENBRANCH2: row.NBRANCH,
                FIRENPRODUCT3: row.NPRODUCT,
                FIRENPOLICY4: row.NPOLICY,
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
    this.HOMEOWNER_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/HOMEOWNERSelectCommandActionHOMEOWNER", false,
            JSON.stringify({                 HOMEOWNERSCERTYPE1: row.SCERTYPE,
                HOMEOWNERNBRANCH2: row.NBRANCH,
                HOMEOWNERNPRODUCT3: row.NPRODUCT,
                HOMEOWNERNPOLICY4: row.NPOLICY,
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
    this.LIFE_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/LIFESelectCommandActionLIFE", false,
            JSON.stringify({                 LIFESCERTYPE1: row.SCERTYPE,
                LIFENBRANCH2: row.NBRANCH,
                LIFENPRODUCT3: row.NPRODUCT,
                LIFENPOLICY4: row.NPOLICY,
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
        app.core.SyncWebMethod("/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/THEFTSelectCommandActionTHEFT", false,
            JSON.stringify({                 THEFTSCERTYPE1: row.SCERTYPE,
                THEFTNBRANCH2: row.NBRANCH,
                THEFTNPRODUCT3: row.NPRODUCT,
                THEFTNPOLICY4: row.NPOLICY,
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
    this.HEALTH_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/HEALTHSelectCommandActionHEALTH", false,
            JSON.stringify({                 HEALTHSCERTYPE1: row.SCERTYPE,
                HEALTHNBRANCH2: row.NBRANCH,
                HEALTHNPRODUCT3: row.NPRODUCT,
                HEALTHNPOLICY4: row.NPOLICY,
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
        app.core.SyncWebMethod("/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/ROLES2SelectCommandActionROLES", false,
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
    this.FINANCIAL_INSTRUMENTS_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/FINANCIAL_INSTRUMENTSSelectCommandActionFINANCIAL_INSTRUMENTS", false,
            JSON.stringify({                 FINANCIALINSTRUMENTSSCERTYPE1: row.SCERTYPE,
                FINANCIALINSTRUMENTSNBRANCH2: row.NBRANCH,
                FINANCIALINSTRUMENTSNPRODUCT3: row.NPRODUCT,
                FINANCIALINSTRUMENTSNPOLICY4: row.NPOLICY,
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
    this.PROTECTION_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/PROTECTIONSelectCommandActionPROTECTION", false,
            JSON.stringify({                 PROTECTIONSCERTYPE1: row.SCERTYPE,
                PROTECTIONNBRANCH2: row.NBRANCH,
                PROTECTIONNPRODUCT3: row.NPRODUCT,
                PROTECTIONNPOLICY4: row.NPOLICY,
                PROTECTIONNCERTIF5: row.NCERTIF }),
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
    this.BENEFICIAR_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/BENEFICIARSelectCommandActionBENEFICIAR", false,
            JSON.stringify({                 BENEFICIARSCERTYPE1: row.SCERTYPE,
                BENEFICIARNBRANCH2: row.NBRANCH,
                BENEFICIARNPRODUCT3: row.NPRODUCT,
                BENEFICIARNPOLICY4: row.NPOLICY,
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
    this.CLAUSE_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/CLAUSESelectCommandActionCLAUSE", false,
            JSON.stringify({                 CLAUSESCERTYPE1: row.SCERTYPE,
                CLAUSENBRANCH2: row.NBRANCH,
                CLAUSENPRODUCT3: row.NPRODUCT,
                CLAUSENPOLICY4: row.NPOLICY,
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
        app.core.SyncWebMethod("/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/POLICY_HISSelectCommandActionPOLICY_HIS", false,
            JSON.stringify({                 POLICYHISSCERTYPE1: row.SCERTYPE,
                POLICYHISNBRANCH2: row.NBRANCH,
                POLICYHISNPRODUCT3: row.NPRODUCT,
                POLICYHISNPOLICY4: row.NPOLICY,
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
    this.PREMIUM_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/PREMIUMSelectCommandActionPREMIUM", false,
            JSON.stringify({                 PREMIUMSCERTYPE3: row.SCERTYPE,
                PREMIUMNBRANCH4: row.NBRANCH,
                PREMIUMNPRODUCT5: row.NPRODUCT,
                PREMIUMNPOLICY6: row.NPOLICY,
                PREMIUMNCERTIF7: row.NCERTIF }),
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
    this.FINANC_PRE_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/FINANC_PRESelectCommandActionFINANC_PRE", false,
            JSON.stringify({                 FINANCPRENRECEIPT1: row.NRECEIPT }),
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
        app.core.SyncWebMethod("/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/FINANC_DRASelectCommandActionFINANC_DRA", false,
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
    this.PREMIUM_CE_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/PREMIUM_CESelectCommandActionPREMIUM_CE", false,
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
    this.CLAIM_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/CLAIMSelectCommandActionCLAIM", false,
            JSON.stringify({                 CLAIMSCERTYPE1: row.SCERTYPE,
                CLAIMNBRANCH2: row.NBRANCH,
                CLAIMNPRODUCT3: row.NPRODUCT,
                CLAIMNPOLICY4: row.NPOLICY,
                CLAIMNCERTIF5: row.NCERTIF }),
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
    this.CURR_ACC_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/CURR_ACCSelectCommandActionCURR_ACC", false,
            JSON.stringify({                 CURRACCSCERTYPE2: row.SCERTYPE,
                CURRACCNBRANCH3: row.NBRANCH,
                CURRACCNPRODUCT4: row.NPRODUCT,
                CURRACCNPOLICY5: row.NPOLICY,
                CURRACCNCERTIF6: row.NCERTIF }),
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
            var formInstance = $("#HT5ConsultadePolizaMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#btnOk'));
                btnLoading.start();
                HT5ConsultadePolizaSupport.ItemsTblRequest();
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();
    
        $("#HT5ConsultadePolizaMainForm").validate({
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
                PolicyID: {
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
                PolicyID: {
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
    this.LookUpForREINSURAN2NCOMPANYFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#REINSURAN2NCOMPANY>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForREINSURAN2NCOMPANY = function (defaultValue, source) {
        var ctrol = $('#REINSURAN2NCOMPANY');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.SyncWebMethod("/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/LookUpForREINSURAN2NCOMPANY", false,
                JSON.stringify({ id: $('#HT5ConsultadePolizaFormId').val() }),
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
    this.LookUpForPREMIUMNTYPEFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#PREMIUMNTYPE>option[value='" + value + "']").text();
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
            onExpandRow: HT5ConsultadePolizaSupport.ItemsTblExpandRow,
            columns: [{
                field: 'NBRANCH',
                title: 'Ramo',
                formatter: 'HT5ConsultadePolizaSupport.CERTIFICATNBRANCH_FormatterMaskData',
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
                formatter: 'HT5ConsultadePolizaSupport.CERTIFICATNPRODUCT_FormatterMaskData',
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
                formatter: 'tableHelperSupport.IsContextMenu',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NCERTIF',
                title: 'Certificado',
                formatter: 'HT5ConsultadePolizaSupport.CERTIFICATNCERTIF_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NCAPITAL',
                title: 'Capital Asegurado',
                formatter: 'HT5ConsultadePolizaSupport.CERTIFICATNCAPITAL_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NPAYFREQ',
                title: 'Frecuencia de Pago',
                formatter: 'HT5ConsultadePolizaSupport.CERTIFICATNPAYFREQ_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NPAYFREQDesc',
                title: 'Frecuencia de Pago',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'SSTATUSVA',
                title: 'Estado',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'SSTATUSVADesc',
                title: 'Estado',
                sortable: true,
                halign: 'center'
            }, {
                field: 'DSTARTDATE',
                title: 'Fecha de Inicio',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'NWAIT_CODE',
                title: 'Causa de Pendiente',
                formatter: 'HT5ConsultadePolizaSupport.CERTIFICATNWAIT_CODE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NWAIT_CODEDesc',
                title: 'Causa de Pendiente',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'DISSUEDAT',
                title: 'Fecha de Emisión',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'SRENEWAL',
                title: 'Renovación Automática',
                formatter: 'tableHelperSupport.IsCheck',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'DNULLDATE',
                title: 'Fecha de Anulación',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center',
                visible: false
            }, {
                field: 'NNULLCODE',
                title: 'Código de Anulación',
                formatter: 'HT5ConsultadePolizaSupport.CERTIFICATNNULLCODE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NNULLCODEDesc',
                title: 'Código de Anulación',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'DEXPIRDAT',
                title: 'Fecha de Vencimiento',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'DCHANGDAT',
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
                field: 'NDIGIT',
                title: 'DígitoVerificador',
                formatter: 'HT5ConsultadePolizaSupport.CERTIFICATNDIGIT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }]
        });

        table.bootstrapTable('refreshOptions', {
            contextMenu: '#ItemsContextMenu',
            contextMenuButton: '.menu-NPOLICY',
            beforeContextMenuRow: function (e, row, buttonElement) {
                HT5ConsultadePolizaSupport.ItemsRowToInput(row);
                if (buttonElement && $(buttonElement).hasClass('menu-NPOLICY')) {

                    $('#ItemsTbl').bootstrapTable('showContextMenu', {
                        event: e,
                        contextMenu: '#Items_CERTIFICATNPOLICYContextMenu',
                        buttonElement: buttonElement
                    });
                }
                return false;
            },
            onContextMenuItem: function (row, $el) {
                HT5ConsultadePolizaSupport.ItemsRowToInput(row);
                switch ($el.data("item")) {
                    case 'Items_CERTIFICATNPOLICY_Item1':
                        HT5ConsultadePolizaSupport.Items_CERTIFICATNPOLICY_Item1(row);
                        break;
                }
            }
        });


    };


    this.ItemsRowToInput = function (row) {
        HT5ConsultadePolizaSupport.currentRow = row;
        AutoNumeric.set('#CERTIFICATNBRANCH', row.NBRANCH);
        $('#CERTIFICATNBRANCHDesc').val(row.NBRANCHDesc);
        AutoNumeric.set('#CERTIFICATNPRODUCT', row.NPRODUCT);
        $('#CERTIFICATNPRODUCTDesc').val(row.NPRODUCTDesc);
        $('#CERTIFICATNPOLICY').val(row.NPOLICY);
        AutoNumeric.set('#CERTIFICATNCERTIF', row.NCERTIF);
        AutoNumeric.set('#CERTIFICATNCAPITAL', row.NCAPITAL);
        AutoNumeric.set('#CERTIFICATNPAYFREQ', row.NPAYFREQ);
        $('#CERTIFICATNPAYFREQDesc').val(row.NPAYFREQDesc);
        $('#CERTIFICATSSTATUSVA').val(row.SSTATUSVA);
        $('#CERTIFICATSSTATUSVADesc').val(row.SSTATUSVADesc);
        $('#CERTIFICATDSTARTDATE').val(generalSupport.ToJavaScriptDateCustom(row.DSTARTDATE, generalSupport.DateFormat()));
        AutoNumeric.set('#CERTIFICATNWAIT_CODE', row.NWAIT_CODE);
        $('#CERTIFICATNWAIT_CODEDesc').val(row.NWAIT_CODEDesc);
        $('#CERTIFICATDISSUEDAT').val(generalSupport.ToJavaScriptDateCustom(row.DISSUEDAT, generalSupport.DateFormat()));
        $('#CERTIFICATSRENEWAL').prop("checked", row.SRENEWAL);
        $('#CERTIFICATDNULLDATE').val(generalSupport.ToJavaScriptDateCustom(row.DNULLDATE, generalSupport.DateFormat()));
        AutoNumeric.set('#CERTIFICATNNULLCODE', row.NNULLCODE);
        $('#CERTIFICATNNULLCODEDesc').val(row.NNULLCODEDesc);
        $('#CERTIFICATDEXPIRDAT').val(generalSupport.ToJavaScriptDateCustom(row.DEXPIRDAT, generalSupport.DateFormat()));
        $('#CERTIFICATDCHANGDAT').val(generalSupport.ToJavaScriptDateCustom(row.DCHANGDAT, generalSupport.DateFormat()));
        $('#CERTIFICATSCERTYPE').val(row.SCERTYPE);
        AutoNumeric.set('#CERTIFICATNDIGIT', row.NDIGIT);

    };
    this.ItemsTblRequest = function (params) {
        app.core.AsyncWebMethod("/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/ItemsTblDataLoad", false,
            JSON.stringify({
                                POLICYIDNPOLICY1: generalSupport.NumericValue('#PolicyID', -9999999999, 9999999999)
            }),
            function (data) {
                    $('#ItemsTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);


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
                title: 'Código del Banco',
                formatter: 'HT5ConsultadePolizaSupport.DIR_DEBITNBANKEXT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NBANKEXTDesc',
                title: 'Código del Banco',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SCREDI_CARD',
                title: 'Tarjeta de Crédito',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NTYP_CRECARD',
                title: 'Tipo de Tarjeta de Crédito',
                formatter: 'HT5ConsultadePolizaSupport.DIR_DEBITNTYP_CRECARD_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NTYP_CRECARDDesc',
                title: 'Tipo de Tarjeta de Crédito',
                sortable: true,
                halign: 'center'
            }, {
                field: 'DCARDEXPIR',
                title: 'Fecha de Vencimiento de la Factura',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }]
        });

        HT5ConsultadePolizaSupport.$el = table;
        HT5ConsultadePolizaSupport.DIR_DEBITTblRequest();
      };

    this.DIR_DEBITRowToInput = function (row) {
        HT5ConsultadePolizaSupport.currentRow = row;
        AutoNumeric.set('#DIR_DEBITNBANKEXT', row.NBANKEXT);
        $('#DIR_DEBITNBANKEXTDesc').val(row.NBANKEXTDesc);
        $('#DIR_DEBITSCREDI_CARD').val(row.SCREDI_CARD);
        AutoNumeric.set('#DIR_DEBITNTYP_CRECARD', row.NTYP_CRECARD);
        $('#DIR_DEBITNTYP_CRECARDDesc').val(row.NTYP_CRECARDDesc);
        $('#DIR_DEBITDCARDEXPIR').val(generalSupport.ToJavaScriptDateCustom(row.DCARDEXPIR, generalSupport.DateFormat()));

    };
    this.DIR_DEBITTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/DIR_DEBITTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                DIRDEBITSCERTYPE1: row.SCERTYPE,
                DIRDEBITNBRANCH2: row.NBRANCH,
                DIRDEBITNPRODUCT3: row.NPRODUCT,
                DIRDEBITNPOLICY4: row.NPOLICY,
                DIRDEBITNCERTIF5: row.NCERTIF
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
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'NROLE',
                title: 'Figura',
                formatter: 'HT5ConsultadePolizaSupport.ROLESNROLE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NROLEDesc',
                title: 'Figura',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SCLIENT',
                title: 'Cliente',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'SCLIENTDesc',
                title: 'Cliente',
                formatter: 'tableHelperSupport.IsContextMenu',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NSTATUSROL',
                title: 'Estado',
                formatter: 'HT5ConsultadePolizaSupport.ROLESNSTATUSROL_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NSTATUSROLDesc',
                title: 'Estado',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SSEXCLIEN',
                title: 'Sexo',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'SSEXCLIENDesc',
                title: 'Sexo',
                sortable: true,
                halign: 'center'
            }, {
                field: 'DBIRTHDATE',
                title: 'Fecha de Nacimiento',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'NTYPERISK',
                title: 'Clasificación',
                formatter: 'HT5ConsultadePolizaSupport.ROLESNTYPERISK_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NTYPERISKDesc',
                title: 'Clasificación',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NRATING',
                title: 'Rating',
                formatter: 'HT5ConsultadePolizaSupport.ROLESNRATING_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'DEFFECDATE',
                title: 'Fecha de Efecto',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }]
        });
        table.bootstrapTable('refreshOptions', {
            contextMenu: '#ROLESContextMenu',
            contextMenuButton: '.menu-SCLIENTDesc',
            beforeContextMenuRow: function (e, row, buttonElement) {
                HT5ConsultadePolizaSupport.ROLESRowToInput(row);
                if (!buttonElement ) {

                    table.bootstrapTable('showContextMenu', {
                        event: e,
                        contextMenu: '#ROLESContextMenu',
                        buttonElement: buttonElement
                    });
                }
                if (buttonElement && $(buttonElement).hasClass('menu-SCLIENTDesc')) {

                    $('#ROLESTbl').bootstrapTable('showContextMenu', {
                        event: e,
                        contextMenu: '#ROLES_ROLESSCLIENTDescContextMenu',
                        buttonElement: buttonElement
                    });
                }
                return false;
            },
            onContextMenuItem: function (row, $el) {
                HT5ConsultadePolizaSupport.ROLESRowToInput(row);
                switch ($el.data("item")) {
                    case 'ROLES_Item1':
                        HT5ConsultadePolizaSupport.ROLES_Item1_Actions(row, null);
                        break;
                    case 'ROLES_Item2':
                        HT5ConsultadePolizaSupport.ROLES_Item2_Actions(row, null);
                        break;
                    case 'ROLES_ROLESSCLIENTDesc_Item1':
                        HT5ConsultadePolizaSupport.ROLES_ROLESSCLIENTDesc_Item1(row);
                        break;
                    case 'ROLES_ROLESSCLIENTDesc_Item2':
                        HT5ConsultadePolizaSupport.ROLES_ROLESSCLIENTDesc_Item2(row);
                        break;
                }
            }
        });

        HT5ConsultadePolizaSupport.$el = table;
        HT5ConsultadePolizaSupport.ROLESTblRequest();
      };

    this.ROLESRowToInput = function (row) {
        HT5ConsultadePolizaSupport.currentRow = row;
        AutoNumeric.set('#ROLESNROLE', row.NROLE);
        $('#ROLESNROLEDesc').val(row.NROLEDesc);
        $('#ROLESSCLIENT').val(row.SCLIENT);
        $('#ROLESSCLIENTDesc').val(row.SCLIENTDesc);
        AutoNumeric.set('#ROLESNSTATUSROL', row.NSTATUSROL);
        $('#ROLESNSTATUSROLDesc').val(row.NSTATUSROLDesc);
        $('#ROLESSSEXCLIEN').val(row.SSEXCLIEN);
        $('#ROLESSSEXCLIENDesc').val(row.SSEXCLIENDesc);
        $('#ROLESDBIRTHDATE').val(generalSupport.ToJavaScriptDateCustom(row.DBIRTHDATE, generalSupport.DateFormat()));
        AutoNumeric.set('#ROLESNTYPERISK', row.NTYPERISK);
        $('#ROLESNTYPERISKDesc').val(row.NTYPERISKDesc);
        AutoNumeric.set('#ROLESNRATING', row.NRATING);
        $('#ROLESDEFFECDATE').val(generalSupport.ToJavaScriptDateCustom(row.DEFFECDATE, generalSupport.DateFormat()));

    };
    this.ROLESTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/ROLESTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                ROLESSCERTYPE1: row.SCERTYPE,
                ROLESNBRANCH2: row.NBRANCH,
                ROLESNPRODUCT3: row.NPRODUCT,
                ROLESNPOLICY4: row.NPOLICY,
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
    this.CURREN_POLTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'NCURRENCY',
                title: 'Moneda',
                formatter: 'HT5ConsultadePolizaSupport.CURREN_POLNCURRENCY_FormatterMaskData',
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

        HT5ConsultadePolizaSupport.$el = table;
        HT5ConsultadePolizaSupport.CURREN_POLTblRequest();
      };

    this.CURREN_POLRowToInput = function (row) {
        HT5ConsultadePolizaSupport.currentRow = row;
        AutoNumeric.set('#CURREN_POLNCURRENCY', row.NCURRENCY);
        $('#CURREN_POLNCURRENCYDesc').val(row.NCURRENCYDesc);

    };
    this.CURREN_POLTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/CURREN_POLTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                CURRENPOLSCERTYPE1: row.SCERTYPE,
                CURRENPOLNBRANCH2: row.NBRANCH,
                CURRENPOLNPRODUCT3: row.NPRODUCT,
                CURRENPOLNPOLICY4: row.NPOLICY,
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
    this.SUM_INSURTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'NSUMINS_COD',
                title: 'Capital Asegurado',
                formatter: 'HT5ConsultadePolizaSupport.SUM_INSURNSUMINS_COD_FormatterMaskData',
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
                field: 'NSUM_INSUR',
                title: 'Valor Asegurado',
                formatter: 'HT5ConsultadePolizaSupport.SUM_INSURNSUM_INSUR_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NCOINSURAN',
                title: 'Coaseguro Pactado',
                formatter: 'HT5ConsultadePolizaSupport.SUM_INSURNCOINSURAN_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NSUMINS_REAL',
                title: 'Valor Real del Bien',
                formatter: 'HT5ConsultadePolizaSupport.SUM_INSURNSUMINS_REAL_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }]
        });

        HT5ConsultadePolizaSupport.$el = table;
        HT5ConsultadePolizaSupport.SUM_INSURTblRequest();
      };

    this.SUM_INSURRowToInput = function (row) {
        HT5ConsultadePolizaSupport.currentRow = row;
        AutoNumeric.set('#SUM_INSURNSUMINS_COD', row.NSUMINS_COD);
        $('#SUM_INSURNSUMINS_CODDesc').val(row.NSUMINS_CODDesc);
        AutoNumeric.set('#SUM_INSURNSUM_INSUR', row.NSUM_INSUR);
        AutoNumeric.set('#SUM_INSURNCOINSURAN', row.NCOINSURAN);
        AutoNumeric.set('#SUM_INSURNSUMINS_REAL', row.NSUMINS_REAL);

    };
    this.SUM_INSURTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/SUM_INSURTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                SUMINSURSCERTYPE1: row.SCERTYPE,
                SUMINSURNBRANCH2: row.NBRANCH,
                SUMINSURNPRODUCT3: row.NPRODUCT,
                SUMINSURNPOLICY4: row.NPOLICY,
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
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'NMODULEC',
                title: 'Módulo',
                formatter: 'HT5ConsultadePolizaSupport.COVERNMODULEC_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NMODULECDesc',
                title: 'Módulo',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NCOVER',
                title: 'Cobertura',
                formatter: 'HT5ConsultadePolizaSupport.COVERNCOVER_FormatterMaskData',
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
                field: 'NROLE',
                title: 'Figura',
                formatter: 'HT5ConsultadePolizaSupport.COVERNROLE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NROLEDesc',
                title: 'Figura',
                sortable: true,
                halign: 'center'
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
                field: 'NCAPITAL',
                title: 'Capital Asegurado',
                formatter: 'HT5ConsultadePolizaSupport.COVERNCAPITAL_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NPREMIUM',
                title: 'Prima Anual',
                formatter: 'HT5ConsultadePolizaSupport.COVERNPREMIUM_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NCURRENCY',
                title: 'Moneda',
                formatter: 'HT5ConsultadePolizaSupport.COVERNCURRENCY_FormatterMaskData',
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
                field: 'DEFFECDATE',
                title: 'Inicio de vigencia',
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
            }]
        });

        HT5ConsultadePolizaSupport.$el = table;
        HT5ConsultadePolizaSupport.COVERTblRequest();
      };

    this.COVERRowToInput = function (row) {
        HT5ConsultadePolizaSupport.currentRow = row;
        AutoNumeric.set('#COVERNMODULEC', row.NMODULEC);
        $('#COVERNMODULECDesc').val(row.NMODULECDesc);
        AutoNumeric.set('#COVERNCOVER', row.NCOVER);
        $('#COVERNCOVERDesc').val(row.NCOVERDesc);
        AutoNumeric.set('#COVERNROLE', row.NROLE);
        $('#COVERNROLEDesc').val(row.NROLEDesc);
        $('#COVERSCLIENT').val(row.SCLIENT);
        $('#COVERSCLIENTDesc').val(row.SCLIENTDesc);
        AutoNumeric.set('#COVERNCAPITAL', row.NCAPITAL);
        AutoNumeric.set('#COVERNPREMIUM', row.NPREMIUM);
        AutoNumeric.set('#COVERNCURRENCY', row.NCURRENCY);
        $('#COVERNCURRENCYDesc').val(row.NCURRENCYDesc);
        $('#COVERDEFFECDATE').val(generalSupport.ToJavaScriptDateCustom(row.DEFFECDATE, generalSupport.DateFormat()));
        $('#COVERDNULLDATE').val(generalSupport.ToJavaScriptDateCustom(row.DNULLDATE, generalSupport.DateFormat()));

    };
    this.COVERTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/COVERTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                COVERSCERTYPE1: row.SCERTYPE,
                COVERNBRANCH2: row.NBRANCH,
                COVERNPRODUCT3: row.NPRODUCT,
                COVERNPOLICY4: row.NPOLICY,
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
                title: 'Ramo de Reaseguro',
                formatter: 'HT5ConsultadePolizaSupport.REINSURANNBRANCH_REI_FormatterMaskData',
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
                formatter: 'HT5ConsultadePolizaSupport.REINSURANNTYPE_REIN_FormatterMaskData',
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
                formatter: 'HT5ConsultadePolizaSupport.REINSURANNCAPITAL_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NCURRENCY',
                title: 'Moneda',
                formatter: 'HT5ConsultadePolizaSupport.REINSURANNCURRENCY_FormatterMaskData',
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
                formatter: 'HT5ConsultadePolizaSupport.REINSURANNSHARE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }]
        });

        HT5ConsultadePolizaSupport.$el = table;
        HT5ConsultadePolizaSupport.REINSURANTblRequest();
      };

    this.REINSURANRowToInput = function (row) {
        HT5ConsultadePolizaSupport.currentRow = row;
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
            url: "/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/REINSURANTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                REINSURANSCERTYPE3: row.SCERTYPE,
                REINSURANNBRANCH4: row.NBRANCH,
                REINSURANNPRODUCT5: row.NPRODUCT,
                REINSURANNPOLICY6: row.NPOLICY,
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
        HT5ConsultadePolizaSupport.LookUpForREINSURAN2NCOMPANY('');
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'NBRANCH_REI',
                title: 'Ramo de Reaseguro',
                formatter: 'HT5ConsultadePolizaSupport.REINSURAN2NBRANCH_REI_FormatterMaskData',
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
                formatter: 'HT5ConsultadePolizaSupport.LookUpForREINSURAN2NCOMPANYFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NCAPITAL',
                title: 'Capital Cedido',
                formatter: 'HT5ConsultadePolizaSupport.REINSURAN2NCAPITAL_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NCURRENCY',
                title: 'Moneda',
                formatter: 'HT5ConsultadePolizaSupport.REINSURAN2NCURRENCY_FormatterMaskData',
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
                formatter: 'HT5ConsultadePolizaSupport.REINSURAN2NSHARE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NCOMMISSI',
                title: '%Comisión',
                formatter: 'HT5ConsultadePolizaSupport.REINSURAN2NCOMMISSI_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }]
        });

        HT5ConsultadePolizaSupport.$el = table;
        HT5ConsultadePolizaSupport.REINSURAN2TblRequest();
      };

    this.REINSURAN2RowToInput = function (row) {
        HT5ConsultadePolizaSupport.currentRow = row;
        AutoNumeric.set('#REINSURAN2NBRANCH_REI', row.NBRANCH_REI);
        $('#REINSURAN2NBRANCH_REIDesc').val(row.NBRANCH_REIDesc);
        HT5ConsultadePolizaSupport.LookUpForREINSURAN2NCOMPANY(row.NCOMPANY, '');
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
            url: "/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/REINSURAN2TblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                REINSURANSCERTYPE3: row.SCERTYPE,
                REINSURANNBRANCH4: row.NBRANCH,
                REINSURANNPRODUCT5: row.NPRODUCT,
                REINSURANNPOLICY6: row.NPOLICY,
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
    this.DISC_XPREMTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'NDISC_CODE',
                title: 'Recargo/Descuento/Impuesto',
                formatter: 'HT5ConsultadePolizaSupport.DISC_XPREMNDISC_CODE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NDISC_CODEDesc',
                title: 'Recargo/Descuento/Impuesto',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SAGREE',
                title: 'Recargo Aceptado',
                formatter: 'tableHelperSupport.IsCheck',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NCAUSE',
                title: 'Causa',
                formatter: 'HT5ConsultadePolizaSupport.DISC_XPREMNCAUSE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NCAUSEDesc',
                title: 'Causa',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NCURRENCY',
                title: 'Moneda',
                formatter: 'HT5ConsultadePolizaSupport.DISC_XPREMNCURRENCY_FormatterMaskData',
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
                title: 'Monto fijo',
                formatter: 'HT5ConsultadePolizaSupport.DISC_XPREMNAMOUNT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }]
        });

        HT5ConsultadePolizaSupport.$el = table;
        HT5ConsultadePolizaSupport.DISC_XPREMTblRequest();
      };

    this.DISC_XPREMRowToInput = function (row) {
        HT5ConsultadePolizaSupport.currentRow = row;
        AutoNumeric.set('#DISC_XPREMNDISC_CODE', row.NDISC_CODE);
        $('#DISC_XPREMNDISC_CODEDesc').val(row.NDISC_CODEDesc);
        $('#DISC_XPREMSAGREE').prop("checked", row.SAGREE);
        AutoNumeric.set('#DISC_XPREMNCAUSE', row.NCAUSE);
        $('#DISC_XPREMNCAUSEDesc').val(row.NCAUSEDesc);
        AutoNumeric.set('#DISC_XPREMNCURRENCY', row.NCURRENCY);
        $('#DISC_XPREMNCURRENCYDesc').val(row.NCURRENCYDesc);
        AutoNumeric.set('#DISC_XPREMNAMOUNT', row.NAMOUNT);

    };
    this.DISC_XPREMTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/DISC_XPREMTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                DISCXPREMSCERTYPE1: row.SCERTYPE,
                DISCXPREMNBRANCH2: row.NBRANCH,
                DISCXPREMNPRODUCT3: row.NPRODUCT,
                DISCXPREMNPOLICY4: row.NPOLICY,
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
    this.AUTOTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'SREGIST',
                title: 'Licencia',
                sortable: true,
                halign: 'center'
            }, {
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
                field: 'NVEHTYPE',
                title: 'Tipo de Vehículo',
                formatter: 'HT5ConsultadePolizaSupport.AUTONVEHTYPE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NVEHTYPEDesc',
                title: 'Tipo de Vehículo',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SMOTOR',
                title: 'Serial del Motor',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SCHASSIS',
                title: 'Chasis',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SCOLOR',
                title: 'Color',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NCAPITAL',
                title: 'Capital Asegurado',
                formatter: 'HT5ConsultadePolizaSupport.AUTONCAPITAL_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NVEH_VALOR',
                title: 'Valor del Vehículo',
                formatter: 'HT5ConsultadePolizaSupport.AUTONVEH_VALOR_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NYEAR',
                title: 'Año de Fabricación',
                formatter: 'HT5ConsultadePolizaSupport.AUTONYEAR_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NAUTOZONE',
                title: 'Zona de Circulación',
                formatter: 'HT5ConsultadePolizaSupport.AUTONAUTOZONE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NAUTOZONEDesc',
                title: 'Zona de Circulación',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NUSE',
                title: 'Uso del Vehículo',
                formatter: 'HT5ConsultadePolizaSupport.AUTONUSE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }]
        });

        HT5ConsultadePolizaSupport.$el = table;
        HT5ConsultadePolizaSupport.AUTOTblRequest();
      };

    this.AUTORowToInput = function (row) {
        HT5ConsultadePolizaSupport.currentRow = row;
        $('#AUTOSREGIST').val(row.SREGIST);
        $('#AUTOSLICENSE_TY').val(row.SLICENSE_TY);
        $('#AUTOSLICENSE_TYDesc').val(row.SLICENSE_TYDesc);
        AutoNumeric.set('#AUTONVEHTYPE', row.NVEHTYPE);
        $('#AUTONVEHTYPEDesc').val(row.NVEHTYPEDesc);
        $('#AUTOSMOTOR').val(row.SMOTOR);
        $('#AUTOSCHASSIS').val(row.SCHASSIS);
        $('#AUTOSCOLOR').val(row.SCOLOR);
        AutoNumeric.set('#AUTONCAPITAL', row.NCAPITAL);
        AutoNumeric.set('#AUTONVEH_VALOR', row.NVEH_VALOR);
        AutoNumeric.set('#AUTONYEAR', row.NYEAR);
        AutoNumeric.set('#AUTONAUTOZONE', row.NAUTOZONE);
        $('#AUTONAUTOZONEDesc').val(row.NAUTOZONEDesc);
        AutoNumeric.set('#AUTONUSE', row.NUSE);

    };
    this.AUTOTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/AUTOTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                AUTOSCERTYPE1: row.SCERTYPE,
                AUTONBRANCH2: row.NBRANCH,
                AUTONPRODUCT3: row.NPRODUCT,
                AUTONPOLICY4: row.NPOLICY,
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
    this.FIRETblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'NCONSTCAT',
                title: 'Categoría de Construcción',
                formatter: 'HT5ConsultadePolizaSupport.FIRENCONSTCAT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NCONSTCATDesc',
                title: 'Categoría de Construcción',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NACTIVITYCAT',
                title: 'Categoría de Actividad',
                formatter: 'HT5ConsultadePolizaSupport.FIRENACTIVITYCAT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NACTIVITYCATDesc',
                title: 'Categoría de Actividad',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NFLOOR_QUAN',
                title: 'Pisos del Edificio',
                formatter: 'HT5ConsultadePolizaSupport.FIRENFLOOR_QUAN_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NROOFTYPE',
                title: 'Tipo de Techo',
                formatter: 'HT5ConsultadePolizaSupport.FIRENROOFTYPE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NROOFTYPEDesc',
                title: 'Tipo de Techo',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NSEISMICZONE',
                title: 'Zona Sísmica',
                formatter: 'HT5ConsultadePolizaSupport.FIRENSEISMICZONE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NSEISMICZONEDesc',
                title: 'Zona Sísmica',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NBUILDTYPE',
                title: 'Tipo de Construcción Sísmica',
                formatter: 'HT5ConsultadePolizaSupport.FIRENBUILDTYPE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NBUILDTYPEDesc',
                title: 'Tipo de Construcción Sísmica',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NSPCOMBTYPE',
                title: 'Tipo de Combustión Espontánea',
                formatter: 'HT5ConsultadePolizaSupport.FIRENSPCOMBTYPE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NSPCOMBTYPEDesc',
                title: 'Tipo de Combustión Espontánea',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SDESCBUSSI',
                title: 'Descripción Específica',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NHURRICAN_ZONE',
                title: 'Zona de Huracán',
                formatter: 'HT5ConsultadePolizaSupport.FIRENHURRICAN_ZONE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NSIDECLOSETYPE',
                title: 'Cerramiento de Costado',
                formatter: 'HT5ConsultadePolizaSupport.FIRENSIDECLOSETYPE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NSIDECLOSETYPEDesc',
                title: 'Cerramiento de Costado',
                sortable: true,
                halign: 'center'
            }]
        });

        HT5ConsultadePolizaSupport.$el = table;
        HT5ConsultadePolizaSupport.FIRETblRequest();
      };

    this.FIRERowToInput = function (row) {
        HT5ConsultadePolizaSupport.currentRow = row;
        AutoNumeric.set('#FIRENCONSTCAT', row.NCONSTCAT);
        $('#FIRENCONSTCATDesc').val(row.NCONSTCATDesc);
        AutoNumeric.set('#FIRENACTIVITYCAT', row.NACTIVITYCAT);
        $('#FIRENACTIVITYCATDesc').val(row.NACTIVITYCATDesc);
        AutoNumeric.set('#FIRENFLOOR_QUAN', row.NFLOOR_QUAN);
        AutoNumeric.set('#FIRENROOFTYPE', row.NROOFTYPE);
        $('#FIRENROOFTYPEDesc').val(row.NROOFTYPEDesc);
        AutoNumeric.set('#FIRENSEISMICZONE', row.NSEISMICZONE);
        $('#FIRENSEISMICZONEDesc').val(row.NSEISMICZONEDesc);
        AutoNumeric.set('#FIRENBUILDTYPE', row.NBUILDTYPE);
        $('#FIRENBUILDTYPEDesc').val(row.NBUILDTYPEDesc);
        AutoNumeric.set('#FIRENSPCOMBTYPE', row.NSPCOMBTYPE);
        $('#FIRENSPCOMBTYPEDesc').val(row.NSPCOMBTYPEDesc);
        $('#FIRESDESCBUSSI').val(row.SDESCBUSSI);
        AutoNumeric.set('#FIRENHURRICAN_ZONE', row.NHURRICAN_ZONE);
        AutoNumeric.set('#FIRENSIDECLOSETYPE', row.NSIDECLOSETYPE);
        $('#FIRENSIDECLOSETYPEDesc').val(row.NSIDECLOSETYPEDesc);

    };
    this.FIRETblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/FIRETblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                FIRESCERTYPE1: row.SCERTYPE,
                FIRENBRANCH2: row.NBRANCH,
                FIRENPRODUCT3: row.NPRODUCT,
                FIRENPOLICY4: row.NPOLICY,
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
    this.HOMEOWNERTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'NOWNERSHIP',
                title: 'Ocupación',
                formatter: 'HT5ConsultadePolizaSupport.HOMEOWNERNOWNERSHIP_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NOWNERSHIPDesc',
                title: 'Ocupación',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NDWELLINGTYPE',
                title: 'Tipo de Vivienda',
                formatter: 'HT5ConsultadePolizaSupport.HOMEOWNERNDWELLINGTYPE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NDWELLINGTYPEDesc',
                title: 'Tipo de Vivienda',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NSEISMICZONE',
                title: 'Zona Sísmica',
                formatter: 'HT5ConsultadePolizaSupport.HOMEOWNERNSEISMICZONE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NYEAR_BUILT',
                title: 'Año de Construcción',
                formatter: 'HT5ConsultadePolizaSupport.HOMEOWNERNYEAR_BUILT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'DDATE_PURCH',
                title: 'Fecha de Compra de la Vivienda',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'NPRICE_PURCH',
                title: 'Precio de Compra',
                formatter: 'HT5ConsultadePolizaSupport.HOMEOWNERNPRICE_PURCH_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NCURRENCY_PURCH',
                title: 'Moneda de Precio de Compra',
                formatter: 'HT5ConsultadePolizaSupport.HOMEOWNERNCURRENCY_PURCH_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NCURRENCY_PURCHDesc',
                title: 'Moneda de Precio de Compra',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NLANDSUPER',
                title: 'Superficie del Terreno',
                formatter: 'HT5ConsultadePolizaSupport.HOMEOWNERNLANDSUPER_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NHOMESUPER',
                title: 'Superficie',
                formatter: 'HT5ConsultadePolizaSupport.HOMEOWNERNHOMESUPER_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NROOFTYPE',
                title: 'Tipo de Techo',
                formatter: 'HT5ConsultadePolizaSupport.HOMEOWNERNROOFTYPE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NROOFTYPEDesc',
                title: 'Tipo de Techo',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NROOFYEAR',
                title: 'Año del Techo',
                formatter: 'HT5ConsultadePolizaSupport.HOMEOWNERNROOFYEAR_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NFLOODZONE',
                title: 'Zona de Inundación',
                formatter: 'HT5ConsultadePolizaSupport.HOMEOWNERNFLOODZONE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NFLOODZONEDesc',
                title: 'Zona de Inundación',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NFOUNDTYPE',
                title: 'Fundación',
                formatter: 'HT5ConsultadePolizaSupport.HOMEOWNERNFOUNDTYPE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NFOUNDTYPEDesc',
                title: 'Fundación',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SSPRINKSYS',
                title: 'Posee Sistema de Riego',
                formatter: 'tableHelperSupport.IsCheck',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NAIRTYPE',
                title: 'Aire Acondicionado',
                formatter: 'HT5ConsultadePolizaSupport.HOMEOWNERNAIRTYPE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NAIRTYPEDesc',
                title: 'Aire Acondicionado',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NSTORIES',
                title: 'Pisos',
                formatter: 'HT5ConsultadePolizaSupport.HOMEOWNERNSTORIES_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NHALFBATH',
                title: 'Medios Baños',
                formatter: 'HT5ConsultadePolizaSupport.HOMEOWNERNHALFBATH_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NFULLBATH',
                title: 'Baños',
                formatter: 'HT5ConsultadePolizaSupport.HOMEOWNERNFULLBATH_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NBEDROOMS',
                title: 'Habitaciones',
                formatter: 'HT5ConsultadePolizaSupport.HOMEOWNERNBEDROOMS_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NFIREPLACE',
                title: 'Chimeneas',
                formatter: 'HT5ConsultadePolizaSupport.HOMEOWNERNFIREPLACE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NGARAGE',
                title: 'Cantidad de Vehículos',
                formatter: 'HT5ConsultadePolizaSupport.HOMEOWNERNGARAGE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'SANIMALSDES',
                title: 'Animales/mascotas',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SATTACKEDIND',
                title: 'Ataque Previo',
                formatter: 'tableHelperSupport.IsCheck',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NDIST_FIRE',
                title: 'Distancia Bomberos',
                formatter: 'HT5ConsultadePolizaSupport.HOMEOWNERNDIST_FIRE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'SALARM_COMP',
                title: 'Proveedor de Sistema de Alarma',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SFENCEPOOL',
                title: 'Piscina Con Cerca',
                formatter: 'tableHelperSupport.IsCheck',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NSWIMPOOL',
                title: 'Ubicación de la Piscina',
                formatter: 'HT5ConsultadePolizaSupport.HOMEOWNERNSWIMPOOL_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NSWIMPOOLDesc',
                title: 'Ubicación de la Piscina',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NFENCEHEIGHT',
                title: 'Altura de la Cerca',
                formatter: 'HT5ConsultadePolizaSupport.HOMEOWNERNFENCEHEIGHT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'STRAMPOLINE',
                title: 'Trampolín',
                formatter: 'tableHelperSupport.IsCheck',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SGAS',
                title: 'Depósito de Gasolina',
                formatter: 'tableHelperSupport.IsCheck',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NEXTERCONSTR',
                title: 'Material de Construcción',
                formatter: 'HT5ConsultadePolizaSupport.HOMEOWNERNEXTERCONSTR_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NEXTERCONSTRDesc',
                title: 'Material de Construcción',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SOTHER_CONSTR',
                title: 'Otros Materiales de Construcción',
                sortable: true,
                halign: 'center'
            }]
        });

        HT5ConsultadePolizaSupport.$el = table;
        HT5ConsultadePolizaSupport.HOMEOWNERTblRequest();
      };

    this.HOMEOWNERRowToInput = function (row) {
        HT5ConsultadePolizaSupport.currentRow = row;
        AutoNumeric.set('#HOMEOWNERNOWNERSHIP', row.NOWNERSHIP);
        $('#HOMEOWNERNOWNERSHIPDesc').val(row.NOWNERSHIPDesc);
        AutoNumeric.set('#HOMEOWNERNDWELLINGTYPE', row.NDWELLINGTYPE);
        $('#HOMEOWNERNDWELLINGTYPEDesc').val(row.NDWELLINGTYPEDesc);
        AutoNumeric.set('#HOMEOWNERNSEISMICZONE', row.NSEISMICZONE);
        AutoNumeric.set('#HOMEOWNERNYEAR_BUILT', row.NYEAR_BUILT);
        $('#HOMEOWNERDDATE_PURCH').val(generalSupport.ToJavaScriptDateCustom(row.DDATE_PURCH, generalSupport.DateFormat()));
        AutoNumeric.set('#HOMEOWNERNPRICE_PURCH', row.NPRICE_PURCH);
        AutoNumeric.set('#HOMEOWNERNCURRENCY_PURCH', row.NCURRENCY_PURCH);
        $('#HOMEOWNERNCURRENCY_PURCHDesc').val(row.NCURRENCY_PURCHDesc);
        AutoNumeric.set('#HOMEOWNERNLANDSUPER', row.NLANDSUPER);
        AutoNumeric.set('#HOMEOWNERNHOMESUPER', row.NHOMESUPER);
        AutoNumeric.set('#HOMEOWNERNROOFTYPE', row.NROOFTYPE);
        $('#HOMEOWNERNROOFTYPEDesc').val(row.NROOFTYPEDesc);
        AutoNumeric.set('#HOMEOWNERNROOFYEAR', row.NROOFYEAR);
        AutoNumeric.set('#HOMEOWNERNFLOODZONE', row.NFLOODZONE);
        $('#HOMEOWNERNFLOODZONEDesc').val(row.NFLOODZONEDesc);
        AutoNumeric.set('#HOMEOWNERNFOUNDTYPE', row.NFOUNDTYPE);
        $('#HOMEOWNERNFOUNDTYPEDesc').val(row.NFOUNDTYPEDesc);
        $('#HOMEOWNERSSPRINKSYS').prop("checked", row.SSPRINKSYS);
        AutoNumeric.set('#HOMEOWNERNAIRTYPE', row.NAIRTYPE);
        $('#HOMEOWNERNAIRTYPEDesc').val(row.NAIRTYPEDesc);
        AutoNumeric.set('#HOMEOWNERNSTORIES', row.NSTORIES);
        AutoNumeric.set('#HOMEOWNERNHALFBATH', row.NHALFBATH);
        AutoNumeric.set('#HOMEOWNERNFULLBATH', row.NFULLBATH);
        AutoNumeric.set('#HOMEOWNERNBEDROOMS', row.NBEDROOMS);
        AutoNumeric.set('#HOMEOWNERNFIREPLACE', row.NFIREPLACE);
        AutoNumeric.set('#HOMEOWNERNGARAGE', row.NGARAGE);
        $('#HOMEOWNERSANIMALSDES').val(row.SANIMALSDES);
        $('#HOMEOWNERSATTACKEDIND').prop("checked", row.SATTACKEDIND);
        AutoNumeric.set('#HOMEOWNERNDIST_FIRE', row.NDIST_FIRE);
        $('#HOMEOWNERSALARM_COMP').val(row.SALARM_COMP);
        $('#HOMEOWNERSFENCEPOOL').prop("checked", row.SFENCEPOOL);
        AutoNumeric.set('#HOMEOWNERNSWIMPOOL', row.NSWIMPOOL);
        $('#HOMEOWNERNSWIMPOOLDesc').val(row.NSWIMPOOLDesc);
        AutoNumeric.set('#HOMEOWNERNFENCEHEIGHT', row.NFENCEHEIGHT);
        $('#HOMEOWNERSTRAMPOLINE').prop("checked", row.STRAMPOLINE);
        $('#HOMEOWNERSGAS').prop("checked", row.SGAS);
        AutoNumeric.set('#HOMEOWNERNEXTERCONSTR', row.NEXTERCONSTR);
        $('#HOMEOWNERNEXTERCONSTRDesc').val(row.NEXTERCONSTRDesc);
        $('#HOMEOWNERSOTHER_CONSTR').val(row.SOTHER_CONSTR);

    };
    this.HOMEOWNERTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/HOMEOWNERTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                HOMEOWNERSCERTYPE1: row.SCERTYPE,
                HOMEOWNERNBRANCH2: row.NBRANCH,
                HOMEOWNERNPRODUCT3: row.NPRODUCT,
                HOMEOWNERNPOLICY4: row.NPOLICY,
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
    this.LIFETblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'SPDURAIND',
                title: 'Tipo de Duración de Pagos',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'SPDURAINDDesc',
                title: 'Tipo de Duración de Pagos',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NPAY_TIME',
                title: 'Duración de Pagos',
                formatter: 'HT5ConsultadePolizaSupport.LIFENPAY_TIME_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NTYPDURINS',
                title: 'Tipo de Duración del Seguro',
                formatter: 'HT5ConsultadePolizaSupport.LIFENTYPDURINS_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NTYPDURINSDesc',
                title: 'Tipo de Duración del Seguro',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NINSUR_TIME',
                title: 'Duración del Seguro',
                formatter: 'HT5ConsultadePolizaSupport.LIFENINSUR_TIME_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NAGE_REINSU',
                title: 'Edad Actuarial',
                formatter: 'HT5ConsultadePolizaSupport.LIFENAGE_REINSU_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NAGE_LIMIT',
                title: 'Edad Límite',
                formatter: 'HT5ConsultadePolizaSupport.LIFENAGE_LIMIT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NAGE',
                title: 'Edad del Asegurado',
                formatter: 'HT5ConsultadePolizaSupport.LIFENAGE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }]
        });

        HT5ConsultadePolizaSupport.$el = table;
        HT5ConsultadePolizaSupport.LIFETblRequest();
      };

    this.LIFERowToInput = function (row) {
        HT5ConsultadePolizaSupport.currentRow = row;
        $('#LIFESPDURAIND').val(row.SPDURAIND);
        $('#LIFESPDURAINDDesc').val(row.SPDURAINDDesc);
        AutoNumeric.set('#LIFENPAY_TIME', row.NPAY_TIME);
        AutoNumeric.set('#LIFENTYPDURINS', row.NTYPDURINS);
        $('#LIFENTYPDURINSDesc').val(row.NTYPDURINSDesc);
        AutoNumeric.set('#LIFENINSUR_TIME', row.NINSUR_TIME);
        AutoNumeric.set('#LIFENAGE_REINSU', row.NAGE_REINSU);
        AutoNumeric.set('#LIFENAGE_LIMIT', row.NAGE_LIMIT);
        AutoNumeric.set('#LIFENAGE', row.NAGE);

    };
    this.LIFETblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/LIFETblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                LIFESCERTYPE1: row.SCERTYPE,
                LIFENBRANCH2: row.NBRANCH,
                LIFENPRODUCT3: row.NPRODUCT,
                LIFENPOLICY4: row.NPOLICY,
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
                formatter: 'HT5ConsultadePolizaSupport.THEFTNCOMMERGRP_FormatterMaskData',
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
                formatter: 'HT5ConsultadePolizaSupport.THEFTNINSURED_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NEMPLOYEES',
                title: 'Transportistas',
                formatter: 'HT5ConsultadePolizaSupport.THEFTNEMPLOYEES_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NAREA',
                title: 'Área',
                formatter: 'HT5ConsultadePolizaSupport.THEFTNAREA_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NVIGILANCE',
                title: 'Vigilantes',
                formatter: 'HT5ConsultadePolizaSupport.THEFTNVIGILANCE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NCAPITAL',
                title: 'Capital Asegurado',
                formatter: 'HT5ConsultadePolizaSupport.THEFTNCAPITAL_FormatterMaskData',
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
                formatter: 'HT5ConsultadePolizaSupport.THEFTNNULLCODE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NNULLCODEDesc',
                title: 'Código de Anulación',
                sortable: true,
                halign: 'center'
            }]
        });

        HT5ConsultadePolizaSupport.$el = table;
        HT5ConsultadePolizaSupport.THEFTTblRequest();
      };

    this.THEFTRowToInput = function (row) {
        HT5ConsultadePolizaSupport.currentRow = row;
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
        $('#THEFTNNULLCODEDesc').val(row.NNULLCODEDesc);

    };
    this.THEFTTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/THEFTTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                THEFTSCERTYPE1: row.SCERTYPE,
                THEFTNBRANCH2: row.NBRANCH,
                THEFTNPRODUCT3: row.NPRODUCT,
                THEFTNPOLICY4: row.NPOLICY,
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
    this.ROLES2TblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'NROLE',
                title: 'Figura',
                formatter: 'HT5ConsultadePolizaSupport.ROLES2NROLE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NROLEDesc',
                title: 'Figura',
                sortable: true,
                halign: 'center'
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
                field: 'DEFFECDATE',
                title: 'Fecha de efecto',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }]
        });

        HT5ConsultadePolizaSupport.$el = table;
        HT5ConsultadePolizaSupport.ROLES2TblRequest();
      };

    this.ROLES2RowToInput = function (row) {
        HT5ConsultadePolizaSupport.currentRow = row;
        AutoNumeric.set('#ROLES2NROLE', row.NROLE);
        $('#ROLES2NROLEDesc').val(row.NROLEDesc);
        $('#ROLES2SCLIENT').val(row.SCLIENT);
        $('#ROLES2SCLIENTDesc').val(row.SCLIENTDesc);
        $('#ROLES2DEFFECDATE').val(generalSupport.ToJavaScriptDateCustom(row.DEFFECDATE, generalSupport.DateFormat()));

    };
    this.ROLES2TblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/ROLES2TblDataLoad",
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

        var detailShow = HT5ConsultadePolizaSupport.ROLES2_ShowValidation(row);
        if (detailShow)
        html.push('<table id="ROLES2Tbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Roles</caption></table>');

        $detail.html(html.join(""));
        if (detailShow)
        HT5ConsultadePolizaSupport.ROLES2TblSetup($detail.find('#ROLES2Tbl-' + index));

    };
    this.HEALTHTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            detailView: true,
            onExpandRow: HT5ConsultadePolizaSupport.HEALTHTblExpandRow,

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
                field: 'NCAPITAL',
                title: 'Capital Asegurado',
                formatter: 'HT5ConsultadePolizaSupport.HEALTHNCAPITAL_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NPREMIUM',
                title: 'Monto de prima.',
                formatter: 'HT5ConsultadePolizaSupport.HEALTHNPREMIUM_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NTARIFF',
                title: 'Tarifa de salud.',
                formatter: 'HT5ConsultadePolizaSupport.HEALTHNTARIFF_FormatterMaskData',
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
                formatter: 'HT5ConsultadePolizaSupport.HEALTHNNULLCODE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NNULLCODEDesc',
                title: 'Código de Anulación',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SCERTYPE',
                title: 'TipoDeRegistro',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'NBRANCH',
                title: 'RamoComercial',
                formatter: 'HT5ConsultadePolizaSupport.HEALTHNBRANCH_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NPRODUCT',
                title: 'CódigoDelProducto',
                formatter: 'HT5ConsultadePolizaSupport.HEALTHNPRODUCT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NPOLICY',
                title: 'NúmeroDeLaPóliza',
                formatter: 'HT5ConsultadePolizaSupport.HEALTHNPOLICY_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NCERTIF',
                title: 'Número del certificado',
                formatter: 'HT5ConsultadePolizaSupport.HEALTHNCERTIF_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }]
        });

        HT5ConsultadePolizaSupport.$el = table;
        HT5ConsultadePolizaSupport.HEALTHTblRequest();
      };

    this.HEALTHRowToInput = function (row) {
        HT5ConsultadePolizaSupport.currentRow = row;
        $('#HEALTHDSTARTDATE').val(generalSupport.ToJavaScriptDateCustom(row.DSTARTDATE, generalSupport.DateFormat()));
        $('#HEALTHDEXPIRDAT').val(generalSupport.ToJavaScriptDateCustom(row.DEXPIRDAT, generalSupport.DateFormat()));
        AutoNumeric.set('#HEALTHNCAPITAL', row.NCAPITAL);
        AutoNumeric.set('#HEALTHNPREMIUM', row.NPREMIUM);
        AutoNumeric.set('#HEALTHNTARIFF', row.NTARIFF);
        $('#HEALTHDNULLDATE').val(generalSupport.ToJavaScriptDateCustom(row.DNULLDATE, generalSupport.DateFormat()));
        AutoNumeric.set('#HEALTHNNULLCODE', row.NNULLCODE);
        $('#HEALTHNNULLCODEDesc').val(row.NNULLCODEDesc);
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
            url: "/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/HEALTHTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                HEALTHSCERTYPE1: row.SCERTYPE,
                HEALTHNBRANCH2: row.NBRANCH,
                HEALTHNPRODUCT3: row.NPRODUCT,
                HEALTHNPOLICY4: row.NPOLICY,
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
    this.FINANCIAL_INSTRUMENTSTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'NCONSECUTIVE',
                title: '#',
                formatter: 'HT5ConsultadePolizaSupport.FINANCIAL_INSTRUMENTSNCONSECUTIVE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NINSTRUMENT_TY',
                title: 'Tipo',
                formatter: 'HT5ConsultadePolizaSupport.FINANCIAL_INSTRUMENTSNINSTRUMENT_TY_FormatterMaskData',
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
                formatter: 'HT5ConsultadePolizaSupport.FINANCIAL_INSTRUMENTSNBANK_CODE_FormatterMaskData',
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
                title: 'Tipo de tarjeta',
                formatter: 'HT5ConsultadePolizaSupport.FINANCIAL_INSTRUMENTSNCARD_TYPE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NCARD_TYPEDesc',
                title: 'Tipo de tarjeta',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SNUMBER',
                title: '#Tarjeta de crédito',
                sortable: true,
                halign: 'center'
            }, {
                field: 'DCARDEXPIR',
                title: 'F.Vencimiento Tarjeta',
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
                formatter: 'HT5ConsultadePolizaSupport.FINANCIAL_INSTRUMENTSNQUOTA_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NAMOUNT',
                title: 'Importe del crédito',
                formatter: 'HT5ConsultadePolizaSupport.FINANCIAL_INSTRUMENTSNAMOUNT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NCURRENCY',
                title: 'Moneda',
                formatter: 'HT5ConsultadePolizaSupport.FINANCIAL_INSTRUMENTSNCURRENCY_FormatterMaskData',
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

        HT5ConsultadePolizaSupport.$el = table;
        HT5ConsultadePolizaSupport.FINANCIAL_INSTRUMENTSTblRequest();
      };

    this.FINANCIAL_INSTRUMENTSRowToInput = function (row) {
        HT5ConsultadePolizaSupport.currentRow = row;
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
            url: "/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/FINANCIAL_INSTRUMENTSTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                FINANCIALINSTRUMENTSSCERTYPE1: row.SCERTYPE,
                FINANCIALINSTRUMENTSNBRANCH2: row.NBRANCH,
                FINANCIALINSTRUMENTSNPRODUCT3: row.NPRODUCT,
                FINANCIALINSTRUMENTSNPOLICY4: row.NPOLICY,
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
    this.PROTECTIONTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'NUSERCODE',
                title: 'Código de Usuario',
                formatter: 'HT5ConsultadePolizaSupport.PROTECTIONNUSERCODE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }]
        });

        HT5ConsultadePolizaSupport.$el = table;
        HT5ConsultadePolizaSupport.PROTECTIONTblRequest();
      };

    this.PROTECTIONRowToInput = function (row) {
        HT5ConsultadePolizaSupport.currentRow = row;
        AutoNumeric.set('#PROTECTIONNUSERCODE', row.NUSERCODE);

    };
    this.PROTECTIONTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/PROTECTIONTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                PROTECTIONSCERTYPE1: row.SCERTYPE,
                PROTECTIONNBRANCH2: row.NBRANCH,
                PROTECTIONNPRODUCT3: row.NPRODUCT,
                PROTECTIONNPOLICY4: row.NPOLICY,
                PROTECTIONNCERTIF5: row.NCERTIF
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
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'NMODULEC',
                title: 'Módulo de Cobertura',
                formatter: 'HT5ConsultadePolizaSupport.BENEFICIARNMODULEC_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NMODULECDesc',
                title: 'Módulo de Cobertura',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NCOVER',
                title: 'Código de la cobertura',
                formatter: 'HT5ConsultadePolizaSupport.BENEFICIARNCOVER_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'SCLIENT',
                title: 'Código de Cliente',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'SCLIENTDesc',
                title: 'Código de Cliente',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NRELATION',
                title: 'Nexo',
                formatter: 'HT5ConsultadePolizaSupport.BENEFICIARNRELATION_FormatterMaskData',
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
                formatter: 'HT5ConsultadePolizaSupport.BENEFICIARNPARTICIP_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'SIRREVOC',
                title: 'Beneficiario Irrevocable',
                formatter: 'tableHelperSupport.IsCheck',
                sortable: true,
                halign: 'center'
            }]
        });

        HT5ConsultadePolizaSupport.$el = table;
        HT5ConsultadePolizaSupport.BENEFICIARTblRequest();
      };

    this.BENEFICIARRowToInput = function (row) {
        HT5ConsultadePolizaSupport.currentRow = row;
        AutoNumeric.set('#BENEFICIARNMODULEC', row.NMODULEC);
        $('#BENEFICIARNMODULECDesc').val(row.NMODULECDesc);
        AutoNumeric.set('#BENEFICIARNCOVER', row.NCOVER);
        $('#BENEFICIARSCLIENT').val(row.SCLIENT);
        $('#BENEFICIARSCLIENTDesc').val(row.SCLIENTDesc);
        AutoNumeric.set('#BENEFICIARNRELATION', row.NRELATION);
        $('#BENEFICIARNRELATIONDesc').val(row.NRELATIONDesc);
        AutoNumeric.set('#BENEFICIARNPARTICIP', row.NPARTICIP);
        $('#BENEFICIARSIRREVOC').prop("checked", row.SIRREVOC);

    };
    this.BENEFICIARTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/BENEFICIARTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                BENEFICIARSCERTYPE1: row.SCERTYPE,
                BENEFICIARNBRANCH2: row.NBRANCH,
                BENEFICIARNPRODUCT3: row.NPRODUCT,
                BENEFICIARNPOLICY4: row.NPOLICY,
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
    this.CLAUSETblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'NCLAUSE',
                title: 'Código de la Cláusula',
                formatter: 'HT5ConsultadePolizaSupport.CLAUSENCLAUSE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NNOTENUM',
                title: 'Número de Nota',
                sortable: true,
                halign: 'center',
                align: 'right'
            }]
        });

        HT5ConsultadePolizaSupport.$el = table;
        HT5ConsultadePolizaSupport.CLAUSETblRequest();
      };

    this.CLAUSERowToInput = function (row) {
        HT5ConsultadePolizaSupport.currentRow = row;
        AutoNumeric.set('#CLAUSENCLAUSE', row.NCLAUSE);
        $('#CLAUSENNOTENUM').val(row.NNOTENUM);

    };
    this.CLAUSETblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/CLAUSETblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                CLAUSESCERTYPE1: row.SCERTYPE,
                CLAUSENBRANCH2: row.NBRANCH,
                CLAUSENPRODUCT3: row.NPRODUCT,
                CLAUSENPOLICY4: row.NPOLICY,
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
                field: 'NTRANSACTIO',
                title: '#Transacción',
                formatter: 'HT5ConsultadePolizaSupport.POLICY_HISNTRANSACTIO_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NMOVEMENT',
                title: 'Movimiento',
                formatter: 'HT5ConsultadePolizaSupport.POLICY_HISNMOVEMENT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NTYPE_HIST',
                title: 'Tipo',
                formatter: 'HT5ConsultadePolizaSupport.POLICY_HISNTYPE_HIST_FormatterMaskData',
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
                field: 'NRECEIPT',
                title: 'Recibo',
                formatter: 'HT5ConsultadePolizaSupport.POLICY_HISNRECEIPT_FormatterMaskData',
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

        HT5ConsultadePolizaSupport.$el = table;
        HT5ConsultadePolizaSupport.POLICY_HISTblRequest();
      };

    this.POLICY_HISRowToInput = function (row) {
        HT5ConsultadePolizaSupport.currentRow = row;
        AutoNumeric.set('#POLICY_HISNTRANSACTIO', row.NTRANSACTIO);
        AutoNumeric.set('#POLICY_HISNMOVEMENT', row.NMOVEMENT);
        AutoNumeric.set('#POLICY_HISNTYPE_HIST', row.NTYPE_HIST);
        $('#POLICY_HISNTYPE_HISTDesc').val(row.NTYPE_HISTDesc);
        $('#POLICY_HISDEFFECDATE').val(generalSupport.ToJavaScriptDateCustom(row.DEFFECDATE, generalSupport.DateFormat()));
        AutoNumeric.set('#POLICY_HISNRECEIPT', row.NRECEIPT);
        $('#POLICY_HISSNULL_MOVE').prop("checked", row.SNULL_MOVE);
        $('#POLICY_HISDNULLDATE').val(generalSupport.ToJavaScriptDateCustom(row.DNULLDATE, generalSupport.DateFormat()));

    };
    this.POLICY_HISTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/POLICY_HISTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                POLICYHISSCERTYPE1: row.SCERTYPE,
                POLICYHISNBRANCH2: row.NBRANCH,
                POLICYHISNPRODUCT3: row.NPRODUCT,
                POLICYHISNPOLICY4: row.NPOLICY,
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
    this.FINANC_DRATblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'NDRAFT',
                title: 'Número de Cuota',
                formatter: 'HT5ConsultadePolizaSupport.FINANC_DRANDRAFT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NAMOUNT',
                title: 'Monto de la cuota',
                formatter: 'HT5ConsultadePolizaSupport.FINANC_DRANAMOUNT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NSTAT_DRAFT',
                title: 'Estado de la Cuota',
                formatter: 'HT5ConsultadePolizaSupport.FINANC_DRANSTAT_DRAFT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NSTAT_DRAFTDesc',
                title: 'Estado de la Cuota',
                sortable: true,
                halign: 'center'
            }]
        });

        HT5ConsultadePolizaSupport.$el = table;
        HT5ConsultadePolizaSupport.FINANC_DRATblRequest();
      };

    this.FINANC_DRARowToInput = function (row) {
        HT5ConsultadePolizaSupport.currentRow = row;
        AutoNumeric.set('#FINANC_DRANDRAFT', row.NDRAFT);
        AutoNumeric.set('#FINANC_DRANAMOUNT', row.NAMOUNT);
        AutoNumeric.set('#FINANC_DRANSTAT_DRAFT', row.NSTAT_DRAFT);
        $('#FINANC_DRANSTAT_DRAFTDesc').val(row.NSTAT_DRAFTDesc);

    };
    this.FINANC_DRATblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/FINANC_DRATblDataLoad",
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
    this.FINANC_PRETblExpandRow = function (index, row, $detail) {
       var tblparentid = $detail.parent().parent().parent()[0].id;
       var html = [];

        var detailShow = HT5ConsultadePolizaSupport.FINANC_DRA_ShowValidation(row);
        if (detailShow)
        html.push('<table id="FINANC_DRATbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Giro de Contrato Financieros</caption></table>');

        $detail.html(html.join(""));
        if (detailShow)
        HT5ConsultadePolizaSupport.FINANC_DRATblSetup($detail.find('#FINANC_DRATbl-' + index));

    };
    this.FINANC_PRETblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            detailView: true,
            onExpandRow: HT5ConsultadePolizaSupport.FINANC_PRETblExpandRow,

            columns: [{
                field: 'NCONTRAT',
                title: 'Número de contrato',
                formatter: 'HT5ConsultadePolizaSupport.FINANC_PRENCONTRAT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'DSTARTDATE',
                title: 'Fecha de Efecto',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'NCURRENCY',
                title: 'Moneda',
                formatter: 'HT5ConsultadePolizaSupport.FINANC_PRENCURRENCY_FormatterMaskData',
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
                title: 'Monto de prima',
                formatter: 'HT5ConsultadePolizaSupport.FINANC_PRENPREMIUM_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }]
        });

        HT5ConsultadePolizaSupport.$el = table;
        HT5ConsultadePolizaSupport.FINANC_PRETblRequest();
      };

    this.FINANC_PRERowToInput = function (row) {
        HT5ConsultadePolizaSupport.currentRow = row;
        AutoNumeric.set('#FINANC_PRENCONTRAT', row.NCONTRAT);
        $('#FINANC_PREDSTARTDATE').val(generalSupport.ToJavaScriptDateCustom(row.DSTARTDATE, generalSupport.DateFormat()));
        AutoNumeric.set('#FINANC_PRENCURRENCY', row.NCURRENCY);
        $('#FINANC_PRENCURRENCYDesc').val(row.NCURRENCYDesc);
        AutoNumeric.set('#FINANC_PRENPREMIUM', row.NPREMIUM);

    };
    this.FINANC_PRETblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/FINANC_PRETblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                FINANCPRENRECEIPT1: row.NRECEIPT
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
                formatter: 'HT5ConsultadePolizaSupport.PREMIUM_CENCERTIF_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NID_BILL',
                title: 'Consecutivo',
                formatter: 'HT5ConsultadePolizaSupport.PREMIUM_CENID_BILL_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'STYPE_DETAI',
                title: 'Tipo de Detalle',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NCODE_DETAI',
                title: 'Detalle facturado',
                formatter: 'HT5ConsultadePolizaSupport.PREMIUM_CENCODE_DETAI_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NBILL_ITEM',
                title: 'Concepto de Facturación',
                formatter: 'HT5ConsultadePolizaSupport.PREMIUM_CENBILL_ITEM_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NBILL_ITEMDesc',
                title: 'Concepto de Facturación',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NPREMIUM',
                title: 'Monto de prima',
                formatter: 'HT5ConsultadePolizaSupport.PREMIUM_CENPREMIUM_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }]
        });

        HT5ConsultadePolizaSupport.$el = table;
        HT5ConsultadePolizaSupport.PREMIUM_CETblRequest();
      };

    this.PREMIUM_CERowToInput = function (row) {
        HT5ConsultadePolizaSupport.currentRow = row;
        AutoNumeric.set('#PREMIUM_CENCERTIF', row.NCERTIF);
        AutoNumeric.set('#PREMIUM_CENID_BILL', row.NID_BILL);
        $('#PREMIUM_CESTYPE_DETAI').val(row.STYPE_DETAI);
        AutoNumeric.set('#PREMIUM_CENCODE_DETAI', row.NCODE_DETAI);
        AutoNumeric.set('#PREMIUM_CENBILL_ITEM', row.NBILL_ITEM);
        $('#PREMIUM_CENBILL_ITEMDesc').val(row.NBILL_ITEMDesc);
        AutoNumeric.set('#PREMIUM_CENPREMIUM', row.NPREMIUM);

    };
    this.PREMIUM_CETblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/PREMIUM_CETblDataLoad",
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
    this.PREMIUMTblExpandRow = function (index, row, $detail) {
       var tblparentid = $detail.parent().parent().parent()[0].id;
       var html = [];

        var detailShow = HT5ConsultadePolizaSupport.FINANC_PRE_ShowValidation(row);
        if (detailShow)
        html.push('<table id="FINANC_PRETbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Contrato de financiamiento</caption></table>');
        html.push('<table id="PREMIUM_CETbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Certificados facturados</caption></table>');

        $detail.html(html.join(""));
        if (detailShow)
        HT5ConsultadePolizaSupport.FINANC_PRETblSetup($detail.find('#FINANC_PRETbl-' + index));
        HT5ConsultadePolizaSupport.PREMIUM_CETblSetup($detail.find('#PREMIUM_CETbl-' + index));

    };
    this.PREMIUMTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            detailView: true,
            onExpandRow: HT5ConsultadePolizaSupport.PREMIUMTblExpandRow,

            columns: [{
                field: 'NRECEIPT',
                title: 'Número del Recibo',
                formatter: 'HT5ConsultadePolizaSupport.PREMIUMNRECEIPT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NCURRENCY',
                title: 'Moneda',
                formatter: 'HT5ConsultadePolizaSupport.PREMIUMNCURRENCY_FormatterMaskData',
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
                title: 'Monto de prima',
                formatter: 'HT5ConsultadePolizaSupport.PREMIUMNPREMIUM_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'DISSUEDAT',
                title: 'Fecha de Emisión',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'DEFFECDATE',
                title: 'Inico de vigencia',
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
                field: 'NSTATUS_PRE',
                title: 'Estado de la Factura',
                formatter: 'HT5ConsultadePolizaSupport.PREMIUMNSTATUS_PRE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NSTATUS_PREDesc',
                title: 'Estado de la Factura',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NWAY_PAY',
                title: 'Vía de Pago',
                formatter: 'HT5ConsultadePolizaSupport.PREMIUMNWAY_PAY_FormatterMaskData',
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
                field: 'SSTATUSVA',
                title: 'Estado del Registro',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'SSTATUSVADesc',
                title: 'Estado del Registro',
                sortable: true,
                halign: 'center'
            }, {
                field: 'DLIMITDATE',
                title: 'Fecha Límite de Pago',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'NTRATYPEI',
                title: 'Origen del recibo',
                formatter: 'HT5ConsultadePolizaSupport.PREMIUMNTRATYPEI_FormatterMaskData',
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
                field: 'NINTERMED',
                title: 'Código de Productor',
                formatter: 'HT5ConsultadePolizaSupport.PREMIUMNINTERMED_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NPARTICIP',
                title: 'Porcentaje de participación',
                formatter: 'HT5ConsultadePolizaSupport.PREMIUMNPARTICIP_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NTAXAMOU',
                title: 'Monto de Impuesto',
                formatter: 'HT5ConsultadePolizaSupport.PREMIUMNTAXAMOU_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NCOMAMOU',
                title: 'Monto de comisión',
                formatter: 'HT5ConsultadePolizaSupport.PREMIUMNCOMAMOU_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NTYPE',
                title: 'Tipo de factura',
                formatter: 'HT5ConsultadePolizaSupport.LookUpForPREMIUMNTYPEFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NCONTRAT',
                title: 'ContratoDeFinanciamiento',
                formatter: 'HT5ConsultadePolizaSupport.PREMIUMNCONTRAT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'SCERTYPE',
                title: 'TipoDeRegistro',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'NBRANCH',
                title: 'RamoComercial',
                formatter: 'HT5ConsultadePolizaSupport.PREMIUMNBRANCH_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NPRODUCT',
                title: 'CódigoDelProducto',
                formatter: 'HT5ConsultadePolizaSupport.PREMIUMNPRODUCT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NDIGIT',
                title: 'DígitoDeControl',
                formatter: 'HT5ConsultadePolizaSupport.PREMIUMNDIGIT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NPAYNUMBE',
                title: 'NúmeroDePago',
                formatter: 'HT5ConsultadePolizaSupport.PREMIUMNPAYNUMBE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }]
        });

        HT5ConsultadePolizaSupport.$el = table;
        HT5ConsultadePolizaSupport.PREMIUMTblRequest();
      };

    this.PREMIUMRowToInput = function (row) {
        HT5ConsultadePolizaSupport.currentRow = row;
        AutoNumeric.set('#PREMIUMNRECEIPT', row.NRECEIPT);
        AutoNumeric.set('#PREMIUMNCURRENCY', row.NCURRENCY);
        $('#PREMIUMNCURRENCYDesc').val(row.NCURRENCYDesc);
        AutoNumeric.set('#PREMIUMNPREMIUM', row.NPREMIUM);
        $('#PREMIUMDISSUEDAT').val(generalSupport.ToJavaScriptDateCustom(row.DISSUEDAT, generalSupport.DateFormat()));
        $('#PREMIUMDEFFECDATE').val(generalSupport.ToJavaScriptDateCustom(row.DEFFECDATE, generalSupport.DateFormat()));
        $('#PREMIUMDEXPIRDAT').val(generalSupport.ToJavaScriptDateCustom(row.DEXPIRDAT, generalSupport.DateFormat()));
        AutoNumeric.set('#PREMIUMNSTATUS_PRE', row.NSTATUS_PRE);
        $('#PREMIUMNSTATUS_PREDesc').val(row.NSTATUS_PREDesc);
        AutoNumeric.set('#PREMIUMNWAY_PAY', row.NWAY_PAY);
        $('#PREMIUMNWAY_PAYDesc').val(row.NWAY_PAYDesc);
        $('#PREMIUMSSTATUSVA').val(row.SSTATUSVA);
        $('#PREMIUMSSTATUSVADesc').val(row.SSTATUSVADesc);
        $('#PREMIUMDLIMITDATE').val(generalSupport.ToJavaScriptDateCustom(row.DLIMITDATE, generalSupport.DateFormat()));
        AutoNumeric.set('#PREMIUMNTRATYPEI', row.NTRATYPEI);
        $('#PREMIUMNTRATYPEIDesc').val(row.NTRATYPEIDesc);
        AutoNumeric.set('#PREMIUMNINTERMED', row.NINTERMED);
        AutoNumeric.set('#PREMIUMNPARTICIP', row.NPARTICIP);
        AutoNumeric.set('#PREMIUMNTAXAMOU', row.NTAXAMOU);
        AutoNumeric.set('#PREMIUMNCOMAMOU', row.NCOMAMOU);
        AutoNumeric.set('#PREMIUMNCONTRAT', row.NCONTRAT);
        $('#PREMIUMSCERTYPE').val(row.SCERTYPE);
        AutoNumeric.set('#PREMIUMNBRANCH', row.NBRANCH);
        AutoNumeric.set('#PREMIUMNPRODUCT', row.NPRODUCT);
        AutoNumeric.set('#PREMIUMNDIGIT', row.NDIGIT);
        AutoNumeric.set('#PREMIUMNPAYNUMBE', row.NPAYNUMBE);

    };
    this.PREMIUMTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/PREMIUMTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                PREMIUMSCERTYPE3: row.SCERTYPE,
                PREMIUMNBRANCH4: row.NBRANCH,
                PREMIUMNPRODUCT5: row.NPRODUCT,
                PREMIUMNPOLICY6: row.NPOLICY,
                PREMIUMNCERTIF7: row.NCERTIF
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
    this.CLAIMTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'NCAUSECOD',
                title: 'Causa del Siniestro',
                formatter: 'HT5ConsultadePolizaSupport.CLAIMNCAUSECOD_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'SCLAIMTYP',
                title: 'Tipo de Pérdida',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'SCLAIMTYPDesc',
                title: 'Tipo de Pérdida',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SCLIENT2',
                title: 'Cliente Afectado',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'SCLIENT2Desc',
                title: 'Cliente Afectado',
                sortable: true,
                halign: 'center'
            }, {
                field: 'DDECLADAT',
                title: 'Fecha de Declaración',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'DOCCURDAT',
                title: 'Fecha de Ocurrencia',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'DLIMIT_PAY',
                title: 'Fecha Límite de Liquidación',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'NLOC_RESERV',
                title: 'Reserva Actual',
                formatter: 'HT5ConsultadePolizaSupport.CLAIMNLOC_RESERV_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NLOC_OUT_AM',
                title: 'Reserva Pendiente',
                formatter: 'HT5ConsultadePolizaSupport.CLAIMNLOC_OUT_AM_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NLOC_PAY_AM',
                title: 'Monto Pagado',
                formatter: 'HT5ConsultadePolizaSupport.CLAIMNLOC_PAY_AM_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NLOC_REC_AM',
                title: 'Monto Recuperado',
                formatter: 'HT5ConsultadePolizaSupport.CLAIMNLOC_REC_AM_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NLOC_COS_RE',
                title: 'Total de Gastos de Recuperación',
                formatter: 'HT5ConsultadePolizaSupport.CLAIMNLOC_COS_RE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NTAX_AMO',
                title: 'Total de Impuesto',
                formatter: 'HT5ConsultadePolizaSupport.CLAIMNTAX_AMO_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'SSTACLAIM',
                title: 'Estado del Siniestro',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'SSTACLAIMDesc',
                title: 'Estado del Siniestro',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NNULLCODE',
                title: 'Causa de anulación',
                formatter: 'HT5ConsultadePolizaSupport.CLAIMNNULLCODE_FormatterMaskData',
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
                field: 'NUNACCODE',
                title: 'Causa de Rechazo de Siniestro',
                formatter: 'HT5ConsultadePolizaSupport.CLAIMNUNACCODE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NUNACCODEDesc',
                title: 'Causa de Rechazo de Siniestro',
                sortable: true,
                halign: 'center'
            }]
        });

        HT5ConsultadePolizaSupport.$el = table;
        HT5ConsultadePolizaSupport.CLAIMTblRequest();
      };

    this.CLAIMRowToInput = function (row) {
        HT5ConsultadePolizaSupport.currentRow = row;
        AutoNumeric.set('#CLAIMNCAUSECOD', row.NCAUSECOD);
        $('#CLAIMSCLAIMTYP').val(row.SCLAIMTYP);
        $('#CLAIMSCLAIMTYPDesc').val(row.SCLAIMTYPDesc);
        $('#CLAIMSCLIENT2').val(row.SCLIENT2);
        $('#CLAIMSCLIENT2Desc').val(row.SCLIENT2Desc);
        $('#CLAIMDDECLADAT').val(generalSupport.ToJavaScriptDateCustom(row.DDECLADAT, generalSupport.DateFormat()));
        $('#CLAIMDOCCURDAT').val(generalSupport.ToJavaScriptDateCustom(row.DOCCURDAT, generalSupport.DateFormat()));
        $('#CLAIMDLIMIT_PAY').val(generalSupport.ToJavaScriptDateCustom(row.DLIMIT_PAY, generalSupport.DateFormat()));
        AutoNumeric.set('#CLAIMNLOC_RESERV', row.NLOC_RESERV);
        AutoNumeric.set('#CLAIMNLOC_OUT_AM', row.NLOC_OUT_AM);
        AutoNumeric.set('#CLAIMNLOC_PAY_AM', row.NLOC_PAY_AM);
        AutoNumeric.set('#CLAIMNLOC_REC_AM', row.NLOC_REC_AM);
        AutoNumeric.set('#CLAIMNLOC_COS_RE', row.NLOC_COS_RE);
        AutoNumeric.set('#CLAIMNTAX_AMO', row.NTAX_AMO);
        $('#CLAIMSSTACLAIM').val(row.SSTACLAIM);
        $('#CLAIMSSTACLAIMDesc').val(row.SSTACLAIMDesc);
        AutoNumeric.set('#CLAIMNNULLCODE', row.NNULLCODE);
        $('#CLAIMNNULLCODEDesc').val(row.NNULLCODEDesc);
        AutoNumeric.set('#CLAIMNUNACCODE', row.NUNACCODE);
        $('#CLAIMNUNACCODEDesc').val(row.NUNACCODEDesc);

    };
    this.CLAIMTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/CLAIMTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                CLAIMSCERTYPE1: row.SCERTYPE,
                CLAIMNBRANCH2: row.NBRANCH,
                CLAIMNPRODUCT3: row.NPRODUCT,
                CLAIMNPOLICY4: row.NPOLICY,
                CLAIMNCERTIF5: row.NCERTIF
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
    this.CURR_ACCTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'NTYP_ACCO',
                title: 'Cuenta Corriente',
                formatter: 'HT5ConsultadePolizaSupport.CURR_ACCNTYP_ACCO_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NTYP_ACCODesc',
                title: 'Cuenta Corriente',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NINSUR_AREA',
                title: 'Área de Seguros',
                formatter: 'HT5ConsultadePolizaSupport.CURR_ACCNINSUR_AREA_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NINSUR_AREADesc',
                title: 'Área de Seguros',
                sortable: true,
                halign: 'center'
            }, {
                field: 'STYPE_ACC',
                title: 'Tipo de Negocio',
                sortable: true,
                halign: 'center'
            }, {
                field: 'DEFFECDATE',
                title: 'Fecha',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'NCURRENCY',
                title: 'Moneda',
                formatter: 'HT5ConsultadePolizaSupport.CURR_ACCNCURRENCY_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NDEBIT',
                title: 'Débito',
                formatter: 'HT5ConsultadePolizaSupport.CURR_ACCNDEBIT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NCREDIT',
                title: 'Crédito',
                formatter: 'HT5ConsultadePolizaSupport.CURR_ACCNCREDIT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NBALANCE',
                title: 'Balance',
                formatter: 'HT5ConsultadePolizaSupport.CURR_ACCNBALANCE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NLED_COMPAN',
                title: 'Compañía Contable',
                formatter: 'HT5ConsultadePolizaSupport.CURR_ACCNLED_COMPAN_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'SACCOUNT',
                title: 'Cuenta Contable',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SAUX_ACCOUN',
                title: 'Auxiliar',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SSTATREGT',
                title: 'Estado del Registro',
                sortable: true,
                halign: 'center'
            }]
        });

        HT5ConsultadePolizaSupport.$el = table;
        HT5ConsultadePolizaSupport.CURR_ACCTblRequest();
      };

    this.CURR_ACCRowToInput = function (row) {
        HT5ConsultadePolizaSupport.currentRow = row;
        AutoNumeric.set('#CURR_ACCNTYP_ACCO', row.NTYP_ACCO);
        $('#CURR_ACCNTYP_ACCODesc').val(row.NTYP_ACCODesc);
        AutoNumeric.set('#CURR_ACCNINSUR_AREA', row.NINSUR_AREA);
        $('#CURR_ACCNINSUR_AREADesc').val(row.NINSUR_AREADesc);
        $('#CURR_ACCSTYPE_ACC').val(row.STYPE_ACC);
        $('#CURR_ACCDEFFECDATE').val(generalSupport.ToJavaScriptDateCustom(row.DEFFECDATE, generalSupport.DateFormat()));
        AutoNumeric.set('#CURR_ACCNCURRENCY', row.NCURRENCY);
        AutoNumeric.set('#CURR_ACCNDEBIT', row.NDEBIT);
        AutoNumeric.set('#CURR_ACCNCREDIT', row.NCREDIT);
        AutoNumeric.set('#CURR_ACCNBALANCE', row.NBALANCE);
        AutoNumeric.set('#CURR_ACCNLED_COMPAN', row.NLED_COMPAN);
        $('#CURR_ACCSACCOUNT').val(row.SACCOUNT);
        $('#CURR_ACCSAUX_ACCOUN').val(row.SAUX_ACCOUN);
        $('#CURR_ACCSSTATREGT').val(row.SSTATREGT);

    };
    this.CURR_ACCTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/HT5ConsultadePolizaActions.aspx/CURR_ACCTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                CURRACCSCERTYPE2: row.SCERTYPE,
                CURRACCNBRANCH3: row.NBRANCH,
                CURRACCNPRODUCT4: row.NPRODUCT,
                CURRACCNPOLICY5: row.NPOLICY,
                CURRACCNCERTIF6: row.NCERTIF
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

        var detailShow = HT5ConsultadePolizaSupport.DIR_DEBIT_ShowValidation(row);
        if (detailShow)
        html.push('<table id="DIR_DEBITTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Pago Automatico Asociado Polizas</caption></table>');
        html.push('<table id="ROLESTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Roles</caption></table>');
        html.push('<table id="CURREN_POLTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Monedas</caption></table>');
        html.push('<table id="SUM_INSURTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Capitales Asegurados</caption></table>');
        html.push('<table id="COVERTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Coberturas</caption></table>');
        html.push('<table id="REINSURANTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Reaseguro</caption></table>');
        html.push('<table id="REINSURAN2Tbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Reaseguro-Facultativos</caption></table>');
        html.push('<table id="DISC_XPREMTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Descuentos/recargos</caption></table>');
        html.push('<table id="AUTOTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Auto</caption></table>');
        html.push('<table id="FIRETbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Incendio</caption></table>');
        html.push('<table id="HOMEOWNERTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Hogar</caption></table>');
        html.push('<table id="LIFETbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Vida</caption></table>');
        html.push('<table id="THEFTTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Robos</caption></table>');
        html.push('<table id="HEALTHTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Ramo Saluds</caption></table>');
        html.push('<table id="FINANCIAL_INSTRUMENTSTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Instrumento Financieros</caption></table>');
        html.push('<table id="PROTECTIONTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Elementos de Protección</caption></table>');
        html.push('<table id="BENEFICIARTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Beneficiarios</caption></table>');
        html.push('<table id="CLAUSETbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Cláusulas</caption></table>');
        html.push('<table id="POLICY_HISTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Historia</caption></table>');
        html.push('<table id="PREMIUMTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Primas</caption></table>');
        html.push('<table id="CLAIMTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Siniestros</caption></table>');
        html.push('<table id="CURR_ACCTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Cuentas Corrientes</caption></table>');

        $detail.html(html.join(""));
        if (detailShow)
        HT5ConsultadePolizaSupport.DIR_DEBITTblSetup($detail.find('#DIR_DEBITTbl-' + index));
        HT5ConsultadePolizaSupport.ROLESTblSetup($detail.find('#ROLESTbl-' + index));
        HT5ConsultadePolizaSupport.CURREN_POLTblSetup($detail.find('#CURREN_POLTbl-' + index));
        HT5ConsultadePolizaSupport.SUM_INSURTblSetup($detail.find('#SUM_INSURTbl-' + index));
        HT5ConsultadePolizaSupport.COVERTblSetup($detail.find('#COVERTbl-' + index));
        HT5ConsultadePolizaSupport.REINSURANTblSetup($detail.find('#REINSURANTbl-' + index));
        HT5ConsultadePolizaSupport.REINSURAN2TblSetup($detail.find('#REINSURAN2Tbl-' + index));
        HT5ConsultadePolizaSupport.DISC_XPREMTblSetup($detail.find('#DISC_XPREMTbl-' + index));
        HT5ConsultadePolizaSupport.AUTOTblSetup($detail.find('#AUTOTbl-' + index));
        HT5ConsultadePolizaSupport.FIRETblSetup($detail.find('#FIRETbl-' + index));
        HT5ConsultadePolizaSupport.HOMEOWNERTblSetup($detail.find('#HOMEOWNERTbl-' + index));
        HT5ConsultadePolizaSupport.LIFETblSetup($detail.find('#LIFETbl-' + index));
        HT5ConsultadePolizaSupport.THEFTTblSetup($detail.find('#THEFTTbl-' + index));
        HT5ConsultadePolizaSupport.HEALTHTblSetup($detail.find('#HEALTHTbl-' + index));
        HT5ConsultadePolizaSupport.FINANCIAL_INSTRUMENTSTblSetup($detail.find('#FINANCIAL_INSTRUMENTSTbl-' + index));
        HT5ConsultadePolizaSupport.PROTECTIONTblSetup($detail.find('#PROTECTIONTbl-' + index));
        HT5ConsultadePolizaSupport.BENEFICIARTblSetup($detail.find('#BENEFICIARTbl-' + index));
        HT5ConsultadePolizaSupport.CLAUSETblSetup($detail.find('#CLAUSETbl-' + index));
        HT5ConsultadePolizaSupport.POLICY_HISTblSetup($detail.find('#POLICY_HISTbl-' + index));
        HT5ConsultadePolizaSupport.PREMIUMTblSetup($detail.find('#PREMIUMTbl-' + index));
        HT5ConsultadePolizaSupport.CLAIMTblSetup($detail.find('#CLAIMTbl-' + index));
        HT5ConsultadePolizaSupport.CURR_ACCTblSetup($detail.find('#CURR_ACCTbl-' + index));

    };


    this.CERTIFICATNBRANCH_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.CERTIFICATNPRODUCT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.CERTIFICATNCERTIF_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: "",
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.CERTIFICATNCAPITAL_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 2,
            minimumValue: -999999999999999999
        });
      };
    this.CERTIFICATNPAYFREQ_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.CERTIFICATNWAIT_CODE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.CERTIFICATNNULLCODE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.CERTIFICATNDIGIT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9,
            decimalPlaces: 0,
            minimumValue: -9
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
    this.ROLESNROLE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.ROLESNSTATUSROL_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.ROLESNTYPERISK_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99,
            decimalPlaces: 0,
            minimumValue: -99
        });
      };
    this.ROLESNRATING_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.CURREN_POLNCURRENCY_FormatterMaskData = function (value, row, index) {          
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
    this.SUM_INSURNSUM_INSUR_FormatterMaskData = function (value, row, index) {          
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
    this.SUM_INSURNSUMINS_REAL_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.COVERNMODULEC_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.COVERNCOVER_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.COVERNROLE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.COVERNCAPITAL_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.COVERNPREMIUM_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.COVERNCURRENCY_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
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
    this.DISC_XPREMNDISC_CODE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.DISC_XPREMNCAUSE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.DISC_XPREMNCURRENCY_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.DISC_XPREMNAMOUNT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.AUTONVEHTYPE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.AUTONCAPITAL_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.AUTONVEH_VALOR_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.AUTONYEAR_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.AUTONAUTOZONE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.AUTONUSE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.FIRENCONSTCAT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.FIRENACTIVITYCAT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.FIRENFLOOR_QUAN_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.FIRENROOFTYPE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.FIRENSEISMICZONE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.FIRENBUILDTYPE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.FIRENSPCOMBTYPE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.FIRENHURRICAN_ZONE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.FIRENSIDECLOSETYPE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.HOMEOWNERNOWNERSHIP_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.HOMEOWNERNDWELLINGTYPE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.HOMEOWNERNSEISMICZONE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.HOMEOWNERNYEAR_BUILT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999,
            decimalPlaces: 0,
            minimumValue: -9999
        });
      };
    this.HOMEOWNERNPRICE_PURCH_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.HOMEOWNERNCURRENCY_PURCH_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.HOMEOWNERNLANDSUPER_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999999,
            decimalPlaces: 2,
            minimumValue: -99999999
        });
      };
    this.HOMEOWNERNHOMESUPER_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999999,
            decimalPlaces: 2,
            minimumValue: -99999999
        });
      };
    this.HOMEOWNERNROOFTYPE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.HOMEOWNERNROOFYEAR_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999,
            decimalPlaces: 0,
            minimumValue: -9999
        });
      };
    this.HOMEOWNERNFLOODZONE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99,
            decimalPlaces: 0,
            minimumValue: -99
        });
      };
    this.HOMEOWNERNFOUNDTYPE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.HOMEOWNERNAIRTYPE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.HOMEOWNERNSTORIES_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.HOMEOWNERNHALFBATH_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.HOMEOWNERNFULLBATH_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.HOMEOWNERNBEDROOMS_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.HOMEOWNERNFIREPLACE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.HOMEOWNERNGARAGE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.HOMEOWNERNDIST_FIRE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999,
            decimalPlaces: 2,
            minimumValue: -999999
        });
      };
    this.HOMEOWNERNSWIMPOOL_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.HOMEOWNERNFENCEHEIGHT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.HOMEOWNERNEXTERCONSTR_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.LIFENPAY_TIME_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.LIFENTYPDURINS_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.LIFENINSUR_TIME_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.LIFENAGE_REINSU_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.LIFENAGE_LIMIT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.LIFENAGE_FormatterMaskData = function (value, row, index) {          
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
    this.HEALTHNTARIFF_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.HEALTHNNULLCODE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.HEALTHNBRANCH_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9,
            decimalPlaces: 0,
            minimumValue: -9
        });
      };
    this.HEALTHNPRODUCT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9,
            decimalPlaces: 0,
            minimumValue: -9
        });
      };
    this.HEALTHNPOLICY_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9,
            decimalPlaces: 0,
            minimumValue: -9
        });
      };
    this.HEALTHNCERTIF_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9,
            decimalPlaces: 0,
            minimumValue: -9
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
    this.PROTECTIONNUSERCODE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.BENEFICIARNMODULEC_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.BENEFICIARNCOVER_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.BENEFICIARNRELATION_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.BENEFICIARNPARTICIP_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 2,
            minimumValue: -99999
        });
      };
    this.CLAUSENCLAUSE_FormatterMaskData = function (value, row, index) {          
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
    this.POLICY_HISNRECEIPT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.PREMIUMNRECEIPT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: "",
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
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
    this.PREMIUMNSTATUS_PRE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.PREMIUMNWAY_PAY_FormatterMaskData = function (value, row, index) {          
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
    this.PREMIUMNINTERMED_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.PREMIUMNPARTICIP_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 2,
            minimumValue: -99999
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
    this.PREMIUMNCOMAMOU_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
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
    this.PREMIUMNBRANCH_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9,
            decimalPlaces: 0,
            minimumValue: -9
        });
      };
    this.PREMIUMNPRODUCT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9,
            decimalPlaces: 0,
            minimumValue: -9
        });
      };
    this.PREMIUMNDIGIT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9,
            decimalPlaces: 0,
            minimumValue: -9
        });
      };
    this.PREMIUMNPAYNUMBE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9,
            decimalPlaces: 0,
            minimumValue: -9
        });
      };
    this.FINANC_PRENCONTRAT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.FINANC_PRENCURRENCY_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.FINANC_PRENPREMIUM_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
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
    this.PREMIUM_CENCERTIF_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.PREMIUM_CENID_BILL_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.PREMIUM_CENCODE_DETAI_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
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
    this.CLAIMNCAUSECOD_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.CLAIMNLOC_RESERV_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.CLAIMNLOC_OUT_AM_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.CLAIMNLOC_PAY_AM_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.CLAIMNLOC_REC_AM_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.CLAIMNLOC_COS_RE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.CLAIMNTAX_AMO_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.CLAIMNNULLCODE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.CLAIMNUNACCODE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.CURR_ACCNTYP_ACCO_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.CURR_ACCNINSUR_AREA_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.CURR_ACCNCURRENCY_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.CURR_ACCNDEBIT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.CURR_ACCNCREDIT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.CURR_ACCNBALANCE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.CURR_ACCNLED_COMPAN_FormatterMaskData = function (value, row, index) {          
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
        masterSupport.setPageTitle('HT5Consulta de póliza');
        

    HT5ConsultadePolizaSupport.ControlBehaviour();
    HT5ConsultadePolizaSupport.ControlActions();
    HT5ConsultadePolizaSupport.ValidateSetup();

    AutoNumeric.set('#PolicyID', generalSupport.URLNumericValue('PolicyID'));
    $('#RecordEffectiveDate').val(generalSupport.URLDateValue('RecordEffectiveDate'));

    $("#ItemsTblPlaceHolder").replaceWith('<table id="ItemsTbl"><caption >Certificados</caption></table>');
    HT5ConsultadePolizaSupport.ItemsTblSetup($('#ItemsTbl'));

    $('#RecordEffectiveDate').val(moment().format(generalSupport.DateFormat()));
        HT5ConsultadePolizaSupport.ItemsTblRequest();



});

