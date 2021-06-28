<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCollection" %>
<%@ Import namespace="eReports" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.44.07
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

    Dim mobjValues As eFunctions.Values

'% insUpd_print: actualiza la temporal
'--------------------------------------------------------------------------------------------
Private Sub insUpd_print()
	'--------------------------------------------------------------------------------------------
	Dim lclsPremium As eCollection.Premium
	
	lclsPremium = New eCollection.Premium
	
	Call lclsPremium.insUpdCOC679(Request.QueryString.Item("sKey"), mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nReceipt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nDraft"), eFunctions.Values.eTypeData.etdLong), Request.QueryString.Item("sPrint"))
	
	lclsPremium = Nothing
End Sub

'% insUpdateTemp: actualiza la temporal
'--------------------------------------------------------------------------------------------
Private Sub insUpdateTemp()
	'--------------------------------------------------------------------------------------------
	Dim lclsPremiums As eCollection.Premiums
	
	lclsPremiums = New eCollection.Premiums
	
	Call lclsPremiums.insUpdateTmp_COC679(Request.QueryString.Item("sKey"), Request.QueryString.Item("sChains"), mobjValues.StringToType(Request.QueryString.Item("nFirstRecord"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nLastRecord"), eFunctions.Values.eTypeData.etdDouble))
	
	lclsPremiums = Nothing
End Sub

'% insShowData_Policy: 
'--------------------------------------------------------------------------------------------
Private Sub insShowData_Policy()
	'--------------------------------------------------------------------------------------------
	Dim lclsPremium As eCollection.Premium
	Dim lobjValues As eFunctions.Values
	With Server
		lclsPremium = New eCollection.Premium
		lobjValues = New eFunctions.Values
		'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
		lobjValues.sSessionID = Session.SessionID
		lobjValues.nUsercode = Session("nUsercode")
		'~End Body Block VisualTimer Utility
		
		lobjValues.sCodisplPage = "showdefvalues"
	End With
	
	With lclsPremium
		If .FindQuery_COC002("2", lobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), lobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), lobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), "1") Then
			Response.Write("opener.document.forms[0].cbeWayPay.value=" & .nWay_Pay & ";")
			Response.Write("opener.document.forms[0].cbeOffice.value=" & .nOffice & ";")
			Response.Write("opener.document.forms[0].tcdInitDate.value='" & lobjValues.StringToType(CStr(.dStatdate), eFunctions.Values.eTypeData.etdDate) & "';")
			Response.Write("opener.document.forms[0].tcdEndDate.value='" & lobjValues.StringToType(CStr(.dExpirdat), eFunctions.Values.eTypeData.etdDate) & "';")
			Response.Write("opener.document.forms[0].dtcClient.value='" & .sClient & "';")
			Response.Write("opener.$('#dtcClient').change();")
			Response.Write("opener.document.forms[0].tcnBalance.value=" & .nAmount_Tot & ";")
			Response.Write("opener.document.forms[0].cbeCurrency.value=" & .nCurrency & ";")
		Else
			
			Call insBlankData()
		End If
	End With
	lclsPremium = Nothing
End Sub

'% insShowData_Proposal: 
'--------------------------------------------------------------------------------------------
Private Sub insShowData_Proposal()
	'--------------------------------------------------------------------------------------------
	Dim lclsPremium As eCollection.Premium
	Dim lobjValues As eFunctions.Values
	With Server
		lclsPremium = New eCollection.Premium
		lobjValues = New eFunctions.Values
		'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
		lobjValues.sSessionID = Session.SessionID
		lobjValues.nUsercode = Session("nUsercode")
		'~End Body Block VisualTimer Utility
		
		lobjValues.sCodisplPage = "showdefvalues"
	End With
	
	With lclsPremium
		If .FindQuery_COC002("2", lobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), lobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), lobjValues.StringToType(Request.QueryString.Item("nProposal"), eFunctions.Values.eTypeData.etdDouble), "2") Then
			Response.Write("opener.document.forms[0].cbeWayPay.value=" & .nWay_Pay & ";")
			Response.Write("opener.document.forms[0].cbeOffice.value=" & .nOffice & ";")
			Response.Write("opener.document.forms[0].tcdInitDate.value='" & lobjValues.StringToType(CStr(.dStatdate), eFunctions.Values.eTypeData.etdDate) & "';")
			Response.Write("opener.document.forms[0].tcdEndDate.value='" & lobjValues.StringToType(CStr(.dExpirdat), eFunctions.Values.eTypeData.etdDate) & "';")
			Response.Write("opener.document.forms[0].dtcClient.value='" & lobjValues.typetostring(.sClient, eFunctions.Values.eTypeData.etdDouble) & "';")
			Response.Write("opener.$('#dtcClient').change();")
			Response.Write("opener.document.forms[0].tcnBalance.value=" & lobjValues.typetostring(.nAmount_Tot, eFunctions.Values.eTypeData.etdDouble) & ";")
			Response.Write("opener.document.forms[0].cbeCurrency.value=" & lobjValues.typetostring(.nCurrency, eFunctions.Values.eTypeData.etdDouble) & ";")
		Else
			
			Call insBlankData()
		End If
	End With
	lclsPremium = Nothing
