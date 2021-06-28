<%@ Page LANGUAGE="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="ADODB" %>
<%@ Import namespace="eSchedule" %>

<script language="VB" runat="Server">
Dim strPath As String


    Private Sub DownloadFile(ByRef xfile As String, ByVal sDownloadType As String)
        Dim strAbsFile As String

        If sDownloadType = "cm" Then
            strAbsFile = New eFunctions.Values().insGetSetting("MASSIVELOAD", String.Empty, "PATHS").Trim() & String.Format("\{0}.XLS", xfile)
            
        Else
            strAbsFile = Session("sDirOut") & "\" & xfile  'Server.MapPath(file)
        End If

        'Response.write (strAbsFile )
        'Response.flush
        'Response.end
        'exit sub

        Dim file As System.IO.FileInfo = New System.IO.FileInfo(strAbsFile)
        If file.Exists Then 'set appropriate headers
            Response.Clear()
            Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
            Response.AddHeader("Content-Length", file.Length.ToString())
            Response.ContentType = "application/octet-stream"
            Response.WriteFile(file.FullName)
            Response.End() 'if file does not exist
        Else
            Response.Write("This file does not exist.")
            Response.Write(strAbsFile)
        End If 'nothing in the URL as HTTP GET


    End Sub

</script>
<%Response.Buffer = True


    strPath = CStr(Request.QueryString.Item("file"))

    Call DownloadFile(strPath, Request.QueryString.Item("dt"))
%>





