<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">

'-Variable para el manejo de funciones generales
Dim mobjValues As Object
Dim mobjDoc As Object


'% InsCod_Provider: Muestra el codigo del proveedor
'--------------------------------------------------------------------------------------------
Sub InsCod_Provider()
	'--------------------------------------------------------------------------------------------
	Dim Cod As String
	Dim sClient_aux As String
	Dim mobjDoc  as new eClaim.Document_Pay
	
	sClient_aux = Request.QueryString.Item("sClient")
	
	'Response.Write "alert(Request.QueryString("sClient"));"
	
	If mobjDoc.Find_Provider(sClient_aux) Then
		Cod = mobjDoc.nProvider
		
		With Response
			.Write("with(top.frames['fraHeader'].document.forms[0]){")
			.Write("cbeCod_Provider.value='" & Cod & "';")
			.Write("}")
		End With
	Else
		With Response
			.Write("with(top.frames['fraHeader'].document.forms[0]){")
			.Write("cbeCod_Provider.value=0;")
			.Write("}")
		End With
	End If
	
	
End Sub

</script>
<%Response.Expires = -1
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">


</HEAD>
<BODY>
    <FORM NAME="ShowDefValues">
    </FORM>
</BODY>
</HTML>
<%'UPGRADE_NOTE: The 'eFunctions.Values' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
Dim mobjValues  as new eFunctions.Values
'UPGRADE_NOTE: The 'eClaim.Document_Pay' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
Dim mobjDoc  as new eClaim.Document_Pay


Response.Write("<SCRIPT>")
Select Case Request.QueryString.Item("Field")
	Case "Provider"
		Call InsCod_Provider()
End Select

Response.Write(mobjValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")

'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object mobjDoc may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjDoc = Nothing

%>





