<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo del grid    
Dim mobjGrid As eFunctions.Grid


'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "ms002"
	
	'+ Se definen las columns del Grid
	
	With mobjGrid.Columns
		Call .AddNumericColumn(41679, GetLocalResourceObject("tcnCodigoColumnCaption"), "tcnCodigo", 6, CStr(eRemoteDB.Constants.strNull), True,  ,  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddPossiblesColumn(41677, GetLocalResourceObject("cbeErrorTypeColumnCaption"), "cbeErrorType", "Table153", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeErrorTypeColumnToolTip"))
		Call .AddNumericColumn(41680, GetLocalResourceObject("tcnnivelColumnCaption"), "tcnnivel", 3, CStr(eRemoteDB.Constants.strNull), True)
		Call .AddPossiblesColumn(41678, GetLocalResourceObject("cbeStaterrColumnCaption"), "cbeStaterr", "Table26", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeStaterrColumnCaption"))
		Call .AddTextColumn(41681, GetLocalResourceObject("tctCausaColumnCaption"), "tctCausa", 80, CStr(eRemoteDB.Constants.strNull))
	End With
	
	'+ Se asignan las caracteristicas del Grid
	
	With mobjGrid
		.Codispl = "MS002"
		.Codisp = "MS002"
		Select Case Request.QueryString.Item("nMainAction")
			Case "401"
				mobjGrid.ActionQuery = True
				mobjGrid.Columns("Sel").GridVisible = False
				.Columns("cbeErrorType").EditRecord = False
			Case "302"
				.DeleteButton = True
				.AddButton = True 'False
				.Columns("Sel").GridVisible = True
				.Columns("cbeErrorType").EditRecord = True
			Case "301"
				.DeleteButton = True
				.AddButton = True
				.Columns("Sel").GridVisible = True
				.Columns("cbeErrorType").EditRecord = True
			Case Else
				.DeleteButton = False
				.AddButton = False
				.ActionQuery = True
				.Columns("Sel").GridVisible = False
				.Columns("cbeErrorType").EditRecord = False
		End Select
		
		.sDelRecordParam = "sCodisp=" & Session("sCodispl") & "&nErrornum='+ marrArray[lintIndex].tcnCodigo + '" & "&nErrorType='+ marrArray[lintIndex].cbeErrorType + '" & "&nnivel='+marrArray[lintIndex].tcnnivel + '" & "&nStaterr='+marrArray[lintIndex].cbeStaterr + '" & "&sCausa='+marrArray[lintIndex].tctCausa + '"
		.Height = 300
		.Width = 350
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
End Sub
'------------------------------------------------------------------------------
Private Sub insPreMS002()
	'------------------------------------------------------------------------------
	Dim lcolWinMessags As eGeneral.WinMessags
	Dim lclsWinMessag As eGeneral.WinMessag
	
	lclsWinMessag = New eGeneral.WinMessag
	lcolWinMessags = New eGeneral.WinMessags
	
	If lcolWinMessags.Find(Session("sCodisp")) Then
		For	Each lclsWinMessag In lcolWinMessags
			With mobjGrid
				.Columns("tcnCodigo").DefValue = CStr(lclsWinMessag.nErrorNum)
				.Columns("cbeErrorType").DefValue = mobjValues.StringToType(lclsWinMessag.sErrortyp, eFunctions.Values.eTypeData.etdDouble)
				.Columns("tcnnivel").DefValue = CStr(lclsWinMessag.nLevel)
				.Columns("cbeStaterr").DefValue = mobjValues.StringToType(lclsWinMessag.sStatregt, eFunctions.Values.eTypeData.etdDouble)
				.Columns("tctCausa").DefValue = lclsWinMessag.sAction_err
				
				Response.Write(.DoRow)
				
			End With
			
			'+Se ejecuta el metodo DoRow, que se encarga de mostrar los elementos de grid
		Next lclsWinMessag
	End If
	Response.Write(mobjGrid.closeTable())
End Sub
'------------------------------------------------------------------------------
Private Sub insPreMS002Upd()
	'------------------------------------------------------------------------------
	Dim lclsWinMessag As eGeneral.WinMessag
	Dim lstrErrors As Object
	
	If Request.QueryString.Item("Action") = "Del" Then
		
		lclsWinMessag = New eGeneral.WinMessag
		
		Response.Write(mobjValues.ConfirmDelete())
		With lclsWinMessag
			.sCodispl = Session("sCodisp")
			.nErrorNum = mobjValues.StringToType(Request.QueryString.Item("nErrornum"), eFunctions.Values.eTypeData.etdDouble)
			.Delete()
		End With
		
		lclsWinMessag = Nothing
	Else
		Response.Write(mobjValues.ShowWindowsName("MS002"))
	End If
	
	With Response
		.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValMantSys.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("MainAction"),  , CShort(Request.QueryString.Item("Index"))))
		.Write(mobjValues.HiddenControl("sAction", Request.QueryString.Item("Action")))
	End With
End Sub

</script>
<%Response.Expires = 0
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjGrid = New eFunctions.Grid

mobjValues.sCodisplPage = "ms002"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
	<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">




<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "MS002", "MS002.aspx"))
End If
%>
<SCRIPT>
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
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">

<FORM METHOD="POST" ID="FORM" NAME="frmMesWin" ACTION="valmantsys.aspx?mode=1">
<%
Response.Write("<SCRIPT>var	nMainAction	= 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMS002()
Else
	Call insPreMS002Upd()
End If

mobjMenu = Nothing
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>




