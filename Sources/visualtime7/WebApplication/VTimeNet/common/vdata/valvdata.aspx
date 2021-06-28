<%@ Page Language="VB" explicit="true" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClient" %>
<script language="VB" runat="Server">

Dim cSQLS As Object
Dim oField As Object

Dim mstrErrors As String
Dim lobjValues As eFunctions.Values

'+ Se define la contante para el manejo de errores en caso de advertencias
Dim mstrCommand As String


'--------------------------------------------------------------------------------------------
Function Validate() As String
	'--------------------------------------------------------------------------------------------
	Dim lobjErrors As eFunctions.Errors
	Dim Validated As Boolean
	
	lobjErrors = New eFunctions.Errors
	
	Validate = ""
	With cSQLS
		If Request.QueryString.Item("time") = "1" Then
			For	Each oField In .cKeyProperty
				
				Validated = True
				
				If .moPage.mcPageContents(oField.sName).bRequired Then
					If Trim(Request.Form.Item(oField.sName)) = "" Then
						lobjErrors.ErrorMessage(Request.QueryString.Item("sCodispl"), 2001)
						Validated = False
					End If
				End If
				
				If Validated And (.moPage.mcPageContents(oField.sName).nType = 3 Or .moPage.mcPageContents(oField.sName).nType = 4 Or .moPage.mcPageContents(oField.sName).nType = 5) Then
					
					If .moPage.mcPageContents(oField.sName).nMinimum <> 0 Then
						If CShort(Request.Form.Item(oField.sName)) < .moPage.mcPageContents(oField.sName).nMinimum Then
							lobjErrors.ErrorMessage(Request.QueryString.Item("sCodispl"), 2002)
							Validated = False
						End If
					End If
					
					If .moPage.mcPageContents(oField.sName).nMaximum <> 0 Then
						If CShort(Request.Form.Item(oField.sName)) > .moPage.mcPageContents(oField.sName).nMaximum Then
							lobjErrors.ErrorMessage(Request.QueryString.Item("sCodispl"), 2003)
							Validated = False
						End If
					End If
					
				End If
				
			Next oField
			
		Else
			
			For	Each oField In .cAuxKeyProperty
			Next oField
			
			For	Each oField In .cNoKeyProperty
			Next oField
			
		End If
		
	End With
	
	Validate = lobjErrors.Confirm()
	
End Function

'--------------------------------------------------------------------------------------------
Function PostAction() As Boolean
	'--------------------------------------------------------------------------------------------
	
	
	If Request.QueryString.Item("time") <> "1" Then
		With cSQLS
			For	Each oField In .cKeyProperty
				oField.vValue = Session(oField.sName)
			Next oField
			
			For	Each oField In .cNoKeyProperty
				oField.vValue = Request.Form.Item(oField.sName)
			Next oField
		End With
		
		Select Case Request.QueryString.Item("nMainAction")
			Case CStr(301)
				cSQLS.AddRecord()
			Case CStr(302)
				If Request.QueryString.Item("WindowType") = "PopUp" Then
					If Request.QueryString.Item("Action") = "Add" Then
						cSQLS.AddRecord()
					ElseIf Request.QueryString.Item("Action") = "Update" Then 
						cSQLS.UpDateRecord()
					End If
				Else
					cSQLS.UpDateRecord()
				End If
			Case CStr(303)
				cSQLS.DeleteRecord()
		End Select
	End If
	PostAction = True
End Function


Sub PostClientAction()
	Dim lobjClient As eClient.ClientWin
	
	lobjClient = New eClient.ClientWin
	
	lobjClient.insUpdClient_win(Session("sClient"), CStr(Request.QueryString.Item("sCodispl")), "2")
	
	'UPGRADE_NOTE: Object lobjClient may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lobjClient = Nothing
End Sub

Sub PostClaimAction()
End Sub

Sub PostPolicyAction()
End Sub

</script>
<%Response.Expires = 0
%>
<HTML>
<HEAD>
	<LINK REL="StyleSheet" TYPE="text/css" HREF="/VTimeNet/Common/Custom.css">
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
	<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'VTimeNet/Includes/General.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>


    <%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'VTimeNet/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>

		
</HEAD>

<%

'UPGRADE_NOTE: The 'eVdata.SQLS' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
cSQLS = server.CreateObject("eVdata.SQLS")

cSQLS.Init(Request.QueryString.Item("sCodispl"))

If Request.QueryString.Item("time") = "1" Then
	With cSQLS
		For	Each oField In .cKeyProperty
			Session(oField.sName) = Request.Form.Item(oField.sName)
		Next oField
	End With
End If

If CDbl(Request.QueryString.Item("nZone")) = 1 Then
	%><BODY><%	
Else
	%><BODY CLASS="Header"><%	
End If
%>
<SCRIPT>
function CancelErrors(){self.history.go(-1)}
function NewLocation(Source,Codisp){
    var lstrLocation = "";
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp;
    Source.location = lstrLocation
}
</SCRIPT>
<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT src="/VTimeNet/scripts/GenFunctions.js"> </SCRIPT>
<%mstrCommand = "&sModule=VData&sProject=Common&sCodisplReload=" & Request.QueryString.Item("sCodispl")

lobjValues = New eFunctions.Values

'+ Si no se han validado los campos de la página
If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = Validate
	Session("sErrorTable") = mstrErrors
Else
	Session("sErrorTable") = vbNullString
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sForm=" & server.URLEncode(Request.Form.ToString) & server.URLEncode(mstrCommand) & "&sQueryString=" & server.URLEncode(Request.Params.Get("Query_String")) & """,""ClaimSeqErrors"",660,330);")
		'			.Write "ShowPopUp(""/VTimeNet/Common/Errors.aspx?sForm=" & Server.URLEncode(mstrErrors) & """,""ClaimSeqErrors"",660,330);"
		.Write("self.history.go(-1)")
		.Write("</SCRIPT>")
	End With
Else
	If PostAction() Then
		If Request.QueryString.Item("WindowType") <> "PopUp" Then
			If cSQLS.moPage.nTypeWork = 1 Then 'eSequence
				If cSQLS.moPage.bClient = -1 Then 'Active
					Call PostClientAction()
					Response.Write("<SCRIPT>top.frames['fraSequence'].document.location='/VTimeNet/Client/ClientSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "';</SCRIPT>")
					
				ElseIf cSQLS.moPage.bClaim = -1 Then  'Active
					Call PostClaimAction()
				ElseIf cSQLS.moPage.bPolicy = -1 Then  'Active
					Call PostPolicyAction()
				End If
				
			Else
				'eMaintenance
				'+ Se mueve automaticamente a la siguiente página
				If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
					Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
				Else
					Response.Write("<SCRIPT>;self.history.go(-1);top.fraFolder.document.location=""" & Replace(UCase(Session("sCodisp")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
				End If
			End If
		Else
			
			'+ Se recarga la página que invocó la PopUp
			
			Response.Write("<SCRIPT>opener.document.location.href='" & Session("sCodisp") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "'</SCRIPT>")
			
		End If
	End If
End If
'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
lobjValues = Nothing

'UPGRADE_NOTE: Object oField may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
oField = Nothing
'UPGRADE_NOTE: Object cSQLS may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
cSQLS = Nothing
%>

</BODY>
</HTML>





