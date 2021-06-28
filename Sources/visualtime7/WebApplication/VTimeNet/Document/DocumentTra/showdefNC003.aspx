<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">
' Response.CacheControl = "private"
    Dim mobjValues As New eFunctions.Values
Dim mblnRefresh As Boolean

'-Codigo de la transaccion
Dim lintTransaction As String

'% InsCalServ: Se buscan el nombre del proveedor de la orden de servicio ingresada
'--------------------------------------------------------------------------------------------
Private Sub InsCalServ()
	'--------------------------------------------------------------------------------------------
        Dim lclsServ As New eClaim.Document_Pay

	With lclsServ
		If .FindProvNC003(Request.QueryString.Item("nServ_order")) Then
			Response.Write("top.fraFolder.document.forms[0].cbProvider.value = '" & lclsServ.sCliename & "';")
			Response.Write("top.fraFolder.document.forms[0].cbDocument.Parameters.Param1.sValue = '" & lclsServ.sClient & "';")
			Response.Write("top.fraFolder.document.forms[0].HddProvider.value = '" & lclsServ.nProvider & "';")
			Response.Write("top.fraFolder.document.forms[0].HddSclient.value = '" & lclsServ.sClient & "';")
		Else
			Response.Write("top.fraFolder.document.forms[0].cbProvider.value = '';")
			Response.Write("top.fraFolder.document.forms[0].cbDocument.value = '';")
			Response.Write("top.fraFolder.document.forms[0].HddProvider.value = '';")
			Response.Write("top.fraFolder.document.forms[0].HddSclient.value = '';")
			Response.Write("top.fraFolder.document.forms[0].cbOrdServ.value = '';")
			Response.Write(" top.fraFolder.document.getElementById('cbOrdServDesc').innerHTML= '';")
			
		End If
	End With
	
	'UPGRADE_NOTE: Object lclsServ may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsServ = Nothing
End Sub

'% InsCalDocument: Se buscan los datos asociados al documento seleccionado
'--------------------------------------------------------------------------------------------
Private Sub InsCalDocument()
	'--------------------------------------------------------------------------------------------
	  Dim lclsServ As New eClaim.Document_Pay

	With lclsServ
		If .Find(0, Request.QueryString.Item("sClient"), Request.QueryString.Item("nDocument"), 0) Then
			Response.Write("top.fraFolder.document.forms[0].HddTypesupport.value = '" & lclsServ.nTypesupport & "';")
			Response.Write("top.fraFolder.document.forms[0].HddProvider.value = '" & lclsServ.nProvider & "';")
			Response.Write("top.fraFolder.document.forms[0].HddSclient.value = '" & lclsServ.sClient & "';")
			Response.Write("top.fraFolder.document.forms[0].HddNstatus.value = '" & lclsServ.nStatus & "';")
			
		Else
			Response.Write("top.fraFolder.document.forms[0].HddTypesupport.value = '';")
			Response.Write("top.fraFolder.document.forms[0].HddProvider.value = '';")
			Response.Write("top.fraFolder.document.forms[0].HddSclient.value = '';")
			Response.Write("top.fraFolder.document.forms[0].HddNstatus.value = '';")
			Response.Write("top.fraFolder.document.forms[0].cbDocument.value = '';")
			Response.Write(" top.fraFolder.document.getElementById('cbDocumentDesc').innerHTML= '';")
			
			
		End If
	End With
	
	'UPGRADE_NOTE: Object lclsServ may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsServ = Nothing
End Sub

</script>
<%Response.Expires = -1
mblnRefresh = False


lintTransaction = Request.QueryString.Item("nTransaction")

%>
<HTML>
<HEAD>
    <%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


	<%=mobjValues.StyleSheet()%>
<SCRIPT>

//+ Variable para el control de versiones 
    document.VssVersion="$$Revision:   1.24  $|$$Date:   Sep 11 2006 09:42:32  $|$$Author:   chvillan  $"

</SCRIPT>
</HEAD>
<BODY>
<FORM NAME="ShowValues">
</FORM>
</BODY>
</HTML>
<%
Response.Write("<SCRIPT>")

Select Case Request.QueryString.Item("Field")
	Case "nServ_Order_rep"
		Call InsCalServ()
	Case "nDocument_rep"
		Call InsCalDocument()
End Select

'Response.Write "setPointer('');"
Response.Write(mobjValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
%>




