<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSchedule" %>
<script language="VB" runat="Server">

'+ Se define la contante para el manejo de errores en caso de advertencias
Dim mstrCommand As String
Dim mstrKey As String
Dim mstrCodisplBatch As Object
Dim mlngGroup As String
Dim mlngBatch As String

Dim mstrQueryString As String
Dim mobjValues As eFunctions.Values

'- Variable para el manejo de los errores de la página, devueltos por insvalSequence
Dim mstrErrors As String


'% insvalBatch: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Function insValBatch() As String
	'--------------------------------------------------------------------------------------------
	Dim lclsBatch_job As eSchedule.Batch_job
	
	Select Case Request.QueryString.Item("sCodispl")
		Case "BTC001"
			If CDbl(Request.QueryString.Item("nZone")) <> 1 And Request.Form.Item("chkActive") = "1" And Request.QueryString.Item("WindowType") = "PopUp" Then
				lclsBatch_job = New eSchedule.Batch_job
				insValBatch = lclsBatch_job.InsValBtc001(Request.Form.Item("tctKey"), CInt(Request.Form.Item("tcnBatch")))
				lclsBatch_job = Nothing
			End If
			
		Case "BTC002"
			insValBatch = ""
			
		Case Else
			insValBatch = "insvalBatch: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
End Function

'% insPostBatch: Se realizan las actualizaciones a las tablas
'--------------------------------------------------------------------------------------------
Function insPostBatch() As Boolean
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	Dim lclsBatch_job As eSchedule.Batch_job
	
	lblnPost = False
	
	Select Case Request.QueryString.Item("sCodispl")
		'+ BTC001: Resultados de procesos batch
		Case "BTC001"
			lblnPost = True
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				mstrQueryString = "&nBatch=" & Request.Form.Item("valBatch") & "&nUser=" & Request.Form.Item("valUsercod") & "&dProcDate=" & Request.Form.Item("tcdProc") & "&nSheet=" & Request.Form.Item("tcnsheet")
			Else
				mstrQueryString = "&nBatch=" & Request.Form.Item("hddBatch") & "&nUser=" & Request.Form.Item("hddUser") & "&dProcDate=" & Request.Form.Item("hddProcDate") & "&nSheet=" & Request.Form.Item("hddnsheet")
				
				If Request.Form.Item("chkActive") = "1" Then
					lclsBatch_job = New eSchedule.Batch_job
					Call lclsBatch_job.Update_status(Request.Form.Item("tctKey"), eSchedule.Batch_job.enmBatchStatus.batchStatusActive, Session("nUsercode"))
					lclsBatch_job = Nothing
				End If
			End If
			
			'+ BTC002: Parametros de procesos batch
		Case "BTC002"
			lblnPost = True
			
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				mstrQueryString = "&nBatch=" & Request.Form.Item("valBatch")
			End If
			
	End Select
	
	insPostBatch = lblnPost
End Function

</script>
<%Response.Expires = 0

mstrCommand = "&sModule=batch&sProject=batchtra&sCodisplReload=" & Request.QueryString.Item("sCodispl")

'mstrCodisplBatch = Request.QueryString("sCodisplBatch")
mstrKey = Request.QueryString.Item("sKey")
mlngBatch = Request.QueryString.Item("nBatch")
mlngGroup = Request.QueryString.Item("nGroup")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 3/4/03 12.00.01
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "valCollectionSeq"

%>
<HTML>
<HEAD>
	<LINK REL="StyleSheet" TYPE="text/css" HREF="/VTimeNet/Common/Custom.css">
<SCRIPT src="/VTimeNet/scripts/GenFunctions.js"> </SCRIPT>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>





	

</HEAD>
    <%=mobjValues.StyleSheet()%>

<%If CDbl(Request.QueryString.Item("nZone")) = 1 Then
	%><BODY><%	
Else
	%><BODY CLASS="Header"><%	
End If
%>

<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 9-09-09 19:30 $|$$Author: Mpalleres $"


//% CancelErrors:
//--------------------------------------------------------------------------------------
function CancelErrors(){self.history.go(-1)}
//--------------------------------------------------------------------------------------

//% NewLocation:
//--------------------------------------------------------------------------------------
function NewLocation(Source,Codisp){
//--------------------------------------------------------------------------------------
    var lstrLocation = "";
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp;
    Source.location = lstrLocation
}
</SCRIPT>
<%
If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insValBatch
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If

If mstrErrors > vbNullString Then
	With Response
		.Write(mstrErrors)
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""BatchtraError"",660,330);")
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	If insPostBatch Then
		If Request.QueryString.Item("WindowType") <> "PopUp" Then
			If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
				If Request.Form.Item("sCodisplReload") = vbNullString Then
					Response.Write("<SCRIPT>insReloadTop(false);</SCRIPT>")
				Else
					Response.Write("<SCRIPT>insReloadTop(true);</SCRIPT>")
				End If
			Else
				If Request.QueryString.Item("nZone") = "1" Then
					If Request.Form.Item("sCodisplReload") = vbNullString Then
						Response.Write("<SCRIPT>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & """;</SCRIPT>")
					Else
						Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & """;</SCRIPT>")
					End If
				Else
					'Response.Write "<NOTSCRIPT>insReloadTop(false);</SCRIPT>"
				End If
			End If
		Else
			'+ Se recarga la página que invocó la PopUp
			Select Case Request.QueryString.Item("sCodispl")
				'					Case "BTC001"
				'						Response.Write "<NOTSCRIPT>top.opener.document.location.href='" & Request.QueryString("sCodispl") & ".aspx?Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=0" & Request.QueryString("ReloadIndex") & "&nMainAction=" & Request.QueryString("nMainAction") & mstrQueryString & "'</SCRIPT>"
				Case Else
					Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & "'</SCRIPT>")
			End Select
		End If
	Else
		Response.Write("<SCRIPT>alert('No se pudo realizar el proceso')</SCRIPT>")
	End If
End If
mobjValues = Nothing
%>
</BODY>
</HTML>




