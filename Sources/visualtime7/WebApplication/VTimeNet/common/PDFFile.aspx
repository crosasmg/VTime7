<%@ Page LANGUAGE="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="ADODB" %>
<%@ Import namespace="eSchedule" %>

<script language="VB" runat="Server">
Dim strPath As String
Dim lstrFile As String

    Private Sub DownloadFile(ByRef xfile As String)
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("Pragma", "no-cache")
        Response.AddHeader("Expires", "Mon, 1 Jan 2000 05:00:00 GMT")
        Response.AddHeader("Last-Modified", Now & " GMT")
        Response.ContentType = "application/pdf"
        Response.AddHeader("Content-Disposition", "inline; filename=jjj.pdf") ' & sFileName
        lstrFile = Request.QueryString.Item("file")
        lstrFile = Replace(lstrFile, "/", "\")
        Response.WriteFile(lstrFile) 'oHelper.sExportedFilePath)
        Response.End()
    End Sub

</script>
<%Response.Buffer = True
    strPath = CStr(Request.QueryString.Item("file"))
    Call DownloadFile(strPath)
%>





