﻿'---------------------------------------------------------------------------------------------------
' <generated>
'     This code was generated by Form Designer v7.3.24.1 at 2019-11-08 04:12:57 p. m. model release 1, Form Generator v1.0.37.9
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

    Public Class H5MantMovimientoCasoActions
        Inherits System.Web.UI.Page

#Region "Actions Methods"
  

        <WebMethod()>
        Public Shared Function TabManualOrAutomaticType_GridTblDataLoad(filter As String, TransManualOrAutomaticTypeLanguageId1 As String) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            Dim response As Object = New With {.ManualOrAutomatic = 0, .RecordStatus = 0, .CreatorUserCode = 0, .CreationDate = Date.MinValue, .UpdateUserCode = 0, .UpdateDate = Date.MinValue, .Description = String.Empty, .ShortDescription = String.Empty}
            Dim selectDataTableItem As DataTable
            Dim responseList As New List(Of Object)
            
            
            Try
                InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")

                With New DataManagerFactory("SELECT TABMANUALORAUTOMATICTYPE.MANUALORAUTOMATIC, TABMANUALORAUTOMATICTYPE.RECORDSTATUS, TABMANUALORAUTOMATICTYPE.CREATORUSERCODE, TABMANUALORAUTOMATICTYPE.CREATIONDATE, TABMANUALORAUTOMATICTYPE.UPDATEUSERCODE, TABMANUALORAUTOMATICTYPE.UPDATEDATE, TRANSMANUALORAUTOMATICTYPE.DESCRIPTION, TRANSMANUALORAUTOMATICTYPE.SHORTDESCRIPTION FROM TABMANUALORAUTOMATICTYPE TABMANUALORAUTOMATICTYPE  LEFT JOIN TRANSMANUALORAUTOMATICTYPE TRANSMANUALORAUTOMATICTYPE ON TRANSMANUALORAUTOMATICTYPE.MANUALORAUTOMATIC = TABMANUALORAUTOMATICTYPE.MANUALORAUTOMATIC  AND TRANSMANUALORAUTOMATICTYPE.LANGUAGEID = @:LANGUAGEID1", "TabManualOrAutomaticType", "Linked.Underwriting")
                    .AddParameter("LANGUAGEID1", DbType.AnsiString, 5, (TransManualOrAutomaticTypeLanguageId1 = String.Empty), TransManualOrAutomaticTypeLanguageId1)

                    selectDataTableItem = .QueryExecuteToTable(True)
                End With
                With selectDataTableItem
                    If Not IsNothing(.Rows) AndAlso .Rows.Count > 0 Then
                        For Each itemData As DataRow In .Rows
                            response = New With {.ManualOrAutomatic = itemData.NumericValue("MANUALORAUTOMATIC"), .RecordStatus = itemData.NumericValue("RECORDSTATUS"), .CreatorUserCode = itemData.NumericValue("CREATORUSERCODE"), .CreationDate = itemData.DateTimeValue("CREATIONDATE"), .UpdateUserCode = itemData.NumericValue("UPDATEUSERCODE"), .UpdateDate = itemData.DateTimeValue("UPDATEDATE"), .Description = itemData.StringValue("DESCRIPTION"), .ShortDescription = itemData.StringValue("SHORTDESCRIPTION")}
                            
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
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantMovimientoCaso", "TabManualOrAutomaticType_GridTblDataLoad", String.Empty)
            End Try
            
            Return resultData
        End Function

        <WebMethod()>
        Public Shared Function TabManualOrAutomaticType_Grid1InsertCommandActionTabManualOrAutomaticType(MANUALORAUTOMATIC1 As Decimal, RECORDSTATUS2 As Decimal, CREATORUSERCODE2 As Decimal, UPDATEUSERCODE4 As Decimal) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            
            Try
               InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")
                With New DataManagerFactory("INSERT INTO TabManualOrAutomaticType (MANUALORAUTOMATIC, RECORDSTATUS, CREATORUSERCODE, CREATIONDATE, UPDATEUSERCODE, UPDATEDATE) VALUES (@:MANUALORAUTOMATIC1, @:RECORDSTATUS2, @:CREATORUSERCODE2, @:CREATIONDATE3, @:UPDATEUSERCODE4, @:UPDATEDATE5)", "TabManualOrAutomaticType", "Linked.Underwriting")
                    .AddParameter("MANUALORAUTOMATIC1", DbType.Decimal, 0, False, MANUALORAUTOMATIC1)
                    .AddParameter("RECORDSTATUS2", DbType.Decimal, 0, False, RECORDSTATUS2)
                    .AddParameter("CREATORUSERCODE2", DbType.Decimal, 0, False, CREATORUSERCODE2)
                    .AddParameter("CREATIONDATE3", DbType.DateTime, 0, False, Date.Now)
                    .AddParameter("UPDATEUSERCODE4", DbType.Decimal, 0, False, UPDATEUSERCODE4)
                    .AddParameter("UPDATEDATE5", DbType.DateTime, 0, False, Date.Now)
 
                    .CommandExecute()
              End With

            Catch ex As Exception
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantMovimientoCaso", "TabManualOrAutomaticType_Grid1InsertCommandActionTabManualOrAutomaticType", String.Empty)
            End Try
            
            Return resultData
        End Function
        <WebMethod()>
        Public Shared Function TabManualOrAutomaticType_Grid3InsertCommandActionTransManualOrAutomaticType(MANUALORAUTOMATIC1 As Decimal, LANGUAGEID1 As Decimal, DESCRIPTION3 As String, SHORTDESCRIPTION4 As String, CREATORUSERCODE4 As Decimal, UPDATEUSERCODE6 As Decimal) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            
            Try
               InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")
                    With New DataManagerFactory("INSERT INTO TransManualOrAutomaticType (MANUALORAUTOMATIC, LANGUAGEID, DESCRIPTION, SHORTDESCRIPTION, CREATORUSERCODE, CREATIONDATE, UPDATEUSERCODE, UPDATEDATE) VALUES (@:MANUALORAUTOMATIC1, @:LANGUAGEID1, @:DESCRIPTION3, @:SHORTDESCRIPTION4, @:CREATORUSERCODE4, @:CREATIONDATE5, @:UPDATEUSERCODE6, @:UPDATEDATE7)", "TransManualOrAutomaticType", "Linked.Underwriting")
                    .AddParameter("MANUALORAUTOMATIC1", DbType.Decimal, 0, False, MANUALORAUTOMATIC1)
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
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantMovimientoCaso", "TabManualOrAutomaticType_Grid3InsertCommandActionTransManualOrAutomaticType", String.Empty)
            End Try
            
            Return resultData
        End Function

        <WebMethod()>
        Public Shared Function TabManualOrAutomaticType_Grid1UpdateCommandActionTabManualOrAutomaticType(RECORDSTATUS1 As Decimal, UPDATEUSERCODE1 As Decimal, TabManualOrAutomaticTypeManualOrAutomatic3 As Decimal) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            
            Try
               InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")
                With New DataManagerFactory("UPDATE TabManualOrAutomaticType SET RECORDSTATUS = @:RECORDSTATUS1, UPDATEUSERCODE = @:UPDATEUSERCODE1 WHERE TABMANUALORAUTOMATICTYPE.MANUALORAUTOMATIC = @:MANUALORAUTOMATIC3", "TabManualOrAutomaticType", "Linked.Underwriting")
                    .AddParameter("RECORDSTATUS1", DbType.Decimal, 0, False, RECORDSTATUS1)
                    .AddParameter("UPDATEUSERCODE1", DbType.Decimal, 0, False, UPDATEUSERCODE1)
                    .AddParameter("MANUALORAUTOMATIC3", DbType.Decimal, 0, False, TabManualOrAutomaticTypeManualOrAutomatic3)
 
                    .CommandExecute()
              End With

            Catch ex As Exception
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantMovimientoCaso", "TabManualOrAutomaticType_Grid1UpdateCommandActionTabManualOrAutomaticType", String.Empty)
            End Try
            
            Return resultData
        End Function
        <WebMethod()>
        Public Shared Function TabManualOrAutomaticType_Grid3SelectCommandActionTransManualOrAutomaticType(TransManualOrAutomaticTypeManualOrAutomatic1 As Decimal, TransManualOrAutomaticTypeLanguageId2 As Decimal) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            Dim response As Object = New With {.Result = 0}
            Dim responseList As New List(Of Object)
            
            Try
                InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")

                With New DataManagerFactory("Select COUNT(TRANSMANUALORAUTOMATICTYPE.MANUALORAUTOMATIC) MANUALORAUTOMATIC FROM TRANSMANUALORAUTOMATICTYPE TRANSMANUALORAUTOMATICTYPE  WHERE TRANSMANUALORAUTOMATICTYPE.MANUALORAUTOMATIC = @:MANUALORAUTOMATIC1 AND TRANSMANUALORAUTOMATICTYPE.LANGUAGEID = @:LANGUAGEID2", "TransManualOrAutomaticType", "Linked.Underwriting")
                    .AddParameter("MANUALORAUTOMATIC1", DbType.Decimal, 0, False, TransManualOrAutomaticTypeManualOrAutomatic1)
                    .AddParameter("LANGUAGEID2", DbType.Decimal, 0, False, TransManualOrAutomaticTypeLanguageId2)

                    response.Result = .QueryExecuteScalarToInteger()
                End With
                With resultData
                    .Count = 1
                    .Data = response
                End With
            
            If responseList.Count <> 0 Then          
		    
            End If
                
            Catch ex As Exception            
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantMovimientoCaso", "TabManualOrAutomaticType_Grid3SelectCommandActionTransManualOrAutomaticType", String.Empty)
            End Try
            
            Return resultData
        End Function
        <WebMethod()>
        Public Shared Function TabManualOrAutomaticType_Grid5InsertCommandActionTransManualOrAutomaticType(MANUALORAUTOMATIC1 As Decimal, LANGUAGEID1 As Decimal, DESCRIPTION3 As String, SHORTDESCRIPTION4 As String, CREATORUSERCODE4 As Decimal, UPDATEUSERCODE6 As Decimal) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            
            Try
               InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")
                        With New DataManagerFactory("INSERT INTO TransManualOrAutomaticType (MANUALORAUTOMATIC, LANGUAGEID, DESCRIPTION, SHORTDESCRIPTION, CREATORUSERCODE, CREATIONDATE, UPDATEUSERCODE, UPDATEDATE) VALUES (@:MANUALORAUTOMATIC1, @:LANGUAGEID1, @:DESCRIPTION3, @:SHORTDESCRIPTION4, @:CREATORUSERCODE4, @:CREATIONDATE5, @:UPDATEUSERCODE6, @:UPDATEDATE7)", "TransManualOrAutomaticType", "Linked.Underwriting")
                    .AddParameter("MANUALORAUTOMATIC1", DbType.Decimal, 0, False, MANUALORAUTOMATIC1)
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
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantMovimientoCaso", "TabManualOrAutomaticType_Grid5InsertCommandActionTransManualOrAutomaticType", String.Empty)
            End Try
            
            Return resultData
        End Function
        <WebMethod()>
        Public Shared Function TabManualOrAutomaticType_Grid6UpdateCommandActionTransManualOrAutomaticType(DESCRIPTION1 As String, SHORTDESCRIPTION2 As String, UPDATEUSERCODE2 As Decimal, TransManualOrAutomaticTypeManualOrAutomatic4 As Decimal, TransManualOrAutomaticTypeLanguageId5 As Decimal) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            
            Try
               InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")
                        With New DataManagerFactory("UPDATE TransManualOrAutomaticType SET DESCRIPTION = @:DESCRIPTION1, SHORTDESCRIPTION = @:SHORTDESCRIPTION2, UPDATEUSERCODE = @:UPDATEUSERCODE2 WHERE TRANSMANUALORAUTOMATICTYPE.MANUALORAUTOMATIC = @:MANUALORAUTOMATIC4 AND TRANSMANUALORAUTOMATICTYPE.LANGUAGEID = @:LANGUAGEID5", "TransManualOrAutomaticType", "Linked.Underwriting")
                    .AddParameter("DESCRIPTION1", DbType.AnsiString, 0, (DESCRIPTION1 = String.Empty), DESCRIPTION1)
                    .AddParameter("SHORTDESCRIPTION2", DbType.AnsiString, 0, (SHORTDESCRIPTION2 = String.Empty), SHORTDESCRIPTION2)
                    .AddParameter("UPDATEUSERCODE2", DbType.Decimal, 0, False, UPDATEUSERCODE2)
                    .AddParameter("MANUALORAUTOMATIC4", DbType.Decimal, 0, False, TransManualOrAutomaticTypeManualOrAutomatic4)
                    .AddParameter("LANGUAGEID5", DbType.Decimal, 0, False, TransManualOrAutomaticTypeLanguageId5)
 
                    .CommandExecute()
              End With

            Catch ex As Exception
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantMovimientoCaso", "TabManualOrAutomaticType_Grid6UpdateCommandActionTransManualOrAutomaticType", String.Empty)
            End Try
            
            Return resultData
        End Function

        <WebMethod()>
        Public Shared Function TabManualOrAutomaticType_Grid1DeleteCommandActionTransManualOrAutomaticType(TransManualOrAutomaticTypeManualOrAutomatic1 As Decimal) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            
            Try
               InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")
                With New DataManagerFactory("DELETE FROM TransManualOrAutomaticType WHERE TRANSMANUALORAUTOMATICTYPE.MANUALORAUTOMATIC = @:MANUALORAUTOMATIC1", "TransManualOrAutomaticType", "Linked.Underwriting")
                    .AddParameter("MANUALORAUTOMATIC1", DbType.Decimal, 0, False, TransManualOrAutomaticTypeManualOrAutomatic1)
 
                    .CommandExecute()
              End With

            Catch ex As Exception
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantMovimientoCaso", "TabManualOrAutomaticType_Grid1DeleteCommandActionTransManualOrAutomaticType", String.Empty)
            End Try
            
            Return resultData
        End Function
        <WebMethod()>
        Public Shared Function TabManualOrAutomaticType_Grid3DeleteCommandActionTabManualOrAutomaticType(TabManualOrAutomaticTypeManualOrAutomatic1 As Decimal) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            
            Try
               InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")
                    With New DataManagerFactory("DELETE FROM TabManualOrAutomaticType WHERE TABMANUALORAUTOMATICTYPE.MANUALORAUTOMATIC = @:MANUALORAUTOMATIC1", "TabManualOrAutomaticType", "Linked.Underwriting")
                    .AddParameter("MANUALORAUTOMATIC1", DbType.Decimal, 0, False, TabManualOrAutomaticTypeManualOrAutomatic1)
 
                    .CommandExecute()
              End With

            Catch ex As Exception
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantMovimientoCaso", "TabManualOrAutomaticType_Grid3DeleteCommandActionTabManualOrAutomaticType", String.Empty)
            End Try
            
            Return resultData
        End Function

        <WebMethod()>
        Public Shared Function TabManualOrAutomaticType_Grid2SelectCommandActionTabManualOrAutomaticType() As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            Dim response As Object = New With {.Result = 0}
            Dim responseList As New List(Of Object)
            
            Try
                InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")

                With New DataManagerFactory("Select MAX(TABMANUALORAUTOMATICTYPE.MANUALORAUTOMATIC) MANUALORAUTOMATIC FROM TABMANUALORAUTOMATICTYPE TABMANUALORAUTOMATICTYPE ", "TabManualOrAutomaticType", "Linked.Underwriting")

                    response.Result = .QueryExecuteScalarToInteger()
                End With
                With resultData
                    .Count = 1
                    .Data = response
                End With
            
            If responseList.Count <> 0 Then          
		    
            End If
                
            Catch ex As Exception            
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantMovimientoCaso", "TabManualOrAutomaticType_Grid2SelectCommandActionTabManualOrAutomaticType", String.Empty)
            End Try
            
            Return resultData
        End Function

        <WebMethod()>
        Public Shared Function TabManualOrAutomaticTypeTranslator_GridTblDataLoad(filter As String) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            Dim response As Object = New With {.ManualOrAutomatic = 0, .LanguageId = 0, .Description = String.Empty, .ShortDescription = String.Empty}
            Dim selectDataTableItem As DataTable
            Dim responseList As New List(Of Object)
            
            
            Try
                InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")

                With New DataManagerFactory("SELECT TABMANUALORAUTOMATICTYPE.MANUALORAUTOMATIC, TRANSMANUALORAUTOMATICTYPE.LANGUAGEID, TRANSMANUALORAUTOMATICTYPE.DESCRIPTION, TRANSMANUALORAUTOMATICTYPE.SHORTDESCRIPTION FROM TABMANUALORAUTOMATICTYPE TABMANUALORAUTOMATICTYPE  LEFT JOIN TRANSMANUALORAUTOMATICTYPE TRANSMANUALORAUTOMATICTYPE ON TRANSMANUALORAUTOMATICTYPE.MANUALORAUTOMATIC = TABMANUALORAUTOMATICTYPE.MANUALORAUTOMATIC ", "TabManualOrAutomaticType", "Linked.Underwriting")

                    selectDataTableItem = .QueryExecuteToTable(True)
                End With
                With selectDataTableItem
                    If Not IsNothing(.Rows) AndAlso .Rows.Count > 0 Then
                        For Each itemData As DataRow In .Rows
                            response = New With {.ManualOrAutomatic = itemData.NumericValue("MANUALORAUTOMATIC"), .LanguageId = itemData.NumericValue("LANGUAGEID"), .Description = itemData.StringValue("DESCRIPTION"), .ShortDescription = itemData.StringValue("SHORTDESCRIPTION")}
                            
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
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantMovimientoCaso", "TabManualOrAutomaticTypeTranslator_GridTblDataLoad", String.Empty)
            End Try
            
            Return resultData
        End Function

        <WebMethod()>
        Public Shared Function TabManualOrAutomaticTypeTranslator_Grid1UpdateCommandActionTransManualOrAutomaticType(DESCRIPTION1 As String, SHORTDESCRIPTION2 As String, UPDATEUSERCODE2 As Decimal, TransManualOrAutomaticTypeManualOrAutomatic4 As Decimal, TransManualOrAutomaticTypeLanguageId5 As Decimal) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            
            Try
               InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")
                With New DataManagerFactory("UPDATE TransManualOrAutomaticType SET DESCRIPTION = @:DESCRIPTION1, SHORTDESCRIPTION = @:SHORTDESCRIPTION2, UPDATEUSERCODE = @:UPDATEUSERCODE2 WHERE TRANSMANUALORAUTOMATICTYPE.MANUALORAUTOMATIC = @:MANUALORAUTOMATIC4 AND TRANSMANUALORAUTOMATICTYPE.LANGUAGEID = @:LANGUAGEID5", "TransManualOrAutomaticType", "Linked.Underwriting")
                    .AddParameter("DESCRIPTION1", DbType.AnsiString, 0, (DESCRIPTION1 = String.Empty), DESCRIPTION1)
                    .AddParameter("SHORTDESCRIPTION2", DbType.AnsiString, 0, (SHORTDESCRIPTION2 = String.Empty), SHORTDESCRIPTION2)
                    .AddParameter("UPDATEUSERCODE2", DbType.Decimal, 0, False, UPDATEUSERCODE2)
                    .AddParameter("MANUALORAUTOMATIC4", DbType.Decimal, 0, False, TransManualOrAutomaticTypeManualOrAutomatic4)
                    .AddParameter("LANGUAGEID5", DbType.Decimal, 0, False, TransManualOrAutomaticTypeLanguageId5)
 
                    .CommandExecute()
              End With

            Catch ex As Exception
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantMovimientoCaso", "TabManualOrAutomaticTypeTranslator_Grid1UpdateCommandActionTransManualOrAutomaticType", String.Empty)
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
                result = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantMovimientoCaso", "LookUpForRecordStatus", String.Empty)
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
                result = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantMovimientoCaso", "LookUpForLanguageIdTranslator", String.Empty)
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
            <DataMember()> Public Property TabManualOrAutomaticTypeCollectionManualOrAutomatic As System.Double
            <DataMember()> Public Property TabManualOrAutomaticTypeCollectionRecordStatus As System.Double
            <DataMember()> Public Property TabManualOrAutomaticTypeCollectionCreatorUserCode As System.Double
            <DataMember()> Public Property TabManualOrAutomaticTypeCollectionCreationDate As System.DateTime
            <DataMember()> Public Property TabManualOrAutomaticTypeCollectionUpdateUserCode As System.Double
            <DataMember()> Public Property TabManualOrAutomaticTypeCollectionUpdateDate As System.DateTime
            <DataMember()> Public Property TabManualOrAutomaticTypeCollectionDescription As System.String
            <DataMember()> Public Property TabManualOrAutomaticTypeCollectionShortDescription As System.String
            <DataMember()> Public Property TabManualOrAutomaticType_Grid_TabManualOrAutomaticType_Item As List(Of TabManualOrAutomaticType_Grid_TabManualOrAutomaticType_ItemItem)
            <DataMember()> Public Property TabManualOrAutomaticTypeCollectionLanguageId As System.Double
            <DataMember()> Public Property TabManualOrAutomaticTypeTranslator_Grid_TabManualOrAutomaticType_Item As List(Of TabManualOrAutomaticTypeTranslator_Grid_TabManualOrAutomaticType_ItemItem)

        End Class

        <Serializable()>
        <DataContract()>
        Public Class TabManualOrAutomaticType_Grid_TabManualOrAutomaticType_ItemItem

            <DataMember()> Public Property ManualOrAutomatic As System.Double
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
        Public Class TabManualOrAutomaticTypeTranslator_Grid_TabManualOrAutomaticType_ItemItem

            <DataMember()> Public Property ManualOrAutomatic As System.Double
            <DataMember()> Public Property LanguageId As System.Double
            <DataMember()> Public Property Description As System.String
            <DataMember()> Public Property ShortDescription As System.String

        End Class


#End Region

    End Class

End Namespace

