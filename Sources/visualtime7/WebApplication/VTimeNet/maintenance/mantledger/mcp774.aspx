<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eLedge" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la página
Dim mobjMenu As eFunctions.Menues


'%insDefineHeader: Se definen las columnas del grid
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		Call .AddTextColumn(0, GetLocalResourceObject("tctCodeVisualColumnCaption"), "tctCodeVisual", 10, "", True, GetLocalResourceObject("tctCodeVisualColumnToolTip"),  ,  ,  , False)
		Call .AddTextColumn(0, GetLocalResourceObject("tctCodeAsiColumnCaption"), "tctCodeAsi", 10, "", False, GetLocalResourceObject("tctCodeAsiColumnToolTip"),  ,  ,  , False)
		Call .AddTextColumn(0, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, "", False, GetLocalResourceObject("tctDescriptColumnToolTip"),  ,  ,  , False)
	End With
	
	With mobjGrid
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Codispl = "MCP774"
		.Codisp = "MCP774"
		.sCodisplPage = "MCP774"
		.Top = 215
		.Left = 150
		.Height = 224
		.Width = 625
		.WidthDelete = 620
		.ActionQuery = mobjValues.ActionQuery
		.bOnlyForQuery = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery)
		Session("bQuery") = .ActionQuery
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("tctCodeVisual").Disabled = Request.QueryString.Item("Action") = "Update"
		.Columns("tctCodeVisual").EditRecord = True
		.sDelRecordParam = "nLed_compan=" & Request.QueryString.Item("nLed_compan") & "&nTypecode=" & Request.QueryString.Item("nTypecode") & "&sCodeVisual='+ marrArray[lintIndex].tctCodeVisual + '"
		.sEditRecordParam = "nLed_compan=" & Request.QueryString.Item("nLed_compan") & "&nTypecode=" & Request.QueryString.Item("nTypecode")
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'%insPreMCP774. Se crea la ventana madre (Principal)
'------------------------------------------------------------------------------
Private Sub insPreMCP774()
	'------------------------------------------------------------------------------
	Dim lcolTab_equals As eLedge.Tab_equals
	Dim lclsTab_equal As Object
	
	With Request
		lcolTab_equals = New eLedge.Tab_equals
		With mobjGrid
			If lcolTab_equals.Find(mobjValues.StringToType(Request.QueryString.Item("nLed_compan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nTypecode"), eFunctions.Values.eTypeData.etdDouble)) Then
				For	Each lclsTab_equal In lcolTab_equals
					.Columns("tctCodeVisual").DefValue = lclsTab_equal.sCodeVisual
					.Columns("tctCodeAsi").DefValue = lclsTab_equal.sCodeAsi
					.Columns("tctDescript").DefValue = lclsTab_equal.sDescript
					Response.Write(mobjGrid.DoRow())
				Next lclsTab_equal
			End If
		End With
		
	End With
	Response.Write(mobjGrid.CloseTable())
	
	lclsTab_equal = Nothing
	lcolTab_equals = Nothing
End Sub

'% insPreMCP774Upd. Se define esta funcion para contruir el contenido de la 
'%                     ventana UPD de los archivos de datos particulares
'------------------------------------------------------------------------------
Private Sub insPreMCP774Upd()
	'------------------------------------------------------------------------------
	Dim lclsTab_equal As eLedge.Tab_equal
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			lclsTab_equal = New eLedge.Tab_equal
			Call lclsTab_equal.InsPostMCP774(False, .QueryString.Item("sCodispl"), CInt(.QueryString.Item("nMainAction")), .QueryString.Item("Action"), Session("nUsercode"), mobjValues.StringToType(.QueryString.Item("nLed_compan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nTypecode"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("sCodeVisual"), "", "")
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantLedger.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjGrid.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	lclsTab_equal = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MCP774"
%>
<HTML>
<HEAD>




<SCRIPT LANGUAGE="JavaSCRIPT" SRC="/VTimeNet/SCRIPTs/GenFunctions.js"></SCRIPT>
	<META NAME="GENERATOR" CONTENT="eTransaction Designer for Visual TIME">
<%
mobjValues.ActionQuery = (Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery))
With Response
	.Write(mobjValues.StyleSheet())
	.Write("<SCRIPT>var	nMainAction	= " & CShort("0" & Request.QueryString.Item("nMainAction")) & "</SCRIPT>")
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.setZone(2, "MCP774", "MCP774.aspx"))
		mobjMenu = Nothing
	End If
End With
%>

<SCRIPT>
 //+Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:03 $|$$Author: Nvaplat61 $"
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmMCP774" ACTION="valMantLedger.aspx?sZone=2">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMCP774()
Else
	Call insPreMCP774Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>	  
</FORM>
</BODY>
</HTML>





