<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">
'-Variable para el manejo de funciones generales
Dim mobjValues As eFunctions.Values
Dim mstrCheque As Object
Dim mobjDoc As eClaim.Document_Pay


'% InsUpd_Moveacc: Actualiza registros en tabla de movimientos ctas.ctes (move_acc)
'--------------------------------------------------------------------------------------------
Sub InsUpd_Moveacc()
	'--------------------------------------------------------------------------------------------
	Dim nId_aux As String
	Dim TypDoc As String
	Dim upd As String
	
	nId_aux = Request.QueryString.Item("nId")
	TypDoc = Request.QueryString.Item("nTs")
	upd = Request.QueryString.Item("Sw")
	
	Call mobjDoc.insPostNC005(2, 12, "0", Session("sClient"), 1,  ,  , nId_aux, TypDoc, upd, "0")
	
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
    
    mobjValues = New eFunctions.Values
'UPGRADE_NOTE: The 'eClaim.Document_Pay' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
    mobjDoc = New eClaim.Document_Pay


Response.Write("<SCRIPT>")

Select Case Request.QueryString.Item("Field")
	Case "Move"
		Call InsUpd_Moveacc()
End Select

Response.Write(mobjValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")

'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object mobjDoc may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjDoc = Nothing%>





