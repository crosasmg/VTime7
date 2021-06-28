
Partial Class Underwriting_Controls_CSVExporter
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim bytesContent As Byte() = Session("csv")

        HttpContext.Current.Response.ClearContent()
        HttpContext.Current.Response.Buffer = True
        HttpContext.Current.Response.Charset = ""
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=gridexport.csv")
        'Response.AddHeader("Content-Length", binaryFile.Length)
        HttpContext.Current.Response.ContentType = "text/csv"
        HttpContext.Current.Response.BinaryWrite(bytesContent)
        HttpContext.Current.Response.Flush()
        HttpContext.Current.Response.End()
    End Sub

End Class
