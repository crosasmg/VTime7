<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eLedge" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues
Dim lclsLedger As eLedge.Ledger


'% insLoadCP002: Dibuja los campos no repetitivos de la pantalla, con sus respectivos
'% valores segùn sea el caso.
'------------------------------------------------------------------------------------------
Private Sub insLoadCP002()
	'------------------------------------------------------------------------------------------
	Dim lclsLedgerAcc As eLedge.LedgerAcc
	Dim lstrTypeLevelPrevious As String
	Dim lstrAccoun As String
	Dim lstrTypeCreAux As Object
	Dim lblnDisabled As Boolean
	
	lclsLedgerAcc = New eLedge.LedgerAcc
	
	lstrAccoun = Request.QueryString.Item("sAccount")
	lstrTypeCreAux = 1
	If InStr(1, Request.QueryString.Item("sAccount"), "-") > 1 Then
		lstrAccoun = Mid(Request.QueryString.Item("sAccount"), 1, InStr(1, Request.QueryString.Item("sAccount"), "-") - 1)
	End If
	
	If lclsLedgerAcc.Find_Account(CInt(Request.QueryString.Item("nLedCompan")), lstrAccoun) Then
		lstrTypeLevelPrevious = lclsLedgerAcc.sType_acc
		lstrTypeCreAux = lclsLedgerAcc.nAux_create
	End If
	
	If lclsLedgerAcc.Find(CInt(Request.QueryString.Item("nLedCompan")), Request.QueryString.Item("sAccount"), Request.QueryString.Item("sAux_Account")) Then
		lstrTypeLevelPrevious = lclsLedgerAcc.sType_acc
		lstrTypeCreAux = lclsLedgerAcc.nAux_create
	End If
	
	If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 303 Then
		lblnDisabled = True
	Else
		lblnDisabled = False
	End If
	
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL>" & GetLocalResourceObject("cbeTypeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeType", "table285", 1, lstrTypeLevelPrevious,  ,  ,  ,  ,  ,  , lblnDisabled,  , GetLocalResourceObject("cbeTypeToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.CheckControl("chkBudget", GetLocalResourceObject("chkBudgetCaption"), lclsLedgerAcc.sBudget_ind, "1",  , lblnDisabled))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL>" & GetLocalResourceObject("tctDescriptCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.TextControl("tctDescript", 40, lclsLedgerAcc.sDescript,  , GetLocalResourceObject("tctDescriptToolTip"), lblnDisabled))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.CheckControl("chkOrgUnit", GetLocalResourceObject("chkOrgUnitCaption"), lclsLedgerAcc.sOrgan_unit, "1",  , lblnDisabled))


Response.Write("</TD>            " & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.HiddenControl("tcthidAuxType", lstrTypeLevelPrevious))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL>" & GetLocalResourceObject("cbeAuxCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("			")

	If IsNothing(Request.QueryString.Item("sAux_Account")) Then
Response.Write("" & vbCrLf)
Response.Write("			    <TD>")


