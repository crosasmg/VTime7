﻿'---------------------------------------------------------------------------------------------------
' <generated>
'     This code was generated by Form Designer v7.3.24.1 at 2019-11-08 03:13:32 p. m. model release 2, Form Generator v1.0.37.9
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

    Public Class H5MantRequerimientosPorRolActions
        Inherits System.Web.UI.Page

#Region "Actions Methods"
  

        <WebMethod()>
        Public Shared Function TabRequirementTypeByRole_GridTblDataLoad(filter As String) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            Dim response As Object = New With {.RequirementType = 0, .RoleCode = 0, .CreationDate = Date.MinValue, .CreatorUserCode = 0, .UpdateDate = Date.MinValue, .UpdateUserCode = 0}
            Dim selectDataTableItem As DataTable
            Dim responseList As New List(Of Object)
            
            
            Try
                InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")

                With New DataManagerFactory("SELECT TABREQUIREMENTTYPEBYROLE.REQUIREMENTTYPE, TABREQUIREMENTTYPEBYROLE.ROLECODE, TABREQUIREMENTTYPEBYROLE.CREATIONDATE, TABREQUIREMENTTYPEBYROLE.CREATORUSERCODE, TABREQUIREMENTTYPEBYROLE.UPDATEDATE, TABREQUIREMENTTYPEBYROLE.UPDATEUSERCODE FROM TABREQUIREMENTTYPEBYROLE TABREQUIREMENTTYPEBYROLE ", "TabRequirementTypeByRole", "Linked.Underwriting")

                    selectDataTableItem = .QueryExecuteToTable(True)
                End With
                With selectDataTableItem
                    If Not IsNothing(.Rows) AndAlso .Rows.Count > 0 Then
                        For Each itemData As DataRow In .Rows
                            response = New With {.RequirementType = itemData.NumericValue("REQUIREMENTTYPE"), .RoleCode = itemData.NumericValue("ROLECODE"), .CreationDate = itemData.DateTimeValue("CREATIONDATE"), .CreatorUserCode = itemData.NumericValue("CREATORUSERCODE"), .UpdateDate = itemData.DateTimeValue("UPDATEDATE"), .UpdateUserCode = itemData.NumericValue("UPDATEUSERCODE")}
                            
                            responseList.Add(response)
                        Next

                        With resultData
                            .Count = responseList.Count
                            .Data = responseList
                        End With
                    End If
                End With
            
            If responseList.Count <> 0 Then          
		    
            End If
                
            Catch ex As Exception            
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantRequerimientosPorRol", "TabRequirementTypeByRole_GridTblDataLoad", String.Empty)
            End Try
            
            Return resultData
        End Function

        <WebMethod()>
        Public Shared Function TabRequirementTypeByRole_Grid1InsertCommandActionTabRequirementTypeByRole(REQUIREMENTTYPE1 As Decimal, ROLECODE2 As Decimal, CREATORUSERCODE3 As Decimal, UPDATEUSERCODE5 As Decimal) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            
            Try
               InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")
                With New DataManagerFactory("INSERT INTO TabRequirementTypeByRole (REQUIREMENTTYPE, ROLECODE, CREATIONDATE, CREATORUSERCODE, UPDATEDATE, UPDATEUSERCODE) VALUES (@:REQUIREMENTTYPE1, @:ROLECODE2, @:CREATIONDATE2, @:CREATORUSERCODE3, @:UPDATEDATE4, @:UPDATEUSERCODE5)", "TabRequirementTypeByRole", "Linked.Underwriting")
                    .AddParameter("REQUIREMENTTYPE1", DbType.Decimal, 0, False, REQUIREMENTTYPE1)
                    .AddParameter("ROLECODE2", DbType.Decimal, 0, False, ROLECODE2)
                    .AddParameter("CREATIONDATE2", DbType.DateTime, 0, False, Date.Now)
                    .AddParameter("CREATORUSERCODE3", DbType.Decimal, 0, False, CREATORUSERCODE3)
                    .AddParameter("UPDATEDATE4", DbType.DateTime, 0, False, Date.Now)
                    .AddParameter("UPDATEUSERCODE5", DbType.Decimal, 0, False, UPDATEUSERCODE5)
 
                    .CommandExecute()
              End With

            Catch ex As Exception
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantRequerimientosPorRol", "TabRequirementTypeByRole_Grid1InsertCommandActionTabRequirementTypeByRole", String.Empty)
            End Try
            
            Return resultData
        End Function

        <WebMethod()>
        Public Shared Function TabRequirementTypeByRole_Grid1UpdateCommandActionTabRequirementTypeByRole(UPDATEUSERCODE1 As Decimal, TabRequirementTypeByRoleRequirementType3 As Decimal, TabRequirementTypeByRoleRoleCode4 As Decimal) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            
            Try
               InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")
                With New DataManagerFactory("UPDATE TabRequirementTypeByRole SET UPDATEDATE = @:UPDATEDATE0, UPDATEUSERCODE = @:UPDATEUSERCODE1 WHERE TABREQUIREMENTTYPEBYROLE.REQUIREMENTTYPE = @:REQUIREMENTTYPE3 AND TABREQUIREMENTTYPEBYROLE.ROLECODE = @:ROLECODE4", "TabRequirementTypeByRole", "Linked.Underwriting")
                    .AddParameter("UPDATEDATE0", DbType.DateTime, 0, False, Date.Now)
                    .AddParameter("UPDATEUSERCODE1", DbType.Decimal, 0, False, UPDATEUSERCODE1)
                    .AddParameter("REQUIREMENTTYPE3", DbType.Decimal, 0, False, TabRequirementTypeByRoleRequirementType3)
                    .AddParameter("ROLECODE4", DbType.Decimal, 0, False, TabRequirementTypeByRoleRoleCode4)
 
                    .CommandExecute()
              End With

            Catch ex As Exception
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantRequerimientosPorRol", "TabRequirementTypeByRole_Grid1UpdateCommandActionTabRequirementTypeByRole", String.Empty)
            End Try
            
            Return resultData
        End Function

        <WebMethod()>
        Public Shared Function TabRequirementTypeByRole_Grid1DeleteCommandActionTabRequirementTypeByRole(TabRequirementTypeByRoleRequirementType1 As Decimal, TabRequirementTypeByRoleRoleCode2 As Decimal) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            
            Try
               InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")
                With New DataManagerFactory("DELETE FROM TabRequirementTypeByRole WHERE TABREQUIREMENTTYPEBYROLE.REQUIREMENTTYPE = @:REQUIREMENTTYPE1 AND TABREQUIREMENTTYPEBYROLE.ROLECODE = @:ROLECODE2", "TabRequirementTypeByRole", "Linked.Underwriting")
                    .AddParameter("REQUIREMENTTYPE1", DbType.Decimal, 0, False, TabRequirementTypeByRoleRequirementType1)
                    .AddParameter("ROLECODE2", DbType.Decimal, 0, False, TabRequirementTypeByRoleRoleCode2)
 
                    .CommandExecute()
              End With

            Catch ex As Exception
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantRequerimientosPorRol", "TabRequirementTypeByRole_Grid1DeleteCommandActionTabRequirementTypeByRole", String.Empty)
            End Try
            
            Return resultData
        End Function