End Sub

'% insBlankData: 
'--------------------------------------------------------------------------------------------
Private Sub insBlankData()
	'--------------------------------------------------------------------------------------------
	Response.Write("opener.document.forms[0].cbeWayPay.value='';")
	Response.Write("opener.document.forms[0].cbeOffice.value='';")
	Response.Write("opener.document.forms[0].tcdInitDate.value='';")
	Response.Write("opener.document.forms[0].tcdEndDate.value='';")
	Response.Write("opener.document.forms[0].dtcClient.value='';")
	Response.Write("opener.$('#dtcClient').change();")
	Response.Write("opener.document.forms[0].tcnBalance.value='';")
	Response.Write("opener.document.forms[0].cbeCurrency.value='';")
End Sub

'% insShowData_Receipt: 
'--------------------------------------------------------------------------------------------
Private Sub insShowData_Receipt()
	'--------------------------------------------------------------------------------------------
	Dim lclsPremium As eCollection.Premium
	Dim lobjValues As eFunctions.Values
	With Server
		lclsPremium = New eCollection.Premium
		lobjValues = New eFunctions.Values
		'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
		lobjValues.sSessionID = Session.SessionID
		lobjValues.nUsercode = Session("nUsercode")
		'~End Body Block VisualTimer Utility
		
		lobjValues.sCodisplPage = "showdefvalues"
	End With
	
	With lclsPremium
		If .Find_Receipt_exist(lobjValues.StringToType(Request.QueryString.Item("nReceipt"), eFunctions.Values.eTypeData.etdDouble)) Then
			Response.Write("top.frames['fraHeader'].document.forms[0].cbeBranch.value=" & .nBranch & ";")
			Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.Parameters.Param1.sValue=top.frames['fraHeader'].document.forms[0].cbeBranch.value;")
			Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.value=" & .nProduct & ";")
			Response.Write("top.frames['fraHeader'].$('#valProduct').change();")
		Else
			Call insBlankData_COC003()
		End If
	End With
	
	lclsPremium = Nothing
End Sub

'% insShowData_Receipt_Branch: 
'--------------------------------------------------------------------------------------------
Private Sub insShowData_Receipt_Branch()
	'--------------------------------------------------------------------------------------------
	Dim lclsPremium As eCollection.Premium
	Dim lobjValues As eFunctions.Values
	With Server
		lclsPremium = New eCollection.Premium
		lobjValues = New eFunctions.Values
		'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
		lobjValues.sSessionID = Session.SessionID
		lobjValues.nUsercode = Session("nUsercode")
		'~End Body Block VisualTimer Utility
		
		lobjValues.sCodisplPage = "showdefvalues"
	End With
	
	With lclsPremium
		If .Find_Receipt_Branch(lobjValues.StringToType(Request.QueryString.Item("nReceipt"), eFunctions.Values.eTypeData.etdDouble), lobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble)) Then
			Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.Parameters.Param1.sValue=top.frames['fraHeader'].document.forms[0].cbeBranch.value;")
			Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.value=" & .nProduct & ";")
			Response.Write("top.frames['fraHeader'].$('#valProduct').change();")
		Else
			Call insBlankData_COC003()
		End If
	End With
	
	lclsPremium = Nothing
End Sub

