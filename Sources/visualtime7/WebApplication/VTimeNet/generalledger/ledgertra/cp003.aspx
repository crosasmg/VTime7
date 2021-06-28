<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eLedge" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues
Dim lclsBal_Histor As eLedge.Bal_histor
Dim lclsLedger As eLedge.Ledger


'- Declaraciòn de Variables para la recarga y bùsqueda
Dim mstrBussityp As Object

'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	'+ Se definen las columnas del grid
	
	mobjGrid.sCodisplPage = "CP003"
	
	With mobjGrid.Columns
		Call .AddTextColumn(0, GetLocalResourceObject("tctPerColumnCaption"), "tctPer", 4, "")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnYearColumnCaption"), "tcnYear", 4, CStr(0))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnDebColumnCaption"), "tcnDeb", 18, CStr(0),  ,  , True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCredColumnCaption"), "tcnCred", 18, CStr(0),  ,  , True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnBalanceColumnCaption"), "tcnBalance", 18, CStr(0),  ,  , True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnBal_CompColumnCaption"), "tcnBal_Comp", 16, CStr(0),  ,  , True, 6,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnDiferenceColumnCaption"), "tcnDiference", 18, CStr(0),  ,  , True, 6,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPorcColumnCaption"), "tcnPorc", 3, CStr(0),  ,  , True,  ,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnOrg_DebColumnCaption"), "tcnOrg_Deb", 18, CStr(0),  ,  , True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnOrg_CredColumnCaption"), "tcnOrg_Cred", 18, CStr(0),  ,  , True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnOrg_BalColumnCaption"), "tcnOrg_Bal", 18, CStr(0),  ,  , True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnIndColumnCaption"), "tcnInd", 4, CStr(0))
		'*********************************************************************		
		Call .AddHiddenColumn("tctAuxPer", CStr(0))
		Call .AddHiddenColumn("tcnAuxYear", CStr(0))
		Call .AddHiddenColumn("tcnAuxDeb", CStr(0))
		Call .AddHiddenColumn("tcnAuxCred", CStr(0))
		Call .AddHiddenColumn("tcnAuxBalance", CStr(0))
		Call .AddHiddenColumn("tcnAuxBal_Comp", CStr(0))
		Call .AddHiddenColumn("tcnAuxDiference", CStr(0))
		Call .AddHiddenColumn("tcnAuxPorc", CStr(0))
		Call .AddHiddenColumn("tcnAuxOrg_Deb", CStr(0))
		Call .AddHiddenColumn("tcnAuxOrg_Cred", CStr(0))
		Call .AddHiddenColumn("tcnAuxOrg_Bal", CStr(0))
		Call .AddHiddenColumn("tcnAuxInd", CStr(0))
		Call .AddHiddenColumn("sAuxSel", CStr(0))
		
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		If Request.QueryString.Item("Action") = "Update" Then
			.Columns("Sel").GridVisible = False
		End If
		.Columns("Sel").GridVisible = False
		.Codispl = "CP003"
		.Width = 450
		.Height = 450
		.DeleteButton = False
		.AddButton = False
		If Session("bQuery") Then
			.Columns("Sel").GridVisible = False
			.bOnlyForQuery = True
		End If
		.Columns("tctPer").EditRecord = True
		.Columns("tcnYear").EditRecord = True
		.Columns("tcnDeb").EditRecord = True
		.Columns("tcnCred").EditRecord = True
		.DeleteScriptName = vbNullString
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		.Columns("Sel").OnClick = "if(document.forms[0].sAuxSel.length>0)document.forms[0].sAuxSel[this.value].value =(this.checked?1:2); else document.forms[0].sAuxSel.value =(this.checked?1:2);insSelected(this);"
	End With
	
End Sub

