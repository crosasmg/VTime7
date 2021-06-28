﻿'---------------------------------------------------------------------------------------------------
' <generated>
'     This code was generated by Form Designer v7.3.24.1 at 2019-11-08 04:34:09 p. m. model release 1, Form Generator v1.0.37.9
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

    Public Class H5MantNivelEnfermedadActions
        Inherits System.Web.UI.Page

#Region "Actions Methods"
  

        <WebMethod()>
        Public Shared Function TabDegree_GridTblDataLoad(filter As String, TransDegreeLanguageId1 As String) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            Dim response As Object = New With {.DegreeId = 0, .RecordStatus = 0, .CreatorUserCode = 0, .CreationDate = Date.MinValue, .UpdateUserCode = 0, .UpdateDate = Date.MinValue, .Description = String.Empty, .ShortDescription = String.Empty}
            Dim selectDataTableItem As DataTable
            Dim responseList As New List(Of Object)
            
            
            Try
                InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")

                With New DataManagerFactory("SELECT TABDEGREE.DEGREEID, TABDEGREE.RECORDSTATUS, TABDEGREE.CREATORUSERCODE, TABDEGREE.CREATIONDATE, TABDEGREE.UPDATEUSERCODE, TABDEGREE.UPDATEDATE, TRANSDEGREE.DESCRIPTION, TRANSDEGREE.SHORTDESCRIPTION FROM TABDEGREE TABDEGREE  LEFT JOIN TRANSDEGREE TRANSDEGREE ON TRANSDEGREE.DEGREEID = TABDEGREE.DEGREEID  AND TRANSDEGREE.LANGUAGEID = @:LANGUAGEID1", "TabDegree", "Linked.Underwriting")
                    .AddParameter("LANGUAGEID1", DbType.AnsiString, 5, (TransDegreeLanguageId1 = String.Empty), TransDegreeLanguageId1)

                    selectDataTableItem = .QueryExecuteToTable(True)
                End With
                With selectDataTableItem
                    If Not IsNothing(.Rows) AndAlso .Rows.Count > 0 Then
                        For Each itemData As DataRow In .Rows
                            response = New With {.DegreeId = itemData.NumericValue("DEGREEID"), .RecordStatus = itemData.NumericValue("RECORDSTATUS"), .CreatorUserCode = itemData.NumericValue("CREATORUSERCODE"), .CreationDate = itemData.DateTimeValue("CREATIONDATE"), .UpdateUserCode = itemData.NumericValue("UPDATEUSERCODE"), .UpdateDate = itemData.DateTimeValue("UPDATEDATE"), .Description = itemData.StringValue("DESCRIPTION"), .ShortDescription = itemData.StringValue("SHORTDESCRIPTION")}
                            
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
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantNivelEnfermedad", "TabDegree_GridTblDataLoad", String.Empty)
            End Try
            
            Return resultData
        End Function

        <WebMethod()>
        Public Shared Function TabDegree_Grid1InsertCommandActionTabDegree(DEGREEID1 As Decimal, RECORDSTATUS2 As Decimal, CREATORUSERCODE2 As Decimal, UPDATEUSERCODE4 As Decimal) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            
            Try
               InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")
                With New DataManagerFactory("INSERT INTO TabDegree (DEGREEID, RECORDSTATUS, CREATORUSERCODE, CREATIONDATE, UPDATEUSERCODE, UPDATEDATE) VALUES (@:DEGREEID1, @:RECORDSTATUS2, @:CREATORUSERCODE2, @:CREATIONDATE3, @:UPDATEUSERCODE4, @:UPDATEDATE5)", "TabDegree", "Linked.Underwriting")
                    .AddParameter("DEGREEID1", DbType.Decimal, 0, False, DEGREEID1)
                    .AddParameter("RECORDSTATUS2", DbType.Decimal, 0, False, RECORDSTATUS2)
                    .AddParameter("CREATORUSERCODE2", DbType.Decimal, 0, False, CREATORUSERCODE2)
                    .AddParameter("CREATIONDATE3", DbType.DateTime, 0, False, Date.Now)
                    .AddParameter("UPDATEUSERCODE4", DbType.Decimal, 0, False, UPDATEUSERCODE4)
                    .AddParameter("UPDATEDATE5", DbType.DateTime, 0, False, Date.Now)
 
                    .CommandExecute()
              End With

            Catch ex As Exception
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantNivelEnfermedad", "TabDegree_Grid1InsertCommandActionTabDegree", String.Empty)
            End Try
            
            Return resultData
        End Function
        <WebMethod()>
        Public Shared Function TabDegree_Grid3InsertCommandActionTransDegree(DEGREEID1 As Decimal, LANGUAGEID1 As Decimal, DESCRIPTION3 As String, SHORTDESCRIPTION4 As String, CREATORUSERCODE4 As Decimal, UPDATEUSERCODE6 As Decimal) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            
            Try
               InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")
                    With New DataManagerFactory("INSERT INTO TransDegree (DEGREEID, LANGUAGEID, DESCRIPTION, SHORTDESCRIPTION, CREATORUSERCODE, CREATIONDATE, UPDATEUSERCODE, UPDATEDATE) VALUES (@:DEGREEID1, @:LANGUAGEID1, @:DESCRIPTION3, @:SHORTDESCRIPTION4, @:CREATORUSERCODE4, @:CREATIONDATE5, @:UPDATEUSERCODE6, @:UPDATEDATE7)", "TransDegree", "Linked.Underwriting")
                    .AddParameter("DEGREEID1", DbType.Decimal, 0, False, DEGREEID1)
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
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantNivelEnfermedad", "TabDegree_Grid3InsertCommandActionTransDegree", String.Empty)
            End Try
            
            Return resultData
        End Function

        <WebMethod()>
        Public Shared Function TabDegree_Grid1UpdateCommandActionTabDegree(RECORDSTATUS1 As Decimal, UPDATEUSERCODE1 As Decimal, TabDegreeDegreeId3 As Decimal) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            
            Try
               InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")
                With New DataManagerFactory("UPDATE TabDegree SET RECORDSTATUS = @:RECORDSTATUS1, UPDATEUSERCODE = @:UPDATEUSERCODE1 WHERE TABDEGREE.DEGREEID = @:DEGREEID3", "TabDegree", "Linked.Underwriting")
                    .AddParameter("RECORDSTATUS1", DbType.Decimal, 0, False, RECORDSTATUS1)
                    .AddParameter("UPDATEUSERCODE1", DbType.Decimal, 0, False, UPDATEUSERCODE1)
                    .AddParameter("DEGREEID3", DbType.Decimal, 0, False, TabDegreeDegreeId3)
 
                    .CommandExecute()
              End With

            Catch ex As Exception
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantNivelEnfermedad", "TabDegree_Grid1UpdateCommandActionTabDegree", String.Empty)
            End Try
            
            Return resultData
        End Function
        <WebMethod()>
        Public Shared Function TabDegree_Grid3SelectCommandActionTransDegree(TransDegreeDegreeId1 As Decimal, TransDegreeLanguageId2 As Decimal) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            Dim response As Object = New With {.Result = 0}
            Dim responseList As New List(Of Object)
            
            Try
                InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")

                With New DataManagerFactory("Select COUNT(TRANSDEGREE.DEGREEID) DEGREEID FROM TRANSDEGREE TRANSDEGREE  WHERE TRANSDEGREE.DEGREEID = @:DEGREEID1 AND TRANSDEGREE.LANGUAGEID = @:LANGUAGEID2", "TransDegree", "Linked.Underwriting")
                    .AddParameter("DEGREEID1", DbType.Decimal, 0, False, TransDegreeDegreeId1)
                    .AddParameter("LANGUAGEID2", DbType.Decimal, 0, False, TransDegreeLanguageId2)

                    response.Result = .QueryExecuteScalarToInteger()
                End With
                With resultData
                    .Count = 1
                    .Data = response
                End With
            
            If responseList.Count <> 0 Then          
		    
            End If
                
            Catch ex As Exception            
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantNivelEnfermedad", "TabDegree_Grid3SelectCommandActionTransDegree", String.Empty)
            End Try
            
            Return resultData
        End Function
        <WebMethod()>
        Public Shared Function TabDegree_Grid5InsertCommandActionTransDegree(DEGREEID1 As Decimal, LANGUAGEID1 As Decimal, DESCRIPTION3 As String, SHORTDESCRIPTION4 As String, CREATORUSERCODE4 As Decimal, UPDATEUSERCODE6 As Decimal) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            
            Try
               InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")
                        With New DataManagerFactory("INSERT INTO TransDegree (DEGREEID, LANGUAGEID, DESCRIPTION, SHORTDESCRIPTION, CREATORUSERCODE, CREATIONDATE, UPDATEUSERCODE, UPDATEDATE) VALUES (@:DEGREEID1, @:LANGUAGEID1, @:DESCRIPTION3, @:SHORTDESCRIPTION4, @:CREATORUSERCODE4, @:CREATIONDATE5, @:UPDATEUSERCODE6, @:UPDATEDATE7)", "TransDegree", "Linked.Underwriting")
                    .AddParameter("DEGREEID1", DbType.Decimal, 0, False, DEGREEID1)
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
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantNivelEnfermedad", "TabDegree_Grid5InsertCommandActionTransDegree", String.Empty)
            End Try
            
            Return resultData
        End Function
        <WebMethod()>
        Public Shared Function TabDegree_Grid6UpdateCommandActionTransDegree(DESCRIPTION1 As String, SHORTDESCRIPTION2 As String, UPDATEUSERCODE2 As Decimal, TransDegreeDegreeId4 As Decimal, TransDegreeLanguageId5 As Decimal) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            
            Try
               InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")
                        With New DataManagerFactory("UPDATE TransDegree SET DESCRIPTION = @:DESCRIPTION1, SHORTDESCRIPTION = @:SHORTDESCRIPTION2, UPDATEUSERCODE = @:UPDATEUSERCODE2 WHERE TRANSDEGREE.DEGREEID = @:DEGREEID4 AND TRANSDEGREE.LANGUAGEID = @:LANGUAGEID5", "TransDegree", "Linked.Underwriting")
                    .AddParameter("DESCRIPTION1", DbType.AnsiString, 0, (DESCRIPTION1 = String.Empty), DESCRIPTION1)
                    .AddParameter("SHORTDESCRIPTION2", DbType.AnsiString, 0, (SHORTDESCRIPTION2 = String.Empty), SHORTDESCRIPTION2)
                    .AddParameter("UPDATEUSERCODE2", DbType.Decimal, 0, False, UPDATEUSERCODE2)
                    .AddParameter("DEGREEID4", DbType.Decimal, 0, False, TransDegreeDegreeId4)
                    .AddParameter("LANGUAGEID5", DbType.Decimal, 0, False, TransDegreeLanguageId5)
 
                    .CommandExecute()
              End With

            Catch ex As Exception
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantNivelEnfermedad", "TabDegree_Grid6UpdateCommandActionTransDegree", String.Empty)
            End Try
            
            Return resultData
        End Function

        <WebMethod()>
        Public Shared Function TabDegree_Grid1DeleteCommandActionTransDegree(TransDegreeDegreeId1 As Decimal) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            
            Try
               InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")
                With New DataManagerFactory("DELETE FROM TransDegree WHERE TRANSDEGREE.DEGREEID = @:DEGREEID1", "TransDegree", "Linked.Underwriting")
                    .AddParameter("DEGREEID1", DbType.Decimal, 0, False, TransDegreeDegreeId1)
 
                    .CommandExecute()
              End With

            Catch ex As Exception
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantNivelEnfermedad", "TabDegree_Grid1DeleteCommandActionTransDegree", String.Empty)
            End Try
            
            Return resultData
        End Function
        <WebMethod()>
        Public Shared Function TabDegree_Grid3DeleteCommandActionTabDegree(TabDegreeDegreeId1 As Decimal) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            
            Try
               InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")
                    With New DataManagerFactory("DELETE FROM TabDegree WHERE TABDEGREE.DEGREEID = @:DEGREEID1", "TabDegree", "Linked.Underwriting")
                    .AddParameter("DEGREEID1", DbType.Decimal, 0, False, TabDegreeDegreeId1)
 
                    .CommandExecute()
              End With

            Catch ex As Exception
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantNivelEnfermedad", "TabDegree_Grid3DeleteCommandActionTabDegree", String.Empty)
            End Try
            
            Return resultData
        End Function

        <WebMethod()>
        Public Shared Function TabDegree_Grid2SelectCommandActionTabDegree() As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            Dim response As Object = New With {.Result = 0}
            Dim responseList As New List(Of Object)
            
            Try
                InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")

                With New DataManagerFactory("Select MAX(TABDEGREE.DEGREEID) DEGREEID FROM TABDEGREE TABDEGREE ", "TabDegree", "Linked.Underwriting")

                    response.Result = .QueryExecuteScalarToInteger()
                End With
                With resultData
                    .Count = 1
                    .Data = response
                End With
            
            If responseList.Count <> 0 Then          
		    
            End If
                
            Catch ex As Exception            
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantNivelEnfermedad", "TabDegree_Grid2SelectCommandActionTabDegree", String.Empty)
            End Try
            
            Return resultData
        End Function

        <WebMethod()>
        Public Shared Function TabDegreeTranslator_GridTblDataLoad(filter As String) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            Dim response As Object = New With {.DegreeId = 0, .LanguageId = 0, .Description = String.Empty, .ShortDescription = String.Empty}
            Dim selectDataTableItem As DataTable
            Dim responseList As New List(Of Object)
            
            
            Try
                InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")

                With New DataManagerFactory("SELECT TABDEGREE.DEGREEID, TRANSDEGREE.LANGUAGEID, TRANSDEGREE.DESCRIPTION, TRANSDEGREE.SHORTDESCRIPTION FROM TABDEGREE TABDEGREE  LEFT JOIN TRANSDEGREE TRANSDEGREE ON TRANSDEGREE.DEGREEID = TABDEGREE.DEGREEID ", "TabDegree", "Linked.Underwriting")

                    selectDataTableItem = .QueryExecuteToTable(True)
                End With
                With selectDataTableItem
                    If Not IsNothing(.Rows) AndAlso .Rows.Count > 0 Then
                        For Each itemData As DataRow In .Rows
                            response = New With {.DegreeId = itemData.NumericValue("DEGREEID"), .LanguageId = itemData.NumericValue("LANGUAGEID"), .Description = itemData.StringValue("DESCRIPTION"), .ShortDescription = itemData.StringValue("SHORTDESCRIPTION")}
                            
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
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantNivelEnfermedad", "TabDegreeTranslator_GridTblDataLoad", String.Empty)
            End Try
            
            Return resultData
        End Function

        <WebMethod()>
        Public Shared Function TabDegreeTranslator_Grid1UpdateCommandActionTransDegree(DESCRIPTION1 As String, SHORTDESCRIPTION2 As String, UPDATEUSERCODE2 As Decimal, TransDegreeDegreeId4 As Decimal, TransDegreeLanguageId5 As Decimal) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            
            Try
               InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("EASE1,Suscriptor")
                With New DataManagerFactory("UPDATE TransDegree SET DESCRIPTION = @:DESCRIPTION1, SHORTDESCRIPTION = @:SHORTDESCRIPTION2, UPDATEUSERCODE = @:UPDATEUSERCODE2 WHERE TRANSDEGREE.DEGREEID = @:DEGREEID4 AND TRANSDEGREE.LANGUAGEID = @:LANGUAGEID5", "TransDegree", "Linked.Underwriting")
                    .AddParameter("DESCRIPTION1", DbType.AnsiString, 0, (DESCRIPTION1 = String.Empty), DESCRIPTION1)
                    .AddParameter("SHORTDESCRIPTION2", DbType.AnsiString, 0, (SHORTDESCRIPTION2 = String.Empty), SHORTDESCRIPTION2)
                    .AddParameter("UPDATEUSERCODE2", DbType.Decimal, 0, False, UPDATEUSERCODE2)
                    .AddParameter("DEGREEID4", DbType.Decimal, 0, False, TransDegreeDegreeId4)
                    .AddParameter("LANGUAGEID5", DbType.Decimal, 0, False, TransDegreeLanguageId5)
 
                    .CommandExecute()
              End With

            Catch ex As Exception
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantNivelEnfermedad", "TabDegreeTranslator_Grid1UpdateCommandActionTransDegree", String.Empty)
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
                result = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantNivelEnfermedad", "LookUpForRecordStatus", String.Empty)
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
                result = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "H5MantNivelEnfermedad", "LookUpForLanguageIdTranslator", String.Empty)
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
            <DataMember()> Public Property TabDegreeCollectionDegreeId As System.Double
            <DataMember()> Public Property TabDegreeCollectionRecordStatus As System.Double
            <DataMember()> Public Property TabDegreeCollectionCreatorUserCode As System.Double
            <DataMember()> Public Property TabDegreeCollectionCreationDate As System.DateTime
            <DataMember()> Public Property TabDegreeCollectionUpdateUserCode As System.Double
            <DataMember()> Public Property TabDegreeCollectionUpdateDate As System.DateTime
            <DataMember()> Public Property TabDegreeCollectionDescription As System.String
            <DataMember()> Public Property TabDegreeCollectionShortDescription As System.String
            <DataMember()> Public Property TabDegree_Grid_TabDegree_Item As List(Of TabDegree_Grid_TabDegree_ItemItem)
            <DataMember()> Public Property TabDegreeCollectionLanguageId As System.Double
            <DataMember()> Public Property TabDegreeTranslator_Grid_TabDegree_Item As List(Of TabDegreeTranslator_Grid_TabDegree_ItemItem)

        End Class

        <Serializable()>
        <DataContract()>
        Public Class TabDegree_Grid_TabDegree_ItemItem

            <DataMember()> Public Property DegreeId As System.Double
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
        Public Class TabDegreeTranslator_Grid_TabDegree_ItemItem

            <DataMember()> Public Property DegreeId As System.Double
            <DataMember()> Public Property LanguageId As System.Double
            <DataMember()> Public Property Description As System.String
            <DataMember()> Public Property ShortDescription As System.String

        End Class


#End Region

    End Class

End Namespace

