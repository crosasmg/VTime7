<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
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
	
	mobjGrid.sCodisplPage = "dp039"
	
	'+ Se definen las columnas del grid.
	With mobjGrid.Columns
		Call .AddNumericColumn(41217, GetLocalResourceObject("tcnCovergenColumnCaption"), "tcnCovergen", 5, CStr(0),  , GetLocalResourceObject("tcnCovergenColumnCaption"))
		Call .AddTextColumn(41218, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, vbNullString,  , GetLocalResourceObject("tctDescriptColumnCaption"))
		Call .AddCheckColumn(41219, GetLocalResourceObject("chkLifeColumnCaption"), "chkLife", "",  ,  ,  , True, GetLocalResourceObject("chkLifeColumnToolTip"))
	End With
	'+ Se definen las propiedades generales del grid.
	With mobjGrid
		.Codispl = "DP039"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
		.ActionQuery = True
		If mobjValues.StringToType(Session("nTypCov"), eFunctions.Values.eTypeData.etdDouble) = 3 Then
			.Columns("chkLife").GridVisible = True
		Else
			.Columns("chkLife").GridVisible = False
		End If
	End With
End Sub
'% insPreDP039: Permite setear los objetos a ser utilizados y realiza la lectura del o los registros
'% a ser mostrados en el grid.
'--------------------------------------------------------------------------------------------
Private Sub insPreDP039()
	'--------------------------------------------------------------------------------------------
	Dim lintCount As Short
	Dim lobjTab_cov As eProduct.Tab_gencov
	Dim lobjObject As Object
	Dim lcolObj As Object
	lobjTab_cov = New eProduct.Tab_gencov
	lcolObj = lobjTab_cov.insPreDP039(mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nTypCov"), eFunctions.Values.eTypeData.etdDouble))
	lintCount = 0
	For	Each lobjObject In lcolObj
		With lobjObject
			mobjGrid.Columns("tcnCovergen").DefValue = .nCovergen
			mobjGrid.Columns("tctDescript").DefValue = .sDescript
			If mobjValues.StringToType(Session("nTypCov"), eFunctions.Values.eTypeData.etdDouble) = 3 Then
				If .sCheck = 1 Then
					mobjGrid.Columns("chkLife").Checked = 1
				Else
					mobjGrid.Columns("chkLife").Checked = 2
				End If
			End If
			Response.Write(mobjGrid.DoRow())
		End With
		lintCount = lintCount + 1
		If lintCount = 200 Then
			Exit For
		End If
	Next lobjObject
	Response.Write(mobjGrid.closeTable())
	lobjTab_cov = Nothing
	lobjObject = Nothing
	lcolObj = Nothing
End Sub

</script>
<%
Response.Expires = 0
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "dp039"
%>
<SCRIPT LANGUAGE="JavaScript">
//- Variable para el control de versiones
       document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:56 $"

//%insCancel: Permite cancelar la página invocada.
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
    <%Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.setZone(2, "DP039", "DP039.aspx"))
If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
	mobjValues.ActionQuery = True
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="DP039" ACTION="ValProduct.aspx?Zone=2">
<%
Response.Write(mobjValues.ShowWindowsName("DP039"))
Call insDefineHeader()
Call insPreDP039()
mobjGrid = Nothing
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>





