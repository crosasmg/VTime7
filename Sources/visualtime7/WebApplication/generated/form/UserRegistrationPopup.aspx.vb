#Region "using"

Imports System.Data
Imports System.Web.Services
Imports InMotionGIT.Common.Proxy

#End Region

Partial Public Class UserRegistrationPopup
    Inherits GIT.Core.PageBase

#Region "Web Methods"

    <WebMethod()>
    Public Shared Function IntermediaryExist(code As System.String) As Object
        Dim response As Object = New With {.Result = False}
        Dim count As Integer

        If Not Information.IsNumeric(code) Then
            code = "0"
        End If

        With New DataManagerFactory(" SELECT " +
                                            " 	COUNT (NINTERMED) " +
                                            " FROM " +
                                            "	INTERMEDIA " +
                                            " WHERE " +
                                            " 	NINTERMED = @:NINTERMED",
                                            "INTERMEDIA", "BackOfficeConnectionString")


            .AddParameter("NINTERMED", DbType.Decimal, 9, False, code)
            count = .QueryExecuteScalarToInteger()
            If count <> 0 Then
                response.Result = True
            End If
        End With
        Return response
    End Function

    <WebMethod()>
    Public Shared Function ClientExist(email As System.String, sclient As String) As Object
        Dim response As Object = New With {.Result = False}
        Dim count As Integer

        With New DataManagerFactory(" SELECT " +
                                    " 	COUNT (CLIENT.SCLIENT) " +
                                    " FROM " +
                                    " 	CLIENT " +
                                    " JOIN ADDRESS ON ADDRESS.NRECOWNER = 2 " +
                                    " AND ADDRESS.SCLIENT = CLIENT.SCLIENT " +
                                    " AND ADDRESS.SE_MAIL = @:MAIL " +
                                    " WHERE " +
                                    " 	CLIENT.SCLIENT = @:SCLIENT ",
                                            "INTERMEDIA", "BackOfficeConnectionString")
            .AddParameter("MAIL", DbType.StringFixedLength, 24, False, email)
            .AddParameter("SCLIENT", DbType.StringFixedLength, 14, False, sclient)
            count = .QueryExecuteScalarToInteger()
            If count <> 0 Then
                response.Result = True
            End If
        End With
        Return response
    End Function


    <WebMethod()>
    Public Shared Function Exist35598edad2a14d1cbb03ddae3b84de0f(USERNAME As System.String) As Object
        Dim response As Object = New With {.Result = False}

        response.Result = (New InMotionGIT.FrontOffice.Proxy.UserService.UsersClient).Exist(userName:=USERNAME)

        Return response
    End Function

    <WebMethod()>
    Public Shared Function ExistEmail0347c7f44c9049379f116072478a1ca2(EMAIL As System.String) As Object
        Dim response As Object = New With {.Result = False}

        response.Result = (New InMotionGIT.FrontOffice.Proxy.UserService.UsersClient).ExistEmail(email:=EMAIL)

        Return response
    End Function

#End Region

End Class