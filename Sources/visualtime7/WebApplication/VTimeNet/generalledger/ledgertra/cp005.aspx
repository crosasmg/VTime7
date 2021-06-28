<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eLedge" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues
Dim lclsAcc_lines As eLedge.Acc_lines
Dim lclsAcc_transa As eLedge.Acc_transa
Dim lclsLedger As eLedge.Ledger
Dim mintLine As Object


'% insLoadCP005: Dibuja los campos no repetitivos de la pantalla, con sus respectivos
' valores segùn sea el caso.
'------------------------------------------------------------------------------------------
Private Sub insLoadCP005()
	'------------------------------------------------------------------------------------------
	If lclsAcc_transa.find(mobjValues.StringToType(Session("nLedCompan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nVoucher"), eFunctions.Values.eTypeData.etdDouble)) Then
		
	End If
	
	
Response.Write("" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"" COLS=3 WIDTH=""35%"">" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("		<TD WIDTH=""35%"" ><LABEL ID=11627>" & GetLocalResourceObject("tctDescriptCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD WIDTH=""35%"" >")


Response.Write(mobjValues.TextControl("tctDescript", 30, lclsAcc_lines.sDescript,  , GetLocalResourceObject("tctDescriptToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		<TD WIDTH=""35%"" >")


Response.Write(mobjValues.ButtonNotes("SCA2-O", lclsAcc_lines.nNoteNum, True, Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery)))


Response.Write("</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	</TABLE>" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"" COLS=3 WIDTH=""35%"">" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD WIDTH=""35%"" >&nbsp</TD>" & vbCrLf)
Response.Write("		<TD WIDTH=""5%"" >")


Response.Write(mobjValues.AnimatedButtonControl("cmdRever", "/VTimeNet/images/wo_sel.bmp", GetLocalResourceObject("cmdReverToolTip"),  , "insLoadPostCut();"))


Response.Write("</TD>" & vbCrLf)
Response.Write("		<TD WIDTH=""5%"" >")


Response.Write(mobjValues.AnimatedButtonControl("cmdCut", "/VTimeNet/images/RevVoucherInside.bmp", GetLocalResourceObject("cmdCutToolTip"),  , "insLoadPostReverse();"))


Response.Write("</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	</TABLE>")

	
End Sub

'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	
	'+ Se definen las columnas del grid
	
	mobjGrid.sCodisplPage = "CP005"
	
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnLineColumnCaption"), "tcnLine", 4, CStr(0))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("valAccountColumnCaption"), "valAccount", "tabLedger_acc", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  ,  , 20,  , eFunctions.Values.eTypeCode.eString)
		mobjGrid.Columns("valAccount").Parameters.Add("nLed_compan", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("valAuxColumnCaption"), "valAux", "tabLedger_accAux", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  ,  ,  ,  , eFunctions.Values.eTypeCode.eString)
		mobjGrid.Columns("valAux").Parameters.Add("nLed_compan", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		mobjGrid.Columns("valAux").Parameters.Add("sAccount", eRemoteDB.Constants.strnull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Call .AddTextColumn(0, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, "")
		Call .AddPossiblesColumn(0, GetLocalResourceObject("valUnitColumnCaption"), "valUnit", "tabTab_cost_c", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  ,  ,  ,  , eFunctions.Values.eTypeCode.eString)
		mobjGrid.Columns("valUnit").Parameters.Add("nLed_compan", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnDebitColumnCaption"), "tcnDebit", 18, CStr(0),  ,  , True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCreditColumnCaption"), "tcnCredit", 18, CStr(0),  ,  , True, 6)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cboCurrencyColumnCaption"), "cboCurrency", "table11", eFunctions.Values.eValuesType.clngComboType)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnExchangeColumnCaption"), "tcnExchange", 4, CStr(0),  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnOri_amoColumnCaption"), "tcnOri_amo", 18, CStr(0),  ,  , True, 6)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cboDoc_TypeColumnCaption"), "cboDoc_Type", "table288", eFunctions.Values.eValuesType.clngComboType)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnDocNumberColumnCaption"), "tcnDocNumber", 4, CStr(0),  ,  , True)
		Call .AddDateColumn(0, GetLocalResourceObject("tcdDateDocColumnCaption"), "tcdDateDoc", CStr(Today))
		Call .AddClientColumn(0, GetLocalResourceObject("valClientColumnCaption"), "valClient", vbNullString)
		Call .AddButtonColumn(0, GetLocalResourceObject("SCA2-OColumnCaption"), "SCA2-O", lclsAcc_lines.nNoteNum, True, Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery))
		Call .AddHiddenColumn("tctDesAccount", " ")
		Call .AddHiddenColumn("tctDesAux", "1")
		'*********************************************************************		
		Call .AddHiddenColumn("tcnAuxLine", CStr(0))
		Call .AddHiddenColumn("valAuxAccount", CStr(0))
		Call .AddHiddenColumn("valAuxAux", CStr(0))
		Call .AddHiddenColumn("tctAuxDescript", " ")
		Call .AddHiddenColumn("valAuxUnit", " ")
		Call .AddHiddenColumn("tcnAuxDebit", CStr(0))
		Call .AddHiddenColumn("tcnAuxCredit", CStr(0))
		Call .AddHiddenColumn("cboAuxCurrency", CStr(0))
		Call .AddHiddenColumn("tcnAuxExchange", CStr(0))
		Call .AddHiddenColumn("tcnAuxOri_amo", CStr(0))
		Call .AddHiddenColumn("cboAuxDoc_Type", CStr(0))
		Call .AddHiddenColumn("tcnAuxDocNumber", CStr(0))
		Call .AddHiddenColumn("tcdAuxDateDoc", " ")
		Call .AddHiddenColumn("valAuxClient", CStr(0))
		Call .AddHiddenColumn("btnAuxNotenum", CStr(0))
		Call .AddHiddenColumn("tctAuxDesAccount", " ")
		Call .AddHiddenColumn("tctAuxDesAux", " ")
		Call .AddHiddenColumn("sAuxSel", "2")
		
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Columns("Sel").GridVisible = True
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Codispl = "CP005"
		.Width = 700
		.Height = 380
		.Top = 100
		.FieldsByRow = 2
		.DeleteButton = True
		.AddButton = True
		If Session("bQuery") Then
			.Columns("Sel").GridVisible = False
			.bOnlyForQuery = True
		End If
		.Columns("tcnLine").EditRecord = True
		.Columns("valAccount").EditRecord = True
		.Columns("tctDescript").EditRecord = True
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		mobjGrid.sEditRecordParam = "sDescript=' + self.document.forms[0].tctDescript.value + '" & "&nNote=' + document.forms[0].btnNotenum.value + '"
		
	End With
	
