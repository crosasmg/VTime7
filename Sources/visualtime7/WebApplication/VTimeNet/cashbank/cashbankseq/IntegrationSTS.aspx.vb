Partial Class VTimeNet_cashbank_cashbankseq_IntegrationSTS
    Inherits System.Web.UI.Page

    <System.Web.Services.WebMethod( _
    Description:="This method calls the Web service sts.")> _
    Public Shared Function invokeWebServiceSTS(ByVal name As String) As String 
        Return "Nombre: " & name 
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As _ 
                            System.EventArgs) Handles Me.Load
        Dim lstrResult As String
        Dim nRequestNum As Integer = Request.QueryString("nRequestNum")
        lstrResult = invokeWebServiceSTS("WebServiceSTS")
    End Sub

End Class

