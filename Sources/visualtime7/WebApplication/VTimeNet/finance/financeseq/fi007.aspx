<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eFinance" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 3/4/03 11.58.23
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues

Dim mstrTypeFind As String
Dim mblnVisible As Boolean
Dim mblnDisabled As Object


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid.sCodisplPage = "fi007"
	
	'+ Se definen las columnas del grid
	
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCompanyColumnCaption"), "cbeCompany", "tabCompany_sType", 1,  , True,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCompanyColumnToolTip"))
		mobjGrid.Columns("cbeCompany").Parameters.Add("sType", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnReceiptColumnCaption"), "tcnReceipt", 10, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnReceiptColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPolicyColumnCaption"), "tcnPolicy", 10, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnPolicyColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnBranchColumnCaption"), "tcnBranch", 5, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnBranchColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeOfficeColumnCaption"), "cbeOffice", "table9", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeOfficeColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctClienameColumnCaption"), "tctCliename", 30, "",  , GetLocalResourceObject("tctClienameColumnToolTip"))
		Call .AddDateColumn(0, GetLocalResourceObject("tcdEffecdateColumnCaption"), "tcdEffecdate", CStr(eRemoteDB.Constants.dtmnull),  , GetLocalResourceObject("tcdEffecdateColumnToolTip"))
		Call .AddDateColumn(0, GetLocalResourceObject("tcdExpirdatColumnCaption"), "tcdExpirdat", CStr(eRemoteDB.Constants.dtmnull),  , GetLocalResourceObject("tcdExpirdatColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCurrencyColumnCaption"), "cbeCurrency", "table11", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  , "ShownCurr()",  ,  , GetLocalResourceObject("cbeCurrencyColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnExchangeColumnCaption"), "tcnExchange", 11, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnExchangeColumnToolTip"),  , 6)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("tcnIntermedColumnCaption"), "tcnIntermed", "Intermedia", 2,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("tcnIntermedColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCommissionColumnCaption"), "tcnCommission", 18, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnCommissionColumnToolTip"), True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPremiumColumnCaption"), "tcnPremium", 18, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnPremiumColumnToolTip"), True, 6)
		Call .AddHiddenColumn("sAuxSel", CStr(2))
		Call .AddHiddenColumn("tcnContrat", CStr(0))
		Call .AddHiddenColumn("tctClient", CStr(0))
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "FI007"
		.Codisp = "FI007"
		.Width = 400
		.Height = 520
		.DeleteButton = False
		.AddButton = False
		If session("bQuery") Or CStr(session("optType")) = "1" Then
			.Columns("Sel").GridVisible = False
			.bOnlyForQuery = True
		ElseIf mstrTypeFind = "2" Then 
			.Columns("Sel").Title = ""
		ElseIf mstrTypeFind = "1" Then 
			.Columns("Sel").Title = ""
			.Columns("tcnReceipt").EditRecord = True
		End If
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		.Columns("Sel").OnClick = "if(document.forms[0].sAuxSel.length>0)document.forms[0].sAuxSel[this.value].value =(this.checked?1:2); else document.forms[0].sAuxSel.value =(this.checked?1:2);"
	End With
	
Response.Write("" & vbCrLf)
Response.Write("")

	
End Sub

'% insPreFI007: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreFI007()
	'--------------------------------------------------------------------------------------------
	Dim lclsFinanPre As eFinance.FinancePre
	Dim lcolFinanPre As eFinance.FinancePres
	
	lclsFinanPre = New eFinance.FinancePre
	
	lcolFinanPre = New eFinance.FinancePres
	
	mobjGrid.AddButton = True
	
	If lcolFinanPre.Find(mobjValues.StringToType(session("nContrat"), eFunctions.Values.eTypeData.etdDouble)) Then
		mobjGrid.DeleteButton = True
		For	Each lclsFinanPre In lcolFinanPre
			With mobjGrid
				.Columns("cbeCompany").DefValue = CStr(lclsFinanPre.nCompany)
				.Columns("tcnBranch").DefValue = CStr(lclsFinanPre.nBranch)
				.Columns("tcnReceipt").DefValue = CStr(lclsFinanPre.nReceipt)
				.Columns("tcnPolicy").DefValue = CStr(lclsFinanPre.nPolicy)
				.Columns("cbeOffice").DefValue = CStr(lclsFinanPre.nOffice)
				.Columns("tctCliename").DefValue = lclsFinanPre.scliename
				.Columns("tcdEffecdate").DefValue = CStr(lclsFinanPre.dStartDate)
				.Columns("tcdExpirdat").DefValue = CStr(lclsFinanPre.dExpirdat)
				.Columns("cbeCurrency").DefValue = CStr(lclsFinanPre.nCurrency)
				.Columns("tcnExchange").DefValue = CStr(lclsFinanPre.nExchange)
				.Columns("tcnIntermed").DefValue = CStr(lclsFinanPre.nIntermed)
				.Columns("tcnCommission").DefValue = CStr(lclsFinanPre.nCommission)
				.Columns("tcnPremium").DefValue = CStr(lclsFinanPre.nPremium)
				.Columns("tcnContrat").DefValue = CStr(lclsFinanPre.nContrat)
				.Columns("tctClient").DefValue = lclsFinanPre.sclient
				
				.sDelRecordParam = "nReceipt=' + marrArray[lintIndex].tcnReceipt + '&nContrat=' + marrArray[lintIndex].tcnContrat + '"
				Response.Write(.DoRow)
			End With
		Next lclsFinanPre
	Else
		mblnVisible = True
	End If
	
	Response.Write(mobjGrid.closeTable())
	lclsFinanPre = Nothing
	lcolFinanPre = Nothing