'% insShowData_Receipt_COC009_k: Busca el Ramo y Producto de un Recibo
'--------------------------------------------------------------------------------------------
Private Sub insShowData_Receipt_COC009_k()
	'--------------------------------------------------------------------------------------------
	Dim lclsPremium As eCollection.Premium
	Dim lobjValues As eFunctions.Values
	
        With Server
            lclsPremium = New eCollection.Premium
            lobjValues = New eFunctions.Values
            '^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
            lobjValues.sSessionID = Session.SessionID
            lobjValues.nUsercode = Session("nUsercode")
            '~End Body Block VisualTimer Utility
		
            lobjValues.sCodisplPage = "showdefvalues"
        End With
	
	
        With lclsPremium
		
            If .Find(Request.QueryString.Item("sCertype"), lobjValues.StringToType(Request.QueryString.Item("nReceipt"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, lobjValues.StringToType("0", eFunctions.Values.eTypeData.etdDouble), lobjValues.StringToType("0", eFunctions.Values.eTypeData.etdDouble), False) Then
                'If .Find_COC009(Request.QueryString.Item("sCertype"), lobjValues.StringToType(Request.QueryString.Item("nReceipt"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, lobjValues.StringToType("0", eFunctions.Values.eTypeData.etdDouble), lobjValues.StringToType("0", eFunctions.Values.eTypeData.etdDouble), False) Then
			
                Response.Write("top.frames['fraHeader'].document.forms[0].dtcClient_Digit.value=insgetDV('" & .sClient & "');")
                Response.Write("with(top.frames['fraHeader'].document.forms[0]){")
                Response.Write("cbeBranch.value='" & .nBranch & "';")
                Response.Write("valProduct.Parameters.Param1.sValue=top.frames['fraHeader'].document.forms[0].cbeBranch.value;")
                Response.Write("valProduct.value='" & lobjValues.TypeToString(.nProduct, eFunctions.Values.eTypeData.etdDouble) & "';")
                Response.Write("top.frames['fraHeader'].$('#valProduct').change();")
                Response.Write("cbeAgency.value='" & lobjValues.TypeToString(.nAgency, eFunctions.Values.eTypeData.etdDouble) & "';")
                Response.Write("tcnPolicy.value='" & lobjValues.TypeToString(.nPolicy, eFunctions.Values.eTypeData.etdDouble) & "';")
                Response.Write("dtcClient.value='" & .sClient & "';")
                Response.Write("dtcClient_Digit.disabled=true;")
                Response.Write("cbeInspecto.value='" & lobjValues.TypeToString(.nInspecto, eFunctions.Values.eTypeData.etdDouble) & "';")
                Response.Write("cbeIntermed.value='" & lobjValues.TypeToString(.nIntermed, eFunctions.Values.eTypeData.etdDouble) & "';")
                Response.Write("top.frames['fraHeader'].$('#cbeIntermed').change();")
                Response.Write("tcnPremium.value='" & lobjValues.TypeToString(.nPremium, eFunctions.Values.eTypeData.etdDouble) & "';")
                Response.Write("cbeStatus_pre.value='" & .nStatus_pre & "';")
                Response.Write("top.frames['fraHeader'].$('#cbeStatus_pre').change();")
			
                If .nInspecto = 0 Then
                    Response.Write("tcnContrat.value='';")
                Else
                    Response.Write("tcnContrat.value='" & mobjValues.TypeToString(.nContrat, eFunctions.Values.eTypeData.etdInteger) & "';")
                End If
			
			
                If .nIntermed = 0 Then
                    Response.Write("cbeInspecto.value='';")
                Else
                    Response.Write("cbeInspecto.value='" & lobjValues.TypeToString(.nInspecto, eFunctions.Values.eTypeData.etdDouble) & "';")
                End If
			
                If .nContrat = 0 Then
                    Response.Write("cbeIntermed.value='';")
                Else
                    Response.Write("cbeIntermed.value='" & lobjValues.TypeToString(.nIntermed, eFunctions.Values.eTypeData.etdDouble) & "';")
                End If
			
                Response.Write("bQuery.disabled=false;")
                Response.Write("};")
			
                Response.Write("top.frames['fraHeader'].UpdateDiv('divCurrency','" & .sCurrency & "','');")
                Response.Write("top.frames['fraHeader'].UpdateDiv(""lblCliename"",""" & .sCliename & """,""Normal"");")
            Else
                Response.Write("alert('El número recibo indicado no existe');")
                Call insBlankData_COC009_k()
			
            End If
        End With
	
	lclsPremium = Nothing
End Sub

'% insBlankData: 
'--------------------------------------------------------------------------------------------
Private Sub insBlankData_COC003()
	'--------------------------------------------------------------------------------------------
	
	Response.Write("top.frames['fraHeader'].document.forms[0].cbeBranch.value='';")
	Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.disabled=true;")
	Response.Write("top.frames['fraHeader'].document.forms[0].btnvalProduct.disabled=true;")
	Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.value='';")
	Response.Write("top.frames['fraHeader'].$('#valProduct').change();")
	
End Sub

'% insBlankData: 
'--------------------------------------------------------------------------------------------
Private Sub insBlankData_COC009_k()
	'--------------------------------------------------------------------------------------------
	Response.Write("with(top.frames['fraHeader'].document.forms[0]){")
	Response.Write("  cbeBranch.value='';")
	Response.Write("  valProduct.value='';")
	Response.Write("  valProduct.Parameters.Param1.sValue='';")
	Response.Write("  tcnReceipt.value='';")
	Response.Write("  cbeAgency.value='';")
	Response.Write("  tcnPolicy.value='';")
	Response.Write("  dtcClient.value='';")
	Response.Write("  dtcClient_Digit.value='';")
	Response.Write("  cbeIntermed.value='';")
	Response.Write("  cbeInspecto.value='';")
	Response.Write("  tcnPremium.value='';")
	Response.Write("  cbeStatus_pre.value='';")
	Response.Write("  tcnContrat.value='';")
	Response.Write("  bQuery.disabled=true;")
	Response.Write("}")
	Response.Write("top.frames['fraHeader'].UpdateDiv('divCurrency','','');")
        Response.Write("top.frames['fraHeader'].UpdateDiv('lblCliename','','Normal');")
        Response.Write("top.frames['fraHeader'].UpdateDiv('cbeStatus_preDesc', '', '');")
        Response.Write("top.frames['fraHeader'].UpdateDiv('valProductDesc', '', '');")
        Response.Write("top.frames['fraHeader'].UpdateDiv('cbeIntermedDesc', '', '');")
	
End Sub


'% insPrintCollectionRep: Se encarga de generar el reporte correspondiente.  
'--------------------------------------------------------------------------------------------  
Private Sub insPrintCollectionRep()
	'--------------------------------------------------------------------------------------------  
	Dim mobjDocuments As eReports.Report
	mobjDocuments = New eReports.Report
	
	With mobjDocuments
		.ReportFilename = "COL747.rpt"
		.sCodispl = "COL747"
		
		Response.Write((.Command))
		
	End With
	
	mobjDocuments = Nothing
End Sub

'% insShow_Client_Agreement: Muestra el cliente asociado a un convenio
'--------------------------------------------------------------------------------------------  
Private Sub insShow_Client_Agreement()
	'--------------------------------------------------------------------------------------------  
	Dim lclsAgreement As eCollection.Agreement
	Dim lobjValues As eFunctions.Values
	
	lobjValues = New eFunctions.Values
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
	lobjValues.sSessionID = Session.SessionID
	lobjValues.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	lobjValues.sCodisplPage = "showdefvalues"
	lclsAgreement = New eCollection.Agreement
	
	If lclsAgreement.Find_sClient(lobjValues.StringToType(Request.QueryString.Item("nCod_Agree"), eFunctions.Values.eTypeData.etdDouble)) Then
		Response.Write("top.frames[""fraHeader""].UpdateDiv(""sClient"",""" & lclsAgreement.sClient & "-" & lclsAgreement.sDigit & " " & lclsAgreement.sCliename & """);")
	End If
	
	lclsAgreement = Nothing
	lobjValues = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("showdefvalues")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "showdefvalues"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<SCRIPT>
//+ Variable para el control de versiones
     document.VssVersion="$$Revision: 3 $|$$Date: 22/10/03 13:10 $|$$Author: Nvaplat11 $"

//--------------------------------------------------------------------------------------------
//%insgetDV: Obtiene el dígito verificador de sClient
//--------------------------------------------------------------------------------------------
function insgetDV(sClient) {

	llngFactor = 2;
	llngSummary = 0;

	for (i = sClient.length-1;i>=0; i--){
	    if (llngFactor == 8){
	        llngFactor = 2;
	    };
	    llngSummary = llngSummary + sClient.substr(i,1)*llngFactor;
	    llngFactor++;
	};
		 
	llngRUT = llngSummary%11;
	llngRUT = 11 - llngRUT;
 
	switch (llngRUT){
	     case 11: 
				return "0";
	     case 10: 
				return "K";
	     default:
				return llngRUT.toString();
	};
}
	</SCRIPT>







<%If Request.QueryString.Item("Field") = "COL747_REP" Then
	Call insPrintCollectionRep()
	Response.Write("<SCRIPT>")
	Response.Write(mobjValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
	Response.Write("</SCRIPT>")
Else
	Response.Write("<SCRIPT>")
	Select Case Request.QueryString.Item("Field")
		Case "Policy"
			Call insShowData_Policy()
		Case "Proposal"
			Call insShowData_Proposal()
		Case "Blank"
			Call insBlankData()
		Case "Receipt"
			Call insShowData_Receipt()
		Case "Receipt_Branch"
			Call insShowData_Receipt_Branch()
		Case "Blank_COC003"
			Call insBlankData_COC003()
		Case "Receipt_COC009_k"
			Call insShowData_Receipt_COC009_k()
		Case "Client_Agreement"
			Call insShow_Client_Agreement()
		Case "Letter"
			Call insUpdateTemp()
		Case "InsPrint"
			Call insUpd_print()
	End Select
	
	Response.Write(mobjValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
	Response.Write("</SCRIPT>")
End If
mobjValues = Nothing
%>

</HEAD>
<BODY>
	<FORM NAME="ShowValues1">
	</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.44.07
Call mobjNetFrameWork.FinishPage("showdefvalues")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




