﻿'---------------------------------------------------------------------------------------------------
' <generated>
'     This code was generated by Form Designer v7.3.24.1 at 2019-11-08 03:54:38 p. m. model release 1, Form Generator v1.0.37.9
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

    Public Class H5MAntEstadoRequisitosActions
        Inherits System.Web.UI.Page

#Region "Actions Methods"
  

        <WebMethod()>
        Public Shared Function TabRequirementStatusType_GridTblDataLoad(filter As String, TransRequirementStatusTypeLanguageId1 As String) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            Dim response As Object = New With {.RequirementStatus = 0, .RecordStatus = 0, .CreatorUserCode = 0, .CreationDate = Date.MinValue, .UpdateUserCode = 0, .UpdateDate = Date.MinValue, .Description = String.Empty, .ShortDescription = String.Empty}
            Dim selectDataTableItem As DataTable
            Dim responseList As New List(Of Object)
            
            
            Try
                InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")

                With New DataManagerFactory("SELECT TABREQUIREMENTSTATUSTYPE.REQUIREMENTSTATUS, TABREQUIREMENTSTATUSTYPE.RECORDSTATUS, TABREQUIREMENTSTATUSTYPE.CREATORUSERCODE, TABREQUIREMENTSTATUSTYPE.CREATIONDATE, TABREQUIREMENTSTATUSTYPE.UPDATEUSERCODE, TABREQUIREMENTSTATUSTYPE.UPDATEDATE, TRANSREQUIREMENTSTATUSTYPE.DESCRIPTION, TRANSREQUIREMENTSTATUSTYPE.SHORTDESCRIPTION FROM TABREQUIREMENTSTATUSTYPE TABREQUIREMENTSTATUSTYPE  LEFT JOIN TRANSREQUIREMENTSTATUSTYPE TRANSREQUIREMENTSTATUSTYPE ON TRANSREQUIREMENTSTATUSTYPE.REQUIREMENTSTATUS = TABREQUIREMENTSTATUSTYPE.REQUIREMENTSTATUS  AND TRANSREQUIREMENTSTATUSTYPE.LANGUAGEID = @:LANGUAGEID1", "TabRequirementStatusType", "Linked.Underwriting")
                    .AddParameter("LANGUAGEID1", DbType.AnsiString, 5, (TransRequirementStatusTypeLanguageId1 = String.Empty), TransRequirementStatusTypeLanguageId1)

                    selectDataTableItem = .QueryExecuteToTable(True)
                End With
                With selectDataTableItem
                    If Not IsNothing(.Rows) AndAlso .Rows.Count > 0 Then
                        For Each itemData As DataRow In .Rows
                            response = New With {.RequirementStatus = itemData.NumericValue("REQUIREMENTSTATUS"), .RecordStatus = itemData.NumericValue("RECORDSTATUS"), .CreatorUserCode = itemData.NumericValue("CREATORUSERCODE"), .CreationDate = itemData.DateTimeValue("CREATIONDATE"), .UpdateUserCode = itemData.NumericValue("UPDATEUSERCODE"), .UpdateDate = itemData.DateTimeValue("UPDATEDATE"), .Description = itemData.StringValue("DESCRIPTION"), .ShortDescription = itemData.StringValue("SHORTDESCRIPTION")}
                            
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
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MAntEstadoRequisitos", "TabRequirementStatusType_GridTblDataLoad", String.Empty)
            End Try
            
            Return resultData
        End Function

        <WebMethod()>
        Public Shared Function TabRequirementStatusType_Grid1InsertCommandActionTabRequirementStatusType(REQUIREMENTSTATUS1 As Decimal, RECORDSTATUS2 As Decimal, CREATORUSERCODE2 As Decimal, UPDATEUSERCODE4 As Decimal) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            
            Try
               InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")
                With New DataManagerFactory("INSERT INTO TabRequirementStatusType (REQUIREMENTSTATUS, RECORDSTATUS, CREATORUSERCODE, CREATIONDATE, UPDATEUSERCODE, UPDATEDATE) VALUES (@:REQUIREMENTSTATUS1, @:RECORDSTATUS2, @:CREATORUSERCODE2, @:CREATIONDATE3, @:UPDATEUSERCODE4, @:UPDATEDATE5)", "TabRequirementStatusType", "Linked.Underwriting")
                    .AddParameter("REQUIREMENTSTATUS1", DbType.Decimal, 0, False, REQUIREMENTSTATUS1)
                    .AddParameter("RECORDSTATUS2", DbType.Decimal, 0, False, RECORDSTATUS2)
                    .AddParameter("CREATORUSERCODE2", DbType.Decimal, 0, False, CREATORUSERCODE2)
                    .AddParameter("CREATIONDATE3", DbType.DateTime, 0, False, Date.Now)
                    .AddParameter("UPDATEUSERCODE4", DbType.Decimal, 0, False, UPDATEUSERCODE4)
                    .AddParameter("UPDATEDATE5", DbType.DateTime, 0, False, Date.Now)
 
                    .CommandExecute()
              End With

            Catch ex As Exception
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MAntEstadoRequisitos", "TabRequirementStatusType_Grid1InsertCommandActionTabRequirementStatusType", String.Empty)
            End Try
            
            Return resultData
        End Function
        <WebMethod()>
        Public Shared Function TabRequirementStatusType_Grid3InsertCommandActionTransRequirementStatusType(REQUIREMENTSTATUS1 As Decimal, LANGUAGEID1 As Decimal, DESCRIPTION3 As String, SHORTDESCRIPTION4 As String, CREATORUSERCODE4 As Decimal, UPDATEUSERCODE6 As Decimal) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            
            Try
               InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")
                    With New DataManagerFactory("INSERT INTO TransRequirementStatusType (REQUIREMENTSTATUS, LANGUAGEID, DESCRIPTION, SHORTDESCRIPTION, CREATORUSERCODE, CREATIONDATE, UPDATEUSERCODE, UPDATEDATE) VALUES (@:REQUIREMENTSTATUS1, @:LANGUAGEID1, @:DESCRIPTION3, @:SHORTDESCRIPTION4, @:CREATORUSERCODE4, @:CREATIONDATE5, @:UPDATEUSERCODE6, @:UPDATEDATE7)", "TransRequirementStatusType", "Linked.Underwriting")
                    .AddParameter("REQUIREMENTSTATUS1", DbType.Decimal, 0, False, REQUIREMENTSTATUS1)
                    .AddParameter("LANGUAGEID1", DbType.Decimal, 0, False, LANGUAGEID1)
                    .AddParameter("DESCRIPTION3", DbType.AnsiString, 0, (DESCRIPTION3 = String.Empty), DESCRIPTION3)
                    .AddParameter("SHORTDESCRIPTION4", DbType.AnsiString, 0, (SHORTDESCRIPTION4 = String.Empty), SHORTDESCRIPTION4)
                    .AddParameter("CREATORUSERCODE4", DbType.Decimal, 0, False, CREATORUSERCODE4)
                    .AddParameter("CREATIONDATE5", DbType.DateTime, 0, False, Date.Now)
                    .AddParameter("UPDATEUSERCODE6", DbType.Decimal, 0, False, UPDATEUSERCODE6)
                    .AddParameter("UPDATEDATE7", DbType.DateTime, 0, False, Date.Now)
 
                    .CommandExecute()
              End With

            Catch ex As Exception
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MAntEstadoRequisitos", "TabRequirementStatusType_Grid3InsertCommandActionTransRequirementStatusType", String.Empty)
            End Try
            
            Return resultData
        End Function

        <WebMethod()>
        Public Shared Function TabRequirementStatusType_Grid1UpdateCommandActionTabRequirementStatusType(RECORDSTATUS1 As Decimal, UPDATEUSERCODE1 As Decimal, TabRequirementStatusTypeRequirementStatus3 As Decimal) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            
            Try
               InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")
                With New DataManagerFactory("UPDATE TabRequirementStatusType SET RECORDSTATUS = @:RECORDSTATUS1, UPDATEUSERCODE = @:UPDATEUSERCODE1 WHERE TABREQUIREMENTSTATUSTYPE.REQUIREMENTSTATUS = @:REQUIREMENTSTATUS3", "TabRequirementStatusType", "Linked.Underwriting")
                    .AddParameter("RECORDSTATUS1", DbType.Decimal, 0, False, RECORDSTATUS1)
                    .AddParameter("UPDATEUSERCODE1", DbType.Decimal, 0, False, UPDATEUSERCODE1)
                    .AddParameter("REQUIREMENTSTATUS3", DbType.Decimal, 0, False, TabRequirementStatusTypeRequirementStatus3)
 
                    .CommandExecute()
              End With

            Catch ex As Exception
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MAntEstadoRequisitos", "TabRequirementStatusType_Grid1UpdateCommandActionTabRequirementStatusType", String.Empty)
            End Try
            
            Return resultData
        End Function
        <WebMethod()>
        Public Shared Function TabRequirementStatusType_Grid3SelectCommandActionTransRequirementStatusType(TransRequirementStatusTypeRequirementStatus1 As Decimal, TransRequirementStatusTypeLanguageId2 As Decimal) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            Dim response As Object = New With {.Result = 0}
            Dim responseList As New List(Of Object)
            
            Try
                InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")

                With New DataManagerFactory("Select COUNT(TRANSREQUIREMENTSTATUSTYPE.REQUIREMENTSTATUS) REQUIREMENTSTATUS FROM TRANSREQUIREMENTSTATUSTYPE TRANSREQUIREMENTSTATUSTYPE  WHERE TRANSREQUIREMENTSTATUSTYPE.REQUIREMENTSTATUS = @:REQUIREMENTSTATUS1 AND TRANSREQUIREMENTSTATUSTYPE.LANGUAGEID = @:LANGUAGEID2", "TransRequirementStatusType", "Linked.Underwriting")
                    .AddParameter("REQUIREMENTSTATUS1", DbType.Decimal, 0, False, TransRequirementStatusTypeRequirementStatus1)
                    .AddParameter("LANGUAGEID2", DbType.Decimal, 0, False, TransRequirementStatusTypeLanguageId2)

                    response.Result = .QueryExecuteScalarToInteger()
                End With
                With resultData
                    .Count = 1
                    .Data = response
                End With
            
            If responseList.Count <> 0 Then          
		    
            End If
                
            Catch ex As Exception            
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MAntEstadoRequisitos", "TabRequirementStatusType_Grid3SelectCommandActionTransRequirementStatusType", String.Empty)
            End Try
            
            Return resultData
        End Function
        <WebMethod()>
        Public Shared Function TabRequirementStatusType_Grid5InsertCommandActionTransRequirementStatusType(REQUIREMENTSTATUS1 As Decimal, LANGUAGEID1 As Decimal, DESCRIPTION3 As String, SHORTDESCRIPTION4 As String, CREATORUSERCODE4 As Decimal, UPDATEUSERCODE6 As Decimal) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            
            Try
               InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")
                        With New DataManagerFactory("INSERT INTO TransRequirementStatusType (REQUIREMENTSTATUS, LANGUAGEID, DESCRIPTION, SHORTDESCRIPTION, CREATORUSERCODE, CREATIONDATE, UPDATEUSERCODE, UPDATEDATE) VALUES (@:REQUIREMENTSTATUS1, @:LANGUAGEID1, @:DESCRIPTION3, @:SHORTDESCRIPTION4, @:CREATORUSERCODE4, @:CREATIONDATE5, @:UPDATEUSERCODE6, @:UPDATEDATE7)", "TransRequirementStatusType", "Linked.Underwriting")
                    .AddParameter("REQUIREMENTSTATUS1", DbType.Decimal, 0, False, REQUIREMENTSTATUS1)
                    .AddParameter("LANGUAGEID1", DbType.Decimal, 0, False, LANGUAGEID1)
                    .AddParameter("DESCRIPTION3", DbType.AnsiString, 0, (DESCRIPTION3 = String.Empty), DESCRIPTION3)
                    .AddParameter("SHORTDESCRIPTION4", DbType.AnsiString, 0, (SHORTDESCRIPTION4 = String.Empty), SHORTDESCRIPTION4)
                    .AddParameter("CREATORUSERCODE4", DbType.Decimal, 0, False, CREATORUSERCODE4)
                    .AddParameter("CREATIONDATE5", DbType.DateTime, 0, False, Date.Now)
                    .AddParameter("UPDATEUSERCODE6", DbType.Decimal, 0, False, UPDATEUSERCODE6)
                    .AddParameter("UPDATEDATE7", DbType.DateTime, 0, False, Date.Now)
 
                    .CommandExecute()
              End With

            Catch ex As Exception
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MAntEstadoRequisitos", "TabRequirementStatusType_Grid5InsertCommandActionTransRequirementStatusType", String.Empty)
            End Try
            
            Return resultData
        End Function
        <WebMethod()>
        Public Shared Function TabRequirementStatusType_Grid6UpdateCommandActionTransRequirementStatusType(DESCRIPTION1 As String, SHORTDESCRIPTION2 As String, UPDATEUSERCODE2 As Decimal, TransRequirementStatusTypeRequirementStatus4 As Decimal, TransRequirementStatusTypeLanguageId5 As Decimal) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            
            Try
               InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")
                        With New DataManagerFactory("UPDATE TransRequirementStatusType SET DESCRIPTION = @:DESCRIPTION1, SHORTDESCRIPTION = @:SHORTDESCRIPTION2, UPDATEUSERCODE = @:UPDATEUSERCODE2 WHERE TRANSREQUIREMENTSTATUSTYPE.REQUIREMENTSTATUS = @:REQUIREMENTSTATUS4 AND TRANSREQUIREMENTSTATUSTYPE.LANGUAGEID = @:LANGUAGEID5", "TransRequirementStatusType", "Linked.Underwriting")
                    .AddParameter("DESCRIPTION1", DbType.AnsiString, 0, (DESCRIPTION1 = String.Empty), DESCRIPTION1)
                    .AddParameter("SHORTDESCRIPTION2", DbType.AnsiString, 0, (SHORTDESCRIPTION2 = String.Empty), SHORTDESCRIPTION2)
                    .AddParameter("UPDATEUSERCODE2", DbType.Decimal, 0, False, UPDATEUSERCODE2)
                    .AddParameter("REQUIREMENTSTATUS4", DbType.Decimal, 0, False, TransRequirementStatusTypeRequirementStatus4)
                    .AddParameter("LANGUAGEID5", DbType.Decimal, 0, False, TransRequirementStatusTypeLanguageId5)
 
                    .CommandExecute()
              End With

            Catch ex As Exception
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MAntEstadoRequisitos", "TabRequirementStatusType_Grid6UpdateCommandActionTransRequirementStatusType", String.Empty)
            End Try
            
            Return resultData
        End Function

        <WebMethod()>
        Public Shared Function TabRequirementStatusType_Grid1DeleteCommandActionTransRequirementStatusType(TransRequirementStatusTypeRequirementStatus1 As Decimal) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            
            Try
               InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")
                With New DataManagerFactory("DELETE FROM TransRequirementStatusType WHERE TRANSREQUIREMENTSTATUSTYPE.REQUIREMENTSTATUS = @:REQUIREMENTSTATUS1", "TransRequirementStatusType", "Linked.Underwriting")
                    .AddParameter("REQUIREMENTSTATUS1", DbType.Decimal, 0, False, TransRequirementStatusTypeRequirementStatus1)
 
                    .CommandExecute()
              End With

            Catch ex As Exception
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MAntEstadoRequisitos", "TabRequirementStatusType_Grid1DeleteCommandActionTransRequirementStatusType", String.Empty)
            End Try
            
            Return resultData
        End Function
        <WebMethod()>
        Public Shared Function TabRequirementStatusType_Grid3DeleteCommandActionTabRequirementStatusType(TabRequirementStatusTypeRequirementStatus1 As Decimal) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            
            Try
               InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")
                    With New DataManagerFactory("DELETE FROM TabRequirementStatusType WHERE TABREQUIREMENTSTATUSTYPE.REQUIREMENTSTATUS = @:REQUIREMENTSTATUS1", "TabRequirementStatusType", "Linked.Underwriting")
                    .AddParameter("REQUIREMENTSTATUS1", DbType.Decimal, 0, False, TabRequirementStatusTypeRequirementStatus1)
 
                    .CommandExecute()
              End With

            Catch ex As Exception
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MAntEstadoRequisitos", "TabRequirementStatusType_Grid3DeleteCommandActionTabRequirementStatusType", String.Empty)
            End Try
            
            Return resultData
        End Function

        <WebMethod()>
        Public Shared Function TabRequirementStatusType_Grid2SelectCommandActionTabRequirementStatusType() As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            Dim response As Object = New With {.Result = 0}
            Dim responseList As New List(Of Object)
            
            Try
                InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")

                With New DataManagerFactory("Select MAX(TABREQUIREMENTSTATUSTYPE.REQUIREMENTSTATUS) REQUIREMENTSTATUS FROM TABREQUIREMENTSTATUSTYPE TABREQUIREMENTSTATUSTYPE ", "TabRequirementStatusType", "Linked.Underwriting")

                    response.Result = .QueryExecuteScalarToInteger()
                End With
                With resultData
                    .Count = 1
                    .Data = response
                End With
            
            If responseList.Count <> 0 Then          
		    
            End If
                
            Catch ex As Exception            
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MAntEstadoRequisitos", "TabRequirementStatusType_Grid2SelectCommandActionTabRequirementStatusType", String.Empty)
            End Try
            
            Return resultData
        End Function

        <WebMethod()>
        Public Shared Function TabRequirementStatusTypeTranslator_GridTblDataLoad(filter As String) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            Dim response As Object = New With {.RequirementStatus = 0, .LanguageId = 0, .Description = String.Empty, .ShortDescription = String.Empty}
            Dim selectDataTableItem As DataTable
            Dim responseList As New List(Of Object)
            
            
            Try
                InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")

                With New DataManagerFactory("SELECT TABREQUIREMENTSTATUSTYPE.REQUIREMENTSTATUS, TRANSREQUIREMENTSTATUSTYPE.LANGUAGEID, TRANSREQUIREMENTSTATUSTYPE.DESCRIPTION, TRANSREQUIREMENTSTATUSTYPE.SHORTDESCRIPTION FROM TABREQUIREMENTSTATUSTYPE TABREQUIREMENTSTATUSTYPE  LEFT JOIN TRANSREQUIREMENTSTATUSTYPE TRANSREQUIREMENTSTATUSTYPE ON TRANSREQUIREMENTSTATUSTYPE.REQUIREMENTSTATUS = TABREQUIREMENTSTATUSTYPE.REQUIREMENTSTATUS ", "TabRequirementStatusType", "Linked.Underwriting")

                    selectDataTableItem = .QueryExecuteToTable(True)
                End With
                With selectDataTableItem
                    If Not IsNothing(.Rows) AndAlso .Rows.Count > 0 Then
                        For Each itemData As DataRow In .Rows
                            response = New With {.RequirementStatus = itemData.NumericValue("REQUIREMENTSTATUS"), .LanguageId = itemData.NumericValue("LANGUAGEID"), .Description = itemData.StringValue("DESCRIPTION"), .ShortDescription = itemData.StringValue("SHORTDESCRIPTION")}
                            
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
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MAntEstadoRequisitos", "TabRequirementStatusTypeTranslator_GridTblDataLoad", String.Empty)
            End Try
            
            Return resultData
        End Function

        <WebMethod()>
        Public Shared Function TabRequirementStatusTypeTranslator_Grid1UpdateCommandActionTransRequirementStatusType(DESCRIPTION1 As String, SHORTDESCRIPTION2 As String, UPDATEUSERCODE2 As Decimal, TransRequirementStatusTypeRequirementStatus4 As Decimal, TransRequirementStatusTypeLanguageId5 As Decimal) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            
            Try
               InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")
                With New DataManagerFactory("UPDATE TransRequirementStatusType SET DESCRIPTION = @:DESCRIPTION1, SHORTDESCRIPTION = @:SHORTDESCRIPTION2, UPDATEUSERCODE = @:UPDATEUSERCODE2 WHERE TRANSREQUIREMENTSTATUSTYPE.REQUIREMENTSTATUS = @:REQUIREMENTSTATUS4 AND TRANSREQUIREMENTSTATUSTYPE.LANGUAGEID = @:LANGUAGEID5", "TransRequirementStatusType", "Linked.Underwriting")
                    .AddParameter("DESCRIPTION1", DbType.AnsiString, 0, (DESCRIPTION1 = String.Empty), DESCRIPTION1)
                    .AddParameter("SHORTDESCRIPTION2", DbType.AnsiString, 0, (SHORTDESCRIPTION2 = String.Empty), SHORTDESCRIPTION2)
                    .AddParameter("UPDATEUSERCODE2", DbType.Decimal, 0, False, UPDATEUSERCODE2)
                    .AddParameter("REQUIREMENTSTATUS4", DbType.Decimal, 0, False, TransRequirementStatusTypeRequirementStatus4)
                    .AddParameter("LANGUAGEID5", DbType.Decimal, 0, False, TransRequirementStatusTypeLanguageId5)
 
                    .CommandExecute()
              End With

            Catch ex As Exception
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MAntEstadoRequisitos", "TabRequirementStatusTypeTranslator_Grid1UpdateCommandActionTransRequirementStatusType", String.Empty)
            End Try
            
            Return resultData
        End Function


