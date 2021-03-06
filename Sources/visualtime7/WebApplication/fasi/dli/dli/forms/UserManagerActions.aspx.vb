'---------------------------------------------------------------------------------------------------
' <generated>
'     This code was generated by Form Designer v7.1.212.1 at 2018/11/07 03:23:00 PM model release 35, Form Generator v1.0.33.2
'     
'     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
' </generated>
'---------------------------------------------------------------------------------------------------
      
#Region "using"

Imports System.Data
Imports System.Globalization
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
   

#End Region

#Region "Lookups Web Methods"



       <WebMethod()>
        Public Shared Function LookUpForRolAssiged() As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            Dim result As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True}
            Dim resultData As DataTable = Nothing
            Dim newLookupList As New List(Of Object)

            Try
                
                With New DataManagerFactory(String.Format(CultureInfo.CurrentCulture,
                                                          "SELECT  ROLE.ROLEID, ROLE.ROLENAME, ROLE.SECURITYLEVEL FROM ROLE ROLE    WHERE NOT ROLENAME IS NULL ORDER BY ROLENAME", ""), 
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
                LogHandler.ErrorLog("UserManager", "LookUpForRolAssiged", ex)

                With result
                    .Success = False
                    .Reason = String.Format(CultureInfo.InvariantCulture, "{0} ({1})", ex.Message, "LookUpForRolAssiged")
                End With
            End Try
            Return result
        End Function

       <WebMethod()>
        Public Shared Function LookUpForGroupAssiged() As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            Dim result As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True}
            Dim resultData As DataTable = Nothing
            Dim newLookupList As New List(Of Object)

            Try
                
                With New DataManagerFactory(String.Format(CultureInfo.CurrentCulture,
                                                          "SELECT  USERGROUPS.GROUPID, USERGROUPS.DESCRIPTION FROM USERGROUPS USERGROUPS    WHERE NOT DESCRIPTION IS NULL ORDER BY DESCRIPTION", ""), 
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
                LogHandler.ErrorLog("UserManager", "LookUpForGroupAssiged", ex)

                With result
                    .Success = False
                    .Reason = String.Format(CultureInfo.InvariantCulture, "{0} ({1})", ex.Message, "LookUpForGroupAssiged")
                End With
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
            <DataMember()> Public Property UserListSupervisorId As System.Int32
            <DataMember()> Public Property UserListRolAssiged As System.String
            <DataMember()> Public Property UserListGroupAssiged As System.String
            <DataMember()> Public Property UserListSecurityLevel As System.Int32
            <DataMember()> Public Property UserListClientId As System.String
            <DataMember()> Public Property UserListProducerId As System.String
            <DataMember()> Public Property UserListCreationDate As System.DateTime
            <DataMember()> Public Property UserListLastLoginDate As System.DateTime
            <DataMember()> Public Property UserListLastLockedOutDate As System.DateTime
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
            <DataMember()> Public Property SupervisorId As System.Int32
            <DataMember()> Public Property RolAssiged As System.String
            <DataMember()> Public Property GroupAssiged As System.String
            <DataMember()> Public Property SecurityLevel As System.Int32
            <DataMember()> Public Property ClientId As System.String
            <DataMember()> Public Property ProducerId As System.String
            <DataMember()> Public Property CreationDate As System.DateTime
            <DataMember()> Public Property LastLoginDate As System.DateTime
            <DataMember()> Public Property LastLockedOutDate As System.DateTime

        End Class


#End Region

    End Class

End Namespace