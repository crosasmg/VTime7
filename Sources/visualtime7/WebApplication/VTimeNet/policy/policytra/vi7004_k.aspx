<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSecurity" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

'**- The object to handling the general function to load values is defined
'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mdtmDateSurr As Object
Dim mobjSecurity As eSecurity.SecurScheSurr

'**- The object to handling the generic routines is defined
'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues

    Dim mblnDisabled As Boolean
    Dim mblnDisabled_date As Boolean
Dim mstrBranch As Object
Dim mstrProduct As Object
Dim mstrPolicy As Object
Dim mstrCertif As String
'+ 1-Proceso preliminar; 2-Proceso definitivo
Dim mstrCodisplOri As String
Dim mdtmEffecdate As Object
Dim mstrOffice As Object
Dim mstrAgency As String
Dim mstrOfficeagen As String
Dim mintPreliminar As Object
Dim mintDefinitivo As Object
Dim mintTyp_surr As Object
Dim mintInd_Insur As Object
Dim sTotal As String
Dim sPartial As String
Dim mstrCurrency As Object
Dim mstrClient As String
Dim lclsUsers As eGeneral.Users
Dim lclsRoles As ePolicy.Roles

Dim lstrCurrency As String
Dim lclsCurrenPol As ePolicy.Curren_pol


</script>
<%
Response.Expires = -1
lclsUsers = New eGeneral.Users
lclsUsers.Find(Session("nUsercode"))
     
mobjValues = New eFunctions.Values
mobjSecurity = New eSecurity.SecurScheSurr

Call mobjSecurity.Find(Session("sSche_Code"), False)
If mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate) = eRemoteDB.Constants.dtmNull Then
	'-Fecha Libre
	If mobjSecurity.nTypeResc = 5 Then
		mdtmDateSurr = ""
		'-Fecha del Dia    
	ElseIf mobjSecurity.nTypeResc = 4 Then 
		mdtmDateSurr = Today
		'-Primer Dia del mes siguiente    
	ElseIf mobjSecurity.nTypeResc = 1 Then 
		If Len(CStr(Month(Today) + 1)) > 1 Then
			mdtmDateSurr = "01/" & Month(Today) + 1 & "/" & Year(Today)
		Else
			mdtmDateSurr = "01/0" & Month(Today) + 1 & "/" & Year(Today)
		End If
	End If
Else
	mdtmDateSurr = mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)
End If


mobjValues.sCodisplPage = "vi7004_k"

mobjMenu = New eFunctions.Menues

If IsNothing(Request.QueryString.Item("sTyp_surr")) Then
	mintTyp_surr = 2
Else
	mintTyp_surr = Request.QueryString.Item("sTyp_surr")
End If

If mintTyp_surr = 1 And mobjSecurity.sRescTot = "1" Then
	sTotal = "1"
ElseIf mobjSecurity.sRescPar = "1" Then 
	sPartial = "1"
End If


If IsNothing(Request.QueryString.Item("sInd_Insur")) Then
	mintInd_Insur = 2
Else
	mintInd_Insur = Request.QueryString.Item("sInd_Insur")
End If

mstrCodisplOri = Request.QueryString.Item("sCodisplOri")
mblnDisabled = mstrCodisplOri = "CA767"

    mblnDisabled_date = False

    If mobjSecurity.sModDateR = "1" Or mstrCodisplOri <> "CA767" Then
        mblnDisabled_date = False
    Else
        mblnDisabled_date = True
    End If
    
If mblnDisabled And CDbl(Request.QueryString.Item("nOperat")) = 2 Then
	mintDefinitivo = 1
Else
	
	If mblnDisabled Or CDbl(Request.QueryString.Item("nOperat")) = 5 Then
		If mobjSecurity.bAllowsPreliminaryExecutions Then
			mintPreliminar = 1
		ElseIf mobjSecurity.bAllowsDefinitiveExecutions Then 
			mintDefinitivo = 1
		End If
	Else
		If mobjSecurity.bAllowsDefinitiveExecutions Then
			mintDefinitivo = 1
		ElseIf mobjSecurity.bAllowsPreliminaryExecutions Then 
			mintPreliminar = 1
		End If
	End If
End If

mstrBranch = Request.QueryString.Item("nBranch")
'If mstrBranch = vbNullString Then mstrBranch = Session("nBranch")
If mstrBranch = vbNullString Then mstrBranch = eRemoteDB.Constants.intNull