End Sub

'% insPreFI007Upd: Se muetra la ventana Popup para efecto de actualización del Gird
'--------------------------------------------------------------------------------------------
Private Sub insPreFI007Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsFinanPre As eFinance.FinancePre
	
	lclsFinanPre = New eFinance.FinancePre
	
	'+ Se elimina el registro marcado
	If Request.QueryString.Item("Action") = "Del" Then
		Response.Write(mobjValues.ConfirmDelete)
		Call lclsFinanPre.insPostFI007(Request.QueryString.Item("Action"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, vbNullString, mobjValues.StringToType(Request.QueryString.Item("nReceipt"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.intNull, mobjValues.StringToType(Request.QueryString.Item("nContrat"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull)
	End If
	'+ Se muestra la ventana PopUp para modificar o actualizar	
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valFinanceseq.aspx", "FI007", Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
	If Request.QueryString.Item("Action") = "Update" Then
		Response.Write("<SCRIPT>DisabledItems();</" & "Script>")
	ElseIf Request.QueryString.Item("Action") = "Add" Then 
		Response.Write("<SCRIPT>ShowDefVal();</" & "Script>")
	End If
	lclsFinanPre = Nothing
End Sub

</script>
<%Response.Expires = -1
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = session.SessionID
mobjNetFrameWork.nUsercode = session("nUsercode")
Call mobjNetFrameWork.BeginPage(Request.QueryString.Item("sCodispl"))

mobjValues = New eFunctions.Values
mobjNetFrameWork.sSessionID = session.SessionID
mobjNetFrameWork.nUsercode = session("nUsercode")
Call mobjNetFrameWork.BeginPage(Request.QueryString.Item("sCodispl"))

mobjValues = New eFunctions.Values
mobjGrid = New eFunctions.Grid
mobjMenu = New eFunctions.Menues
If IsNothing(Request.QueryString.Item("sTypeFind")) Then
	mstrTypeFind = "1"
Else
	mstrTypeFind = "2"
End If
mobjValues.ActionQuery = session("bQuery")

mobjValues.sCodisplPage = "fi007"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 4 $|$$Date: 9/09/04 19:23 $|$$Author: Nvaplat40 $"
</SCRIPT>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">




    <%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("FI007"))
	.Write(mobjValues.ShowWindowsName("FI007"))
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		mobjNetFrameWork.sSessionID = session.SessionID
		mobjNetFrameWork.nUsercode = session("nUsercode")
		Call mobjNetFrameWork.BeginPage(Request.QueryString.Item("sCodispl"))
		.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
		.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), "FI007.aspx"))
		mobjMenu = Nothing
	End If
End With

%>
<SCRIPT LANGUAGE="JAVASCRIPT">
function ShowDefVal()
{	
	ShowPopUp("/VTimeNet/Finance/FinanceSeq/ShowDefValues.aspx?Field=" + "AddFI007" +  "&nCurrency=" + self.document.forms[0].cbeCurrency.value, "ShowDefValuesFinance" , 1, 1,"no","no",2000,2000);
}
function ShownCurr()
{
	ShowPopUp("/VTimeNet/Finance/FinanceSeq/ShowDefValues.aspx?Field=" + "nCurrency" +  "&nCurrency=" + self.document.forms[0].cbeCurrency.value , "ShowDefValuesFinance" , 1, 1,"no","no",2000,2000);
}
function ChangeValues()
{
	with (self.document.forms[0])
	{	
		tctCliename.disabled = true;
		tcnExchange.disabled = true;
	}
}
function DisabledItems()
{
	with (self.document.forms[0])
	{	tcnReceipt.disabled = true;
		tctCliename.disabled = true;
		tcnExchange.disabled = true;
	}
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmFI007" ACTION="valFinanceSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
 <%
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreFI007Upd()
Else
	Call insPreFI007()
End If
%>

</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjGrid = Nothing
%>





