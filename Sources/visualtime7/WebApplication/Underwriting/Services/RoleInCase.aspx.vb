Imports System.Web.Services
Imports System.Web.Script.Services
Imports InMotionGIT.Seguridad.Proxy

Partial Class UnderwritingAsync_Services_RoleInCase
    Inherits GIT.Core.PageBase

#Region "Web Methods"
    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetAllRolesInCase(caseId As Integer) As List(Of InMotionGIT.Underwriting.Contracts.RoleInCase)
        Dim listado As New List(Of InMotionGIT.Underwriting.Contracts.RoleInCase)
        If (isUnderwriter() AndAlso caseId > 0) Then
            Try
                listado = InMotionGIT.Underwriting.Proxy.Helpers.RoleInCase.SelectAll(caseId, Convert.ToInt32(HttpContext.Current.Session("LanguageID")), TokenHelper.GetValidToken)
            Catch ex As Exception
                ResponseHelper.ErrorToClient(ex, HttpContext.Current)
            End Try
        End If
        Return listado
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetAllRolesInCaseByRequirementType(caseId As Integer, requirementType As Integer) As List(Of InMotionGIT.Underwriting.Contracts.RoleInCase)
        Dim listado As New List(Of InMotionGIT.Underwriting.Contracts.RoleInCase)
        If (isUnderwriter() AndAlso caseId > 0) Then
            Try
                If (requirementType > 0) Then
                    listado = InMotionGIT.Underwriting.Proxy.Helpers.RoleInCase.SelectAllByRequirementType(caseId, requirementType, Convert.ToInt32(HttpContext.Current.Session("LanguageID")), TokenHelper.GetValidToken)
                End If
                If IsNothing(listado) OrElse listado.Count = 0 Then
                    listado = InMotionGIT.Underwriting.Proxy.Helpers.RoleInCase.SelectAll(caseId, Convert.ToInt32(HttpContext.Current.Session("LanguageID")), TokenHelper.GetValidToken)
                End If
                If listado.Count > 0 AndAlso listado.Item(0).ClientID <> "" Then
                    listado.Insert(0, New InMotionGIT.Underwriting.Contracts.RoleInCase())
                End If
            Catch ex As Exception
                ResponseHelper.ErrorToClient(ex, HttpContext.Current)
            End Try
        End If
        Return listado
    End Function

    '<WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    '   Public Shared Function GetAllRolesInCaseWithNoSelectedIndex() As List(Of InMotionGIT.Underwriting.Contracts.RoleInCase)
    '       Dim listado As New List(Of InMotionGIT.Underwriting.Contracts.RoleInCase)
    '       If (isUnderwriter()) Then
    '           Try
    '               listado = InMotionGIT.Underwriting.Proxy.Helpers.RoleInCase.SelectAll(Convert.ToInt32(HttpContext.Current.Session("LanguageID")))
    '               If listado.Count > 0 AndAlso listado.Item(0).ClientID <> "" Then
    '                   listado.Insert(0, New InMotionGIT.Underwriting.Contracts.RoleInCase())
    '               End If
    '           Catch ex As Exception
    '               ResponseHelper.ErrorToClient(ex, HttpContext.Current)
    '           End Try
    '       End If
    '       Return listado
    '   End Function

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Sub RemoveRoleInCase(caseId As Integer, role As String, clientId As String)
        If (isUnderwriter()) Then
            Try
                ResponseHelper.VerifyEditMode()
                Dim listado As List(Of InMotionGIT.Underwriting.Contracts.RoleInCase) = InMotionGIT.Underwriting.Proxy.Helpers.RoleInCase.SelectAll(caseId, Convert.ToInt32(HttpContext.Current.Session("LanguageID")), TokenHelper.GetValidToken)
                Dim roleToDelete As InMotionGIT.Underwriting.Contracts.RoleInCase = (From r In listado Where r.Role = role And r.ClientID = clientId Select r).FirstOrDefault()
                If IsRoleInCaseValid(roleToDelete) Then InMotionGIT.Underwriting.Proxy.Helpers.RoleInCase.DeleteOnCache(caseId, roleToDelete, TokenHelper.GetValidToken)
            Catch ex As Exception
                ResponseHelper.ErrorToClient(ex, HttpContext.Current)
            End Try
        End If
    End Sub

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Sub AddRoleInCaseParameters(ActuarialAge As String, ClientID As String, ClientName As String, CompleteAddress As String,
                                            Gender As String, Height As String, PhoneNumber As String, Role As String, RoleEnumText As String,
                                            SmokerIndicator As String, Weight As String, id As String, oper As String, caseId As Integer)
        If (isUnderwriter()) Then
            Try
                ResponseHelper.VerifyEditMode()
                Dim clientManager As New InMotionGIT.Client.Proxy.Manager
                If Not (clientManager.Find(ClientID)) Then
                    Throw New ArgumentException("Código de cliente no existe")
                End If
                Dim newRoleInCase As New InMotionGIT.Underwriting.Contracts.RoleInCase
                newRoleInCase.UnderwritingCaseID = caseId
                newRoleInCase.Role = RoleEnumText
                newRoleInCase.ClientID = ClientID
                newRoleInCase.ClientName = ClientName
                newRoleInCase.CompleteAddress = CompleteAddress
                newRoleInCase.PhoneNumber = PhoneNumber
                newRoleInCase.ActuarialAge = IIf(String.IsNullOrEmpty(ActuarialAge), 0, ActuarialAge)
                newRoleInCase.IsNew = True
                newRoleInCase.Gender = Gender
                newRoleInCase.Height = IIf(String.IsNullOrEmpty(Height), 0, Height)
                newRoleInCase.Weight = IIf(String.IsNullOrEmpty(Weight), 0, Weight)
                If SmokerIndicator = "on" Then
                    newRoleInCase.SmokerIndicator = 1
                Else
                    newRoleInCase.SmokerIndicator = 2
                End If

                AddRoleInCase(newRoleInCase)
            Catch ex As Exception
                ResponseHelper.ErrorToClient(ex, HttpContext.Current) 'TODO esta mandando un Error 404 y se queda pegado, debería de mostrar el tipo de Error
            End Try
        End If
    End Sub

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Sub AddRoleInCase(newRoleInCase As InMotionGIT.Underwriting.Contracts.RoleInCase)
        If (isUnderwriter()) Then
            Try
                ResponseHelper.VerifyEditMode()
                Dim listado As List(Of InMotionGIT.Underwriting.Contracts.RoleInCase) = InMotionGIT.Underwriting.Proxy.Helpers.RoleInCase.SelectAll(newRoleInCase.UnderwritingCaseID, Convert.ToInt32(HttpContext.Current.Session("LanguageID")), TokenHelper.GetValidToken)

                If (listado.Count > 0) Then
                    Dim oldRole As InMotionGIT.Underwriting.Contracts.RoleInCase = Nothing
                    oldRole = (From r In listado Where r.Role = newRoleInCase.Role AndAlso r.ClientID = newRoleInCase.ClientID Select r).FirstOrDefault()
                    If (IsNothing(oldRole)) Then
                        If IsRoleInCaseValid(newRoleInCase) Then InMotionGIT.Underwriting.Proxy.Helpers.RoleInCase.InsertOnCache(newRoleInCase, TokenHelper.GetValidToken)
                    Else
                        If IsRoleInCaseValid(oldRole) Then InMotionGIT.Underwriting.Proxy.Helpers.RoleInCase.UpdateOnCache(oldRole, newRoleInCase, TokenHelper.GetValidToken)
                    End If
                End If

            Catch ex As Exception
                ResponseHelper.ErrorToClient(ex, HttpContext.Current)
            End Try
        End If
    End Sub

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Sub EditRoleInCase(newRoleInCase As InMotionGIT.Underwriting.Contracts.RoleInCase, role As Integer, clientId As Integer)
        If (isUnderwriter()) Then
            Try
                ResponseHelper.VerifyEditMode()
                Dim listado As List(Of InMotionGIT.Underwriting.Contracts.RoleInCase) = InMotionGIT.Underwriting.Proxy.Helpers.RoleInCase.SelectAll(newRoleInCase.UnderwritingCaseID, Convert.ToInt32(HttpContext.Current.Session("LanguageID")), TokenHelper.GetValidToken)
                Dim oldRole As InMotionGIT.Underwriting.Contracts.RoleInCase

                oldRole = (From r In listado Where r.Role = role AndAlso r.ClientID = clientId Select r).FirstOrDefault()
                If IsRoleInCaseValid(oldRole) Then InMotionGIT.Underwriting.Proxy.Helpers.RoleInCase.UpdateOnCache(oldRole, newRoleInCase, TokenHelper.GetValidToken)
            Catch ex As Exception
                ResponseHelper.ErrorToClient(ex, HttpContext.Current)
            End Try
        End If
    End Sub

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Sub ExportToCSV(csv As String)
        If (isUnderwriter()) Then
            Try
                'TODO has to look better
                ' CSV is save so it can be exported by using another page.
                HttpContext.Current.Session("csv") = Encoding.ASCII.GetBytes(csv)
            Catch ex As Exception
                ResponseHelper.ErrorToClient(ex, HttpContext.Current)
            End Try
        End If
    End Sub

    Public Shared Function IsRoleInCaseValid(roleInCase As InMotionGIT.Underwriting.Contracts.RoleInCase) As Boolean
        If Not IsNothing(roleInCase) AndAlso roleInCase.Role.IsNotEmpty AndAlso roleInCase.ClientID.IsNotEmpty Then Return True
        Throw New InvalidOperationException("Rol inválido, por favor complete los campos correctamente.")
    End Function

    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetHeaderValues() As List(Of String)
        If (isUnderwriter()) Then
            Dim currentCultureInfo = New System.Globalization.CultureInfo(HttpContext.Current.Session("App_CultureInfoCode").ToString())
            System.Threading.Thread.CurrentThread.CurrentCulture = currentCultureInfo
            System.Threading.Thread.CurrentThread.CurrentUICulture = currentCultureInfo

            Return New List(Of String) From {
                Resources.RoleInCase.RoleName,
                Resources.RoleInCase.ClientID,
                Resources.RoleInCase.ClientName,
                Resources.RoleInCase.CompleteAddress,
                Resources.RoleInCase.PhoneNumber,
                Resources.RoleInCase.ActuarialAge,
                Resources.RoleInCase.Gender,
                Resources.RoleInCase.Height,
                Resources.RoleInCase.Weight,
                Resources.RoleInCase.SmokerIndicator,
                Resources.RoleInCase.ExclusionDate
            }
        Else
            Return Nothing
        End If
    End Function

#End Region

    ''' <summary>
    ''' Retorna un valor booleano true en caso de que el usuario a validar sea suscriptor, falso en caso de que no lo sea.
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function isUnderwriter() As Boolean
        Dim Response As Boolean = False
        If HttpContext.Current.Session("SessionTimeOut") <> "Yes" Then
            Try
                Dim userRoles As String
                Dim userContext As InMotionGIT.Membership.Providers.FrontOfficeMembershipUser
                userContext = InMotionGIT.Membership.Providers.Helper.RetriveUserContext()
                userRoles = InMotionGIT.Membership.Providers.Helper.RetrivellUserData(userContext.UserName).RoleName.ToLower()
                If (Not IsNothing(ConfigurationManager.AppSettings.Get("NBEnableHTML5")) AndAlso ConfigurationManager.AppSettings.Get("NBEnableHTML5")) Then
                    Response = userRoles.Split(",").Contains("suscriptor")
                Else
                    Response = userRoles.Split(";").Contains("suscriptor")
                End If
            Catch ex As Exception
                Response = False
            End Try
        End If
        Return Response
    End Function
End Class