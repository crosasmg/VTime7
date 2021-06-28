Imports System.IO

Partial Class Underwriting_Controls_Exporter
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim serverPath As String = Server.MapPath(ConfigurationManager.AppSettings("AttachmentsPath").ToString())
        Dim file As String = Request.QueryString("filename")
        Dim fileInfo As New FileInfo(String.Format("{0}\{1}", serverPath, file))
        'Dim fullName As String = serverPath + "\" + file

        Response.ClearContent()
        Response.AddHeader("Content-Disposition", "attachment; filename=" + FileInfo.Name)
        Response.AddHeader("Content-Length", FileInfo.Length.ToString())
        Response.ContentType = "application/octet-stream"
        Response.WriteFile(fileInfo.FullName)
        Response.End()
    End Sub
End Class
