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
	mobjGrid.sCodisplPage = "SGC001"
	
	'+ Se definen las columnas del grid  
	
	With mobjGrid.Columns
		Call .AddNumericColumn(100457, GetLocalResourceObject("tcnUsercodeColumnCaption"), "tcnUsercode", 5, CStr(0),  , GetLocalResourceObject("tcnUsercodeColumnToolTip"))
		Call .AddTextColumn(100457, GetLocalResourceObject("tctClienameColumnCaption"), "tctCliename", 40, vbNullString,  , GetLocalResourceObject("tctClienameColumnToolTip"))
		Call .AddTextColumn(100458, GetLocalResourceObject("tctOfficeColumnCaption"), "tctOffice", 30, vbNullString,  , GetLocalResourceObject("tctOfficeColumnToolTip"))
		Call .AddTextColumn(100459, GetLocalResourceObject("tctDepartmenColumnCaption"), "tctDepartmen", 30, vbNullString,  , GetLocalResourceObject("tctDepartmenColumnToolTip"))
		Call .AddTextColumn(100460, GetLocalResourceObject("tctSche_codeColumnCaption"), "tctSche_code", 6, vbNullString,  , GetLocalResourceObject("tctSche_codeColumnToolTip"))
	End With
	
	'+ Se definen las propiedades generales del grid.
	
	With mobjGrid
		.Codispl = "SGC001"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
		.ActionQuery = True
	End With
End Sub

'% insPreSGC001: Se cargan los controles de la página.
'--------------------------------------------------------------------------------------------
Private Sub insPreSGC001()
	'--------------------------------------------------------------------------------------------
	Dim lintCount As Object
	Dim lcolUsers As eSecurity.Users
	Dim lobjObject As Object
	
	lcolUsers = New eSecurity.Users
	
	If lcolUsers.insConstructUsers(mobjValues.StringToType(Session("nOffice"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nDepartmen"), eFunctions.Values.eTypeData.etdDouble), Session("sSchema")) Then
		
		For	Each lobjObject In lcolUsers
			With lobjObject
				mobjGrid.Columns("tcnUsercode").DefValue = .nUsercode
				mobjGrid.Columns("tctCliename").DefValue = .sCliename
				mobjGrid.Columns("tctOffice").DefValue = .sOffice
				mobjGrid.Columns("tctDepartmen").DefValue = .sDepartme
				mobjGrid.Columns("tctSche_code").DefValue = .sSche_code
				
				Response.Write(mobjGrid.DoRow())
			End With
		Next lobjObject
	End If
	
	Response.Write(mobjGrid.closeTable())
	
	lcolUsers = Nothing
	lobjObject = Nothing
End Sub

</script>
<%
Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "SGC001"

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
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.setZone(2, "SGC001", "SGC001.aspx"))

If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
	mobjValues.ActionQuery = True
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="SGC001" ACTION="ValSecurityQue.aspx?Zone=2">
<%
Response.Write(mobjValues.ShowWindowsName("SGC001"))

Call insDefineHeader()
Call insPreSGC001()

mobjGrid = Nothing
mobjValues = Nothing
%>     
</FORM>
</BODY>
</HTML>




