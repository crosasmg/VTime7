<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.23
Dim mobjNetFrameWork As Object
'~End Header Block VisualTimer Utility

'- Se define la contante para el manejo de errores en caso de advertencias 

Dim mstrCodispl As String
Dim mstrCommand As String
Dim mstrErrors As String
Dim mblnReload As Boolean

'- Variable auxiliar para pase de valores del encabezado al folder

Dim mstrQueryString As String
Dim mstrMessage As Object
    Dim mobjNC001 As Object
    Dim mobjValues As New eFunctions.Values


'% insValNC001: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Private Function insValNC001() As String
	
	Dim lclsDoc_Pay As Object
        lclsDoc_Pay = New eClaim.Document_Pay
	  
        insValNC001 = lclsDoc_Pay.insValNC001_K(mobjValues.StringToType(Request.Form.Item("cbeTypesupport"), eFunctions.Values.eTypeData.etdInteger), _
                                         Request.Form.Item("cbeClient_Provider"), _
                                         mobjValues.StringToType(Request.Form.Item("cbeCod_Provider"), eFunctions.Values.eTypeData.etdInteger), _
                                         mobjValues.StringToType(Request.Form.Item("tcnN_Document"), eFunctions.Values.eTypeData.etdDouble), _
                                         mobjValues.StringToType(Request.Form.Item("tcnMount_Document"), eFunctions.Values.eTypeData.etdDouble))
	
	'UPGRADE_NOTE: Object lclsDoc_Pay may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsDoc_Pay = Nothing
	
End Function

'% insPostNC001: Se realizan las actualizaciones a las tablas
'-----------                 'mobjValues.StringToType(Request.Form.Item("cbeCod_Provider"), eFunctions.Values.eTypeData.etdLong), _---------------------------------------------------------------------------------
Private Function insPostNC001() As Boolean
	Dim lblnPost As Boolean
	     mobjNC001 =  new eClaim.Document_Pay
        lblnPost = mobjNC001.insPostNC001_K(mobjValues.StringToType(Request.Form.Item("cbeTypesupport"), eFunctions.Values.eTypeData.etdLong), _
                                            Request.Form.Item("cbeClient_Provider"),  _
                                             mobjValues.StringToType(Request.Form.Item("cbeCod_Provider"), eFunctions.Values.eTypeData.etdLong), _
                                            mobjValues.StringToType(Request.Form.Item("tcnN_Document"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnMount_Document"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdDate_Document"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
	
	insPostNC001 = lblnPost
End Function

</script>
<%Response.Expires = -1
'UPGRADE_NOTE: The 'eNetFrameWork.Layout' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm

  Dim mobjNetFrameWork = New eNetFrameWork.Layout

mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("valNC001tra")
'UPGRADE_NOTE: The 'eFunctions.Values' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm

Dim mobjValues as new eFunctions.Values
'UPGRADE_NOTE: The 'eClaim.Document_Pay' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
 
Dim mobjNC001 as eClaim.Document_Pay

'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.31
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mstrCodispl = Request.QueryString.Item("sCodispl")

mobjValues.sCodisplPage = mstrCodispl

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

//%NewLocation: se recalcula el URL de la página
//------------------------------------------------------------------------------------------
function NewLocation(Source,Codisp){
//------------------------------------------------------------------------------------------
    var lstrLocation = "";
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp
    Source.location = lstrLocation
}
</SCRIPT>
<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT SRC="/VTimeNet/scripts/GenFunctions.js"></SCRIPT>
<%

'+ Si no se han validado los campos de la página



'If Request.QueryString("nAction") <> clngAcceptdataFinish Then
If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insValNC001
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
	mblnReload = False
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
	mblnReload = True
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & mstrQueryString & """, ""DocumentTraError"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
		.Write(mobjValues.StatusControl(False, Request.QueryString.Item("nZone"), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
	
Else
	If insPostNC001 Then
		If Request.QueryString.Item("WindowType") <> "PopUp" Then
			If Request.Form.Item("sCodisplReload") = vbNullString Then
				Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
			Else
				Response.Write("<SCRIPT>window.close();opener.top.document.location.reload();</SCRIPT>")
			End If
		End If
	End If
End If

'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object mobjNC001 may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNC001 = Nothing
%>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.31
Call mobjNetFrameWork.FinishPage("valNC002tra")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





