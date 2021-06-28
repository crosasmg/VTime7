﻿'---------------------------------------------------------------------------------------------------
' <generated>
'     This code was generated by Query Designer v7.2.22.1 at 2020/02/10 12:48:57 PM model release 1, Form Generator v1.0.37.32 - Query Generator v1.0.17.15
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

    Public Class TestPolicyExportActions
        Inherits System.Web.UI.Page

#Region "Actions Methods"
  

        <WebMethod()>
        Public Shared Function ItemsTblDataLoad() As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            Dim response As Object = New With {.NPOLICY = 0, .NBRANCH = 0, .NPRODUCT = 0, .NCERTIF = 0, .NROLE = 0, .SCLIENT = String.Empty}
            Dim selectDataTableItem As DataTable
            Dim responseList As New List(Of Object)
            
            
            Try
                InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("*")

                With New DataManagerFactory("SELECT ROLES.NPOLICY, ROLES.NBRANCH, ROLES.NPRODUCT, ROLES.NCERTIF, ROLES.NROLE, TRIM(ROLES.SCLIENT) SCLIENT FROM ROLES ROLES ", "ROLES", "Linked.LatCombined")

                    .MaxNumberOfRecord = 2000
                    selectDataTableItem = .QueryExecuteToTable(True)
                End With
                With selectDataTableItem
                    If Not IsNothing(.Rows) AndAlso .Rows.Count > 0 Then
                        For Each itemData As DataRow In .Rows
                            response = New With {.NPOLICY = itemData.NumericValue("NPOLICY"), .NBRANCH = itemData.NumericValue("NBRANCH"), .NPRODUCT = itemData.NumericValue("NPRODUCT"), .NCERTIF = itemData.NumericValue("NCERTIF"), .NROLE = itemData.NumericValue("NROLE"), .SCLIENT = itemData.StringValue("SCLIENT")}
                            
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
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "TestPolicyExport", "ItemsTblDataLoad", String.Empty)
            End Try
            
            Return resultData
        End Function


#End Region

#Region "Lookups Web Methods"


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
            <DataMember()> Public Property RootItemsNPOLICY As System.Int32
            <DataMember()> Public Property RootItemsNBRANCH As System.Int32
            <DataMember()> Public Property RootItemsNPRODUCT As System.Int32
            <DataMember()> Public Property RootItemsNCERTIF As System.Int32
            <DataMember()> Public Property RootItemsNROLE As System.Int32
            <DataMember()> Public Property RootItemsSCLIENT As System.String
            <DataMember()> Public Property Items_Item As List(Of Items_ItemItem)

        End Class

        <Serializable()>
        <DataContract()>
        Public Class Items_ItemItem

            <DataMember()> Public Property NPOLICY As System.Int32
            <DataMember()> Public Property NBRANCH As System.Int32
            <DataMember()> Public Property NPRODUCT As System.Int32
            <DataMember()> Public Property NCERTIF As System.Int32
            <DataMember()> Public Property NROLE As System.Int32
            <DataMember()> Public Property SCLIENT As System.String

        End Class


#End Region

    End Class

End Namespace

