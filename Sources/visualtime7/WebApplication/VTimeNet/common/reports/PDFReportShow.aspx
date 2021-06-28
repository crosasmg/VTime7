<%@ Page Language="VB" explicit="true"  EnableViewState="false"%>
<script language="VB" runat="Server">

    Private Sub OpenAndShowFile(ByVal sPDFName As String, ByVal sFullPath As String)
	
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("Pragma", "no-cache")
        Response.AddHeader("Expires", "Mon, 1 Jan 2000 05:00:00 GMT")
        Response.AddHeader("Last-Modified", Now & " GMT")
        Response.ContentType = "application/pdf"
        Response.AddHeader("Content-Disposition", "inline; filename=" & sPDFName) 
        Response.WriteFile(sFullPath)
	
        Response.End()
    End Sub
    
</script>
<%
    Dim sFullP = Server.UrlDecode(Request.QueryString("sPDFFullPath"))
    OpenAndShowFile(Request.QueryString("sPDFname"), sFullP)
 %>



