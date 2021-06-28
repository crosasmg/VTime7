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
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnVoucherColumnCaption"), "tcnVoucher", 6, "", False, GetLocalResourceObject("tcnVoucherColumnToolTip"), False, 0,  ,  ,  , False)
		Call .AddTextColumn(0, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, "", False, GetLocalResourceObject("tctDescriptColumnToolTip"),  ,  ,  , False)
		Call .AddTextColumn(0, GetLocalResourceObject("tctFile_FIN700ColumnCaption"), "tctFile_FIN700", 12, "", False, GetLocalResourceObject("tctFile_FIN700ColumnToolTip"),  ,  ,  , False)
		Call .AddDateColumn(0, GetLocalResourceObject("tcdDate_FIN700ColumnCaption"), "tcdDate_FIN700", "",  , GetLocalResourceObject("tcdDate_FIN700ColumnToolTip"),  ,  ,  , False)
		Call .AddHiddenColumn("hddVoucher", CStr(0))
	End With
	
	With mobjGrid
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Codispl = "MCP776"
		.Codisp = "MCP776"
		.sCodisplPage = "MCP776"
		.Top = 100
		.Height = 256
		.Width = 625
		.Columns("Sel").Title = "Sel"
		.AddButton = False
		.DeleteButton = False
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'%insPreMCP776. Se crea la ventana madre (Principal)
'------------------------------------------------------------------------------
Private Sub insPreMCP776()
	'------------------------------------------------------------------------------
	Dim lclsFIN700_lines As eLedge.Fin700_Lines
	Dim lintCount As Integer
	
	With Request
		lclsFIN700_lines = New eLedge.Fin700_Lines
		With mobjGrid
			
			If lclsFIN700_lines.FindMCP776(mobjValues.StringToType(Request.QueryString.Item("nLed_compan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nLed_year"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nLed_Month"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sShowVoucher")) Then
				For lintCount = 1 To lclsFIN700_lines.CountMCP776
					If lclsFIN700_lines.ItemMCP776(lintCount) Then
						If Request.QueryString.Item("sShowVoucher") = "2" Then
							.Columns("Sel").Disabled = True
						End If
						.Columns("tcnVoucher").DefValue = CStr(lclsFIN700_lines.nVoucher)
						.Columns("tctDescript").DefValue = lclsFIN700_lines.sDescript
						.Columns("tctFile_FIN700").DefValue = lclsFIN700_lines.sFile_FIN700
						.Columns("tcdDate_FIN700").DefValue = CStr(lclsFIN700_lines.dDate_FIN700)
						.Columns("hddVoucher").DefValue = CStr(lclsFIN700_lines.nVoucher)
						Response.Write(mobjGrid.DoRow())
					End If
				Next 
			End If
		End With
		
	End With
	Response.Write(mobjGrid.CloseTable())
	
	lclsFIN700_lines = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MCP776"
%>
<HTML>
  <HEAD>
	<META NAME="GENERATOR" CONTENT="eTransaction Designer for Visual TIME">




<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%
mobjValues.ActionQuery = (Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery))
With Response
	.Write(mobjValues.StyleSheet())
	.Write("<SCRIPT>var	nMainAction	= " & CShort("0" & Request.QueryString.Item("nMainAction")) & "</SCRIPT>")
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.setZone(2, "MCP776", "MCP776.aspx"))
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
<FORM METHOD="POST" ID="FORM" NAME="frmMCP776" ACTION="valMantLedger.aspx?sZone=2&nLed_compan=<%=Request.QueryString.Item("nLed_compan")%>" >
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Call insDefineHeader()
Call insPreMCP776()

mobjValues = Nothing
mobjGrid = Nothing
%>	  
</FORM>
</BODY>
</HTML>





