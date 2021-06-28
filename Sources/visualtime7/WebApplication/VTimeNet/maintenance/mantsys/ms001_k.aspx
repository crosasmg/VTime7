<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

Dim mobjMenu As eFunctions.Menues

Dim mobjGrid As eFunctions.Grid


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------
	Dim lclsMessages As eGeneral.Messages
	lclsMessages = New eGeneral.Messages
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del grid      	    	   	
	With mobjGrid.Columns
		If Request.QueryString.Item("Action") = "Add" Then
			Call .AddNumericColumn(41339, GetLocalResourceObject("tcnCodeColumnCaption"), "tcnCode", 6, vbNullString,  , GetLocalResourceObject("tcnCodeColumnToolTip"),  ,  ,  ,  ,  , False)
		Else
			Call .AddNumericColumn(41339, GetLocalResourceObject("tcnCodeColumnCaption"), "tcnCode", 6, vbNullString,  , GetLocalResourceObject("tcnCodeColumnToolTip"),  ,  ,  ,  ,  , True)
		End If
		Call .AddTextColumn(41676, GetLocalResourceObject("tctMessageColumnCaption"), "tctMessage", 80, vbNullString,  , GetLocalResourceObject("tctMessageColumnToolTip"))
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "MS001"
		.Codisp = "MS001"
		If Request.QueryString.Item("nMainAction") = "401" Or Request.QueryString.Item("nMainAction") = vbNullString Then
			mobjGrid.ActionQuery = True
			mobjGrid.Columns("Sel").GridVisible = False
		Else
			.Columns("tctMessage").EditRecord = True
		End If
		
		.Height = 210
		.Width = 420
		.Top = 100
		.WidthDelete = 500
		
		.sDelRecordParam = "sCodisp=" & Session("sCodispl") & "&nErrornum='+ marrArray[lintIndex].tcnCode + '" & "&sMessaged='+ marrArray[lintIndex].tctMessage + '"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		
	End With
	
	lclsMessages = Nothing
End Sub

'% insPreMS001: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreMS001()
	'--------------------------------------------------------------------------------------------
	Dim lclsAddress As Object
	Dim lcolAddress As Object
	Dim lclsMessage As eGeneral.Message
	Dim lclsMessages As eGeneral.Messages
	
	lclsMessage = New eGeneral.Message
	lclsMessages = New eGeneral.Messages
	
	'If Session("nUsercode") = "4835" Then
	'    Response.Write "<NOTSCRIPT> alert (""" & "Entro uno : " &  """) </" & "Script>"
	'End If		
	
	With Request
		If lclsMessages.Find(mobjValues.StringToType(vbNullString, eFunctions.Values.eTypeData.etdDouble), vbNullString) Then
			
			'If Session("nUsercode") = "4835" Then
			'    Response.Write "<NOTSCRIPT> alert (""" & "Encontre: " &  """) </" & "Script>"
			'End If			                         
			
			For	Each lclsMessage In lclsMessages
				'If Session("nUsercode") = "4835" Then
				'    Response.Write "<NOTSCRIPT> alert (""" & "En For: " &  """) </" & "Script>"
				'End If	
				With mobjGrid
					.Columns("tcnCode").DefValue = CStr(lclsMessage.nErrorNum)
					.Columns("tctMessage").DefValue = lclsMessage.sMessaged
					Response.Write(mobjGrid.DoRow())
				End With
			Next lclsMessage
		Else
			
			'If Session("nUsercode") = "4835" Then
			'    Response.Write "<NOTSCRIPT> alert (""" & "No Entro: " & """) </" & "Script>"
			'End If	
			With mobjGrid
				.DeleteButton = False
				.AddButton = False
				.ActionQuery = True
				.Columns("Sel").GridVisible = False
				.Columns("tctMessage").EditRecord = False
			End With
		End If
	End With
	Response.Write(mobjGrid.closeTable())
	lclsMessage = Nothing
	lclsMessages = Nothing
	
End Sub

Private Sub insPreMS001Upd()
	
	Dim lclsMessage As eGeneral.Message
	Dim lstrErrors As String
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			lclsMessage = New eGeneral.Message
			lstrErrors = lclsMessage.insValMS001_K(.QueryString.Item("sCodispl"), mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("Action"), mobjValues.StringToType(.QueryString.Item("nErrornum"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("sMessaged"))
			If lstrErrors = vbNullString Then
				Response.Write(mobjValues.ConfirmDelete())
				With lclsMessage
					.nErrorNum = mobjValues.StringToType(Request.QueryString.Item("nErrornum"), eFunctions.Values.eTypeData.etdDouble)
					.Delete()
				End With
			Else
				Response.Write(lstrErrors)
			End If
			lclsMessage = Nothing
		End If
	End With
	
	With Response
		.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valmantsys.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"),  , CShort(Request.QueryString.Item("Index"))))
		.Write(mobjValues.HiddenControl("sAction", Request.QueryString.Item("Action")))
	End With
End Sub

</script>
<%
Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjGrid = New eFunctions.Grid
%>

<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




<SCRIPT LANGUAGE=JavaScript>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:29 $|$$Author: Nvaplat61 $"

//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------	
}

//% insPreZone: Define ubicacion de documento
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

//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
    return true;
}

</SCRIPT>
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<SCRIPT>var nMainAction=0</SCRIPT>")
	Response.Write("<SCRIPT LANGUAGE=""JavaScript"" SRC=""/VTimeNet/Scripts/tmenu.js""></SCRIPT>" & vbCrLf)
End If
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MS001_K.aspx", 1, ""))
		Response.Write("<BR></BR>")
		mobjMenu = Nothing
	End If
End With

%>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MS001" ACTION="valmantsys.aspx?sMode=2">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreMS001Upd()
Else
	Call insPreMS001()
End If
mobjValues = Nothing
mobjGrid = Nothing

%>
</FORM>
</BODY>
</HTML>




