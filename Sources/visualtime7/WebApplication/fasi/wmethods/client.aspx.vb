Imports System.Globalization
Imports System.Web.Services
Imports InMotionGIT.Common.Helpers

Partial Class fasihtml5_wmethods_client
    Inherits System.Web.UI.Page

    <WebMethod(EnableSession:=True)>
    Public Shared Function Search(filter As String, pageLength As Integer, currentPage As Integer) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
        Dim result As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True}
        Try
            InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("*")
            result = InMotionGIT.BackOffice.Support.Connection.Client.Lookup(filter, pageLength * currentPage + 1, pageLength * (currentPage + 1))
        Catch ex As Exception
            result = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "Client", "Search", String.Empty)
        End Try
        Return result
    End Function

    <WebMethod(EnableSession:=True)>
    Public Shared Function CompleteClientName(clientId As String, withClientId As Boolean) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
        Dim result As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
        Try
            InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("*")
            result = InMotionGIT.BackOffice.Support.Connection.Client.CompleteClientName(clientId, withClientId)
        Catch ex As Exception
            result = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "Client", "CompleteClientName", String.Empty)
        End Try
        Return result
    End Function


    'TODO: Esta función debe ser eliminado en las próximas semanas
    <WebMethod(EnableSession:=True)>
    Public Shared Function LookupList(filter As String) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
        Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
        Try
            InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("*")
            resultData = InMotionGIT.BackOffice.Support.Connection.Client.Lookup(filter, 1, 20)
        Catch ex As Exception
            resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "Client", "LookupList", String.Empty)
        End Try
        Return resultData
    End Function



End Class
