<%@ Page Language="VB" explicit="true" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del grid  
	With mobjGrid.Columns
		Call .AddTextColumn(0, "Chassis", "tctChassis", 30, CStr(eRemoteDB.Constants.strnull))
		Call .AddTextColumn(0, "Motor", "tctMotor", 30, CStr(eRemoteDB.Constants.strnull))
		Call .AddPossiblesColumn(0, "Matricula/Tipo", "cboDescLyctype", "table80", eFunctions.Values.eValuesType.clngComboType, CStr(0),  ,  ,  ,  ,  ,  ,  , "Matricula/Tipo de Vehiculo")
		Call .AddTextColumn(0, "Matricula/Numero", "tcnRegist", 15, "0")
		Call .AddClientColumn(0, "Propietario", "tcnClient", CStr(0),  ,  ,  ,  , "tctClientName")
		Call .AddPossiblesColumn(0, "Codigo", "cboVehCode", "tabtab_au_veh", eFunctions.Values.eValuesType.clngComboType, CStr(0), True,  ,  ,  ,  ,  ,  , "Codigo")
		Call .AddPossiblesColumn(0, "Marca", "cboDescBrand", "table7042", eFunctions.Values.eValuesType.clngComboType, CStr(0),  ,  ,  ,  ,  ,  ,  , "Marca")
		Call .AddTextColumn(0, "Modelo", "tctVehmodel", 20, CStr(eRemoteDB.Constants.strnull))
		Call .AddTextColumn(0, "Color", "tctColor", 20, CStr(eRemoteDB.Constants.strnull))
		Call .AddNumericColumn(0, "Año", "tcnYear", 4, CStr(0),  ,  , False, 0)
		Call .AddPossiblesColumn(0, "Estado", "cbonVestatus", "table220", eFunctions.Values.eValuesType.clngComboType, CStr(0),  ,  ,  ,  ,  ,  ,  , "Estado del Vehiculo")
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "BVC001_k"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
		.Height = 520
		.Width = 400
		.Top = 10
		.Left = 10
	End With
End Sub

'% insPreBVC001: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreBVC001()
	'--------------------------------------------------------------------------------------------
	Dim lclsAuto_db As ePolicy.Auto_db
	Dim lcolAuto_dbs As ePolicy.Auto_dbs
	Dim lCountReg As Short
	Dim lclsacc As Object
	
	lclsAuto_db = New ePolicy.Auto_db
	
	lcolAuto_dbs = New ePolicy.Auto_dbs
	
	'+ Se ejecuta el select preparado
	
	If Not IsNothing(Request.QueryString.Item("Sql")) Then
		If lcolAuto_dbs.FindCondition(session("sql")) Then
			lCountReg = 1
			For	Each lclsAuto_db In lcolAuto_dbs
				With lclsAuto_db
					mobjGrid.Columns("tctChassis").DefValue = .sChassis
					mobjGrid.Columns("tctMotor").DefValue = .sMotor
					mobjGrid.Columns("cboDescLyctype").DefValue = .sLicense_ty '.sRegistW '.sDescLycType ò Registtype
					mobjGrid.Columns("tcnRegist").DefValue = .sRegist
					mobjGrid.Columns("tcnClient").DefValue = .sClient
					mobjGrid.Columns("cboVehCode").DefValue = .sVehCode
					mobjGrid.Columns("cboDescBrand").DefValue = CStr(.nVehType) 'sDescript 
					mobjGrid.Columns("tctVehmodel").DefValue = .sVehModel
					mobjGrid.Columns("tctColor").DefValue = .sColor
					mobjGrid.Columns("tcnYear").DefValue = CStr(.nYear)
					mobjGrid.Columns("cbonVestatus").DefValue = CStr(.nVestatus)
					
					Response.Write(mobjGrid.DoRow())
					
				End With
				lCountReg = lCountReg + 1
				If lCountReg = 200 Then
					Exit For
				End If
			Next lclsAuto_db
			'		else
		End If
	End If
	Response.Write(mobjGrid.closeTable())
	
	'UPGRADE_NOTE: Object lclsAuto_db may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsAuto_db = Nothing
	'UPGRADE_NOTE: Object lcolAuto_dbs may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lcolAuto_dbs = Nothing
End Sub

'------------------------------------------------------------------------------------------------------------------
Private Sub insPreBVC001Upd()
	'------------------------------------------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("    <P ALIGN=""Center"">" & vbCrLf)
Response.Write("    </P>" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD WIDTH=""100%"" COLSPAN=""4"">&nbsp;</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""4"" ><LABEL><A NAME=""Consulta de Base de Datos de Vehiculo"">Consulta de Base de Datos de Vehiculo</A></LABEL>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD WIDTH=""100%"" COLSPAN=""5""><HR></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("")

	
	mobjGrid.Columns("cboVehCode").Parameters.Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValBranchQue.aspx", "BVC001", Request.QueryString.Item("nMainAction"), False, -1))
End Sub

</script>
<%
Response.Expires = 0

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

%>
<HTML>
<HEAD>
   <%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	%>
	<%	'$$EWI_1012:D:\VisualTIMEChile\Result\VTimeStep1\templates\branches\branchque\Vtime\Scripts\tMenu.js#%>
<%	'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%	'UPGRADE_NOTE: The file '/VTimeNet/Scripts/tMenu.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<%	
End If
%>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
     <%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'vtime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

    <%'=mobjValues.StyleSheet()%>
    <SCRIPT>
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
    var lintIndex = 0;
    for (lintIndex=0;lintIndex<document.forms[0].length;lintIndex++)
        document.forms[0].elements[lintIndex].disabled = false
    EditRecord(-1, nMainAction,'Add')
}
</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjValues.WindowsTitle("BVC001"))
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), "BVC001.aspx"))
		.Write(mobjMenu.MakeMenu("BVC001", "BVC001_k.aspx", 2, ""))
		.Write("<SCRIPT>var nMainAction=top.frames[""fraSequence""].plngMainAction</SCRIPT>")
	End If
End With
'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing
%>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmQDBVehicle" ACTION="ValBranchQue.aspx?Zone=1">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>")
End If
Response.Write(mobjValues.ShowWindowsName("BVC001"))

Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR>")
	Call insPreBVC001()
Else
	Call insPreBVC001Upd()
End If
'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjGrid = Nothing
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
%>     
</FORM>
</BODY>
</HTML>