#End Region

#Region "Lookups Web Methods"


        <WebMethod(EnableSession:=True)>
        Public Shared Function LookUpForRequirementType(id As String) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim result As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True}
            Dim resultData As DataTable = Nothing
            Dim newLookupList As New List(Of InMotionGIT.Common.DataType.LookUpValue)                       
            
            Try
                InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")
                 
                Dim UserInfo As New InMotionGIT.Membership.Providers.MemberContext
                With New DataManagerFactory(String.Format(CultureInfo.CurrentCulture,
                                                          "SELECT  TABREQUIREMENTTYPE.REQUIREMENTTYPE, TRANSREQUIREMENTTYPE.REQUIREMENTTYPE, TRANSREQUIREMENTTYPE.LANGUAGEID, TRANSREQUIREMENTTYPE.DESCRIPTION FROM TABREQUIREMENTTYPE TABREQUIREMENTTYPE JOIN TRANSREQUIREMENTTYPE TRANSREQUIREMENTTYPE ON TRANSREQUIREMENTTYPE.REQUIREMENTTYPE = TABREQUIREMENTTYPE.REQUIREMENTTYPE  WHERE TRANSREQUIREMENTTYPE.LANGUAGEID = {0}  AND NOT TRANSREQUIREMENTTYPE.DESCRIPTION IS NULL ORDER BY DESCRIPTION", InMotionGIT.FASI.Support.Handlers.LanguageHandler.ContextLanguageId()), 
                                            "TabRequirementType", "Linked.Underwriting")

                    
                    resultData = .QueryExecuteToTable(True)
                End With

                If Not IsNothing(resultData) Then
                    For Each item As DataRow In resultData.Rows
                        newLookupList.Add(New InMotionGIT.Common.DataType.LookUpValue With {.Code = item.NumericValue("RequirementType"),
                                                                                            .Description = item.StringValue("Description")})
                    Next
                End If

                result.Data = newLookupList

            Catch ex As Exception
                result = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantRequerimientosPorRol", "LookUpForRequirementType", String.Empty)
            End Try
                        
            Return result
        End Function

        <WebMethod(EnableSession:=True)>
        Public Shared Function LookUpForRoleCode(id As String) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim result As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True}
            Dim resultData As DataTable = Nothing
            Dim newLookupList As New List(Of InMotionGIT.Common.DataType.LookUpValue)                       
            
            Try
                InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")
                 
                Dim UserInfo As New InMotionGIT.Membership.Providers.MemberContext
                With New DataManagerFactory(String.Format(CultureInfo.CurrentCulture,
                                                          "SELECT  TABROLETYPE.ROLECODE, TRIM(TABROLETYPE.RECORDSTATUS) RECORDSTATUS, TRANSROLETYPE.ROLECODE, TRANSROLETYPE.LANGUAGEID, TRANSROLETYPE.DESCRIPTION FROM TABROLETYPE TABROLETYPE JOIN TRANSROLETYPE TRANSROLETYPE ON TRANSROLETYPE.ROLECODE = TABROLETYPE.ROLECODE  WHERE TRANSROLETYPE.LANGUAGEID = {0} AND TABROLETYPE.RECORDSTATUS = '{1}'  AND NOT TRANSROLETYPE.DESCRIPTION IS NULL ORDER BY DESCRIPTION", InMotionGIT.FASI.Support.Handlers.LanguageHandler.ContextLanguageId(), "1"), 
                                            "TabRoleType", "Linked.Underwriting")

                    
                    resultData = .QueryExecuteToTable(True)
                End With

                If Not IsNothing(resultData) Then
                    For Each item As DataRow In resultData.Rows
                        newLookupList.Add(New InMotionGIT.Common.DataType.LookUpValue With {.Code = item.NumericValue("RoleCode"),
                                                                                            .Description = item.StringValue("Description")})
                    Next
                End If

                result.Data = newLookupList

            Catch ex As Exception
                result = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantRequerimientosPorRol", "LookUpForRoleCode", String.Empty)
            End Try
                        
            Return result
        End Function