mstrProduct = Request.QueryString.Item("nProduct")
'If mstrProduct = vbNullString Then mstrProduct = Session("nProduct")
If mstrProduct = vbNullString Then mstrProduct = eRemoteDB.Constants.intNull

mstrPolicy = Request.QueryString.Item("nProponum")
'If mstrPolicy =  vbNullString Then mstrPolicy = Session("nPolicy")
If mstrPolicy = vbNullString Then mstrPolicy = eRemoteDB.Constants.intNull

mstrCertif = Request.QueryString.Item("nCertif")
'If mstrCertif = vbNullString Then mstrCertif = Session("nCertif")
If mstrCertif = vbNullString Then mstrCertif = " "

mdtmEffecdate = mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)
'If mdtmEffecdate = dtmNull Then mdtmEffecdate = Session("dEffecdate")
If mdtmEffecdate = eRemoteDB.Constants.dtmNull Then mdtmEffecdate = Today

'mstrOffice = Session("nOffice")
    
If IsNothing(Request.QueryString.Item("nOffice")) Then
    mstrOffice = lclsUsers.nOffice
Else
	mstrOffice = mobjValues.StringToType(Request.QueryString.Item("nOffice"), eFunctions.Values.eTypeData.etdDouble)
End If   
    
If IsNothing(Request.QueryString.Item("nOfficeagen")) Then
    mstrOfficeagen = lclsUsers.nOfficeagen
Else
	mstrOfficeagen = mobjValues.StringToType(Request.QueryString.Item("nOfficeagen"), eFunctions.Values.eTypeData.etdDouble)
End If    
    
If IsNothing(Request.QueryString.Item("nAgency")) Then
    mstrAgency = lclsUsers.nAgency
Else
	mstrAgency = mobjValues.StringToType(Request.QueryString.Item("nAgency"), eFunctions.Values.eTypeData.etdDouble)
End If
    
lclsRoles = New ePolicy.Roles
If lclsRoles.Find("2", mobjValues.StringToType(mstrBranch, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mstrProduct, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mstrPolicy, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mstrCertif, eFunctions.Values.eTypeData.etdDouble), 1, "", mobjValues.StringToType(mdtmEffecdate, eFunctions.Values.eTypeData.etdDate)) Then
	mstrClient = lclsRoles.sClient
End If
lclsRoles = Nothing
lclsCurrenPol = New ePolicy.Curren_pol
lstrCurrency = lclsCurrenPol.findCurrency("2", mobjValues.StringToType(mstrBranch, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mstrProduct, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mstrPolicy, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mstrCertif, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mdtmEffecdate, eFunctions.Values.eTypeData.etdDate))

If lstrCurrency = "*" Then
	mstrCurrency = 1
Else
	mstrCurrency = lclsCurrenPol.nCurrency
End If
lclsCurrenPol = Nothing

