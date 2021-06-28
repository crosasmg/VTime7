Imports System.Web.Services
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization

Partial Class UnderwritingAsync_Services_Attachment
    Inherits GIT.Core.PageBase

    '<WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    'Public Shared Function GetAttachments() As List(Of InMotionGIT.Underwriting.Contracts.Attachment)
    '    Dim listado As New List(Of InMotionGIT.Underwriting.Contracts.Attachment)
    '    If (isUnderwriter()) Then
    '        Try
    '            listado = InMotionGIT.Underwriting.Proxy.Helpers.Attachment.SelectAll()
    '        Catch ex As Exception
    '            ResponseHelper.ErrorToClient(ex, HttpContext.Current)
    '        End Try
    '    End If
    '    Return listado
    'End Function

    '<WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    'Public Shared Function RemoveAttachment(attachmentToRemove As InMotionGIT.Underwriting.Contracts.Attachment) As List(Of InMotionGIT.Underwriting.Contracts.Attachment)
    '    If (isUnderwriter()) Then
    '        Try
    '            If Not IsNothing(attachmentToRemove) Then InMotionGIT.Underwriting.Proxy.Helpers.Attachment.DeleteOnCache(attachmentToRemove)
    '        Catch ex As Exception
    '            ResponseHelper.ErrorToClient(ex, HttpContext.Current)
    '        End Try
    '    End If
    '    Return GetAttachments()
    'End Function

    '<WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    'Public Shared Function AddAttachment(newAttachment As InMotionGIT.Underwriting.Contracts.Attachment) As List(Of InMotionGIT.Underwriting.Contracts.Attachment)
    '    If (isUnderwriter()) Then
    '        Try
    '            If Not IsNothing(newAttachment) Then InMotionGIT.Underwriting.Proxy.Helpers.Attachment.InsertOnCache(newAttachment)
    '        Catch ex As Exception
    '            ResponseHelper.ErrorToClient(ex, HttpContext.Current)
    '        End Try
    '    End If
    '    Return GetAttachments()
    'End Function

    '<WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    'Public Shared Function EditRoleInCase(roleInCaseToEdit As InMotionGIT.Underwriting.Contracts.Attachment) As List(Of InMotionGIT.Underwriting.Contracts.Attachment)
    '    If (isUnderwriter()) Then
    '        Try
    '            If Not IsNothing(roleInCaseToEdit) Then InMotionGIT.Underwriting.Proxy.Helpers.Attachment.UpdateOnCache(roleInCaseToEdit)
    '        Catch ex As Exception
    '            ResponseHelper.ErrorToClient(ex, HttpContext.Current)
    '        End Try
    '    End If
    '    Return GetAttachments()
    'End Function

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
