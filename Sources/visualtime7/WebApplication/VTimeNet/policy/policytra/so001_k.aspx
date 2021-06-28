<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.20
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
Dim mobjSoap_entry As ePolicy.Soap_entry

</script>
<%mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "SO001"
Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("SO001_k")
mobjSoap_entry = New ePolicy.Soap_entry
%>
<html>
<head>
<script>
    //- Variable para el control de versiones
    document.VssVersion = "$$Revision: 12 $|$$Date: 13/10/04 12:12 $|$$Author: Nvaplat28 $"
</script>
	<meta name = "GENERATOR" content = "Microsoft Visual Studio 6.0">
	<%=mobjValues.StyleSheet()%>
<script language="JavaScript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
<script language="JavaScript" src="/VTimeNet/Scripts/tmenu.js"></script>
    <%
        mobjMenu = New eFunctions.Menues
        '^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
        mobjMenu.sSessionID = Session.SessionID
        mobjMenu.nUsercode = Session("nUsercode")
        '~End Body Block VisualTimer Utility
        Response.Write(mobjMenu.MakeMenu("SO001", "SO001_k.aspx", 1, "", Session("sDesMultiCompany"), Session("sSche_code")))
        Response.Write(mobjMenu.setZone(1, "SO001", "", CShort(Request.QueryString.Item("nWindowTy"))))
	    Response.Write(mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl")))        
        
        mobjMenu = Nothing
    %>
<script>

    //- Variable para el control de versiones
    document.VssVersion = "$$Revision: 12 $|$$Date: 13/10/04 12:12 $"

    function hideManualMakeAndModelTR() {
        $("#ManualMakeAndModelTR").hide();
    }

    function showManualMakeAndModelTR() {
        $("#ManualMakeAndModelTR").show();
    }

    function onChangeCausal(sCausal) {
        if (sCausal == 0) {
            document.getElementsByName('cbeLicense_ty')[0].disabled = false;
            document.getElementsByName('tctRegist')[0].disabled = false;
            document.getElementsByName('tctDigit')[0].disabled = false;
            document.getElementsByName('ValVehMark')[0].disabled = false;
            document.getElementsByName('ValVehModel')[0].disabled = false;
            document.getElementsByName('tcnYear')[0].disabled = false;
            document.getElementsByName('tctMotor')[0].disabled = false;
            document.getElementsByName('tctChassis')[0].disabled = false;
            document.getElementsByName('tctColor')[0].disabled = false;
            document.getElementsByName('dtcClient')[0].disabled = false;
            document.getElementsByName('tctFatherLastName')[0].disabled = false;
            document.getElementsByName('tctMotherLastName')[0].disabled = false;
            document.getElementsByName('tctNames')[0].disabled = false;
            document.getElementsByName('tcnCollectedPremium')[0].disabled = false;
        }
        else {
            if (sCausal == 13) {
                document.getElementsByName('tcnCollectedPremium')[0].disabled = true;
                document.getElementsByName('cbeLicense_ty')[0].disabled = true;
                document.getElementsByName('tctRegist')[0].disabled = true;
                document.getElementsByName('tctDigit')[0].disabled = true;
                document.getElementsByName('ValVehMark')[0].disabled = true;
                document.getElementsByName('ValVehModel')[0].disabled = true;
                document.getElementsByName('tcnYear')[0].disabled = true;
                document.getElementsByName('tctMotor')[0].disabled = true;
                document.getElementsByName('tctChassis')[0].disabled = true;
                document.getElementsByName('tctColor')[0].disabled = true;
                document.getElementsByName('dtcClient')[0].disabled = true;
                document.getElementsByName('tctFatherLastName')[0].disabled = true;
                document.getElementsByName('tctMotherLastName')[0].disabled = true;
                document.getElementsByName('tctNames')[0].disabled = true;
            }
            else {
                document.getElementsByName('cbeLicense_ty')[0].disabled = true;
                document.getElementsByName('tctRegist')[0].disabled = true;
                document.getElementsByName('tctDigit')[0].disabled = true;
                document.getElementsByName('ValVehMark')[0].disabled = true;
                document.getElementsByName('ValVehModel')[0].disabled = true;
                document.getElementsByName('tcnYear')[0].disabled = true;
                document.getElementsByName('tctMotor')[0].disabled = true;
                document.getElementsByName('tctChassis')[0].disabled = true;
                document.getElementsByName('tctColor')[0].disabled = true;
                document.getElementsByName('dtcClient')[0].disabled = true;
                document.getElementsByName('tctFatherLastName')[0].disabled = true;
                document.getElementsByName('tctMotherLastName')[0].disabled = true;
                document.getElementsByName('tctNames')[0].disabled = true;
                document.getElementsByName('tcnCollectedPremium')[0].disabled = true;
            }
        }
     }

    //% insCancel: se controla la acción Cancelar de la página
    //------------------------------------------------------------------------------------------
    function insCancel() {
        //------------------------------------------------------------------------------------------
        return true;
    }
    //%insStateZone: se controla el estado de los campos de la página
    //------------------------------------------------------------------------------------------
    function insStateZone() {
        //------------------------------------------------------------------------------------------
    }
    //% insCheckClient: Verifica si el cliente se encuentra previamente registrado; en caso 
    //%                 contrario es generado automáticamente.
    //--------------------------------------------------------------------------------------------------
    function insCheckClient(sClient) {
        //--------------------------------------------------------------------------------------------------
        with (self.document.forms[0]) {
            if (sClient != '') {
                insDefValues('CheckClient', 'sClient=' + dtcClient.value, '/VTimeNet/policy/policytra');
                //btn_dtcBirthdayDate.disabled = true;
            }
            else {
                tctFatherLastName.value = '';
                tctMotherLastName.value = '';
                tctNames.value = '';
                dtcBirthdayDate.value = '';

                tctFatherLastName.disabled = false;
                tctMotherLastName.disabled = false;
                tctNames.disabled = false;
                //btn_dtcBirthdayDate.disabled = false;
            }
            UpdateDiv('lblCliename', '', 'Normal');
        }
    }

    //% InsChangeValues: Se actualizan los parametros de las listas de valores 
    //------------------------------------------------------------------------------------------- 
    function InsChangeValues(Field) {
        //-------------------------------------------------------------------------------------------
        self.document.forms[0].ValVehModel.Parameters.Param2.sValue = Field.value
    }

        //%insParameterLocat: Actualiza parametros de la region
        //---------------------------------------------------------------------------
        function insParameterLocat(Field) {
            //---------------------------------------------------------------------------
            with (self.document.forms[0]) {
                valLocal.Parameters.Param1.sValue = Field.value;
                valMunicipality.Parameters.Param1.sValue = 0;
                valMunicipality.Parameters.Param2.sValue = Field.value;
                valLocal.disabled = (Field.value == '') ? true : false;
                valLocal.value = '';
                UpdateDiv('valLocalDesc', '')
                valMunicipality.value = '';
                UpdateDiv('valMunicipalityDesc', '')
            }

        }
        //%insParameterMunicipality: Actualiza los parámetros de la comuna
        //---------------------------------------------------------------------------
        function insParameterMunicipality(Field) {
            //---------------------------------------------------------------------------
            with (self.document.forms[0]) {
                valMunicipality.Parameters.Param1.sValue = Field.value;
                valMunicipality.Parameters.Param2.sValue = cbeProvince.value;

                if (Field.value == '') {
                    valMunicipality.Parameters.Param1.sValue = 0;
                }

                valMunicipality.disabled = (Field.value == '') ? true : false;
                if (valMunicipality_nLocal.value != Field.value) {
                    valMunicipality.value = '';
                    UpdateDiv('valMunicipalityDesc', '')
                }
            }
        }

        //%InsChangeMunicipality: Busca la ciudad y la región dada la comuna
        //-------------------------------------------------------------------------------------------	
        function InsChangeMunicipality(nMunicipality) {
            //---------------------------------------------------------------------------------------	
            insDefValues('Municipality', 'nMunicipality=' + nMunicipality, '/VTimeNet/policy/policytra')

        }

        //%insChangeFolio: Busca los datos de la póliza desde el número de folio
        //-------------------------------------------------------------------------------------------	
        function insChangeFolio(nFolio) {
            insDefValues('Folio', 'nFolio=' + nFolio + '&nTypeVeh=' + self.document.forms[0].tctType.value , '/VTimeNet/policy/policytra')
            //setTimeout('insDefValues("ChangeStartDate", "dStartDate=" + self.document.forms[0].tcdStartDate.value, "/VTimeNet/policy/policytra")', 2000);
        }

        //%insChangeTypeVeh: Busca los datos de las fechas desde el tipo de vehiculo
        //-------------------------------------------------------------------------------------------	
        function insChangeTypeVeh(field) {
            //alert(field.value);          
            insDefValues('TypeVeh', 'nTypeVeh=' + field.value +'&nYear=' + self.document.forms[0].hddYear.value, '/VTimeNet/policy/policytra')
        }

        //%insChangeStartDate: Actualiza la fecha de vencimiento.
        //-------------------------------------------------------------------------------------------	
        function insChangeStartDate(dStartDate) {
            //insDefValues('ChangeStartDate', 'dStartDate=' + dStartDate, '/VTimeNet/policy/policytra')
        }
        //% ShowData: Se cargan los valores de acuerdo al número de placa, si ésta está previamente registrada en el sistema 
        //--------------------------------------------------------------------------------------------------------------------
        function ShowData(sField, sField_1) {
            //--------------------------------------------------------------------------------------------------------------------
            var lobjdocument_form = document.forms[0];

            switch (sField) {
                case "Auto_Regist":
                    if (sField_1.oldValue != sField_1.value) {
                        sField_1.oldValue = sField_1.value;
                        lobjdocument_form.tctDigit.value = '';
                        //insDefValues(sField, "sRegist=" + sField_1.value + "&Slicense_ty=" + lobjdocument_form.cbeLicense_ty.value)
                        insDefValues(sField, "sRegist=" + sField_1.value + "&Slicense_ty=" + lobjdocument_form.cbeLicense_ty.value);
                    }
                    break;
                case "Auto_Digit":
                    insDefValues(sField, "sRegist=" + sField_1.value + "&Slicense_ty=" + lobjdocument_form.cbeLicense_ty.value);
                    break;

                case "Slicense_ty":
                    //lobjdocument_form.cbeNlic_special.disabled = true;
                    //lobjdocument_form.cbeNlic_special.value = "";
                    //lobjdocument_form.tctDigit.value = "";

                    if (sField_1.value == '1') {
                        lobjdocument_form.tctRegist.disabled = false;
                        insDefValues("Auto_Regist", "sRegist=" + lobjdocument_form.tctRegist.value + "&Slicense_ty=" + lobjdocument_form.cbeLicense_ty.value)
                    }
                    else {
                        //lobjdocument_form.tctRegister.disabled = false;
                        //lobjdocument_form.tctMotor.disabled = false;
                        //lobjdocument_form.tctChassis.disabled = false;
                        //if (sField_1.value == '3') {
                        //    insDefValues(sField, "sRegist=" + lobjdocument_form.tctRegister.value + "&Slicense_ty=" + sField_1.value)
                        //}
                        //else {
                        //    lobjdocument_form.cbeNlic_special.disabled = false;
                        //}
                    }
                    break;
            }
        }

        //% InsChangeField: Despliega un campo de texto cuando se elige la opcion 'otros' en marca y modelo. 
        //--------------------------------------------------------------------------------------------------------------------
        function InsChangeField(vObj, sField) {
            switch (sField) {
                case 'ValVehMark':
                    if (vObj.value == 9999) {
                        showManualMakeAndModelTR();
                        document.getElementsByName('ValVehModel')[0].focus();
                        document.getElementsByName('ValVehModel')[0].value = '9999';
                        UpdateDiv('ValVehModelDesc', 'Otros');
                        document.getElementsByName('btnValVehModel')[0].disabled = true;
                        document.getElementsByName('ValVehModel')[0].disabled = true;
                        document.getElementsByName('tctMark')[0].focus();
                    }
                    else {
                        document.getElementsByName('tctMark')[0].value = '';
                        document.getElementsByName('tctModel')[0].value = '';
                        hideManualMakeAndModelTR();
                        document.getElementsByName('btnValVehModel')[0].disabled = false;
                        document.getElementsByName('ValVehModel')[0].disabled = false;
                        document.getElementsByName('ValVehModel')[0].value = '';
                        UpdateDiv('ValVehModelDesc', '');
                    }
                    break;
            }
        }

        function insShowInitials() {
            //------------------------------------------------------------------------------------------
            //document.getElementsByTagName("TR")[5].style.display = 'none';
            //document.getElementsByTagName("TR")[8].style.display = 'none';
        }

        //%insChangeIntermedia: Actualiza los parámetros de la comuna
        //---------------------------------------------------------------------------
        function insChangeIntermed(Field) {
            //---------------------------------------------------------------------------
            with (self.document.forms[0]) {
                
                if (Field.value == '') {
                    valAgreement.Parameters.Param1.sValue = 0;
                    valAgreement.value = '';
                    UpdateDiv('valAgreementDesc', '');
                    valAgreement.disabled = true;
                    btnvalAgreement.disabled = true;
                }
                else {
                    valAgreement.Parameters.Param1.sValue = Field.value;
                    valAgreement.value = '';
                    UpdateDiv('valAgreementDesc', '');
                    valAgreement.disabled = false;
                    btnvalAgreement.disabled = false;
                }
                
            }
        }
</script>
</head>
<body onunload="closeWindows();">
<form method="post" id="FORM" name="frmSO001_K" action="ValPolicyTra.aspx?x=1">
	<br></br>
    	<%=mobjValues.ShowWindowsName("SO001", Request.QueryString.Item("sWindowDescript"))%>
    <table width=100% border=0>
<!------ SOAP---------->
	    <tr>
			<td colspan="8" class="HighLighted"><label id=0><a name="SOAP"><%= GetLocalResourceObject("AnchorSOAPCaption") %></a></label></td>
	    </tr>
	    <tr>
			<td colspan=8 class="Horline"></td>
	    </tr>
        <tr>	
            <td><label id=Label4><%= GetLocalResourceObject("tcnFolioCaption") %></label></td>
			<td><%= mobjValues.NumericControl("tcnFolio", 10, CStr(0), , GetLocalResourceObject("tcnFolioToolTip"), , , , , , "insChangeFolio(this.value);")%></td>
            <td>&nbsp;</td>
			<td><label id=LABEL5><%= GetLocalResourceObject("cbeBranchCaption") %></label></td>
			<td><%= mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), "", "valProduct", , , , , True)%></td>
            <td>&nbsp;</td>
			<td><label id=LABEL11><%= GetLocalResourceObject("valProductCaption") %></label></td>
			<td><%= mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), "", eFunctions.Values.eValuesType.clngWindowType, True)%></td>
		</tr>
		<tr>
        	<td><label id=LABEL29>Tipo de Vehículo</label></td>
            <td><%= mobjValues.PossiblesValues("tctType", "table78109", eFunctions.Values.eValuesType.clngComboType, "", , , , , , "insChangeTypeVeh(this);", , , GetLocalResourceObject("cbeModuleToolTip"))%></td>
            <td><input type="hidden" name="dStartDateOri" id="dStartDateOri"/></td>
            <td><label id=0><%= GetLocalResourceObject("valAgreementCaption")%></label></td>
            <% 
                mobjValues.Parameters.Add("nIntermed", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	        %>
            <td><%= mobjValues.PossiblesValues("valAgreement", "TABTABAGREEMENT_INT", Values.eValuesType.clngWindowType, "", True, , , , , "", True, , GetLocalResourceObject("valAgreementToolTip"))%></td>
            <td><input type="hidden" name="dStartDatePol" id="dStartDatePol"/></td>
            <td><input type="hidden" name="dExpirDatePol" id="dExpirDatePol"/></td>
            <!-- InsChangeValues(this);InsChangeField(this,""ValVehMark"hidden"); -->
            <!--InsChangeValues(this);InsChangeField(this,""ValVehMark""); -->
		</tr>
		<tr>
			<td><label id=LABEL6><%= GetLocalResourceObject("tcdStartDateCaption") %></label></td>
			<td><%= mobjValues.DateControl("tcdStartDate", , , GetLocalResourceObject("tcdStartDateToolTip"), , , , "insChangeStartDate(this.value);", True)%></td>
            <td>&nbsp;</td>
		    <td><label id=0><%= GetLocalResourceObject("tcnPolicyCaption") %></label></td>
			<td><%= mobjValues.NumericControl("tcnPolicy", 10, "", , GetLocalResourceObject("tcnPolicyToolTip"), , 0, , , , , True)%></td>
            <td>&nbsp;</td>
			<td><label id=LABEL7><%= GetLocalResourceObject("tcnCertifCaption") %></label></td>
			<td><%= mobjValues.NumericControl("tcnCertif", 10, CStr(0), , GetLocalResourceObject("tcnCertifToolTip"), , 0, , , , , True)%>
            <%= mobjValues.HiddenControl("hddStatusva", "")%>
            </td>
		</tr>
		<tr>
			<td><label id=LABEL8><%= GetLocalResourceObject("tcdExpirDateCaption") %></label></td>
        	<td><%= mobjValues.DateControl("tcdExpirDate", , , GetLocalResourceObject("tcdExpirDateToolTip"), , , , , True)%></td>
            <td>&nbsp;</td>
            <td><label id=LABEL9><%= GetLocalResourceObject("cbeModuleCaption")%></label></td>
            <% 
                mobjValues.Parameters.Add("sCertype", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                mobjValues.Parameters.Add("nBranch", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                mobjValues.Parameters.Add("nProduct", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                mobjValues.Parameters.Add("nPolicy", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                mobjValues.Parameters.Add("nCertif", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                mobjValues.Parameters.Add("dEffecdate", Date.Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                mobjValues.Parameters.Add("nGroup", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	        %>
            <td><%=mobjValues.PossiblesValues("cbeModule", "TabTabModul_Exc", Values.eValuesType.clngWindowType, "", True, , , , , "", True, , GetLocalResourceObject("cbeModuleToolTip"))%></td>
            <td>&nbsp;</TD>
            <td><LABEL ID=LABEL10><%= GetLocalResourceObject("valIntermedCaption") %></LABEL></TD>
            <td><%= mobjValues.PossiblesValues("valIntermed", "Intermedia", eFunctions.Values.eValuesType.clngWindowType, "", , , , , , "insChangeIntermed(this);", True, 10, GetLocalResourceObject("valIntermedToolTip"))%></td>
		</tr>
        <tr>
			<td><label id=LABEL1><%= GetLocalResourceObject("tcnCollectedPremiumCaption") %></label></td>
			<td><%= mobjValues.NumericControl("tcnCollectedPremium", 10, , , GetLocalResourceObject("tcnCollectedPremiumToolTip"), ,2)%></td>
            <td>&nbsp;</td>
			<td><label id=LABEL12>Estado</label></td>
            <td><%= mobjValues.PossiblesValues("valStatusva", "table181", eFunctions.Values.eValuesType.clngComboType, "", , , , , , , True, 10, "")%></td>
            <td>&nbsp;</td>
            
            <td><label id=LABEL31><%= GetLocalResourceObject("ValCausalCaption") %></label></td>
            <td><%  mobjValues.TypeList = 1
                   mobjValues.List = "0,13"
                    Response.Write(mobjValues.PossiblesValues("valCausal", "TAB_WAITPO", eFunctions.Values.eValuesType.clngComboType, "0", , , , , , "onChangeCausal(this.value);", , 10, GetLocalResourceObject("ValCausalToolTip")))%></td>
		</tr>
        <tr>
			<td colspan="2"><%= mobjValues.CheckControl("chkAcchsend_ind", GetLocalResourceObject("chkAcchsend_indCaption"), "0", "1", , True, , GetLocalResourceObject("chkAcchsend_indToolTip"))%></td>
         
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>			
		</tr>
<!------END SOAP-------->
<!------VEH---------->
	    <tr>
		    <td colspan="8" class="HighLighted"><label id=LABEL2><a name="Datos del vehiculo"><%= GetLocalResourceObject("AnchorDatos del vehiculoCaption") %></a></label></td>
	    </tr>
	    <tr>
			<td colspan=8 class="Horline"></td>
	    </tr>
	    <tr>
			<td><label id=LABEL17>Tipo de licencia</label></td>
            <% mobjValues.BlankPosition = False%>
			<td><%=mobjValues.PossiblesValues("cbeLicense_ty", "table80", eFunctions.Values.eValuesType.clngComboType, "1", , , , , , "ShowData(""Slicense_ty"",this);", , , GetLocalResourceObject("cbeLicense_tyToolTip"))%></td>
            <td>&nbsp;</td>
			<td><label id=LABEL3><%= GetLocalResourceObject("tctRegistCaption") %></label></td>
            <td><%= mobjValues.TextControl("tctRegist", 10, "", , GetLocalResourceObject("tctRegistToolTip"), , , , "ShowData(""Auto_Regist"",this);")%>-<%= mobjValues.TextControl("tctDigit", 1, "", , "Dígito verificador de la patente", , , , "ShowData(""Auto_Digit"", document.forms[0].tctRegist);", False)%></td>
	        <td>&nbsp;</td>
			<td><label id=LABEL23>Dígito patente errado</label></td>
            <td><%= mobjValues.TextControl("tctMistakenDigit", 1, "", , "", , , , "", True)%></td>
        </tr>
	    <tr>
            <td><label id=LABEL32><%= GetLocalResourceObject("ValVehMarkCaption") %></label></td>
            <td><%= mobjValues.PossiblesValues("ValVehMark", "table7042", eFunctions.Values.eValuesType.clngComboType, "", , , , , , "InsChangeValues(this);InsChangeField(this,""ValVehMark"");", , , GetLocalResourceObject("ValVehMarkToolTip"))%>
            </td>
            <td>&nbsp;</td>
            <td ><label id=LABEL24><%= GetLocalResourceObject("ValVehModelCaption") %></label></td>
            <td><%  With mobjValues.Parameters
                        .Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Add("nVehBrand", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    End With
                    Response.Write(mobjValues.PossiblesValues("ValVehModel", "tabTab_au_model", eFunctions.Values.eValuesType.clngWindowType, "", True, , , , , "", , 6, GetLocalResourceObject("ValVehModelToolTip")))
                  %>
			</td>
        </tr>
        <tr style="display:none" id='ManualMakeAndModelTR'>
            <td><label id=LABEL33><%= GetLocalResourceObject("ValVehMarkCaption")%></label></td>
            <td><%= mobjValues.TextControl("tctMark", 30, "", , GetLocalResourceObject("ValVehMarkToolTip"), , , , , False)%> </td>
            <td>&nbsp;</td>
            <td><label id=LABEL34><%= GetLocalResourceObject("ValVehModelCaption")%></label></td>
            <td><%= mobjValues.TextControl("tctModel", 30, "", , GetLocalResourceObject("ValVehModelToolTip"), , , , ,False)%> </td>
        </tr>
        <tr>
            <td><label id=LABEL25><%= GetLocalResourceObject("tcnYearCaption") %></label></td>
            <td><%= mobjValues.NumericControl("tcnYear", 4, "", , GetLocalResourceObject("tcnYearToolTip"), , , , , , , False)%></td>
            <td>&nbsp;</td>
            <td><label id=LABEL26><%= GetLocalResourceObject("tctMotorCaption") %></label></td>
            <td><%= mobjValues.TextControl("tctMotor", 40, "", , GetLocalResourceObject("tctMotorToolTip"))%></td>
            <td colspan=6>&nbsp;</td>
        <tr>
            <td><label id=LABEL30>Observaciones</label></td>
            <td colspan=7><%= mobjValues.TextControl("tctDigitalLink", 145, "", , "")%></td>
        </tr>
        <td style='display:none' >
            <td><label id=LABEL27><%= GetLocalResourceObject("tctChassisCaption") %></label></td>
            <td><%= mobjValues.TextControl("tctChassis", 40, "", , GetLocalResourceObject("tctChassisToolTip"))%></td>
            <td>&nbsp;</td>
            <td><label id=LABEL28><%= GetLocalResourceObject("tctColorCaption") %></label></td>
            <td><%= mobjValues.TextControl("tctColor", 40, "", , GetLocalResourceObject("tctColorToolTip"))%></td>
            <td>&nbsp;</td>
	    </tr>
        <!--tr>
           <td> </td>
          <td> <%= mobjValues.TextControl("tctVehModel", 40, "", , GetLocalResourceObject("tctVehModelToolTip"))%></td>
         </tr-->
<!------ END VEH---------->
<!------PROP------------>
	   <tr>
			<td colspan="8" class="HighLighted"><label id=LABEL13><a name="Propietario"><%= GetLocalResourceObject("AnchorPropietarioCaption") %></a></label></td>
	   </tr>
	   <tr>
			<td colspan=8 class="Horline"></td> 
	   </tr>
	   <tr>
			<td><label id=LABEL14><%= GetLocalResourceObject("dtcClientCaption") %></label></td>
            <td><%= mobjValues.ClientControl("dtcClient", "", , GetLocalResourceObject("dtcClientToolTip"), "insCheckClient(this.value);", , "lblCliename", False, , , , , , False)%></td>
	        <td>&nbsp;</td>
			<td><label id=LABEL16><%= GetLocalResourceObject("tctFatherLastNameCaption") %></label></td>			
			<td colspan=4><%=mobjValues.TextControl("tctFatherLastName", 19, "",  , GetLocalResourceObject("tctFatherLastNameToolTip"),  ,  ,  ,  , False)%>
                <%= mobjValues.TextControl("tctMotherLastName", 19, "", , GetLocalResourceObject("tctMotherLastNameToolTip"), , , , , False)%>
            </td>
       </tr>
       <tr>
			<td><label id=LABEL15><%= GetLocalResourceObject("tctNamesCaption") %></label></td>			
			<td colspan=5><%=mobjValues.TextControl("tctNames", 19, "",  , GetLocalResourceObject("tctNamesToolTip"),  ,  ,  ,  , False)%></td>
            <!--td>&nbsp;</td>
			<td ><label id=LABEL17><%= GetLocalResourceObject("dtcBirthdayDateCaption") %></label></td-->			
			<!--td><%=mobjValues.DateControl("dtcBirthdayDate", "",  , GetLocalResourceObject("dtcBirthdayDateToolTip"),  ,  ,  ,  , False)%></td>
            <td colspan=3>&nbsp;</td-->
        </tr>
        <tr style='display:none'>
            <td><label id=LABEL18><%= GetLocalResourceObject("tctAddressCaption") %></label></td>
            <td><%= mobjValues.TextAreaControl("tctAddress", 2, 50, "", , GetLocalResourceObject("tctAddressToolTip"))%></td>
            <td>&nbsp;</td>
            <td><label id=LABEL19><%= GetLocalResourceObject("cbeProvinceCaption") %></label></td>
            <td><%= mobjValues.PossiblesValues("cbeProvince", "Tab_Province", eFunctions.Values.eValuesType.clngComboType, "", , , , , , "insParameterLocat(this)", , , GetLocalResourceObject("cbeProvinceToolTip"))%></td>
            <td>&nbsp;</td>
            <td><label id=LABEL20><%= GetLocalResourceObject("valLocalCaption") %></label></td>
            <td><%
                    mobjValues.Parameters.Add("nProvince", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    Response.Write(mobjValues.PossiblesValues("valLocal", "tabTab_locat_a", eFunctions.Values.eValuesType.clngWindowType, "", True, , , , , "insParameterMunicipality(this)", , , GetLocalResourceObject("valLocalToolTip")))
                %>
            </td>
        </tr>
        <tr style='display:none'>
            <td><label id=LABEL21><%= GetLocalResourceObject("valMunicipalityCaption") %></label></td>
            <td><%
                    With mobjValues.Parameters
                        .Add("nLocat", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Add("nProvince", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .ReturnValue("nLocal", False, vbNullString, True)
                    End With
                    Response.Write(mobjValues.PossiblesValues("valMunicipality", "tab_municipality_a", eFunctions.Values.eValuesType.clngWindowType, "", True, , , , , "InsChangeMunicipality(this.value)", , , GetLocalResourceObject("valMunicipalityToolTip")))
                %>
            </td>
            <td>&nbsp;</td>
            <td><label ID=LABEL22><%= GetLocalResourceObject("tctPhoneCaption") %></label></td>
            <td><%= mobjValues.TextControl("tctPhone", 11, "", True, GetLocalResourceObject("tctPhoneToolTip"), , , , , False)%></td>
		</tr>
        <tr style='display:none'>
            <td colspan="8"><%= mobjValues.CheckControl("chkSequence", GetLocalResourceObject("chkSequenceCaption"),"1")%></td>
        </tr>

    </table>
    <%= mobjValues.HiddenControl("hddYear",String.Empty)%>
</form>
<script>    insShowInitials();</script>
</body>
</html>
<%
    mobjSoap_entry = Nothing
    mobjValues = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.20
    Call mobjNetFrameWork.FinishPage("SO001_k")
    mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




