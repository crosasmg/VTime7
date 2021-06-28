Imports System.Data
Imports System.Web.Services
Imports InMotionGIT.Common.Proxy

Partial Class fasihtml5_wmethods_widgets
    Inherits System.Web.UI.Page

    <WebMethod()>
    Public Shared Function NavigationDirectory(category As Integer) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
        Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0, .Data = New List(Of Object)}
        Dim selectDataTableItem As DataTable
        Dim language As Integer = 2

        Try
            'InMotionGIT.FASI.Support.Authentication.AuthorizationProcess()

            With New DataManagerFactory("SELECT NDDESC.TITLE, NDDESC.DESCRIPTION, URLPATH, DOCUMENTTYPE" &
                                        "  FROM NAVIGATIONDIRECTORY ND" &
                                        "  LEFT JOIN NAVIGATIONDIRECTORYDESC NDDESC ON NDDESC.ID=ND.ID AND NDDESC.LANGUAGEID=@:LANGUAGEID" &
                                        " WHERE CATEGORYCODE=@:CATEGORYCODE AND STATUS=1 ORDER BY NDDESC.TITLE", "NAVIGATIONDIRECTORY", "Linked.FrontOffice")
                .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, language)
                .AddParameter("CATEGORYCODE", DbType.Decimal, 5, False, category)
                selectDataTableItem = .QueryExecuteToTable(True)
            End With
            With selectDataTableItem
                If Not IsNothing(.Rows) AndAlso .Rows.Count > 0 Then
                    For Each itemData As DataRow In .Rows
                        resultData.Data.Add(New With {.Title = itemData.StringValue("TITLE"),
                                                      .Description = itemData.StringValue("DESCRIPTION"),
                                                      .Path = itemData.StringValue("URLPATH")})
                    Next
                    With resultData
                        .Count = resultData.Data.Count
                    End With
                End If
            End With

        Catch ex As Exception
            resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "NAVIGATIONDIRECTORY", "NavigationDirectory", String.Empty)
        End Try

        Return resultData
    End Function

#Region "Operaciones temporales para la inicialización"

    <WebMethod(EnableSession:=True)>
    Public Shared Function Init_rol_1() As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
        Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0, .Data = New List(Of Object)}
        Try
            InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("*")
            InMotionGIT.FrontOffice.Support.Initialization.InitializeFrontOfficeRoles()
        Catch ex As Exception
            resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "fasihtml5_wmethods_widgets", "Initialization_rol_1", String.Empty)
        End Try

        Return resultData
    End Function

    <WebMethod(EnableSession:=True)>
    Public Shared Function Init_rol_2(roleMode As Integer, roleList As String) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
        Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0, .Data = New List(Of Object)}
        Dim rolList As New List(Of String)
        Try
            InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("*")
            If roleMode = 1 Then
                rolList = roleList.Split(";").ToList
            End If
            InMotionGIT.FrontOffice.Support.Initialization.InitializeBackOfficeRoles(rolList)
        Catch ex As Exception
            resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "fasihtml5_wmethods_widgets", "Initialization_rol_2", String.Empty)
        End Try

        Return resultData
    End Function

    <WebMethod(EnableSession:=True)>
    Public Shared Function Init_rol_3() As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
        Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0, .Data = New List(Of Object)}
        Try
            InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("*")
            InMotionGIT.FrontOffice.Support.Initialization.InitializeWidgetsInRolesDefaultConfiguration()
        Catch ex As Exception
            resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "fasihtml5_wmethods_widgets", "Initialization_rol_1", String.Empty)
        End Try

        Return resultData
    End Function

    <WebMethod(EnableSession:=True)>
    Public Shared Function Init_user_1(userMode As Integer, userList As String, sendEMail As Boolean) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
        Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0, .Data = New List(Of Object)}
        Dim uList As New List(Of String)
        Try
            InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("*")
            If userMode = 0 Then
                InMotionGIT.FrontOffice.Support.Initialization.InitializeAllBackOfficeUser("", sendEMail)
            Else
                For Each id As String In userList.Split(";")
                    InMotionGIT.FrontOffice.Support.Initialization.InitializeOneBackOfficeUser("", id, sendEMail)
                Next
            End If

        Catch ex As Exception
            resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "fasihtml5_wmethods_widgets", "Init_user_1", String.Empty)
        End Try

        Return resultData
    End Function

    <WebMethod(EnableSession:=True)>
    Public Shared Function Init_user_2() As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
        Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0, .Data = New List(Of Object)}
        Try
            InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("*")
            ResponseHelper.Initialization()
        Catch ex As Exception
            resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "fasihtml5_wmethods_widgets", "Init_user_2", String.Empty)
        End Try

        Return resultData
    End Function

    <WebMethod(EnableSession:=True)>
    Public Shared Function Init_user_3(userName As String, copyOption As String) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
        Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0, .Data = New List(Of Object)}
        Dim uList As New List(Of String)
        Try
            InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("*")
            InMotionGIT.FrontOffice.Support.Initialization.CopyUserConfiguration(userName, copyOption)
        Catch ex As Exception
            resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "fasihtml5_wmethods_widgets", "Init_user_3", String.Empty)
        End Try

        Return resultData
    End Function

#End Region

End Class