%>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT>
    var nMainInsur = 0;
    //**+ Source Safe control of version
    //+ Para Control de Versiones de Source Safe
    document.VssVersion = "$$Revision: 3 $|$$Date: 10-05-06 12:02 $"

    //% insEnabledChkInsur: Habilita o no el Check de solicitar antecedentes
    //-------------------------------------------------------------------------------------------
    function insEnabledChkInsur() {
        //-------------------------------------------------------------------------------------------
        with (self.document.forms[0]) {

            if ((cbeSurrReas.value == "2") || (cbeSurrReas.value == "1")) {
                chkInsur.disabled = true;
                chkInsur.checked = false;

            }
            else {
                chkInsur.disabled = false;
            }
        }
    }

    //% InsCheckNullPrem: Habilita o no el Check de anular recibos pendientes
    //-------------------------------------------------------------------------------------------
    function InsHandleRelatedFields() {
        //-------------------------------------------------------------------------------------------
        with (self.document.forms[0]) {
            if (optSurrType[0].checked) {
                chkInsur.disabled = true;
                chkInsur.checked = false;

            }
            else {
                if (nMainInsur == 0) chkInsur.disabled = false;
            }
        }
    }

    //% InsHandlechkInsur: Habilita o no el Check de antecedentes de acuerdo a la póliza
    //-------------------------------------------------------------------------------------------
    function InsHandlechkInsur() {
        //-------------------------------------------------------------------------------------------
        with (document.forms[0]) {
            if (cbeBranch.value != '' && valProduct.value != '' && tcnPolicy.value != '' && tcnCertif.value != '' && tcdEffecdate.value != '') {
                if (!optSurrType[0].checked) {
                    insDefValues('DisabledInsurRecord', 'nBranch=' + cbeBranch.value +
				                                    '&nProduct=' + valProduct.value +
				                                    '&nPolicy=' + tcnPolicy.value +
				                                    "&nCertif=" + tcnCertif.value +
				                                    '&dEffecdate=' + tcdEffecdate.value +
				                                    '&sCodispl=VI7004');
                }
            }
            else {
                nMainInsur = 0;
            }
        }
    }



    //**% insStateZone: This function enable/disable the fields of the page according to the action 
    //**% to be performed
    //% insStateZone: Habilita los campos de la forma según la acción a ejecutar
    //-------------------------------------------------------------------------------------------
    function SetProductParameters() {
        //-------------------------------------------------------------------------------------------    
        var frm = self.document.forms[0];
        with (document.forms[0]) {
            if (cbeBranch.value != '0' && cbeBranch.value != '') {
                valProduct.Parameters.Param1.sValue = cbeBranch.value;
                valProduct.disabled = false;
                btnvalProduct.disabled = false;
            }
            else {
                valProduct.disabled = true;
                btnvalProduct.disabled = true;
                UpdateDiv('valProductDesc', '');
            }
        }
    }

    //**% insStateZone: This function enable/disable the fields of the page according to the action 
    //**% to be performed
    //% insStateZone: Habilita los campos de la forma según la acción a ejecutar
    //-------------------------------------------------------------------------------------------
    function SetReasonParameters() {
        //-------------------------------------------------------------------------------------------    
        var frm = self.document.forms[0];
        with (document.forms[0]) {
            if (cbeBranch.value != '0' && valProduct.value != '' && tcdEffecdate.value != '') {
                cbeSurrReas.Parameters.Param1.sValue = cbeBranch.value;
                cbeSurrReas.Parameters.Param2.sValue = valProduct.value;
                cbeSurrReas.Parameters.Param3.sValue = tcdEffecdate.value;
                cbeSurrReas.disabled = false;
                btncbeSurrReas.disabled = false;

                insDefValues('inssApv', 'nBranch=' + frm.cbeBranch.value +
			                    '&nProduct=' + frm.valProduct.value +
			                    '&dEffecdate=' + frm.tcdEffecdate.value);
            }
            else {
                //cbeSurrReas.disabled = true;
                //btncbeSurrReas.disabled = true;
                cbeSurrReas.value = '';
                UpdateDiv('cbeSurrReasDesc', '');
            }
        }
    }




    //**% insStateZone: This function enable/disable the fields of the page according to the action 
    //**% to be performed
    //% insStateZone: Habilita los campos de la forma según la acción a ejecutar
    //-------------------------------------------------------------------------------------------
    function insStateZone() {
        //-------------------------------------------------------------------------------------------    
    }

    //**% insCancel: This function executes the action to cancel of the page.
    //% insCancel: Esta función ejecuta la acción Cancelar de la página.
    //------------------------------------------------------------------------------------------
    function insCancel() {
        //------------------------------------------------------------------------------------------
        return true;
    }



    //**% FindCurrPolicy: The search of the policy currency is performed
    //% FindCurrPolicy: Se busca la moneda de la póliza
    //-----------------------------------------------------------------------------
    function FindCurrPolicy() {
        //-----------------------------------------------------------------------------
        var frm = self.document.forms[0];
        insDefValues('Switch_Curr_Pol', 'nBranch=' + frm.cbeBranch.value +
                                    '&nProduct=' + frm.valProduct.value +
                                    '&nPolicy=' + frm.tcnPolicy.value +
									'&dEffecdate=' + frm.tcdEffecdate.value +
                                    '&sCodispl=VI7000');

        //+Se espera un tiempo antes de cargar datos de certificado
        //setTimeout("FindCurrCertif()",1000);
    }

    //**% FindCurrCertif: The search of the certificate currency is performed
    //% FindCurr: Se busca la moneda del certificado
    //-----------------------------------------------------------------------------
    function FindCurrCertif() {
        //-----------------------------------------------------------------------------
        var frm = self.document.forms[0];
        var sStringQu

        sStringQu = "nBranch=" + frm.cbeBranch.value +
                "&nProduct=" + frm.valProduct.value +
                "&nPolicy=" + frm.tcnPolicy.value +
                "&nCertif=" + (frm.tcnCertif.value == '' ? '0' : frm.tcnCertif.value) +
				"&sCod_VI7004=VI7004" +
                "&dEffecdate=" + frm.tcdEffecdate.value;

        insDefValues('Switch_Curr_Cer', sStringQu);
    }

    //-------------------------------------------------------------------------------------------
    function insPopulatePFields(nOffice, nOfficeAgen, nAgency, sOfficeAgenDesc, sAgencyDesc) {
        //-------------------------------------------------------------------------------------------
        with (self.document.forms[0]) {
            cbeOfficeAgen.value = nOfficeAgen;
            UpdateDiv('cbeOfficeAgenDesc', sOfficeAgenDesc);
            cbeOfficeAgen.Parameters.Param1.sValue = nOffice;
            cbeOfficeAgen.Parameters.Param2.sValue = '0';
            cbeAgency.value = nAgency;
            UpdateDiv('cbeAgencyDesc', sAgencyDesc);
            cbeAgency.Parameters.Param1.sValue = nOffice;
            cbeAgency.Parameters.Param2.sValue = '0';
        }
    }


    //-------------------------------------------------------------------------------------------
    function insBlankPFields(nOffice) {
        //-------------------------------------------------------------------------------------------
        with (self.document.forms[0]) {
            cbeOfficeAgen.value = '';
            UpdateDiv('cbeOfficeAgenDesc', '');
            cbeOfficeAgen.Parameters.Param1.sValue = nOffice;
            cbeOfficeAgen.Parameters.Param2.sValue = '0';
            cbeAgency.value = '';
            UpdateDiv('cbeAgencyDesc', '');
            cbeAgency.Parameters.Param1.sValue = nOffice;
            cbeAgency.Parameters.Param2.sValue = '0';
        }
    }


    //% insInitialAgency: manejo de sucursal/oficina/agencia
    //-------------------------------------------------------------------------------------------
    function insInitialAgency(nInd) {
        //-------------------------------------------------------------------------------------------    
        with (self.document.forms[0]) {
            //+ Cambia la sucursal 
            if (nInd == 1) {
                if (typeof (cbeOffice) != 'undefined') {
                    if (cbeOffice.value != 0) {
                        if (typeof (cbeOfficeAgen) != 'undefined') {
                            cbeOfficeAgen.Parameters.Param1.sValue = (cbeOffice.value == '' ? 0 : cbeOffice.value);
                            cbeOfficeAgen.Parameters.Param2.sValue = 0;
                            cbeAgency.Parameters.Param1.sValue = (cbeOffice.value == '' ? 0 : cbeOffice.value);
                            if (cbeOfficeAgen.value != "" && cbeOfficeAgen.value > 0)
                                cbeAgency.Parameters.Param2.sValue = (cbeOfficeAgen.value == '' ? 0 : cbeOfficeAgen.value);
                            else
                                cbeAgency.Parameters.Param2.sValue = 0;
                        }
                    }
                    else {
                        if (typeof (cbeOfficeAgen) != 'undefined') {
                            cbeOfficeAgen.Parameters.Param1.sValue = (cbeOffice.value == '' ? 0 : cbeOffice.value);
                            cbeOfficeAgen.Parameters.Param2.sValue = 0;
                            cbeAgency.Parameters.Param1.sValue = (cbeOffice.value == '' ? 0 : cbeOffice.value);
                            if (cbeOfficeAgen.value != "" && cbeOfficeAgen.value > 0) {
                                cbeAgency.Parameters.Param2.sValue = (cbeOfficeAgen.value == '' ? 0 : cbeOfficeAgen.value);
                            }
                            else {
                                cbeAgency.Parameters.Param2.sValue = 0;
                            }
                        }
                    }
                }
            }
            //+ Cambia la oficina 
            else {
                if (nInd == 2) {
                    if (cbeOfficeAgen.value != '') {
                        cbeAgency.Parameters.Param1.sValue = (cbeOffice.value == '' ? 0 : cbeOffice.value);
                        cbeAgency.Parameters.Param2.sValue = (cbeOfficeAgen.value == '' ? 0 : cbeOfficeAgen.value);
                        cbeOffice.value = cbeOfficeAgen_nBran_off.value;
                        cbeOfficeAgen.Parameters.Param1.sValue = (cbeOffice.value == '' ? 0 : cbeOffice.value);
                    }
                    else {
                        cbeAgency.Parameters.Param1.sValue = 0;
                        cbeAgency.Parameters.Param2.sValue = 0;
                    }
                }
                //+ Cambia la Agencia
                else {
                    if (nInd == 3) {
                        if (cbeAgency.value != "") {
                            cbeOffice.value = cbeAgency_nBran_off.value;
                            if (cbeOfficeAgen.value == '') {
                                cbeOfficeAgen.value = cbeAgency_nOfficeAgen.value;
                                UpdateDiv('cbeOfficeAgenDesc', cbeAgency_sDesAgen.value);
                            }
                            cbeOfficeAgen.Parameters.Param1.sValue = (cbeOffice.value == '' ? 0 : cbeOffice.value);
                            cbeAgency.Parameters.Param1.sValue = (cbeOffice.value == '' ? 0 : cbeOffice.value);
                            cbeAgency.Parameters.Param2.sValue = (cbeOfficeAgen.value == '' ? 0 : cbeOfficeAgen.value);
                        }
                    }
                }
            }
        }
    }
</SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    
<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("VI7004", "VI7004_k.aspx", 1, ""))
End With

mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<TD><BR></TD>
<TD><BR></TD>
<FORM METHOD="post" ID="FORM" NAME="VI7004" ACTION="valPolicyTra.aspx?x=1&sCodisplOri=<%=Request.QueryString.Item("sCodisplOri")%>&nOperat=<%=Request.QueryString.Item("nOperat")%>">
    <TABLE WIDTH="100%" BORDER=0>
        <TR>
            <TD WIDTH=15%><LABEL ID=13658><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD WIDTH=20%><%mobjValues.BlankPosition = False
mobjValues.Parameters.Add("nUserCode", Session("nUserCode"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("sLife", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
Response.Write(mobjValues.PossiblesValues("cbeBranch", "TabScheSurrBranch", 1, vbNullString, True,  ,  ,  ,  , "SetProductParameters();SetReasonParameters();InsHandlechkInsur();", CBool(mblnDisabled),  , GetLocalResourceObject("cbeBranchToolTip")))
%>
            <TD WIDTH=15%><LABEL ID=13664><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
            <TD WIDTH=30%><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), mstrBranch, eFunctions.Values.eValuesType.clngWindowType, CBool(mblnDisabled), mstrProduct,  ,  ,  , "SetReasonParameters();InsHandlechkInsur();",  ,  ,  , eFunctions.Values.eProdClass.clngAll)%></TD>
            <TD WIDTH=10%></TD>
            <TD WIDTH=30%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=13663><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnPolicy", 8, mstrPolicy,  , GetLocalResourceObject("tcnPolicyToolTip"),  , 0,  ,  ,  , "FindCurrPolicy();InsHandlechkInsur();", CBool(mblnDisabled))%></TD>
            <TD><LABEL ID=13660><%= GetLocalResourceObject("tcnCertifCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnCertif", 8, mstrCertif,  , GetLocalResourceObject("tcnCertifToolTip"),  , 0,  ,  ,  , "FindCurrCertif();InsHandlechkInsur();", True)%></TD>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnProponumCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnProponum", 10, Request.QueryString.Item("nPolicy"),  , GetLocalResourceObject("tcnProponumToolTip"),  ,  ,  ,  ,  ,  , True)%></TD>
			<TD><LABEL ID=13837><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
			<TD><%= mobjValues.DateControl("tcdEffecdate", mdtmDateSurr, , GetLocalResourceObject("tcdEffecdateToolTip"), , , , "SetReasonParameters();InsHandlechkInsur();", CBool(mblnDisabled_date))%></TD>
		</TR>
        <TR>
            <TD><LABEL ID=13378><%= GetLocalResourceObject("cbeOfficeCaption") %></LABEL></TD>
            <TD>
            <%
mobjValues.TypeOrder = 1
Response.Write(mobjValues.PossiblesValues("cbeOffice", "Table9", 1, mstrOffice,  ,  ,  ,  ,  , "insInitialAgency(1)", ,  , GetLocalResourceObject("cbeOfficeToolTip")))
%>
            </TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeOfficeAgenCaption") %></LABEL></TD>
            <TD>
            <%
With mobjValues
	.Parameters.Add("nOfficeAgen", mstrOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nAgency", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.ReturnValue("nBran_off",  ,  , True)
	Response.Write(.PossiblesValues("cbeOfficeAgen", "TabAgencies_T5556", 2,  mstrOfficeagen , True,  ,  ,  ,  , "insInitialAgency(2)", ,  , GetLocalResourceObject("cbeOfficeAgenToolTip")))
End With
%>
            </TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeAgencyCaption") %></LABEL></TD>
            <TD>
            <%
With mobjValues
	.Parameters.Add("nOfficeAgen", mstrOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nAgency", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.ReturnValue("nBran_off",  ,  , True)
	.Parameters.ReturnValue("nOfficeAgen",  ,  , True)
	.Parameters.ReturnValue("sDesAgen",  ,  , True)
	Response.Write(mobjValues.PossiblesValues("cbeAgency", "TabAgencies_T5555", 2, mstrAgency, True,  ,  ,  ,  , "insInitialAgency(3)", ,  , GetLocalResourceObject("cbeAgencyToolTip")))
End With
%>
            </TD>

        </TR>
     </TABLE>   
     <TABLE WIDTH="100%" BORDER=0>
        <TR>
           <TD align=left CLASS="HighLighted" WIDTH=20%><LABEL ID=41180><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
           <TD></TD>
           <TD align=left CLASS="HighLighted" WIDTH=20%><LABEL ID=41180><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
        </TR>
        <TR>
           <TD CLASS="Horline"></TD>
           <TD></TD>        
           <TD CLASS="Horline"></TD>
        </TR>
        <TR>
           <TD><%=mobjValues.OptionControl(41185, "optSurrType", GetLocalResourceObject("optSurrType_1Caption"), sTotal, "1", "InsHandleRelatedFields()", CBool(mblnDisabled) Or mobjSecurity.sRescTot <> "1",  , GetLocalResourceObject("optSurrType_1ToolTip"))%></TD>
           <TD></TD>
           <TD><%=mobjValues.OptionControl(41185, "optProcessType", GetLocalResourceObject("optProcessType_2Caption"), mintDefinitivo, "2",  , CBool(mblnDisabled) Or Not mobjSecurity.bAllowsDefinitiveExecutions,  , GetLocalResourceObject("optProcessType_2ToolTip"))%></TD>        
           <TD></TD>
           <TD><LABEL><%= GetLocalResourceObject("cbeSurrReasCaption") %></LABEL></TD>
           <%
With mobjValues
	.Parameters.Add("nBranch", mstrBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nProduct", mstrProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("dEffecDate", mdtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("SSCHE_CODE", Session("SSCHE_CODE"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With
%>
           <TD WIDTH=35%><%=mobjValues.PossiblesValues("cbeSurrReas", "TabSur_reason_schema", eFunctions.Values.eValuesType.clngWindowType, Request.QueryString.Item("nSurrReas"), True,  ,  ,  ,  , "insEnabledChkInsur()", False,  , GetLocalResourceObject("cbeSurrReasToolTip"),  , 1)%></TD>
        </TR>        
        <TR>    
           <TD><%=mobjValues.OptionControl(41185, "optSurrType", GetLocalResourceObject("optSurrType_2Caption"), sPartial, "2", "InsHandleRelatedFields();", CBool(mblnDisabled) Or mobjSecurity.sRescPar <> "1",  , GetLocalResourceObject("optSurrType_2ToolTip"))%></TD>
           <TD></TD>
           <TD><%=mobjValues.OptionControl(41185, "optProcessType", GetLocalResourceObject("optProcessType_1Caption"), mintPreliminar, "1",  , CBool(mblnDisabled) Or Not mobjSecurity.bAllowsPreliminaryExecutions,  , GetLocalResourceObject("optProcessType_1ToolTip"))%></TD>
           <TD></TD>
           <TD><LABEL><%= GetLocalResourceObject("Anchor3Caption") %></LABEL></TD>
           <TD><%=mobjValues.CheckControl("chkInsur", "", mintInd_Insur, "1",  , CBool(mblnDisabled) Or mstrProduct = 703 Or mstrProduct = 704,  , GetLocalResourceObject("chkInsurToolTip"))%></TD>
			<TD><%=mobjValues.HiddenControl("hsApv", "")%></TD>           
        </TR>
     </TABLE>        
    
<%
With Response
	.Write(mobjValues.HiddenControl("cbeCurrency", mstrCurrency))
	.Write(mobjValues.HiddenControl("hddClientBenef", mstrClient))
	.Write("<SCRIPT>")
	.Write("FindCurrPolicy();")
	.Write("insInitialAgency(3);")
	.Write("SetProductParameters();")
	
	.Write("</SCRIPT>")
End With
%>    
</FORM>
</BODY>
<%
mobjValues = Nothing%> 

</HTML>
