<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneralQue" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid


'-----------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'-----------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "MGE001"
	
	'+ Se definen las columnas del Grid
	
	With mobjGrid.Columns
		Call .AddNumericColumn(40608, GetLocalResourceObject("tcnIdPropertyColumnCaption"), "tcnIdProperty", 3, vbNullString, False, GetLocalResourceObject("tcnIdPropertyColumnCaption"))
		Call .AddTextColumn(40609, GetLocalResourceObject("tctPropertyColumnCaption"), "tctProperty", 20, vbNullString, False, GetLocalResourceObject("tctPropertyColumnCaption"))
		Call .AddTextColumn(40610, GetLocalResourceObject("tctFormatColumnCaption"), "tctFormat", 20, vbNullString, False, GetLocalResourceObject("tctFormatColumnCaption"))
	End With
	
	
	With mobjGrid
		.Columns("tcnIdProperty").Disabled = Not (Request.QueryString.Item("Action") = "Add")
		
		If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or Request.QueryString.Item("nMainAction") = vbNullString Then
			.Columns("Sel").GridVisible = False
			.ActionQuery = True
		End If
		
		'If Request.QueryString("nMainAction")= clngActionUpdate Then
		.Columns("tctProperty").EditRecord = True
		.AddButton = True
		.DeleteButton = True
		.Codispl = "MGE001"
		.Codisp = "MGE001_K"
		.Height = 220
		.Width = 320
		.sDelRecordParam = "nIdProperty=' + marrArray[lintIndex].tcnIdProperty + '"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		'End If
	End With
End Sub

'-----------------------------------------------------------------------------------------
Private Sub insPreMGE001()
	'-----------------------------------------------------------------------------------------
	Dim lcolPropertyLibrary As eGeneralQue.PropertyLibraries
	Dim lclsPropertyLibrary As Object
	
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function insPreZone(llngAction){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("	switch (llngAction){" & vbCrLf)
Response.Write("	    case 301:" & vbCrLf)
Response.Write("	    case 302:" & vbCrLf)
Response.Write("	    case 401:" & vbCrLf)
Response.Write("	        document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + llngAction" & vbCrLf)
Response.Write("	        break;" & vbCrLf)
Response.Write("	}" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("</" & "SCRIPT><BR><BR>")

	
	lcolPropertyLibrary = New eGeneralQue.PropertyLibraries
	If lcolPropertyLibrary.Find() Then
		For	Each lclsPropertyLibrary In lcolPropertyLibrary
			With lclsPropertyLibrary
				mobjGrid.Columns("tcnIdProperty").DefValue = lclsPropertyLibrary.nIdProperty
				mobjGrid.Columns("tctProperty").DefValue = lclsPropertyLibrary.sProperty
				mobjGrid.Columns("tctFormat").DefValue = lclsPropertyLibrary.sFormat
			End With
			Response.Write(mobjGrid.DoRow())
		Next lclsPropertyLibrary
	End If
	lcolPropertyLibrary = Nothing
	Response.Write(mobjGrid.closeTable())
End Sub

'-----------------------------------------------------------------------------------------
Private Sub insPreMGE001Upd()
	'-----------------------------------------------------------------------------------------
	Dim lclsPropertyLibrary As eGeneralQue.PropertyLibrary
	
	lclsPropertyLibrary = New eGeneralQue.PropertyLibrary
	
	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
	
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			lclsPropertyLibrary.insPostMGE001(.QueryString.Item("Action"), mobjValues.StringToType(.QueryString.Item("nIdProperty"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("tctProperty"), .QueryString.Item("tctFormat"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
			
		End If
	End With
	
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValMantGeneralQue.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"),  , CShort(Request.QueryString.Item("Index"))))
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "MGE001"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	%>
		<%	'$$EWI_1012:D:\VisualTIMEChile\Result\VTimeStep1\generalque\mantgeneralque\Vtime\Scripts\tMenu.js#%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<%	
End If
%>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<%=mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl"))%>




<%

With Response
	.Write(mobjValues.StyleSheet())
	
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
		.Write(mobjMenu.MakeMenu("MGE001", "MGE001_k.aspx", 1, ""))
		mobjMenu = Nothing
	End If
End With
%>
<SCRIPT>
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="MGE001_K" ACTION="valMantGeneralQue.aspx?mode=1">
<%
Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMGE001()
Else
	Call insPreMGE001Upd()
End If

mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>




