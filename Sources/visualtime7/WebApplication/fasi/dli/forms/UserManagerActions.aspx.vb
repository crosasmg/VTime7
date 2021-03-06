'---------------------------------------------------------------------------------------------------
' <generated>
'     This code was generated by Form Designer v7.3.45.1 at 2020-05-26 01:46:17 PM model release 49, Form Generator v1.0.37.67
'     
'     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
' </generated>
'---------------------------------------------------------------------------------------------------
      
#Region "using"

Imports System.Data
Imports System.Globalization
Imports System.Net
Imports System.Runtime.Serialization
Imports System.Web.Script.Services
Imports System.Web.Services
Imports InMotionGIT.Common.Helpers
Imports InMotionGIT.Common.Proxy
Imports InMotionGIT.FrontOffice.Support

#End Region

Namespace dli.forms

    Public Class UserManagerActions
        Inherits System.Web.UI.Page

#Region "Actions Methods"

       <WebMethod(EnableSession:=True)>
        Public Shared Function Initialization(id As String, urlid As String, fromid As String) As InMotionGIT.FrontOffice.Support.DataType.ServerActionResult
            
            Dim instance As New EntryData With {.InstanceFormId = id}
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ServerActionResult
            Dim currentAction As String = String.Empty
            
            
            
            Try
                InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("Administrador")
                
                If id.IsEmpty AndAlso urlid.IsEmpty Then
                    instance.InstanceFormId = System.Guid.NewGuid().ToString
                  
                
                Else
                    instance.InstanceFormId = id.IfEmpty(urlid)
                End If             
             
                With resultData
                    .Success = True
                    .Data = New With {.Instance = instance, .LookUps = LoadLookupsList()}
                End With

            Catch ex As Exception
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessServerAction(ex, "UserManager", "Initialization", currentAction)
            End Try
            
            Return resultData
        End Function
  

        Public Shared Function LoadLookupsList() As List(Of InMotionGIT.Common.DataType.LookUpPackage)
            Dim result As New List(Of InMotionGIT.Common.DataType.LookUpPackage)
            
            Dim dataFactory As New DataManagerFactory("PackageExecuteToLookUp", "LOOKUPS", "")
            Dim dataCommand As InMotionGIT.Common.Services.Contracts.DataCommand = Nothing
            
            dataCommand = dataFactory.AddCommand(String.Format(CultureInfo.CurrentCulture, "SELECT  ROLE.ROLEID, ROLE.ROLENAME, ROLE.SECURITYLEVEL FROM ROLE ROLE    WHERE NOT ROLE.ROLENAME IS NULL ORDER BY ROLENAME", ""), New InMotionGIT.Common.DataType.LookUpValue With {.Code = "ROLEID", .Description = "ROLENAME"}, "RolAssiged", "ROLE", "Linked.FrontOffice")

            dataCommand = dataFactory.AddCommand(String.Format(CultureInfo.CurrentCulture, "SELECT  USERGROUPS.GROUPID, USERGROUPS.DESCRIPTION FROM USERGROUPS USERGROUPS    WHERE NOT USERGROUPS.DESCRIPTION IS NULL ORDER BY DESCRIPTION", ""), New InMotionGIT.Common.DataType.LookUpValue With {.Code = "GROUPID", .Description = "DESCRIPTION"}, "GroupAssiged", "USERGROUPS", "Linked.FrontOffice")

            dataCommand = dataFactory.AddCommand(String.Format(CultureInfo.CurrentCulture, "SELECT  USERMEMBER.USERID, USERMEMBER.USERNAME FROM USERMEMBER USERMEMBER  WHERE USERMEMBER.ISANONYMOUS = '{0}'  AND NOT USERMEMBER.USERNAME IS NULL ORDER BY USERNAME", 0), New InMotionGIT.Common.DataType.LookUpValue With {.Code = "USERID", .Description = "USERNAME"}, "Supervisors", "USERMEMBER", "Linked.FrontOffice")
            
            result = dataFactory.PackageExecuteToLookUp()
	
            Return result
        End Function




#End Region

