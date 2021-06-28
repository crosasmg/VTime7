<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eFinance" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 3/4/03 11.58.23
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	mobjNetFrameWork.sSessionID = Session.SessionID
	mobjNetFrameWork.nUsercode = Session("nUsercode")
	Call mobjNetFrameWork.BeginPage(Request.QueryString.Item("sCodispl"))
	
	mobjGrid.sCodisplPage = "fi004"
	
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
		.AddNumericColumn(0, GetLocalResourceObject("tcnDraftColumnCaption"), "tcnDraft", 2, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnDraftColumnToolTip"),  ,  ,  ,  ,  , True)
		.AddDateColumn(0, GetLocalResourceObject("tcdExpirdatColumnCaption"), "tcdExpirdat", CStr(eRemoteDB.Constants.dtmnull),  , GetLocalResourceObject("tcdExpirdatColumnToolTip"),  ,  ,  , True)
		.AddNumericColumn(0, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 18, CStr(0),  , GetLocalResourceObject("tcnAmountColumnToolTip"), True, 6,  ,  ,  , True)
		.AddNumericColumn(0, GetLocalResourceObject("tcnCommissionColumnCaption"), "tcnCommission", 18, CStr(0),  , GetLocalResourceObject("tcnCommissionColumnToolTip"), True, 6,  ,  ,  , True)
		.AddNumericColumn(0, GetLocalResourceObject("tcnBalanceColumnCaption"), "tcnBalance", 18, CStr(0),  , GetLocalResourceObject("tcnBalanceColumnToolTip"), True, 6,  ,  ,  , True)
		.AddNumericColumn(0, GetLocalResourceObject("tcnAmount_NetColumnCaption"), "tcnAmount_Net", 18, CStr(0),  , GetLocalResourceObject("tcnAmount_NetColumnToolTip"), True, 6,  ,  ,  , True)
		.AddPossiblesColumn(0, GetLocalResourceObject("cbeStat_draftColumnCaption"), "cbeStat_draft", "Table253", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeStat_draftColumnToolTip"))
		.AddDateColumn(0, GetLocalResourceObject("tcdStat_dateColumnCaption"), "tcdStat_date", CStr(eRemoteDB.Constants.dtmnull),  , GetLocalResourceObject("tcdStat_dateColumnToolTip"),  ,  ,  , True)
		.AddPossiblesColumn(0, GetLocalResourceObject("cbeWay_payColumnCaption"), "cbeWay_pay", "Table5002", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeWay_payColumnToolTip"))
		'+Como grid es de solo lectura, los campoa anteriores se crean como etiquetas
		'+Luego, para pasar valores a página valfinanceseq.aspx, se crean estos campos
		.AddHiddenColumn("tcnAuxDraft", CStr(eRemoteDB.Constants.intNull))
		.AddHiddenColumn("tcnAuxAmount", CStr(eRemoteDB.Constants.intNull))
		.AddHiddenColumn("tcnAuxAmount_Net", CStr(eRemoteDB.Constants.intNull))
		.AddHiddenColumn("tcnAuxCommission", CStr(eRemoteDB.Constants.intNull))
		.AddHiddenColumn("tcnAuxCom_afec", CStr(eRemoteDB.Constants.intNull))
		.AddHiddenColumn("tcnAuxCom_exen", CStr(eRemoteDB.Constants.intNull))
		.AddHiddenColumn("tcnStat_draft", CStr(eRemoteDB.Constants.intNull))
		.AddHiddenColumn("tcdAuxExpirdat", CStr(eRemoteDB.Constants.dtmnull))
		.AddHiddenColumn("tcdAuxLimitdat", CStr(eRemoteDB.Constants.dtmnull))
		.AddHiddenColumn("tcnIndicator", CStr(eRemoteDB.Constants.intNull))
		.AddHiddenColumn("tcnTotalAmount", CStr(0))
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = Request.QueryString.Item("sCodispl")
		.Top = 150
		.Height = 350
		.AddButton = False
		.DeleteButton = False
		.Columns("Sel").GridVisible = False
	End With
End Sub

'%insPreFI004: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreFI004()
	Dim lclsFinanceDraft As Object
	'--------------------------------------------------------------------------------------------
	Dim lcolFinancePres As eFinance.FinancePres
	Dim lcolRefinanDraft As eFinance.RefinanceDrafts
	Dim lstrAlert As String
	Dim lobjErrors As eGeneral.GeneralFunction
	Dim lblnFoundReg As Boolean
	Dim ldblTotal As Object
	Dim ldblTotalNet As Object
	
	
	lblnFoundReg = False
	
	'+ se verfica si existen registros en la FI002
	lcolFinancePres = New eFinance.FinancePres
	If lcolFinancePres.Find_DataReceipt(mobjValues.StringToType(Session("nContrat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), True) Then
		lblnFoundReg = True
	End If
	lcolFinancePres = Nothing
	
	'+ se verfica si existen registros en la FI003        
	lcolRefinanDraft = New eFinance.RefinanceDrafts
	If lcolRefinanDraft.Find(mobjValues.StringToType(Session("nContrat"), eFunctions.Values.eTypeData.etdDouble), True, True) Then
		lblnFoundReg = True
	End If
	lcolRefinanDraft = Nothing
	
	Dim lcolFinanceDrafts As eFinance.FinanceDrafts
	If lblnFoundReg Then
		
		lcolFinanceDrafts = New eFinance.FinanceDrafts
		
		If lcolFinanceDrafts.Find_Contrat(Session("nTransaction"), Session("nContrat"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
			
			ldblTotal = 0
			ldblTotalNet = 0
			For	Each lclsFinanceDraft In lcolFinanceDrafts
				With mobjGrid
					.Columns("tcnDraft").DefValue = lclsFinanceDraft.nDraft
					.Columns("tcdExpirdat").DefValue = lclsFinanceDraft.dLimitdate
					.Columns("tcnAmount").DefValue = lclsFinanceDraft.nAmount
					.Columns("tcnCommission").DefValue = lclsFinanceDraft.nCommission
					.Columns("tcnBalance").DefValue = lclsFinanceDraft.nBalance
					.Columns("tcnAmount_net").DefValue = lclsFinanceDraft.nAmount_net
					.Columns("cbeStat_draft").DefValue = lclsFinanceDraft.nStat_draft
					.Columns("cbeStat_draft").Descript = lclsFinanceDraft.sStat_draft
					.Columns("tcdStat_date").DefValue = lclsFinanceDraft.dStat_date
					.Columns("cbeWay_pay").DefValue = lclsFinanceDraft.nWay_pay
					.Columns("cbeWay_pay").Descript = lclsFinanceDraft.sDesWay_pay
					.Columns("tcnAuxDraft").DefValue = lclsFinanceDraft.nDraft
					.Columns("tcnAuxAmount").DefValue = lclsFinanceDraft.nAmount
					.Columns("tcnAuxAmount_Net").DefValue = lclsFinanceDraft.nAmount_net
					.Columns("tcnAuxCommission").DefValue = lclsFinanceDraft.nCommission
					.Columns("tcnAuxCom_afec").DefValue = lclsFinanceDraft.nCom_afec
					.Columns("tcnAuxCom_exen").DefValue = lclsFinanceDraft.nCom_exen
					.Columns("tcnStat_draft").DefValue = lclsFinanceDraft.nStat_draft
					.Columns("tcdAuxExpirdat").DefValue = lclsFinanceDraft.dExpirdat
					.Columns("tcdAuxLimitdat").DefValue = lclsFinanceDraft.dLimitdate
					.Columns("tcnIndicator").DefValue = lclsFinanceDraft.nStatInstanc
					ldblTotal = ldblTotal + lclsFinanceDraft.nAmount
					ldblTotalNet = ldblTotalNet + lclsFinanceDraft.nAmount_net
					Response.Write(.DoRow)
				End With
			Next lclsFinanceDraft
		Else
			mobjGrid.AddButton = False
		End If
		lclsFinanceDraft = Nothing
		lcolFinanceDrafts = Nothing
	Else
		lobjErrors = New eGeneral.GeneralFunction
		lstrAlert = "Err. 56038 " & lobjErrors.insLoadMessage(56038)
		Response.Write("<SCRIPT>alert('" & lstrAlert & "')</" & "Script>")
	End If
	
	Response.Write(mobjGrid.closeTable())
	Response.Write("<BR>")
	Response.Write("<TABLE>")
	Response.Write("    <TR><TD><LABEL>" & GetLocalResourceObject("AnchorCaption") & "&nbsp;&nbsp;</LABEL></TD><TD>" & mobjValues.NumericControl("tcnAmountNetCO", 18, ldblTotalNet, False, "Monto a financiar en contrato", True, 6, True) & "</TD></TR>")
	Response.Write("    <TR><TD><LABEL>" & GetLocalResourceObject("Anchor2Caption") & "&nbsp;&nbsp;</LABEL></TD><TD>" & mobjValues.NumericControl("tcnAmountNetCO", 18, ldblTotal - ldblTotalNet, False, "Monto a financiar en contrato", True, 6, True) & "</TD></TR>")
	Response.Write("    <TR><TD><LABEL>" & GetLocalResourceObject("Anchor3Caption") & "&nbsp;&nbsp;</LABEL></TD><TD>" & mobjValues.NumericControl("tcnAmountCO", 18, ldblTotal, False, "Monto a financiar en contrato", True, 6, True) & "</TD></TR>")
	Response.Write("</TABLE>")
	
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage(Request.QueryString.Item("sCodispl"))

mobjValues = New eFunctions.Values
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage(Request.QueryString.Item("sCodispl"))

mobjValues.ActionQuery = Session("bQuery")

mobjValues.sCodisplPage = "fi004"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 11 $|$$Date: 4/08/04 10:06 $|$$Author: Nvaplat9 $"
</SCRIPT>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">




    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("FI004"))
	.Write(mobjValues.ShowWindowsName("FI004"))
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		mobjNetFrameWork.sSessionID = Session.SessionID
		mobjNetFrameWork.nUsercode = Session("nUsercode")
		Call mobjNetFrameWork.BeginPage(Request.QueryString.Item("sCodispl"))
		.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
		.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), "FI004.aspx"))
		mobjMenu = Nothing
	End If
End With%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmFI004" ACTION="valFinanceSeq.aspx?mode=2">
    <TABLE WIDTH="100%">
<%
Call insDefineHeader()
Call insPreFI004()
%>
	<TABLE WIDTH="100%">
    </TABLE>
</FORM>
</HTML>
<%
mobjValues = Nothing
mobjGrid = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 3/4/03 11.58.23
Call mobjNetFrameWork.FinishPage(Request.QueryString.Item("sCodispl"))
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer
%>





