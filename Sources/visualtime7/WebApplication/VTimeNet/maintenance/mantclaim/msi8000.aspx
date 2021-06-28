<%@ Page Language="VB" explicit="true" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo particular de los datos de la página
Dim mcolTar_Disabilitys As eClaim.Tar_Disabilitys


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		If Request.QueryString("Type") <> "PopUp" Then
			Call .AddTextColumn(0, "Código", "tctCovergen", 3, vbNullString,  , "Código de la cobertura genérica")
		End If
		
		Call .AddPossiblesColumn(0, "Cobertura genérica", "tcnCovergen", "tabTab_LifCov", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  , Request.QueryString("Action") = "Update", 4, "Cobertura genérica asociada a la tasa de indemnización de invalidez")
		
		If Request.QueryString("Type") <> "PopUp" Then
			Call .AddTextColumn(0, "Abreviado", "tctShort_Des", 12, vbNullString,  , "Descripción corta de la cobertura genérica")
		End If
		
		Call .AddPossiblesColumn(0, "Forma de invalidez", "tcnDisability", "Table5505", eFunctions.Values.eValuesType.clngComboType, "2",  ,  ,  ,  ,  , Request.QueryString("Action") = "Update",  , "Forma de invalidez asociada a la tasa")
		Call .AddNumericColumn(0, "Porcentaje de indemnización", "tcnRate", 9, vbNullString,  , "Porcentaje de indemnización", False, 6)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "MSI8000"
		.sCodisplPage = "MSI8000"
		.ActionQuery = mobjValues.ActionQuery
		
		If Request.QueryString("Type") <> "PopUp" Then
			.Columns("tctCovergen").EditRecord = False
			.Columns("tctShort_des").EditRecord = False
			
			.Columns("tctCovergen").GridVisible = True
			.Columns("tctShort_des").GridVisible = True
		End If
		
		.Columns("tcnCovergen").EditRecord = True
		.Height = 220
		.Width = 620
		.nMainAction = Request.QueryString("nMainAction")
		.Columns("Sel").GridVisible = Not .ActionQuery
		.sDelRecordParam = "nCovergen=' + marrArray[lintIndex].tcnCovergen + '"
		If Request.QueryString("Reload") = "1" Then
			.sReloadIndex = Request.QueryString("ReloadIndex")
		End If
		.Columns("tcnCovergen").Parameters.Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("tcnCovergen").Parameters.Add("nCoverGen", mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With
End Sub

'% insPreMSI8000: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMSI8000()
	'--------------------------------------------------------------------------------------------
	Dim lclsTar_Disability As eClaim.Tar_Disability
	
	mcolTar_Disabilitys = New eClaim.Tar_Disabilitys
	
	If mcolTar_Disabilitys.Find(mobjValues.StringToType(CStr(Session("nBranch")), eFunctions.Values.eTypeData.etdDouble), CDate(Session("dEffecdate"))) Then
		For	Each lclsTar_Disability In mcolTar_Disabilitys
			With mobjGrid
				.Columns("tcnCovergen").DefValue = CStr(lclsTar_Disability.nCovergen)
				.Columns("tcnDisability").DefValue = CStr(lclsTar_Disability.nDisability)
				.Columns("tcnRate").DefValue = CStr(lclsTar_Disability.nRate)
				.Columns("tctCovergen").DefValue = CStr(lclsTar_Disability.nCovergen)
				.Columns("tctShort_des").DefValue = lclsTar_Disability.sShort_Des
				Response.Write(.DoRow)
			End With
		Next lclsTar_Disability
	End If
	
	Response.Write(mobjGrid.closeTable())
End Sub

'% insPreMSI8000Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMSI8000Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsTar_Disability As eClaim.Tar_Disability
	
	
	With Request
		If Request.QueryString("Action") = "Del" Then
			lclsTar_Disability = New eClaim.Tar_Disability
			
			Response.Write(mobjValues.ConfirmDelete())
			Call lclsTar_Disability.InsPostMSI8000(.QueryString("Action"), mobjValues.StringToType(CStr(Session("nBranch")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nCovergen"), eFunctions.Values.eTypeData.etdDouble), CDate(Session("dEffecdate")), mobjValues.StringToType(.QueryString("nDisability"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble))
			
			'UPGRADE_NOTE: Object lclsTar_Disability may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
			lclsTar_Disability = Nothing
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString("Action"), "valmantclaim.aspx", "MSI8000", .QueryString("nMainAction"), mobjValues.ActionQuery, .QueryString("Index")))
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = Request.QueryString("nMainAction") = 401
mobjValues.sCodisplPage = "MSI8000"
%>
<HTML>
<HEAD>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0">
<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'VTime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE=JavaScript>
//- Variable para el control de versiones
       document.VssVersion="$$Revision: 1 $|$$Date: 26-12-11 15:27 $"
</SCRIPT>    
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "MSI8000", "MSI8000.aspx"))
	'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MSI8000.aspx" ACTION="valmantclaim.aspx?sMode=2">
<%Response.Write(mobjValues.ShowWindowsName("MSI8000"))
Call insDefineHeader()

If Request.QueryString("Type") = "PopUp" Then
	Call insPreMSI8000Upd()
Else
	Call insPreMSI8000()
End If
%>
</FORM> 
</BODY>
</HTML>




