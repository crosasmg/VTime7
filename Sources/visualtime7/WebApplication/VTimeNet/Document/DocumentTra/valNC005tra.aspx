<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">
Dim clngAcceptdataFinish As String
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.23
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility


'- Se define la contante para el manejo de errores en caso de advertencias 

Dim mstrCommand As String
Dim mstrErrors As Object
Dim mstrCheque As Byte


'- Variable auxiliar para pase de valores del encabezado al folder

Dim mstrQueryString As String
Dim mstrCodispl As Object
Dim mstrMessage As Object


    Dim mobjValues As eFunctions.Values
    Dim mobjNC005 As eClaim.Document_Pay
 


'% insValNC005: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Private Function insValNC005() As Object
	'--------------------------------------------------------------------------------------------
	Dim lblnVal As Object
	
	If CDbl(Request.Form.Item("optTypProcess")) = 1 Then
		
		lblnVal = mobjNC005.insValNC005_K(Request.QueryString.Item("nzone"), Request.Form.Item("cbeClient_Provider"), Request.Form.Item("tcnMount_Total"), Request.Form.Item("tcnMount_Saldo"))
		
	Else
		
		lblnVal = True
		
	End If
	
	
	insValNC005 = lblnVal
	
End Function

'% insPostNC002: Se realizan las actualizaciones a las tablas
'--------------------------------------------------------------------------------------------
Private Function insPostNC005() As Boolean
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	
	If CDbl(Request.Form.Item("optTypProcess")) = 1 Then
		
		If CDbl(Request.QueryString.Item("nzone")) = 1 Then
			
			Session("sClient") = Request.Form.Item("cbeClient_Provider")
			lblnPost = mobjNC005.insPostNC005_K(Request.Form.Item("cbeClient_Provider"))
		Else
			
			mstrCheque = 0
			
			lblnPost = mobjNC005.insPostNC005(1, 12, "0", Session("sClient"), 1, 82, Session("nUsercode"), 0, 0, 0, Request.Form.Item("sKey"))
			mstrCheque = mobjNC005.ncheque
			
			If lblnPost Then
				insPrintDocuments()
			End If
			
		End If
		
	Else
		
		insPrintDocuments_2()
		
		lblnPost = True
		
	End If
	
	
	insPostNC005 = lblnPost
	
End Function

'% insPrintDocuments: Impresión de los documentos
'-----------------------------------------------------------------------------------------
Private Sub insPrintDocuments()
	'-----------------------------------------------------------------------------------------
	
	Dim mobjDocuments As eReports.Report
	
'UPGRADE_NOTE: The 'eReports.Report' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
        
        mobjDocuments = New eReports.Report()
	With mobjDocuments
		.sCodispl = "NC005"
		.ReportFilename = "NC0051.rpt"
		.setStorProcParam(1, Session("sClient"))
		.setStorProcParam(2, mstrCheque)
		'.setStorProcParam 3, Request.Form("tcnN_Document")
		'.setStorProcParam 4, Request.Form("cbeStatus")
		
		Response.Write((.Command))
	End With
	
	'UPGRADE_NOTE: Object mobjDocuments may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	mobjDocuments = Nothing
End Sub

'% insPrintDocuments_2: Impresión de los documentos
'-----------------------------------------------------------------------------------------
Private Sub insPrintDocuments_2()
	'-----------------------------------------------------------------------------------------
	
        Dim mobjDocuments As New eReports.Report
	With mobjDocuments
		.sCodispl = "NC005"
		.ReportFilename = "NC0052.rpt"
            .setStorProcParam(1, mobjDocuments.setdate(Request.Form.Item("tcddate_process")))
		.setStorProcParam(2, Request.Form.Item("optProcess"))
		.setStorProcParam(3, Session("nUsercode"))
		'Response.Write("<SCRIPT>alert(' " & .ReportFilename & " ');</" & "Script>")
		Response.Write((.Command))
	End With
	
	'UPGRADE_NOTE: Object mobjDocuments may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	mobjDocuments = Nothing
End Sub

</script>
<%Response.Expires = -1
'UPGRADE_NOTE: The 'eNetFrameWork.Layout' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
    
    mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("valNC005tra")
'UPGRADE_NOTE: The 'eFunctions.Values' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
    
    mobjValues = New eFunctions.Values
'UPGRADE_NOTE: The 'eClaim.Document_Pay' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
mobjNC005 = New eClaim.Document_Pay

'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.31
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "valNC005tra"

mstrCommand = "sModule=Document&sProject=DocumentTra&sCodisplReload=" & Request.QueryString.Item("sCodispl")


%>
<HTML>
<HEAD>
	<LINK REL="StyleSheet" TYPE="text/css" HREF="/VTimeNet/Common/Custom.css">
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0">


</HEAD>
<BODY>
<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT SRC="/VTimeNet/scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>


//- Variable para el control de versiones
    document.VssVersion="$$Revision:   1.44  $|$$Date:   Oct 11 2006 13:25:28  $|$$Author:   chvillan  $"

//%CancelErrors: Se ejecuta cuando se cancela la ventana de errores
//------------------------------------------------------------------------------------------
function CancelErrors(){
//------------------------------------------------------------------------------------------
    self.history.go(-1)
}

</SCRIPT>

<%

'+ Si no se han validado los campos de la página

'If Request.QueryString("nAction") <> clngAcceptdataFinish Then
If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insValNC005
	Session("sErrorTable") = mstrErrors
Else
	Session("sErrorTable") = vbNullString
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & mstrQueryString & """, ""PolicyTraError"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
		.Write(mobjValues.StatusControl(False, Request.QueryString.Item("nZone"), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	If insPostNC005 Then
		If Request.QueryString.Item("WindowType") <> "PopUp" Then
			If Request.QueryString.Item("nAction") = clngAcceptdataFinish Then
				If Request.Form.Item("sCodisplReload") = vbNullString Then
					Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
				Else
					Response.Write("<SCRIPT>window.close();opener.top.document.location.reload();</SCRIPT>")
				End If
			Else
				If Request.Form.Item("sCodisplReload") = vbNullString Then
					
					If CDbl(Request.Form.Item("optTypProcess")) = 1 Then
						Response.Write("<SCRIPT>self.history.go(-1);top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & """;</SCRIPT>")
						
					Else
						Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
						
					End If
					
				Else
					Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & """;</SCRIPT>")
				End If
			End If
			
		End If
	Else
		Response.Write("<SCRIPT>alert('La lectura de registros en la base de datos arrojó un error');</SCRIPT>")
		Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
	End If
End If

'Else
'	If Request.Form("sCodisplReload") = vbNullString Then
'       Response.Write "<NOTSCRIPT>top.document.location.reload();</SCRIPT>"
'  Else
'      Response.Write "<NOTSCRIPT>window.close();opener.top.document.location.reload();</SCRIPT>"
'  End If

'End If



'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object mobjNC005 may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNC005 = Nothing
%>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.31
Call mobjNetFrameWork.FinishPage("valNC005tra")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





