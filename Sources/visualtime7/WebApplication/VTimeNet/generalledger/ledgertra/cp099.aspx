<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eLedge" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid


'% insDefineHeader: Define las columnas del Grid
'-----------------------------------------------
Private Sub insDefineHeader()
	'-----------------------------------------------
	
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "CP009"
	
	'+ Se definen todas las columnas del Grid
	
	With mobjGrid.Columns
		Call .AddTextColumn(0, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 15, "",  , GetLocalResourceObject("tctDescriptColumnToolTip"))
	End With
	
	With mobjGrid
		.AddButton = False
		.Top = 70
		.Codispl = "CP099"
		.Width = 330
		.Height = 400
		.Columns("Sel").GridVisible = True
	End With
End Sub

'% insPreCP099: Carga los datos en le grid de la forma "Folder" 
'--------------------------------------------------------------
Private Sub insPreCP099()
	Dim lclsLed_compans As Object
	'--------------------------------------------------------------
	
	Dim lclsLed_compan As Object
	Dim lcolLed_compans As eLedge.Led_compans
	
	lcolLed_compans = New eLedge.Led_compans
	
	If lcolLed_compans.Find(True) Then
		For	Each lclsLed_compans In lcolLed_compans
			With mobjGrid
				.Columns("tctDescript").DefValue = lclsLed_compans.sDescript
				Response.Write(.DoRow)
			End With
		Next lclsLed_compans
	End If
	
	'+ Se llama a la propiedad CloseTable, para dar por finalizada la creación de la tabla (Grid)
	
	Response.Write(mobjGrid.CloseTable())
	
	lcolLed_compans = Nothing
	lclsLed_compan = Nothing
End Sub

</script>
<%Response.Expires = -1

'- Objeto para el manejo de las rutinas genéricas
mobjValues = New eFunctions.Values
%>

<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


	<%=mobjValues.StyleSheet()%>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="CP099" ACTION="valLedgerTra.aspx">

<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Call insDefineHeader()
Call insPreCP099()

mobjGrid = Nothing
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>