#End Region

#Region "Form Actions"


#End Region





#Region "Common Utilities"

        Private Shared Sub SetDefaultValuesFromQueryString(formData As EntryData)

        End Sub	


#End Region

#Region "Form Contracts"

        <Serializable()>
        <DataContract()>
        Public Class EntryData

            <DataMember()> Public Property InstanceFormId As String
            <DataMember()> Public Property TabRequirementTypeByRoleCollectionRequirementType As System.Decimal
            <DataMember()> Public Property TabRequirementTypeByRoleCollectionRoleCode As System.Decimal
            <DataMember()> Public Property TabRequirementTypeByRoleCollectionCreationDate As System.DateTime
            <DataMember()> Public Property TabRequirementTypeByRoleCollectionCreatorUserCode As System.Decimal
            <DataMember()> Public Property TabRequirementTypeByRoleCollectionUpdateDate As System.DateTime
            <DataMember()> Public Property TabRequirementTypeByRoleCollectionUpdateUserCode As System.Decimal
            <DataMember()> Public Property TabRequirementTypeByRole_Grid_TabRequirementTypeByRole_Item As List(Of TabRequirementTypeByRole_Grid_TabRequirementTypeByRole_ItemItem)

        End Class

        <Serializable()>
        <DataContract()>
        Public Class TabRequirementTypeByRole_Grid_TabRequirementTypeByRole_ItemItem

            <DataMember()> Public Property RequirementType As System.Decimal
            <DataMember()> Public Property RoleCode As System.Decimal
            <DataMember()> Public Property CreationDate As System.DateTime
            <DataMember()> Public Property CreatorUserCode As System.Decimal
            <DataMember()> Public Property UpdateDate As System.DateTime
            <DataMember()> Public Property UpdateUserCode As System.Decimal

        End Class


#End Region

    End Class

End Namespace