#End Region

#Region "Lookups Web Methods"


        <WebMethod(EnableSession:=True)>
        Public Shared Function LookUpForRecordStatus(id As String) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim result As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True}
            Dim resultData As DataTable = Nothing
            Dim newLookupList As New List(Of InMotionGIT.Common.DataType.LookUpValue)                       
            
            Try
                InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")
                 
                Dim UserInfo As New InMotionGIT.Membership.Providers.MemberContext
                With New DataManagerFactory(String.Format(CultureInfo.CurrentCulture,
                                                          "SELECT TRIM(ETRANRECORDSTATUS.RECORDSTATUS) RECORDSTATUS, ETRANRECORDSTATUS.DESCRIPTION FROM ETRANRECORDSTATUS ETRANRECORDSTATUS WHERE ETRANRECORDSTATUS.LANGUAGEID = {0} AND NOT ETRANRECORDSTATUS.DESCRIPTION IS NULL ORDER BY DESCRIPTION", InMotionGIT.FASI.Support.Handlers.LanguageHandler.ContextLanguageId()), 
                                            "ETranRecordStatus", "Linked.Common")

                    
                    resultData = .QueryExecuteToTable(True)
                End With

                If Not IsNothing(resultData) Then
                    For Each item As DataRow In resultData.Rows
                        newLookupList.Add(New InMotionGIT.Common.DataType.LookUpValue With {.Code = item.StringValue("RecordStatus"),
                                                                                            .Description = item.StringValue("Description")})
                    Next
                End If

                result.Data = newLookupList

            Catch ex As Exception
                result = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MAntEstadoRequisitos", "LookUpForRecordStatus", String.Empty)
            End Try
                        
            Return result
        End Function

        <WebMethod(EnableSession:=True)>
        Public Shared Function LookUpForLanguageIdTranslator(id As String) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim result As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True}
            Dim resultData As DataTable = Nothing
            Dim newLookupList As New List(Of InMotionGIT.Common.DataType.LookUpValue)                       
            
            Try
                InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")
                 
                Dim UserInfo As New InMotionGIT.Membership.Providers.MemberContext
                With New DataManagerFactory(String.Format(CultureInfo.CurrentCulture,
                                                          "SELECT TRANSLANGUAGE.LANGUAGECODEID, TRANSLANGUAGE.DESCRIPTION FROM TRANSLANGUAGE TRANSLANGUAGE WHERE TRANSLANGUAGE.LANGUAGEID = {0} AND NOT TRANSLANGUAGE.DESCRIPTION IS NULL ORDER BY DESCRIPTION", InMotionGIT.FASI.Support.Handlers.LanguageHandler.ContextLanguageId()), 
                                            "TransLanguage", "Linked.Common")

                    
                    resultData = .QueryExecuteToTable(True)
                End With

                If Not IsNothing(resultData) Then
                    For Each item As DataRow In resultData.Rows
                        newLookupList.Add(New InMotionGIT.Common.DataType.LookUpValue With {.Code = item.NumericValue("LanguageCodeID"),
                                                                                            .Description = item.StringValue("Description")})
                    Next
                End If

                result.Data = newLookupList

            Catch ex As Exception
                result = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MAntEstadoRequisitos", "LookUpForLanguageIdTranslator", String.Empty)
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
            <DataMember()> Public Property TabRequirementStatusTypeCollectionRequirementStatus As System.Double
            <DataMember()> Public Property TabRequirementStatusTypeCollectionRecordStatus As System.Double
            <DataMember()> Public Property TabRequirementStatusTypeCollectionCreatorUserCode As System.Double
            <DataMember()> Public Property TabRequirementStatusTypeCollectionCreationDate As System.DateTime
            <DataMember()> Public Property TabRequirementStatusTypeCollectionUpdateUserCode As System.Double
            <DataMember()> Public Property TabRequirementStatusTypeCollectionUpdateDate As System.DateTime
            <DataMember()> Public Property TabRequirementStatusTypeCollectionDescription As System.String
            <DataMember()> Public Property TabRequirementStatusTypeCollectionShortDescription As System.String
            <DataMember()> Public Property TabRequirementStatusType_Grid_TabRequirementStatusType_Item As List(Of TabRequirementStatusType_Grid_TabRequirementStatusType_ItemItem)
            <DataMember()> Public Property TabRequirementStatusTypeCollectionLanguageId As System.Double
            <DataMember()> Public Property TabRequirementStatusTypeTranslator_Grid_TabRequirementStatusType_Item As List(Of TabRequirementStatusTypeTranslator_Grid_TabRequirementStatusType_ItemItem)

        End Class

        <Serializable()>
        <DataContract()>
        Public Class TabRequirementStatusType_Grid_TabRequirementStatusType_ItemItem

            <DataMember()> Public Property RequirementStatus As System.Double
            <DataMember()> Public Property RecordStatus As System.Double
            <DataMember()> Public Property CreatorUserCode As System.Double
            <DataMember()> Public Property CreationDate As System.DateTime
            <DataMember()> Public Property UpdateUserCode As System.Double
            <DataMember()> Public Property UpdateDate As System.DateTime
            <DataMember()> Public Property Description As System.String
            <DataMember()> Public Property ShortDescription As System.String

        End Class

        <Serializable()>
        <DataContract()>
        Public Class TabRequirementStatusTypeTranslator_Grid_TabRequirementStatusType_ItemItem

            <DataMember()> Public Property RequirementStatus As System.Double
            <DataMember()> Public Property LanguageId As System.Double
            <DataMember()> Public Property Description As System.String
            <DataMember()> Public Property ShortDescription As System.String

        End Class


#End Region

    End Class

End Namespace

