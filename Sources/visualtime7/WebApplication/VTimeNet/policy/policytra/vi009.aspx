<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eSecurity" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.39
Dim mobjNetFrameWork As eNetFrameWork.Layout
Dim sRequest As String

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas    
Dim mobjMenu As eFunctions.Menues

'- Objetos de consulta a tablas
Dim mobjValPolicyTra As ePolicy.ValPolicyTra

'- Variable que guarda el número de propuesta
Dim mlngProponum As Object

Dim mobjSecurity As eSecurity.SecurScheSurr

Dim lstrQueryString As String
Dim oRole As eSecurity.GenericItem


'%insPreVI009: Esta función se encarga de cargar los datos en la forma "Folder" 
'--------------------------------------------------------------------------------------------
Private Sub insPreVI009()
	'--------------------------------------------------------------------------------------------
	mobjValPolicyTra = New ePolicy.ValPolicyTra
	With Request
		Call mobjValPolicyTra.insPreVI009(.QueryString.Item("sSurrType"), .QueryString.Item("sProcessType"), .QueryString.Item("sCertype"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), mobjValues.StringToType(.QueryString.Item("nOperat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("sSurrPayWay"), eFunctions.Values.eTypeData.etdDouble), mlngProponum, "VI009")
	End With
End Sub

</script>
<%
Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("VI009")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"

mobjSecurity = New eSecurity.SecurScheSurr

Call mobjSecurity.Find(Session("sSche_Code"), False)

mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.39
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.39
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "vi009"
mlngProponum = mobjValues.StringToType(Request.QueryString.Item("nProponum"), eFunctions.Values.eTypeData.etdDouble)
%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 5 $|$$Date: 23/03/04 18:35 $|$$Author: Nvaplat40 $"

//% InsShowVIC001: Despliega la ventana de datos particulares.
//-------------------------------------------------------------------------------------------
function InsShowVIC001(){
//-------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
        ShowPopUp('/VTimeNet/Common/VIC001_K.aspx?sCertype=2&nBranch=' + hddnBranch.value + 
                  '&nProduct=' + hddnProduct.value + '&nPolicy=' + hddnPolicy.value +
                  '&nCertif=' + hddnCertif.value + '&dEffectDate=' + hdddEffecdate.value,'VIC001_K', 500, 400)
    }
}

//% insCalSurrCurr: Calcula el monto según el factor de cambio 
//------------------------------------------------------------------------------------------- 
function insCalSurrCurr(Field){ 
//------------------------------------------------------------------------------------------- 
	with(self.document.forms[0]){ 
//insConvertNumber(tcnPremium.value)	
		if (Field.value!=''){ 
			tcnRescDef.value=VTFormat(insConvertNumber(Field.value) - insConvertNumber(tcnSurrCostPar.value) , '', '', '', tcnRescDef.DecimalPlace, true); 
			tcnSurrCurr.value = VTFormat(insConvertNumber(hddnExchange.value)* (insConvertNumber(Field.value)-insConvertNumber(tcnSurrCostPar.value) ), '', '', '', tcnSurrCurr.DecimalPlace, true); 
		} 
		else { 
			tcnSurrCurr.value = VTFormat(0, '', '', '', tcnSurrCurr.DecimalPlace,true); 
			tcnRescDef.value=VTFormat(0, '', '', '', tcnRescDef.DecimalPlace, true); 
		} 
	} 
}
//% insCalSurrCurr: Calcula el monto según el factor de cambio
//-------------------------------------------------------------------------------------------
function insCalSurrCurr_1(Field){
//-------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
        if (Field.value!='')
            tcnSurrCurr.value = VTFormat(insConvertNumber(hddnExchange.value)*
                                         insConvertNumber(Field.value),
                                         '', '', '', tcnSurrCurr.DecimalPlace, true);
        else
            tcnSurrCurr.value = VTFormat(0, '', '', '', tcnSurrCurr.DecimalPlace, true);
    }
}

//%InsShowClientRole: Muestra la información del rol indicado
//-------------------------------------------------------------------------------------------
function InsShowClientRole(){
//-------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
        if (tcnPolicy.value != hddnPolicy_old.value){
            insDefValues('InsShowClientRole', 'sCertype=2&nBranch=' + cbeBranch.value +
                                              '&nProduct=' + valProduct.value +
                                              '&nPolicy=' + tcnPolicy.value +
                                              '&nCertif=0&nRole=1' +
                                              '&dEffecdate=' + hdddEffecdate.value +
                                              '&sCodispl=VI009&sFrame=fraFolder');
            hddnPolicy_old.value = tcnPolicy.value;
        }
    }
}
//%InsChangePayDate: Cambia la fecha de valorizacion
//-------------------------------------------------------------------------------------------
function InsChangePayDate(sDate){
//-------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
 	    
    tcdPaymentDate.value = sDate.value;

	insDefValues("InsNexchangeVI009",'dPaydate=' + sDate.value + 
								     '&nRequestedSurrAmt=' + tcnRescDef.value,'/VTimeNet/policy/policytra');

    }
}
</SCRIPT>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<%
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.setZone(2, "VI009", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
mobjMenu = Nothing
Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%
Response.Write(mobjValues.ShowWindowsName("VI009", Request.QueryString.Item("sWindowDescript")))
Call insPreVI009()
%>
<FORM METHOD="POST" ID="FORM" NAME="VI009" ACTION="valPolicyTra.aspx?x=1">
<TABLE WIDTH="100%">
    <TR>
        <TD><%=mobjValues.AnimatedButtonControl("btnPolicyValues", "/VTimeNet/images/btn_ValuesOff.png", GetLocalResourceObject("btnPolicyValuesToolTip"),  , "InsShowVIC001()", False)%></TD>
        <TD COLSPAN=2>&nbsp;</TD>
        <TD><LABEL><%= GetLocalResourceObject("tcdPaymentDateCaption") %></LABEL></TD>
		<TD colspan=2><%=mobjValues.DateControl("tcdPaymentDate", CStr(mobjValPolicyTra.dPaymentDate), False, GetLocalResourceObject("tcdPaymentDateToolTip"), False, "", "", "InsChangePayDate(this);", mobjSecurity.sModDatePV <> "1", 32)%></TD>

    </TR>
    <TR>
<TD><LABEL><%= GetLocalResourceObject("cbeCurrencyCaption") %></LABEL></TD>
<TD><%=mobjValues.PossiblesValues("cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, mobjValPolicyTra.DefaultValueVI009("nCurrency"),  , True,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCurrencyToolTip"))%></TD>
<TD>&nbsp;</TD>
<TD><LABEL><%= GetLocalResourceObject("tcnUFValueCaption") %></LABEL></TD>
<TD><%=mobjValues.NumericControl("tcnUFValue", 18, mobjValPolicyTra.DefaultValueVI009("tcnExchange"),  , GetLocalResourceObject("tcnUFValueToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
        
    <TR>
        <TD><LABEL><%= GetLocalResourceObject("tcnSurrValCaption") %></LABEL></TD>
        <TD><%=mobjValues.NumericControl("tcnSurrVal", 18, mobjValPolicyTra.DefaultValueVI009("tcnSurrVal"),  , GetLocalResourceObject("tcnSurrValToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
        <TD>&nbsp;</TD>
        <TD><LABEL><%= GetLocalResourceObject("tcnPremiumCaption") %></LABEL></TD>
        <TD><%=mobjValues.NumericControl("tcnPremium", 18, CStr(mobjValPolicyTra.nPremium),  , GetLocalResourceObject("tcnPremiumToolTip"), True, 6,  ,  ,  ,  , True)%></TD>

    </TR>
    <TR>
        <TD><LABEL><%= GetLocalResourceObject("tcnLoansCaption") %></LABEL></TD>
        <TD><%Response.Write(mobjValues.NumericControl("tcnLoans", 18, mobjValPolicyTra.DefaultValueVI009("nLoans"),  , GetLocalResourceObject("tcnLoansToolTip"), True, 6,  ,  ,  ,  , True))%></TD>
        <TD>&nbsp;</TD>
        <TD><LABEL><%= GetLocalResourceObject("tcnInterestCaption") %></LABEL></TD>
        <TD><%Response.Write(mobjValues.NumericControl("tcnInterest", 18, mobjValPolicyTra.DefaultValueVI009("tcnInterest"),  , GetLocalResourceObject("tcnInterestToolTip"), True, 6,  ,  ,  ,  , True))%></TD>
    </TR>
    <TR>
        <TD><LABEL><%= GetLocalResourceObject("tcnSurrAmountCaption") %></LABEL></TD>
        <TD><%=mobjValues.NumericControl("tcnSurrAmount", 18, mobjValPolicyTra.DefaultValueVI009("tcnSurrAmount", mlngProponum),  , GetLocalResourceObject("tcnSurrAmountToolTip"), True, 6,  ,  ,  , "insCalSurrCurr(this);", Request.QueryString.Item("sSurrType") = "1")%></TD>
        <TD>&nbsp;</TD>
        <TD><LABEL><%= GetLocalResourceObject("tcnSurrCostParCaption") %></LABEL></TD>
        <TD><%=mobjValues.NumericControl("tcnSurrCostPar", 18, mobjValPolicyTra.DefaultValueVI009("tcnSurrCostPar", mlngProponum),  , GetLocalResourceObject("tcnSurrCostParToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
    </TR>
    <TR>
        <TD><LABEL><%= GetLocalResourceObject("tcnRescDefCaption") %></LABEL></TD>
        <TD><%=mobjValues.NumericControl("tcnRescDef", 18, mobjValPolicyTra.DefaultValueVI009("tcnRescDef", mlngProponum),  , GetLocalResourceObject("tcnRescDefToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
        <TD>&nbsp;</TD>
        <TD><LABEL><%= GetLocalResourceObject("tcnSurrCurrCaption") %></LABEL></TD>
        <TD><%=mobjValues.NumericControl("tcnSurrCurr", 18, mobjValPolicyTra.DefaultValueVI009("tcnSurrCurr", mlngProponum),  , GetLocalResourceObject("tcnSurrCurrToolTip"), True, 0,  ,  ,  ,  , True)%></TD>
    </TR>    


<%If Request.QueryString.Item("sSurrPayWay") = "3" Then%>
    <TR>
        <TD COLSPAN="5" CLASS="HighLighted"><LABEL><A><%= GetLocalResourceObject("AnchorCaption") %></A></LABEL></TD>
    </TR>
    <TR>
        <TD COLSPAN="5" CLASS="Horline"></TD>
    </TR>
    <TR>
        <TD><LABEL><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
        <TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), mobjValPolicyTra.DefaultValueVI009("nBraPaySurr"))%></TD>
        <TD>&nbsp;</TD>
        <TD><LABEL><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
        <TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), mobjValPolicyTra.DefaultValueVI009("nBraPaySurr"),  ,  , mobjValPolicyTra.DefaultValueVI009("nProPaySurr"))%></TD>
    </TR>
    <TR>
        <TD><LABEL><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
        <TD>
        <%	
	Response.Write(mobjValues.NumericControl("tcnPolicy", 10, mobjValPolicyTra.DefaultValueVI009("nPolPaySurr"),  , GetLocalResourceObject("tcnPolicyToolTip"),  ,  ,  ,  ,  , "InsShowClientRole();"))
	Response.Write(mobjValues.HiddenControl("hddnPolicy_old", mobjValPolicyTra.DefaultValueVI009("nPolPaySurr")))
	%>
        </TD>
        <TD>&nbsp;</TD>
        <TD><LABEL><%= GetLocalResourceObject("tcnCertifCaption") %></LABEL></TD>
        <TD><%=mobjValues.NumericControl("tcnCertif", 10, mobjValPolicyTra.DefaultValueVI009("nCerPaySurr"),  , GetLocalResourceObject("tcnCertifToolTip"))%></TD>
    </TR>
    <TR>
        <TD><LABEL><%= GetLocalResourceObject("tctClientCaption") %></LABEL></TD>
        <TD><%=mobjValues.ClientControl("tctClient", mobjValPolicyTra.sClient,  , GetLocalResourceObject("tctClientToolTip"),  , True)%></TD>
    </TR>
<%Else%>
<%	If Request.QueryString.Item("sSurrPayWay") = "1" Then%>
    <TR>
        <TD><LABEL><%= GetLocalResourceObject("tctClientCaption") %></LABEL></TD>
        <TD COLSPAN="4"><%		
		mobjValues.TypeList = 1
		mobjValues.ClientRole = "-1"
		
		If mobjSecurity.FindVTRoles(Session("sSche_Code")) Then
			oRole = New eSecurity.GenericItem
			For	Each oRole In mobjSecurity.cVTRoles
				If oRole.bSelected Then
					
					mobjValues.ClientRole = mobjValues.ClientRole & "," & oRole.nId
				End If
			Next oRole
		End If
		oRole = Nothing
		' 'Contratante 
		
		lstrQueryString = "&sCertype=" & Request.QueryString.Item("sCertype") & "&nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&nPolicy=" & Request.QueryString.Item("nPolicy") & "&nCertif=" & Request.QueryString.Item("nCertif") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate")
		
		Response.Write(mobjValues.ClientControl("tctClient", mobjValPolicyTra.sClient,  , GetLocalResourceObject("tctClientToolTip"),  ,  ,  ,  ,  ,  ,  , eFunctions.Values.eTypeClient.SearchClientPolicy,  ,  ,  , lstrQueryString))%></TD>
    </TR>
<%	Else%>
    <TR>
        <TD><LABEL><%= GetLocalResourceObject("tctClientCaption") %></LABEL></TD>
        <TD COLSPAN="4"><%=mobjValues.ClientControl("tctClient", mobjValPolicyTra.sClient,  , GetLocalResourceObject("tctClientToolTip"),  , True)%></TD>
    </TR>
<%	End If%>    
<%	
	If Request.QueryString.Item("sSurrPayWay") = "4" Then
		Response.Write("<TR><TD>" & mobjValues.ButtonNotes("SCA2-9", mobjValPolicyTra.DefaultValueVI009("nNotenum"),  , False) & "<TD></TR>")
	End If
End If
%>
    <TR>
        <TD COLSPAN="2" CLASS="HighLighted"><LABEL><A><%= GetLocalResourceObject("Anchor2Caption") %></A></LABEL></TD>
    </TR>
    <TR>
        <TD COLSPAN="2" CLASS="Horline"></TD>
    </TR>
    <TR>
        <TD><LABEL><%= GetLocalResourceObject("tcnCapitalCaption") %></LABEL></TD>
        <TD><%=mobjValues.NumericControl("tcnCapital", 18, mobjValPolicyTra.DefaultValueVI009("tcnSaldCap"),  , GetLocalResourceObject("tcnCapitalToolTip"), True, 6, True)%></TD>
        <TD>&nbsp;</TD>
        <%
'sRequest = mobjValPolicyTra.DefaultValueVI009("chkRequest")
If Request.QueryString.Item("sProcessType") = "2" Or mlngProponum > 0 Then
	sRequest = "2"
Else
	sRequest = mobjSecurity.sRequest
End If
%>
        <TD COLSPAN="2"><%=mobjValues.CheckControl("chkRequest", GetLocalResourceObject("chkRequestCaption"), sRequest,  ,  , Request.QueryString.Item("sProcessType") = "2" Or mlngProponum > 0 Or mobjSecurity.sRequest <> "3",  , GetLocalResourceObject("chkRequestToolTip"))%></TD>
    </TR>
    <TR>
        <TD><LABEL><%= GetLocalResourceObject("tcnSaldPremCaption") %></LABEL></TD>
        <TD><%=mobjValues.NumericControl("tcnSaldPrem", 18, mobjValPolicyTra.DefaultValueVI009("tcnSaldPrem"),  , GetLocalResourceObject("tcnSaldPremToolTip"), True, 6, True)%></TD>
        <TD>&nbsp;</TD>
        <TD COLSPAN="2"><%=mobjValues.CheckControl("chkReport", GetLocalResourceObject("chkReportCaption"), mobjSecurity.sReport,  ,  , mobjSecurity.sReport <> "3",  , GetLocalResourceObject("chkReportToolTip"))%></TD>
    </TR>
</TABLE>
<%
With Request
	Response.Write(mobjValues.HiddenControl("hddsCertype", .QueryString.Item("sCertype")))
	Response.Write(mobjValues.HiddenControl("hddnBranch", .QueryString.Item("nBranch")))
	Response.Write(mobjValues.HiddenControl("hddnProduct", .QueryString.Item("nProduct")))
	Response.Write(mobjValues.HiddenControl("hddnPolicy", .QueryString.Item("nPolicy")))
	Response.Write(mobjValues.HiddenControl("hddnCertif", .QueryString.Item("nCertif")))
	Response.Write(mobjValues.HiddenControl("hdddEffecdate", .QueryString.Item("dEffecdate")))
	Response.Write(mobjValues.HiddenControl("hddsSurrType", .QueryString.Item("sSurrType")))
	Response.Write(mobjValues.HiddenControl("hddsSurrPayWay", .QueryString.Item("sSurrPayWay")))
	Response.Write(mobjValues.HiddenControl("hddnExchange", mobjValPolicyTra.DefaultValueVI009("tcnExchange")))
	Response.Write(mobjValues.HiddenControl("hddsProcessType", .QueryString.Item("sProcessType")))
	Response.Write(mobjValues.HiddenControl("hddsCodisplOri", .QueryString.Item("sCodisplOri")))
	Response.Write(mobjValues.HiddenControl("hddnOperat", .QueryString.Item("nOperat")))
	Response.Write(mobjValues.HiddenControl("hddnBalance", mobjValPolicyTra.DefaultValueVI009("hddnBalance")))
	Response.Write(mobjValues.HiddenControl("hddnCurrency", mobjValPolicyTra.DefaultValueVI009("nCurrency")))
	Response.Write(mobjValues.HiddenControl("hddnProponum", mlngProponum))
	Response.Write(mobjValues.HiddenControl("hddsAnulReceipt", .QueryString.Item("sAnulReceipt")))
	Response.Write(mobjValues.HiddenControl("hddOffice", .QueryString.Item("nOffice")))
	Response.Write(mobjValues.HiddenControl("hddOfficeAgen", .QueryString.Item("nOfficeAgen")))
	Response.Write(mobjValues.HiddenControl("hddAgency", .QueryString.Item("nAgency")))
	Response.Write(mobjValues.HiddenControl("hddTaxSurr", mobjValPolicyTra.DefaultValueVI009("tcnTaxSurr")))
	Response.Write(mobjValues.HiddenControl("hddSurrValue_Tax", mobjValPolicyTra.DefaultValueVI009("tcnSurrValue_Tax")))
	
End With
Session("OP006_dReqDate") = mobjValues.TypeToString(Today, eFunctions.Values.eTypeData.etdDate)
%>
</FORM>
</BODY>
</HTML>
<%
mobjValPolicyTra = Nothing
mobjValues = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.39
Call mobjNetFrameWork.FinishPage("VI009")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




