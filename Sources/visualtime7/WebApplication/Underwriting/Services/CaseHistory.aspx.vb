Imports System.Web.Services
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
Imports InMotionGIT.Seguridad.Proxy

Partial Class UnderwritingAsync_Services_CaseHistory
    Inherits GIT.Core.PageBase
    Private Function GetUserName(userCode As Integer) As String
        Dim lstUser = InMotionGIT.Membership.Providers.Helpers.User.UserLkp()
        Dim userName = (From x In lstUser Where x.Code = userCode Select x.Description).FirstOrDefault()
        If (IsNothing(userName) OrElse userName.IsEmpty()) Then ' It means that the userhttp://localhost:47115/Underwriting/Controls/Partials/_notes.aspx.vb is located in Security
            Return InMotionGIT.General.Proxy.Security.UserName(userCode)
        Else
            Return userName
        End If
    End Function

	<WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
	Public Shared Function GetCaseHistory(caseId As Integer) As List(Of InMotionGIT.Underwriting.Contracts.CaseHistory)
		Dim listado As New List(Of InMotionGIT.Underwriting.Contracts.CaseHistory)
		Dim userName As String
		If (isUnderwriter()) Then
			Dim managerClient As New InMotionGIT.FrontOffice.Proxy.FrontOfficeManager.ManagerClient
			Try
				Dim listadoUsuario As List(Of InMotionGIT.Common.DataType.LookUpValue) = managerClient.UserLkp()
                For Each historiaCaso As InMotionGIT.Underwriting.Contracts.CaseHistory In InMotionGIT.Underwriting.Proxy.Helpers.CaseHistory.SelectAll(caseId, Convert.ToInt32(HttpContext.Current.Session("LanguageID")), TokenHelper.GetValidToken)
                    If (Not historiaCaso.IsNew Or (historiaCaso.IsNew And historiaCaso.CreatorUserCode = Convert.ToInt32(HttpContext.Current.Session("UserId")))) Then
                        If Not (String.IsNullOrEmpty(historiaCaso.Underwriter)) Then
                            If historiaCaso.Underwriter.IndexOf("-") < 0 Then
                                userName = (From x In listadoUsuario Where x.Code = historiaCaso.Underwriter Select x.Description).FirstOrDefault()
                                If (IsNothing(userName) OrElse userName.IsEmpty()) Then ' It means that the userhttp://localhost:47115/Underwriting/Controls/Partials/_notes.aspx.vb is located in Security
                                    userName = InMotionGIT.General.Proxy.Security.UserName(historiaCaso.Underwriter)
                                End If
                                historiaCaso.Underwriter += String.Format(" - {0}", userName)
                            End If
                        End If
                        listado.Add(historiaCaso)
                    End If
                Next
            Catch ex As Exception
				ResponseHelper.ErrorToClient(ex, HttpContext.Current)
			End Try
		End If
		Return listado
	End Function

	'<WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
	'   Public Shared Sub AddCaseHistory(newCaseHistory As InMotionGIT.Underwriting.Contracts.CaseHistory)
	'       If (isUnderwriter()) Then
	'           Try
	'               If Not IsNothing(newCaseHistory) Then InMotionGIT.Underwriting.Proxy.Helpers.CaseHistory.InsertOnCache(newCaseHistory)
	'           Catch ex As Exception
	'               ResponseHelper.ErrorToClient(ex, HttpContext.Current)
	'           End Try
	'       End If
	'   End Sub

	<WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetHeaderValues() As List(Of String)
        If (isUnderwriter()) Then
            Dim currentCultureInfo = New System.Globalization.CultureInfo(HttpContext.Current.Session("App_CultureInfoCode").ToString())
            System.Threading.Thread.CurrentThread.CurrentCulture = currentCultureInfo
            System.Threading.Thread.CurrentThread.CurrentUICulture = currentCultureInfo

            Return New List(Of String) From {
                Resources.History.CaseHistoryId,
                Resources.History.CreationDate,
                Resources.History.EntryTypeEnumText,
                Resources.History.EntryTypeByLanguage,
                Resources.History.RequirementTypeEnumText,
                Resources.History.RequirementTypeByLanguage,
                Resources.History.StageTypeByLanguage,
                Resources.History.StatusTypeByLanguage,
                Resources.History.ManualOrAutomaticEnumText,
                Resources.History.ManualOrAutomaticByLanguage,
                Resources.History.AlarmTypeEnumText,
                Resources.History.AlarmTypeByLanguage,
                Resources.History.Underwriter,
                Resources.History.Remarks
            }
        End If
    End Function

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
