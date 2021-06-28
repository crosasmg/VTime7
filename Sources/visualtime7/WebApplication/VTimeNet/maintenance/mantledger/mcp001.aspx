<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eLedge" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores es definido

Dim mobjValues As eFunctions.Values

Dim mobjMenu As eFunctions.Menues

'-   Objeto para el manejo del grid es definido   

Dim mobjGrid As eFunctions.Grid

'-   Objeto para el manejo contable es definido
Dim mobjeLedGe As Object

Dim mintCount As Object


'**% insDefineHeader: The detail fields of the page are defined
'%   insDefineHeader: Se definen los campos del detalle de la página
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	If Request.QueryString.Item("Action") <> "Del" Then
		
Response.Write("	" & vbCrLf)
Response.Write("		<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("		")

		If Request.QueryString.Item("Type") <> "PopUp" Then
			
Response.Write("" & vbCrLf)
Response.Write("	    		<TR>" & vbCrLf)
Response.Write("					<TD> &nbsp; </TD>" & vbCrLf)
Response.Write("  					<TD> &nbsp; </TD>" & vbCrLf)
Response.Write("  					<TD> &nbsp; </TD>" & vbCrLf)
Response.Write("  				</TR>")

		Else
			Response.Write("<BR>")
		End If
		
Response.Write("" & vbCrLf)
Response.Write("		</TABLE>	")

		
	End If
	
	'+   Se definen las columnas del Grid
	With mobjGrid.Columns
		Call .AddHiddenColumn("nConsec", "")
		Call .AddCheckColumn(0, GetLocalResourceObject("chkDebitColumnCaption"), "chkDebit", "",  , CStr(1), "insMark(this,1)", False)
		Call .AddCheckColumn(0, GetLocalResourceObject("chkCreditColumnCaption"), "chkCredit", "",  , CStr(1), "insMark(this,2)", False)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeParameterColumnCaption"), "cbeParameter", "Table307", eFunctions.Values.eValuesType.clngWindowType, vbNullString, False,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeParameterColumnToolTip"),  , 5)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeComplementColumnCaption"), "cbeComplement", "Table308", eFunctions.Values.eValuesType.clngComboType, "1", False,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeComplementColumnToolTip"),  , 6)
		
		If Request.QueryString.Item("Type") <> "PopUp" Then
			Call .AddTextColumn(0, GetLocalResourceObject("tctAccount_codeColumnCaption"), "tctAccount_code", 20, vbNullString, False, GetLocalResourceObject("tctAccount_codeColumnToolTip"),  ,  ,  , True, 7)
			Call .AddPossiblesColumn(0, GetLocalResourceObject("tctAccountColumnCaption"), "tctAccount", "TabLedger_acc", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  ,  , 20, GetLocalResourceObject("tctAccountColumnToolTip"), eFunctions.Values.eTypeCode.eString, 8)
		Else
			Call .AddPossiblesColumn(0, GetLocalResourceObject("tctAccountColumnCaption"), "tctAccount", "TabLedger_acc", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  ,  , 20, GetLocalResourceObject("tctAccountColumnCaption"), eFunctions.Values.eTypeCode.eString, 8)
		End If
		
		mobjGrid.Columns("tctAccount").Parameters.Add("nLed_compan", Session("nComp_led"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
		Call .AddPossiblesColumn(0, GetLocalResourceObject("valPayFormColumnCaption"), "valPayForm", "Table78", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valPayFormColumnToolTip"))
		
	End With
	
	With mobjGrid
		If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or Request.QueryString.Item("nMainAction") = vbNullString Then
			.Columns("Sel").GridVisible = False
			.ActionQuery = True
		End If
		
		If Request.QueryString.Item("Type") <> "PopUp" Then
			.Columns("tctAccount_code").GridVisible = True
			.Columns("chkDebit").Disabled = True
			.Columns("chkCredit").Disabled = True
		End If
		
		If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionUpdate) Or Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionAdd) Then
			.AddButton = True
			.Codispl = Request.QueryString.Item("sCodispl")
			.Codisp = "MCP001"
			.sCodisplPage = "MCP001"
			.Height = 320
			.Width = 400
			.sDelRecordParam = "nConsec=' + marrArray[lintIndex].nConsec + '"
			
			If Request.QueryString.Item("Reload") = "1" Then
				.sReloadIndex = Request.QueryString.Item("ReloadIndex")
			End If
		End If
	End With
End Sub

'%   insPreMCP001: Se cargan los datos del detalle de la página
'------------------------------------------------------------------------------
Private Sub insPreMCP001()
	'------------------------------------------------------------------------------
	Dim lcolDet_lines As eLedge.Det_liness
	Dim lclsDet_lines As eLedge.Det_lines
	
	lclsDet_lines = New eLedge.Det_lines
	lcolDet_lines = New eLedge.Det_liness
	
	If lcolDet_lines.Find(Session("nArea_led"), Session("nTransac_ty"), Session("nTratypei"), Session("nReceipt_ty"), Session("nProduct_ty"), Session("sPay_type"), Session("nTyp_acco"), Session("nComp_led")) Then
		mintCount = 0
		For	Each lclsDet_lines In lcolDet_lines
			With mobjGrid
				
				.Columns("nConsec").DefValue = CStr(lclsDet_lines.nConsec)
				.sEditRecordParam = "nConsec=" & lclsDet_lines.nConsec
				
				If lclsDet_lines.nLine_type = 1 Then
					.Columns("chkDebit").Checked = 1
					.Columns("chkCredit").Checked = 2
				Else
					.Columns("chkDebit").Checked = 2
					.Columns("chkCredit").Checked = 1
				End If
				
				.Columns("cbeParameter").DefValue = CStr(lclsDet_lines.nParameter)
				.Columns("cbeComplement").DefValue = CStr(lclsDet_lines.nComplement)
				
				If Request.QueryString.Item("Type") <> "PopUp" Then
					.Columns("tctAccount_code").DefValue = lclsDet_lines.sAccount
				End If
				
				.Columns("tctAccount").DefValue = lclsDet_lines.sAccount
				
				mintCount = mintCount + 1
				
				.Columns("Sel").OnClick = "ValidateAccount(this.value," & Session("nComp_led") & ",""" & lclsDet_lines.sAccount & """)"
				
				.Columns("valPayForm").DefValue = lclsDet_lines.sPay_form
				
				Response.Write(.DoRow)
			End With
		Next lclsDet_lines
	End If
	Response.Write(mobjGrid.closeTable())
End Sub

'% insPreMCP001Upd: Se borrar los datos del detalle de la página
'------------------------------------------------------------------------------
Private Sub insPreMCP001Upd()
	'------------------------------------------------------------------------------
	Dim lclsDet_lines As eLedge.Det_lines
	If Request.QueryString.Item("Action") = "Del" Then
		
		lclsDet_lines = New eLedge.Det_lines
		
		With lclsDet_lines
			
			.nArea_led = Session("nArea_led")
			.nTransac_ty = Session("nTransac_ty")
			.nTratypei = Session("nTratypei")
			.nReceipt_ty = Session("nReceipt_ty")
			.nProduct_ty = Session("nProduct_ty")
			.sPay_type = Session("sPay_type")
			.nTyp_acco = Session("nTyp_acco")
			.nConsec = CInt(Request.QueryString.Item("nConsec"))
			.nLed_compan = Session("nComp_led")
			
			Call .insDelDet_lines()
			
			Response.Write(mobjValues.ConfirmDelete())
		End With
		
		lclsDet_lines = Nothing
	End If
	
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValMantLedger.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"),  , CShort(Request.QueryString.Item("Index"))))
	If Request.QueryString.Item("Action") <> "Del" Then
		If (Session("nArea_led") = 5) Then
			Response.Write("<SCRIPT>self.document.forms[0].elements['valPayForm'].disabled=false;</" & "Script>")
			Response.Write("<SCRIPT>self.document.forms[0].elements['btnvalPayForm'].disabled=false;</" & "Script>")
		Else
			Response.Write("<SCRIPT>self.document.forms[0].elements['valPayForm'].disabled=true;</" & "Script>")
			Response.Write("<SCRIPT>self.document.forms[0].elements['btnvalPayForm'].disabled=true;</" & "Script>")
		End If
	End If
	
End Sub

</script>
<%
Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjGrid = New eFunctions.Grid

mobjValues.sCodisplPage = "MCP001"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
	<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">




<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "MCP001", "MCP001.aspx"))
End If
%>
<SCRIPT>

//**% insMark: This function allows to change check mark of the Debit and credit fields 
//%   insMark: Desmarca crédito si débito esta marcado y veceversa
//------------------------------------------------------------------------------------------
function insMark(lobject, llngInd){
//------------------------------------------------------------------------------------------
	if(llngInd==1)
	{
	    if(lobject.checked)
	          self.document.forms[0].chkCredit.checked = false;
    }
    else
    {
	    if(lobject.checked)
	          self.document.forms[0].chkDebit.checked = false;
	}		
}

//**% insStateZone: This function allows to control the status of the items page
//%   insStateZone: Se controla el estado de los campos de la página
//-------------------------------------------------------------------------------------------------------------------
function insStateZone()
//-------------------------------------------------------------------------------------------------------------------
{

}

//**% insPreZone: This function allows to control the action in process
//%   insPreZone: Se controla la acción ejecutada
//-------------------------------------------------------------------------------------------------------------------
function insPreZone(llngAction){
//-------------------------------------------------------------------------------------------------------------------
	switch (llngAction)
	{
	    case 301:
	    case 302:
	    case 401:
	    {
	        document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + llngAction;
	        break;
		}
	}
}

//**% insCancel: This function is executed when the page is cancelled
//%   insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

//**% insFinish: This function is executed when the finish button is pushed
//%   insFinish: Ejecuta rutinas necesarias en el momento de presionar el botón de finalizar
//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
	return true;
}

//% ValidateAccount: Verifica si la cuenta tiene asientos contables.
//------------------------------------------------------------------------------------------
function ValidateAccount(lintIndex, nLed_Compan, sAccount){
//------------------------------------------------------------------------------------------
	if(self.document.forms[0].hddCount.value>1)
	{
		if(self.document.forms[0].Sel[lintIndex].checked==true)
			insDefValues('ValidateAccount', 'nLed_Compan=' + nLed_Compan + '&sAccount=' + sAccount + '&Index=' + lintIndex, '/VTimeNet/Maintenance/MantLedger');
	}
	else
	{
		if(self.document.forms[0].Sel.checked==true)
			insDefValues('ValidateAccount', 'nLed_Compan=' + nLed_Compan + '&sAccount=' + sAccount + '&Index=' + lintIndex, '/VTimeNet/Maintenance/MantLedger');
	}
}

//+ Esta línea guarda la versión procedente de VSS 
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $" 

</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmMCP001" ACTION="ValMantLedger.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Response.Write("<SCRIPT>var nMainAction = 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))

Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMCP001()
	%>
    <TR>
        <TD><%=mobjValues.HiddenControl("hddCount", mintCount)%></TD>
    </TR>
<%	
Else
	Call insPreMCP001Upd()
End If

mobjMenu = Nothing
mobjValues = Nothing
mobjGrid = Nothing
mobjeLedGe = Nothing
%>
</FORM>
</BODY>
</HTML>





