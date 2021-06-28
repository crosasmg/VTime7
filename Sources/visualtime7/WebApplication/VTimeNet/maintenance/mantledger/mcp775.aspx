<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eLedge" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las zonas de la página    
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid



'%insDefineHeader: Se definen las columnas del grid
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		
		Select Case mobjValues.StringToType(Request.QueryString.Item("nArea_led"), eFunctions.Values.eTypeData.etdDouble)
			Case 1 '+ Primas
				Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeTransactionColumnCaption"), "cbeTransaction", "Table6", 1,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeTransactionColumnToolTip"))
			Case 2 '+ Siniestros
				Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeTransactionColumnCaption"), "cbeTransaction", "Table140", 1,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeTransactionColumnToolTip"))
			Case 3 '+ Cuentas Corrientes
				Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeTransactionColumnCaption"), "cbeTransaction", "Table401", 1,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeTransactionColumnToolTip"))
			Case 5 '+ Caja ingreso
				Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeTransactionColumnCaption"), "cbeTransaction", "Table22", 1,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeTransactionColumnToolTip"))
			Case 6 '+ Caja egreso
				Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeTransactionColumnCaption"), "cbeTransaction", "Table293", 1,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeTransactionColumnToolTip"))
			Case 7 '+ Financiamiento
				Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeTransactionColumnCaption"), "cbeTransaction", "Table260", 1,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeTransactionColumnToolTip"))
		End Select
		
		Call .AddTextColumn(0, GetLocalResourceObject("tctAccount_baseColumnCaption"), "tctAccount_base", 20, "",  , GetLocalResourceObject("tctAccount_baseColumnToolTip"),  ,  ,  , True)
		Call .AddTextColumn(0, GetLocalResourceObject("tctDescript_AccColumnCaption"), "tctDescript_Acc", 50, "",  , GetLocalResourceObject("tctDescript_AccColumnToolTip"),  ,  ,  , True)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeComplementColumnCaption"), "cbeComplement", "Table308", 1,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeComplementColumnToolTip"))
		Call .AddCheckColumn(0, GetLocalResourceObject("chkDebtColumnCaption"), "chkDebt", "",  ,  , "CheckFields(this)", True)
		Call .AddCheckColumn(0, GetLocalResourceObject("chkCreditColumnCaption"), "chkCredit", "",  ,  , "CheckFields(this)", True)
		Call .AddTextColumn(0, GetLocalResourceObject("tctAccount_FIN700ColumnCaption"), "tctAccount_FIN700", 20, "",  , GetLocalResourceObject("tctAccount_FIN700ColumnToolTip"),  ,  ,  , False)
	End With
	
	With mobjGrid
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Codispl = "MCP775"
		.Codisp = "MCP775"
		.sCodisplPage = "MCP775"
		.Top = 200
		.Left = 150
		.Height = 300
		.Width = 625
		.Columns("Sel").gridvisible = False
		.ActionQuery = mobjValues.ActionQuery
		.bOnlyForQuery = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery)
		.sEditRecordParam = "nLed_compan=" & Request.QueryString.Item("nLed_compan") & "&nArea_led=" & Request.QueryString.Item("nArea_led") & "&nGroup=" & Request.QueryString.Item("nGroup")
		.AddButton = False
		.DeleteButton = False
		.Columns("tctAccount_base").EditRecord = True
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'%insPreMCP775: Se definen los objetos a ser utilizados y permite realizar el llamado al
'%método de lectura para mostrar la información en la parte de detalle de la página.
'-----------------------------------------------------------------------------------------
Private Sub insPreMCP775()
	'-----------------------------------------------------------------------------------------
	Dim lintCount As Integer
	Dim lclsFin700_Lines As eLedge.Fin700_Lines
	
	lclsFin700_Lines = New eLedge.Fin700_Lines
	With mobjGrid
		If lclsFin700_Lines.Find_FIN700(mobjValues.StringToType(Request.QueryString.Item("nLed_compan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nArea_led"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nGroup"), eFunctions.Values.eTypeData.etdDouble)) Then
			
			For lintCount = 1 To lclsFin700_Lines.Count_FIN700
				If lclsFin700_Lines.ItemFIN700(lintCount) Then
					.Columns("cbeTransaction").DefValue = CStr(lclsFin700_Lines.nTransac_ty)
					.Columns("tctAccount_base").DefValue = lclsFin700_Lines.sAccount_Base
					.Columns("tctDescript_Acc").DefValue = lclsFin700_Lines.sDescript_Acc
					.Columns("cbeComplement").DefValue = CStr(lclsFin700_Lines.nComplement)
					
					If lclsFin700_Lines.nLine_Type = 1 Then
						.Columns("chkDebt").Checked = 1
						.Columns("chkCredit").Checked = 2
					Else
						.Columns("chkDebt").Checked = 2
						.Columns("chkCredit").Checked = 1
					End If
					
					.Columns("tctAccount_FIN700").DefValue = lclsFin700_Lines.sAccount_FIN700
					Response.Write(mobjGrid.DoRow())
				End If
			Next 
		End If
		Response.Write(mobjGrid.CloseTable())
	End With
	lclsFin700_Lines = Nothing
End Sub

'% insPreMCP775Upd. Se define esta funcion para contruir el contenido de la 
'%                     ventana UPD de los archivos de datos particulares
'------------------------------------------------------------------------------
Private Sub insPreMCP775Upd()
	'------------------------------------------------------------------------------        
	With Request
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantLedger.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjGrid.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjGrid = New eFunctions.Grid
mobjValues.sCodisplPage = "MCP775"
%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="eTransaction Designer for Visual TIME">




<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	mobjMenu = New eFunctions.Menues
	Response.Write("<SCRIPT>var	nMainAction	= 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
	Response.Write(mobjMenu.setZone(2, "MCP775", "MCP775.aspx"))
	mobjMenu = Nothing
End If
If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
	mobjValues.ActionQuery = True
End If
%>
<SCRIPT> 
//%insCancel: Controla la acción Cancelar de la página
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
   return true
}

 //+Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:03 $|$$Author: Nvaplat61 $"

</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="MCP775" ACTION="valMantLedger.aspx?sZone=2">
<%
Response.Write(mobjValues.ShowWindowsName("MCP775"))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMCP775()
Else
	Call insPreMCP775Upd()
End If

mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>





