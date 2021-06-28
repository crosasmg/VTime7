﻿'---------------------------------------------------------------------------------------------------
' <generated>
'     This code was generated by Form Designer v7.3.24.1 at 2019-11-08 03:18:48 p. m. model release 1, Form Generator v1.0.37.9
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

    Public Class H5MantEstadoDelCAsoActions
        Inherits System.Web.UI.Page

#Region "Actions Methods"
  

        <WebMethod()>
        Public Shared Function TabUnderwritingCaseSType_GridTblDataLoad(filter As String, TransUnderwritingCaseSTypeLanguageId1 As String) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            Dim response As Object = New With {.UnderwritingCaseStatus = 0, .RecordStatus = 0, .CreatorUserCode = 0, .CreationDate = Date.MinValue, .UpdateUserCode = 0, .UpdateDate = Date.MinValue, .Description = String.Empty, .ShortDescription = String.Empty}
            Dim selectDataTableItem As DataTable
            Dim responseList As New List(Of Object)
            
            
            Try
                InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")

                With New DataManagerFactory("SELECT TABUNDERWRITINGCASESTYPE.UNDERWRITINGCASESTATUS, TABUNDERWRITINGCASESTYPE.RECORDSTATUS, TABUNDERWRITINGCASESTYPE.CREATORUSERCODE, TABUNDERWRITINGCASESTYPE.CREATIONDATE, TABUNDERWRITINGCASESTYPE.UPDATEUSERCODE, TABUNDERWRITINGCASESTYPE.UPDATEDATE, TRANSUNDERWRITINGCASESTYPE.DESCRIPTION, TRANSUNDERWRITINGCASESTYPE.SHORTDESCRIPTION FROM TABUNDERWRITINGCASESTYPE TABUNDERWRITINGCASESTYPE  LEFT JOIN TRANSUNDERWRITINGCASESTYPE TRANSUNDERWRITINGCASESTYPE ON TRANSUNDERWRITINGCASESTYPE.UNDERWRITINGCASESTATUS = TABUNDERWRITINGCASESTYPE.UNDERWRITINGCASESTATUS  AND TRANSUNDERWRITINGCASESTYPE.LANGUAGEID = @:LANGUAGEID1", "TabUnderwritingCaseSType", "Linked.Underwriting")
                    .AddParameter("LANGUAGEID1", DbType.AnsiString, 5, (TransUnderwritingCaseSTypeLanguageId1 = String.Empty), TransUnderwritingCaseSTypeLanguageId1)

                    selectDataTableItem = .QueryExecuteToTable(True)
                End With
                With selectDataTableItem
                    If Not IsNothing(.Rows) AndAlso .Rows.Count > 0 Then
                        For Each itemData As DataRow In .Rows
                            response = New With {.UnderwritingCaseStatus = itemData.NumericValue("UNDERWRITINGCASESTATUS"), .RecordStatus = itemData.NumericValue("RECORDSTATUS"), .CreatorUserCode = itemData.NumericValue("CREATORUSERCODE"), .CreationDate = itemData.DateTimeValue("CREATIONDATE"), .UpdateUserCode = itemData.NumericValue("UPDATEUSERCODE"), .UpdateDate = itemData.DateTimeValue("UPDATEDATE"), .Description = itemData.StringValue("DESCRIPTION"), .ShortDescription = itemData.StringValue("SHORTDESCRIPTION")}
                            
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
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantEstadoDelCAso", "TabUnderwritingCaseSType_GridTblDataLoad", String.Empty)
            End Try
            
            Return resultData
        End Function

        <WebMethod()>
        Public Shared Function TabUnderwritingCaseSType_Grid1InsertCommandActionTabUnderwritingCaseSType(UNDERWRITINGCASESTATUS1 As Decimal, RECORDSTATUS2 As Decimal, CREATORUSERCODE2 As Decimal, UPDATEUSERCODE4 As Decimal) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            
            Try
               InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")
                With New DataManagerFactory("INSERT INTO TabUnderwritingCaseSType (UNDERWRITINGCASESTATUS, RECORDSTATUS, CREATORUSERCODE, CREATIONDATE, UPDATEUSERCODE, UPDATEDATE) VALUES (@:UNDERWRITINGCASESTATUS1, @:RECORDSTATUS2, @:CREATORUSERCODE2, @:CREATIONDATE3, @:UPDATEUSERCODE4, @:UPDATEDATE5)", "TabUnderwritingCaseSType", "Linked.Underwriting")
                    .AddParameter("UNDERWRITINGCASESTATUS1", DbType.Decimal, 0, False, UNDERWRITINGCASESTATUS1)
                    .AddParameter("RECORDSTATUS2", DbType.Decimal, 0, False, RECORDSTATUS2)
                    .AddParameter("CREATORUSERCODE2", DbType.Decimal, 0, False, CREATORUSERCODE2)
                    .AddParameter("CREATIONDATE3", DbType.DateTime, 0, False, Date.Now)
                    .AddParameter("UPDATEUSERCODE4", DbType.Decimal, 0, False, UPDATEUSERCODE4)
                    .AddParameter("UPDATEDATE5", DbType.DateTime, 0, False, Date.Now)
 
                    .CommandExecute()
              End With

            Catch ex As Exception
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantEstadoDelCAso", "TabUnderwritingCaseSType_Grid1InsertCommandActionTabUnderwritingCaseSType", String.Empty)
            End Try
            
            Return resultData
        End Function
        <WebMethod()>
        Public Shared Function TabUnderwritingCaseSType_Grid3InsertCommandActionTransUnderwritingCaseSType(UNDERWRITINGCASESTATUS1 As Decimal, LANGUAGEID1 As Decimal, DESCRIPTION3 As String, SHORTDESCRIPTION4 As String, CREATORUSERCODE4 As Decimal, UPDATEUSERCODE6 As Decimal) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            
            Try
               InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")
                    With New DataManagerFactory("INSERT INTO TransUnderwritingCaseSType (UNDERWRITINGCASESTATUS, LANGUAGEID, DESCRIPTION, SHORTDESCRIPTION, CREATORUSERCODE, CREATIONDATE, UPDATEUSERCODE, UPDATEDATE) VALUES (@:UNDERWRITINGCASESTATUS1, @:LANGUAGEID1, @:DESCRIPTION3, @:SHORTDESCRIPTION4, @:CREATORUSERCODE4, @:CREATIONDATE5, @:UPDATEUSERCODE6, @:UPDATEDATE7)", "TransUnderwritingCaseSType", "Linked.Underwriting")
                    .AddParameter("UNDERWRITINGCASESTATUS1", DbType.Decimal, 0, False, UNDERWRITINGCASESTATUS1)
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
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantEstadoDelCAso", "TabUnderwritingCaseSType_Grid3InsertCommandActionTransUnderwritingCaseSType", String.Empty)
            End Try
            
            Return resultData
        End Function

        <WebMethod()>
        Public Shared Function TabUnderwritingCaseSType_Grid1UpdateCommandActionTabUnderwritingCaseSType(RECORDSTATUS1 As Decimal, UPDATEUSERCODE1 As Decimal, TabUnderwritingCaseSTypeUnderwritingCaseStatus3 As Decimal) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            
            Try
               InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")
                With New DataManagerFactory("UPDATE TabUnderwritingCaseSType SET RECORDSTATUS = @:RECORDSTATUS1, UPDATEUSERCODE = @:UPDATEUSERCODE1 WHERE TABUNDERWRITINGCASESTYPE.UNDERWRITINGCASESTATUS = @:UNDERWRITINGCASESTATUS3", "TabUnderwritingCaseSType", "Linked.Underwriting")
                    .AddParameter("RECORDSTATUS1", DbType.Decimal, 0, False, RECORDSTATUS1)
                    .AddParameter("UPDATEUSERCODE1", DbType.Decimal, 0, False, UPDATEUSERCODE1)
                    .AddParameter("UNDERWRITINGCASESTATUS3", DbType.Decimal, 0, False, TabUnderwritingCaseSTypeUnderwritingCaseStatus3)
 
                    .CommandExecute()
              End With

            Catch ex As Exception
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantEstadoDelCAso", "TabUnderwritingCaseSType_Grid1UpdateCommandActionTabUnderwritingCaseSType", String.Empty)
            End Try
            
            Return resultData
        End Function
        <WebMethod()>
        Public Shared Function TabUnderwritingCaseSType_Grid3SelectCommandActionTransUnderwritingCaseSType(TransUnderwritingCaseSTypeUnderwritingCaseStatus1 As Decimal, TransUnderwritingCaseSTypeLanguageId2 As Decimal) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            Dim response As Object = New With {.Result = 0}
            Dim responseList As New List(Of Object)
            
            Try
                InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")

                With New DataManagerFactory("Select COUNT(TRANSUNDERWRITINGCASESTYPE.UNDERWRITINGCASESTATUS) UNDERWRITINGCASESTATUS FROM TRANSUNDERWRITINGCASESTYPE TRANSUNDERWRITINGCASESTYPE  WHERE TRANSUNDERWRITINGCASESTYPE.UNDERWRITINGCASESTATUS = @:UNDERWRITINGCASESTATUS1 AND TRANSUNDERWRITINGCASESTYPE.LANGUAGEID = @:LANGUAGEID2", "TransUnderwritingCaseSType", "Linked.Underwriting")
                    .AddParameter("UNDERWRITINGCASESTATUS1", DbType.Decimal, 0, False, TransUnderwritingCaseSTypeUnderwritingCaseStatus1)
                    .AddParameter("LANGUAGEID2", DbType.Decimal, 0, False, TransUnderwritingCaseSTypeLanguageId2)

                    response.Result = .QueryExecuteScalarToInteger()
                End With
                With resultData
                    .Count = 1
                    .Data = response
                End With
            
            If responseList.Count <> 0 Then          
		    
            End If
                
            Catch ex As Exception            
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantEstadoDelCAso", "TabUnderwritingCaseSType_Grid3SelectCommandActionTransUnderwritingCaseSType", String.Empty)
            End Try
            
            Return resultData
        End Function
        <WebMethod()>
        Public Shared Function TabUnderwritingCaseSType_Grid5InsertCommandActionTransUnderwritingCaseSType(UNDERWRITINGCASESTATUS1 As Decimal, LANGUAGEID1 As Decimal, DESCRIPTION3 As String, SHORTDESCRIPTION4 As String, CREATORUSERCODE4 As Decimal, UPDATEUSERCODE6 As Decimal) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            
            Try
               InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")
                        With New DataManagerFactory("INSERT INTO TransUnderwritingCaseSType (UNDERWRITINGCASESTATUS, LANGUAGEID, DESCRIPTION, SHORTDESCRIPTION, CREATORUSERCODE, CREATIONDATE, UPDATEUSERCODE, UPDATEDATE) VALUES (@:UNDERWRITINGCASESTATUS1, @:LANGUAGEID1, @:DESCRIPTION3, @:SHORTDESCRIPTION4, @:CREATORUSERCODE4, @:CREATIONDATE5, @:UPDATEUSERCODE6, @:UPDATEDATE7)", "TransUnderwritingCaseSType", "Linked.Underwriting")
                    .AddParameter("UNDERWRITINGCASESTATUS1", DbType.Decimal, 0, False, UNDERWRITINGCASESTATUS1)
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
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantEstadoDelCAso", "TabUnderwritingCaseSType_Grid5InsertCommandActionTransUnderwritingCaseSType", String.Empty)
            End Try
            
            Return resultData
        End Function
        <WebMethod()>
        Public Shared Function TabUnderwritingCaseSType_Grid6UpdateCommandActionTransUnderwritingCaseSType(DESCRIPTION1 As String, SHORTDESCRIPTION2 As String, UPDATEUSERCODE2 As Decimal, TransUnderwritingCaseSTypeUnderwritingCaseStatus4 As Decimal, TransUnderwritingCaseSTypeLanguageId5 As Decimal) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            
            Try
               InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")
                        With New DataManagerFactory("UPDATE TransUnderwritingCaseSType SET DESCRIPTION = @:DESCRIPTION1, SHORTDESCRIPTION = @:SHORTDESCRIPTION2, UPDATEUSERCODE = @:UPDATEUSERCODE2 WHERE TRANSUNDERWRITINGCASESTYPE.UNDERWRITINGCASESTATUS = @:UNDERWRITINGCASESTATUS4 AND TRANSUNDERWRITINGCASESTYPE.LANGUAGEID = @:LANGUAGEID5", "TransUnderwritingCaseSType", "Linked.Underwriting")
                    .AddParameter("DESCRIPTION1", DbType.AnsiString, 0, (DESCRIPTION1 = String.Empty), DESCRIPTION1)
                    .AddParameter("SHORTDESCRIPTION2", DbType.AnsiString, 0, (SHORTDESCRIPTION2 = String.Empty), SHORTDESCRIPTION2)
                    .AddParameter("UPDATEUSERCODE2", DbType.Decimal, 0, False, UPDATEUSERCODE2)
                    .AddParameter("UNDERWRITINGCASESTATUS4", DbType.Decimal, 0, False, TransUnderwritingCaseSTypeUnderwritingCaseStatus4)
                    .AddParameter("LANGUAGEID5", DbType.Decimal, 0, False, TransUnderwritingCaseSTypeLanguageId5)
 
                    .CommandExecute()
              End With

            Catch ex As Exception
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantEstadoDelCAso", "TabUnderwritingCaseSType_Grid6UpdateCommandActionTransUnderwritingCaseSType", String.Empty)
            End Try
            
            Return resultData
        End Function

        <WebMethod()>
        Public Shared Function TabUnderwritingCaseSType_Grid1DeleteCommandActionTransUnderwritingCaseSType(TransUnderwritingCaseSTypeUnderwritingCaseStatus1 As Decimal) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            
            Try
               InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")
                With New DataManagerFactory("DELETE FROM TransUnderwritingCaseSType WHERE TRANSUNDERWRITINGCASESTYPE.UNDERWRITINGCASESTATUS = @:UNDERWRITINGCASESTATUS1", "TransUnderwritingCaseSType", "Linked.Underwriting")
                    .AddParameter("UNDERWRITINGCASESTATUS1", DbType.Decimal, 0, False, TransUnderwritingCaseSTypeUnderwritingCaseStatus1)
 
                    .CommandExecute()
              End With

            Catch ex As Exception
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantEstadoDelCAso", "TabUnderwritingCaseSType_Grid1DeleteCommandActionTransUnderwritingCaseSType", String.Empty)
            End Try
            
            Return resultData
        End Function
        <WebMethod()>
        Public Shared Function TabUnderwritingCaseSType_Grid3DeleteCommandActionTabUnderwritingCaseSType(TabUnderwritingCaseSTypeUnderwritingCaseStatus1 As Decimal) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            
            Try
               InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")
                    With New DataManagerFactory("DELETE FROM TabUnderwritingCaseSType WHERE TABUNDERWRITINGCASESTYPE.UNDERWRITINGCASESTATUS = @:UNDERWRITINGCASESTATUS1", "TabUnderwritingCaseSType", "Linked.Underwriting")
                    .AddParameter("UNDERWRITINGCASESTATUS1", DbType.Decimal, 0, False, TabUnderwritingCaseSTypeUnderwritingCaseStatus1)
 
                    .CommandExecute()
              End With

            Catch ex As Exception
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantEstadoDelCAso", "TabUnderwritingCaseSType_Grid3DeleteCommandActionTabUnderwritingCaseSType", String.Empty)
            End Try
            
            Return resultData
        End Function

        <WebMethod()>
        Public Shared Function TabUnderwritingCaseSType_Grid2SelectCommandActionTabUnderwritingCaseSType() As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            Dim response As Object = New With {.Result = 0}
            Dim responseList As New List(Of Object)
            
            Try
                InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")

                With New DataManagerFactory("Select MAX(TABUNDERWRITINGCASESTYPE.UNDERWRITINGCASESTATUS) UNDERWRITINGCASESTATUS FROM TABUNDERWRITINGCASESTYPE TABUNDERWRITINGCASESTYPE ", "TabUnderwritingCaseSType", "Linked.Underwriting")

                    response.Result = .QueryExecuteScalarToInteger()
                End With
                With resultData
                    .Count = 1
                    .Data = response
                End With
            
            If responseList.Count <> 0 Then          
		    
            End If
                
            Catch ex As Exception            
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantEstadoDelCAso", "TabUnderwritingCaseSType_Grid2SelectCommandActionTabUnderwritingCaseSType", String.Empty)
            End Try
            
            Return resultData
        End Function

        <WebMethod()>
        Public Shared Function TabUnderwritingCaseSTypeTranslator_GridTblDataLoad(filter As String) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            Dim response As Object = New With {.UnderwritingCaseStatus = 0, .LanguageId = 0, .Description = String.Empty, .ShortDescription = String.Empty}
            Dim selectDataTableItem As DataTable
            Dim responseList As New List(Of Object)
            
            
            Try
                InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")

                With New DataManagerFactory("SELECT TABUNDERWRITINGCASESTYPE.UNDERWRITINGCASESTATUS, TRANSUNDERWRITINGCASESTYPE.LANGUAGEID, TRANSUNDERWRITINGCASESTYPE.DESCRIPTION, TRANSUNDERWRITINGCASESTYPE.SHORTDESCRIPTION FROM TABUNDERWRITINGCASESTYPE TABUNDERWRITINGCASESTYPE  LEFT JOIN TRANSUNDERWRITINGCASESTYPE TRANSUNDERWRITINGCASESTYPE ON TRANSUNDERWRITINGCASESTYPE.UNDERWRITINGCASESTATUS = TABUNDERWRITINGCASESTYPE.UNDERWRITINGCASESTATUS ", "TabUnderwritingCaseSType", "Linked.Underwriting")

                    selectDataTableItem = .QueryExecuteToTable(True)
                End With
                With selectDataTableItem
                    If Not IsNothing(.Rows) AndAlso .Rows.Count > 0 Then
                        For Each itemData As DataRow In .Rows
                            response = New With {.UnderwritingCaseStatus = itemData.NumericValue("UNDERWRITINGCASESTATUS"), .LanguageId = itemData.NumericValue("LANGUAGEID"), .Description = itemData.StringValue("DESCRIPTION"), .ShortDescription = itemData.StringValue("SHORTDESCRIPTION")}
                            
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
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantEstadoDelCAso", "TabUnderwritingCaseSTypeTranslator_GridTblDataLoad", String.Empty)
            End Try
            
            Return resultData
        End Function

        <WebMethod()>
        Public Shared Function TabUnderwritingCaseSTypeTranslator_Grid1UpdateCommandActionTransUnderwritingCaseSType(DESCRIPTION1 As String, SHORTDESCRIPTION2 As String, UPDATEUSERCODE2 As Decimal, TransUnderwritingCaseSTypeUnderwritingCaseStatus4 As Decimal, TransUnderwritingCaseSTypeLanguageId5 As Decimal) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            
            Try
               InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")
                With New DataManagerFactory("UPDATE TransUnderwritingCaseSType SET DESCRIPTION = @:DESCRIPTION1, SHORTDESCRIPTION = @:SHORTDESCRIPTION2, UPDATEUSERCODE = @:UPDATEUSERCODE2 WHERE TRANSUNDERWRITINGCASESTYPE.UNDERWRITINGCASESTATUS = @:UNDERWRITINGCASESTATUS4 AND TRANSUNDERWRITINGCASESTYPE.LANGUAGEID = @:LANGUAGEID5", "TransUnderwritingCaseSType", "Linked.Underwriting")
                    .AddParameter("DESCRIPTION1", DbType.AnsiString, 0, (DESCRIPTION1 = String.Empty), DESCRIPTION1)
                    .AddParameter("SHORTDESCRIPTION2", DbType.AnsiString, 0, (SHORTDESCRIPTION2 = String.Empty), SHORTDESCRIPTION2)
                    .AddParameter("UPDATEUSERCODE2", DbType.Decimal, 0, False, UPDATEUSERCODE2)
                    .AddParameter("UNDERWRITINGCASESTATUS4", DbType.Decimal, 0, False, TransUnderwritingCaseSTypeUnderwritingCaseStatus4)
                    .AddParameter("LANGUAGEID5", DbType.Decimal, 0, False, TransUnderwritingCaseSTypeLanguageId5)
 
                    .CommandExecute()
              End With

            Catch ex As Exception
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantEstadoDelCAso", "TabUnderwritingCaseSTypeTranslator_Grid1UpdateCommandActionTransUnderwritingCaseSType", String.Empty)
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
                                                          "SELECT  TRIM(ETRANRECORDSTATUS.RECORDSTATUS) RECORDSTATUS, ETRANRECORDSTATUS.DESCRIPTION FROM ETRANRECORDSTATUS ETRANRECORDSTATUS  WHERE ETRANRECORDSTATUS.LANGUAGEID = {0}  AND NOT ETRANRECORDSTATUS.DESCRIPTION IS NULL ORDER BY DESCRIPTION", InMotionGIT.FASI.Support.Handlers.LanguageHandler.ContextLanguageId()), 
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
                result = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantEstadoDelCAso", "LookUpForRecordStatus", String.Empty)
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
                result = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantEstadoDelCAso", "LookUpForLanguageIdTranslator", String.Empty)
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
            <DataMember()> Public Property TabUnderwritingCaseSTypeCollectionUnderwritingCaseStatus As System.Double
            <DataMember()> Public Property TabUnderwritingCaseSTypeCollectionCreatorUserCode As System.Double
            <DataMember()> Public Property TabUnderwritingCaseSTypeCollectionCreationDate As System.DateTime
            <DataMember()> Public Property TabUnderwritingCaseSTypeCollectionUpdateUserCode As System.Double
            <DataMember()> Public Property TabUnderwritingCaseSTypeCollectionUpdateDate As System.DateTime
            <DataMember()> Public Property TabUnderwritingCaseSTypeCollectionDescription As System.String
            <DataMember()> Public Property TabUnderwritingCaseSTypeCollectionShortDescription As System.String
            <DataMember()> Public Property TabUnderwritingCaseSTypeCollectionRecordStatus As System.Double
            <DataMember()> Public Property TabUnderwritingCaseSType_Grid_TabUnderwritingCaseSType_Item As List(Of TabUnderwritingCaseSType_Grid_TabUnderwritingCaseSType_ItemItem)
            <DataMember()> Public Property TabUnderwritingCaseSTypeCollectionLanguageId As System.Double
            <DataMember()> Public Property TabUnderwritingCaseSTypeTranslator_Grid_TabUnderwritingCaseSType_Item As List(Of TabUnderwritingCaseSTypeTranslator_Grid_TabUnderwritingCaseSType_ItemItem)

        End Class

        <Serializable()>
        <DataContract()>
        Public Class TabUnderwritingCaseSType_Grid_TabUnderwritingCaseSType_ItemItem

            <DataMember()> Public Property UnderwritingCaseStatus As System.Double
            <DataMember()> Public Property CreatorUserCode As System.Double
            <DataMember()> Public Property CreationDate As System.DateTime
            <DataMember()> Public Property UpdateUserCode As System.Double
            <DataMember()> Public Property UpdateDate As System.DateTime
            <DataMember()> Public Property Description As System.String
            <DataMember()> Public Property ShortDescription As System.String
            <DataMember()> Public Property RecordStatus As System.Double

        End Class

        <Serializable()>
        <DataContract()>
        Public Class TabUnderwritingCaseSTypeTranslator_Grid_TabUnderwritingCaseSType_ItemItem

            <DataMember()> Public Property UnderwritingCaseStatus As System.Double
            <DataMember()> Public Property LanguageId As System.Double
            <DataMember()> Public Property Description As System.String
            <DataMember()> Public Property ShortDescription As System.String

        End Class


#End Region

    End Class

End Namespace

