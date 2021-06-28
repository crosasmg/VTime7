<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCashBank" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo de las rutinas genéricas

Dim mobjMenu As eFunctions.Menues

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid



'%insDefineHeader: Se definen las columnas del grid
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "op008"
	
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		Call .AddTextColumn(0, GetLocalResourceObject("tctRequestColumnCaption"), "tctRequest", 15, "",  , GetLocalResourceObject("tctRequestColumnToolTip"),  ,  ,  , True)
		Call .AddTextColumn(0, GetLocalResourceObject("tctChequeColumnCaption"), "tctCheque", 15, "",  , GetLocalResourceObject("tctChequeColumnToolTip"),  ,  ,  , True)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeStateColumnCaption"), "cbeState", "table187", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , True, 4, GetLocalResourceObject("cbeStateColumnToolTip"), eFunctions.Values.eTypeCode.eNumeric)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAccountNumColumnCaption"), "tcnAccountNum", 0, "", False, GetLocalResourceObject("tcnAccountNumColumnToolTip"), False,  ,  ,  ,  , True)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeBankColumnCaption"), "cbeBank", "table7", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , True, 4, GetLocalResourceObject("cbeBankColumnToolTip"), eFunctions.Values.eTypeCode.eNumeric)
		Call .AddTextColumn(0, GetLocalResourceObject("tctAcc_NumberColumnCaption"), "tctAcc_Number", 20, "",  , GetLocalResourceObject("tctAcc_NumberColumnToolTip"),  ,  ,  , True)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("valConceptColumnCaption"), "valConcept", "table293", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , True, 4, GetLocalResourceObject("valConceptColumnToolTip"), eFunctions.Values.eTypeCode.eNumeric)
		Call .AddTextColumn(0, GetLocalResourceObject("tctDescriptionColumnCaption"), "tctDescription", 30, "",  , GetLocalResourceObject("tctDescriptionColumnToolTip"),  ,  ,  , True)
		Call .AddClientColumn(0, GetLocalResourceObject("tctBenefColumnCaption"), "tctBenef", "",  , GetLocalResourceObject("tctBenefColumnToolTip"),  ,  ,  , True)
		Call .AddClientColumn(0, GetLocalResourceObject("tctIntermedColumnCaption"), "tctIntermed", "",  , GetLocalResourceObject("tctIntermedColumnToolTip"),  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 18, "", False, GetLocalResourceObject("tcnAmountColumnToolTip"), True, 6,  ,  ,  , True)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCurrencyColumnCaption"), "cbeCurrency", "table11", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , True, 4, GetLocalResourceObject("cbeCurrencyColumnToolTip"), eFunctions.Values.eTypeCode.eNumeric)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnQpaysColumnCaption"), "tcnQpays", 0, "", False, GetLocalResourceObject("tcnQpaysColumnToolTip"), False,  ,  ,  ,  , True)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeFreqColumnCaption"), "cbeFreq", "table36", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , True, 4, GetLocalResourceObject("cbeFreqColumnToolTip"), eFunctions.Values.eTypeCode.eNumeric)
		Call .AddClientColumn(0, GetLocalResourceObject("tctUsersColumnCaption"), "tctUsers", "",  , GetLocalResourceObject("tctUsersColumnToolTip"),  ,  ,  , True)
		Call .AddHiddenColumn("hddRequest", "")
		Call .AddHiddenColumn("hddCheque", "")
		
	End With
	
	With mobjGrid
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Codispl = "OP008"
		.Codisp = "OP008"
		.Top = 100
		.Height = 230
		.Width = 400
		.AddButton = False
		.DeleteButton = False
		.ActionQuery = mobjValues.ActionQuery
		.bOnlyForQuery = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery)
		
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("Sel").Checked = 1
		
		If CStr(Session("optNull")) = "1" Or CStr(Session("optNull")) = "2" Then
			.Columns("Sel").Disabled = True
		Else
			.Columns("Sel").Disabled = False
		End If
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub
'------------------------------------------------------------------------------------------------------------------
Private Function insPreOP008() As Object
	'------------------------------------------------------------------------------------------------------------------
	Dim lnRequest_nu As Object
	Dim lnBordereaux As Object
	Dim lsCheque As Object
	Dim lnConsec As Object
	Dim lclsCheque As eCashBank.Cheque
	Dim lcolCheque As eCashBank.Cheques
	
	lclsCheque = New eCashBank.Cheque
	lcolCheque = New eCashBank.Cheques
	
	If CStr(Session("optNull")) = "1" Then
		lnRequest_nu = Session("gmnCheque")
		lsCheque = ""
		lnBordereaux = eRemoteDB.Constants.intNull
		lnConsec = 0
	ElseIf CStr(Session("optNull")) = "2" Then 
		lnRequest_nu = eRemoteDB.Constants.intNull
		lsCheque = Session("gmtCheque")
		lnBordereaux = eRemoteDB.Constants.intNull
		lnConsec = 0
	Else
		lnRequest_nu = eRemoteDB.Constants.intNull
		lsCheque = ""
		lnBordereaux = Session("gmnBordereaux")
		lnConsec = 0
	End If
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""6"" CLASS=""HighLighted""><LABEL ID=40069>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD WIDTH=""100%"" COLSPAN=""6""><HR></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=8669>" & GetLocalResourceObject("cboCauseCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cboCause", "table295", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cboCauseToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=8670>" & GetLocalResourceObject("gmdNullDateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DateControl("gmdNullDate", CStr(Today),  , GetLocalResourceObject("gmdNullDateToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD></TD>" & vbCrLf)
Response.Write("            <TD></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD WIDTH=""100%"" COLSPAN=""6""><HR></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>")

	
	
	If lcolCheque.FindOP008(lnRequest_nu, lsCheque, mobjValues.StringToType(lnConsec, eFunctions.Values.eTypeData.etdDouble), lnBordereaux) Then
		For	Each lclsCheque In lcolCheque
			With mobjGrid
				
				.Columns("tctRequest").DefValue = CStr(lclsCheque.nRequest_nu)
				.Columns("tctCheque").DefValue = lclsCheque.sCheque
				.Columns("cbeState").DefValue = CStr(lclsCheque.nSta_cheque)
				.Columns("tcnAccountNum").DefValue = CStr(lclsCheque.nAcc_bank)
				.Columns("cbeBank").DefValue = CStr(lclsCheque.nBank_code)
				.Columns("tctAcc_Number").DefValue = lclsCheque.sAcc_number
				.Columns("valConcept").DefValue = CStr(lclsCheque.nConcept)
				.Columns("tctDescription").DefValue = lclsCheque.sDescript
				.Columns("tctBenef").DefValue = lclsCheque.sClient
				.Columns("tctIntermed").DefValue = lclsCheque.sClientInter
				.Columns("tcnAmount").DefValue = CStr(lclsCheque.nAmount)
				.Columns("cbeCurrency").DefValue = CStr(lclsCheque.nBank_curr)
				.Columns("tcnQpays").DefValue = CStr(lclsCheque.nQ_pays)
				.Columns("cbeFreq").DefValue = lclsCheque.sPay_freq
				.Columns("tctUsers").DefValue = lclsCheque.sClientUser
				.Columns("hddRequest").DefValue = CStr(lclsCheque.nRequest_nu)
				.Columns("hddCheque").DefValue = lclsCheque.sCheque
				
				Response.Write(.DoRow)
			End With
		Next lclsCheque
		Response.Write(mobjGrid.CloseTable())
	End If
	
	lclsCheque = Nothing
	lcolCheque = Nothing
End Function

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "op008"
%>
<SCRIPT LANGUAGE="JavaScript">
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
}
//-------------------------------------------------------------------------------------------
function LoadFields(Field){
//-------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		if ((mintIntAccount != valIntAccount.value)){
		    self.document.location.href="/VTimeNet/CashBank/CashBank/OP012I.aspx?sCodispl=OP012I&mintIntAccount="+valIntAccount.value+"&sField=" + Field.name
		}
    }
}
  document.VssVersion="$$Revision: 1 $|$$Date: 11/02/04 17:25 $"
</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>






<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.setZone(2, "OP008", "OP008.aspx"))
If Request.QueryString.Item("nMainAction") = "401" Then
	mobjValues.ActionQuery = True
End If
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmTransBank" ACTION="ValCashBank.aspx?x=1&nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Call insDefineHeader()
Call insPreOP008()
mobjValues = Nothing
mobjGrid = Nothing
%>    
</FORM>
</BODY>
</HTML>    




