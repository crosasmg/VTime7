var H5NNInformacionGeneralClienteIndicadoSupport = new function () {

    this.currentRow = {};
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#H5NNInformacionGeneralClienteIndicadoFormId').val(),
            Client: ($('#Client').data('code') !== undefined) ? $('#Client').data('code') : '',
            RecordEffectiveDate: generalSupport.DatePickerValueInputToObject('#RecordEffectiveDate')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        $('#H5NNInformacionGeneralClienteIndicadoFormId').val(data.InstanceFormId);
        $('#Client').data('code', data.Client);
        clientSupport.CompleteClientName('#Client', data.Client);
        $('#RecordEffectiveDate').val(generalSupport.ToJavaScriptDateCustom(data.RecordEffectiveDate, generalSupport.DateFormat()));

        H5NNInformacionGeneralClienteIndicadoSupport.LookUpForOTHERTYPEADDRESSCountryID(source);
        H5NNInformacionGeneralClienteIndicadoSupport.LookUpForCertificatNINTERMED(source);

        if (data.PHYSICALADDRESSES_PHYSICALADDRESSES !== null)
            $('#PHYSICALADDRESSESTbl').bootstrapTable('load', data.PHYSICALADDRESSES_PHYSICALADDRESSES);
        if (data.EMAILS_EMAILS !== null)
            $('#EMAILSTbl').bootstrapTable('load', data.EMAILS_EMAILS);
        if (data.SOCIALNETWORK_SOCIALNETWORK !== null)
            $('#SOCIALNETWORKTbl').bootstrapTable('load', data.SOCIALNETWORK_SOCIALNETWORK);
        if (data.OTHERTYPEADDRESS_OTHERTYPEADDRESS !== null)
            $('#OTHERTYPEADDRESSTbl').bootstrapTable('load', data.OTHERTYPEADDRESS_OTHERTYPEADDRESS);
        if (data.Certificat_Certificat !== null)
            $('#CertificatTbl').bootstrapTable('load', data.Certificat_Certificat);
        if (data.Premium_Premium !== null)
            $('#PremiumTbl').bootstrapTable('load', data.Premium_Premium);
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
        if (data.Relations_Relations !== null)
            $('#RelationsTbl').bootstrapTable('load', data.Relations_Relations);
        if (data.CHEQUES_CHEQUES !== null)
            $('#CHEQUESTbl').bootstrapTable('load', data.CHEQUES_CHEQUES);
        if (data.ROLEINCASE_ROLEINCASE !== null)
            $('#ROLEINCASETbl').bootstrapTable('load', data.ROLEINCASE_ROLEINCASE);
        H5NNInformacionGeneralClienteIndicadoSupport.ItemsTblRequest();
        if (data.Items_Item !== null)
            $('#ItemsTbl').bootstrapTable('load', data.Items_Item);

    };

    this.ControlBehaviour = function () {

       this.Items_ClientsCliename_Item1 = function (row) {
           generalSupport.CallBackOfficePage('BC003_K', '&tctClient='+ row.SCLIENT +'&tctClient_Digit='+ row.SDIGIT +'');

            return true;
        };
       this.Items_ClientsCliename_Item2 = function (row) {
           generalSupport.CallBackOfficePage('', '');

            return true;
        };
       this.Items_ClientsCliename_Item3 = function (row) {
           generalSupport.CallBackOfficePage('SI001', '&cbeTransactio=&tcdEffecdate=&tcnClaim=&cbeOffice=&cbeBranch=&valProduct=&tcnPolicy=&tcnCertificat=&dtcClient=&tcdBirthdat=&tcdOccurrdat=');

            return true;
        };
       this.Certificat_CertificatnPolicy_Item1 = function (row) {
           window.open('/fasi/dli/queries/NNConsultaDePolizaPopup.html?PolicyID='+ row.nPolicy +'&accept='+ 'true' +'','_blank','scrollbars=yes,resizable=yes,toolbar=no,location=no,directories=no,status=yes,menubar=no,copyhistory=no,width=500,height=500,left=0,top=0');

            return true;
        };
       this.Certificat_CertificatnPolicy_Item2 = function (row) {
           generalSupport.CallBackOfficePage('CA001', '&cbeTransactio=&tcnPolicy=');

            return true;
        };
       this.Certificat_CertificatnPolicy_Item3 = function (row) {
           window.location.href = '/fasi/dli/forms/RequestOfPrintingPolicy.aspx?LineOfBusinessToPrint='+ row.nBranch +'&ProductToPrint='+ row.nProduct +'&PolicyToPrint='+ row.nPolicy +'&CertificateToPrint='+ CertificatNCERTIF +'&PProcessDate='+ generalSupport.ToJavaScriptDateCustom(row.dChangdat, generalSupport.DateFormat())  +'';

            return true;
        };
       this.Premium_PremiumnReceipt_Item1 = function (row) {
           window.open('/fasi/dli/queries/NNConsultaDeReciboPopup.html?Recibo='+ row.nReceipt +'&accept='+ 'true' +'','_blank','scrollbars=yes,resizable=yes,toolbar=no,location=no,directories=no,status=yes,menubar=no,copyhistory=no,width=500,height=500,left=0,top=0');

            return true;
        };
       this.Claim_ClaimnClaim_Item1 = function (row) {
           window.open('/fasi/dli/queries/ConsultaSiniestrosPopup.html?Siniestro='+ row.nClaim +'&accept='+ 'true' +'','_blank','scrollbars=no,resizable=yes,toolbar=no,location=no,directories=no,status=yes,menubar=no,copyhistory=no,width=500,height=500,left=0,top=0');

            return true;
        };
       this.ROLEINCASE_ROLEINCASEUnderwritingCaseID_Item1 = function (row) {
           window.location.href = '/fasi/dli/forms/UnderwritingPanel.aspx?uwCaseid='+ row.UnderwritingCaseID +'';

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
                H5NNInformacionGeneralClienteIndicadoSupport.ObjectToInput(data.d.Data, source);
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };



    this.PHYSICALADDRESSES_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5NNInformacionGeneralClienteIndicadoActions.aspx/PHYSICALADDRESSESSelectCommandActionPHYSICALADDRESSES", false,
            JSON.stringify({                 PHYSICALADDRESSESADDRESSID1: row.NADDRESSID }),
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
    this.EMAILS_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5NNInformacionGeneralClienteIndicadoActions.aspx/EMAILSSelectCommandActionEMAILS", false,
            JSON.stringify({                 EMAILSADDRESSID1: row.NADDRESSID }),
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
    this.SOCIALNETWORK_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5NNInformacionGeneralClienteIndicadoActions.aspx/SOCIALNETWORKSelectCommandActionSOCIALNETWORK", false,
            JSON.stringify({                 SOCIALNETWORKADDRESSID1: row.NADDRESSID }),
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
    this.OTHERTYPEADDRESS_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5NNInformacionGeneralClienteIndicadoActions.aspx/OTHERTYPEADDRESSSelectCommandActionOTHERTYPEADDRESS", false,
            JSON.stringify({                 OTHERTYPEADDRESSADDRESSID1: row.NADDRESSID }),
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
    this.Certificat_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5NNInformacionGeneralClienteIndicadoActions.aspx/CertificatSelectCommandActionCERTIFICAT", false,
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
    this.Certificat_Item1_Actions = function (row, $modal) {
    window.open('/fasi/dli/queries/NNConsultaDePolizaPopup.html?PolicyID='+ row.nPolicy +'&accept='+ 'true' +'','_blank','scrollbars=yes,resizable=yes,toolbar=no,location=no,directories=no,status=yes,menubar=no,copyhistory=no,width=500,height=500,left=0,top=0');

    };
    this.Certificat_Item2_Actions = function (row, $modal) {
    generalSupport.CallBackOfficePage('CA001', '&cbeTransactio=&tcnPolicy=');

    };
    this.Certificat_Item3_Actions = function (row, $modal) {
    window.location.href = '/fasi/dli/forms/RequestOfPrintingPolicy.aspx?LineOfBusinessToPrint='+ row.nBranch +'&ProductToPrint='+ row.nProduct +'&PolicyToPrint='+ row.nPolicy +'&CertificateToPrint='+ CertificatNCERTIF +'&PProcessDate='+ generalSupport.ToJavaScriptDateCustom(row.dChangdat, generalSupport.DateFormat())  +'';

    };
    this.Premium_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5NNInformacionGeneralClienteIndicadoActions.aspx/PremiumSelectCommandActionPREMIUM", false,
            JSON.stringify({                 PREMIUMSCLIENT3: row.SCLIENT }),
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
    this.Premium_Item1_Actions = function (row, $modal) {
    window.open('/fasi/dli/queries/NNConsultaDeReciboPopup.html?Recibo='+ row.nReceipt +'&accept='+ 'true' +'','_blank','scrollbars=yes,resizable=yes,toolbar=no,location=no,directories=no,status=yes,menubar=no,copyhistory=no,width=500,height=500,left=0,top=0');

    };
    this.Claim_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5NNInformacionGeneralClienteIndicadoActions.aspx/ClaimSelectCommandActionCLAIM", false,
            JSON.stringify({                 CLAIMSCLIENT1: row.SCLIENT }),
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
    this.Claim_Item1_Actions = function (row, $modal) {
    window.open('/fasi/dli/queries/ConsultaSiniestrosPopup.html?Siniestro='+ row.nClaim +'&accept='+ 'true' +'','_blank','scrollbars=no,resizable=yes,toolbar=no,location=no,directories=no,status=yes,menubar=no,copyhistory=no,width=500,height=500,left=0,top=0');

    };
    this.Address_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5NNInformacionGeneralClienteIndicadoActions.aspx/AddressSelectCommandActionADDRESS", false,
            JSON.stringify({                 ADDRESSSCLIENT1: row.SCLIENT }),
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
    this.PHONES_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5NNInformacionGeneralClienteIndicadoActions.aspx/PHONESSelectCommandActionPHONES", false,
            JSON.stringify({                 PHONESNRECOWNER1: row.NRECOWNER,
                PHONESSKEYADDRESS2: row.SKEYADDRESS,
                PHONESDEFFECDATE3: row.DEFFECDATE }),
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
    this.CLIDOCUMENTS_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5NNInformacionGeneralClienteIndicadoActions.aspx/CLIDOCUMENTSSelectCommandActionCLIDOCUMENTS", false,
            JSON.stringify({                 CLIDOCUMENTSSCLIENT1: row.SCLIENT }),
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
    this.DIR_DEBIT_CLI_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5NNInformacionGeneralClienteIndicadoActions.aspx/DIR_DEBIT_CLISelectCommandActionDIR_DEBIT_CLI", false,
            JSON.stringify({                 DIRDEBITCLISCLIENT1: row.SCLIENT }),
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
    this.Bk_account_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5NNInformacionGeneralClienteIndicadoActions.aspx/Bk_accountSelectCommandActionBK_ACCOUNT", false,
            JSON.stringify({                 BKACCOUNTSCLIENT1: row.SCLIENT }),
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
    this.Cred_card_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5NNInformacionGeneralClienteIndicadoActions.aspx/Cred_cardSelectCommandActionCRED_CARD", false,
            JSON.stringify({                 CREDCARDSCLIENT1: row.SCLIENT }),
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
    this.Curr_acc_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5NNInformacionGeneralClienteIndicadoActions.aspx/Curr_accSelectCommandActionCURR_ACC", false,
            JSON.stringify({                 CURRACCSCLIENT1: row.SCLIENT }),
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
    this.Sport_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5NNInformacionGeneralClienteIndicadoActions.aspx/SportSelectCommandActionSPORT", false,
            JSON.stringify({                 SPORTSCLIENT1: row.SCLIENT }),
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
    this.Hobby_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5NNInformacionGeneralClienteIndicadoActions.aspx/HobbySelectCommandActionHOBBY", false,
            JSON.stringify({                 HOBBYSCLIENT1: row.SCLIENT }),
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
    this.Financ_cli_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5NNInformacionGeneralClienteIndicadoActions.aspx/Financ_cliSelectCommandActionFINANC_CLI", false,
            JSON.stringify({                 FINANCCLISCLIENT1: row.SCLIENT }),
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
    this.Relations_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5NNInformacionGeneralClienteIndicadoActions.aspx/RelationsSelectCommandActionRELATIONS", false,
            JSON.stringify({                 RELATIONSSCLIENT1: row.SCLIENT }),
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
        app.core.SyncWebMethod("/fasi/dli/queries/H5NNInformacionGeneralClienteIndicadoActions.aspx/CHEQUESSelectCommandActionCHEQUES", false,
            JSON.stringify({                 CHEQUESSCLIENT1: row.SCLIENT }),
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
    this.ROLEINCASE_ShowValidation = function (row) {
            var returnData;
            var countData;
        app.core.SyncWebMethod("/fasi/dli/queries/H5NNInformacionGeneralClienteIndicadoActions.aspx/ROLEINCASESelectCommandActionROLEINCASE", false,
            JSON.stringify({                 ROLEINCASECLIENTID1: row.SCLIENT,
                ROLEINCASECLIENTID5: row.SCLIENT }),
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
    this.ROLEINCASE_Item1_Actions = function (row, $modal) {
    window.location.href = '/fasi/dli/forms/UnderwritingPanel.aspx?uwCaseid='+ row.UnderwritingCaseID +'';

    };

    this.ControlActions = function () {

        $('#btnOk').click(function (event) {
            var formInstance = $("#H5NNInformacionGeneralClienteIndicadoMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var btnLoading = Ladda.create(document.querySelector('#btnOk'));
                btnLoading.start();
                H5NNInformacionGeneralClienteIndicadoSupport.ItemsTblRequest();
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();
    
        $("#H5NNInformacionGeneralClienteIndicadoMainForm").validate({
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
    this.LookUpForEMAILSHasBeenConfirmedFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#EMAILSHasBeenConfirmed>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForOTHERTYPEADDRESSCountryIDFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#OTHERTYPEADDRESSCountryID>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForOTHERTYPEADDRESSCountryID = function (defaultValue, source) {
        var ctrol = $('#OTHERTYPEADDRESSCountryID');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.SyncWebMethod("/fasi/dli/queries/H5NNInformacionGeneralClienteIndicadoActions.aspx/LookUpForOTHERTYPEADDRESSCountryID", false,
                JSON.stringify({ id: $('#H5NNInformacionGeneralClienteIndicadoFormId').val() }),
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
    this.LookUpForOTHERTYPEADDRESSAddressValidatedElectronicallyFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#OTHERTYPEADDRESSAddressValidatedElectronically>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForCertificatNINTERMEDFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#CertificatNINTERMED>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForCertificatNINTERMED = function (defaultValue, source) {
        var ctrol = $('#CertificatNINTERMED');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.SyncWebMethod("/fasi/dli/queries/H5NNInformacionGeneralClienteIndicadoActions.aspx/LookUpForCertificatNINTERMED", false,
                JSON.stringify({ id: $('#H5NNInformacionGeneralClienteIndicadoFormId').val() }),
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
    this.LookUpForPremiumsRejectFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#PremiumsReject>option[value='" + value + "']").text();
        }
        return result;
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

    this.ItemsTblSetup = function (table) {
        table.bootstrapTable({
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            detailView: true,
            onExpandRow: H5NNInformacionGeneralClienteIndicadoSupport.ItemsTblExpandRow,
            columns: [{
                field: 'SCLIENT',
                title: 'Código',
                sortable: true,
                halign: 'center'
            }, {
                field: 'sCliename',
                title: 'Nombre completo',
                formatter: 'tableHelperSupport.IsContextMenu',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SLEGALNAME',
                title: 'Nombre legal',
                sortable: true,
                halign: 'center'
            }, {
                field: 'sFirstname',
                title: 'Nombre(s)',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'sLastname',
                title: 'Apellido paterno',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'sLastName2',
                title: 'Apellido materno',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'sSexClien',
                title: 'Género',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'sSexClienDesc',
                title: 'Género',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nCivilSta',
                title: 'Estado civil',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.ClientnCivilSta_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nCivilStaDesc',
                title: 'Estado civil',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nWeight',
                title: 'Peso',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.ClientnWeight_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nHeight',
                title: 'Altura',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.ClientnHeight_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'dBirthdat',
                title: 'F.Nacimiento',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'dDeathdat',
                title: 'F.Defunción',
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
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.ClientnLanguage_FormatterMaskData',
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
                title: 'Preferencia para el correo',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.ClientnMailingPref_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nMailingPrefDesc',
                title: 'Preferencia para el correo',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nTitle',
                title: 'Profesión',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.ClientnTitle_FormatterMaskData',
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
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.ClientnSpeciality_FormatterMaskData',
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
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.ClientnNationality_FormatterMaskData',
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
                title: 'Clasificación',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.ClientnClass_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nClassDesc',
                title: 'Clasificación',
                sortable: true,
                halign: 'center'
            }, {
                field: 'dDependant',
                title: 'F.Trabajador dependiente',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'dIndependant',
                title: 'F.Trabajador independiente',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'dInpdate',
                title: 'F.Ingreso',
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
            }, {
                field: 'ClientID',
                title: 'Código del cliente',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'NADDRESSID',
                title: 'NADDRESSID',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.ClientNADDRESSID_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }]
        });

        table.bootstrapTable('refreshOptions', {
            contextMenu: '#ItemsContextMenu',
            contextMenuButton: '.menu-sCliename',
            beforeContextMenuRow: function (e, row, buttonElement) {
                H5NNInformacionGeneralClienteIndicadoSupport.ItemsRowToInput(row);
                if (buttonElement && $(buttonElement).hasClass('menu-sCliename')) {

                    $('#ItemsTbl').bootstrapTable('showContextMenu', {
                        event: e,
                        contextMenu: '#Items_ClientsClienameContextMenu',
                        buttonElement: buttonElement
                    });
                }
                return false;
            },
            onContextMenuItem: function (row, $el) {
                H5NNInformacionGeneralClienteIndicadoSupport.ItemsRowToInput(row);
                switch ($el.data("item")) {
                    case 'Items_ClientsCliename_Item1':
                        H5NNInformacionGeneralClienteIndicadoSupport.Items_ClientsCliename_Item1(row);
                        break;
                    case 'Items_ClientsCliename_Item2':
                        H5NNInformacionGeneralClienteIndicadoSupport.Items_ClientsCliename_Item2(row);
                        break;
                    case 'Items_ClientsCliename_Item3':
                        H5NNInformacionGeneralClienteIndicadoSupport.Items_ClientsCliename_Item3(row);
                        break;
                }
            }
        });


    };


    this.ItemsRowToInput = function (row) {
        H5NNInformacionGeneralClienteIndicadoSupport.currentRow = row;
        $('#ClientSCLIENT').val(row.SCLIENT);
        $('#ClientsCliename').val(row.sCliename);
        $('#ClientSLEGALNAME').val(row.SLEGALNAME);
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
        $('#ClientClientID').val(row.ClientID);
        AutoNumeric.set('#ClientNADDRESSID', row.NADDRESSID);

    };
    this.ItemsTblRequest = function (params) {
        app.core.AsyncWebMethod("/fasi/dli/queries/H5NNInformacionGeneralClienteIndicadoActions.aspx/ItemsTblDataLoad", false,
            JSON.stringify({
                                CLIENTSCLIENT1: ($('#Client').data('code') !== undefined) ? $('#Client').data('code') : ''
            }),
            function (data) {
                    $('#ItemsTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);


            });
    };
    this.PHYSICALADDRESSESTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'TypeOfPhysicalAddress',
                title: 'Tipo',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.PHYSICALADDRESSESTypeOfPhysicalAddress_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'TypeOfPhysicalAddressDesc',
                title: 'Tipo',
                sortable: true,
                halign: 'center'
            }, {
                field: 'AddressNormalizedI',
                title: 'Dirección (parte I)',
                sortable: true,
                halign: 'center'
            }, {
                field: 'AddressNormalizedII',
                title: 'Dirección (parte II)',
                sortable: true,
                halign: 'center'
            }, {
                field: 'ZipCode',
                title: 'Código postal',
                sortable: true,
                halign: 'center'
            }, {
                field: 'CancellationDate',
                title: 'F.Anulación',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'AddressID',
                title: 'Address ID',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.PHYSICALADDRESSESAddressID_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'EffectiveDate',
                title: 'F.Efecto',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'InternalAddressKey',
                title: 'Internal Address Key',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.PHYSICALADDRESSESInternalAddressKey_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }]
        });

        H5NNInformacionGeneralClienteIndicadoSupport.$el = table;
        H5NNInformacionGeneralClienteIndicadoSupport.PHYSICALADDRESSESTblRequest();
      };

    this.PHYSICALADDRESSESRowToInput = function (row) {
        H5NNInformacionGeneralClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#PHYSICALADDRESSESTypeOfPhysicalAddress', row.TypeOfPhysicalAddress);
        $('#PHYSICALADDRESSESTypeOfPhysicalAddressDesc').val(row.TypeOfPhysicalAddressDesc);
        $('#PHYSICALADDRESSESAddressNormalizedI').val(row.AddressNormalizedI);
        $('#PHYSICALADDRESSESAddressNormalizedII').val(row.AddressNormalizedII);
        $('#PHYSICALADDRESSESZipCode').val(row.ZipCode);
        $('#PHYSICALADDRESSESCancellationDate').val(generalSupport.ToJavaScriptDateCustom(row.CancellationDate, generalSupport.DateFormat()));
        AutoNumeric.set('#PHYSICALADDRESSESAddressID', row.AddressID);
        $('#PHYSICALADDRESSESEffectiveDate').val(generalSupport.ToJavaScriptDateCustom(row.EffectiveDate, generalSupport.DateFormat()));
        AutoNumeric.set('#PHYSICALADDRESSESInternalAddressKey', row.InternalAddressKey);

    };
    this.PHYSICALADDRESSESTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5NNInformacionGeneralClienteIndicadoActions.aspx/PHYSICALADDRESSESTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                PHYSICALADDRESSESADDRESSID1: row.NADDRESSID
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
    this.EMAILSTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'eMail',
                title: 'Correo electrónico',
                sortable: true,
                halign: 'center'
            }, {
                field: 'HasBeenConfirmed',
                title: 'Ha Sido Confirmado',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.LookUpForEMAILSHasBeenConfirmedFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'CancellationDate',
                title: 'F.Anulación',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'EffectiveDate',
                title: 'F.Efecto',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'AddressID',
                title: 'Address ID',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.EMAILSAddressID_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'InternalAddressKey',
                title: 'Internal Address Key',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.EMAILSInternalAddressKey_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }]
        });

        H5NNInformacionGeneralClienteIndicadoSupport.$el = table;
        H5NNInformacionGeneralClienteIndicadoSupport.EMAILSTblRequest();
      };

    this.EMAILSRowToInput = function (row) {
        H5NNInformacionGeneralClienteIndicadoSupport.currentRow = row;
        $('#EMAILSeMail').val(row.eMail);
        $('#EMAILSCancellationDate').val(generalSupport.ToJavaScriptDateCustom(row.CancellationDate, generalSupport.DateFormat()));
        $('#EMAILSEffectiveDate').val(generalSupport.ToJavaScriptDateCustom(row.EffectiveDate, generalSupport.DateFormat()));
        AutoNumeric.set('#EMAILSAddressID', row.AddressID);
        AutoNumeric.set('#EMAILSInternalAddressKey', row.InternalAddressKey);

    };
    this.EMAILSTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5NNInformacionGeneralClienteIndicadoActions.aspx/EMAILSTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                EMAILSADDRESSID1: row.NADDRESSID
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
    this.SOCIALNETWORKTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'NetworkType',
                title: 'Tipo',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.SOCIALNETWORKNetworkType_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NetworkTypeDesc',
                title: 'Tipo',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NetworkValue',
                title: 'Red social',
                sortable: true,
                halign: 'center'
            }, {
                field: 'AddressID',
                title: 'Address ID',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.SOCIALNETWORKAddressID_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'EffectiveDate',
                title: 'F.Efecto',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'CancellationDate',
                title: 'F.Anulación',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'InternalAddressKey',
                title: 'Internal Address Key',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.SOCIALNETWORKInternalAddressKey_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }]
        });

        H5NNInformacionGeneralClienteIndicadoSupport.$el = table;
        H5NNInformacionGeneralClienteIndicadoSupport.SOCIALNETWORKTblRequest();
      };

    this.SOCIALNETWORKRowToInput = function (row) {
        H5NNInformacionGeneralClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#SOCIALNETWORKNetworkType', row.NetworkType);
        $('#SOCIALNETWORKNetworkTypeDesc').val(row.NetworkTypeDesc);
        $('#SOCIALNETWORKNetworkValue').val(row.NetworkValue);
        AutoNumeric.set('#SOCIALNETWORKAddressID', row.AddressID);
        $('#SOCIALNETWORKEffectiveDate').val(generalSupport.ToJavaScriptDateCustom(row.EffectiveDate, generalSupport.DateFormat()));
        $('#SOCIALNETWORKCancellationDate').val(generalSupport.ToJavaScriptDateCustom(row.CancellationDate, generalSupport.DateFormat()));
        AutoNumeric.set('#SOCIALNETWORKInternalAddressKey', row.InternalAddressKey);

    };
    this.SOCIALNETWORKTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5NNInformacionGeneralClienteIndicadoActions.aspx/SOCIALNETWORKTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                SOCIALNETWORKADDRESSID1: row.NADDRESSID
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
    this.OTHERTYPEADDRESSTblSetup = function (table) {
        H5NNInformacionGeneralClienteIndicadoSupport.LookUpForOTHERTYPEADDRESSCountryID('');
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'CountryID',
                title: 'País',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.LookUpForOTHERTYPEADDRESSCountryIDFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'POBox',
                title: 'Buzón de correo',
                sortable: true,
                halign: 'center'
            }, {
                field: 'Url',
                title: 'Dirección Web',
                sortable: true,
                halign: 'center'
            }, {
                field: 'AddressValidatedElectronically',
                title: 'Dirección validada',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.LookUpForOTHERTYPEADDRESSAddressValidatedElectronicallyFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'CancellationDate',
                title: 'F.Anulación',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'EffectiveDate',
                title: 'F.Efecto',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'AddressID',
                title: 'Address ID',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.OTHERTYPEADDRESSAddressID_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'InternalAddressKey',
                title: 'Internal Address Key',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.OTHERTYPEADDRESSInternalAddressKey_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }]
        });

        H5NNInformacionGeneralClienteIndicadoSupport.$el = table;
        H5NNInformacionGeneralClienteIndicadoSupport.OTHERTYPEADDRESSTblRequest();
      };

    this.OTHERTYPEADDRESSRowToInput = function (row) {
        H5NNInformacionGeneralClienteIndicadoSupport.currentRow = row;
        H5NNInformacionGeneralClienteIndicadoSupport.LookUpForOTHERTYPEADDRESSCountryID(row.CountryID, '');
        $('#OTHERTYPEADDRESSPOBox').val(row.POBox);
        $('#OTHERTYPEADDRESSUrl').val(row.Url);
        $('#OTHERTYPEADDRESSCancellationDate').val(generalSupport.ToJavaScriptDateCustom(row.CancellationDate, generalSupport.DateFormat()));
        $('#OTHERTYPEADDRESSEffectiveDate').val(generalSupport.ToJavaScriptDateCustom(row.EffectiveDate, generalSupport.DateFormat()));
        AutoNumeric.set('#OTHERTYPEADDRESSAddressID', row.AddressID);
        AutoNumeric.set('#OTHERTYPEADDRESSInternalAddressKey', row.InternalAddressKey);

    };
    this.OTHERTYPEADDRESSTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5NNInformacionGeneralClienteIndicadoActions.aspx/OTHERTYPEADDRESSTblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                OTHERTYPEADDRESSADDRESSID1: row.NADDRESSID
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
    this.CertificatTblSetup = function (table) {
        H5NNInformacionGeneralClienteIndicadoSupport.LookUpForCertificatNINTERMED('');
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            search: true,

            columns: [{
                field: 'nBranch',
                title: 'Ramo',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.CertificatnBranch_FormatterMaskData',
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
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.CertificatnProduct_FormatterMaskData',
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
                formatter: 'tableHelperSupport.IsContextMenu',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nCapital',
                title: 'Capital asegurado',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.CertificatnCapital_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'dIssuedat',
                title: 'F.Emisión',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center',
                visible: false
            }, {
                field: 'dStartdate',
                title: 'Inicio de vigencia',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'dExpirdat',
                title: 'Vencimiento',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'dNulldate',
                title: 'F.Anulación',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center',
                visible: false
            }, {
                field: 'nNullcode',
                title: 'Causa de anulación',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.CertificatnNullcode_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nNullcodeDesc',
                title: 'Causa de anulación',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'nPayfreq',
                title: 'Frecuencia de pago',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.CertificatnPayfreq_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nPayfreqDesc',
                title: 'Frecuencia de pago',
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
                field: 'nWait_code',
                title: 'Causa de pendiente',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.CertificatnWait_code_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nWait_codeDesc',
                title: 'Causa de pendiente',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'sRenewal',
                title: 'Renovación automática',
                formatter: 'tableHelperSupport.IsCheck',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'dChangdat',
                title: 'F.Último cambio',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center',
                visible: false
            }, {
                field: 'NINTERMED',
                title: 'Productor',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.LookUpForCertificatNINTERMEDFormatter',
                sortable: true,
                halign: 'center'
            }]
        });
        table.bootstrapTable('refreshOptions', {
            contextMenu: '#CertificatContextMenu',
            contextMenuButton: '.menu-nPolicy',
            beforeContextMenuRow: function (e, row, buttonElement) {
                H5NNInformacionGeneralClienteIndicadoSupport.CertificatRowToInput(row);
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
                H5NNInformacionGeneralClienteIndicadoSupport.CertificatRowToInput(row);
                switch ($el.data("item")) {
                    case 'Certificat_Item1':
                        H5NNInformacionGeneralClienteIndicadoSupport.Certificat_Item1_Actions(row, null);
                        break;
                    case 'Certificat_Item2':
                        H5NNInformacionGeneralClienteIndicadoSupport.Certificat_Item2_Actions(row, null);
                        break;
                    case 'Certificat_Item3':
                        H5NNInformacionGeneralClienteIndicadoSupport.Certificat_Item3_Actions(row, null);
                        break;
                    case 'Certificat_CertificatnPolicy_Item1':
                        H5NNInformacionGeneralClienteIndicadoSupport.Certificat_CertificatnPolicy_Item1(row);
                        break;
                    case 'Certificat_CertificatnPolicy_Item2':
                        H5NNInformacionGeneralClienteIndicadoSupport.Certificat_CertificatnPolicy_Item2(row);
                        break;
                    case 'Certificat_CertificatnPolicy_Item3':
                        H5NNInformacionGeneralClienteIndicadoSupport.Certificat_CertificatnPolicy_Item3(row);
                        break;
                }
            }
        });

        H5NNInformacionGeneralClienteIndicadoSupport.$el = table;
        H5NNInformacionGeneralClienteIndicadoSupport.CertificatTblRequest();
      };

    this.CertificatRowToInput = function (row) {
        H5NNInformacionGeneralClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#CertificatnBranch', row.nBranch);
        $('#CertificatnBranchDesc').val(row.nBranchDesc);
        AutoNumeric.set('#CertificatnProduct', row.nProduct);
        $('#CertificatnProductDesc').val(row.nProductDesc);
        $('#CertificatnPolicy').val(row.nPolicy);
        AutoNumeric.set('#CertificatnCapital', row.nCapital);
        $('#CertificatdIssuedat').val(generalSupport.ToJavaScriptDateCustom(row.dIssuedat, generalSupport.DateFormat()));
        $('#CertificatdStartdate').val(generalSupport.ToJavaScriptDateCustom(row.dStartdate, generalSupport.DateFormat()));
        $('#CertificatdExpirdat').val(generalSupport.ToJavaScriptDateCustom(row.dExpirdat, generalSupport.DateFormat()));
        $('#CertificatdNulldate').val(generalSupport.ToJavaScriptDateCustom(row.dNulldate, generalSupport.DateFormat()));
        AutoNumeric.set('#CertificatnNullcode', row.nNullcode);
        $('#CertificatnNullcodeDesc').val(row.nNullcodeDesc);
        AutoNumeric.set('#CertificatnPayfreq', row.nPayfreq);
        $('#CertificatnPayfreqDesc').val(row.nPayfreqDesc);
        $('#CertificatsStatusva').val(row.sStatusva);
        $('#CertificatsStatusvaDesc').val(row.sStatusvaDesc);
        AutoNumeric.set('#CertificatnWait_code', row.nWait_code);
        $('#CertificatnWait_codeDesc').val(row.nWait_codeDesc);
        $('#CertificatsRenewal').prop("checked", row.sRenewal);
        $('#CertificatdChangdat').val(generalSupport.ToJavaScriptDateCustom(row.dChangdat, generalSupport.DateFormat()));
        H5NNInformacionGeneralClienteIndicadoSupport.LookUpForCertificatNINTERMED(row.NINTERMED, '');

    };
    this.CertificatTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5NNInformacionGeneralClienteIndicadoActions.aspx/CertificatTblDataLoad",
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
    this.PremiumTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            search: true,

            columns: [{
                field: 'nCurrency',
                title: 'Moneda',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.PremiumnCurrency_FormatterMaskData',
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
                field: 'nStatus_pre',
                title: 'Estado',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.PremiumnStatus_pre_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nStatus_preDesc',
                title: 'Estado',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nReceipt',
                title: 'Recibo',
                formatter: 'tableHelperSupport.IsContextMenu',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nBranch',
                title: 'Ramo',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.PremiumnBranch_FormatterMaskData',
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
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.PremiumnProduct_FormatterMaskData',
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
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.PremiumnPolicy_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nTratypei',
                title: 'Origen del recibo',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.PremiumnTratypei_FormatterMaskData',
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
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.PremiumnPremium_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NBALANCE',
                title: 'Balance',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.PremiumNBALANCE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'dIssuedat',
                title: 'F. Emisión',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center',
                visible: false
            }, {
                field: 'DEFFECDATE',
                title: 'Inicio de vigencia',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'dExpirdat',
                title: 'Vencimiento',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'nCollector',
                title: 'Encargado de cobro',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.PremiumnCollector_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'sReject',
                title: 'Indicador de rechazo',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.LookUpForPremiumsRejectFormatter',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'dLimitDate',
                title: 'F.Límite de pago',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center',
                visible: false
            }, {
                field: 'nWay_pay',
                title: 'Vía de pago',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.PremiumnWay_pay_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nWay_payDesc',
                title: 'Vía de pago',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NINTERMED',
                title: 'Productor',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.PremiumNINTERMED_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nParticip',
                title: '%Participación',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.PremiumnParticip_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nComamou',
                title: 'Monto de comisión',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.PremiumnComamou_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nType',
                title: 'Tipo de factura',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.LookUpForPremiumnTypeFormatter',
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
                field: 'sManauti',
                title: 'Recibo manual o automático',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.LookUpForPremiumsManautiFormatter',
                sortable: true,
                halign: 'center',
                visible: false
            }]
        });
        table.bootstrapTable('refreshOptions', {
            contextMenu: '#PremiumContextMenu',
            contextMenuButton: '.menu-nReceipt',
            beforeContextMenuRow: function (e, row, buttonElement) {
                H5NNInformacionGeneralClienteIndicadoSupport.PremiumRowToInput(row);
                if (!buttonElement ) {

                    table.bootstrapTable('showContextMenu', {
                        event: e,
                        contextMenu: '#PremiumContextMenu',
                        buttonElement: buttonElement
                    });
                }
                if (buttonElement && $(buttonElement).hasClass('menu-nReceipt')) {

                    $('#PremiumTbl').bootstrapTable('showContextMenu', {
                        event: e,
                        contextMenu: '#Premium_PremiumnReceiptContextMenu',
                        buttonElement: buttonElement
                    });
                }
                return false;
            },
            onContextMenuItem: function (row, $el) {
                H5NNInformacionGeneralClienteIndicadoSupport.PremiumRowToInput(row);
                switch ($el.data("item")) {
                    case 'Premium_Item1':
                        H5NNInformacionGeneralClienteIndicadoSupport.Premium_Item1_Actions(row, null);
                        break;
                    case 'Premium_PremiumnReceipt_Item1':
                        H5NNInformacionGeneralClienteIndicadoSupport.Premium_PremiumnReceipt_Item1(row);
                        break;
                }
            }
        });

        H5NNInformacionGeneralClienteIndicadoSupport.$el = table;
        H5NNInformacionGeneralClienteIndicadoSupport.PremiumTblRequest();
      };

    this.PremiumRowToInput = function (row) {
        H5NNInformacionGeneralClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#PremiumnCurrency', row.nCurrency);
        $('#PremiumnCurrencyDesc').val(row.nCurrencyDesc);
        AutoNumeric.set('#PremiumnStatus_pre', row.nStatus_pre);
        $('#PremiumnStatus_preDesc').val(row.nStatus_preDesc);
        $('#PremiumnReceipt').val(row.nReceipt);
        AutoNumeric.set('#PremiumnBranch', row.nBranch);
        $('#PremiumnBranchDesc').val(row.nBranchDesc);
        AutoNumeric.set('#PremiumnProduct', row.nProduct);
        $('#PremiumnProductDesc').val(row.nProductDesc);
        AutoNumeric.set('#PremiumnPolicy', row.nPolicy);
        AutoNumeric.set('#PremiumnTratypei', row.nTratypei);
        $('#PremiumnTratypeiDesc').val(row.nTratypeiDesc);
        AutoNumeric.set('#PremiumnPremium', row.nPremium);
        AutoNumeric.set('#PremiumNBALANCE', row.NBALANCE);
        $('#PremiumdIssuedat').val(generalSupport.ToJavaScriptDateCustom(row.dIssuedat, generalSupport.DateFormat()));
        $('#PremiumDEFFECDATE').val(generalSupport.ToJavaScriptDateCustom(row.DEFFECDATE, generalSupport.DateFormat()));
        $('#PremiumdExpirdat').val(generalSupport.ToJavaScriptDateCustom(row.dExpirdat, generalSupport.DateFormat()));
        AutoNumeric.set('#PremiumnCollector', row.nCollector);
        $('#PremiumdLimitDate').val(generalSupport.ToJavaScriptDateCustom(row.dLimitDate, generalSupport.DateFormat()));
        AutoNumeric.set('#PremiumnWay_pay', row.nWay_pay);
        $('#PremiumnWay_payDesc').val(row.nWay_payDesc);
        AutoNumeric.set('#PremiumNINTERMED', row.NINTERMED);
        AutoNumeric.set('#PremiumnParticip', row.nParticip);
        AutoNumeric.set('#PremiumnComamou', row.nComamou);
        $('#PremiumsStatusva').val(row.sStatusva);
        $('#PremiumsStatusvaDesc').val(row.sStatusvaDesc);

    };
    this.PremiumTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5NNInformacionGeneralClienteIndicadoActions.aspx/PremiumTblDataLoad",
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
    this.ClaimTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            search: true,

            columns: [{
                field: 'nBranch',
                title: 'Ramo',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.ClaimnBranch_FormatterMaskData',
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
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.ClaimnProduct_FormatterMaskData',
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
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.ClaimnPolicy_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NCERTIF',
                title: 'Certificado',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.ClaimNCERTIF_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'dDecladat',
                title: 'F.Declaración',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'nClaim',
                title: 'Siniestro',
                formatter: 'tableHelperSupport.IsContextMenu',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'dOccurdat',
                title: 'F.Ocurrencia',
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
                title: 'Causa del siniestro',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.ClaimnCausecod_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nCausecodDesc',
                title: 'Causa del siniestro',
                sortable: true,
                halign: 'center'
            }, {
                field: 'sStaclaim',
                title: 'Estado',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'sStaclaimDesc',
                title: 'Estado',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nUnaccode',
                title: 'Causa de rechazo',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.ClaimnUnaccode_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nUnaccodeDesc',
                title: 'Causa de rechazo',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'dPrescdat',
                title: 'F.Prescripción',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center',
                visible: false
            }, {
                field: 'nLoc_reserv',
                title: 'Reserva actual',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.ClaimnLoc_reserv_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nLoc_pay_am',
                title: 'Monto pagado',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.ClaimnLoc_pay_am_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nLoc_out_am',
                title: 'Reserva pendiente',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.ClaimnLoc_out_am_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nLoc_rec_am',
                title: 'Monto recuperado',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.ClaimnLoc_rec_am_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nLoc_cos_re',
                title: 'Gastos de recuperación',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.ClaimnLoc_cos_re_FormatterMaskData',
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
        table.bootstrapTable('refreshOptions', {
            contextMenu: '#ClaimContextMenu',
            contextMenuButton: '.menu-nClaim',
            beforeContextMenuRow: function (e, row, buttonElement) {
                H5NNInformacionGeneralClienteIndicadoSupport.ClaimRowToInput(row);
                if (!buttonElement ) {

                    table.bootstrapTable('showContextMenu', {
                        event: e,
                        contextMenu: '#ClaimContextMenu',
                        buttonElement: buttonElement
                    });
                }
                if (buttonElement && $(buttonElement).hasClass('menu-nClaim')) {

                    $('#ClaimTbl').bootstrapTable('showContextMenu', {
                        event: e,
                        contextMenu: '#Claim_ClaimnClaimContextMenu',
                        buttonElement: buttonElement
                    });
                }
                return false;
            },
            onContextMenuItem: function (row, $el) {
                H5NNInformacionGeneralClienteIndicadoSupport.ClaimRowToInput(row);
                switch ($el.data("item")) {
                    case 'Claim_Item1':
                        H5NNInformacionGeneralClienteIndicadoSupport.Claim_Item1_Actions(row, null);
                        break;
                    case 'Claim_ClaimnClaim_Item1':
                        H5NNInformacionGeneralClienteIndicadoSupport.Claim_ClaimnClaim_Item1(row);
                        break;
                }
            }
        });

        H5NNInformacionGeneralClienteIndicadoSupport.$el = table;
        H5NNInformacionGeneralClienteIndicadoSupport.ClaimTblRequest();
      };

    this.ClaimRowToInput = function (row) {
        H5NNInformacionGeneralClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#ClaimnBranch', row.nBranch);
        $('#ClaimnBranchDesc').val(row.nBranchDesc);
        AutoNumeric.set('#ClaimnProduct', row.nProduct);
        $('#ClaimnProductDesc').val(row.nProductDesc);
        AutoNumeric.set('#ClaimnPolicy', row.nPolicy);
        AutoNumeric.set('#ClaimNCERTIF', row.NCERTIF);
        $('#ClaimdDecladat').val(generalSupport.ToJavaScriptDateCustom(row.dDecladat, generalSupport.DateFormat()));
        $('#ClaimnClaim').val(row.nClaim);
        $('#ClaimdOccurdat').val(generalSupport.ToJavaScriptDateCustom(row.dOccurdat, generalSupport.DateFormat()));
        $('#ClaimsClaimtyp').val(row.sClaimtyp);
        $('#ClaimsClaimtypDesc').val(row.sClaimtypDesc);
        AutoNumeric.set('#ClaimnCausecod', row.nCausecod);
        $('#ClaimnCausecodDesc').val(row.nCausecodDesc);
        $('#ClaimsStaclaim').val(row.sStaclaim);
        $('#ClaimsStaclaimDesc').val(row.sStaclaimDesc);
        AutoNumeric.set('#ClaimnUnaccode', row.nUnaccode);
        $('#ClaimnUnaccodeDesc').val(row.nUnaccodeDesc);
        $('#ClaimdPrescdat').val(generalSupport.ToJavaScriptDateCustom(row.dPrescdat, generalSupport.DateFormat()));
        AutoNumeric.set('#ClaimnLoc_reserv', row.nLoc_reserv);
        AutoNumeric.set('#ClaimnLoc_pay_am', row.nLoc_pay_am);
        AutoNumeric.set('#ClaimnLoc_out_am', row.nLoc_out_am);
        AutoNumeric.set('#ClaimnLoc_rec_am', row.nLoc_rec_am);
        AutoNumeric.set('#ClaimnLoc_cos_re', row.nLoc_cos_re);
        $('#ClaimdCompdate').val(generalSupport.ToJavaScriptDateCustom(row.dCompdate, generalSupport.DateFormat()));

    };
    this.ClaimTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5NNInformacionGeneralClienteIndicadoActions.aspx/ClaimTblDataLoad",
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

            columns: [{
                field: 'NPHONE_TYPE',
                title: 'Tipo de Teléfono',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.PHONESNPHONE_TYPE_FormatterMaskData',
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
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.PHONESNAREA_CODE_FormatterMaskData',
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
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.PHONESNEXTENS1_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NEXTENS2',
                title: 'Número de extensión telefónica',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.PHONESNEXTENS2_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NBESTTIMETOCALL',
                title: 'Mejor Hora para Llamar',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.PHONESNBESTTIMETOCALL_FormatterMaskData',
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

        H5NNInformacionGeneralClienteIndicadoSupport.$el = table;
        H5NNInformacionGeneralClienteIndicadoSupport.PHONESTblRequest();
      };

    this.PHONESRowToInput = function (row) {
        H5NNInformacionGeneralClienteIndicadoSupport.currentRow = row;
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
            url: "/fasi/dli/queries/H5NNInformacionGeneralClienteIndicadoActions.aspx/PHONESTblDataLoad",
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

        var detailShow = H5NNInformacionGeneralClienteIndicadoSupport.PHONES_ShowValidation(row);
        if (detailShow)
        html.push('<table id="PHONESTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Teléfonos</caption></table>');

        $detail.html(html.join(""));
        if (detailShow)
        H5NNInformacionGeneralClienteIndicadoSupport.PHONESTblSetup($detail.find('#PHONESTbl-' + index));

    };
    this.AddressTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            detailView: true,
            onExpandRow: H5NNInformacionGeneralClienteIndicadoSupport.AddressTblExpandRow,

            columns: [{
                field: 'SRECTYPE',
                title: 'Tipo de dirección',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nCountry',
                title: 'País',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.AddressnCountry_FormatterMaskData',
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
                field: 'nProvince',
                title: 'Región',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.AddressnProvince_FormatterMaskData',
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
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.AddressnLocal_FormatterMaskData',
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
                field: 'nMunicipality',
                title: 'Municipalidad',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.AddressnMunicipality_FormatterMaskData',
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
                sortable: true,
                halign: 'center',
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
                sortable: true,
                halign: 'center',
                visible: false
            }]
        });

        H5NNInformacionGeneralClienteIndicadoSupport.$el = table;
        H5NNInformacionGeneralClienteIndicadoSupport.AddressTblRequest();
      };

    this.AddressRowToInput = function (row) {
        H5NNInformacionGeneralClienteIndicadoSupport.currentRow = row;
        $('#AddressSRECTYPE').val(row.SRECTYPE);
        AutoNumeric.set('#AddressnCountry', row.nCountry);
        $('#AddressnCountryDesc').val(row.nCountryDesc);
        AutoNumeric.set('#AddressnProvince', row.nProvince);
        $('#AddressnProvinceDesc').val(row.nProvinceDesc);
        AutoNumeric.set('#AddressnLocal', row.nLocal);
        $('#AddressnLocalDesc').val(row.nLocalDesc);
        AutoNumeric.set('#AddressnMunicipality', row.nMunicipality);
        $('#AddressnMunicipalityDesc').val(row.nMunicipalityDesc);
        $('#AddresssDescAdd').val(row.sDescAdd);
        $('#AddresssE_mail').val(row.sE_mail);
        $('#AddresssStreet').val(row.sStreet);
        $('#AddresssStreet1').val(row.sStreet1);
        $('#AddresssBuild').val(row.sBuild);
        $('#AddresssDepartment').val(row.sDepartment);
        $('#AddresssPobox').val(row.sPobox);
        $('#AddressNRECOWNER').val(row.NRECOWNER);
        $('#AddressSKEYADDRESS').val(row.SKEYADDRESS);
        $('#AddressDEFFECDATE').val(row.DEFFECDATE);

    };
    this.AddressTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5NNInformacionGeneralClienteIndicadoActions.aspx/AddressTblDataLoad",
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

            columns: [{
                field: 'NTYPCLIENTDOC',
                title: 'Tipo',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.CLIDOCUMENTSNTYPCLIENTDOC_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NTYPCLIENTDOCDesc',
                title: 'Tipo',
                sortable: true,
                halign: 'center'
            }, {
                field: 'SCLINUMDOCU',
                title: 'Número/Código',
                sortable: true,
                halign: 'center'
            }, {
                field: 'DISSUEDAT',
                title: 'F.Emisión',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'DEXPIRDAT',
                title: 'F.Vencimiento',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }]
        });

        H5NNInformacionGeneralClienteIndicadoSupport.$el = table;
        H5NNInformacionGeneralClienteIndicadoSupport.CLIDOCUMENTSTblRequest();
      };

    this.CLIDOCUMENTSRowToInput = function (row) {
        H5NNInformacionGeneralClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#CLIDOCUMENTSNTYPCLIENTDOC', row.NTYPCLIENTDOC);
        $('#CLIDOCUMENTSNTYPCLIENTDOCDesc').val(row.NTYPCLIENTDOCDesc);
        $('#CLIDOCUMENTSSCLINUMDOCU').val(row.SCLINUMDOCU);
        $('#CLIDOCUMENTSDISSUEDAT').val(generalSupport.ToJavaScriptDateCustom(row.DISSUEDAT, generalSupport.DateFormat()));
        $('#CLIDOCUMENTSDEXPIRDAT').val(generalSupport.ToJavaScriptDateCustom(row.DEXPIRDAT, generalSupport.DateFormat()));

    };
    this.CLIDOCUMENTSTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5NNInformacionGeneralClienteIndicadoActions.aspx/CLIDOCUMENTSTblDataLoad",
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

            columns: [{
                field: 'STYP_DIRDEB',
                title: 'Domiciliación',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NBANKEXT',
                title: 'Banco',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.DIR_DEBIT_CLINBANKEXT_FormatterMaskData',
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
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.DIR_DEBIT_CLINBILL_DAY_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'DEFFECDATE',
                title: 'F.Efecto',
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

        H5NNInformacionGeneralClienteIndicadoSupport.$el = table;
        H5NNInformacionGeneralClienteIndicadoSupport.DIR_DEBIT_CLITblRequest();
      };

    this.DIR_DEBIT_CLIRowToInput = function (row) {
        H5NNInformacionGeneralClienteIndicadoSupport.currentRow = row;
        $('#DIR_DEBIT_CLISTYP_DIRDEB').val(row.STYP_DIRDEB);
        AutoNumeric.set('#DIR_DEBIT_CLINBANKEXT', row.NBANKEXT);
        $('#DIR_DEBIT_CLINBANKEXTDesc').val(row.NBANKEXTDesc);
        $('#DIR_DEBIT_CLISACCOUNT').val(row.SACCOUNT);
        AutoNumeric.set('#DIR_DEBIT_CLINBILL_DAY', row.NBILL_DAY);
        $('#DIR_DEBIT_CLIDEFFECDATE').val(generalSupport.ToJavaScriptDateCustom(row.DEFFECDATE, generalSupport.DateFormat()));
        $('#DIR_DEBIT_CLIDNULLDATE').val(generalSupport.ToJavaScriptDateCustom(row.DNULLDATE, generalSupport.DateFormat()));

    };
    this.DIR_DEBIT_CLITblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5NNInformacionGeneralClienteIndicadoActions.aspx/DIR_DEBIT_CLITblDataLoad",
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

            columns: [{
                field: 'nTyp_acc',
                title: 'Tipo',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.Bk_accountnTyp_acc_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nTyp_accDesc',
                title: 'Tipo',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nBankExt',
                title: 'Banco',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.Bk_accountnBankExt_FormatterMaskData',
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
                title: 'Cuenta bancaria',
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

        H5NNInformacionGeneralClienteIndicadoSupport.$el = table;
        H5NNInformacionGeneralClienteIndicadoSupport.Bk_accountTblRequest();
      };

    this.Bk_accountRowToInput = function (row) {
        H5NNInformacionGeneralClienteIndicadoSupport.currentRow = row;
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
            url: "/fasi/dli/queries/H5NNInformacionGeneralClienteIndicadoActions.aspx/Bk_accountTblDataLoad",
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

            columns: [{
                field: 'sCredi_Card',
                title: 'Tarjeta',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nCard_type',
                title: 'Tipo',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.Cred_cardnCard_type_FormatterMaskData',
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
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.Cred_cardnBankExt_FormatterMaskData',
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
                title: 'F.Vencimiento',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }]
        });

        H5NNInformacionGeneralClienteIndicadoSupport.$el = table;
        H5NNInformacionGeneralClienteIndicadoSupport.Cred_cardTblRequest();
      };

    this.Cred_cardRowToInput = function (row) {
        H5NNInformacionGeneralClienteIndicadoSupport.currentRow = row;
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
            url: "/fasi/dli/queries/H5NNInformacionGeneralClienteIndicadoActions.aspx/Cred_cardTblDataLoad",
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

            columns: [{
                field: 'nTyp_acco',
                title: 'Tipo',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.Curr_accnTyp_acco_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nTyp_accoDesc',
                title: 'Tipo',
                sortable: true,
                halign: 'center'
            }, {
                field: 'nDebit',
                title: 'Débito',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.Curr_accnDebit_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nCredit',
                title: 'Crédito',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.Curr_accnCredit_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nBalance',
                title: 'Balance',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.Curr_accnBalance_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nCurrency',
                title: 'Moneda',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.Curr_accnCurrency_FormatterMaskData',
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

        H5NNInformacionGeneralClienteIndicadoSupport.$el = table;
        H5NNInformacionGeneralClienteIndicadoSupport.Curr_accTblRequest();
      };

    this.Curr_accRowToInput = function (row) {
        H5NNInformacionGeneralClienteIndicadoSupport.currentRow = row;
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
            url: "/fasi/dli/queries/H5NNInformacionGeneralClienteIndicadoActions.aspx/Curr_accTblDataLoad",
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

            columns: [{
                field: 'nSport',
                title: 'Deporte',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.SportnSport_FormatterMaskData',
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

        H5NNInformacionGeneralClienteIndicadoSupport.$el = table;
        H5NNInformacionGeneralClienteIndicadoSupport.SportTblRequest();
      };

    this.SportRowToInput = function (row) {
        H5NNInformacionGeneralClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#SportnSport', row.nSport);
        $('#SportnSportDesc').val(row.nSportDesc);

    };
    this.SportTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5NNInformacionGeneralClienteIndicadoActions.aspx/SportTblDataLoad",
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

            columns: [{
                field: 'nHobby',
                title: 'Hobby',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.HobbynHobby_FormatterMaskData',
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

        H5NNInformacionGeneralClienteIndicadoSupport.$el = table;
        H5NNInformacionGeneralClienteIndicadoSupport.HobbyTblRequest();
      };

    this.HobbyRowToInput = function (row) {
        H5NNInformacionGeneralClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#HobbynHobby', row.nHobby);
        $('#HobbynHobbyDesc').val(row.nHobbyDesc);

    };
    this.HobbyTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5NNInformacionGeneralClienteIndicadoActions.aspx/HobbyTblDataLoad",
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

            columns: [{
                field: 'nConcept',
                title: 'Concepto',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.Financ_clinConcept_FormatterMaskData',
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
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.Financ_clinAmount_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nCurrency',
                title: 'Moneda',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.Financ_clinCurrency_FormatterMaskData',
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
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.Financ_clinUnits_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'nFinanStat',
                title: 'Estado',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.Financ_clinFinanStat_FormatterMaskData',
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

        H5NNInformacionGeneralClienteIndicadoSupport.$el = table;
        H5NNInformacionGeneralClienteIndicadoSupport.Financ_cliTblRequest();
      };

    this.Financ_cliRowToInput = function (row) {
        H5NNInformacionGeneralClienteIndicadoSupport.currentRow = row;
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
            url: "/fasi/dli/queries/H5NNInformacionGeneralClienteIndicadoActions.aspx/Financ_cliTblDataLoad",
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
    this.RelationsTblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',

            columns: [{
                field: 'nRelaship',
                title: 'Nexo',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.RelationsnRelaship_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'nRelashipDesc',
                title: 'Nexo',
                sortable: true,
                halign: 'center'
            }, {
                field: 'sClientr',
                title: 'Cliente',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'sClientrDesc',
                title: 'Cliente',
                sortable: true,
                halign: 'center'
            }]
        });

        H5NNInformacionGeneralClienteIndicadoSupport.$el = table;
        H5NNInformacionGeneralClienteIndicadoSupport.RelationsTblRequest();
      };

    this.RelationsRowToInput = function (row) {
        H5NNInformacionGeneralClienteIndicadoSupport.currentRow = row;
        AutoNumeric.set('#RelationsnRelaship', row.nRelaship);
        $('#RelationsnRelashipDesc').val(row.nRelashipDesc);
        $('#RelationssClientr').val(row.sClientr);
        $('#RelationssClientrDesc').val(row.sClientrDesc);

    };
    this.RelationsTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5NNInformacionGeneralClienteIndicadoActions.aspx/RelationsTblDataLoad",
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
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.CHEQUESNREQUEST_NU_FormatterMaskData',
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
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.CHEQUESNAMOUNT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'NCURRENCYPAY',
                title: 'Moneda del pago',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.CHEQUESNCURRENCYPAY_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NCURRENCYPAYDesc',
                title: 'Moneda del pago',
                sortable: true,
                halign: 'center'
            }, {
                field: 'NCONCEPT',
                title: 'Concepto',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.CHEQUESNCONCEPT_FormatterMaskData',
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
                field: 'NSTA_CHEQUE',
                title: 'Estado',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.CHEQUESNSTA_CHEQUE_FormatterMaskData',
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
                field: 'SINTER_PAY',
                title: 'Beneficiario',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'SINTER_PAYDesc',
                title: 'Beneficiario',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'NOFFICE',
                title: 'Sucursal de entrega',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.CHEQUESNOFFICE_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NOFFICEDesc',
                title: 'Sucursal de entrega',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'NOFFICEAGEN',
                title: 'Oficina de entrega',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.CHEQUESNOFFICEAGEN_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NOFFICEAGENDesc',
                title: 'Oficina de entrega',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'NAGENCY',
                title: 'Agencia de entrega',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.CHEQUESNAGENCY_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NTYPESUPPORT',
                title: 'Documento soporte',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.CHEQUESNTYPESUPPORT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'NTYPESUPPORTDesc',
                title: 'Documento soporte',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'NDOCSUPPORT',
                title: 'Nro.Documento',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.CHEQUESNDOCSUPPORT_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'DSTAT_DATE',
                title: 'F.Estado',
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
            }]
        });

        H5NNInformacionGeneralClienteIndicadoSupport.$el = table;
        H5NNInformacionGeneralClienteIndicadoSupport.CHEQUESTblRequest();
      };

    this.CHEQUESRowToInput = function (row) {
        H5NNInformacionGeneralClienteIndicadoSupport.currentRow = row;
        $('#CHEQUESSREQUEST_TY').val(row.SREQUEST_TY);
        $('#CHEQUESSREQUEST_TYDesc').val(row.SREQUEST_TYDesc);
        AutoNumeric.set('#CHEQUESNREQUEST_NU', row.NREQUEST_NU);
        $('#CHEQUESSCHEQUE').val(row.SCHEQUE);
        AutoNumeric.set('#CHEQUESNAMOUNT', row.NAMOUNT);
        AutoNumeric.set('#CHEQUESNCURRENCYPAY', row.NCURRENCYPAY);
        $('#CHEQUESNCURRENCYPAYDesc').val(row.NCURRENCYPAYDesc);
        AutoNumeric.set('#CHEQUESNCONCEPT', row.NCONCEPT);
        $('#CHEQUESNCONCEPTDesc').val(row.NCONCEPTDesc);
        AutoNumeric.set('#CHEQUESNSTA_CHEQUE', row.NSTA_CHEQUE);
        $('#CHEQUESNSTA_CHEQUEDesc').val(row.NSTA_CHEQUEDesc);
        $('#CHEQUESSINTER_PAY').val(row.SINTER_PAY);
        $('#CHEQUESSINTER_PAYDesc').val(row.SINTER_PAYDesc);
        AutoNumeric.set('#CHEQUESNOFFICE', row.NOFFICE);
        $('#CHEQUESNOFFICEDesc').val(row.NOFFICEDesc);
        AutoNumeric.set('#CHEQUESNOFFICEAGEN', row.NOFFICEAGEN);
        $('#CHEQUESNOFFICEAGENDesc').val(row.NOFFICEAGENDesc);
        AutoNumeric.set('#CHEQUESNAGENCY', row.NAGENCY);
        AutoNumeric.set('#CHEQUESNTYPESUPPORT', row.NTYPESUPPORT);
        $('#CHEQUESNTYPESUPPORTDesc').val(row.NTYPESUPPORTDesc);
        AutoNumeric.set('#CHEQUESNDOCSUPPORT', row.NDOCSUPPORT);
        $('#CHEQUESDSTAT_DATE').val(generalSupport.ToJavaScriptDateCustom(row.DSTAT_DATE, generalSupport.DateFormat()));
        $('#CHEQUESSDESCRIPT').val(row.SDESCRIPT);

    };
    this.CHEQUESTblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5NNInformacionGeneralClienteIndicadoActions.aspx/CHEQUESTblDataLoad",
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
    this.ROLEINCASETblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            pagination: true,
            pageSize: 10,
            sortable: true,
            sidePagination: 'client',
            search: true,

            columns: [{
                field: 'UnderwritingCaseID',
                title: 'Caso de suscripción',
                formatter: 'tableHelperSupport.IsContextMenu',
                sortable: true,
                halign: 'center',
                align: 'right'
            }, {
                field: 'OpenDate',
                title: 'F.Apertura',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'CloseDate',
                title: 'F.Cierre',
                formatter: 'tableHelperSupport.OnlyDateFormatter',
                sortable: true,
                halign: 'center',
                align: 'center'
            }, {
                field: 'Decision',
                title: 'Decisión',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.ROLEINCASEDecision_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'DecisionDesc',
                title: 'Decisión',
                sortable: true,
                halign: 'center'
            }, {
                field: 'Status',
                title: 'Estado',
                formatter: 'H5NNInformacionGeneralClienteIndicadoSupport.ROLEINCASEStatus_FormatterMaskData',
                sortable: true,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'StatusDesc',
                title: 'Estado',
                sortable: true,
                halign: 'center'
            }]
        });
        table.bootstrapTable('refreshOptions', {
            contextMenu: '#ROLEINCASEContextMenu',
            contextMenuButton: '.menu-UnderwritingCaseID',
            beforeContextMenuRow: function (e, row, buttonElement) {
                H5NNInformacionGeneralClienteIndicadoSupport.ROLEINCASERowToInput(row);
                if (!buttonElement ) {

                    table.bootstrapTable('showContextMenu', {
                        event: e,
                        contextMenu: '#ROLEINCASEContextMenu',
                        buttonElement: buttonElement
                    });
                }
                if (buttonElement && $(buttonElement).hasClass('menu-UnderwritingCaseID')) {

                    $('#ROLEINCASETbl').bootstrapTable('showContextMenu', {
                        event: e,
                        contextMenu: '#ROLEINCASE_ROLEINCASEUnderwritingCaseIDContextMenu',
                        buttonElement: buttonElement
                    });
                }
                return false;
            },
            onContextMenuItem: function (row, $el) {
                H5NNInformacionGeneralClienteIndicadoSupport.ROLEINCASERowToInput(row);
                switch ($el.data("item")) {
                    case 'ROLEINCASE_Item1':
                        H5NNInformacionGeneralClienteIndicadoSupport.ROLEINCASE_Item1_Actions(row, null);
                        break;
                    case 'ROLEINCASE_ROLEINCASEUnderwritingCaseID_Item1':
                        H5NNInformacionGeneralClienteIndicadoSupport.ROLEINCASE_ROLEINCASEUnderwritingCaseID_Item1(row);
                        break;
                }
            }
        });

        H5NNInformacionGeneralClienteIndicadoSupport.$el = table;
        H5NNInformacionGeneralClienteIndicadoSupport.ROLEINCASETblRequest();
      };

    this.ROLEINCASERowToInput = function (row) {
        H5NNInformacionGeneralClienteIndicadoSupport.currentRow = row;
        $('#ROLEINCASEUnderwritingCaseID').val(row.UnderwritingCaseID);
        $('#ROLEINCASEOpenDate').val(generalSupport.ToJavaScriptDateCustom(row.OpenDate, generalSupport.DateFormat()));
        $('#ROLEINCASECloseDate').val(generalSupport.ToJavaScriptDateCustom(row.CloseDate, generalSupport.DateFormat()));
        AutoNumeric.set('#ROLEINCASEDecision', row.Decision);
        $('#ROLEINCASEDecisionDesc').val(row.DecisionDesc);
        AutoNumeric.set('#ROLEINCASEStatus', row.Status);
        $('#ROLEINCASEStatusDesc').val(row.StatusDesc);

    };
    this.ROLEINCASETblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];

        $.ajax({
            type: "POST",
            url: "/fasi/dli/queries/H5NNInformacionGeneralClienteIndicadoActions.aspx/ROLEINCASETblDataLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                filter: '',
                ROLEINCASECLIENTID1: row.SCLIENT,
                ROLEINCASECLIENTID5: row.SCLIENT
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

        var detailShow = H5NNInformacionGeneralClienteIndicadoSupport.PHYSICALADDRESSES_ShowValidation(row);
        if (detailShow)
        html.push('<table id="PHYSICALADDRESSESTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Direcciones</caption></table>');
        html.push('<table id="EMAILSTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Correos electrónicos</caption></table>');
        html.push('<table id="SOCIALNETWORKTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Redes sociales</caption></table>');
        html.push('<table id="OTHERTYPEADDRESSTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Otras direcciones</caption></table>');
        html.push('<table id="CertificatTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Pólizas</caption></table>');
        html.push('<table id="PremiumTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Primas</caption></table>');
        html.push('<table id="ClaimTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Siniestros</caption></table>');
        html.push('<table id="AddressTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Direcciones</caption></table>');
        html.push('<table id="CLIDOCUMENTSTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Documentos</caption></table>');
        html.push('<table id="DIR_DEBIT_CLITbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Pago Automático</caption></table>');
        html.push('<table id="Bk_accountTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Cuentas Bancarias</caption></table>');
        html.push('<table id="Cred_cardTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Tarjetas de Crédito</caption></table>');
        html.push('<table id="Curr_accTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Cuentas Corrientes</caption></table>');
        html.push('<table id="SportTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Deportes</caption></table>');
        html.push('<table id="HobbyTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Hobbies</caption></table>');
        html.push('<table id="Financ_cliTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Informacion Financiera</caption></table>');
        html.push('<table id="RelationsTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Relaciones</caption></table>');
        html.push('<table id="CHEQUESTbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Cheques</caption></table>');
        html.push('<table id="ROLEINCASETbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption>Casos de suscripción</caption></table>');

        $detail.html(html.join(""));
        if (detailShow)
        H5NNInformacionGeneralClienteIndicadoSupport.PHYSICALADDRESSESTblSetup($detail.find('#PHYSICALADDRESSESTbl-' + index));
        H5NNInformacionGeneralClienteIndicadoSupport.EMAILSTblSetup($detail.find('#EMAILSTbl-' + index));
        H5NNInformacionGeneralClienteIndicadoSupport.SOCIALNETWORKTblSetup($detail.find('#SOCIALNETWORKTbl-' + index));
        H5NNInformacionGeneralClienteIndicadoSupport.OTHERTYPEADDRESSTblSetup($detail.find('#OTHERTYPEADDRESSTbl-' + index));
        H5NNInformacionGeneralClienteIndicadoSupport.CertificatTblSetup($detail.find('#CertificatTbl-' + index));
        H5NNInformacionGeneralClienteIndicadoSupport.PremiumTblSetup($detail.find('#PremiumTbl-' + index));
        H5NNInformacionGeneralClienteIndicadoSupport.ClaimTblSetup($detail.find('#ClaimTbl-' + index));
        H5NNInformacionGeneralClienteIndicadoSupport.AddressTblSetup($detail.find('#AddressTbl-' + index));
        H5NNInformacionGeneralClienteIndicadoSupport.CLIDOCUMENTSTblSetup($detail.find('#CLIDOCUMENTSTbl-' + index));
        H5NNInformacionGeneralClienteIndicadoSupport.DIR_DEBIT_CLITblSetup($detail.find('#DIR_DEBIT_CLITbl-' + index));
        H5NNInformacionGeneralClienteIndicadoSupport.Bk_accountTblSetup($detail.find('#Bk_accountTbl-' + index));
        H5NNInformacionGeneralClienteIndicadoSupport.Cred_cardTblSetup($detail.find('#Cred_cardTbl-' + index));
        H5NNInformacionGeneralClienteIndicadoSupport.Curr_accTblSetup($detail.find('#Curr_accTbl-' + index));
        H5NNInformacionGeneralClienteIndicadoSupport.SportTblSetup($detail.find('#SportTbl-' + index));
        H5NNInformacionGeneralClienteIndicadoSupport.HobbyTblSetup($detail.find('#HobbyTbl-' + index));
        H5NNInformacionGeneralClienteIndicadoSupport.Financ_cliTblSetup($detail.find('#Financ_cliTbl-' + index));
        H5NNInformacionGeneralClienteIndicadoSupport.RelationsTblSetup($detail.find('#RelationsTbl-' + index));
        H5NNInformacionGeneralClienteIndicadoSupport.CHEQUESTblSetup($detail.find('#CHEQUESTbl-' + index));
        H5NNInformacionGeneralClienteIndicadoSupport.ROLEINCASETblSetup($detail.find('#ROLEINCASETbl-' + index));

    };


    this.ClientnCivilSta_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.ClientnWeight_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 2,
            minimumValue: -99999
        });
      };
    this.ClientnHeight_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999,
            decimalPlaces: 2,
            minimumValue: -9999
        });
      };
    this.ClientnLanguage_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.ClientnMailingPref_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.ClientnTitle_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.ClientnSpeciality_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.ClientnNationality_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.ClientnClass_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.ClientNADDRESSID_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.PHYSICALADDRESSESTypeOfPhysicalAddress_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.PHYSICALADDRESSESAddressID_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.PHYSICALADDRESSESInternalAddressKey_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.EMAILSAddressID_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.EMAILSInternalAddressKey_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.SOCIALNETWORKNetworkType_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.SOCIALNETWORKAddressID_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.SOCIALNETWORKInternalAddressKey_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.OTHERTYPEADDRESSAddressID_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.OTHERTYPEADDRESSInternalAddressKey_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
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
    this.CertificatnNullcode_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
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
    this.PremiumnCurrency_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.PremiumnStatus_pre_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.PremiumnBranch_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.PremiumnProduct_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.PremiumnPolicy_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: "",
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.PremiumnTratypei_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.PremiumnPremium_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.PremiumNBALANCE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.PremiumnCollector_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.PremiumnWay_pay_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.PremiumNINTERMED_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.PremiumnParticip_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 2,
            minimumValue: -99999
        });
      };
    this.PremiumnComamou_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.ClaimnBranch_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.ClaimnProduct_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: "",
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
    this.ClaimnCausecod_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.ClaimnUnaccode_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.ClaimnLoc_reserv_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.ClaimnLoc_pay_am_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.ClaimnLoc_out_am_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.ClaimnLoc_rec_am_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.ClaimnLoc_cos_re_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.AddressnCountry_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.AddressnProvince_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.AddressnLocal_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.AddressnMunicipality_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.PHONESNPHONE_TYPE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.PHONESNAREA_CODE_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.PHONESNEXTENS1_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.PHONESNEXTENS2_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.PHONESNBESTTIMETOCALL_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.CLIDOCUMENTSNTYPCLIENTDOC_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.DIR_DEBIT_CLINBANKEXT_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.DIR_DEBIT_CLINBILL_DAY_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.Bk_accountnTyp_acc_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.Bk_accountnBankExt_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.Cred_cardnCard_type_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.Cred_cardnBankExt_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.Curr_accnTyp_acco_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.Curr_accnDebit_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.Curr_accnCredit_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.Curr_accnBalance_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.Curr_accnCurrency_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.SportnSport_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.HobbynHobby_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.Financ_clinConcept_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9999999999,
            decimalPlaces: 0,
            minimumValue: -9999999999
        });
      };
    this.Financ_clinAmount_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.Financ_clinCurrency_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.Financ_clinUnits_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999999999999,
            decimalPlaces: 6,
            minimumValue: -999999999999999999
        });
      };
    this.Financ_clinFinanStat_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.RelationsnRelaship_FormatterMaskData = function (value, row, index) {          
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
    this.CHEQUESNCONCEPT_FormatterMaskData = function (value, row, index) {          
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
    this.CHEQUESNOFFICE_FormatterMaskData = function (value, row, index) {          
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
    this.ROLEINCASEDecision_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.ROLEINCASEStatus_FormatterMaskData = function (value, row, index) {          
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
        masterSupport.setPageTitle('H5Información del cliente');
        

    H5NNInformacionGeneralClienteIndicadoSupport.ControlBehaviour();
    H5NNInformacionGeneralClienteIndicadoSupport.ControlActions();
    H5NNInformacionGeneralClienteIndicadoSupport.ValidateSetup();

    $('#RecordEffectiveDate').val(generalSupport.URLDateValue('RecordEffectiveDate'));

    $("#ItemsTblPlaceHolder").replaceWith('<table id="ItemsTbl"><caption >Clientes</caption></table>');
    H5NNInformacionGeneralClienteIndicadoSupport.ItemsTblSetup($('#ItemsTbl'));

    $('#RecordEffectiveDate').val(moment().format(generalSupport.DateFormat()));
        H5NNInformacionGeneralClienteIndicadoSupport.ItemsTblRequest();



});

