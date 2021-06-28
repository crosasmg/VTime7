<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eInterface" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid


'%insDefineHeader. Definición de columnas del GRID
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	Dim lobjt_err_interface As Object
	
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "GI1406"
	
	'+ Se definen las columns del Grid
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnrowColumnCaption"), "tcnrow", 5, "", True, GetLocalResourceObject("tcnrowColumnToolTip"), False, 0)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnseqColumnCaption"), "tcnseq", 5, "", True, GetLocalResourceObject("tcnseqColumnToolTip"), False, 0)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnerrorColumnCaption"), "tcnerror", 5, "", True, GetLocalResourceObject("tcnerrorColumnToolTip"), False, 0)
		Call .AddTextColumn(0, GetLocalResourceObject("tcsdescriptColumnCaption"), "tcsdescript", 5, vbNullString,  , GetLocalResourceObject("tcsdescriptColumnToolTip"))
	End With
	
	'+ Se asignan las caracteristicas del Grid
	With mobjGrid
		.Codispl = "GI1406"
		.Left = 200
		.Width = 570
		.Height = 230
		
		.ActionQuery = (Request.QueryString.Item("nMainAction") = "401" Or IsNothing(Request.QueryString.Item("nMainAction")))
		.nMainAction = mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble)
		.Columns("Sel").GridVisible = Not .ActionQuery
		.AddButton = False
		.DeleteButton = False
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
End Sub

'%insPreGI1406: Esta función se encarga de cargar los datos en la forma "Folder"
'------------------------------------------------------------------------------
Private Sub insPreGI1406()
	'------------------------------------------------------------------------------
	Dim lcolt_err_interface As eInterface.Errors
	Dim lclst_err_interface As eInterface.ErrorTyp
	Dim lblnFind As Object

	lcolt_err_interface = New eInterface.Errors
	lclst_err_interface = New eInterface.ErrorTyp
	If lcolt_err_interface.Find(session("sKey")) Then
		For	Each lclst_err_interface In lcolt_err_interface
			With mobjGrid
				.Columns("tcnrow").DefValue = CStr(lclst_err_interface.nRow)
				.Columns("tcnseq").DefValue = CStr(lclst_err_interface.nSeq)
				.Columns("tcnerror").DefValue = CStr(lclst_err_interface.nError)
				.Columns("tcsdescript").DefValue = lclst_err_interface.sDescript
			End With
			Response.Write(mobjGrid.DoRow())
		Next lclst_err_interface
	End If
	Response.Write(mobjGrid.closeTable())
	lcolt_err_interface = Nothing
	lclst_err_interface = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "GI1406"
%>
<html>
<head>
   <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></script>
	 <%=mobjValues.WindowsTitle("GI1406")%>
	
<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></script>



    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.setZone(2, "GI1406", "GI1406.aspx"))
		mobjMenu = Nothing
	End If
End With
%>
<script>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 31/10/03 17:16 $"
 
//-------------------------------------------------------------------------------------------------------------------
function insStateZone(){}

//-------------------------------------------------------------------------------------------------------------------
//-------------------------------------------------------------------------------------------------------------------
function insPreZone(llngAction){
//-------------------------------------------------------------------------------------------------------------------
	switch (llngAction){
	    case 301:
	    case 302:
	    case 401:
	        document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + llngAction
	        break;
	}
}
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
	return true;
	
}
</script>		

</head>
<body ONUNLOAD="closeWindows();">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>")
End If
%>

<form METHOD="POST" ID="FORM" NAME="GI1406" ACTION="valinterfaceseq.aspx?">
 <%
Response.Write("<SCRIPT>var	nMainAction	= 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
Response.Write(mobjValues.ShowWindowsName("GI1406"))
Call insDefineHeader()
Call insPreGI1406()
mobjValues = Nothing
%>
</form>
</body>
</html>




