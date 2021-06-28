<%@ Page Language="VB" explicit="true" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eJobs" %>
<script language="VB" runat="Server">

Dim mclsUser_jobs As Object
Dim mobjValues As eFunctions.Values
Dim mstrErrors As String
Dim mstrQueryString As String
Dim mstrCommand As String


'% insValidateInformation: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Function insValidateInformation() As String
	'--------------------------------------------------------------------------------------------
	
	Select Case Request.QueryString.Item("sCodispl")
		'+MA5000: Tareas
		Case "MA5000"
			insValidateInformation = ""
			
		Case "MA6000"
			insValidateInformation = ""
			
		Case Else
			insValidateInformation = "insValidateInformation: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
End Function

'% insPostInformation: Se realizan las actualizaciones a las tablas
'--------------------------------------------------------------------------------------------
Function insPostInformation() As Boolean
	'--------------------------------------------------------------------------------------------
	insPostInformation = False
	
	Select Case Request.QueryString.Item("sCodispl")
		'+MA5000: Tareas
		Case "MA5000"
			With Request
				mclsUser_jobs = New eJobs.User_jobs
				insPostInformation = mclsUser_jobs.InsPostMA5000(.QueryString("Action"), mobjValues.StringToType(.Form.Item("tcnJob"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdNext_date"), eFunctions.Values.eTypeData.etdDate), .Form.Item("tctWhat"))
			End With
			
		Case "MA6000"
			With Request
				mclsUser_jobs = New eJobs.Win_chklist
				insPostInformation = mclsUser_jobs.InsPostMA6000Upd(.QueryString("Action"), .QueryString("valCodispl"), mobjValues.StringToType(.QueryString.Item("nModules"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("hddsComments"), .Form.Item("cbeObject_type"), .Form.Item("tctObject_name"), mobjValues.StringToType(.Form.Item("hddnId"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctPath"), mobjValues.StringToType(.Form.Item("tcnSequence"), eFunctions.Values.eTypeData.etdDouble))
				
				mstrQueryString = "&valCodispl=" & .QueryString.Item("valCodispl") & "&nModules=" & .QueryString.Item("nModules")
			End With
			
	End Select
	
End Function

'% insFinish: Se activa cuando la acción es finalizar
'-----------------------------------------------------------------------------------------------------------------------
Function insFinish() As Object
	'-----------------------------------------------------------------------------------------------------------------------
	Response.Write("<SCRIPT>insReloadTop(true, false);</" & "Script>")
End Function

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values


mstrCommand = "sModule=Maintenance&sProject=MantProduct&sCodisplReload=" & Request.QueryString.Item("sCodispl")
%>
<SCRIPT>
//%CancelErrors: Va a la ventana anterior si se produce un error.
//---------------------------------------------------------------------------------------------------
function CancelErrors(){
//---------------------------------------------------------------------------------------------------
    self.history.go(-1)
}
//%NewLocation: Se posiciona en la página seleccionada. 
//------------------------------------------------------------------------------------------
function NewLocation(Source,Codisp){
//------------------------------------------------------------------------------------------
    var lstrLocation = "";
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp
    Source.location = lstrLocation
}
</SCRIPT>
<HTML>
<HEAD>
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("GE002"))
End With
%>
    <META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">    
    <%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

    <%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Includes/General.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/General.aspx" -->

        
    <%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
</HEAD>
<BODY>
<FORM id=Form1 name=Form1>
<%
'+ Si no se han validado los campos de la página

If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insValidateInformation
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""MantProductError"",660,330);")
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
		If insPostInformation Then
			If Request.QueryString.Item("WindowType") <> "PopUp" Then
				If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
					Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
				Else
					If Request.QueryString.Item("nZone") = "1" Then
						If Request.Form.Item("sCodisplReload") = vbNullString Then
							Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
						Else
							Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
						End If
					Else
						Response.Write("<SCRIPT>;self.history.go(-1);top.fraHeader.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
					End If
				End If
				
				'+ Se recarga la página que invocó la PopUp
			Else
				Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & "_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & "' </SCRIPT>")
			End If
		End If
	Else
		If Session("bQuery") = True Then
			Response.Write("<SCRIPT>insReloadTop(true, false);</SCRIPT>")
		Else
			insFinish()
		End If
	End If
End If
'UPGRADE_NOTE: Object mclsUser_jobs may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mclsUser_jobs = Nothing
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
%>
        </FORM>
    </BODY>
</HTML>
</BODY>
</HTML>