End Sub

'% insPreCP005: Se cargan los controles de la página, tanto de la parte fija como del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCP005()
	'--------------------------------------------------------------------------------------------
	
	Dim lclsAcc_lines As eLedge.Acc_lines
	Dim lcolAcc_lineses As eLedge.Acc_lineses
	Dim lintIndex As Short
	
	Call insLoadCP005()
	With Server
		lclsAcc_lines = New eLedge.Acc_lines
		lcolAcc_lineses = New eLedge.Acc_lineses
	End With
	If lcolAcc_lineses.FindAll(mobjValues.StringToType(Session("nLedCompan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nVoucher"), eFunctions.Values.eTypeData.etdDouble)) Then
		lintIndex = 0
		For	Each lclsAcc_lines In lcolAcc_lineses
			With mobjGrid
				.Columns("tcnLine").DefValue = mobjValues.StringToType(CStr(lclsAcc_lines.nLine), eFunctions.Values.eTypeData.etdDouble)
				.Columns("tcnAuxLine").DefValue = CStr(lclsAcc_lines.nLine)
				.Columns("valAccount").Parameters.Add("nLed_compan", lclsAcc_lines.nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Columns("valAccount").DefValue = lclsAcc_lines.sAccount
				.Columns("valAux").Parameters.Add("nLed_compan", lclsAcc_lines.nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Columns("valAux").Parameters.Add("sAccount", lclsAcc_lines.sAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Columns("valAux").DefValue = lclsAcc_lines.sAux_accoun
				.Columns("tctDescript").DefValue = lclsAcc_lines.sDescript
				.Columns("valUnit").Parameters.Add("nLed_compan", lclsAcc_lines.nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Columns("valUnit").DefValue = lclsAcc_lines.sCost_cente
				.Columns("tcnDebit").DefValue = mobjValues.StringToType(CStr(lclsAcc_lines.nDebit), eFunctions.Values.eTypeData.etdDouble)
				.Columns("tcnCredit").DefValue = mobjValues.StringToType(CStr(lclsAcc_lines.nCredit), eFunctions.Values.eTypeData.etdDouble)
				.Columns("cboCurrency").DefValue = mobjValues.StringToType(CStr(lclsAcc_lines.nOri_curr), eFunctions.Values.eTypeData.etdDouble)
				.Columns("tcnExchange").DefValue = mobjValues.StringToType(CStr(lclsAcc_lines.nExchange), eFunctions.Values.eTypeData.etdDouble)
				.Columns("tcnOri_amo").DefValue = mobjValues.StringToType(CStr(lclsAcc_lines.nOri_amo), eFunctions.Values.eTypeData.etdDouble)
				.Columns("cboDoc_Type").DefValue = mobjValues.StringToType(CStr(lclsAcc_lines.nDoc_type), eFunctions.Values.eTypeData.etdDouble)
				.Columns("tcnDocNumber").DefValue = mobjValues.StringToType(CStr(lclsAcc_lines.nDocNumber), eFunctions.Values.eTypeData.etdDouble)
				.Columns("tcdDateDoc").DefValue = CStr(mobjValues.StringToDate(CStr(lclsAcc_lines.dDate_doc)))
				.Columns("valClient").DefValue = lclsAcc_lines.sClient
				.Columns("btnNotenum").nNoteNum = mobjValues.StringToType(CStr(lclsAcc_lines.nNoteNum), eFunctions.Values.eTypeData.etdDouble)
				.Columns("tctDesAccount").DefValue = lclsAcc_lines.sDesAccount
				.Columns("tctDesAux").DefValue = lclsAcc_lines.sDesAux
				'-------------------------------------------------------------------------------------------------------------------------------------------------------------------		    	    
				.Columns("tcnAuxLine").DefValue = CStr(lclsAcc_lines.nLine)
				.Columns("valAuxAccount").Parameters.Add("nLed_compan", lclsAcc_lines.nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Columns("valAuxAccount").DefValue = lclsAcc_lines.sAccount
				.Columns("valAuxAux").Parameters.Add("nLed_compan", lclsAcc_lines.nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Columns("valAuxAux").Parameters.Add("sAccount", lclsAcc_lines.sAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Columns("valAuxAux").DefValue = lclsAcc_lines.sAux_accoun
				.Columns("tctAuxDescript").DefValue = lclsAcc_lines.sDescript
				.Columns("valAuxUnit").Parameters.Add("nLed_compan", lclsAcc_lines.nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Columns("valAuxUnit").DefValue = lclsAcc_lines.sCost_cente
				.Columns("tcnAuxDebit").DefValue = mobjValues.StringToType(CStr(lclsAcc_lines.nDebit), eFunctions.Values.eTypeData.etdDouble)
				.Columns("tcnAuxCredit").DefValue = mobjValues.StringToType(CStr(lclsAcc_lines.nCredit), eFunctions.Values.eTypeData.etdDouble)
				.Columns("cboAuxCurrency").DefValue = mobjValues.StringToType(CStr(lclsAcc_lines.nOri_curr), eFunctions.Values.eTypeData.etdDouble)
				.Columns("tcnAuxExchange").DefValue = mobjValues.StringToType(CStr(lclsAcc_lines.nExchange), eFunctions.Values.eTypeData.etdDouble)
				.Columns("tcnAuxOri_amo").DefValue = mobjValues.StringToType(CStr(lclsAcc_lines.nOri_amo), eFunctions.Values.eTypeData.etdDouble)
				.Columns("cboAuxDoc_Type").DefValue = mobjValues.StringToType(CStr(lclsAcc_lines.nDoc_type), eFunctions.Values.eTypeData.etdDouble)
				.Columns("tcnAuxDocNumber").DefValue = mobjValues.StringToType(CStr(lclsAcc_lines.nDocNumber), eFunctions.Values.eTypeData.etdDouble)
				.Columns("tcdAuxDateDoc").DefValue = CStr(mobjValues.StringToDate(CStr(lclsAcc_lines.dDate_doc)))
				.Columns("valAuxClient").DefValue = lclsAcc_lines.sClient
				.Columns("btnAuxNotenum").nNoteNum = mobjValues.StringToType(CStr(lclsAcc_lines.nNoteNum), eFunctions.Values.eTypeData.etdDouble)
				.Columns("tctAuxDesAccount").DefValue = lclsAcc_lines.sDesAccount
				.Columns("tctAuxDesAux").DefValue = lclsAcc_lines.sDesAux
				
				mobjGrid.sDelRecordParam = "nLine=' + marrArray[lintIndex].tcnLine + '"
				Response.Write(.DoRow)
				lintIndex = lintIndex + 1
				
			End With
			mintLine = mobjValues.StringToType(CStr(lclsAcc_lines.nLine), eFunctions.Values.eTypeData.etdDouble)
		Next lclsAcc_lines
		mintLine = mintLine + 1
		Response.Write(mobjValues.HiddenControl("SequenceLine", mintLine))
	Else
		mintLine = 1
		Response.Write(mobjValues.HiddenControl("SequenceLine", mintLine))
	End If
	
	
	Response.Write(mobjGrid.closeTable())
	Response.Write("<SCRIPT>top.fraHeader.document.forms[0].tcnLedCompan.value='" & Session("nLedCompan") & "';</" & "Script>")
	Response.Write("<SCRIPT>top.fraHeader.document.forms[0].tcnNumber.value='" & Session("nVoucher") & "';</" & "Script>")
	Response.Write("<SCRIPT>top.fraHeader.document.forms[0].tcnNumOffi.value='" & Session("nOffiNum") & "';</" & "Script>")
	
	
	
	lclsAcc_lines = Nothing
	lcolAcc_lineses = Nothing
End Sub

'% insPreCP005Upd: Se muetra la ventana Popup para efecto de actualización del Gird
'--------------------------------------------------------------------------------------------
Private Sub insPreCP005Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsAcc_lines As eLedge.Acc_lines
	Dim lclsErrors As eFunctions.Errors
	Dim lblnPost As Boolean
	lclsAcc_lines = New eLedge.Acc_lines
	lclsErrors = New eFunctions.Errors
	If Request.QueryString.Item("Action") = "Del" Then
		Response.Write(mobjValues.ConfirmDelete())
		
		lblnPost = lclsAcc_lines.insDelAcc_linesEach(mobjValues.StringToType(Session("nLedCompan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nVoucher"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nLine"), eFunctions.Values.eTypeData.etdDouble))
	End If
	
	With Request
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valLedGerTra.aspx", "CP005", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
		Response.Write(mobjValues.HiddenControl("sDescript", .QueryString.Item("sDescript")))
		Response.Write(mobjValues.HiddenControl("nNote", .QueryString.Item("nNote")))
	End With
	
	If Request.QueryString.Item("Action") = "Add" Then
		
Response.Write("" & vbCrLf)
Response.Write("		<SCRIPT>" & vbCrLf)
Response.Write("			self.document.forms[0].elements[""tcnLine""].value = (opener.document.forms[0].elements[""SequenceLine""].value)" & vbCrLf)
Response.Write("		</" & "SCRIPT>")

		
	End If
	
	lclsAcc_lines = Nothing
	lclsErrors = Nothing
End Sub

</script>
<%
Response.Expires = -1


mintLine = 0

With Server
	mobjValues = New eFunctions.Values
	mobjGrid = New eFunctions.Grid
	mobjMenu = New eFunctions.Menues
	lclsAcc_lines = New eLedge.Acc_lines
	lclsAcc_transa = New eLedge.Acc_transa
	lclsLedger = New eLedge.Ledger
End With

mobjValues.sCodisplPage = "CP005"


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
		.Write("<SCRIPT>var nMainAction=" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
		.Write(mobjMenu.setZone(2, "CP005", "CP005.aspx"))
		mobjMenu = Nothing
	End If
	.Write(mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl")))
End With%>
<SCRIPT LANGUAGE="JavaScript">
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//------------------------------------------------------------------------------------------
function insLoadPostCut(){
//------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
        document.forms[0].action="/VTimeNet/GeneralLedGer/LedGerTra/valLedGerTra.aspx?sCodispl=CP005&Action=" + "Cut"
        document.forms[0].submit()
	}
}
//------------------------------------------------------------------------------------------
function insLoadPostReverse(){
//------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
        document.forms[0].action="/VTimeNet/GeneralLedGer/LedGerTra/valLedGerTra.aspx?sCodispl=CP005&Action=" + 'Reverse'
        document.forms[0].submit()
	}
}
//------------------------------------------------------------------------------------------
function insSelected(Field){
//---------------------------------------------------------------------------
    Field.checked = !Field.checked
}


</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%Response.Write(mobjValues.ShowWindowsName("CP005"))%>
<FORM METHOD="POST" ID="FORM" NAME="frmHisBalance" ACTION="ValLedGerTra.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
    <TABLE WIDTH="100%">
        <%
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreCP005()
Else
	Call insPreCP005Upd()
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




