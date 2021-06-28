<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSecurity" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores.

Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader: Se definen los campos del grid.
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	mobjGrid.sCodisplPage = "SGC002"
	
	'+ Se definen las columnas del grid  
	
	With mobjGrid.Columns
		Call .AddTextColumn(100461, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 40, vbNullString,  , GetLocalResourceObject("tctDescriptColumnCaption"))
		Call .AddPossiblesColumn(100461, GetLocalResourceObject("cbeModulesColumnCaption"), "cbeModules", "Table87", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeModulesColumnCaption"))
		Call .AddTextColumn(100462, GetLocalResourceObject("tctCodisplColumnCaption"), "tctCodispl", 8, vbNullString,  , GetLocalResourceObject("tctCodisplColumnCaption"))
		Call .AddTextColumn(100463, GetLocalResourceObject("tctCodispColumnCaption"), "tctCodisp", 8, vbNullString,  , GetLocalResourceObject("tctCodispColumnCaption"))
		Call .AddTextColumn(100464, GetLocalResourceObject("tctPseudoColumnCaption"), "tctPseudo", 12, vbNullString,  , GetLocalResourceObject("tctPseudoColumnCaption"))
	End With
	
	'+ Se definen las propiedades generales del grid.
	
	With mobjGrid
		.Codispl = "SGC002"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
		.ActionQuery = True
		
		If mobjValues.StringToType(Session("nModules"), eFunctions.Values.eTypeData.etdDouble) <> 0 Then
			.Columns("cbeModules").GridVisible = False
		Else
			.Columns("cbeModules").GridVisible = True
		End If
	End With
End Sub

'% insPreSGC002: Se cargan los controles de la página.
'--------------------------------------------------------------------------------------------
Private Sub insPreSGC002()
	'--------------------------------------------------------------------------------------------
	Dim lintCount As Short
	Dim lcolWindowss As eSecurity.Windowss
	Dim lobjObject As Object
	Dim lintIndex As Object
	
	lcolWindowss = New eSecurity.Windowss
	
	If lcolWindowss.insConstructWindows(mobjValues.StringToType(Session("nModules"), eFunctions.Values.eTypeData.etdDouble), Session("sCodispLog"), Session("sCodisp"), Session("sPseudo")) Then
		
		lintCount = 0
		
		For	Each lobjObject In lcolWindowss
			With lobjObject
				mobjGrid.Columns("tctDescript").DefValue = .sDescript
				
				If mobjValues.StringToType(Session("nModules"), eFunctions.Values.eTypeData.etdDouble) <> 0 Then
					mobjGrid.Columns("cbeModules").GridVisible = False
					mobjGrid.Columns("cbeModules").DefValue = CStr(0)
				Else
					mobjGrid.Columns("cbeModules").GridVisible = True
					mobjGrid.Columns("cbeModules").DefValue = .nModules
				End If
				
				mobjGrid.Columns("tctCodispl").DefValue = .sCodispl
				mobjGrid.Columns("tctCodisp").DefValue = .sCodisp
				mobjGrid.Columns("tctPseudo").DefValue = .sPseudo
				
				Response.Write(mobjGrid.DoRow())
			End With
			
			lintCount = lintCount + 1
			
		Next lobjObject
	End If
	
	Response.Write(mobjGrid.closeTable())
	
	lcolWindowss = Nothing
	lobjObject = Nothing
End Sub

</script>
<%
Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "SGC002"
mobjMenu = New eFunctions.Menues
%>
<SCRIPT LANGUAGE="JavaScript">
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
    <%
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.setZone(2, "SGC002", "SGC002.aspx"))

If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
	mobjValues.ActionQuery = True
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="SGC002" ACTION="ValSecurityQue.aspx?Zone=2">
<%
Response.Write(mobjValues.ShowWindowsName("SGC002"))

Call insDefineHeader()
Call insPreSGC002()

mobjGrid = Nothing
mobjValues = Nothing
%>     
</FORM>
</BODY>
</HTML>




