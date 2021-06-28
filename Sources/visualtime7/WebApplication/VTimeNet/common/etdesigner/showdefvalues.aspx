<%@ Page Language="VB" explicit="true" %>
<%@ Import namespace="eJobs" %>
<script language="VB" runat="Server">


'% InsGenerateChklist: Genera archivo plano
'--------------------------------------------------------------------------------------------
Sub InsGenerateChklist()
	'--------------------------------------------------------------------------------------------
	Dim lclsWin_chklist As eJobs.Win_chklist
	
	lclsWin_chklist = New eJobs.Win_chklist
	If lclsWin_chklist.InsConstructFile(Request.QueryString.Item("valCodispl"), Request.QueryString.Item("valCodispl") & Session("nUsercode") & ".txt", Request.QueryString.Item("sModules")) Then
		Response.Write("alert('Proceso finalizado');")
	Else
		Response.Write("alert('Error generando el archivo');")
	End If
	'UPGRADE_NOTE: Object lclsWin_chklist may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsWin_chklist = Nothing
End Sub

</script>
<%Response.Expires = 0
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'VTimeNet/Includes/Includes/General.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/General.aspx" -->

</HEAD>
<BODY>
    <FORM NAME="ShowDefValues">
    </FORM>
</BODY>
<BODY>
</BODY>
</HTML>
<%
Response.Write("<SCRIPT>")
Select Case Request.QueryString.Item("Field")
	Case "CHK_LIST"
		Call InsGenerateChklist()
End Select
Response.Write("window.close();self.document.location.href='../../Common/blank.htm';")
Response.Write("</SCRIPT>")
%>




