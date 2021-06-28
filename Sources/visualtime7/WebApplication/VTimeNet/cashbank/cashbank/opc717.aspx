<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCashBank" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues


'----------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'----------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "opc717"
	
	With mobjGrid.Columns
		Call .AddTextColumn(0, GetLocalResourceObject("sChequeColumnCaption"), "sCheque", 12, "",  , GetLocalResourceObject("sChequeColumnToolTip"),  ,  ,  , True)
		Call .AddDateColumn(0, GetLocalResourceObject("dDoc_dateColumnCaption"), "dDoc_date",  ,  , GetLocalResourceObject("dDoc_dateColumnToolTip"),  ,  ,  , True)
		Call .AddTextColumn(0, GetLocalResourceObject("tctCheque_statColumnCaption"), "tctCheque_stat", 30, CStr(0),  , GetLocalResourceObject("tctCheque_statColumnToolTip"),  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("nAmountColumnCaption"), "nAmount", 19, CStr(0),  , GetLocalResourceObject("nAmountColumnToolTip"), True, 6,  ,  ,  , True)
		Call .AddTextColumn(0, GetLocalResourceObject("tctBankColumnCaption"), "tctBank", 30, CStr(0),  , GetLocalResourceObject("tctBankColumnToolTip"),  ,  ,  , True)
		Call .AddTextColumn(0, GetLocalResourceObject("tctChequeLocatColumnCaption"), "tctChequeLocat", 30, CStr(0),  , GetLocalResourceObject("tctChequeLocatColumnToolTip"),  ,  ,  , True)
		Call .AddTextColumn(0, GetLocalResourceObject("tctCard_TypeColumnCaption"), "tctCard_Type", 30, CStr(0),  , GetLocalResourceObject("tctCard_TypeColumnToolTip"),  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("nBordereauxColumnCaption"), "nBordereaux", 10, CStr(0),  , GetLocalResourceObject("nBordereauxColumnToolTip"),  ,  ,  ,  ,  , True)
		Call .AddTextColumn(0, GetLocalResourceObject("tctConceptColumnCaption"), "tctConcept", 30, CStr(0),  , GetLocalResourceObject("tctConceptColumnToolTip"),  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("nCash_idColumnCaption"), "nCash_id", 10, CStr(0),  , GetLocalResourceObject("nCash_idColumnToolTip"),  ,  ,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("nCashNumColumnCaption"), "nCashNum", 5, CStr(0),  , GetLocalResourceObject("nCashNumColumnToolTip"),  ,  ,  ,  ,  , True)
		Call .AddTextColumn(0, GetLocalResourceObject("tctOfficeAgenColumnCaption"), "tctOfficeAgen", 30, CStr(0),  , GetLocalResourceObject("tctOfficeAgenColumnToolTip"),  ,  ,  , True)
		Call .AddDateColumn(0, GetLocalResourceObject("dEffecdateColumnCaption"), "dEffecdate",  ,  , GetLocalResourceObject("dEffecdateColumnToolTip"),  ,  ,  , True)
		Call .AddDateColumn(0, GetLocalResourceObject("dRealDateColumnCaption"), "dRealDate",  ,  , GetLocalResourceObject("dRealDateColumnToolTip"),  ,  ,  , True)
	End With
	With mobjGrid
		.Codispl = "OPC717"
		.ActionQuery = True
		.Columns("Sel").GridVisible = False
	End With
End Sub

'----------------------------------------------------------------------------------------------
Private Sub insPreOPC717()
	'----------------------------------------------------------------------------------------------
	Dim lcolCash_movs As eCashBank.Cash_movs
	Dim lintIndex As Integer
	
	lcolCash_movs = New eCashBank.Cash_movs
	
	If lcolCash_movs.insPreOPC717(Session("dStartDate"), Session("dEndDate"), Session("nCurrency"), Session("nBank"), Session("nChequeLocat"), Session("nCheque_stat"), Session("sDocnumbe"), Session("sTypeInfo"), Session("nCard_Type"), Session("sSupervisor")) Then
		For lintIndex = 1 To lcolCash_movs.Count
			With mobjGrid
				.Columns("sCheque").DefValue = lcolCash_movs.Item(lintIndex).sDocnumbe
				.Columns("nAmount").DefValue = CStr(lcolCash_movs.Item(lintIndex).nAmount)
				.Columns("nBordereaux").DefValue = CStr(lcolCash_movs.Item(lintIndex).nBordereaux)
				.Columns("nCashNum").DefValue = CStr(lcolCash_movs.Item(lintIndex).nCase_Num)
				.Columns("dDoc_date").DefValue = CStr(lcolCash_movs.Item(lintIndex).dDoc_date)
				.Columns("nCash_id").DefValue = CStr(lcolCash_movs.Item(lintIndex).nCash_id)
				.Columns("dEffecdate").DefValue = CStr(lcolCash_movs.Item(lintIndex).dEffecdate)
				.Columns("tctBank").DefValue = lcolCash_movs.Item(lintIndex).sDes_Bank
				.Columns("tctCard_Type").DefValue = lcolCash_movs.Item(lintIndex).sDesCard_type
				.Columns("tctChequeLocat").DefValue = lcolCash_movs.Item(lintIndex).sDes_Cheloc
				.Columns("tctCheque_stat").DefValue = lcolCash_movs.Item(lintIndex).sDes_Chestat
				.Columns("tctConcept").DefValue = lcolCash_movs.Item(lintIndex).sDes_Concep
				.Columns("tctOfficeAgen").DefValue = lcolCash_movs.Item(lintIndex).sDes_Office
				.Columns("dRealDate").DefValue = CStr(lcolCash_movs.Item(lintIndex).dRealDep)
			End With
			Response.Write(mobjGrid.DoRow())
		Next 
	End If
	Response.Write(mobjGrid.closeTable() & mobjValues.BeginPageButton)
	mobjGrid = Nothing
	lcolCash_movs = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "opc717"
%>
<HTML>
    <%="<SCRIPT>nMainAction='" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>"%>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">




    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.setZone(2, "OPC717", "OPC717.aspx"))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<SCRIPT>
//+ Esta línea guarda la versión procedente de VSS 
    document.VssVersion="$$Revision: 3 $|$$Date: 23/03/04 17:45 $|$$Author: Nvaplat53 $"	
</SCRIPT>

<FORM METHOD="post" ID="FORM" NAME="frmCheque" ACTION="valCashBank.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Call insDefineHeader()
Response.Write(mobjValues.ShowWindowsName("OPC717"))
%>
<BR>
<%
Call insPreOPC717()
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>