#Region "Lookups Web Methods"


       <WebMethod(EnableSession:=True)>
        Public Shared Function LookUpForRolAssiged() As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim result As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True}
            Dim resultData As DataTable = Nothing
            Dim newLookupList As New List(Of Object)
            
            Try
                InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("Administrador")
                
                With New DataManagerFactory(String.Format(CultureInfo.CurrentCulture,
                                                          "SELECT  ROLE.ROLEID, ROLE.ROLENAME, ROLE.SECURITYLEVEL FROM ROLE ROLE    WHERE NOT ROLE.ROLENAME IS NULL ORDER BY ROLENAME", ""), 
                                            "ROLE", "Linked.FrontOffice")

                    
                    resultData = .QueryExecuteToTable(True)
                End With

                If Not IsNothing(resultData) Then
                    For Each item As DataRow In resultData.Rows
                        newLookupList.Add(New With {.ROLEID = item.NumericValue("ROLEID"),
                                                    .ROLENAME = item.StringValue("ROLENAME")})
                    Next
                End If

                result.Data = newLookupList

            Catch ex As Exception
                result = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "UserManager", "LookUpForRolAssiged", String.Empty)
            End Try
            
            Return result
        End Function

       <WebMethod(EnableSession:=True)>
        Public Shared Function LookUpForGroupAssiged() As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim result As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True}
            Dim resultData As DataTable = Nothing
            Dim newLookupList As New List(Of Object)
            
            Try
                InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("Administrador")
                
                With New DataManagerFactory(String.Format(CultureInfo.CurrentCulture,
                                                          "SELECT  USERGROUPS.GROUPID, USERGROUPS.DESCRIPTION FROM USERGROUPS USERGROUPS    WHERE NOT USERGROUPS.DESCRIPTION IS NULL ORDER BY DESCRIPTION", ""), 
                                            "USERGROUPS", "Linked.FrontOffice")

                    
                    resultData = .QueryExecuteToTable(True)
                End With

                If Not IsNothing(resultData) Then
                    For Each item As DataRow In resultData.Rows
                        newLookupList.Add(New With {.GROUPID = item.StringValue("GROUPID"),
                                                    .DESCRIPTION = item.StringValue("DESCRIPTION")})
                    Next
                End If

                result.Data = newLookupList

            Catch ex As Exception
                result = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "UserManager", "LookUpForGroupAssiged", String.Empty)
            End Try
            
            Return result
        End Function



       <WebMethod(EnableSession:=True)>
        Public Shared Function LookUpForSupervisors() As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim result As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True}
            Dim resultData As DataTable = Nothing
            Dim newLookupList As New List(Of Object)
            
            Try
                InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("Administrador")
                
                With New DataManagerFactory(String.Format(CultureInfo.CurrentCulture,
                                                          "SELECT  USERMEMBER.USERID, USERMEMBER.USERNAME FROM USERMEMBER USERMEMBER  WHERE USERMEMBER.ISANONYMOUS = '{0}'  AND NOT USERMEMBER.USERNAME IS NULL ORDER BY USERNAME", "0"), 
                                            "USERMEMBER", "Linked.FrontOffice")

                    
                    resultData = .QueryExecuteToTable(True)
                End With

                If Not IsNothing(resultData) Then
                    For Each item As DataRow In resultData.Rows
                        newLookupList.Add(New With {.USERID = item.NumericValue("USERID"),
                                                    .USERNAME = item.StringValue("USERNAME")})
                    Next
                End If

                result.Data = newLookupList

            Catch ex As Exception
                result = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "UserManager", "LookUpForSupervisors", String.Empty)
            End Try
            
            Return result
        End Function

#End Region

#Region "Form Actions"


#End Region





#Region "Common Utilities"


#End Region

#Region "Form Contracts"

        <Serializable()>
        <DataContract()>
        Public Class EntryData

            <DataMember()> Public Property InstanceFormId As String
            <DataMember()> Public Property UserListUserId As System.Int32
            <DataMember()> Public Property UserListUserName As System.String
            <DataMember()> Public Property UserListEmail As System.String
            <DataMember()> Public Property UserListIsEmployee As System.Boolean
            <DataMember()> Public Property UserListIsApproved As System.Boolean
            <DataMember()> Public Property UserListIsAdministrator As System.Boolean
            <DataMember()> Public Property UserListAllowScheduler As System.Boolean
            <DataMember()> Public Property UserListIsLockedOut As System.Boolean
            <DataMember()> Public Property UserListPasswordNeverExpires As System.Boolean
            <DataMember()> Public Property UserListRolAssiged As System.String
            <DataMember()> Public Property UserListGroupAssiged As System.String
            <DataMember()> Public Property UserListSecurityLevel As System.Int32
            <DataMember()> Public Property UserListClientId As System.String
            <DataMember()> Public Property UserListProducerId As System.String
            <DataMember()> Public Property UserListCreationDate As System.DateTime
            <DataMember()> Public Property UserListLastLoginDate As System.DateTime
            <DataMember()> Public Property UserListLastLockedOutDate As System.DateTime
            <DataMember()> Public Property UserListSupervisors As System.String
            <DataMember()> Public Property User_User As List(Of User_UserItem)
            <DataMember()> Public Property EmailOld As String
            <DataMember()> Public Property EmailChangeResult As Boolean
            <DataMember()> Public Property Type As Int32

        End Class

        <Serializable()>
        <DataContract()>
        Public Class User_UserItem

            <DataMember()> Public Property UserId As System.Int32
            <DataMember()> Public Property UserName As System.String
            <DataMember()> Public Property Email As System.String
            <DataMember()> Public Property IsEmployee As System.Boolean
            <DataMember()> Public Property IsApproved As System.Boolean
            <DataMember()> Public Property IsAdministrator As System.Boolean
            <DataMember()> Public Property AllowScheduler As System.Boolean
            <DataMember()> Public Property IsLockedOut As System.Boolean
            <DataMember()> Public Property PasswordNeverExpires As System.Boolean
            <DataMember()> Public Property RolAssiged As System.String
            <DataMember()> Public Property GroupAssiged As System.String
            <DataMember()> Public Property SecurityLevel As System.Int32
            <DataMember()> Public Property ClientId As System.String
            <DataMember()> Public Property ProducerId As System.String
            <DataMember()> Public Property CreationDate As System.DateTime
            <DataMember()> Public Property LastLoginDate As System.DateTime
            <DataMember()> Public Property LastLockedOutDate As System.DateTime
            <DataMember()> Public Property Supervisors As System.String

        End Class


#End Region

    End Class

End Namespace