Response.Write(mobjValues.PossiblesValues("cbeAux", "table286", 1, CStr(2),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeAuxToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("			")

	Else
Response.Write("" & vbCrLf)
Response.Write("			    <TD>")


Response.Write(mobjValues.PossiblesValues("cbeAux", "table286", 1, lstrTypeCreAux,  ,  ,  ,  ,  ,  , lblnDisabled,  , GetLocalResourceObject("cbeAuxToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("			")

	End If
Response.Write("" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.CheckControl("chkAdjust", GetLocalResourceObject("chkAdjustCaption"), lclsLedgerAcc.sAdju_exci, "1",  , lblnDisabled))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>        " & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">        " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD CLASS=""HighLighted""><LABEL><A NAME=""Bloquear"">" & GetLocalResourceObject("AnchorBloquearCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("			<TD>&nbsp</TD>" & vbCrLf)
Response.Write("			<TD CLASS=""HighLighted""><LABEL><A NAME=""Total"">" & GetLocalResourceObject("AnchorTotalCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("		</TR>  " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><HR></TD>" & vbCrLf)
Response.Write("            <TD>&nbsp</TD>" & vbCrLf)
Response.Write("            <TD><HR></TD>" & vbCrLf)
Response.Write("		</TR>  " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.CheckControl("chkDebit", GetLocalResourceObject("chkDebitCaption"), lclsLedgerAcc.sBlock_deb,  ,  , lblnDisabled))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>&nbsp</TD>" & vbCrLf)
Response.Write("            <TD><LABEL>" & GetLocalResourceObject("lblTDebitCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.TextControl("lblTDebit", 15, CStr(lclsLedgerAcc.nTotal_deb),  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>  " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.CheckControl("chkCredit", GetLocalResourceObject("chkCreditCaption"), lclsLedgerAcc.sBlock_cre,  ,  , lblnDisabled))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>&nbsp</TD>" & vbCrLf)
Response.Write("            <TD><LABEL>" & GetLocalResourceObject("lblTCreditCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.TextControl("lblTCredit", 15, CStr(lclsLedgerAcc.nTotal_cre),  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>  " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD>&nbsp</TD>" & vbCrLf)
Response.Write("            <TD>&nbsp</TD>" & vbCrLf)
Response.Write("            <TD><LABEL>" & GetLocalResourceObject("lblTBalanceCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.TextControl("lblTBalance", 30,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>  " & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD CLASS=""HighLighted""><LABEL><A NAME=""Total"">" & GetLocalResourceObject("AnchorTotal2Caption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><HR></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>")

	If Not mobjValues.ActionQuery Then
		If Not CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 303 Then
			Response.Write(("<SCRIPT>insDefValues('Locked','sWindow=Folder&nLed_compan=" & Request.QueryString.Item("nLedCompan") & "&sAccount=" & Request.QueryString.Item("sAccount") & "&sAux=" & Request.QueryString.Item("sAux_Account") & "','/VTimeNet/GeneralLedGer/LedgerTra');</" & "Script>"))
		End If
	End If
End Sub

'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	'+ Se definen las columnas del grid
	
	mobjGrid.sCodisplPage = "CP002"
	
	With mobjGrid.Columns
		Call .AddTextColumn(0, GetLocalResourceObject("tctAccountColumnCaption"), "tctAccount", 20, "")
		Call .AddTextColumn(0, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 20, "")
		
		Call .AddHiddenColumn("tctAuxAccount", "")
		Call .AddHiddenColumn("tctAuxDescript", "")
		Call .AddHiddenColumn("sAuxSel", CStr(0))
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		If Request.QueryString.Item("Action") = "Update" Then
			.Columns("Sel").GridVisible = False
		End If
		.Columns("Sel").GridVisible = False
		.Codispl = "CP002"
		.Width = 450
		.Height = 250
		.DeleteButton = False
		.AddButton = False
		If Session("bQuery") Then
			.DeleteButton = False
			.AddButton = False
			.Columns("Sel").GridVisible = False
			.bOnlyForQuery = True
		End If
		.DeleteScriptName = vbNullString
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreCP002: Se cargan los controles de la página, tanto de la parte fija como del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCP002()
	'--------------------------------------------------------------------------------------------
	Dim lclsLedger_Acc As eLedge.LedgerAcc
	Dim lcolLedger_Acc As Microsoft.VisualBasic.Collection
	Dim lintIndex As Object
	
	lclsLedger_Acc = New eLedge.LedgerAcc
	lcolLedger_Acc = lclsLedger_Acc.FullChargePrevLevel(mobjValues.StringToType(Request.QueryString.Item("nLedCompan"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sAccount"))
	
	If lcolLedger_Acc.Count > 0 Then
		For	Each lclsLedger_Acc In lcolLedger_Acc
			With mobjGrid
				.Columns("tctAccount").DefValue = lclsLedger_Acc.sAccount
				.Columns("tctDescript").DefValue = lclsLedger_Acc.sDescript
				.Columns("tctAuxAccount").DefValue = lclsLedger_Acc.sAccount
				.Columns("tctAuxDescript").DefValue = lclsLedger_Acc.sDescript
				Response.Write(.DoRow)
			End With
		Next lclsLedger_Acc
	End If
	
	Response.Write(mobjGrid.closeTable())
	
	lclsLedger_Acc = Nothing
	lcolLedger_Acc = Nothing
End Sub

</script>
<%
Response.Expires = -1

With Server
	mobjValues = New eFunctions.Values
	mobjGrid = New eFunctions.Grid
	mobjMenu = New eFunctions.Menues
	lclsLedger = New eLedge.Ledger
End With

If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
	mobjValues.ActionQuery = True
End If

mobjValues.sCodisplPage = "CP002"
%>
<SCRIPT>
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
// % insValNumField: Coloca en 0 los campos numericos que dejen en blanco
//-------------------------------------------------------------------------------------------
function insValNumField(field){
//-------------------------------------------------------------------------------------------
    if (field.value.replace(/ */,'') == '')
        field.value = 0
}

//+ Esta línea guarda la versión procedente de VSS 
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 15:06 $" 
</SCRIPT>

<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/ValFunctions.js"></SCRIPT>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">





    <%With Response
	.Write(mobjValues.StyleSheet())
	
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, "CP002", "CP002.aspx"))
		.Write(mobjValues.ShowWindowsName("CP002"))
		mobjMenu = Nothing
	End If
End With%>
	 
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmInsLedCompan" ACTION="ValLedGerTra.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction") & "&nLedCompan=" & Request.QueryString.Item("nLedCompan") & "&sAccount=" & Request.QueryString.Item("sAccount") & "&sAux_Account=" & Request.QueryString.Item("sAux_Account")%>">
    <TABLE WIDTH="100%">
		<%
Call insLoadCP002()
Call insDefineHeader()
Response.Write("<BR>")
Call insPreCP002()
%>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjGrid = Nothing
%>




