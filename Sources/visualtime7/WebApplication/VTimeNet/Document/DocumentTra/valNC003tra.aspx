<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">
Dim clngAcceptdataFinish As String
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.23
    
Dim mobjNetFrameWork As   eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Se define la contante para el manejo de errores en caso de advertencias 
Dim mstrCommand As String
Dim mstrErrors As Object
Dim mstrKey As String
Dim lclsGeneral As Object

'- Variable auxiliar para pase de valores del encabezado al folder
Dim mstrQueryString As String
Dim mstrCodispl As Object
Dim mstrMessage As Object
    Dim mobjValues As eFunctions.Values
    Dim mobjNC003 As eClaim.Document_Pay
    


'% insValNC003: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Private Function insValNC003() As Object

	'--------------------------------------------------------------------------------------------
	Dim lblnVal As Object
	
	If CDbl(Request.QueryString.Item("nzone")) = 1 Then
		
		lblnVal = True
		
	Else
		
		lblnVal = mobjNC003.insValNC003_K(mobjValues.StringToType(Request.Form.Item("cbAction"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.Form.Item("tcnClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbOrdServ"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbDocument"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("HddNstatus"), eFunctions.Values.eTypeData.etdLong))
		
	End If
	
	insValNC003 = lblnVal
	
End Function

'% insPostNC003: Se realizan las actualizaciones a las tablas
'--------------------------------------------------------------------------------------------
Private Function insPostNC003() As Boolean
	
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	
	If CDbl(Request.QueryString.Item("nzone")) = 1 Then
		lblnPost = True
	Else
		Session("sKey_NC003") = mstrKey
		lblnPost = mobjNC003.insPostNC003_K(mstrKey, mobjValues.StringToType(Request.Form.Item("HddTypesupport"), eFunctions.Values.eTypeData.etdLong), Request.Form.Item("HddSclient"), mobjValues.StringToType(Request.Form.Item("HddProvider"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.Form.Item("cbDocument"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbAction"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.Form.Item("tcnClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbOrdServ"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong)) ', 	End If
		
		insPostNC003 = lblnPost
		
	End If
End Function

</script>
<%Response.Expires = -1
'UPGRADE_NOTE: The 'eNetFrameWork.Layout' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
     mobjNetFrameWork = New  eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("valNC003tra")
'UPGRADE_NOTE: The 'eFunctions.Values' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
     mobjValues = New eFunctions.Values
'UPGRADE_NOTE: The 'eClaim.Document_Pay' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
    mobjNC003 = New eClaim.Document_Pay
    

mstrQueryString = "&sCodispl=NC003&Action=Add&Index=-1&nMainAction=302&sModule=Document&sProject=DocumentTra&nHeight=130&skey=" & Request.QueryString.Item("skey")

'	If Request.QueryString("sKey") = vbnullstring Then
'		Set lclsGeneral = Server.CreateObject("eGeneral.GeneralFunction")
'		mstrKey = lclsGeneral.getsKey(Session("nUsercode"))
'		Set lclsGeneral = Nothing
'		mstrQueryString = Request.QueryString & "&sKey=" & mstrKey
'
'	Else
'		mstrKey = Request.QueryString("sKey")
'		mstrQueryString = Request.QueryString
'	End if

mstrKey = Request.QueryString.Item("sKey")
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.31
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "valNC003tra"

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
	mstrErrors = insValNC003
	Session("sErrorTable") = mstrErrors
Else
	Session("sErrorTable") = vbNullString
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""PolicyTraError"",660,330);")
		.Write(mobjValues.StatusControl(False, Request.QueryString.Item("nZone"), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
		'.Write "<NOTSCRIPT></SCRIPT>"
	End With
Else
	If insPostNC003 Then
		If Request.QueryString.Item("WindowType") <> "PopUp" Then
			If Request.QueryString.Item("nAction") = clngAcceptdataFinish Then
				Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
			Else
				If Request.QueryString.Item("nZone") = "1" Then
					If Request.Form.Item("sCodisplReload") = vbNullString Then
						Response.Write("<SCRIPT>;top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrQueryString & """;</SCRIPT>")
					Else
						Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
					End If
				Else
					Response.Write("<SCRIPT>;self.history.go(-1);top.fraHeader.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
				End If
			End If
		Else
			Response.Write("<SCRIPT>top.opener.document.location.href='NC003_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=-1" & mstrQueryString & "'</SCRIPT>")
		End If
	End If
End If

'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object mobjNC003 may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNC003 = Nothing
%>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.31
Call mobjNetFrameWork.FinishPage("valNC003tra")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





