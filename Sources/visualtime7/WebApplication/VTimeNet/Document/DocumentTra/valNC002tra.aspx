<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">
Dim clngAcceptdataFinish As String
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.23
Dim mobjNetFrameWork As Object
'~End Header Block VisualTimer Utility

'- Se define la contante para el manejo de errores en caso de advertencias 

Dim mstrCommand As String
Dim mstrErrors As Object

'- Variable auxiliar para pase de valores del encabezado al folder

Dim mstrQueryString As String
Dim mstrCodispl As Object
Dim mstrMessage As Object
    Dim mobjValues As New eFunctions.Values
Dim mobjNC002 As Object


'% insValNC002: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
 Function insValNC002() As Object

	'--------------------------------------------------------------------------------------------
	Dim lblnVal As Object
        Dim lclsDoc_Pay As Object
        lclsDoc_Pay = New eClaim.Document_Pay
	
	If CDbl(Request.QueryString.Item("nzone")) = 1 Then
		lblnVal = lclsDoc_Pay.insValNC002_K(mobjValues.StringToType(Request.Form.Item("cbeTypesupport"), eFunctions.Values.eTypeData.etdLong), Request.Form.Item("cbeClient_Provider"), mobjValues.StringToType(Request.Form.Item("tcnN_Document"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.Form.Item("cbeStatus"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToDate(Request.Form.Item("tcdDate_dStatus1")), mobjValues.StringToDate(Request.Form.Item("tcdDate_dStatus2")), mobjValues.StringToType(Request.Form.Item("tcnUsercode"), eFunctions.Values.eTypeData.etdLong))
	Else
		lblnVal = True
	End If
        lclsDoc_Pay = Nothing
        
	insValNC002 = lblnVal
	
End Function

'% insPostNC002: Se realizan las actualizaciones a las tablas
'--------------------------------------------------------------------------------------------
 Function insPostNC002() As Boolean
	Dim lblnPost As Boolean
        Dim lclsDoc_Pay As Object
        lclsDoc_Pay = New eClaim.Document_Pay
	
	If CDbl(Request.QueryString.Item("nzone")) = 1 Then
            lblnPost = lclsDoc_Pay.insPostNC002_K(mobjValues.StringToType(Request.Form.Item("cbeTypesupport"), eFunctions.Values.eTypeData.etdLong), Request.Form.Item("cbeClient_Provider"), mobjValues.StringToType(Request.Form.Item("tcnN_Document"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeStatus"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToDate(Request.Form.Item("tcdDate_dStatus1")), mobjValues.StringToDate(Request.Form.Item("tcdDate_dStatus2")), mobjValues.StringToType(Request.Form.Item("tcnUsercode"), eFunctions.Values.eTypeData.etdLong))
		lblnPost = True
		mstrQueryString = "&nTypesupport=" & Request.Form.Item("cbeTypesupport") & "&sClient=" & Request.Form.Item("cbeClient_Provider") & "&nDocument=" & Request.Form.Item("tcnN_Document") & "&nStatus=" & Request.Form.Item("cbeStatus") & "&dDate_dStatus1=" & Request.Form.Item("tcdDate_dStatus1") & "&dDate_dStatus2=" & Request.Form.Item("tcdDate_dStatus2") & "&nCodeuser=" & Request.Form.Item("tcnUsercode") & "&chkReport=" & Request.Form.Item("chkReport")
	Else
		
		If Request.Form.Item("hddReport") = "1" Then
			insPrintDocuments()
		End If
		
		lblnPost = True
	End If
	 lclsDoc_Pay = Nothing
	insPostNC002 = lblnPost
	
End Function


'% insPrintDocuments: Impresión de los documentos
'-----------------------------------------------------------------------------------------
    Sub insPrintDocuments()
	
  Dim mobjDocuments As New eReports.Report
        With mobjDocuments
            .sCodispl = "NC002"
            .ReportFilename = "NC002.rpt"
            If mobjValues.StringToType(Request.Form.Item("hddnTypesupport"), eFunctions.Values.eTypeData.etdLong) > 0 Then
                .setStorProcParam(1, Request.Form.Item("hddnTypesupport"))
            Else
                .setStorProcParam(1, "")
            End If
            .setStorProcParam(2, Request.Form.Item("hddsClient"))
            .setStorProcParam(3, Request.Form.Item("hddnDocument"))
		
            If mobjValues.StringToType(Request.Form.Item("hddnStatus"), eFunctions.Values.eTypeData.etdLong) > 0 Then
                .setStorProcParam(4, Request.Form.Item("hddnStatus"))
            Else
                .setStorProcParam(4, "")
            End If
            If Not IsNothing(Request.Form.Item("hdddDate_dStatus1")) Then
                .setStorProcParam(5, .setdate(Request.Form.Item("hdddDate_dStatus1")))
            Else
                .setStorProcParam(5, "")
            End If
            If Not IsNothing(Request.Form.Item("hdddDate_dStatus2")) Then
                .setStorProcParam(6, .setdate(Request.Form.Item("hdddDate_dStatus2")))
            Else
                .setStorProcParam(6, "")
            End If
            .setStorProcParam(7, Request.Form.Item("hddnCodeuser"))
		
            Response.Write((.Command))
        End With
	
        'UPGRADE_NOTE: Object mobjDocuments may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        mobjDocuments = Nothing
    End Sub

</script>
<%Response.Expires = -1
'UPGRADE_NOTE: The 'eNetFrameWork.Layout' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
    
    Dim mobjNetFrameWork As New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("valNC002tra")
'UPGRADE_NOTE: The 'eFunctions.Values' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
    Dim mobjValues As New eFunctions.Values
'UPGRADE_NOTE: The 'eClaim.Document_Pay' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
    Dim mobjNC002 As eClaim.Document_Pay
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.31
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "valNC002tra"

mstrCommand = "sModule=Document&sProject=DocumentTra&sCodisplReload=" & Request.QueryString.Item("sCodispl")





%>
<HTML>
<HEAD>
	<LINK REL="StyleSheet" TYPE="text/css" HREF="/VTimeNet/Common/Custom.css">
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0">

</HEAD>
<BODY>
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
<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT SRC="/VTimeNet/scripts/GenFunctions.js"></SCRIPT>
<%

'+ Si no se han validado los campos de la página

'If Request.QueryString("nAction") <> clngAcceptdataFinish Then
If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insValNC002
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
	If insPostNC002 Then
		If Request.QueryString.Item("WindowType") <> "PopUp" Then
			If Request.QueryString.Item("nAction") = clngAcceptdataFinish Then
				If Request.Form.Item("sCodisplReload") = vbNullString Then
					Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
				Else
					Response.Write("<SCRIPT>window.close();opener.top.document.location.reload();</SCRIPT>")
				End If
			Else
				If Request.Form.Item("sCodisplReload") = vbNullString Then
					Response.Write("<SCRIPT>self.history.go(-1);top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & """;</SCRIPT>")
				Else
					Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & """;</SCRIPT>")
				End If
			End If
			
		End If
	Else
		Response.Write("<SCRIPT>alert('No existen documentos para la condición de busqueda');</SCRIPT>")
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

%>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.31
Call mobjNetFrameWork.FinishPage("valNC002tra")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