'% insPreCP003: Se cargan los controles de la página, tanto de la parte fija como del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCP003()
	'--------------------------------------------------------------------------------------------
	Dim lclsBal_Histor As eLedge.Bal_histor
	Dim lcolBal_Histors As eLedge.Bal_Histors
	Dim ldblbal_comp As Object
	Dim ldblDiference As Object
	Dim ldblPorc As String
	Dim lintIndex As Short
	
	With Server
		lclsBal_Histor = New eLedge.Bal_histor
		lcolBal_Histors = New eLedge.Bal_Histors
	End With
	If lcolBal_Histors.Find(mobjValues.StringToType(Session("nLedCompan"), eFunctions.Values.eTypeData.etdDouble), Session("sAccount"), Session("sAux_Account"), Session("sCost_Cente"), mobjValues.StringToType(Session("nLed_Year"), eFunctions.Values.eTypeData.etdDouble)) Then
		lintIndex = 0
		For	Each lclsBal_Histor In lcolBal_Histors
			With mobjGrid
				.Columns("tctPer").DefValue = CStr(lclsBal_Histor.nMonth)
				.Columns("tcnYear").DefValue = mobjValues.StringToType(CStr(lclsBal_Histor.nYear), eFunctions.Values.eTypeData.etdDouble)
				.Columns("tcnDeb").DefValue = mobjValues.StringToType(CStr(lclsBal_Histor.nDebit), eFunctions.Values.eTypeData.etdDouble)
				.Columns("tcnCred").DefValue = mobjValues.StringToType(CStr(lclsBal_Histor.nCredit), eFunctions.Values.eTypeData.etdDouble)
				.Columns("tcnBalance").DefValue = mobjValues.StringToType(CStr(lclsBal_Histor.nBalance), eFunctions.Values.eTypeData.etdDouble)
				
				ldblbal_comp = lclsBal_Histor.insCalc_PrevBal(Session("optStyle0"), Session("optSel0"), Session("optSel1"), Session("optType0"), Session("optType1"), Session("optType2"), lclsBal_Histor.nMonth, Session("sAccount"), Session("sAux"), Session("sUnit"), mobjValues.StringToType(CStr(lclsBal_Histor.nYear), eFunctions.Values.eTypeData.etdDouble))
				
				
				.Columns("tcnBal_Comp").DefValue = mobjValues.StringToType(ldblbal_comp, eFunctions.Values.eTypeData.etdDouble)
				
				ldblDiference = lclsBal_Histor.insCalc_Diference(lclsBal_Histor.nBalance, ldblbal_comp)
				
				.Columns("tcnDiference").DefValue = mobjValues.StringToType(ldblDiference, eFunctions.Values.eTypeData.etdDouble)
				
				ldblPorc = lclsBal_Histor.insCalc_Porc(lclsBal_Histor.nBalance, ldblDiference)
				
				.Columns("tcnPorc").DefValue = mobjValues.StringToType(ldblPorc, eFunctions.Values.eTypeData.etdDouble)
				
				.Columns("tcnOrg_Deb").DefValue = mobjValues.StringToType(CStr(lclsBal_Histor.nDebit), eFunctions.Values.eTypeData.etdDouble)
				.Columns("tcnOrg_Cred").DefValue = mobjValues.StringToType(CStr(lclsBal_Histor.nCredit), eFunctions.Values.eTypeData.etdDouble)
				.Columns("tcnOrg_Bal").DefValue = mobjValues.StringToType(CStr(lclsBal_Histor.nBalance), eFunctions.Values.eTypeData.etdDouble)
				.Columns("tcnInd").DefValue = mobjValues.StringToType(CStr(lclsBal_Histor.nInd_automa), eFunctions.Values.eTypeData.etdDouble)
				'*******************************************************************************************************************
				.Columns("tctAuxPer").DefValue = CStr(lclsBal_Histor.nMonth)
				.Columns("tcnAuxYear").DefValue = mobjValues.StringToType(CStr(lclsBal_Histor.nYear), eFunctions.Values.eTypeData.etdDouble)
				.Columns("tcnAuxDeb").DefValue = mobjValues.StringToType(CStr(lclsBal_Histor.nDebit), eFunctions.Values.eTypeData.etdDouble)
				.Columns("tcnAuxCred").DefValue = mobjValues.StringToType(CStr(lclsBal_Histor.nCredit), eFunctions.Values.eTypeData.etdDouble)
				.Columns("tcnAuxBalance").DefValue = mobjValues.StringToType(CStr(lclsBal_Histor.nBalance), eFunctions.Values.eTypeData.etdDouble)
				.Columns("tcnAuxBal_Comp").DefValue = mobjValues.StringToType(ldblbal_comp, eFunctions.Values.eTypeData.etdDouble)
				.Columns("tcnAuxDiference").DefValue = mobjValues.StringToType(ldblDiference, eFunctions.Values.eTypeData.etdDouble)
				.Columns("tcnAuxPorc").DefValue = mobjValues.StringToType(ldblPorc, eFunctions.Values.eTypeData.etdDouble)
				.Columns("tcnAuxOrg_Deb").DefValue = mobjValues.StringToType(CStr(lclsBal_Histor.nDebit), eFunctions.Values.eTypeData.etdDouble)
				.Columns("tcnAuxOrg_Cred").DefValue = mobjValues.StringToType(CStr(lclsBal_Histor.nCredit), eFunctions.Values.eTypeData.etdDouble)
				.Columns("tcnAuxOrg_Bal").DefValue = mobjValues.StringToType(CStr(lclsBal_Histor.nBalance), eFunctions.Values.eTypeData.etdDouble)
				.Columns("tcnAuxInd").DefValue = mobjValues.StringToType(CStr(lclsBal_Histor.nInd_automa), eFunctions.Values.eTypeData.etdDouble)
				Response.Write(.DoRow)
				lintIndex = lintIndex + 1
			End With
		Next lclsBal_Histor
		
	End If
	
	Response.Write(mobjGrid.closeTable())
	Response.Write("<SCRIPT>top.fraHeader.document.forms[0].valAccount.value='" & Session("sAccount") & "';</" & "Script>")
	Response.Write("<SCRIPT>top.fraHeader.document.forms[0].valAux.value='" & Session("sAux_Account") & "';</" & "Script>")
	Response.Write("<SCRIPT>top.fraHeader.document.forms[0].cboCompare.value='" & Session("cboCompare") & "';</" & "Script>")
	Response.Write("<SCRIPT>top.fraHeader.document.forms[0].valUnit.value='" & Session("sCost_Cente") & "';</" & "Script>")
	Response.Write("<SCRIPT>top.fraHeader.document.forms[0].tcnLedger_Year.value='" & Session("nLed_Year") & "';</" & "Script>")
	
	
	lclsBal_Histor = Nothing
	lcolBal_Histors = Nothing
End Sub

'% insPreCP003Upd: Se muetra la ventana Popup para efecto de actualización del Gird
'--------------------------------------------------------------------------------------------
Private Sub insPreCP003Upd()
	'--------------------------------------------------------------------------------------------
	With Request
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valLedGerTra.aspx", "CP003", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
		'Response.Write mobjValues.HiddenControl("mstPolitype",.QueryString("mstrPolitype"))
		'Response.Write mobjValues.HiddenControl("mstrCompon",.QueryString("mstrCompon"))
	End With
	
	
End Sub

</script>
<%
Response.Expires = -1


With Server
	mobjValues = New eFunctions.Values
	mobjGrid = New eFunctions.Grid
	mobjMenu = New eFunctions.Menues
	lclsBal_Histor = New eLedge.Bal_histor
	lclsLedger = New eLedge.Ledger
End With

mobjValues.sCodisplPage = "CP003"

If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
	mobjValues.ActionQuery = True
End If
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">




    <%With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
		.Write(mobjMenu.setZone(2, "CP003", "CP003.aspx"))
		mobjMenu = Nothing
	End If
End With%>
<SCRIPT LANGUAGE="JavaScript">
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%Response.Write(mobjValues.ShowWindowsName("CP003"))%>
<FORM METHOD="POST" ID="FORM" NAME="frmHisBalance" ACTION="ValLedGerTra.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
    <TABLE WIDTH="100%">
        <%
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreCP003()
Else
	Call insPreCP003Upd()
End If

%>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjGrid = Nothing
%>




